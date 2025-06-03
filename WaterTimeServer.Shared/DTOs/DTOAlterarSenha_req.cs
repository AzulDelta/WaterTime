using System.ComponentModel.DataAnnotations;
using WaterTimeServer.Shared.Entidades;

namespace WaterTimeServer.Shared.DTOs
{
    public sealed class DTOAlterarSenha_req
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid IdUsuario { get; set; } = Guid.Empty;


        [Required(ErrorMessage = "É necessário um Email confirmado")]
        public string Login { get; set; } = string.Empty;

        [Required(ErrorMessage = "É necessário uma Senha")]
        public string Senha { get; set; } = string.Empty;

        [Required(ErrorMessage = "É necessário confirmar a Senha")]
        [Compare("Senha", ErrorMessage = "As senhas não conferem")]
        public string ConfirmarSenha { get; set; } = string.Empty;
    }
}

