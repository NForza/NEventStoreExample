using System;
using NEventStoreExample.Infrastructure;

namespace NEventStoreExample.Command
{
    public class CloseAccountCommand : ICommand
    {
        public CloseAccountCommand(Guid accountId)
        {
            AccountId = accountId;
        }

        public Guid AccountId { get; private set; }
    }
}