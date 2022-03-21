using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagement.Application.Contracts.Persistence
{
    public interface IGenericRepository<T> where T: class
    {
        Task<T> Get(int id);
        Task<IReadOnlyList<T>> GetAll();
        Task<bool> Exists(int id);
        Task<T> Add(T entity);

        //remove the <T> - dont have to return anything when we do an update
        Task Update(T entity);

        //remove the <T> - dont have to return anything when we do a delete
        Task Delete(T entity);
    }
}
