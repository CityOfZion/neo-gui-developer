using Neo.Core;
using Neo.SmartContract;
using Neo.VM;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Neo.UI
{
    internal partial class DeployContractDialog : Form
    {
        public DeployContractDialog()
        {
            InitializeComponent();
        }

        public InvocationTransaction GetTransaction()
        {
            byte[] script = textBox8.Text.HexToBytes();
            byte[] parameter_list = textBox6.Text.HexToBytes();
            ContractParameterType return_type = textBox7.Text.HexToBytes().Select(p => (ContractParameterType?)p).FirstOrDefault() ?? ContractParameterType.Void;
            ContractPropertyState properties = ContractPropertyState.NoProperty;
            if (checkBox1.Checked) properties |= ContractPropertyState.HasStorage;
            if (checkBox2.Checked) properties |= ContractPropertyState.HasDynamicInvoke;
            string name = textBox1.Text;
            string version = textBox2.Text;
            string author = textBox3.Text;
            string email = textBox4.Text;
            string description = textBox5.Text;

            InformationBox.Show(script.ToScriptHash().ToString(), "This is the Script Hash for your Smart Contract:", "Script Hash");

            using (ScriptBuilder sb = new ScriptBuilder())
            {
                sb.EmitSysCall("Neo.Contract.Create", script, parameter_list, return_type, properties, name, version, author, email, description);
                return new InvocationTransaction
                {
                    Script = sb.ToArray()
                };
            }
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            button2.Enabled = textBox1.TextLength > 0
                && textBox2.TextLength > 0
                && textBox3.TextLength > 0
                && textBox4.TextLength > 0
                && textBox5.TextLength > 0
                && textBox8.TextLength > 0;
            try
            {
                textBox9.Text = textBox8.Text.HexToBytes().ToScriptHash().ToString();
            }
            catch (FormatException)
            {
                textBox9.Text = "";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() != DialogResult.OK) return;
            textBox8.Text = File.ReadAllBytes(openFileDialog1.FileName).ToHexString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //parent.scListAdd("Deployed ScriptHash", textBox1.Text, textBox8.Text.HexToBytes().ToScriptHash().ToString(), true);
        }

        private void textBox5_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyValue == 9) {
                // tab in description should just jump to the next control
                e.SuppressKeyPress = true;
                SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void DeployContractDialog_Load(object sender, EventArgs e)
        {
            comboBoxParameterCodes.DisplayMember = "Text";
            comboBoxParameterCodes.ValueMember = "Value";

            comboBox1.DisplayMember = "Text";
            comboBox1.ValueMember = "Value";

            var items = new[]
            {
                new { Text = "NEP5", Value = "0710" },
                new { Text = "Signature", Value = "00" },
                new { Text = "Boolean", Value = "01" },
                new { Text = "Integer", Value = "02" },
                new { Text = "Hash160", Value = "03" },
                new { Text = "Hash256", Value = "04" },
                new { Text = "ByteArray", Value = "05" },
                new { Text = "PublicKey", Value = "06" },
                new { Text = "String", Value = "07" },
                new { Text = "Array", Value = "10" },
                new { Text = "InteropInterface", Value = "f0" },
                new { Text = "Void", Value = "ff" }
            };
            
            comboBoxParameterCodes.DataSource = items;
            comboBoxParameterCodes.SelectedValue = "0710";

            comboBox1.DataSource = items.Clone();
            comboBox1.SelectedValue = "05";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox6.Text += comboBoxParameterCodes.SelectedValue;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox7.Text = (string)comboBox1.SelectedValue;
        }
    }
}
