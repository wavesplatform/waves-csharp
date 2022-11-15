﻿using Waves.NET.Transactions.Common;

namespace Waves.NET.Transactions
{
    public interface IInvokeScriptTransaction : INonGenesisTransaction
    {
        IRecipient DApp { get; set; }
        ICollection<Payment> Payment { get; set; }
        Call Call { get; set; }
        StateChanges StateChanges { get; set; }
    }
}