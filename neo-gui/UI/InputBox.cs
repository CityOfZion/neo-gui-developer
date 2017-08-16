using System.Windows.Forms;
using System.Numerics;
using System;

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
                switch (dialog.comboBox1.GetItemText(dialog.comboBox1.SelectedItem))
                {
                    case "byte[]":
                        try
                        {
                            byte[] byteResult = dialog.textBox1.Text.HexToBytes();
                        }
                        catch (FormatException)
                        {
                            return null;
                        }
                        break;
                    case "BigInteger":
                        if(!BigInteger.TryParse(dialog.textBox1.Text, out BigInteger intResult)) return null;
                        break;
                    case "string":
                        break;
                    default:
                        return null;
                }
                string[] returnString = new string[] { dialog.comboBox1.GetItemText(dialog.comboBox1.SelectedItem), dialog.textBox1.Text };
                return returnString;
            }
        }
    }
}
