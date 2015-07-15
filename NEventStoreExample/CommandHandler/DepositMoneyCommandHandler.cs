using System;
using CommonDomain.Persistence;
using NEventStoreExample.Command;
using NEventStoreExample.Infrastructure;
using NEventStoreExample.Model;

namespace NEventStoreExample.CommandHandler
{
    public class DepositMoneyCommandHandler : ICommandHandler<DepositMoneyCommand>
    {
        private readonly IRepository eventstore;

        public DepositMoneyCommandHandler(IRepository eventstore)
        {
            this.eventstore = eventstore;
        }

        public void Handle(DepositMoneyCommand command)
        {
            var account = eventstore.GetById<Account>(command.AccountId);
            account.Deposit(command.Amount);
            eventstore.Save(account, Guid.NewGuid());
        }
    }
}