using System;
using System.Collections.Generic;
using System.Text;

namespace TradeFeed.Reader
{
    public interface IJsonFeed
    {
        public enum StatusCode
        {
            Ok,
            CouldNotOpenFeed,
            InvalidDataInFeed
        }

        public IJsonFeed.StatusCode Status { get; }

        public string Error { get; }
        public string DeadLetter { get; }
        List<TradeFeed> GetTradeFeedData();
    }
}
