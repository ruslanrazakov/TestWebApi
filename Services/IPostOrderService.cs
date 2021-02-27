using System.IO;
using System.Threading.Tasks;
using TestWebApi.Models;

namespace TestWebApi.Services
{
    public interface IPostOrderService
    {
        public Task <bool> PostAsync(SystemType type, Stream requestBody);
    }
}