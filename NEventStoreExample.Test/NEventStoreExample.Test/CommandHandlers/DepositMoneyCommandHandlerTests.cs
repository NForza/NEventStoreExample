﻿using System;
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
    public class DepositMoneyCommandHandlerTests
    {
        [TestMethod]
        public void When_depositing_money_it_should_add_it_to_the_account()
        {
            // Arrange
            var account = new Account(Guid.NewGuid(), "Thomas");
            var eventStore = new InMemoryEventRepositoryBuilder()
                .WithAggregates(account)
                .Build();
            var handler = new DepositMoneyCommandHandler(eventStore);

            // Act
            handler.Handle(new DepositMoneyCommand(account.Id, 200));

            // Assert
            eventStore.Events.Should().HaveCount(1);
            eventStore.Events.OfType<MoneyDepositedEvent>().Should().HaveCount(1);

            account = eventStore.GetById<Account>(account.Id);
            account.CurrentAmount.Should().Be(200);
        }

        [TestMethod]
        public void When_depositing_money_on_a_closed_account_it_should_throw()
        {
            // Arrange
            var account = new Account(Guid.NewGuid(), "Thomas");
            account.Close();

            var repository = new InMemoryEventRepositoryBuilder()
                .WithAggregates(account)
                .Build();
            var handler = new DepositMoneyCommandHandler(repository);

            // Act
            Action action = () => handler.Handle(
                new DepositMoneyCommand(account.Id, 200));

            // Assert
            action.ShouldThrow<InvalidOperationException>();
        }
    }
}
