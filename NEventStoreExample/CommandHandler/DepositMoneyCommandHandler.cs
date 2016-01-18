using NEventStoreExample.Command;
using NEventStoreExample.Infrastructure;
using NEventStoreExample.Model;
using System.Collections.Generic;

namespace NEventStoreExample.CommandHandler
{
    public class DepositMoneyCommandHandler : ICommandHandler<DepositMoneyCommand, Account>
    {
        public IEnumerable<IEvent> Handle(DepositMoneyCommand command, Account account)
        {
            return account.Deposit(command.Amount);
        }
    }
}