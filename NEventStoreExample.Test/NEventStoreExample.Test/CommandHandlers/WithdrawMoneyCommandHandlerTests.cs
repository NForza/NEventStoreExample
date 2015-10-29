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
    public class WithdrawMoneyCommandHandlerTests
    {
        [TestMethod]
        public void When_withdrawing_money_it_should_add_it_to_the_account()
        {
            // Arrange
            var account = new Account(Guid.NewGuid(), "Thomas");  
            account.Deposit(100);
            var eventStore = new InMemoryEventRepositoryBuilder()
                .WithAggregates(account)
                .Build();
            var handler = new WithdrawMoneyCommandHandler(eventStore);
            
            // Act
            handler.Handle(new WithdrawMoneyCommand(account.Id, 30));

            // Assert
            eventStore.Events.Should().HaveCount(1);
            eventStore.Events.OfType<MoneyWithdrawnEvent>().Should().HaveCount(1);

            account = eventStore.GetById<Account>(account.Id);
            account.Amount.Should().Be(70);
        }

        [TestMethod]
        public void When_withdrawing_money_on_a_closed_account_it_should_throw()
        {
            // Arrange
            var account = new Account(Guid.NewGuid(), "Thomas");
            account.Close();

            var eventStore = new InMemoryEventRepositoryBuilder()
                .WithAggregates(account)
                .Build();
            var handler = new WithdrawMoneyCommandHandler(eventStore);

            // Act
            Action action = () => handler.Handle(
                new WithdrawMoneyCommand(account.Id, 200));

            // Assert
            action.ShouldThrow<InvalidOperationException>();
        }
    }
}
