using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TournaBet.Shared.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly DbContext _dbContext;
    private readonly Dictionary<Type, object> _repositories;

    public UnitOfWork(DbContext dbContext)
    {
        _dbContext = dbContext;
        _repositories = new Dictionary<Type, object>();
    }

    public IBaseRepository<T> GetRepository<T>() where T : BaseEntity
    {
        if (_repositories.ContainsKey(typeof(T)))
        {
            return (IBaseRepository<T>)_repositories[typeof(T)];
        }

        var repository = new BaseRepository<T>(_dbContext);
        _repositories.Add(typeof(T), repository);
        return repository;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _dbContext.SaveChangesAsync();
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }
}