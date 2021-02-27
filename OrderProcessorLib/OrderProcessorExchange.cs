using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TestWebApi.Models;

namespace TestWebApi.OrderProcessorLib
{
    /// <summary>
    /// Initializes handlers for all types of Order's system type.
    /// To add new handler create new ***Handler.cs, inherit it from IOrderHandler,
    /// implement interface, and add to Dictionary ProcessHandlers
    /// </summary>
    public class OrderProcessorExchange
    {
        public Dictionary<SystemType, Func<string, string>> ProcessHandlers;

        public OrderProcessorExchange()
        {
            ProcessHandlers = new Dictionary<SystemType, Func<string, string>>
            {
                { SystemType.Zomato, new ZomatoHandler().Process },
                { SystemType.Talabat, new TalabatHandler().Process },
                { SystemType.Uber, new UberHandler().Process }
            };
        }
    }
}