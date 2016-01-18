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
    public class AccountHolderMovedHandlerTests
    {
        [TestMethod]
        public void When_account_holder_moved_command_is_triggered_it_should_raise_the_appropriate_events()
        {
            // Arrange
            var account = new Account(Guid.NewGuid(), "Thomas");
            
            var handler = new AccountHolderMovedCommandHandler();

            // Act
            var events = handler.Handle(
                new AccountHolderMovedCommand(account.Id, "New Address", "New City"), account);
            events.Should().HaveCount(1);
            events.OfType<AccountHolderMovedEvent>().Should().HaveCount(1);

            account.ApplyEvents(events);

            account.Address.Should().Be("New Address");
            account.City.Should().Be("New City");
        }

        [TestMethod]
        public void When_account_holder_moved_command_is_triggered_but_nothing_has_changed_it_should_throw()
        {
            // Arrange
            var account = new Account(Guid.NewGuid(), "Thomas");
            account.ApplyEvents( account.SetDetails("Thomas", "New Address", "New Town"));

            var handler = new AccountHolderMovedCommandHandler();

            // Act
            Action action = () => handler.Handle(new AccountHolderMovedCommand(account.Id, "New Address", "New Town"), account);

            // Assert
            action.ShouldThrow<InvalidOperationException>();
        }        
    }
}
