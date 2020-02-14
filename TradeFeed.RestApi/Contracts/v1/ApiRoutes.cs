using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TradeFeed.RestApi.Contracts.v1
{
    public class ApiRoutes
    {
        public const string Root = "api";
        public const string Version = "v1";

        public const string Base = Root + "/" + Version;

        public static class Stock
        {
            public const string Get = Base + "/stock/all";

            public const string GetById = Base + "/stock/byId/{id}";
        }

        public static class Client
        {
            public const string Get = Base + "/client/all";

            public const string GetById = Base + "/client/byId/{id}";
        }

        public static class Trade
        {
            public const string Get = Base + "/trade/all";

            public const string GetById = Base + "/trade/byId/{id}";

            public const string GetByClientId = Base + "/trade/byClientId/{id}";

            public const string GetByStockId = Base + "/trade/byStockId/{id}";
        }

        public static class DeadLetter
        {
            public const string Get = Base + "/deadLetter/all";

            public const string GetById = Base + "/deadLetter/byId/{id}";
        }
    }
}
