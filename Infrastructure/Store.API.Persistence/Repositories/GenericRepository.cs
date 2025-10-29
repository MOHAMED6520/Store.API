using Microsoft.EntityFrameworkCore;
using Store.API.Domain.Contracts;
using Store.API.Domain.Entities;
using Store.API.Domain.Entities.Products;
using Store.API.Persistence.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.API.Persistence.Repositories
{
    public class GenericRepository<Tkey, TEntity>(StoreDbContext _context) : IGenericRepository<Tkey, TEntity> where TEntity : BaseEntity<Tkey>
    {

        public async Task<IEnumerable<TEntity>> GetAllAsync(bool changeTracker)
        {
            if (typeof(TEntity) == typeof(Product))
            {
                return changeTracker ?
                     await _context.Products.Include(P => P.Brand).Include(P => P.Type).ToListAsync() as IEnumerable<TEntity> :
                   await _context.Products.Include(P => P.Brand).Include(P => P.Type).AsNoTracking().ToListAsync() as IEnumerable<TEntity>;
                
            }
            else
            {
                return changeTracker ? 
                    await _context.Set<TEntity>().ToListAsync() :
                     await _context.Set<TEntity>().AsNoTracking().ToListAsync();
            }
        }

        public async Task<TEntity?> GetAsync(Tkey key)
        {
            if (typeof(TEntity) == typeof(Product))
            {
                return await _context.Products.Include(P => P.Brand).Include(P => P.Type).FirstOrDefaultAsync(P=>P.Id==key as int?) as TEntity;
            }
                return await _context.Set<TEntity>().FindAsync(key);
        }

        public async Task AddAsynk(TEntity entity)
        {
           await _context.Set<TEntity>().AddAsync(entity);
        }

        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }

        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecification<Tkey, TEntity> spec, bool changeTracker = false)
        {
           return await ApplySpecifications(spec).ToListAsync();
        }

        public async Task<TEntity?> GetAsync(ISpecification<Tkey, TEntity> spec)
        {
          return await ApplySpecifications(spec).FirstOrDefaultAsync();
        }
     
        public async Task<IEnumerable<TEntity>> CountAsync(ISpecification<Tkey, TEntity> spec, bool changeTracker = false)
        {
            return await ApplySpecifications(spec).ToListAsync();
        }
        private IQueryable<TEntity> ApplySpecifications(ISpecification<Tkey, TEntity> spec)
        {
            return SpecificationsEvaluator.GetQuery(_context.Set<TEntity>(), spec);
        }

    }
}
