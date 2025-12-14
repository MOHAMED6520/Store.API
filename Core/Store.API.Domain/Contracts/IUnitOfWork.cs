using Store.API.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.API.Domain.Contracts
{
    public interface IUnitOfWork 
    {
        public IGenericRepository<Tkey, TEntity> GetRepository<Tkey, TEntity>() where TEntity:BaseEntity<Tkey>;

        public Task<int> SaveChangesAsynk();
    }
}
