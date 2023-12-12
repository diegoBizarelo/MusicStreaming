using Microsoft.AspNetCore.Mvc;
using Spoticry.Application.MusicaApp;

namespace Spoticry.Musica.API.Controllers
{
    [Route("api/[controller]")]
    public class MusicaController : ControllerBase
    {
        private readonly MusicaService _musicaService = new MusicaService();

        [HttpGet("{id}")]
        public IActionResult ObterMusica(Guid id)
        {
            var result = _musicaService.ObterMusica(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("buscarMusica/{nome}")]
        public IActionResult BuscarMusica(string nome)
        {
            var result = _musicaService.BuscarMusica(nome);
            if (result == null)
                return Ok();

            return Ok(result);
        }

        [HttpGet("buscarBanda/{nome}")]
        public IActionResult BuscarBanda(string nome)
        {
            var result = _musicaService.BuscarBanda(nome);
            if (result == null)
                return Ok();

            return Ok(result);
        }
    }
}
