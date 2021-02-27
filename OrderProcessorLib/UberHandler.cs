using TestWebApi.Models;
using System.Text.Json;
using TestWebApi.Converters;
using System;

namespace TestWebApi.OrderProcessorLib
{
    public class UberHandler : IOrderHandler
    {
        public  SystemType Type { get => SystemType.Uber; }

        public string Process(string sourceOrder)
        {
            throw new NotImplementedException("Hello! I am an exception from UberHandler!");
        }
    }
}