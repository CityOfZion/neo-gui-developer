using Neo.Core;
using Neo.Cryptography.ECC;
using Neo.SmartContract;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Text;
using System.Windows.Forms;

namespace Neo.UI
{
    public partial class InvokeContractParameterEditor : Form
    {
        public string returnValue;

        public InvokeContractParameterEditor(string paramType, string paramValue, bool paramTypeLocked)
        {
            InitializeComponent();

            // disable the update button until a valid value is entered
            btnAdd.Enabled = false;

            if (paramType == null)
            {
                // parameter type is not supplied, default to string
                paramType = "ByteArray";
            }
            comboTypeSelect.SelectedIndex = comboTypeSelect.FindString(paramType);

            // combobox is locked if paramtype is defined by the smart contract
            comboTypeSelect.Enabled = !paramTypeLocked;

            if (paramValue != null) {
                // parameter value has been supplied, populate radio buttons or txtParam
                if (getSelectedParamType() == ContractParameterType.Boolean)
                {
                    Boolean.TryParse(paramValue, out bool result);
                    radioBtnTrue.Checked = result;
                    radioBtnFalse.Checked = !result;
                } else
                {
                    txtParamValue.Text = paramValue;
                }
            }
        }

        public static ContractParameter ShowParamEditor(string paramType, string paramValue, bool paramTypeLocked)
        {
            using (InvokeContractParameterEditor dialog = new InvokeContractParameterEditor(paramType, paramValue, paramTypeLocked))
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {

                    return new ContractParameter
                    {
                        Type = dialog.getSelectedParamType(),
                        Value = dialog.ParsedParameterValue(dialog.userNewParamValue())
                    };
                }
            }
            return null;
        }

        /**
         * user has chosen a new value from the combobox, update form accordingly
         */
        private void comboTypeSelect_SelectIndexChanged(object sender, EventArgs e)
        {
            string selectedItem = comboTypeSelect.GetItemText(comboTypeSelect.SelectedItem);
            if (selectedItem.Equals("Boolean"))
            {
                tabControl1.SelectedTab = tabPage2;
                btnAdd.Enabled = true;
            }
            else
            {
                chkEncodeHex.Checked = (selectedItem.Equals("ByteArray") || selectedItem.Equals("Signature"));
                txtParamValue.Enabled = !selectedItem.Equals("Array");
                tabControl1.SelectedTab = tabPage1;
                validateUserInput();
            }
        }

        private String hexEncodeString(string paramValue)
        {
            return Encoding.UTF8.GetBytes(paramValue).ToHexString();
        }

        /**
         * test if the value provided by the user is valid for its type
         */
        private void validateUserInput()
        {
            if(chkEncodeHex.Checked)
            {
                txtValuePreview.Text = hexEncodeString(txtParamValue.Text);
            } else
            {
                txtValuePreview.Text = txtParamValue.Text;
            }


            if (paramHasValidFormat(getSelectedParamType(), userNewParamValue()))
            {
                btnAdd.Enabled = true;
                txtValuePreview.ForeColor = Color.Empty;
            }
            else
            {
                btnAdd.Enabled = false;
                txtValuePreview.ForeColor = Color.Red;
            }
            txtValuePreview.BackColor = txtValuePreview.BackColor;

        }

        /**
         * user has changed text, revalidate input
         */
        private void txtParamValue_TextChanged(object sender, EventArgs e)
        {
            validateUserInput();
        }

        /**
         * get the value user has chosen from whichever tabpage is selected
         */
        private string userNewParamValue()
        {
            if (getSelectedParamType() == ContractParameterType.Boolean)
            {
                return radioBtnTrue.Checked ? "true" : "false";
            }
            else
            {
                return txtValuePreview.Text;
            }
        }

        /**
         * parse the string provided by user into the proper format for getSelectedParamType()
         */
        public object ParsedParameterValue(string paramValue)
        {
            switch (getSelectedParamType())
            {
                case ContractParameterType.Array:
                    return new List<ContractParameter>();
                case ContractParameterType.Signature:
                case ContractParameterType.ByteArray:
                    return paramValue.HexToBytes();
                case ContractParameterType.Boolean:
                    return Boolean.Parse(paramValue);
                case ContractParameterType.Integer:
                    return BigInteger.Parse(paramValue);
                case ContractParameterType.Hash160:
                    return UInt160.Parse(paramValue).ToArray();
                case ContractParameterType.Hash256:
                    return UInt256.Parse(paramValue).ToArray();
                case ContractParameterType.PublicKey:
                    return ECPoint.Parse(paramValue, ECCurve.Secp256r1).EncodePoint(true);
                default:
                    return false;
            }
        }

        /**
         * which param type is user adding? determine by combobox selection
         */
        private ContractParameterType getSelectedParamType()
        {
            switch(comboTypeSelect.GetItemText(comboTypeSelect.SelectedItem))
            {
                case "Array":
                    return ContractParameterType.Array;
                case "Boolean":
                    return ContractParameterType.Boolean;
                case "Integer":
                    return ContractParameterType.Integer;
                case "Hash160":
                    return ContractParameterType.Hash160;
                case "Hash256":
                    return ContractParameterType.Hash256;
                case "PublicKey":
                    return ContractParameterType.PublicKey;
                case "Signature":
                case "ByteArray":
                default:
                    return ContractParameterType.ByteArray;
            }
        }

        /**
         * is the value that the user has entered/selected valid for the type
         */
        private static bool paramHasValidFormat(ContractParameterType selectedItem, string userParamValue)
        {
            switch (selectedItem)
            {
                case ContractParameterType.Array:
                    break;
                case ContractParameterType.Signature:
                case ContractParameterType.ByteArray:
                    if(userParamValue.Trim().Length <= 0)
                    {
                        return false;
                    }
                    try
                    {
                        byte[] byteResult = userParamValue.HexToBytes();
                    }
                    catch (FormatException)
                    {
                        return false;
                    }
                    break;
                case ContractParameterType.Boolean:
                    bool selectedVal = userParamValue.Equals("true");
                    break;
                case ContractParameterType.Integer:
                    if (!BigInteger.TryParse(userParamValue, out BigInteger intResult))
                    {
                        return false;
                    }
                    break;
                case ContractParameterType.Hash160:
                    if (!UInt160.TryParse(userParamValue, out UInt160 hash160Result))
                    {
                        return false;
                    }
                    break;
                case ContractParameterType.Hash256:
                    if (!UInt256.TryParse(userParamValue, out UInt256 hash256Result))
                    {
                        return false;
                    }
                    break;
                case ContractParameterType.PublicKey:
                    try
                    {
                        byte[] ecPoint = ECPoint.Parse(userParamValue, ECCurve.Secp256r1).EncodePoint(true);
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                    break;
                default:
                    return false;
            }
            return true;
        }

        /**
         * recalculate text preview on hex toggle change
         */
        private void chkEncodeHex_CheckChanged(object sender, EventArgs e)
        {
            validateUserInput();
        }
    }
}
