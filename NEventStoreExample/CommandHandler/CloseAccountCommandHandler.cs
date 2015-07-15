using System;
using CommonDomain.Persistence;
using NEventStoreExample.Command;
using NEventStoreExample.Infrastructure;
using NEventStoreExample.Model;

namespace NEventStoreExample.CommandHandler
{
    public class CloseAccountCommandHandler : ICommandHandler<CloseAccountCommand>
    {
        private readonly IRepository eventstore;

        public CloseAccountCommandHandler(IRepository eventstore)
        {
            this.eventstore = eventstore;
        }

        public void Handle(CloseAccountCommand command)
        {
            var account = eventstore.GetById<Account>(command.AccountId);
            account.Close();
            eventstore.Save(account, Guid.NewGuid());
        }
    }
}