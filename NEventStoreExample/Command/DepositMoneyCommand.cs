using System;
using NEventStoreExample.Infrastructure;

namespace NEventStoreExample.Command
{
    public class DepositMoneyCommand : ICommand
    {
        public DepositMoneyCommand(Guid accountId, double amount)
        {
            ID = accountId;
            Amount = amount;
        }

        public Guid ID { get; set; }

        public double Amount { get; private set; }
    }
}