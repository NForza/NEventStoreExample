using NEventStoreExample.Infrastructure;
using System;

namespace NEventStoreExample.Command
{
    public class AccountHolderMovedCommand: ICommand
    {
        public AccountHolderMovedCommand(Guid accountid, string address, string city)
        {
            ID = accountid;
            Address = address;
            City = city;
        }

        public Guid ID { get; set; }

        public string Address { get; private set; }

        public string City { get; private set; }
    }
}
