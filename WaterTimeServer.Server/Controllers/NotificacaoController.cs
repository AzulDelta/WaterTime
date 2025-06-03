using Microsoft.AspNetCore.Mvc;
using WaterTimeServer.Application.Services;
using WaterTimeServer.Shared.DTOs;
using WaterTimeServer.Shared.Entidades;
using WaterTimeServer.Shared.Enums;
using WaterTimeServer.Shared.Helpers;

namespace WaterTimeServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificacaoController : ControllerBase
    {
        private readonly NotificacaoService _service;
        public NotificacaoController(NotificacaoService service)
        {
            _service = service;
        }

        // GET: api/<UsuarioController>

        [HttpGet]
        public IActionResult Teste()
        {
            return Ok("Funcionou!");
        }

        [HttpGet("obter-notificacao/{usuarioId}")]
        public async Task<ActionResult<TiposNotificacao>> ObterNotificacao([FromRoute] Guid usuarioId)
        {
            if (usuarioId == Guid.Empty) return BadRequest("Usuário inválido.");
            
            TiposNotificacao precisaNotificar = await _service.ObterNotificacaoAsync(usuarioId);            

            return Ok(precisaNotificar);
        }

        [HttpPost("guardar-resposta")]
        public async Task<IActionResult> GuardarResposta([FromBody] DTOResposta_req respostaReq)
        {
            if (respostaReq == null) return BadRequest("Dados inválidos.");
            await _service.GuardarResposta(respostaReq.UserId, respostaReq.BebeuAgua);
            return Ok();
        }

        [HttpGet("obter-dados/{usuarioId}")]
        public async Task<IActionResult> ObterDados([FromRoute] Guid usuarioId)
        {
            if (usuarioId == Guid.Empty) return BadRequest("Usuário inválido.");
            var retorno = await _service.ObterRespostasAsync(usuarioId);
            return Ok(retorno);
        }

    }
}

