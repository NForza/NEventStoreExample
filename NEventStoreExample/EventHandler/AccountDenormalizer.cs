using NEventStoreExample.Event;

namespace NEventStoreExample.EventHandler
{
    using Infrastructure;
    using System;

    public class AccountDenormalizer :
        IEventHandler<AccountCreatedEvent>,
        IEventHandler<AccountClosedEvent>,
        IEventHandler<MoneyDepositedEvent>,
        IEventHandler<MoneyWithdrawnEvent>,
        IEventHandler<AccountDetailsSetEvent>
    {
        private ISqlDatabase _database;
        public AccountDenormalizer(ISqlDatabase database)
        {
            _database = database;
        }
        public void Handle(AccountCreatedEvent e)
        {
            _database.ExecuteSqlCommand("INSERT INTO ActiveAccounts (id, name) VALUES (@id, @name)",
                command =>
                {
                    command.Parameters.AddWithValue("@id", e.AccountId);
                    command.Parameters.AddWithValue("@name", e.Name);
                });
        }

        public void Handle(AccountClosedEvent e)
        {
            _database.ExecuteSqlCommand("Delete from ActiveAccounts WHERE Id = @id",
                command =>
                {
                    command.Parameters.AddWithValue("@id", e.AccountId);
                });
        }

        public void Handle(MoneyDepositedEvent e)
        {
            _database.ExecuteSqlCommand( "Update ActiveAccounts SET Amount = @amount WHERE Id = @id", 
                command =>
                {
                    command.Parameters.AddWithValue("@id", e.AccountId);
                    command.Parameters.AddWithValue("@amount", e.NewAccountAmount);
                } );
        }

        public void Handle(MoneyWithdrawnEvent e)
        {
            _database.ExecuteSqlCommand("Update ActiveAccounts SET Amount = @amount WHERE Id = @id",
                command =>
                {
                    command.Parameters.AddWithValue("@id", e.AccountId);
                    command.Parameters.AddWithValue("@amount", e.NewAccountAmount);
                });
        }

        public void Handle(AccountDetailsSetEvent e)
        {
            _database.ExecuteSqlCommand("Update ActiveAccounts SET Address = @address, city = @city WHERE Id = @id",
                command =>
                {
                    command.Parameters.AddWithValue("@id", e.AccountId);
                    command.Parameters.AddWithValue("@address", (object)e.Address ?? DBNull.Value);
                    command.Parameters.AddWithValue("@city", (object)e.City??DBNull.Value);
                });
        }
    }
}