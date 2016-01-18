using System;
using CommonDomain.Persistence;
using NEventStoreExample.Command;
using NEventStoreExample.Infrastructure;
using NEventStoreExample.Model;
using System.Collections.Generic;
using MemBus.Support;
using NEventStoreExample.Event;

namespace NEventStoreExample.CommandHandler
{
    public class CreateAccountCommandHandler : ICommandHandler<CreateAccountCommand>
    {
        public IEnumerable<IEvent> Handle(CreateAccountCommand command)
        {
            AssertNameIsNotEmpty(command.Name);
            AssertGuidIsNotEmpty(command.ID);

            return new AccountCreatedEvent(command.ID, command.Name, true).AsEnumerable();
        }

        private void AssertGuidIsNotEmpty(Guid guid)
        {
            if (guid == Guid.Empty)
            {
                throw new ArgumentException("Guid can't not be empty", nameof(guid));
            }
        }

        private void AssertNameIsNotEmpty(string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Name can't not be empty", nameof(name));
            }
        }
    }
}