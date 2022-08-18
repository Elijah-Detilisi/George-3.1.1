
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace George.Data.Layer.DataAccess
{
    using Dapper;
    using System.Data;
    using System.Data.SQLite;
    using George.Data.Layer.DataModel;

    public class DataAccess
    {
        public static async Task SaveUserAccountAsync(string emailAddress, string emailPassword)
        {
            using(IDbConnection connection = new SQLiteConnection(ConnectionString()))
            {
                DynamicParameters data = new DynamicParameters();
                data.Add(emailPassword);
                data.Add(emailAddress);

                _ = await connection.ExecuteAsync("", data);
            }
        }

        public static async Task<UserAccount> GetUserAccountAsync(int accountId)
        {
            using (IDbConnection connection = new SQLiteConnection(ConnectionString()))
            {
                var output = await connection.QueryAsync<UserAccount>("", new DynamicParameters());
                return (UserAccount)output;
            }
        }

        private static string ConnectionString()
        {
            return "";
        }
    }
}
