using System;
using NEventStoreExample.Infrastructure;

namespace NEventStoreExample.Command
{
    public class CreateAccountCommand : ICommand
    {
        public CreateAccountCommand(Guid accountId, string name)
        {
            ID = accountId;
            Name = name;
        }

        public Guid ID { get; set; }

        public string Name { get; private set; }
    }
}