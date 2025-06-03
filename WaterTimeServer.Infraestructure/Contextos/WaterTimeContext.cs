using Microsoft.EntityFrameworkCore;
using WaterTimeServer.Shared.Entidades;

namespace WaterTimeServer.Infraestructure.Contextos;

public sealed class WaterTimeContext(DbContextOptions<WaterTimeContext> opcoes) : DbContext(opcoes)
{

    /* ------------ DbSets ------------ */
    public DbSet<Usuario> Usuarios { get; set; } = null!;    
    public DbSet<Resposta> Respostas { get; set; } = null!;

    protected override void OnModelCreating(
    ModelBuilder construtor)
    {
        base.OnModelCreating(construtor);

        /*************** Usuario *****************/
        construtor.Entity<Usuario>(usuario =>
        {            
            usuario.HasMany(e=> e.Respostas)
                .WithOne(e => e.Usuario)
                .HasForeignKey(e => e.IdUsuario)
                .OnDelete(DeleteBehavior.NoAction);
        });
    }

}

