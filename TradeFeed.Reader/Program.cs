using System;
using TradeFeed.DataAccess.Model;
using System.Text;
using TradeFeed.DataAccess.Repository;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TradeFeed.Reader
{
    class Program
    {
        private static string _clientApiUrl = "https://localhost:44355";
        private static string _stockApiUrl = "https://localhost:44358";
        private static string _tradeApiUrl = "https://localhost:44392";

        static void Main(string[] args)
        {
            //console app probably not best way to read a stream - there's probably a better way I'm not aware of (haven't done this before)

            RunAsync().GetAwaiter().GetResult();
        }

        private static async Task RunAsync()
        {
            int loadedCount = 0;
            int deadLetterCount = 0;

            IClientAsyncRepository clientRepository = new ClientApiRepository(_clientApiUrl);
            IStockAsyncRepository stockRepository = new StockApiRepository(_stockApiUrl);
            ITradeAsyncRepository tradeRepository = new TradeApiRepository(_tradeApiUrl);
            IDeadLetterAsyncRepository letterRepository = new DeadLetterApiRepository(_tradeApiUrl);

            // Open Feed
            IJsonFeed feed = new JsonMock.JsonFeed();
            if (feed.Status != IJsonFeed.StatusCode.Ok)
            {
                Console.WriteLine("*** Trade Feed could not be opened ***");
                Console.WriteLine(feed.Error);

                return;
            }

            // Read Json Data
            List<TradeFeed> trades = feed.GetTradeFeedData();
            if (feed.Status != IJsonFeed.StatusCode.Ok)
            {
                Console.WriteLine("*** Trade Feed could not be processed - trades added to dead letter queue ***");
                Console.WriteLine(feed.Error);

                DeadLetter dl = new DeadLetter()
                {
                    Body = feed.DeadLetter,
                    Message = feed.Error
                };
                await letterRepository.CreateDeadLetterAsync(dl);

                return;
            }

            // Process Json Data
            foreach (TradeFeed trade in trades)
            {
                string error = ValidateTrade(trade);

                if (!string.IsNullOrEmpty(error))
                {
                    DeadLetter dlr = new DeadLetter()
                    {
                        Body = JsonConvert.SerializeObject(trade),
                        Message = error
                    };
                    await letterRepository.CreateDeadLetterAsync(dlr);
                    deadLetterCount++;
                }
                else
                {
                    //Check if Stock Exists and add if not
                    Stock stock = await stockRepository.GetStockAsync(Int32.Parse(trade.StockId));
                    if (string.IsNullOrEmpty(stock.Name))
                    {
                        stock.Id = Int32.Parse(trade.StockId);
                        stock.Name = trade.StockName;
                        stock.Type = trade.StockType;
                        await stockRepository.CreateStockAsync(stock);
                    }

                    //Check if Client exists and add if not
                    Client client = await clientRepository.GetClientAsync(Int32.Parse(trade.ClientId));
                    if (string.IsNullOrEmpty(client.Name))
                    {
                        client.Id = Int32.Parse(trade.ClientId);
                        client.Name = trade.ClientName;
                        await clientRepository.CreateClientAsync(client);
                    }

                    Trade tr = new Trade()
                    {
                        StockId = Int32.Parse(trade.StockId),
                        ClientId = Int32.Parse(trade.ClientId),
                        Venue = trade.VenueName,
                        Quantity = Int32.Parse(trade.Quantity),
                        Price = decimal.Parse(trade.Price),
                        BuySell = trade.BuySell.ToUpper()
                    };
                    await tradeRepository.CreateTradeAsync(tr);

                    loadedCount++;
                }
            }

            Console.WriteLine("Finished Processing Feed, {0} trades added, {1} dead letters added", loadedCount.ToString(), deadLetterCount.ToString());
        }

        private static void AddError (string e, StringBuilder sb)
        {
            if (sb.Length > 0)
                sb.Append(", ");
            sb.Append(e);
        }

        private static string ValidateTrade(TradeFeed trade)
        {
            StringBuilder sb = new StringBuilder();

            Int32 intOut;
            decimal decOut;

            //Stock Id
            if (string.IsNullOrEmpty(trade.StockId))
                AddError("StockId is required", sb);
            else
            {
                if (!Int32.TryParse(trade.StockId, out intOut))
                    AddError("StockId is not valid", sb);
            }

            //Stock Name
            if (string.IsNullOrEmpty(trade.StockName))
                AddError("StockName is required", sb);

            //Stock Type
            if (string.IsNullOrEmpty(trade.StockType))
                AddError("StockType is required", sb);

            //Client Id
            if (string.IsNullOrEmpty(trade.ClientId))
                AddError("ClientId is required", sb);
            else
            {
                if (!Int32.TryParse(trade.ClientId, out intOut))
                    AddError("ClientId is not valid", sb);
            }

            //Client Name
            if (string.IsNullOrEmpty(trade.ClientName))
                AddError("ClientName is required", sb);

            //Venue Name
            if (string.IsNullOrEmpty(trade.VenueName))
                AddError("VenueName is required", sb);

            //Quantity
            if (string.IsNullOrEmpty(trade.Quantity))
                AddError("Quantity is required", sb);
            else
            {
                if (!Int32.TryParse(trade.Quantity, out intOut))
                    AddError("Quantity is not valid", sb);
            }

            //Price
            if (string.IsNullOrEmpty(trade.Price))
                AddError("Quantity is required", sb);
            else
            {
                if (!decimal.TryParse(trade.Price, out decOut))
                    AddError("Price is not valid", sb);
            }

            if (string.IsNullOrEmpty(trade.BuySell))
                AddError("BuySell is required", sb);
            else
            {
                if (trade.BuySell.ToLower() != "buy" && trade.BuySell.ToLower() != "sell")
                    AddError("BuySell is not valid", sb);
            }

            return sb.ToString();
        }

    }
}
