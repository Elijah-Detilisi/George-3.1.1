using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace George.Data.Layer.DataBase
{
    public static class Presets
    {
        public static string RestoreEmailDomainTable()
        {
            string query = @"
            INSERT INTO EmailDomain 
                (DomainName) 
            VALUES 
                ('Gmail.com'), 
                ('Yahoo.com'), 
                ('Hotmail.com'), 
                ('Outlook.com'), 
                ('Office365.com')
            ";

            return query;
        }

        public static string RestoreSmtpServerTable()
        {
            string query = @"
            INSERT INTO SmtpServer 
	            (HostName, PortNumber, FK_DomainId) 
            VALUES 
	            ('smtp.gmail.com', 587, 1),
                ('smtp.mail.yahoo.com', 587, 2),
                ('smtp.live.com', 587, 3),
                ('smtp.live.com', 587, 4),
                ('smtp.office365.com', 587, 5)
            ";

            return query;
        }

        public static string RestorePop3ServerTable()
        {
            string query = @"
            INSERT INTO Pop3Server 
	            (HostName, PortNumber, FK_DomainId) 
            VALUES 
	            ('pop.gmail.com', 996, 1),
                ('pop.mail.yahoo.com', 587, 2),
                ('pop3.live.com', 587, 3),
                ('pop3.live.com', 587, 4),
                ('outlook.office365.com ', 587, 5)
            ";

            return query;
        }
    }
}
