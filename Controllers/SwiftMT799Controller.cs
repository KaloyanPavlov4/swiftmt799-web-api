
using Microsoft.AspNetCore.Mvc;
using swiftmt799_web_api.Models;
using swiftmt799_web_api.Services;

namespace swiftmt799_web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SwiftMT799Controller : ControllerBase
    {
        ISwiftMT799Service swiftService;

        public SwiftMT799Controller(ISwiftMT799Service swiftService)
        {
            this.swiftService = swiftService;
        }

        [HttpPost]
        public async Task PostAsync(IFormFile uploadedFile)
        {
            await swiftService.SaveAsync(uploadedFile);
        }

        [HttpGet]
        public async Task<IEnumerable<SwiftMT799Message>> FindAllAsync(IConfiguration configuration)
        {
            return await swiftService.FindAllAsync();
        }
    }
}
