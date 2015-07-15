using System;
using NEventStoreExample.Infrastructure;

namespace NEventStoreExample.Command
{
    public class DepositMoneyCommand : ICommand
    {
        public DepositMoneyCommand(Guid accountId, double amount)
        {
            AccountId = accountId;
            Amount = amount;
        }

        public Guid AccountId { get; private set; }

        public double Amount { get; private set; }
    }
}