﻿using System;
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
    public class DepositMoneyCommandHandlerTests
    {
        [TestMethod]
        public void When_depositing_money_it_should_add_it_to_the_account()
        {
            // Arrange
            var account = new Account(Guid.NewGuid(), "Thomas");
            var handler = new DepositMoneyCommandHandler();

            // Act
            var events = handler.Handle(new DepositMoneyCommand(account.Id, 200), account);

            // Assert
            events.Should().HaveCount(1);
            events.OfType<MoneyDepositedEvent>().Should().HaveCount(1);

            account.ApplyEvents(events);
            account.Amount.Should().Be(200);
        }

        [TestMethod]
        public void When_depositing_money_on_a_closed_account_it_should_throw()
        {
            // Arrange
            var account = new Account(Guid.NewGuid(), "Thomas");
            account.ApplyEvents(account.Close());

            var handler = new DepositMoneyCommandHandler();

            // Act
            Action action = () => handler.Handle(
                new DepositMoneyCommand(account.Id, 200), account);

            // Assert
            action.ShouldThrow<InvalidOperationException>();
        }
    }

}
