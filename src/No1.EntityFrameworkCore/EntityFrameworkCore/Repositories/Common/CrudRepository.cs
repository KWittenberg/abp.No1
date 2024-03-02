using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using No1.Exceptions;
using No1.Interfaces.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.EntityFrameworkCore;

namespace No1.EntityFrameworkCore.Repositories.Common;

public abstract class CrudRepository<TEntity> : ICrudRepository<TEntity> where TEntity : Entity
{
    private readonly IDbContextProvider<No1DbContext> _dbContextProvider;

    protected CrudRepository(IDbContextProvider<No1DbContext> dbContextProvider)
    {
        _dbContextProvider = dbContextProvider;
    }

    public async Task<IList<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default)
    {
        var query = await GetQueryableAsync();

        query = WithDetails(query);

        if (predicate is not null)
        {
            query = query.Where(predicate);
        }

        var entities = await query.ToListAsync(cancellationToken);

        return entities;
    }

    public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        var query = await GetQueryableAsync();
        var entity = await WithDetails(query).FirstOrDefaultAsync(predicate, cancellationToken);

        return entity;
    }

    public async Task<int> GetCountAsync(CancellationToken cancellationToken = default)
    {
        var query = await GetQueryableAsync();
        var count = await query.AsNoTracking().CountAsync(cancellationToken);

        return count;
    }

    protected async Task<No1DbContext> GetContextAsync()
    {
        var context = await _dbContextProvider.GetDbContextAsync();

        return context;
    }

    protected async Task<DbSet<TEntity>> GetDbSetAsync()
    {
        var context = await GetContextAsync();

        return context.Set<TEntity>();
    }

    protected async Task<DbSet<T>> GetDbSetAsync<T>() where T : Entity
    {
        var context = await GetContextAsync();

        return context.Set<T>();
    }

    protected async Task<IQueryable<TEntity>> GetQueryableAsync()
    {
        var dbSet = await GetDbSetAsync();

        return dbSet.AsQueryable();
    }

    protected async Task<IQueryable<T>> GetQueryableAsync<T>() where T : Entity
    {
        var dbSet = await GetDbSetAsync<T>();

        return dbSet.AsQueryable();
    }

    protected abstract IQueryable<TEntity> WithDetails(IQueryable<TEntity> query);

    public async Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        try
        {
            var context = await GetContextAsync();
            var dbSet = context.Set<TEntity>();

            dbSet.Add(entity);

            await context.SaveChangesAsync(cancellationToken);

            return entity;
        }

        catch (SqlException ex)
        {
            switch (ex.Number)
            {
                // TODO: add potential inserting sql exceptions
                case 547:
                    throw new RelatedEntityException(typeof(TEntity));
            }

            throw;
        }
    }

    public async Task<IList<TEntity>> InsertManyAsync(IList<TEntity> entities, CancellationToken cancellationToken = default)
    {
        try
        {
            var context = await GetContextAsync();
            var dbSet = context.Set<TEntity>();

            dbSet.AddRange(entities);

            await context.SaveChangesAsync(cancellationToken);

            return entities;
        }

        catch (SqlException ex)
        {
            switch (ex.Number)
            {
                // TODO: add potential inserting sql exceptions
                case 547:
                    throw new RelatedEntityException(typeof(TEntity));
            }

            throw;
        }
    }

    public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        try
        {
            var context = await GetContextAsync();
            var dbSet = context.Set<TEntity>();

            dbSet.Update(entity);

            await context.SaveChangesAsync(cancellationToken);

            return entity;
        }

        catch (SqlException ex)
        {
            switch (ex.Number)
            {
                // TODO: add potential updating sql exceptions
                case 547:
                    throw new RelatedEntityException(typeof(TEntity));
            }

            throw;
        }
    }

    public async Task<IList<TEntity>> UpdateManyAsync(IList<TEntity> entities, CancellationToken cancellationToken = default)
    {
        try
        {
            var context = await GetContextAsync();
            var dbSet = context.Set<TEntity>();

            dbSet.UpdateRange(entities);

            await context.SaveChangesAsync(cancellationToken);

            return entities;
        }

        catch (SqlException ex)
        {
            switch (ex.Number)
            {
                // TODO: add potential updating sql exceptions
                case 547:
                    throw new RelatedEntityException(typeof(TEntity));
            }

            throw;
        }
    }

    public async Task<TEntity> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        try
        {
            var context = await GetContextAsync();
            var dbSet = context.Set<TEntity>();

            dbSet.Remove(entity);

            await context.SaveChangesAsync(cancellationToken);

            return entity;
        }

        catch (SqlException ex)
        {
            switch (ex.Number)
            {
                case 547:
                    throw new RelatedEntityException(typeof(TEntity));
            }

            throw;
        }
    }

    public async Task<IList<TEntity>> DeleteManyAsync(IList<TEntity> entities, CancellationToken cancellationToken = default)
    {
        try
        {
            var context = await GetContextAsync();
            var dbSet = context.Set<TEntity>();

            dbSet.RemoveRange(entities);

            await context.SaveChangesAsync(cancellationToken);

            return entities;
        }

        catch (SqlException ex)
        {
            switch (ex.Number)
            {
                case 547:
                    throw new RelatedEntityException(typeof(TEntity));
            }

            throw;
        }
    }
}