namespace WaterTimeServer.Domain.Interfaces
{
    public interface IRepositoryComEmail<TEntidade> : IBaseRepository<TEntidade> where TEntidade : class
    {
        Task<TEntidade?> GetByEmailAsync(string email);
    }


}

