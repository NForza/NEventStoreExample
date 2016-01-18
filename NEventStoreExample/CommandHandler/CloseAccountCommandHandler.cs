using NEventStoreExample.Command;
using NEventStoreExample.Infrastructure;
using NEventStoreExample.Model;
using System.Collections.Generic;

namespace NEventStoreExample.CommandHandler
{
    public class CloseAccountCommandHandler : ICommandHandler<CloseAccountCommand, Account>
    {
        public IEnumerable<IEvent> Handle(CloseAccountCommand command, Account account)
        {
            return account.Close();
        }
    }
}