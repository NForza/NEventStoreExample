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
    public class CloseAccountCommandHandlerTests
    {
        [TestMethod]
        public void When_close_account_command_is_triggered_it_should_raise_the_appropriate_events()
        {
            // Arrange
            var account = new Account(Guid.NewGuid(), "Thomas");

            var eventStore = new InMemoryEventRepositoryBuilder().WithAggregates(account).Build();
            var handler = new CloseAccountCommandHandler(eventStore);

            // Act
            handler.Handle(new CloseAccountCommand(account.Id));

            // Assert
            eventStore.Events.Should().HaveCount(1);
            eventStore.Events.OfType<AccountClosedEvent>().Should().HaveCount(1);

            account = eventStore.GetById<Account>(account.Id);
            account.IsActive.Should().BeFalse();
        }

        [TestMethod]
        public void When_close_account_command_is_triggered_on_a_closed_account_it_should_not_raise_events()
        {
            // Arrange
            var account = new Account(Guid.NewGuid(), "Thomas");
            account.Close();

            var eventStore = new InMemoryEventRepositoryBuilder().WithAggregates(account).Build();
            var handler = new CloseAccountCommandHandler(eventStore);

            // Act
            handler.Handle(new CloseAccountCommand(account.Id));

            // Assert
            eventStore.Events.OfType<AccountClosedEvent>().Should().BeEmpty();
        }
    }
}
