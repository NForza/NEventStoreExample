using System;
using NEventStoreExample.Infrastructure;

namespace NEventStoreExample.Event
{
    public class AccountHolderMovedEvent  : IEvent
    {
        public Guid AccountId { get; private set; }
        public string Address { get; private set; }
        public string City { get; private set; }

        public AccountHolderMovedEvent(Guid accountId, string address, string city)
        {
            AccountId = accountId;
            Address = address;
            City = city;
        }
    }
}
