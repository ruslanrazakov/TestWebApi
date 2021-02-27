using System;
using System.IO;
using System.Threading.Tasks;

namespace TestWebApi.Services
{
    public class OrdersProcessorLogger : IOrdersProcessorLogger
    {
        readonly string _filePath = AppDomain.CurrentDomain.BaseDirectory + "OrdersProcessorLogger.txt";
        readonly int _delayTime = 10000;

        public void Log(string exception)
        {
            Task.Run(async () =>
            {
                using (FileStream stream = new FileStream(_filePath, FileMode.Append, FileAccess.Write, FileShare.None, 4096, true))
                    using (StreamWriter sw = new StreamWriter(stream))
                    {
                        await sw.WriteLineAsync(exception + Environment.NewLine);
                    }
                await Task.Delay(_delayTime);
            });
        }
    }
}