using NEventStoreExample.Event;

namespace NEventStoreExample.EventHandler
{
    using Infrastructure;
    public class AccountProjection : ProjectionBase,
        IEventHandler<AccountCreatedEvent>,
        IEventHandler<AccountClosedEvent>,
        IEventHandler<MoneyDepositedEvent>,
        IEventHandler<MoneyWithdrawnEvent>,
        IEventHandler<AccountDetailsSetEvent>
    {
        public void Handle(AccountCreatedEvent e)
        {
            ExecuteSqlCommand("INSERT INTO ActiveAccounts (id, name) VALUES (@id, @name)",
                command =>
                {
                    command.Parameters.AddWithValue("@id", e.AccountId);
                    command.Parameters.AddWithValue("@name", e.Name);
                });
        }

        public void Handle(AccountClosedEvent e)
        {
            ExecuteSqlCommand("Delete from ActiveAccounts WHERE Id = @id",
                command =>
                {
                    command.Parameters.AddWithValue("@id", e.AccountId);
                });
        }

        public void Handle(MoneyDepositedEvent e)
        {
            ExecuteSqlCommand( "Update ActiveAccounts SET Amount = Amount + @amount WHERE Id = @id", 
                command =>
                {
                    command.Parameters.AddWithValue("@id", e.AccountId);
                    command.Parameters.AddWithValue("@amount", e.Amount);
                } );
        }

        public void Handle(MoneyWithdrawnEvent e)
        {
            ExecuteSqlCommand("Update ActiveAccounts SET Amount = Amount - @amount WHERE Id = @id",
                command =>
                {
                    command.Parameters.AddWithValue("@id", e.AccountId);
                    command.Parameters.AddWithValue("@amount", e.Amount);
                });
        }

        public void Handle(AccountDetailsSetEvent e)
        {
            ExecuteSqlCommand("Update ActiveAccounts SET Address = @address, city = @city WHERE Id = @id",
                command =>
                {
                    command.Parameters.AddWithValue("@id", e.AccountId);
                    command.Parameters.AddWithValue("@address", e.Address);
                    command.Parameters.AddWithValue("@city", e.City);
                });
        }
    }
}