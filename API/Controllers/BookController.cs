using Core.DTO;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/book")]
    public class BookController : ControllerBase
    {
        private readonly IManagerFactory _managerFactory;

        public BookController(IManagerFactory managerFactory)
        {
            _managerFactory = managerFactory;
        }

        [HttpPost]
        public async Task<IActionResult> Book([FromBody] BookRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request), "Book request cannot be null.");
            }

            var manager = _managerFactory.CreateManager(request.SearchRequest);
            var result = await manager.Book(request);
            return Ok(result);
        }
    }
}
