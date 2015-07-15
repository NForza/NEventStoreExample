using System;
using CommonDomain.Persistence;
using NEventStoreExample.Command;
using NEventStoreExample.Infrastructure;
using NEventStoreExample.Model;

namespace NEventStoreExample.CommandHandler
{
    public class CreateAccountCommandHandler : ICommandHandler<CreateAccountCommand>
    {
        private readonly IRepository eventstore;

        public CreateAccountCommandHandler(IRepository eventstore)
        {
            this.eventstore = eventstore;
        }

        public void Handle(CreateAccountCommand command)
        {
            AssertNameIsNotEmpty(command.Name);
            AssertGuidIsNotEmpty(command.Id);

            var account = new Account(command.Id, command.Name);
            eventstore.Save(account, Guid.NewGuid());
        }

        private void AssertGuidIsNotEmpty(Guid guid)
        {
            if (guid == Guid.Empty)
            {
                throw new ArgumentException("Guid can't not be empty", "Guid");
            }
        }

        private void AssertNameIsNotEmpty(string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Name can't not be empty", "Name");
            }
        }
    }
}