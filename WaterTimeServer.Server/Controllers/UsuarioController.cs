using Microsoft.AspNetCore.Mvc;
using WaterTimeServer.Application.Services;
using WaterTimeServer.Shared.DTOs;
using WaterTimeServer.Shared.Entidades;
using WaterTimeServer.Shared.Helpers;

namespace WaterTimeServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _service;
        public UsuarioController(UsuarioService service)
        {
            _service = service;
        }

        // GET: api/<UsuarioController>

        [HttpGet]
        public IActionResult Teste()
        {
            return Ok();
        }

        [HttpPost("teste")]
        public IActionResult TestePost([FromBody] string teste)
        {
            return Ok();
        }

        [HttpPost("enviar-email")]
        public async Task<IActionResult> EnviarEmail([FromBody] string email)
        {
            Random random = new Random();
            int resultado = random.Next(100, 999);
            string numeroOfuscado = OfuscadorNumerico.OfuscarNumero(resultado);

            await EmailHelper.EnviarAsync(email, "Código de Confirmação", $"Seu código de confirmação é {resultado}");

            return Ok(numeroOfuscado);
        }

        [HttpPost("cadastrar-usuario")]
        public async Task<IActionResult> CadastrarUsuario([FromBody] DTOCriarUsuario_req novoUsuario)
        {
            if (novoUsuario == null) return BadRequest();
            await _service.CadastrarUsuario(novoUsuario);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] DTOLogin_req loginRequest)
        {            
            Usuario? usuario = await _service.Login(loginRequest.Email, loginRequest.Senha);
            if (usuario == null) return BadRequest();
            return Ok(usuario);
        }

        [HttpGet("obter-usuario/{id}")]
        public async Task<IActionResult> ObterUsuario(Guid id)
        {
            Usuario? usuario = await _service.ObterUsuarioPorId(id);
            if (usuario == null) return NotFound();

            return Ok(usuario);
        }

        [HttpPost("editar-usuario")]
        public async Task<IActionResult> EditarUsuario([FromBody] DTOAlterarDados_req usuario)
        {
            if (usuario == null) return BadRequest();
            await _service.EditarUsuario(usuario);
            return Ok();
        }

        [HttpPost("alterar-senha")]
        public async Task<IActionResult> AlterarSenha([FromBody] DTOAlterarSenha_req usuario)
        {
            if (usuario == null) return BadRequest();
            await _service.AlterarSenha(usuario);
            return Ok();
        }
    }
}

