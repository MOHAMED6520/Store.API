using Store.API.Domain.Contracts;
using Store.API.Domain.Entities;
using Store.API.Persistence.Data.Contexts;
using Store.API.Persistence.Repositories;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.API.Persistence
{
    public class UnitOfWork (StoreDbContext _context) : IUnitOfWork 
    {
        #region IGenericRepository<TKey, TEntity> using Dictionary

        //Dictionary<string, object> _Repository = new Dictionary<string, object>() ;

        //Generate generic object of IGenericRepository<TKey, TEntity> if it dosnt exist 
        //if it exist return it
        //public IGenericRepository<Tkey, TEntity> GetRepository<Tkey, TEntity>() where TEntity : BaseEntity<Tkey>
        //{
        //    var type = typeof(TEntity).Name;
        //    if (!_Repository.ContainsKey(type))
        //    {
        //        _Repository.Add(type, new GenericRepository<Tkey, TEntity>(_context));
        //        return(IGenericRepository < Tkey, TEntity >) _Repository[type];
        //    }
        //    return (IGenericRepository<Tkey, TEntity>)_Repository[type];
        //} 
        #endregion


        ConcurrentDictionary<string, object> _Repository = new ConcurrentDictionary<string, object>();
        public IGenericRepository<Tkey, TEntity> GetRepository<Tkey, TEntity>() where TEntity : BaseEntity<Tkey>
        {
            var type = typeof(TEntity).Name;
            return (IGenericRepository < Tkey, TEntity >) _Repository.GetOrAdd(type, new GenericRepository<Tkey, TEntity>(_context));
        }

        public async Task<int> SaveChangesAsynk()
        {
           return await _context.SaveChangesAsync();
        }
    }
}
