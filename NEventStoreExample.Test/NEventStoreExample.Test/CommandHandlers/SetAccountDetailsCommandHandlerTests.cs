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
    public class SetAccountDetailsCommandHandlerTests
    {
        [TestMethod]
        public void When_set_account_details_command_is_triggered_it_should_raise_the_appropriate_events()
        {
            // Arrange
            var account = new Account(Guid.NewGuid(), "Thomas");

            var handler = new SetAccountDetailsCommandHandler();

            // Act
            var events = handler.Handle(new SetAccountDetailsCommand(account.Id, "New Name", "New Address", "New City"), account);

            // Assert
            events.Should().HaveCount(1);
            events.OfType<AccountDetailsSetEvent>().Should().HaveCount(1);

            account.ApplyEvents(events);
            account.Name.Should().Be("New Name");
            account.Address.Should().Be("New Address");
            account.City.Should().Be("New City");
        }

        [TestMethod]
        public void When_set_account_details_command_is_triggered_but_nothing_has_changed_it_should_not_raise_events()
        {
            // Arrange
            var account = new Account(Guid.NewGuid(), "Thomas");
            account.ApplyEvents(account.SetDetails("New Name", "New Address", "New Town"));

            var handler = new SetAccountDetailsCommandHandler();

            // Act
            var events = handler.Handle(
                new SetAccountDetailsCommand(account.Id, "New Name", "New Address", "New Town"), account);

            // Assert
            events.Should().BeEmpty();
        }
    }
}
