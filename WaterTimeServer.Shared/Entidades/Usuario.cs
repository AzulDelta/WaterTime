using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WaterTimeServer.Shared.Entidades;
public class Usuario
{    
    [Key]    
    public Guid Id { get; set; } = Guid.NewGuid();
    
    public string Email { get; set; } = string.Empty;
    
    public string Nome { get; set; } = string.Empty;    
    
    public string SenhaHash { get; set; } = string.Empty;

    public DateTime? DataNascimento { get; set; } = DateTime.UtcNow;

    public int HorasDeSono { get; set; } = 0;

    public int IntervaloEmMinutos { get; set; } = 0;

    public int MLPorPausa { get; set; } = 0;    

    public int Peso { get; set; } = 0;

    public int CapacidadeGarrafa { get; set; } = 0;

    public int VolumeAtualGarrafaML { get; set; } = 0;
    public string Celular { get; set; } = string.Empty;

    public DateTime UltimateMensagem { get; set; } = DateTime.UtcNow;

    public List<Resposta>? Respostas { get; set; } = new List<Resposta>();
}

