using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace George.Data.Layer.DataBase
{
    public static class StoredProcedures
    {
        public static string GetUserAccount()
        {
            string query = @"
            SELECT 
	            UserAccounts.EmailAddress AS EmailAddress, 
	            UserAccounts.EmailPassword AS EmailPassword, 
	            SmtpServer.HostName AS SmtpHostName, 
	            SmtpServer.PortNumber AS SmtpPortNumber, 
	            Pop3Server.HostName AS Pop3HostName, 
	            Pop3Server.PortNumber AS Pop3PortNumber
            FROM
	            UserAccounts
            INNER JOIN SmtpServer
	            ON SmtpServer.FK_DomainId = UserAccounts.FK_DomainId
            INNER JOIN Pop3Server
	            ON Pop3Server.FK_DomainId = UserAccounts.FK_DomainId
            WHERE
	            UserAccounts.Id = @accountId;
            ";

            return query;
        }

        public static string GetEmailSettings()
        {
            string query = @"
            WITH Variable AS 
            ( 
            SELECT 
	            (   SELECT 
			            Id
		            FROM 
			            EmailDomain
		            WHERE 
			            LOWER(EmailDomain.DomainName) = 
			            (SELECT SUBSTR(@emailAddress, INSTR(@emailAddress, '@')+1, LENGTH(@emailAddress)))
	            ) 
            AS DomainId 
            )

            --Select settings
            SELECT
                SmtpServer.FK_DomainId AS DomainId,
	            SmtpServer.HostName AS SmtpHostName, 
	            SmtpServer.PortNumber AS SmtpPortNumber, 
	            Pop3Server.HostName AS Pop3HostName, 
	            Pop3Server.PortNumber AS Pop3PortNumber
            FROM
	            SmtpServer
            INNER JOIN Pop3Server
	            ON Pop3Server.FK_DomainId = (SELECT DomainId FROM Variable);
            ";

            return query;
        }

        public static string SaveUserAccount()
        {
            string query = @"
            --Insert new account
            INSERT INTO 
	            UserAccounts (EmailAddress, EmailPassword, FK_DomainId)
            VALUES
	            (@emailAddress, @emailPassword, @domainId);
            ";

            return query;
        }
    }
}
