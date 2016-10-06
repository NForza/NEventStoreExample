using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NEventStoreExample.Event;
using NEventStoreExample.EventHandler;
using System;
using System.Linq;

namespace NEventStoreExample.Test.EventHandlers
{
    [TestClass]
    public class AccountDenormalizerTests
    {
        [TestMethod]
        public void DepositAmountEvent_should_be_omnipotent()
        {
            var sqlDatabase = new SqlDatabaseStub();
            AccountDenormalizer denormalizer = new AccountDenormalizer(sqlDatabase);

            var guid = Guid.NewGuid();
            denormalizer.Handle(new MoneyDepositedEvent(guid, 100, 200));
            denormalizer.Handle(new MoneyDepositedEvent(guid, 100, 200));

            sqlDatabase.ExecutedCommands.Should().NotContain(cmd => (double)cmd.Parameters["@amount"].Value == 400.0);
        }

        [TestMethod]
        public void MoneyWithdrawnEvent_should_be_omnipotent()
        {
            var sqlDatabase = new SqlDatabaseStub();
            AccountDenormalizer denormalizer = new AccountDenormalizer(sqlDatabase);

            var guid = Guid.NewGuid();
            denormalizer.Handle(new MoneyWithdrawnEvent(guid, 100, 200));
            denormalizer.Handle(new MoneyWithdrawnEvent(guid, 100, 200));

            sqlDatabase.ExecutedCommands.Should().NotContain(cmd => (double)cmd.Parameters["@amount"].Value == 400.0);
        }

    }
}
