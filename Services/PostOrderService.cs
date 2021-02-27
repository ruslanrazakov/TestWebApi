using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TestWebApi.Converters;
using TestWebApi.Data;
using TestWebApi.Models;

namespace TestWebApi.Services
{
    public class PostOrderService : IPostOrderService
    {
        readonly ApplicationContext _context;
        readonly JsonSerializerOptions _serializerOptions;

        public PostOrderService(ApplicationContext context)
        {
            _context = context;
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters =
                    {
                        new Int32Converter(),
                        new Int64Converter(),
                        new DecimalConverter()       
                    }
            };
        }

        public async Task <bool> PostAsync(SystemType type, Stream requestBody)
        {
            var requestResult = GetRequestResultAsync(requestBody).Result;

            if(requestResult == String.Empty)
                return false;

            _context.Orders.Add(FillOrderModel(requestResult, type));
            await _context.SaveChangesAsync();
            return true;
        }

        private OrderModel FillOrderModel(string requestResult, SystemType type)
        {
            
            RawOrderModel sourceOrder = JsonSerializer.Deserialize<RawOrderModel>(requestResult, _serializerOptions);
            return new OrderModel()
            {
                OrderNumber = sourceOrder.OrderNumber,
                SystemType = type,
                SourceOrder = requestResult,
                CreatedAt = DateTime.UtcNow
            };
        }

        private async Task <string> GetRequestResultAsync(Stream requestBody)
        {
            String requestResult = String.Empty;
            using (StreamReader reader = new StreamReader(requestBody, Encoding.UTF8))
            {  
                requestResult = await reader.ReadToEndAsync();
            }
            return requestResult;      
        }  
    }
}