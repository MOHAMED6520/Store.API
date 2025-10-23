using Store.API.Domain.Contracts;
using Store.API.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Store.API.Services.Specification
{
    public class BaseSpecification<Tkey, TEntity> : ISpecification<Tkey, TEntity> where TEntity : BaseEntity<Tkey>
    {
        public BaseSpecification(Expression<Func<TEntity, bool>>? _Criteria )
        {
            Criteria = _Criteria;
        }
        public List<Expression<Func<TEntity, object>>> Includes { get; set; } = new List<Expression<Func<TEntity, object>>>();
        public Expression<Func<TEntity, bool>>? Criteria { get; set; }
        public Expression<Func<TEntity, object>>? OrderByDescending { get ; set; }
        public Expression<Func<TEntity, object>>? OrderBy { get ; set; }

        public void AddOrderBy(Expression<Func<TEntity, object>> _Order)
        {
            OrderBy = _Order;
        }

        public void AddOrderByDescending(Expression<Func<TEntity, object>> _OrderDes)
        {
            OrderByDescending = _OrderDes;
        }
    }
}
