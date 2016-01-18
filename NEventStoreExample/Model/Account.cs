using System;
using CommonDomain.Core;
using NEventStoreExample.Event;
using NEventStoreExample.Infrastructure;
using System.Collections.Generic;
using MemBus.Support;
using System.Linq;

namespace NEventStoreExample.Model
{
    public class Account : AggregateBase
    {        
        public string Name { get; set; }

        public Account(Guid id, string name)
            : this(id)
        {
            RaiseEvent(new AccountCreatedEvent(Id, name, true));
        }

        private Account(Guid id)
        {
            Id = id;
        }

        public bool IsActive { get; private set; }

        public double Amount { get; private set; }
        public string Address { get; set; }
        public string City { get; set; }

        public IEnumerable<IEvent> Deposit(double amount)
        {
            VerifyActiveAccount();
            return new MoneyDepositedEvent(Id, amount).AsEnumerable();
        }

        private void VerifyActiveAccount()
        {
            if(!IsActive)
                throw new InvalidOperationException("Account is inactive");
        }

        public IEnumerable<IEvent> Withdraw(double amount)
        {
            VerifyActiveAccount();
            return new MoneyWithdrawnEvent(Id, amount).AsEnumerable();
        }

        public IEnumerable<IEvent> Close()
        {
            if(IsActive)
                return new AccountClosedEvent(Id, Name).AsEnumerable();
            return Enumerable.Empty<IEvent>();
        }

        public IEnumerable<IEvent> SetDetails(string name, string address, string city)
        {
            if (Name != name || Address != address || City != city)
                return new AccountDetailsSetEvent(Id, name, address, city).AsEnumerable();
            return Enumerable.Empty<IEvent>();
        }

        internal IEnumerable<IEvent> MoveToNewAddress(string address, string city)
        {
            if (Address != address || City != city)
                return new AccountHolderMovedEvent(Id, address, city).AsEnumerable();
            else
                throw new InvalidOperationException("New address must differ from current address");
        }
        
        private void Apply(AccountCreatedEvent @event)
        {
            Id = @event.AccountId;
            Name = @event.Name;
            IsActive = @event.IsActive;
        }

        private void Apply(AccountClosedEvent e)
        {
            IsActive = false;
        }

        private void Apply(MoneyDepositedEvent e)
        {
            Amount += e.Amount;
        }

        private void Apply(MoneyWithdrawnEvent e)
        {
            Amount -= e.Amount;
        }

        private void Apply(AccountDetailsSetEvent e)
        {
            Name = e.Name;
            Address = e.Address;
            City = e.City;
        }
        
        private void Apply(AccountHolderMovedEvent e)
        {
            Address = e.Address;
            City = e.City;
        }
    }
}