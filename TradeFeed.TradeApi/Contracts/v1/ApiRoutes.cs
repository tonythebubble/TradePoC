using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TradeFeed.TradeApi.Contracts.v1
{
    public class ApiRoutes
    {
        public const string Root = "api";
        public const string Version = "v1";

        public const string Base = Root + "/" + Version;

        public static class Trade
        {
            public const string Get = Base + "/trade/all";

            public const string GetById = Base + "/trade/byId/{id}";

            public const string Create = Base + "/trade/create";
        }

        public static class DeadLetter
        {
            public const string Get = Base + "/deadLetter/all";

            public const string GetById = Base + "/deadLetter/byId/{id}";

            public const string Create = Base + "/deadLetter/create";
        }
    }
}
