using System;
using NEventStoreExample.Infrastructure;

namespace NEventStoreExample.Command
{
    public class SetAccountDetailsCommand : ICommand
    {
        public SetAccountDetailsCommand(Guid accountid, string name, string address, string city)
        {
            AccountId = accountid;
            Name = name;
            Address = address;
            City = city;
        }
        
        public Guid AccountId { get; private set; }
        
        public string Name { get; private set; }
        
        public string Address { get; private set; }
        
        public string City { get; private set; }       
    }
}
