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
                    ('Gmail'), 
                    ('Yahoo'), 
                    ('Hotmail'), 
                    ('Outlook'), 
                    ('Office365')
            ";

            return query;
        }

        public static string RestoreSmtpServerTable()
        {
            string query = "";

            return query;
        }

        public static string RestorePop3ServerTable()
        {
            string query = "";

            return query;
        }
    }
}
