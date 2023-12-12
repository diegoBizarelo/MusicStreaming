using Microsoft.AspNetCore.Mvc;
using Spoticry.Application.PlanoApp;

namespace Spoticry.Plano.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanoController : ControllerBase
    {
        private readonly PlanoService _planoService = new PlanoService();

        [HttpGet("{id}")]
        public IActionResult ObterPlano(Guid id)
        {
            var result = _planoService.ObterPlano(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}
