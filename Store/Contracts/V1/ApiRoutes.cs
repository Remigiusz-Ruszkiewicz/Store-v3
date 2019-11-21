using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Contracts.V1
{
    public static class ApiRoutes
    {
        public const string Base = "api/v1/";
        public static class Products
        {
            public const string GetAll = Base + "products";
            public const string Get = Base + "products/{id}";
            public const string Add = Base + "products";
            public const string Update = Base + "products/{id}";
            public const string Delete = Base + "products/{id}";
        }

        public static class Users
        {
            public const string Add = Base + "users";
            public const string Login = Base + "users/login";
        }
    }
}
