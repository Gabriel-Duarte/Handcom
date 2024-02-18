using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handcom.Data.Data.Uow.Interface
{
    public interface IUnitOfWork : IDisposable, IAsyncDisposable
    {
        Task<bool> CommitAsync();
    }
}
