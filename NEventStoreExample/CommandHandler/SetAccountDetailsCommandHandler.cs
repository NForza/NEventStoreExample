using NEventStoreExample.Infrastructure;
using NEventStoreExample.Command;
using NEventStoreExample.Model;
using System.Collections.Generic;

namespace NEventStoreExample.CommandHandler
{
    public class SetAccountDetailsCommandHandler : ICommandHandler<SetAccountDetailsCommand, Account>
    {
        public IEnumerable<IEvent> Handle(SetAccountDetailsCommand command, Account account)
        {
            return account.SetDetails(command.Name, command.Address, command.City);
        }
    }
}
