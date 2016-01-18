using System;
using CommonDomain.Persistence;
using NEventStoreExample.Command;
using NEventStoreExample.Infrastructure;
using NEventStoreExample.Model;
using System.Collections.Generic;

namespace NEventStoreExample.CommandHandler
{
    public class WithdrawMoneyCommandHandler : ICommandHandler<WithdrawMoneyCommand, Account>
    {
        public IEnumerable<IEvent> Handle(WithdrawMoneyCommand command, Account account)
        {
            return account.Withdraw(command.Amount);
        }
    }
}