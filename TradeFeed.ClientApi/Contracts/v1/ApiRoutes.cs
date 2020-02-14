using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TradeFeed.ClientApi.Contracts.v1
{
    public class ApiRoutes
    {
        public const string Root = "api";
        public const string Version = "v1";

        public const string Base = Root + "/" + Version;

        public static class Client
        {
            public const string Get = Base + "/client/all";

            public const string GetById = Base + "/client/byId/{id}";

            public const string Update = Base + "/client/update/{id}";

            public const string Create = Base + "/client/create";
        }
    }
}
