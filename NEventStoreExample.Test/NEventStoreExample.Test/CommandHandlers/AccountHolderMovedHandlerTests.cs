using System;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NEventStoreExample.Command;
using NEventStoreExample.CommandHandler;
using NEventStoreExample.Event;
using NEventStoreExample.Model;

namespace NEventStoreExample.Test.CommandHandlers
{
    [TestClass]
    public class AccountHolderMovedHandlerTests
    {
        [TestMethod]
        public void When_account_holder_moved_command_is_triggered_it_should_raise_the_appropriate_events()
        {
            // Arrange
            var account = new Account(Guid.NewGuid(), "Thomas");

            var eventStore = new InMemoryEventRepositoryBuilder()
                .WithAggregates(account)
                .Build();
            var handler = new AccountHolderMovedCommandHandler(eventStore);

            // Act
            handler.Handle(
                new AccountHolderMovedCommand(account.Id, "New Address", "New City"));

            // Assert
            eventStore.Events.Should().HaveCount(1);
            eventStore.Events.OfType<AccountHolderMovedEvent>().Should().HaveCount(1);

            account = eventStore.GetById<Account>(account.Id);
            account.Address.Should().Be("New Address");
            account.City.Should().Be("New City");
        }

        [TestMethod]
        public void When_account_holder_moved_command_is_triggered_but_nothing_has_changed_it_should_throw()
        {
            // Arrange
            var account = new Account(Guid.NewGuid(), "Thomas");
            account.SetDetails("Thomas", "New Address", "New Town");

            var eventStore = new InMemoryEventRepositoryBuilder()
                .WithAggregates(account)
                .Build();
            var handler = new AccountHolderMovedCommandHandler(eventStore);

            // Act
            Action action = () => handler.Handle(new AccountHolderMovedCommand(account.Id, "New Address", "New Town"));

            // Assert
            action.ShouldThrow<InvalidOperationException>();
        }        
    }
}
