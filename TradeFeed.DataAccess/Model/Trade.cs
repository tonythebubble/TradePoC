using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;

namespace TradeFeed.DataAccess.Model
{
    public class Trade
    {
        public Int32 Id { get; set; }

        public Int32 StockId { get; set; }

        public Int32 ClientId { get; set; }

        public string Venue { get; set; }

        public Int32 Quantity { get; set; }

        public decimal Price { get; set; }

        public string BuySell { get; set; }
    }
}
