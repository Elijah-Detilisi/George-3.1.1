﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace George.Data.Layer.DataModel
{
    public class UserAccount
    {
        public string EmailAddress { get; set; }
        public string EmailPassword { get; set; }
        public string SmtpHostName { get; set; }
        public int SmtpPortNumber { get; set; }
        public string Pop3HostName { get; set; }
        public int Pop3PortNumber { get; set; }
    }
}
