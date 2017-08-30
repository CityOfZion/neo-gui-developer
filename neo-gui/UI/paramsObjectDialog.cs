using System;
using System.Text;
using System.Windows.Forms;
using Neo.VM;
using System.Numerics;

namespace Neo.UI
{
    public partial class ParamsObjectDialog : Form
    {
        public const string BYTE_ARRAY_TYPE = "byte[]";
        public const string BIG_INTEGER_TYPE = "BigInteger";
        public const string STRING_TYPE = "string";
        const string TYPE_LABEL = "Type";
        const string DATA_LABEL = "Data";

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
                case (BYTE_ARRAY_TYPE):
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
                        Name = TYPE_LABEL,
                        Text = BYTE_ARRAY_TYPE
                    },
                    new ListViewItem.ListViewSubItem
                    {
                        Name = DATA_LABEL,
                        Text = input[1]
                    }
                    }, -1));
                    break;
                case (BIG_INTEGER_TYPE):
                    BigInteger.TryParse(input[1], out BigInteger intResult);
                    listViewParams.Items.Add(new ListViewItem(new[]
                    {
                    new ListViewItem.ListViewSubItem
                    {
                        Name = TYPE_LABEL,
                        Text = BIG_INTEGER_TYPE
                    },
                    new ListViewItem.ListViewSubItem
                    {
                        Name = DATA_LABEL,
                        Text = intResult.ToString()
                    }
                    }, -1));
                    break;
                case (STRING_TYPE):
                    listViewParams.Items.Add(new ListViewItem(new[]
{
                    new ListViewItem.ListViewSubItem
                    {
                        Name = TYPE_LABEL,
                        Text = STRING_TYPE
                    },
                    new ListViewItem.ListViewSubItem
                    {
                        Name = DATA_LABEL,
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
                    switch (item.SubItems[TYPE_LABEL].Text)
                    {
                        case BYTE_ARRAY_TYPE:
                            sb.EmitPush(item.SubItems[DATA_LABEL].Text.HexToBytes());
                            break;
                        case BIG_INTEGER_TYPE:
                            sb.EmitPush(BigInteger.Parse(item.SubItems[DATA_LABEL].Text));
                            break;
                        case STRING_TYPE:
                            sb.EmitPush(Encoding.UTF8.GetBytes(item.SubItems[DATA_LABEL].Text));
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
