using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TradeFeed.StockApi.Contracts.v1
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

            public const string Update = Base + "/stock/update/{id}";

            public const string Create = Base + "/stock/create";
        }
    }
}
