using System.ComponentModel.DataAnnotations;
using WaterTimeServer.Shared.Entidades;

namespace WaterTimeServer.Shared.DTOs
{
    public sealed class DTOAlterarDados_req
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid IdUsuario { get; set; } = Guid.Empty;

        [MaxLength(50,ErrorMessage ="O nome não deve ultrapassar 50 caracteres")]
        public string Nome { get; set; } = string.Empty;

        [Phone(ErrorMessage = "O número de celular deve ser válido")]
        public string Celular { get; set; } = string.Empty;
        
        [Range(1,23,ErrorMessage ="O valor precisa estar entre 1 e 23 horas")]
        public int HorasDeSono { get; set; } = 0;

        public DateTime? DataNascimento { get; set; } = DateTime.Now.AddYears(-1);

        [Range(1, int.MaxValue, ErrorMessage = "O valor não pode ser negativo")]
        public int CapacidadeGarrafa { get; set; } = 0;

        [Range(1, int.MaxValue, ErrorMessage = "O valor não pode ser negativo")]
        public int IntervaloEmMinutos { get; set; } = 0;

        [Range(1,int.MaxValue,ErrorMessage ="O valor não pode ser negativo")]
        public int MLPorPausa { get; set; } = 0;

        [Range(1, int.MaxValue, ErrorMessage = "O valor não pode ser negativo")]
        public int Peso { get; set; } = 0;
    }
}

