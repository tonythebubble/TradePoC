using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace TradeFeed.DataAccess.Model
{
    public class DeadLetter
    {
        public Int32 Id { get; set; }

        public string Body { get; set; }

        public string Message { get; set; }
    }
}
