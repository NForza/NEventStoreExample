using System;
using NEventStoreExample.Infrastructure;

namespace NEventStoreExample.Command
{
    public class CreateAccountCommand : ICommand
    {
        public CreateAccountCommand(Guid accountId, string name)
        {
            Id = accountId;
            Name = name;
        }

        public Guid Id { get; private set; }

        public string Name { get; private set; }
    }
}