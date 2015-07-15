using System;
using NEventStoreExample.Infrastructure;

namespace NEventStoreExample.Event
{
    [Serializable]
    public class AccountCreatedEvent : IEvent
    {
        public AccountCreatedEvent(Guid accountid, string name, bool isActive)
        {
            AccountId = accountid;
            Name = name;
            IsActive = isActive;
        }

        public Guid AccountId { get; private set; }

        public string Name { get; private set; }

        public bool IsActive { get; private set; }
    }
}