using System.Text.Json;
using TestWebApi.Converters;

namespace TestWebApi.Constants
{
    public class SerializeOptions
    {
        public static JsonSerializerOptions CustomOptions()
        {
            return  new JsonSerializerOptions
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

        public static JsonSerializerOptions OtherOptions()
        {
            return new JsonSerializerOptions();
        }
    }
}