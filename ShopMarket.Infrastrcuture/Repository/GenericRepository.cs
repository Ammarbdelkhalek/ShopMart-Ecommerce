using Microsoft.EntityFrameworkCore;
using ShopMarket.Core.Data;
using ShopMarket.Infrastrcuture.interfaces;
using ShopMarket.Infrastrcuture.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShopMarket.Infrastrcuture.Repository
{
    public class GenericRepository<T>  : IGenericInterface<T> where T : class
    {
        private  readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;
        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>(); 
        }

        public async Task<IEnumerable<T>> GetAll()=>await _dbSet.ToListAsync();
        public async Task AddAsync(T entity)=>await _dbSet.AddAsync(entity);
        public async  void Update(T entity)=> _dbSet.Update(entity);    
        public async Task<T> FindAsync(Expression<Func<T, bool>> match)=>await _dbSet.FindAsync(match);
        public async Task<IEnumerable<T>> GetAllEntityWithSpecs(ISpecification<T> spec) => await Apply(spec).ToListAsync();
        public void Delete(T Entity) => _dbSet.Remove(Entity);
        public async Task<T> GetById(int id) => await _dbSet.FindAsync(id);
        public async Task<T> GetByName(string name)=> await _dbSet.FindAsync(name);
        public T GetEntityPredicate(Expression<Func<T, bool>> match, string[] include = null)
        {
            IQueryable<T> quary = _context.Set<T>();
            if (quary == null)
            {
                return null;
            }
            if (include != null)
            {
                foreach (var item in include)
                {
                    quary.Include(item);

                }
            }
            return  quary.FirstOrDefault();
        }
        public  async Task<IEnumerable<T>> GetAllPredicated(Expression<Func<T, bool>> match, string[] include = null)
        {
            IQueryable<T> quary = _context.Set<T>();
            if (quary == null)
            {
                return null;
            }
            if (include != null)
            {
                foreach (var item in include)
                {
                    quary.Include(item);

                }
            }
            return await quary.Where(match).ToListAsync();  
        }
        public async Task<T> GetEntityWithSpec(ISpecification<T> spec)=> await Apply(spec).FirstOrDefaultAsync();
        public async Task<int> GetCount(ISpecification<T> spec) => await Apply(spec).CountAsync();
        public async Task<bool> Isvalid(int id)=> throw new NotImplementedException();
            //await context.Set<T>().AnyAsync(c => c.Id == id);
        private IQueryable<T> Apply(ISpecification<T> specification)
           => EvaluationSpecification<T>.GetQuery(_context.Set<T>().AsQueryable(), specification);
    }
}
