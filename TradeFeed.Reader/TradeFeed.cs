using System;
using System.Collections.Generic;
using System.Text;

namespace TradeFeed.Reader
{
    public class TradeFeed
    {
        public string StockId { get; set; }
        public string StockName { get; set; }
        public string StockType { get; set; }
        public string ClientId { get; set; }
        public string ClientName { get; set; }
        public string VenueName { get; set; }
        public string Quantity { get; set; }
        public string Price { get; set; }
        public string BuySell { get; set; }
    }
}
