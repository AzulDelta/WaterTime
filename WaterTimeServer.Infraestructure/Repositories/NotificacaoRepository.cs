using Microsoft.EntityFrameworkCore;
using WaterTimeServer.Infraestructure.Contextos;
using WaterTimeServer.Shared.Entidades;

namespace WaterTimeServer.Infraestructure.Repositories;
public class NotificacaoRepository(WaterTimeContext context)
{
    private readonly WaterTimeContext context = context;

    public async Task<List<Resposta>> ObterTodasPorUsuarioAsync(Guid usuarioId)
    {
        return await context
            .Respostas
            .Where(r => r.IdUsuario == usuarioId).ToListAsync();
    }    

    public async Task<List<Resposta>> ObterPorDataAsync(DateTime dataInicio, DateTime dataFim, Guid usuarioId)
    {
        return await context
            .Respostas
            .Where(r => r.IdUsuario == usuarioId)
            .Where(r => r.DataHora <= dataFim && r.DataHora >= dataInicio).ToListAsync();
    }

    public async Task EditarAsync(Resposta resposta)
    {
        context.Respostas
            .Update(resposta);
        await context.SaveChangesAsync();
    }

    public async Task AdicionarAsync(Resposta resposta)
    {
        await context
            .Respostas
            .AddAsync(resposta);
        await context.SaveChangesAsync();
    }

}

