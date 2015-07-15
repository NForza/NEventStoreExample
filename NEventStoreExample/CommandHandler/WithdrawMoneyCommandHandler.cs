using System;
using CommonDomain.Persistence;
using NEventStoreExample.Command;
using NEventStoreExample.Infrastructure;
using NEventStoreExample.Model;

namespace NEventStoreExample.CommandHandler
{
    public class WithdrawMoneyCommandHandler : ICommandHandler<WithdrawMoneyCommand>
    {
        private readonly IRepository eventstore;

        public WithdrawMoneyCommandHandler(IRepository eventstore)
        {
            this.eventstore = eventstore;
        }

        public void Handle(WithdrawMoneyCommand command)
        {
            var account = eventstore.GetById<Account>(command.AccountId);
            account.Withdraw(command.Amount);
            eventstore.Save(account, Guid.NewGuid());
        }
    }
}