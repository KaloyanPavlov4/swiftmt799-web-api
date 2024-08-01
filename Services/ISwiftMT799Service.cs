
using swiftmt799_web_api.Models;

namespace swiftmt799_web_api.Services
{
    public interface ISwiftMT799Service
    {
        Task<IEnumerable<SwiftMT799Message>> FindAllAsync();

        Task SaveAsync(IFormFile file);
    }
}
