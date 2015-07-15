using System;
using CommonDomain.Core;
using NEventStoreExample.Event;

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

        public void Deposit(double amount)
        {
            VerifyActiveAccount();
            RaiseEvent(new MoneyDepositedEvent(Id, amount));
        }

        private void VerifyActiveAccount()
        {
            if(!IsActive)
                throw new InvalidOperationException("Account is inactive");
        }

        public void Withdraw(double amount)
        {
            VerifyActiveAccount();
            RaiseEvent(new MoneyWithdrawnEvent(Id, amount));
        }

        public void Close()
        {
            if(IsActive)
                RaiseEvent(new AccountClosedEvent(Id, Name));
        }

        public void SetDetails(string name, string address, string city)
        {
            if (Name != name || Address != address || City != city)
                RaiseEvent(new AccountDetailsSetEvent(Id, name, address, city));
        }

        internal void MoveToNewAddress(string address, string city)
        {
            if (Address != address || City != city)
                RaiseEvent(new AccountHolderMovedEvent(Id, address, city));
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