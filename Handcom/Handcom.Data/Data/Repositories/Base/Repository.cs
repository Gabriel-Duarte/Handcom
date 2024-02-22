using Handcom.Data.Data.Uow.Interface;
using Handcom.Domain.DataAccess.Interfaces.Base;
using Handcom.Domain.DataAccess.Pagination.Base;
using Handcom.Domain.Models;
using Microsoft.EntityFrameworkCore;


namespace Handcom.Data.Data.Repositories.Base
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        protected const int LESS_ONE = 1;
        protected const int DIVIDE_BY_FIVE = 5;
        protected const int LAST_THIRTY_DAYS = -30;

        protected readonly IUnitOfWork _uow;
        private readonly AppDbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        protected Repository(IUnitOfWork uow, AppDbContext context)
        {
            _uow = uow;
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        protected AppDbContext Context() => _context;

        public virtual async Task<List<TEntity>> PaginateAsync(IQueryable<TEntity> query, Pageable pageable, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                return await query
                                .AsNoTrackingWithIdentityResolution()
                                .Skip((pageable.Page - LESS_ONE) * pageable.Size)
                                .Take(pageable.Size)
                                .ToListAsync(cancellationToken)
                                .ConfigureAwait(false);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public virtual async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                return await _dbSet.AsNoTracking().FirstOrDefaultAsync(e => e.Id.Equals(id), cancellationToken).ConfigureAwait(false);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public virtual async Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var result = await _dbSet.AddAsync(entity, cancellationToken).ConfigureAwait(false);
                await _uow.CommitAsync().ConfigureAwait(false);
                return result.Entity;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public virtual async Task<List<TEntity>> CreateAsync(List<TEntity> entities, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                await _dbSet.AddRangeAsync(entities, cancellationToken).ConfigureAwait(false);
                await _uow.CommitAsync().ConfigureAwait(false);
                return entities;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var result = _dbSet.Update(entity);
                await _uow.CommitAsync().ConfigureAwait(false);
                return result.Entity;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public virtual async Task<List<TEntity>> UpdateAsync(List<TEntity> entities, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                _dbSet.UpdateRange(entities);
                await _uow.CommitAsync().ConfigureAwait(false);
                return entities;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public virtual async Task<TEntity> DeleteAsync(TEntity entity, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var result = _dbSet.Remove(entity);
                await _uow.CommitAsync().ConfigureAwait(false);
                return result.Entity;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public virtual async Task<List<TEntity>> DeleteAsync(List<TEntity> entities, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                _dbSet.RemoveRange(entities);
                await _uow.CommitAsync().ConfigureAwait(false);
                return entities;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public virtual async Task CommitAsync()
        {
            try
            {
                await _uow.CommitAsync().ConfigureAwait(false);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
    }
}
