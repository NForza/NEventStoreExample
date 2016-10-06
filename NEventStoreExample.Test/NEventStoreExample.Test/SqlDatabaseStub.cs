using NEventStoreExample.EventHandler;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace NEventStoreExample.Test
{
    class SqlDatabaseStub : ISqlDatabase
    {
        private List<SqlCommand> executedCommands = new List<SqlCommand>();

        public IEnumerable<SqlCommand> ExecutedCommands => executedCommands;

        public void ExecuteSqlCommand(string sqlcommand, Action<SqlCommand> setParamsAction = null)
        {
            var command = new SqlCommand(sqlcommand, new SqlConnection(""));
            setParamsAction?.Invoke(command);
            executedCommands.Add(command);
        }
    }
}
