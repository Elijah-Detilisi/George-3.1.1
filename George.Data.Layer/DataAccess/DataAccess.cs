using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace George.Data.Layer.DataAccess
{
    using Dapper;
    using System.Data;
    using System.Data.SQLite;
    using George.Data.Layer.DataModel;
    using George.Data.Layer.DataBase;

    public class DataAccess
    {
        #region Instances
        private readonly IConnectionManager _connectionManager;
        #endregion

        public DataAccess()
        {
            _connectionManager = new ConnectionManager();
        }

        #region Class Methods
        public async Task SaveUserAccountAsync(string emailAddress, string emailPassword)
        {
            try
            {
                using (IDbConnection db = _connectionManager.DefaultConnection())
                {
                    DynamicParameters data = new DynamicParameters();
                    data.Add(emailPassword);
                    data.Add(emailAddress);

                    await db.ExecuteAsync("", data);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[INFO]: Error while executing SaveUserAccountAsync; {ex.Message}");
            }
        }

        public async Task<UserAccount> GetUserAccountAsync(int accountId)
        {
            try
            {
                using (IDbConnection db = _connectionManager.DefaultConnection())
                {
                    DynamicParameters data = new DynamicParameters();
                    data.Add(accountId.ToString());

                    var output = await db.QueryAsync<UserAccount>("", new DynamicParameters());
                    return (UserAccount)output;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[INFO]: Error while executing GetUserAccountAsync; {ex.Message}");
                return null;
            }
        }
        #endregion

        #region Initialization Methods
        private void CreateTables()
        {
            try
            {
                using (IDbConnection db = _connectionManager.DefaultConnection())
                {
                    db.Execute(Tables.UserAccounts());
                    db.Execute(Tables.EmailDomain());
                    db.Execute(Tables.StmpServer());
                    db.Execute(Tables.Pop3Server());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[INFO]: Error while executing RestoreTableDefaultValues; {ex.Message}");
            }
        }

        private void RestoreTableDefaultValues()
        {
            try
            {
                using (IDbConnection db = _connectionManager.DefaultConnection())
                {
                    db.Execute(Presets.RestoreEmailDomainTable());
                    db.Execute(Presets.RestoreSmtpServerTable());
                    db.Execute(Presets.RestorePop3ServerTable());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[INFO]: Error while executing RestoreTableDefaultValues; {ex.Message}");
            }
        }

        public void RestoreDataBase()
        {
            if (!File.Exists("database.sqlite3")) // pass file path
            {
                Console.WriteLine("Initializing table");
                SQLiteConnection.CreateFile("database.sqlite3");
                CreateTables();
                RestoreTableDefaultValues();
                
            }
            
        }
        #endregion

        #region Support Entities
        private interface IConnectionManager
        {
            IDbConnection DefaultConnection();
        }

        private class ConnectionManager : IConnectionManager
        {
            private readonly string connectionString;
            public ConnectionManager()
            {
                connectionString = "data source= database.sqlite3";
            }
            public IDbConnection DefaultConnection()
            {
                return new SQLiteConnection(connectionString);
            }
        }
        #endregion
    }
}
