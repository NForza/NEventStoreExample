using System;
using NEventStoreExample.Infrastructure;

namespace NEventStoreExample.Command
{
    public class CloseAccountCommand : ICommand
    {
        public CloseAccountCommand(Guid accountId)
        {
            ID = accountId;
        }

        public Guid ID { get; set; }
    }
}