using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text;
using WaterTimeServer.Infraestructure.Repositories;
using WaterTimeServer.Shared.DTOs;
using WaterTimeServer.Shared.Entidades;
using WaterTimeServer.Shared.Enums;

namespace WaterTimeServer.Application.Services
{
    public class NotificacaoService(NotificacaoRepository notificacaoRepository, UsuarioRepository usuarioRepository)
    {
        private readonly NotificacaoRepository _notificacaoRepository = notificacaoRepository;
        private readonly UsuarioRepository _usuarioRepository = usuarioRepository;

        public async Task GuardarResposta(Guid usuarioId, bool bebeu)
        {
            if (usuarioId == Guid.Empty) return;

            Usuario? usuario = await _usuarioRepository.ObterPorIdAsync(usuarioId);
            if (usuario == null) return;

            Resposta resposta = new()
            {
                IdUsuario = usuarioId,
                DataHora = DateTime.UtcNow,
                BebeuAgua = bebeu,
                QuantidadeEmML = usuario.MLPorPausa
            };

            await _notificacaoRepository.AdicionarAsync(resposta);

            usuario.UltimateMensagem = resposta.DataHora;

            if (bebeu) usuario.VolumeAtualGarrafaML -= usuario.MLPorPausa;

            await _usuarioRepository.EditarAsync(usuario);
        }

        public async Task<TiposNotificacao> ObterNotificacaoAsync(Guid usuarioId)
        {
            if (usuarioId == Guid.Empty) throw new ArgumentNullException(nameof(usuarioId), "UsuarioId não pode ser nulo.");

            Usuario? usuario = await _usuarioRepository.ObterPorIdAsync(usuarioId);
            if (usuario == null) return TiposNotificacao.SemAviso;

            DateTime ultimaData = usuario.UltimateMensagem!;
            if (ultimaData.AddMinutes(usuario.IntervaloEmMinutos) < DateTime.UtcNow)
            {
                return TiposNotificacao.AvisoDeHidratacao;
            }
            if (usuario.VolumeAtualGarrafaML <= 0)
            {
                usuario.VolumeAtualGarrafaML = usuario.CapacidadeGarrafa;
                await _usuarioRepository.EditarAsync(usuario);
                return TiposNotificacao.AvisoDeEncherGarrafinha;
            }

            return TiposNotificacao.SemAviso;
        }

        public async Task<List<Resposta>> ObterRespostasAsync(Guid usuarioId)
        {
            if (usuarioId == Guid.Empty) throw new ArgumentNullException(nameof(usuarioId), "UsuarioId não pode ser nulo.");
            return await _notificacaoRepository.ObterTodasPorUsuarioAsync(usuarioId);
        }
    }

}