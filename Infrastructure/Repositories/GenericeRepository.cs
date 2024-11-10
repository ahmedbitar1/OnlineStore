using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class GenericeRepository<T> : IGenericRepository<T> where T : class
    {
        public readonly DBContextClass _context;

        public GenericeRepository(DBContextClass context)
        {
            this._context = context;
        }

        public async Task Add(T entity)
        {
           await _context.Set<T>().AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
          return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
          return  await _context.Set<T>().FindAsync(id);
        }

        public void Update(T entity)
        {
           _context.Set<T>().Update(entity);
        }
    }
}
