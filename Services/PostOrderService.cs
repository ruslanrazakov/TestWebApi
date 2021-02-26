using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TestWebApi.Data;
using TestWebApi.Models;

namespace TestWebApi.Services
{
    public class PostOrderService : IPostOrderService
    {
        ApplicationContext _context;

        public PostOrderService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task <bool> PostOrder(SystemType type, Stream requestBody)
        {
            string requestResult = String.Empty;
            using (StreamReader reader = new StreamReader(requestBody, Encoding.UTF8))
            {  
                requestResult = await reader.ReadToEndAsync();
            }

            if(requestResult == String.Empty)
                return false;

            var serializeOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
                
            };

            RawOrderModel sourceOrder = JsonSerializer.Deserialize<RawOrderModel>(requestResult, serializeOptions);
            OrderModel orderModel = new OrderModel()
            {
                OrderNumber = sourceOrder.OrderNumber,
                SystemType = type,
                SourceOrder = requestResult,
                CreatedAt = DateTime.UtcNow
            };

            _context.Orders.Add(orderModel);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}