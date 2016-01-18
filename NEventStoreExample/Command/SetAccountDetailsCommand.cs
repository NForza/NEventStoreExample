using System;
using NEventStoreExample.Infrastructure;

namespace NEventStoreExample.Command
{
    public class SetAccountDetailsCommand : ICommand
    {
        public SetAccountDetailsCommand(Guid accountid, string name, string address, string city)
        {
            ID = accountid;
            Name = name;
            Address = address;
            City = city;
        }
        
        public Guid ID { get; set; }
        
        public string Name { get; private set; }
        
        public string Address { get; private set; }
        
        public string City { get; private set; }       
    }
}
