using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Spoticry.API.Controllers;
using Spoticry.Application.Conta;
using Spoticry.Application.Conta.Dto;

namespace Spoticry.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : BaseController
    {
        private readonly UsuarioService _service = new UsuarioService();

        public UsuarioController()
        {
        }


        [HttpPost]
        public async Task<IActionResult> CriarConta(UsuarioDto dto)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            var result = await _service.CriarConta(dto);

            return Created($"/usuario/{dto.Id}", result);
        }

        [HttpGet("{id}")]
        public IActionResult ObterUsuario(Guid id)
        {
            var result = _service.ObterUsuario(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost("{id}/favoritar")]
        public async Task<IActionResult> FavoritarMusica(Guid id, FavoritarDto dto)
        {
            await _service.FavoritarMusica(id, dto.IdMusica, dto.IdPlayList);
            return Ok();
        }

        [HttpPost("{id}/criarPlayList")]
        public async Task<IActionResult> criarPlayList(Guid id, string playListNome)
        {
            var result = await _service.CriarPlayList(id, playListNome);

            if (!result)
                return NotFound();

            return Ok();
        }

        [HttpGet("buscarMusica/{nome}")]
        public async Task<IActionResult> buscarMusica(string nome)
        {
            var result = await _service.buscarMusica(nome);
            return Ok(result);
        }

        [HttpGet("buscarBanda/{nome}")]
        public async Task<IActionResult> buscarBanda(string nome)
        {
            var result = await _service.buscarBanda(nome);
            return Ok(result);
        }
    }
}
