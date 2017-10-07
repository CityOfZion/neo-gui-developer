﻿using Neo.Core;
using Neo.Cryptography.ECC;
using Neo.SmartContract;
using Neo.VM;
using Neo.Wallets;
using System;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace Neo.UI
{
    public partial class AssetRegisterDialog : Form
    {
        public AssetRegisterDialog()
        {
            InitializeComponent();
        }

        public InvocationTransaction GetTransaction()
        {
            AssetType asset_type = (AssetType)comboBox1.SelectedItem;
            string name = string.IsNullOrWhiteSpace(textBox1.Text) ? string.Empty : $"[{{\"lang\":\"{CultureInfo.CurrentCulture.Name}\",\"name\":\"{textBox1.Text}\"}}]";
            Fixed8 amount = checkBox1.Checked ? Fixed8.Parse(textBox2.Text) : -Fixed8.Satoshi;
            byte precision = (byte)numericUpDown1.Value;
            ECPoint owner = (ECPoint)comboBox2.SelectedItem;
            UInt160 admin = Wallet.ToScriptHash(comboBox3.Text);
            UInt160 issuer = Wallet.ToScriptHash(comboBox4.Text);
            using (ScriptBuilder sb = new ScriptBuilder())
            {
                sb.EmitSysCall("Neo.Asset.Create", asset_type, name, amount, precision, owner, admin, issuer);
                return new InvocationTransaction
                {
                    Attributes = new[]
                    {
                        new TransactionAttribute
                        {
                            Usage = TransactionAttributeUsage.Script,
                            Data = Contract.CreateSignatureRedeemScript(owner).ToScriptHash().ToArray()
                        }
                    },
                    Script = sb.ToArray()
                };
            }
        }

        private void AssetRegisterDialog_Load(object sender, EventArgs e)
        {
            comboBox1.Items.AddRange(new object[] { AssetType.Share, AssetType.Token });
            comboBox2.Items.AddRange(Program.CurrentWallet.GetContracts().Where(p => p.IsStandard).Select(p => Program.CurrentWallet.GetKey(p.PublicKeyHash).PublicKey).ToArray());
            comboBox3.Items.AddRange(Program.CurrentWallet.GetContracts().Select(p => p.Address).ToArray());
            comboBox4.Items.AddRange(Program.CurrentWallet.GetContracts().Select(p => p.Address).ToArray());
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            numericUpDown1.Enabled = (AssetType)comboBox1.SelectedItem != AssetType.Share;
            if (!numericUpDown1.Enabled) numericUpDown1.Value = 0;
            CheckForm(sender, e);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            textBox2.Enabled = checkBox1.Checked;
            CheckForm(sender, e);
        }

        private void CheckForm(object sender, EventArgs e)
        {
            bool enabled = comboBox1.SelectedIndex >= 0 &&
                              textBox1.TextLength > 0 &&
                              (!checkBox1.Checked || textBox2.TextLength > 0) &&
                              comboBox2.SelectedIndex >= 0 &&
                              !string.IsNullOrWhiteSpace(comboBox3.Text) &&
                              !string.IsNullOrWhiteSpace(comboBox4.Text);
            if (enabled)
            {
                try
                {
                    Wallet.ToScriptHash(comboBox3.Text);
                    Wallet.ToScriptHash(comboBox4.Text);
                }
                catch (FormatException)
                {
                    enabled = false;
                }
            }
            button1.Enabled = enabled;
        }
    }
}
