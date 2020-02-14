using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;

namespace TradeFeed.DataAccess.Model
{
    public class Stock
    {
        public Int32 Id { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }
    }
}
