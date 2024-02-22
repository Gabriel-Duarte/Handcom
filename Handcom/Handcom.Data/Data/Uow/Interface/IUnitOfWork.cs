namespace Handcom.Data.Data.Uow.Interface
{
    public interface IUnitOfWork : IDisposable, IAsyncDisposable
    {
        Task<bool> CommitAsync();
    }
}
