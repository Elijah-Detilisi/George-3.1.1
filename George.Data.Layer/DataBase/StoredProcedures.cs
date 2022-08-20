using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace George.Data.Layer.DataBase
{
    public static class StoredProcedures
    {
        public static string SaveUserAccount()
        {
            string query = @"
             --Initialize variables tabel
            PRAGMA temp_store = 2;
            CREATE TEMP TABLE IF NOT EXISTS _Variables(_serverId INT);

            --Declare variables
            INSERT INTO _Variables (_serverId) VALUES (0);

            --Determine serverId 
            UPDATE _Variables 
            SET 
	            _serverId = (
		            SELECT Id
		            FROM EmailDomain
		            WHERE 
			            LOWER(EmailDomain.DomainName) = (SELECT SUBSTR(@emailAddress, INSTR(@emailAddress, '@')+1, LENGTH(@emailAddress)))
	            );
            --Insert new account
            INSERT INTO 
	            UserAccounts (EmailAddress, EmailPassword, FK_DomainId)
            VALUES
	            (@emailAddress, @emailPassword, (SELECT _serverId FROM _Variables));
            --Release _Variables
            DROP TABLE IF EXISTS _Variables;
            ";

            return query;
        }

        public static string GetUserAccount()
        {
            string query = @"
            SELECT 
                EmailAddress, EmailPassword, 
                SmptHostName, SmptPortNumber, 
                Pop3HostName, Pop3PortNumber
            FROM
                UserAccounts
            INNER JOIN StmpServer
                ON StmpServer.ServerId = UserAccounts.ServerId
            INNER JOIN Pop3Server
                ON Pop3Server.ServerId = UserAccounts.ServerId
            WHERE
                UserAccounts.ID = @accountId
            ";

            return query;
        }
    }
}
