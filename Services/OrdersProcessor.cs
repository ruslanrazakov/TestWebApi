using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestWebApi.Data;
using TestWebApi.Models;
using TestWebApi.OrderProcessorLib;

namespace TestWebApi.Services
{
    public class OrdersProcessor : IOrdersProcessor
    {
        ApplicationContext _context;
        IOrdersProcessorLogger _logger;

        public OrdersProcessor(ApplicationContext context, IOrdersProcessorLogger logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task Init()
        {
            try
            {
                var orders = _context.Orders.Where(o => o.ConvertedOrder == null || o.ConvertedOrder == String.Empty).ToList();
                
                OrderProcessorExchange processor = new OrderProcessorExchange();
                foreach(var o in orders)
                {
                    o.ConvertedOrder = processor.ProcessHandlers[o.SystemType](o.SourceOrder);
                }
                
                await _context.SaveChangesAsync();
            }
            catch(Exception exception)
            {
                _logger.Log(exception.ToString());
            }
        }
    }
}