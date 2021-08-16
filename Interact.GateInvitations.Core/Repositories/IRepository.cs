using Interact.GateInvitations.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Interact.GateInvitations.Core.Repositories
{
    public interface IRepository<T,TID> where T: BaseEntity<TID> where TID:struct
    {
        IQueryable<T> All();
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetAsync(TID id);
        Task InsertAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        IQueryable<T> Where(Expression<Func<T, bool>> predicate);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate = null);
    }
}
