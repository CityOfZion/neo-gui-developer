using System;
using System.Text;
using System.Windows.Forms;
using Neo.VM;
using System.Numerics;
using Neo.Cryptography.ECC;

namespace Neo.UI {
    public partial class ParamsObjectDialog : Form {
        public ParamsObjectDialog() {
            InitializeComponent();
        }

        private void buttonAdd_Click(object sender, EventArgs e) {
            if (listViewParams.Items.Count > 255) {
                MessageBox.Show("Too many arguments not supported!");
                return;
            }
            string[] input = InputBox.ShowParams("Params", "Params");
            if (input == null) {
                return;
            }

            switch (input[0]) {
                case "Signature":
                case "ByteArray":
                    try {
                        byte[] arg = input[1].HexToBytes();
                    } catch (Exception) {
                        return;
                    }
                    break;
                case "Boolean":
                    if (!Boolean.TryParse(input[1], out bool boolResult)) {
                        return;
                    }
                    input[1] = boolResult.ToString();
                    break;
                case "Integer":
                    if (!BigInteger.TryParse(input[1], out BigInteger intResult)) {
                        return;
                    }
                    input[1] = intResult.ToString();
                    break;
                case "Hash160":
                    if (!UInt160.TryParse(input[1], out UInt160 hash160Result)) {
                        return;
                    }
                    break;
                case "Hash256":
                    if (!UInt256.TryParse(input[1], out UInt256 hash256Result)) {
                        return;
                    }
                    break;
                case "PublicKey":
                    try {
                        byte[] ecPoint = ECPoint.Parse(input[1], ECCurve.Secp256r1).EncodePoint(true);
                    } catch (Exception) {
                        return;
                    }
                    break;
                case "String":
                    break;
            }

            AddListItem(input[0], input[1]);
        }

        /**
         * add a new item to the param list
         */
        private void AddListItem(string listType, string listData) {
            listViewParams.Items.Add(new ListViewItem(new[]
{
                    new ListViewItem.ListViewSubItem
                    {
                        Name = "Type",
                        Text = listType
                    },
                    new ListViewItem.ListViewSubItem
                    {
                        Name = "Data",
                        Text = listData
                    }
                    }, -1));
        }

        private void buttonRemove_Click(object sender, EventArgs e) {
            if (listViewParams.SelectedItems.Count == 0) return;
            listViewParams.SelectedItems[0].Remove();
        }

        public string getParams() {
            using (ScriptBuilder sb = new ScriptBuilder()) {
                //sb.EmitPush(listViewParams.Items.Count);
                //sb.Emit(OpCode.NEWARRAY);
                int listIndex = listViewParams.Items.Count - 1;
                while (listIndex >= 0) {
                    ListViewItem item = listViewParams.Items[listIndex];
                    switch (item.SubItems["Type"].Text) {
                        case "Signature":
                        case "ByteArray":
                            sb.EmitPush(item.SubItems["Data"].Text.HexToBytes());
                            break;
                        case "Boolean":
                            sb.EmitPush(Boolean.Parse(item.SubItems["Data"].Text));
                            break;
                        case "Integer":
                            sb.EmitPush(BigInteger.Parse(item.SubItems["Data"].Text));
                            break;
                        case "Hash160":
                            sb.EmitPush(UInt160.Parse(item.SubItems["Data"].Text.ToString()).ToArray());
                            break;
                        case "Hash256":
                            sb.EmitPush(UInt256.Parse(item.SubItems["Data"].Text.ToString().Trim()).ToArray());
                            break;
                        case "PublicKey":
                            sb.EmitPush(ECPoint.Parse(item.SubItems["Data"].Text.ToString(), ECCurve.Secp256r1).EncodePoint(true));
                            break;
                        case "Array":
                            /*
                             foreach(var item in ((object[])parameter.Value).Reverse())
                                sb.EmitPush(((string)item).HexToBytes());
                            sb.EmitPush(((object[])parameter.Value).Length);
                            sb.Emit(OpCode.PACK);
                             */
                            break;
                        case "String":
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