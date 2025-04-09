using ShopMarket.Core.Data;
using ShopMarket.Infrastrcuture.interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMarket.Infrastrcuture.Repository
{
    public class UOW  : IUOW
    {
        private readonly ApplicationDbContext _context;
        private Hashtable _repository;
        public UOW(ApplicationDbContext context)
        {
            _context = context;
            
        }
        public IGenericInterface<T> GenericRepository<T>() where T : class
        {
             if(_repository == null)
            {
                _repository = new Hashtable();
            }
             var name = typeof(T).Name;
            if(_repository.ContainsKey(name))
            {
                var repoInstance = new GenericRepository<T>(_context);
                _repository[name] = repoInstance;
            }
            return (IGenericInterface<T>)_repository[name];
        }
        public async Task<int> Complete()
        {
             return await _context.SaveChangesAsync();
        }

        public  void Dispose()
        {
              _context.Dispose();
        }

        
    }
}
