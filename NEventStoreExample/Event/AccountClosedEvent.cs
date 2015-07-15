using System;
using NEventStoreExample.Infrastructure;

namespace NEventStoreExample.Event
{
    [Serializable]
    public class AccountClosedEvent : IEvent
    {
        public AccountClosedEvent(Guid accountid, string name)
        {
            AccountId = accountid;
            Name = name;
        }
        public Guid AccountId { get; private set; }
        public string Name { get; private set; }
    }
}