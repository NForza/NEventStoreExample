using System;
using System.Configuration;
using System.Data.SqlClient;

namespace NEventStoreExample.EventHandler
{
    public interface ISqlDatabase
    {
        void ExecuteSqlCommand(string sqlcommand, Action<SqlCommand> setParamsAction = null);
    }
}
