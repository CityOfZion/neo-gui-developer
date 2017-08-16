using System;
using System.Text;
using System.Windows.Forms;
using Neo.VM;
using System.Numerics;

namespace Neo.UI
{
    public partial class ParamsObjectDialog : Form
    {
        public ParamsObjectDialog()
        {
            InitializeComponent();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (listViewParams.Items.Count > 255)
            {
                MessageBox.Show("Too many arguments not supported!");
                return;
            }
            string[] input = InputBox.ShowParams("Params", "Params");
            if (input==null) return;
            switch (input[0])
            {
                case ("byte[]"):
                    try
                    {
                        byte[] arg = input[1].HexToBytes();
                    }
                    catch (FormatException)
                    {
                        return;
                    }
                    listViewParams.Items.Add(new ListViewItem(new[]
                    {
                    new ListViewItem.ListViewSubItem
                    {
                        Name = "Type",
                        Text = "byte[]"
                    },
                    new ListViewItem.ListViewSubItem
                    {
                        Name = "Data",
                        Text = input[1]
                    }
                    }, -1));
                    break;
                case ("BigInteger"):
                    BigInteger.TryParse(input[1], out BigInteger intResult);
                    listViewParams.Items.Add(new ListViewItem(new[]
                    {
                    new ListViewItem.ListViewSubItem
                    {
                        Name = "Type",
                        Text = "BigInteger"
                    },
                    new ListViewItem.ListViewSubItem
                    {
                        Name = "Data",
                        Text = intResult.ToString()
                    }
                    }, -1));
                    break;
                case ("string"):
                    listViewParams.Items.Add(new ListViewItem(new[]
{
                    new ListViewItem.ListViewSubItem
                    {
                        Name = "Type",
                        Text = "string"
                    },
                    new ListViewItem.ListViewSubItem
                    {
                        Name = "Data",
                        Text = input[1]
                    }
                    }, -1));
                    break;
            }
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            if (listViewParams.SelectedItems.Count == 0) return;
            listViewParams.SelectedItems[0].Remove();
        }

        public string getParams()
        {
            using (ScriptBuilder sb = new ScriptBuilder())
            {
                //sb.EmitPush(listViewParams.Items.Count);
                //sb.Emit(OpCode.NEWARRAY);
                int listIndex = listViewParams.Items.Count - 1;
                while (listIndex >= 0)
                {
                    ListViewItem item = listViewParams.Items[listIndex];
                    switch (item.SubItems["Type"].Text)
                    {
                        case "byte[]":
                            sb.EmitPush(item.SubItems["Data"].Text.HexToBytes());
                            break;
                        case "BigInteger":
                            sb.EmitPush(BigInteger.Parse(item.SubItems["Data"].Text));
                            break;
                        case "string":
                            sb.EmitPush(Encoding.UTF8.GetBytes(item.SubItems["Data"].Text));
                            break;
                    }
                    listIndex--;
                }
                sb.EmitPush(listViewParams.Items.Count);
                sb.Emit(OpCode.PACK);
                return sb.ToArray().ToHexString();
            }
        }
    }
}
