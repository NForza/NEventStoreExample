using System;
using NEventStoreExample.Event;
using NEventStoreExample.Model;
using NEventStoreExample.Infrastructure;

namespace NEventStoreExample.EventHandler
{
    public class AccountModificationsProjection : ProjectionBase,
        IEventHandler<MoneyDepositedEvent>,
        IEventHandler<MoneyWithdrawnEvent>
    {
        public void Handle(MoneyDepositedEvent e)
        {
            ExecuteSqlCommand(
                    "INSERT INTO Modifications (Id, AccountId, ModificationType, Amount, ModificationDateTime) VALUES (@Id, @AccountId, @ModificationType, @Amount, @ModificationDateTime)",
                command =>
                {
                    command.Parameters.AddWithValue("@Id", Guid.NewGuid());
                    command.Parameters.AddWithValue("@AccountId", e.AccountId);
                    command.Parameters.AddWithValue("@ModificationType", (int)ModificationType.Deposit);
                    command.Parameters.AddWithValue("@Amount", e.Amount);
                    command.Parameters.AddWithValue("@ModificationDateTime", DateTime.Now);
                });
        }

        public void Handle(MoneyWithdrawnEvent e)
        {
            ExecuteSqlCommand(
                        "INSERT INTO Modifications (Id, AccountId, ModificationType, Amount, ModificationDateTime) VALUES (@Id, @AccountId, @ModificationType, @Amount, @ModificationDateTime)",

                command =>
                {
                    command.Parameters.AddWithValue("@Id", Guid.NewGuid());
                    command.Parameters.AddWithValue("@AccountId", e.AccountId);
                    command.Parameters.AddWithValue("@ModificationType", (int)ModificationType.Withdrawal);
                    command.Parameters.AddWithValue("@Amount", e.Amount);
                    command.Parameters.AddWithValue("@ModificationDateTime", DateTime.Now);
                });
        }

    }
}