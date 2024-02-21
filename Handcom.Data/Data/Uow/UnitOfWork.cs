using Handcom.Data.Data.Uow.Interface;

namespace Handcom.Data.Data.Uow
{
    public class UnitOfWork : IUnitOfWork
    {
        private const int EMPTY = 0;
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context) =>
            _context = context;

        public async Task<bool> CommitAsync() =>
            await _context.SaveChangesAsync().ConfigureAwait(false) > EMPTY;

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync().ConfigureAwait(false);
            GC.SuppressFinalize(this);
        }
    }
}
