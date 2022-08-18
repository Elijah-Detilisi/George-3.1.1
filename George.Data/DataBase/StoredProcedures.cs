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
				DECLARE	@serverId AS INT
				DECLARE	@domainName AS VARCHAR(100)
	
				--Determine ServerId

				SET @domainName = SUBSTRING(@emailAddress, CHARINDEX('@' , @emailAddress)+1, LEN(@emailAddress))

				SET @serverId = (	SELECT Id
									FROM dbo.Domain_Server AS EmailServer
									WHERE EmailServer.Domain = @domainName
								)

				--Insert new account
				INSERT INTO dbo.Accounts
				VALUES(@emailAddress, @emailPassword, @serverId)
			END
            ";

            return query;
        }

        public static string GetUserAccount()
        {
            string query = @"
                BEGIN
					SELECT 
						Accounts.Email_Address AS EmailAdddress,
						Accounts.Email_Password AS EmailPassword,
		
						SmtpServer.Host AS StmpHost, 
						SmtpServer.Port_Number AS StmpPort,
		
						Pop3Server.Host AS Pop3Host, 
						Pop3Server.Port_Number AS Pop3Port

					FROM 
						dbo.Accounts AS Accounts		
						INNER JOIN dbo.Domain_SmtpServer AS SmtpServer		
							ON SmtpServer.Server_Id = Accounts.Server_Id
			 
						INNER JOIN dbo.Domain_SmtpServer AS Pop3Server		
							ON Pop3Server.Server_Id = Accounts.Server_Id

					WHERE Email_Address = @emailAddress
				END
            ";

            return query;
        }
    }
}
