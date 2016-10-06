using System;
using System.Configuration;
using System.Data.SqlClient;

namespace NEventStoreExample.EventHandler
{
    public class SqlDatabase : ISqlDatabase
    {
        public void ExecuteSqlCommand(string sqlcommand, Action<SqlCommand> setParamsAction = null)
        {
            using (
                var connection =
                    new SqlConnection(
                        ConfigurationManager.ConnectionStrings["EventStoreExampleConnection"].ConnectionString))
            {

                using (var command = new SqlCommand(sqlcommand, connection))
                {
                    if (setParamsAction != null)
                        setParamsAction(command);
                    connection.Open();
                    command.ExecuteScalar();
                }
            }
        }
    }
}
