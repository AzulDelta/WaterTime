using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text;
using WaterTimeServer.Infraestructure.Repositories;
using WaterTimeServer.Shared.DTOs;
using WaterTimeServer.Shared.Entidades;

namespace WaterTimeServer.Application.Services
{
    public class UsuarioService
    {
        private readonly UsuarioRepository _repository;
        private const string Salt = "C7gZ!mV%pQ4fRd@2";
        public UsuarioService(UsuarioRepository repository)
        {
            _repository = repository;
        }

        public static string HashSenha(string senha)
        {
            byte[] bytesSenha = Encoding.UTF8.GetBytes(senha);
            byte[] hashBytes = SHA256.HashData(bytesSenha);

            StringBuilder hashFinal = new();
            foreach (byte b in hashBytes)
            {
                hashFinal.Append(b.ToString("x2"));
            }

            return hashFinal.ToString();
        }


        public async Task CadastrarUsuario(DTOCriarUsuario_req novoUsuario)
        {
            Usuario usuario = new()
            {
                Email = novoUsuario.Login,
                Nome = novoUsuario.Nome,
                SenhaHash = HashSenha(novoUsuario.Senha),
                Celular = novoUsuario.Celular,
                DataNascimento = novoUsuario.DataNascimento,
                HorasDeSono = novoUsuario.HorasDeSono,
                CapacidadeGarrafa = novoUsuario.CapacidadeGarrafa,
                Peso = novoUsuario.Peso,
                IntervaloEmMinutos = novoUsuario.IntervaloEmMinutos,
                MLPorPausa = novoUsuario.MLPorPausa,
                VolumeAtualGarrafaML = novoUsuario.CapacidadeGarrafa
            };

            await _repository.AdicionarAsync(usuario);
        }

        public async Task<Usuario?> Login(string email, string senha)
        {
            Usuario? usuario = await _repository.ObterAsync(email);
            if (usuario == null) return null;

            if (usuario.SenhaHash != HashSenha(senha)) return null;

            return usuario;
        }

        public async Task EditarUsuario(DTOAlterarDados_req usuario)
        {
            Usuario? usuarioExistente = await _repository.ObterPorIdAsync(usuario.IdUsuario);
            if (usuarioExistente == null) throw new Exception("Usuário não encontrado.");

            usuarioExistente.Nome = usuario.Nome;
            usuarioExistente.IntervaloEmMinutos = usuario.IntervaloEmMinutos;
            usuarioExistente.Celular = usuario.Celular;

            usuarioExistente.HorasDeSono = usuario.HorasDeSono;
            usuarioExistente.MLPorPausa = usuario.MLPorPausa;
            usuarioExistente.Peso = usuario.Peso;

            if (usuarioExistente.CapacidadeGarrafa != usuario.CapacidadeGarrafa)
            {
                usuarioExistente.VolumeAtualGarrafaML = usuario.CapacidadeGarrafa;
            }
            usuarioExistente.CapacidadeGarrafa = usuario.CapacidadeGarrafa;
            await _repository.EditarAsync(usuarioExistente);
        }

        public async Task<Usuario?> ObterUsuarioPorId(Guid id)
        {
            return await _repository.ObterPorIdAsync(id);
        }

        public async Task AlterarSenha(DTOAlterarSenha_req usuario)
        {
            Usuario? usuarioExistente = await _repository.ObterPorIdAsync(usuario.IdUsuario);
            if (usuarioExistente == null) throw new Exception("Usuário não encontrado.");

            usuarioExistente.SenhaHash = HashSenha(usuario.Senha);

            await _repository.EditarAsync(usuarioExistente);
        }
    }
}