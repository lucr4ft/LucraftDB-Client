using System;
using System.Collections.Generic;
using System.Text;

namespace Lucraft.Database.Client
{
    public class OutdatedClientException : Exception
    {
        public OutdatedClientException(string message) : base(message) { }
    }
}
