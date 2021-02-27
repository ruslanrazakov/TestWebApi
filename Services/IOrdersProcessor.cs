using System.Threading.Tasks;

namespace TestWebApi.Services
{
    public interface IOrdersProcessor
    {
        public Task Init();
    }
}