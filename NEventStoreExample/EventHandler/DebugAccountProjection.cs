using System;
using NEventStoreExample.Event;
using NEventStoreExample.Infrastructure;

namespace NEventStoreExample.EventHandler
{
    public class DebugAccountProjection : 
        IEventHandler<AccountCreatedEvent>,
        IEventHandler<AccountClosedEvent>,
        IEventHandler<MoneyDepositedEvent>,
        IEventHandler<MoneyWithdrawnEvent>,
        IEventHandler<AccountDetailsSetEvent>,
        IEventHandler<AccountHolderMovedEvent>
    {
        public void Handle(AccountCreatedEvent e)
        {
            Console.WriteLine("Account created: {0}", e.Name);
        }
        
        public void Handle(AccountClosedEvent e)
        {
            Console.WriteLine("Account closed: {0}", e.Name);
        }

        public void Handle(MoneyDepositedEvent e)
        {
            Console.WriteLine("{1} deposited to AccountId: {0}", e.AccountId, e.Amount);
        }

        public void Handle(MoneyWithdrawnEvent e)
        {
            Console.WriteLine("{1} withdrawn from AccountId: {0}", e.AccountId, e.Amount);
        }

        public void Handle(AccountHolderMovedEvent e)
        {
            Console.WriteLine("{0} moved to new address: {1}, {2}", e.AccountId, e.Address, e.City);
        }

        public void Handle(AccountDetailsSetEvent e)
        {
            Console.WriteLine("{0} details set: {1}, {2}, {3}", e.AccountId, e.Name, e.Address, e.City);
        }
    }
}