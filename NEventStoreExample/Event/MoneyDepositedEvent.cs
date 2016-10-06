using System;
using NEventStoreExample.Infrastructure;

namespace NEventStoreExample.Event
{
    [Serializable]
    public class MoneyDepositedEvent : IEvent
    {
        public MoneyDepositedEvent(Guid id, double amount, double newAccountAmount)
        {
            NewAccountAmount = newAccountAmount;
            AccountId = id;
            Amount = amount;
        }
        public Guid AccountId { get; private set; }
        public double Amount { get; private set; }
        public double NewAccountAmount { get; private set; }
    }
}