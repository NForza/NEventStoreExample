using System;
using NEventStoreExample.Infrastructure;

namespace NEventStoreExample.Event
{
    public class AccountDetailsSetEvent: IEvent
    {
        public Guid AccountId { get; private set; }
        public string Name { get; private set; }
        public string Address { get; private set; }
        public string City { get; private set; }

        public AccountDetailsSetEvent(Guid accountid, string name, string address, string city)
        {            
            AccountId = accountid;
            Name = name;
            Address = address;
            City = city;
        }
    }
}
