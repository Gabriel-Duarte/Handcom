using Handcom.Domain.DataAccess.Pagination.Base;
using Handcom.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handcom.Domain.DataAccess.Interfaces.Base
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        Task<List<TEntity>> PaginateAsync(IQueryable<TEntity> query, Pageable pageable, CancellationToken cancellation);
        Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellation);
        Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellation);
        Task<List<TEntity>> CreateAsync(List<TEntity> entity, CancellationToken cancellation);
        Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellation);
        Task<List<TEntity>> UpdateAsync(List<TEntity> entity, CancellationToken cancellation);
        Task<TEntity> DeleteAsync(TEntity entity, CancellationToken cancellation);
        Task<List<TEntity>> DeleteAsync(List<TEntity> entities, CancellationToken cancellation);
        Task CommitAsync();
    }
}
