using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Neo.Core;
using System.IO;

namespace Neo.UI
{
    public partial class SmartContractList : Form
    {
        private UInt160 ignore;
        private UInt160 script_hash;

        public SmartContractList()
        {
            InitializeComponent();
        }

        public void AddSmartContract(string scType, string scName, string scScripthash, bool newContract)
        {
            TimeSpan localTime = DateTime.Now.TimeOfDay;
            if (listView1.Items.ContainsKey(scScripthash)) return;
            else
            {
                listView1.Items.Insert(0, new ListViewItem(new[]
                    {
                    new ListViewItem.ListViewSubItem
                    {
                        Name = "Time",
                        Text = localTime.ToString()
                    },
                    new ListViewItem.ListViewSubItem
                    {
                        Name = "Type",
                        Text = scType
                    },
                    new ListViewItem.ListViewSubItem
                    {
                        Name = "Name",
                        Text = "(pending...)",
                        Font = new Font(SystemFonts.DefaultFont, FontStyle.Italic)
                    },
                    new ListViewItem.ListViewSubItem
                    {
                        Name = "Message",
                        Text = scScripthash
                    },
                    new ListViewItem.ListViewSubItem
                    {
                        Name = "Status",
                        Text = "Unavailable ☹",
                        ForeColor = Color.Red
                    }
                }, -1)
                {
                    UseItemStyleForSubItems = false,
                    Name = scScripthash
                });
                if (newContract) System.IO.File.AppendAllText(Application.StartupPath + "\\smartcontracts.txt", scType + "," + scName + "," + scScripthash + Environment.NewLine);
            }
        }
        public void updateStatus()
        {
            foreach(ListViewItem Item in listView1.Items)
            {
                if (!UInt160.TryParse(Item.Name, out ignore)) continue;
                script_hash = UInt160.Parse(Item.Name);
                ContractState contract = Blockchain.Default.GetContract(script_hash);
                if (contract != null)
                {
                    if(Item.SubItems[2].Text != contract.Name) {
                        // don't attempt to redraw unless something has changed (in this case name goes from pending -> contract name)
                        Item.SubItems[2].Text = contract.Name;
                        Item.SubItems[2].Font = SystemFonts.DefaultFont;
                        Item.SubItems[4].Text = "Found! ツ";
                        Item.SubItems[4].ForeColor = Color.Green;
                    }
                }
            }
            }

        private void CopySHtoolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) return;
            Clipboard.SetDataObject(listView1.SelectedItems[0].SubItems[3].Text);
        }

        private void SmartContractList_Load(object sender, EventArgs e)
        {
        }

        private void SmartContractList_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel=true;
        }
    }
}
