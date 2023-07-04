using Microsoft.EntityFrameworkCore;
using CRVS.Core.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRVS.EF.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private ApplicationDbContext _context;
        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }
        public T GetById(int id)
        {
            return _context.Set<T>().Find(id)!;
        }
        public async Task<T> GetByIdAsync(int id)
        {
            return (await _context.Set<T>().FindAsync(id))!;
        }
        public void Add(T model)
        {
            _context.Set<T>().Add(model);
            _context.SaveChanges();
        }
        public async Task AddAsync(T model)
        {
            if (model == null)
            {
                BadRequest("");
            }
            _context.Set<T>().Add(model!);
            await _context.SaveChangesAsync();
        }
        private void BadRequest(string V)
        {
            throw new NotImplementedException();
        }
        public void Update(T model)
        {
            _context.Set<T>().Update(model);
            _context.SaveChanges();
        }
        public async Task UpdateAsync(T model)
        {
            _context.Set<T>().Update(model);
            await _context.SaveChangesAsync();
        }
        public void Delete(int id)
        {
            var entity = _context.Set<T>().Find(id);
            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
                _context.SaveChanges();
            }
        }
        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
