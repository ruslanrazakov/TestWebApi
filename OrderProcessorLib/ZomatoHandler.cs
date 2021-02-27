using TestWebApi.Models;
using System.Text.Json;

namespace TestWebApi.OrderProcessorLib
{
    public class ZomatoHandler : IOrderHandler
    {
        public SystemType Type { get => SystemType.Zomato; }

        public string Process(string sourceOrder)
        {
            var convertedOrderModel = JsonSerializer.Deserialize<RawOrderModel>(sourceOrder, Constants.SerializeOptions.CustomOptions());
            foreach(var product in convertedOrderModel.Products)
            {
                product.PaidPrice = product.PaidPrice / product.Quantity;
            }

            return JsonSerializer.Serialize<RawOrderModel>(convertedOrderModel, Constants.SerializeOptions.CustomOptions());
        }
    }
}