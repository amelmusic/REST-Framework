using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Model
{
    public class EmailConfig
    {
        public string From { get; set; }
        public string FromName { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool Ssl { get; set; }
    }
}
