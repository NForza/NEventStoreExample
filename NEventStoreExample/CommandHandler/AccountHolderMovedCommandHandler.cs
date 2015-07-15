using System;
using CommonDomain.Persistence;
using NEventStoreExample.Infrastructure;
using NEventStoreExample.Command;
using NEventStoreExample.Model;

namespace NEventStoreExample.CommandHandler
{
    public class AccountHolderMovedCommandHandler : ICommandHandler<AccountHolderMovedCommand>
    {
        private IRepository eventstore;

        public AccountHolderMovedCommandHandler(IRepository eventstore)
        {            
            this.eventstore = eventstore;
        }

        public void Handle(AccountHolderMovedCommand command)
        {
            var account = eventstore.GetById<Account>(command.AccountId);
            account.MoveToNewAddress(command.Address, command.City);
            eventstore.Save(account, Guid.NewGuid());
        }
    }
}
