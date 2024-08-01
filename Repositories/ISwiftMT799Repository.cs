using swiftmt799_web_api.Models;

namespace swiftmt799_web_api.Repositories
{
    public interface ISwiftMT799Repository
    {
        Task<IEnumerable<SwiftMT799Message>> FindAllAsync();

        Task SaveAsync(SwiftMT799Message message);
    }
}
