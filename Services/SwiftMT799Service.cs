using swiftmt799_web_api.Models;
using swiftmt799_web_api.Repositories;
using System.Text.RegularExpressions;
using System.Text;
using swiftmt799_web_api.Utils;

namespace swiftmt799_web_api.Services
{
    public class SwiftMT799Service : ISwiftMT799Service
    {
        private ISwiftMT799Repository repository;

        public SwiftMT799Service(ISwiftMT799Repository repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<SwiftMT799Message>> FindAllAsync()
        {
            return await repository.FindAllAsync();
        }

        public async Task SaveAsync(IFormFile file)
        {
            var fileSb = new StringBuilder();
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                while (reader.Peek() >= 0)
                    fileSb.AppendLine(reader.ReadLine());
            }
            var input = fileSb.ToString();
            input = Regex.Replace(input, @"\s+", " ");
            input = input.Trim().Replace("\n", "").Replace("\r", "");
            SwiftMT799Message message = SwiftMT799Factory.createFromString(input);
            await repository.SaveAsync(message);
        }
    }
}
