using Neo.Core;
using Neo.Cryptography.ECC;
using Neo.Implementations.Blockchains.LevelDB;
using Neo.IO.Caching;
using Neo.Properties;
using Neo.SmartContract;
using Neo.VM;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Windows.Forms;

namespace Neo.UI
{
    internal partial class InvokeContractDialog : Form
    {
        private List<ContractParameter> requiredParameters = new List<ContractParameter>();         // required params defined by the smartcontract
        private TreeNode targetTreeNode;                                                            // target tree node to add new params 

        private InvocationTransaction tx;
        private UInt160 scriptHash;
        private int numRequiredParameters = 0;                                                      // the number of required parameters for loaded contract
        private enum RequiredParameters { Required, Optional }

        public InvokeContractDialog(InvocationTransaction tx = null, string deployedScriptHash = null)
        {
            InitializeComponent();
            this.tx = tx;
            if (tx != null)
            {
                // transaction will be supplied when being called from another dialog such as deploy contract
                tabControl1.SelectedTab = tabCustom;
                txtCustomScriptHash.Text = deployedScriptHash;
                txtCustomScript.Text = tx.Script.ToHexString();
                txtCustomScriptCopy.Text = txtCustomScript.Text;
            }
        }

        public InvocationTransaction GetTransaction()
        {
            return Program.CurrentWallet.MakeTransaction(new InvocationTransaction
            {
                Version = tx.Version,
                Script = tx.Script,
                Gas = tx.Gas,
                Attributes = tx.Attributes,
                Inputs = tx.Inputs,
                Outputs = tx.Outputs
            });
        }

        private enum ScriptPackMethods
        {
            EmitAppCall,
            EmitOpCodePack,
            NoAction
        }
        /**
         * script has changed, update the bytecode
         */
        private void UpdateScript()
        {
            BuildRequiredParameterCollection();

            // highlight any required fields that aren't valid
            testRequiredParameterValidity();

            using (ScriptBuilder sb = new ScriptBuilder())
            {
                PushParameters(sb, requiredParameters);
                sb.EmitAppCall(scriptHash.ToArray(), false);
                txtCustomScript.Text = sb.ToArray().ToHexString();
                //txtCustomScript.Text = txtCustomScript.Text;
            }
            txtCustomScriptCopy.Text = txtCustomScript.Text;
        }

        private void BuildRequiredParameterCollection()
        {
            requiredParameters = BuildRequiredParameterCollectionArray(treeParamList.Nodes[0]);
        }

        private List<ContractParameter> BuildRequiredParameterCollectionArray(TreeNode nodeArray)
        {
            List<ContractParameter> paramArray = new List<ContractParameter>();
            for (int i = 0; i < nodeArray.Nodes.Count; i++)
            {
                TreeNode node = nodeArray.Nodes[i];
                if(node.Name.Equals(Strings.InvokeContractAddParam))
                {
                    // don't try and add 'add param' nodes
                    continue;
                }

                if (node.Name.Equals(Strings.InvokeContractParamArray))
                {
                    // node is an array
                    paramArray.Add(new ContractParameter {
                        Type = ContractParameterType.Array,
                        Value = BuildRequiredParameterCollectionArray(node)
                    });
                }
                else
                {
                    object[] nodeTag = ((object[])node.Tag);
                    if (nodeTag != null)
                    {
                        ContractParameter param = (ContractParameter)nodeTag[1];
                        paramArray.Add(param);
                    }
                }
            }
            return paramArray;
        }

        /**
         * convert ContractParameter list to byte code
         * updated to reflect code added by @erik in neo-project/neo-gui/nep-5 branch
         */
        private void PushParameters(ScriptBuilder sb, List<ContractParameter> parameterList)
        {
            for (int i = parameterList.Count - 1; i >= 0; i--)
            {
                ContractParameter param = parameterList[i];
                if (param == null || param.Value == null)
                {
                    continue;
                }

                switch (param.Type)
                {
                    case ContractParameterType.Signature:
                    case ContractParameterType.ByteArray:
                    case ContractParameterType.Hash160:
                    case ContractParameterType.Hash256:
                    case ContractParameterType.PublicKey:
                        sb.EmitPush((byte[])param.Value);
                        break;
                    case ContractParameterType.Boolean:
                        sb.EmitPush((bool)param.Value);
                        break;
                    case ContractParameterType.Integer:
                        sb.EmitPush((BigInteger)param.Value);
                        break;
                    case ContractParameterType.Array:
                        List<ContractParameter> arrayList = ((List<ContractParameter>)param.Value);
                        PushParameters(sb, arrayList);
                        sb.EmitPush(arrayList.Count);
                        sb.Emit(OpCode.PACK);
                        break;
                }
            }
        }

        /**
         * reset (empty) the form fields
         */
        private void ClearScriptDetails()
        {
            txtName.Text = txtVersion.Text = txtAuthor.Text = txtDescription.Text = txtParamList.Text = txtCustomScript.Text = txtCustomScriptCopy.Text = "";
            txtScriptHash.ForeColor = Color.Empty;
            requiredParameters = new List<ContractParameter>();
            numRequiredParameters = 0;
            treeParamList.Nodes.Clear();
        }

        /**
         * the value of scripthash has changed - attempt to evaluate and display contract details
         */
        private void txtScriptHash_TextChanged(object sender, EventArgs e)
        {
            ClearScriptDetails();

            if (txtScriptHash.Text.Trim().Equals(""))
            {
                // no scripthash has been provided - clenup just in case previous script values are still showing
                return;
            }

            if (!UInt160.TryParse(txtScriptHash.Text, out UInt160 parsedHash))
            {
                // invalid script hash, reset script details form and highlight field with red text
                txtScriptHash.ForeColor = Color.Red;
                return;
            }

            scriptHash = parsedHash;
            txtScriptHash.ForeColor = Color.Empty;

            ContractState contract = Blockchain.Default.GetContract(scriptHash);
            if (contract == null) return;

            // valid script hash was found on blockchain
            requiredParameters.AddRange(contract.Code.ParameterList.Select(p => new ContractParameter { Type = p }));
            requiredParameters.Add(new ContractParameter { Type = ContractParameterType.Array, Value = new List<ContractParameter>() });
            numRequiredParameters = requiredParameters.Count - 1;

            // populate contract details to form
            txtName.Text = contract.Name;
            txtVersion.Text = contract.CodeVersion;
            txtAuthor.Text = contract.Author;
            if (!contract.Email.Trim().Equals(""))
            {
                // append email address to author field
                txtAuthor.Text += $" ({contract.Email})";
            }
            txtDescription.Text = contract.Description;
            txtParamList.Text = string.Join(", ", contract.Code.ParameterList);

            // show any required parameters for this contract
            InitParmTreeView();

            MainForm.Instance.scList.scListAdd(scriptHash.ToString(), true);

            btnClearScript.Enabled = true;
            UpdateScript();
        }


        /**
         * show the smart contract list dialog and allow user to choose script hash to populate textfield
         */
        private void btnScriptHashSearch_Click(object sender, EventArgs e)
        {
            if (MainForm.Instance.scList.Visible)
            {
                MainForm.Instance.scList.Visible = false;
            }
            MainForm.Instance.scList.ShowDialog();

            ClearScriptDetails();
            txtScriptHash.Text = MainForm.Instance.scList.selectedScriptHash;
        }

        /**
         * don't allow user to invoke script again until they have tested it
         */
        private void txtCustomScript_TextChanged(object sender, EventArgs e)
        {
            btnInvoke.Enabled = false;
            btnTestScript.Enabled = txtCustomScript.TextLength > 0;
        }

        /**
         * run script through ApplicationEngine to determine gas price and bytecode validity
         */
        private void btnTestScript_Click(object sender, EventArgs e)
        {
            if (tx == null) tx = new InvocationTransaction();
            tx.Version = 1;
            tx.Script = txtCustomScriptCopy.Text.HexToBytes();
            if (tx.Attributes == null) tx.Attributes = new TransactionAttribute[0];
            if (tx.Inputs == null) tx.Inputs = new CoinReference[0];
            if (tx.Outputs == null) tx.Outputs = new TransactionOutput[0];
            if (tx.Scripts == null) tx.Scripts = new Witness[0];
            LevelDBBlockchain blockchain = (LevelDBBlockchain)Blockchain.Default;
            DataCache<UInt160, AccountState> accounts = blockchain.GetTable<UInt160, AccountState>();
            DataCache<ECPoint, ValidatorState> validators = blockchain.GetTable<ECPoint, ValidatorState>();
            DataCache<UInt256, AssetState> assets = blockchain.GetTable<UInt256, AssetState>();
            DataCache<UInt160, ContractState> contracts = blockchain.GetTable<UInt160, ContractState>();
            DataCache<StorageKey, StorageItem> storages = blockchain.GetTable<StorageKey, StorageItem>();
            CachedScriptTable script_table = new CachedScriptTable(contracts);
            StateMachine service = new StateMachine(accounts, validators, assets, contracts, storages);

            ////////////////////////////////////////////////////////////
            ////////////////////////EXPERIMENTAL////////////////////////
            //testTx = tx;
            //testTx.Gas = Fixed8.Satoshi;
            //testTx = GetTransaction();
            //ApplicationEngine engine = new ApplicationEngine(TriggerType.Application, testTx, script_table, service, Fixed8.Zero, true);
            //engine.LoadScript(testTx.Script, false);
            ////////////////////////EXPERIMENTAL////////////////////////            
            ////////////////////////////////////////////////////////////

            ApplicationEngine engine = new ApplicationEngine(TriggerType.Application, tx, script_table, service, Fixed8.Zero, true);
            engine.LoadScript(tx.Script, false);

            if (engine.Execute())
            {
                tx.Gas = engine.GasConsumed - Fixed8.FromDecimal(10);
                if (tx.Gas < Fixed8.One) tx.Gas = Fixed8.One;
                tx.Gas = tx.Gas.Ceiling();
                label7.Text = tx.Gas + " gas";
                btnInvoke.Enabled = true;
                if (engine.EvaluationStack.Count != 0)
                {
                    if (engine.EvaluationStack.Peek().ToString() != "Neo.VM.Types.InteropInterface" && engine.EvaluationStack.Peek().ToString() != "Neo.VM.Types.Array")
                    {
                        MessageBox.Show(
                            "Hex: " + engine.EvaluationStack.Peek().GetByteArray().ToHexString() + "\n"
                            + "String: " + System.Text.Encoding.UTF8.GetString(engine.EvaluationStack.Peek().GetByteArray()) + "\n"
                            + "BigInt: " + new BigInteger(engine.EvaluationStack.Peek().GetByteArray()),
                            "Return from Test");
                    }
                }
            }
            else
            {
                MessageBox.Show(Strings.ExecutionFailed);
            }
        }


        /**
         * clear loaded contract details
         */
        private void btnClearScript_Click(object sender, EventArgs e)
        {
            ClearScriptDetails();
            btnClearScript.Enabled = false;
        }

        /**
         * test if a required list item parameter is valid - highlight in red if it is not
         */
        private void testRequiredParameterValidity()
        {
            for (int i = 0; i < treeParamList.Nodes[0].Nodes.Count; i++)
            {
                if (i >= numRequiredParameters)
                {
                    break;
                }
                bool parameterValid = requiredParameters[i].Value != null;
                btnTestScript.Enabled = parameterValid;
                btnInvoke.Enabled = parameterValid;
                treeParamList.Nodes[0].Nodes[i].ForeColor = parameterValid ? Color.Empty : Color.Red;
            }
        }

        /**
         * parse parameter value for something nice to display
         */
        private static string GetValueString(object value)
        {
            if (value == null)
            {
                return Strings.InvokeContractNullParamValue;
            }
            byte[] array = value as byte[];
            string[] array2 = value as string[];
            if (array == null && array2 == null)
            {
                return value.ToString();
            }
            else if (array2 != null)
            {
                string arrayString = "";
                foreach (string stringItem in array2)
                    arrayString += stringItem + ", ";
                return arrayString.Remove(arrayString.Length - 2);
            }
            else
            {
                return array.ToHexString();
            }
        }

        /**
         * after a value has been entered by the user - display something nice for them
         */
        public string GetParameterValueDisplayValue(ContractParameterType paramType, object paramValue)
        {
            if (paramValue == null)
            {
                return null;
            }

            switch (paramType)
            {
                case ContractParameterType.Signature:
                case ContractParameterType.ByteArray:
                    return System.Text.Encoding.UTF8.GetString((byte[])paramValue);
                case ContractParameterType.Boolean:
                case ContractParameterType.Integer:
                    return paramValue.ToString();
                case ContractParameterType.PublicKey:
                    return ((byte[])paramValue).ToHexString();
                case ContractParameterType.Hash160:
                    return (new UInt160((byte[])paramValue).ToString());
                case ContractParameterType.Hash256:
                    return (new UInt256((byte[])paramValue).ToString());
                default:
                    return null;
            }
        }
        /**
         * initialise parameter treeview, populate the list of defined parameters
         */
        private void InitParmTreeView()
        {
            treeParamList.Nodes.Add(new TreeNode(Strings.InvokeContractParamList));

            targetTreeNode = treeParamList.Nodes[0];
            targetTreeNode.Nodes.AddRange(requiredParameters.Select((p, i) => CreateTreeNode(p, RequiredParameters.Required)).ToArray());
            treeParamList.ExpandAll();

            testRequiredParameterValidity();
        }

        private TreeNode CreateTreeNode(ContractParameter param, RequiredParameters requiredField)
        {
            if (param.Type.Equals(ContractParameterType.Array))
            {
                return new TreeNode(Strings.InvokeContractParamArray, new TreeNode[] { AddParamNodeToTreeView() })
                {
                    Name = Strings.InvokeContractParamArray,
                    Tag = new object[] { requiredField, param }
                };
            }

            return new TreeNode($"{param.Type.ToString()}={GetValueString(GetParameterValueDisplayValue(param.Type, param.Value))}")
            {
                Tag = new object[] { requiredField, param },
                Name = param.Type.ToString()
            };
        }
        /**
         * add the "add param" node to the end of the tree
         */
        private TreeNode AddParamNodeToTreeView()
        {
            return new TreeNode(Strings.InvokeContractAddParam) { Name = Strings.InvokeContractAddParam, Tag = new object[2] };
        }

        /**
         * show the parameter editor dialog
         */
        private void InvokeParameterEditor()
        {
            TreeNode node = treeParamList.SelectedNode;

            string paramType = null;
            string paramValue = null;
            bool paramIsRequiredField = false;
            bool listItemExists = node.Tag != null && node.Name != Strings.InvokeContractAddParam;

            if (node.Name.Equals(Strings.InvokeContractParamArray))
            {
                // ignore double clicks on param array node
                return;
            }

            // on a double click event, load the contract parameter editor form
            if (listItemExists)
            {
                // user has clicked into a value that already exists - setup to perform update
                listItemExists = true;
                paramIsRequiredField = ParamNodeIsRequiredField(node);
                paramType = node.Name.ToString();
                paramValue = node.Text.Replace($"{paramType}=", "");
                if (paramValue.Equals(Strings.InvokeContractNullParamValue))
                {
                    paramValue = null;
                }
            }

            ContractParameter newParamData = InvokeContractParameterEditor.ShowParamEditor(paramType, paramValue, paramIsRequiredField);
            if (newParamData == null)
            {
                // dialog was probably cancelled
                return;
            }

            TreeNode newTreeNode;

            if (newParamData.Type.Equals(ContractParameterType.Array))
            {
                // add array as a new parent node
                newTreeNode = new TreeNode("Array", new TreeNode[] { AddParamNodeToTreeView() })
                {
                    Name = Strings.InvokeContractParamArray,
                    Tag = new object[] {
                        RequiredParameters.Optional,
                        new List<ContractParameter>()
                    }
                };
                newTreeNode.Expand();
            }
            else
            {
                newTreeNode = CreateTreeNode(newParamData, RequiredParameters.Optional);
            }

            TreeNodeCollection parentNode = node.Parent.Nodes;
            int selectedNodeIndex = parentNode.IndexOf(node);

            if (listItemExists)
            {
                // replace existing tree item with new one (incase user changes type)
                parentNode.RemoveAt(selectedNodeIndex);
            }
            parentNode.Insert(selectedNodeIndex, newTreeNode);

            // update the scripts bytecode to reflect parameter changes
            UpdateScript();

        }

        /**
         * determine if the parameter treeview node is a required field
         */
        private bool ParamNodeIsRequiredField(TreeNode node)
        {
            return node.Tag != null && ((RequiredParameters)((object[])node.Tag)[0]) == RequiredParameters.Required;
        }

        /**
         * user is clicking on the param list treeview
         */
        private void treeParamList_MouseDown(object sender, MouseEventArgs e)
        {
            treeParamList.SelectedNode = treeParamList.GetNodeAt(e.X, e.Y);
            TreeNode selectedNode = treeParamList.SelectedNode;

            if (selectedNode == null || selectedNode.Tag == null)
            {
                return;
            }

            if (e.Button == MouseButtons.Left && e.Clicks == 2)
            {
                targetTreeNode = selectedNode.Parent;
                InvokeParameterEditor();
            }
            else if (e.Button == MouseButtons.Right && e.Clicks == 1)
            {
                if (selectedNode.Text.Contains(Strings.InvokeContractAddParam)            // add param
                    || selectedNode.Text.Contains(Strings.InvokeContractParamArray)       // param array
                    || selectedNode == treeParamList.Nodes[0])                            // params list
                {
                    // don't show a context menu for special tree nodes
                    return;
                }


                menuParamListViewItem.Show(Cursor.Position);
                menuItemRemove.Enabled = !ParamNodeIsRequiredField(selectedNode);                   // don't allow removal of required fields
                menuItemEdit.Enabled = !selectedNode.Name.Equals(Strings.InvokeContractParamArray); // don't allow editing of array items
            }
        }

        /**
         * user has chosen to edit a specific parameter on the treeview
         */
        private void MenuItemEdit_Click(object sender, EventArgs e)
        {
            InvokeParameterEditor();
        }

        /**
         * user has chosen to remove a parameter from the treeview
         */
        private void MenuItemRemove_Click(object sender, EventArgs e)
        {
            treeParamList.SelectedNode.Remove();

            UpdateScript();
        }

        private void btnLoadAVM_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() != DialogResult.OK) return;
            txtCustomScript.Text = File.ReadAllBytes(openFileDialog1.FileName).ToHexString();
        }
    }
}
