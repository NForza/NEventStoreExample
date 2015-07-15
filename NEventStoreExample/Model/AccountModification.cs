using System;
using System.ComponentModel.DataAnnotations;

namespace NEventStoreExample.Model
{
    public class AccountModification
    {        
        public AccountModification(Guid accountid, ModificationType modificationType, double amount, DateTime modificationDateTime)
        {
            Id = Guid.NewGuid();
            AccountId = accountid;
            ModificationType = modificationType;
            Amount = amount;
            ModificationDateTime = modificationDateTime;
        }

        [Key]
        public Guid Id { get; private set; }

        public Guid AccountId { get; private set; }

        public ModificationType ModificationType { get; private set; }       

        public double Amount { get; private set; }

        public DateTime ModificationDateTime { get; private set; }
    }
}