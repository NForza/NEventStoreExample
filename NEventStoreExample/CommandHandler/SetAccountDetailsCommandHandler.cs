using System;
using CommonDomain.Persistence;
using NEventStoreExample.Infrastructure;
using NEventStoreExample.Command;
using NEventStoreExample.Model;

namespace NEventStoreExample.CommandHandler
{
    public class SetAccountDetailsCommandHandler : ICommandHandler<SetAccountDetailsCommand>
    {
        private readonly IRepository eventstore;

        public SetAccountDetailsCommandHandler(IRepository eventstore)
        {
            this.eventstore = eventstore;
        }

        public void Handle(SetAccountDetailsCommand command)
        {
            var account = eventstore.GetById<Account>(command.AccountId);
            account.SetDetails(command.Name, command.Address, command.City);
            eventstore.Save(account, Guid.NewGuid());
        }
    }
}
