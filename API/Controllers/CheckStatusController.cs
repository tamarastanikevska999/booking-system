using Core.DTO;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/check-status")]
    public class CheckStatusController : ControllerBase
    {
        private readonly IManagerFactory _managerFactory;

        public CheckStatusController(IManagerFactory managerFactory)
        {
            _managerFactory = managerFactory;
        }

        [HttpPost]
        public async Task<IActionResult> CheckStatus([FromBody] CheckStatusRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request), "Check status request cannot be null.");
            }

            var manager = _managerFactory.CreateManager(new SearchRequest());
            var result = await manager.CheckStatus(request);
            return Ok(result);
        }
    }
}
