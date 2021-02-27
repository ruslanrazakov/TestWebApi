using System.Text.Json;
using TestWebApi.Converters;
using TestWebApi.Models;

namespace TestWebApi.OrderProcessorLib
{
    /// <summary>
    /// Handler for every order's SystemType.
    /// Implement this interface if you need a new handler
    /// </summary>
    public interface  IOrderHandler
    {
        public SystemType Type { get; }
        public string Process(string sourceOrder);
    }
}