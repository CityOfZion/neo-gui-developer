using System.Windows.Forms;
using System.Numerics;
using System;
using Neo.Cryptography.ECC;

namespace Neo.UI
{
    internal partial class InputBox : Form
    {
        private InputBox(string text, string caption, string content)
        {
            InitializeComponent();
            this.Text = caption;
            groupBox1.Text = text;
            textBox1.Text = content;
        }

        public static string Show(string text, string caption, string content = "")
        {
            using (InputBox dialog = new InputBox(text, caption, content))
            {
                if (dialog.ShowDialog() != DialogResult.OK) return null;
                return dialog.textBox1.Text;
            }
        }

        public static string[] ShowParams(string text, string caption, string content = "")
        {
            using (InputBox dialog = new InputBox(text, caption, content))
            {
                dialog.comboBox1.Enabled = true;
                dialog.comboBox1.Visible = true;
                if (dialog.ShowDialog() != DialogResult.OK) return null;

                string selectedItem = dialog.comboBox1.GetItemText(dialog.comboBox1.SelectedItem);
                switch (selectedItem)
                {
                    case "Array":
                        MessageBox.Show("not yet implemented array");
                        return null;
                        break;
                    case "Signature":
                    case "ByteArray":
                        try
                        {
                            byte[] byteResult = dialog.textBox1.Text.HexToBytes();
                        }
                        catch (FormatException)
                        {
                            return null;
                        }
                        break;
                    case "Boolean":
                        if (!Boolean.TryParse(dialog.textBox1.Text, out bool boolResult)) {
                            return null;
                        }
                        break;
                    case "Integer":
                        if (!BigInteger.TryParse(dialog.textBox1.Text, out BigInteger intResult)) {
                            return null;
                        }
                        break;
                    case "Hash160":
                        if (!UInt160.TryParse(dialog.textBox1.Text, out UInt160 hash160Result)) {
                            return null;
                        }
                        break;
                    case "Hash256":
                        if (!UInt256.TryParse(dialog.textBox1.Text, out UInt256 hash256Result)) {
                            return null;
                        }
                        break;
                    case "PublicKey":
                        try {
                            byte[] ecPoint = ECPoint.Parse(dialog.textBox1.Text, ECCurve.Secp256r1).EncodePoint(true);
                        } catch (Exception) {
                            return null;
                        }
                        break;
                    case "String":
                        break;
                    default:
                        return null;
                }
                string[] returnString = new string[] { selectedItem, dialog.textBox1.Text };
                return returnString;
            }
        }

        private void comboBox_Enter(object sender, EventArgs e) {
            comboBox1.DroppedDown = true;
        }

        private void comboBox_Leave(object sender, EventArgs e) {
            comboBox1.DroppedDown = false;
        }
    }
}
