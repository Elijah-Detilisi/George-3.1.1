using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace George.Data.Layer.DataBase
{
    public static class Tables
    {
        public static string UserAccounts()
        {
            string  query = @"
                CREATE TABLE UserAccounts (
	                Id	INTEGER AUTOINCREMENT,
	                EmailAddress TEXT NOT NULL UNIQUE,
	                EmailPassword TEXT NOT NULL,
	                FK_DomainId INTEGER NOT NULL,
	                PRIMARY KEY(Id AUTOINCREMENT)
                )
            ";

            return query;
        }

        public static string EmailDomain()
        {
            string query = @"
                CREATE TABLE EmailDomain (
	                Id	INTEGER NOT NULL,
	                DomainName	TEXT NOT NULL,
	                PRIMARY KEY(Id)
                )
            ";

            return query;
        }

        public static string StmpServer()
        {
            string query = @"
                CREATE TABLE StmpServer (
	                HostName TEXT NOT NULL,
	                PortNumber	INTEGER NOT NULL,
	                FK_DomainId	INTEGER NOT NULL
                )
            ";

            return query;
        }

        public static string Pop3Server()
        {
            string query = @"
                CREATE TABLE Pop3Server (
	                HostName TEXT NOT NULL,
	                PortNumber INTEGER NOT NULL,
	                FK_DomainId INTEGER NOT NULL
                )
            ";

            return query;
        }
    }
}
