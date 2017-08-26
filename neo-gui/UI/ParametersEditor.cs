using Neo.Core;
using Neo.Cryptography.ECC;
using Neo.SmartContract;
using System;
using System.Linq;
using System.Numerics;
using System.Windows.Forms;

namespace Neo.UI
{
    internal partial class ParametersEditor : Form
    {
        public ParametersEditor(ContractParameter[] parameters)
        {
            InitializeComponent();
            listView1.Items.AddRange(parameters.Select((p, i) => new ListViewItem(new[]
            {
                new ListViewItem.ListViewSubItem
                {
                    Name = "index",
                    Text = $"[{i}]"
                },
                new ListViewItem.ListViewSubItem
                {
                    Name = "type",
                    Text = p.Type.ToString()
                },
                new ListViewItem.ListViewSubItem
                {
                    Name = "value",
                    Text = GetValueString(p.Value)
                }
            }, -1)
            {
                Tag = p
            }).ToArray());
        }

        private static string GetValueString(object value)
        {
            if (value == null) return "(null)";
            byte[] array = value as byte[];
            string[] array2 = value as string[];
            if (array == null && array2 == null)
                return value.ToString();
            else if (array2 != null)
            {
                string arrayString = "";
                foreach (string stringItem in array2)
                    arrayString += stringItem + ", ";
                return arrayString.Remove(arrayString.Length - 2);
            }
            else
                return array.ToHexString();
        }

        private bool paramSelected = false;
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            paramSelected = (listView1.SelectedIndices.Count > 0);

            if (paramSelected) {
                textBox1.Text = listView1.SelectedItems[0].SubItems["value"].Text;
            } else {
                textBox1.Clear();
            }

            textBox2.Clear();
            // disable parameter value entry until a parameter is selected
            textBox2.Enabled = paramSelected;
        }

        private void paramList_MouseDoubleClick(object sender, MouseEventArgs e) {
            ActiveControl = textBox2;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            bool paramEntered = textBox2.TextLength > 0;

            // disable buttons until a parameter value has been entered
            button1.Enabled = button2.Enabled = paramSelected && paramEntered;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!paramSelected) return;
            ContractParameter parameter = (ContractParameter)listView1.SelectedItems[0].Tag;
            switch (parameter.Type)
            {
                case ContractParameterType.Signature:
                    try
                    {
                        byte[] signature = textBox2.Text.HexToBytes();
                        if (signature.Length != 64) return;
                        parameter.Value = signature;
                    }
                    catch (FormatException)
                    {
                        return;
                    }
                    break;
                case ContractParameterType.Boolean:
                    parameter.Value = string.Equals(textBox2.Text, bool.TrueString, StringComparison.OrdinalIgnoreCase);
                    break;
                case ContractParameterType.Integer:
                    parameter.Value = BigInteger.Parse(textBox2.Text);
                    break;
                case ContractParameterType.Hash160:
                    {
                        UInt160 hash;
                        if (!UInt160.TryParse(textBox2.Text, out hash)) return;
                        parameter.Value = hash;
                    }
                    break;
                case ContractParameterType.Hash256:
                    {
                        UInt256 hash;
                        if (!UInt256.TryParse(textBox2.Text, out hash)) return;
                        parameter.Value = hash;
                    }
                    break;
                case ContractParameterType.ByteArray:
                    try
                    {
                        parameter.Value = textBox2.Text.HexToBytes();
                    }
                    catch (FormatException)
                    {
                        return;
                    }
                    break;
                case ContractParameterType.PublicKey:
                    try
                    {
                        parameter.Value = ECPoint.Parse(textBox2.Text, ECCurve.Secp256r1);
                    }
                    catch (FormatException)
                    {
                        return;
                    }
                    break;
                case ContractParameterType.Array:
                    try
                    {
                        string[] testString = textBox2.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (string stringItem in testString)
                            stringItem.HexToBytes();
                        parameter.Value = testString;
                    }
                    catch (FormatException)
                    {
                        return;
                    }
                    break;
            }
            listView1.SelectedItems[0].SubItems["value"].Text = GetValueString(parameter.Value);
            textBox1.Text = listView1.SelectedItems[0].SubItems["value"].Text;
            textBox2.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!paramSelected) return;
            ContractParameter parameter = (ContractParameter)listView1.SelectedItems[0].Tag;
            switch (parameter.Type)
            {
                case ContractParameterType.ByteArray:
                    string inputString = System.Text.Encoding.UTF8.GetBytes(textBox2.Text).ToHexString();
                    textBox2.Text = inputString;
                    button2.Enabled = false;
                    break;
            }
        }
    }
}
