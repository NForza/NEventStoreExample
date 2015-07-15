using System;

namespace NEventStoreExample.Command
{
    public class AccountHolderMovedCommand
    {
         public AccountHolderMovedCommand(Guid accountid, string address, string city)
        {
            AccountId = accountid;
            Address = address;
            City = city;
        }
        
        public Guid AccountId { get; private set; }
        
        public string Address { get; private set; }
        
        public string City { get; private set; }
    }
}
