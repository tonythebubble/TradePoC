using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TradeFeed.Reader.JsonMock
{
    public class JsonFeed : IJsonFeed
    {
        private string _json;

        public IJsonFeed.StatusCode Status { get; private set; }

        public string Error { get; private set; }

        public string DeadLetter { get; private set; }

        public JsonFeed()
        {
            Status = IJsonFeed.StatusCode.Ok;

            //NEED TO FIND A BETTER WAY OF GETTING THE PATH - BUT MAY BE SUFFICIENT FOR POC / MOCK

            string path = Environment.CurrentDirectory.Substring(0, Environment.CurrentDirectory.IndexOf("TradeFeed.Reader") + 17);

            try
            {
                _json = File.ReadAllText(path + @"JsonMock\TradeFeed.json");
            }
            catch (Exception ex)
            {
                Error = "Feed could not be opened - " + ex.Message;
                Status = IJsonFeed.StatusCode.CouldNotOpenFeed;
            }
        }

        public List<TradeFeed> GetTradeFeedData()
        {
            List<TradeFeed> feed = null;
            try
            {
                feed = JsonConvert.DeserializeObject<List<TradeFeed>>(_json);
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                DeadLetter = _json;
                Status = IJsonFeed.StatusCode.InvalidDataInFeed;
            }

            return feed;
        }
    }
}
