using Core.DTO;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/search")]
    public class SearchController : ControllerBase
    {
        private readonly IManagerFactory _managerFactory;

        public SearchController(IManagerFactory managerFactory)
        {
            _managerFactory = managerFactory;
        }

        [HttpPost]
        public async Task<IActionResult> Search([FromBody] SearchRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request), "Search request cannot be null.");
            }

            var manager = _managerFactory.CreateManager(request);
            var result = await manager.Search(request);
            return Ok(result);
        }
    }
}
