using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WaterTimeServer.Shared.Entidades;
public class Resposta
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid IdUsuario { get; set; } = Guid.Empty;
    public Usuario? Usuario { get; set; } = null!;

    public DateTime DataHora { get; set; } = DateTime.Now;
    public bool BebeuAgua { get; set; } = false;

    public int QuantidadeEmML { get; set; } = 0;    

}

