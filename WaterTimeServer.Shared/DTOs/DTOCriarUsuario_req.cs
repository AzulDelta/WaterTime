using System.ComponentModel.DataAnnotations;
using WaterTimeServer.Shared.Entidades;

namespace WaterTimeServer.Shared.DTOs
{
    public sealed class DTOCriarUsuario_req
    {
        public Guid Id { get; set; } = Guid.Empty;

        [Required(ErrorMessage = "É necessário um Email confirmado")]
        public string Login { get; set; } = string.Empty;

        [Required(ErrorMessage = "É necessário um Nome")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "É necessário uma Senha")]
        public string Senha { get; set; } = string.Empty;

        [Required(ErrorMessage = "É necessário confirmar a Senha")]
        [Compare("Senha", ErrorMessage = "As senhas não conferem")]
        public string ConfirmarSenha { get; set; } = string.Empty;

        [Required(ErrorMessage = "É necessário um número de celular")]
        [Phone(ErrorMessage = "O número de celular deve ser válido")]
        public string Celular { get; set; } = string.Empty;

        [Required(ErrorMessage = "É necessário informar a Data de Nascimento")]
        [DataType(DataType.Date, ErrorMessage = "A data de nascimento deve ser válida")]        
        public DateTime? DataNascimento { get; set; } = DateTime.Now.AddYears(-1);

        [Required(ErrorMessage = "É necessário informar as horas de sono por noite")]
        [Range(1, 23, ErrorMessage = "O valor precisa estar entre 1 e 23 horas")]
        public int HorasDeSono { get; set; } = 8;

        [Required(ErrorMessage = "É necessário informar a capacidade de sua garrafa d'água")]
        [Range(1, int.MaxValue, ErrorMessage = "O valor não pode ser menor que 1")]
        public int CapacidadeGarrafa { get; set; } = 100;

        [Required(ErrorMessage = "É necessario informar o intervalo entre as pausas")]
        [Range(2, int.MaxValue, ErrorMessage = "O valor não pode ser menor que 2 minutos")]
        public int IntervaloEmMinutos { get; set; } = 60;

        [Required(ErrorMessage = "É necessário informar a quantidade de ml por pausa")]
        [Range(1, int.MaxValue, ErrorMessage = "O valor não pode ser menor que 1")]
        public int MLPorPausa { get; set; } = 175;

        [Required(ErrorMessage = "É necessário informar o peso")]
        [Range(1, int.MaxValue, ErrorMessage = "O valor não pode ser negativo")]
        public int Peso { get; set; } = 70;
    }
}

