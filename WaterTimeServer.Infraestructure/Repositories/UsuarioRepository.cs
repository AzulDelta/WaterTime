using Microsoft.EntityFrameworkCore;
using WaterTimeServer.Infraestructure.Contextos;
using WaterTimeServer.Shared.Entidades;

namespace WaterTimeServer.Infraestructure.Repositories;
public class UsuarioRepository(WaterTimeContext context)
{
    private readonly WaterTimeContext context = context;

    public async Task<Usuario?> ObterAsync(string email)
    {
        return await context
            .Usuarios
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<Usuario?> ObterPorIdAsync(Guid id)
    {
        return await context
            .Usuarios
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task EditarAsync(Usuario usuario)
    {
        context.Usuarios
            .Update(usuario);
        await context.SaveChangesAsync();
    }

    public async Task AdicionarAsync(Usuario usuario)
    {
        await context
            .Usuarios
            .AddAsync(usuario);
        await context.SaveChangesAsync();
    }

}

