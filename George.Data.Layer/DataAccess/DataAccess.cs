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
            RestoreDataBase();
        }

        #region Data Access Methods
        public async Task<EmailSettings> GetEmailSettingsAsync(string emailAddress)
        {
            try
            {
                using (IDbConnection db = _connectionManager.DefaultConnection())
                {
                    var parameters = new Dictionary<string, object>()
                    {
                        ["EmailAddress"] = emailAddress
                    };

                    var output = await db.QueryAsync<EmailSettings>(StoredProcedures.GetEmailSettings(), parameters);
                    return output.ToList()[0];
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[INFO]: Error while executing GetEmailSettingsAsync; {ex.Message}");
                throw ex;
            }
        }

        public async Task<UserAccount> GetUserAccountAsync(int accountId)
        {
            try
            {
                using (IDbConnection db = _connectionManager.DefaultConnection())
                {
                    var parameters = new Dictionary<string, object>()
                    {
                        ["accountId"] = accountId
                    };

                    var output = await db.QueryAsync<UserAccount>(StoredProcedures.GetUserAccount(), parameters);
                    return output.ToList()[0];
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[INFO]: Error while executing GetUserAccountAsync; {ex.Message}");
                throw ex;
            }
        }

        public async Task SaveUserAccountAsync(string emailAddress, string emailPassword)
        {
            try
            {
                using (IDbConnection db = _connectionManager.DefaultConnection())
                {
                    var emailSettings = await GetEmailSettingsAsync(emailAddress);

                    var parameters = new Dictionary<string, object>()
                      {
                        ["EmailAddress"] = emailAddress,
                        ["EmailPassword"] = emailPassword,
                        ["DomainId"] = emailSettings.DomainId
                    };

                    await db.ExecuteAsync(StoredProcedures.SaveUserAccount(), parameters);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[INFO]: Error while executing SaveUserAccountAsync; {ex.Message}");
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

        private void RestoreDataBase()
        {

            var _databasePath = Directory.GetCurrentDirectory() + 
                                @"\" + _connectionManager.GetDatabaseName();

            if (!File.Exists(_connectionManager.GetDatabaseName()))
            {
                Console.WriteLine("Initializing table");
                SQLiteConnection.CreateFile(_databasePath);
                CreateTables();
                RestoreTableDefaultValues();
                
            }
            
        }
        #endregion

        #region Support Entities
        private interface IConnectionManager
        {
            IDbConnection DefaultConnection();
            string GetDatabaseName();
        }

        private class ConnectionManager : IConnectionManager
        {
            private readonly string _databaseName;
            private readonly string _connectionString;

            public ConnectionManager()
            {
                _databaseName = "GeorgeDatabase.sqlite3";
                _connectionString = "data source= " + _databaseName;
                
            }
            public IDbConnection DefaultConnection()
            {
                return new SQLiteConnection(_connectionString);
            }
            public string GetDatabaseName()
            {
                   return _databaseName;
            }
        }
        #endregion
    }
}
