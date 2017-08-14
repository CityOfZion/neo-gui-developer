using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Neo.VM;

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
            if (listViewParams.Items.Count > 10)
            {
                MessageBox.Show("Too many arguments not supported!");
                return;
            }
            string input = InputBox.Show("Params", "Params");
            if (string.IsNullOrEmpty(input)) return;
            try
            {
                byte[] arg = input.HexToBytes();
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
                    Text = input
                }
            }, -1));
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            if (listViewParams.SelectedItems.Count == 0) return;
            listViewParams.SelectedItems[0].Remove();
        }

        public string getParams()
        {
            //53c5760003313131c4765103323232c4765203333333c4
            using (ScriptBuilder sb = new ScriptBuilder())
            {
                sb.EmitPush(listViewParams.Items.Count);
                sb.Emit(OpCode.NEWARRAY);
                foreach (ListViewItem item in listViewParams.Items)
                {
                    sb.Emit(OpCode.DUP);
                    if (item.Index == 0) sb.Emit(OpCode.PUSH0);
                    else sb.EmitPush(item.Index);
                    switch (item.SubItems["Type"].Text)
                    {
                        case "byte[]":
                            sb.EmitPush(item.SubItems["Data"].Text.HexToBytes());
                            break;
                    }
                    sb.Emit(OpCode.SETITEM);
                }
                string returnParams = sb.ToArray().ToHexString();
                return returnParams;
            }
        }
    }
}
