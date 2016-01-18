using System;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NEventStoreExample.Command;
using NEventStoreExample.CommandHandler;
using NEventStoreExample.Event;
using NEventStoreExample.Model;
using NEventStoreExample.Infrastructure;

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

            var handler = new CloseAccountCommandHandler();

            // Act
            var events = handler.Handle(new CloseAccountCommand(account.Id), account);

            // Assert
            events.Should().HaveCount(1);
            events.OfType<AccountClosedEvent>().Should().HaveCount(1);

            account.ApplyEvents(events);

            account.IsActive.Should().BeFalse();
        }

        [TestMethod]
        public void When_close_account_command_is_triggered_on_a_closed_account_it_should_not_raise_events()
        {
            // Arrange
            var account = new Account(Guid.NewGuid(), "Thomas");
            account.ApplyEvents(account.Close());

            var handler = new CloseAccountCommandHandler();

            // Act
            var events = handler.Handle(new CloseAccountCommand(account.Id), account);

            // Assert
            events.Should().BeEmpty();
        }
    }
}
