using System.IO;
using System.Threading.Tasks;
using TestWebApi.Models;

namespace TestWebApi.Services
{
    public interface IPostOrderService
    {
        public Task <bool> Post(SystemType type, Stream requestBody);
    }
}