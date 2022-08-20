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
             BEGIN
				--locals
				DECLARE	@serverId INT
				DECLARE	@domainName TEXT
	
				--Determine ServerId
				SET @domainName = SUBSTRING(
									@emailAddress, CHARINDEX('@', 
									@emailAddress)+1, LEN(@emailAddress)
				)

				SET @serverId = (	SELECT Id
									FROM EmailDomain
									WHERE EmailDomain.Domain = @domainName
								)

				--Insert new account
				INSERT INTO UserAccounts
				VALUES(@emailAddress, @emailPassword, @serverId)
			END
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
            ";

            return query;
        }
    }
}
