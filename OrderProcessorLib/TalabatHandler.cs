using System.Text.Json;
using TestWebApi.Models;

namespace TestWebApi.OrderProcessorLib
{
    public class TalabatHandler : IOrderHandler
    {
        public  SystemType Type { get => SystemType.Talabat; }

        public string Process(string sourceOrder)
        {
            var convertedOrderModel = JsonSerializer.Deserialize<RawOrderModel>(sourceOrder, Constants.SerializeOptions.CustomOptions());
            foreach(var product in convertedOrderModel.Products)
            {
                product.PaidPrice = - product.PaidPrice;
            }

            return JsonSerializer.Serialize<RawOrderModel>(convertedOrderModel, Constants.SerializeOptions.CustomOptions());
        }
    }
}