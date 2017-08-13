﻿using Neo.Core;
using Neo.Wallets;

namespace Neo.UI
{
    internal class TxOutListBoxItem
    {
        public TransactionOutput Output;
        public string AssetName;

        public override string ToString()
        {
            return $"{Wallet.ToAddress(Output.ScriptHash)}\t{Output.Value}\t{AssetName}";
        }
    }
}
