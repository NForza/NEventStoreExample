using System;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NEventStoreExample.Command;
using NEventStoreExample.CommandHandler;
using NEventStoreExample.Event;

namespace NEventStoreExample.Test.CommandHandlers
{
    [TestClass]
    public class CreateAccountCommandHandlerTests
    {
        [TestMethod]
        public void When_create_account_command_is_triggered_it_should_raise_the_appropriate_events()
        {
            // Arrange
            var eventStore = new InMemoryEventRepositoryBuilder().Build();
            var handler = new CreateAccountCommandHandler(eventStore);
            
            // Act
            handler.Handle(new CreateAccountCommand(Guid.NewGuid(), "Thomas"));

            // Assert
            eventStore.Events.Should().HaveCount(1);
            eventStore.Events.OfType<AccountCreatedEvent>().Should().HaveCount(1);
        }

        [TestMethod]
        public void When_create_account_command_is_triggered_with_an_empty_name_it_should_throw()
        {
            // Arrange
            var eventStore = new InMemoryEventRepositoryBuilder().Build();
            var handler = new CreateAccountCommandHandler(eventStore);

            // Act
            Action action = () => handler.Handle(
                new CreateAccountCommand(Guid.NewGuid(), ""));

            // Assert
            action.ShouldThrow<ArgumentException>();
        }

        [TestMethod]
        public void When_create_account_command_is_triggered_without_a_name_it_should_throw()
        {
            // Arrange
            var eventStore = new InMemoryEventRepositoryBuilder().Build();
            var handler = new CreateAccountCommandHandler(eventStore);

            // Act
            Action action = () => handler.Handle(
                new CreateAccountCommand(Guid.NewGuid(), null));

            // Assert
            action.ShouldThrow<ArgumentException>();
        }
        
        [TestMethod]
        public void When_create_account_command_is_triggered_without_a_id_it_should_throw()
        {
            // Arrange
            var eventStore = new InMemoryEventRepositoryBuilder().Build();
            var handler = new CreateAccountCommandHandler(eventStore);
            
            // Act
            Action action = () => handler.Handle(
                new CreateAccountCommand(Guid.Empty, "Thomas"));

            // Assert
            action.ShouldThrow<ArgumentException>();
        }
    }
}
