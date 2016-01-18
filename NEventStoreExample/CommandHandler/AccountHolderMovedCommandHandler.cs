using System;
using CommonDomain.Persistence;
using NEventStoreExample.Infrastructure;
using NEventStoreExample.Command;
using NEventStoreExample.Model;
using System.Collections.Generic;

namespace NEventStoreExample.CommandHandler
{
    public class AccountHolderMovedCommandHandler : ICommandHandler<AccountHolderMovedCommand, Account>
    {
        public IEnumerable<IEvent> Handle(AccountHolderMovedCommand command, Account account)
        {
            return account.MoveToNewAddress(command.Address, command.City);
        }
    }
}
