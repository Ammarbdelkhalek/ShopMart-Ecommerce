using ShopMarket.Infrastrcuture.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShopMarket.Infrastrcuture.interfaces
{
    public interface IGenericInterface<T>  where T :class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task<T> GetByName(string name);
        void Update(T entity);
        void Delete(T entity);
        Task AddAsync(T entity);
        Task<T> FindAsync(Expression<Func<T, bool>> match);
        Task<IEnumerable<T>> GetAllEntityWithSpecs(ISpecification<T>spec);
        T GetEntityPredicate(Expression<Func<T, bool>> match, string[] include = null!);
        Task<IEnumerable<T>> GetAllPredicated(Expression<Func<T, bool>> match, string[] include = null!);
        Task<T> GetEntityWithSpec(ISpecification<T> spec);
        Task<int>GetCount(ISpecification<T> spec);
        Task<bool> Isvalid(int id);

    }
}
