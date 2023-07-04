using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRVS.Core.IRepositories
{
    public interface IBaseRepository<T> where T : class
    {
        T GetById(int id);
        Task<T> GetByIdAsync(int id);
        IEnumerable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync();
        void Add(T model);
        Task AddAsync(T model);
        void Update(T model);
        Task UpdateAsync(T model);
        void Delete(int id);
        Task DeleteAsync(int id);
    }
}
