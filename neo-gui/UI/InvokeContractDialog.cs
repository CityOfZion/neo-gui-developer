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
        private List<ContractParameter> optionalParameters = new List<ContractParameter>();         // optional params for the contract
        private TreeNode targetTreeNode;                                                            // target tree node to add new params 

        private InvocationTransaction tx;
        private UInt160 scriptHash;

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

        /*
        private void UpdateScript()
        {
            if (parameters.Any(p => p.Value == null)) return;
            using (ScriptBuilder sb = new ScriptBuilder())
            {
                foreach (ContractParameter parameter in parameters.Reverse())
                {
                    switch (parameter.Type)
                    {
                        case ContractParameterType.Signature:
                        case ContractParameterType.ByteArray:
                            sb.EmitPush((byte[])parameter.Value);
                            break;
                        case ContractParameterType.Boolean:
                            sb.EmitPush((bool)parameter.Value);
                            break;
                        case ContractParameterType.Integer:
                            sb.EmitPush((BigInteger)parameter.Value);
                            break;
                        case ContractParameterType.Hash160:
                            sb.EmitPush(((UInt160)parameter.Value).ToArray());
                            break;
                        case ContractParameterType.Hash256:
                            sb.EmitPush(((UInt256)parameter.Value).ToArray());
                            break;
                        case ContractParameterType.PublicKey:
                            sb.EmitPush(((ECPoint)parameter.Value).EncodePoint(true));
                            break;
                        case ContractParameterType.Array:
                            foreach(var item in ((object[])parameter.Value).Reverse())
                                sb.EmitPush(((string)item).HexToBytes());
                            sb.EmitPush(((object[])parameter.Value).Length);
                            sb.Emit(OpCode.PACK);
                            break;
                    }
                }
                sb.EmitAppCall(script_hash.ToArray(), true);
                textBox6.Text = sb.ToArray().ToHexString();
            }
        }
        */

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
            string scriptByteCode = parametersToByteCode(requiredParameters, ScriptPackMethods.EmitAppCall);
            txtCustomScript.Text = scriptByteCode;

            string optionalParamsBytecode = parametersToByteCode(optionalParameters, ScriptPackMethods.EmitOpCodePack);
            if (optionalParamsBytecode != null)
            {
                txtCustomScript.Text = optionalParamsBytecode + txtCustomScript.Text;
            }
            else
            {
                txtCustomScript.Text = "00" + txtCustomScript.Text;

            }
            txtCustomScriptCopy.Text = txtCustomScript.Text;
        }


        /**
         * convert ContractParameter list to byte code
         */
        private string parametersToByteCode(List<ContractParameter> parameterList, ScriptPackMethods scriptPackMethod)
        {
            using (ScriptBuilder sb = new ScriptBuilder())
            {
                int parametersAdded = 0;
                for (int i = parameterList.Count - 1; i >= 0; i--)
                {
                    ContractParameter param = parameterList[i];
                    if (param == null || param.Value == null)
                    {
                        continue;
                    }

                    parametersAdded++;

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
                            sb.EmitPush(parametersToByteCode(arrayList, ScriptPackMethods.EmitOpCodePack).HexToBytes());
                            break;
                    }
                }

                if (parametersAdded <= 0)
                {
                    return null;
                }

                switch (scriptPackMethod)
                {
                    case ScriptPackMethods.EmitAppCall:
                        sb.EmitAppCall(scriptHash.ToArray());
                        break;
                    case ScriptPackMethods.EmitOpCodePack:
                        sb.EmitPush(parametersAdded);
                        sb.Emit(OpCode.PACK);
                        break;
                }

                return sb.ToArray().ToHexString();
            }

        }

        /*
        private void button1_Click(object sender, EventArgs e)
        {
            script_hash = UInt160.Parse(textBox1.Text);
            ContractState contract = Blockchain.Default.GetContract(script_hash);
            if (contract == null) return;
            parameters = contract.Code.ParameterList.Select(p => new ContractParameter { Type = p }).ToArray();
            textBox2.Text = contract.Name;
            textBox3.Text = contract.CodeVersion;
            textBox4.Text = contract.Author;
            textBox5.Text = string.Join(", ", contract.Code.ParameterList);
            button2.Enabled = parameters.Length > 0;
            
            // save the contract hash to the contract list
            MainForm.Instance.scList.scListAdd(textBox1.Text, true);
            UpdateScript();
        }
        */

        /**
         * reset (empty) the form fields
         */
        private void ClearScriptDetails()
        {
            txtName.Text = txtVersion.Text = txtAuthor.Text = txtDescription.Text = txtParamList.Text = txtCustomScript.Text = txtCustomScriptCopy.Text = "";
            txtScriptHash.ForeColor = Color.Empty;
            optionalParameters = new List<ContractParameter>();
            requiredParameters = new List<ContractParameter>();
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
            if (optionalParameters.Count == 0)
            {
                // list doesn't have enough items in it initialise list
                for (int i = 0; i < requiredParameters.Count; i++)
                {
                    optionalParameters.Insert(i, null);
                }
            }


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
            initParmTreeView();

            // todo: restore scList call 
            //MainForm.Instance.scList.scListAdd(scriptHash.ToString(), true);

            btnClearScript.Enabled = true;
            UpdateScript();
        }


        /**
         * show the smart contract list dialog and allow user to choose script hash to populate textfield
         */
        private void btnScriptHashSearch_Click(object sender, EventArgs e)
        {
            InformationBox.Show("https://github.com/neo-project/neo-gui/pull/51", "Smart Contract Address Book Required");
            // todo: restore scList call
            /*
            if (MainForm.Instance.scList.Visible)
            {
                MainForm.Instance.scList.Visible = false;
            }
            MainForm.Instance.scList.ShowDialog();

            ClearScriptDetails();
            txtScriptHash.Text = MainForm.Instance.scList.selectedScriptHash;
            */
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
            testTx = tx;
            testTx.Gas = Fixed8.Satoshi;
            testTx = GetTransaction();
            ////////////////////////EXPERIMENTAL////////////////////////            
            ////////////////////////////////////////////////////////////

            ApplicationEngine engine = new ApplicationEngine(TriggerType.Application, testTx, script_table, service, Fixed8.Zero, true);
            engine.LoadScript(testTx.Script, false);
            
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
         * initialise parameter treeview, populate the list of defined parameters
         */
        private void initParmTreeView()
        {
            treeParamList.Nodes.Add(new TreeNode(Strings.InvokeContractParamList));
            targetTreeNode = treeParamList.Nodes[0];
            targetTreeNode.Nodes.AddRange(requiredParameters.Select((p, i) => new TreeNode($"{p.Type.ToString()}={GetValueString(p.Value)}")
            {
                Tag = new int[] { i, 1 },
                Name = p.Type.ToString()
            }).ToArray());

            targetTreeNode.Nodes.Add(new TreeNode(Strings.InvokeContractParamArray, new TreeNode[] { addParamNodeToTreeView() }) { Name = "ParamsArray" });
            //targetTreeNode.Nodes.Add(addParamNodeToTreeView());
            treeParamList.ExpandAll();

            testRequiredParameterValidity();
        }

        /**
         * add the "add param" node to the end of the tree
         */
        private TreeNode addParamNodeToTreeView()
        {
            return new TreeNode(Strings.InvokeContractAddParam);

        }

        /**
         * test if a required list item parameter is valid - highlight in red if it is not
         */
        private void testRequiredParameterValidity()
        {
            for (int i = 0; i < treeParamList.Nodes[0].Nodes.Count; i++)
            {
                if (i >= requiredParameters.Count)
                {
                    break;
                }
                bool parameterValid = requiredParameters[i].Value != null;
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
            switch (paramType)
            {
                case ContractParameterType.Array:
                    return "Array";
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
         * user has chosen to edit a specific parameter on the treeview
         */
        private void menuItemEdit_Click(object sender, EventArgs e)
        {
            invokeParameterEditor();
        }

        /**
         * user has chosen to remove a parameter from the treeview
         */
        private void menuItemRemove_Click(object sender, EventArgs e)
        {
            optionalParameters[treeParamList.SelectedNode.Index] = null;
            treeParamList.SelectedNode.Remove();

            UpdateScript();
        }

        /**
         * show the parameter editor dialog
         */
        private void invokeParameterEditor()
        {
            TreeNode node = treeParamList.SelectedNode;

            string paramType = null;
            string paramValue = null;
            bool paramIsRequiredField = false;
            bool listItemExists = node.Tag != null;

            // on a double click event, load the contract parameter editor form
            if (listItemExists)
            {
                // user has clicked into a value that already exists - setup to perform update
                listItemExists = true;
                paramIsRequiredField = ((int[])node.Tag)[1] == 1;
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

            string displayValue = GetParameterValueDisplayValue(newParamData.Type, newParamData.Value);
            if (listItemExists)
            {
                node.Name = newParamData.Type.ToString();
                // update existing item
                node.Text = $"{node.Name}=" + displayValue;
            }
            else
            {
                // add a new item to the treeview
                targetTreeNode.Nodes[targetTreeNode.Nodes.Count - 1].Remove();
                TreeNode newTreeNode = new TreeNode($"{newParamData.Type.ToString()}={displayValue}")
                {
                    Tag = new int[] { targetTreeNode.Nodes.Count, 0 },
                    Name = newParamData.Type.ToString()
                };

                if (newParamData.Type.ToString().Equals("Array"))
                {
                    // add array as a new parent node
                    newTreeNode = new TreeNode("Array", new TreeNode[] { addParamNodeToTreeView() }) { Name = "Array" };
                    newTreeNode.Expand();
                }
                targetTreeNode.Nodes.Add(newTreeNode);
                targetTreeNode.Nodes.Add(addParamNodeToTreeView());

                ActiveControl = btnTestScript;
            }

            if (paramIsRequiredField)
            {
                requiredParameters[node.Index].Value = newParamData.Value;
            }
            else
            {
                // default target list to optionalParameters
                List<ContractParameter> targetList = optionalParameters;

                if (targetTreeNode.Name.Equals("Array"))
                {
                    // user has added a parameter to an array item - add ContractParameter to list instead
                    targetList = (List<ContractParameter>)optionalParameters[targetTreeNode.Index].Value;
                }

                if (targetList.Count > node.Index && targetList[node.Index] != null)
                {
                    // incase this is an update, remove existing ContractParameter before adding again
                    targetList.RemoveAt(node.Index);
                }
                targetList.Insert(node.Index, newParamData);
            }

            // highlight any required fields that aren't valid
            testRequiredParameterValidity();

            // update the scripts bytecode to reflect parameter changes
            UpdateScript();

        }

        /**
         * determine if the parameter treeview node is a required field
         */
        private bool paramNodeIsRequiredField(TreeNode node)
        {
            return node.Tag != null && ((int[])node.Tag)[1] == 1;
        }

        /**
         * user is clicking on the param list treeview
         */
        private void treeParamList_MouseDown(object sender, MouseEventArgs e)
        {
            treeParamList.SelectedNode = treeParamList.GetNodeAt(e.X, e.Y);
            if (treeParamList.SelectedNode == null)
            {
                return;
            }

            if (e.Button == MouseButtons.Left && e.Clicks == 2)
            {
                targetTreeNode = treeParamList.SelectedNode.Parent;
                invokeParameterEditor();
            }
            else if (e.Button == MouseButtons.Right && e.Clicks == 1)
            {
                if (treeParamList.SelectedNode.Text.Contains(Strings.InvokeContractAddParam)            // add param
                    || treeParamList.SelectedNode.Text.Contains(Strings.InvokeContractParamArray)       // param array
                    || treeParamList.SelectedNode == treeParamList.Nodes[0])                            // params list
                {
                    // don't allow removal of special tree nodes
                    return;
                }

                menuItemRemove.Enabled = true;
                menuParamListViewItem.Show(Cursor.Position);
                menuItemRemove.Enabled = !paramNodeIsRequiredField(treeParamList.SelectedNode);
            }
        }

        private void btnLoadAVM_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() != DialogResult.OK) return;
            txtCustomScript.Text = File.ReadAllBytes(openFileDialog1.FileName).ToHexString();
        }

        private void buttonParams_Click(object sender, EventArgs e)
        {
            using (ParamsObjectDialog dialog = new ParamsObjectDialog())
            {
                if (dialog.ShowDialog() != DialogResult.OK) return;
                if (dialog.getParams() != null) textBox6.Text = dialog.getParams() + textBox6.Text;
                else textBox6.Text = "00" + textBox6.Text;
            }
        }
    }
}
