using System.Threading.Tasks;

namespace TestWebApi.Services
{
    public interface IOrdersProcessorLogger
    {
        public void Log(string exception);
    }
}