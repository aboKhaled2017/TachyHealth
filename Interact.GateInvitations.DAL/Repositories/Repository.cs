using Interact.GateInvitations.Core.Data;
using Interact.GateInvitations.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Interact.GateInvitations.DAL.Repositories
{
    public class Repository<T,TID> : IRepository<T,TID> where T : BaseEntity<TID> where TID:struct
    {
        private readonly AppDbContext context;
        private DbSet<T> entities;
        string errorMessage = string.Empty;
        public Repository(AppDbContext context)
        {
            this.context = context;
            entities = context.Set<T>();
        }
        public  Task<IEnumerable<T>> GetAllAsync()
        {
            return  Task.FromResult(entities.AsEnumerable());
        }
        public async Task<T> GetAsync(TID id)
        {
            return await entities.SingleOrDefaultAsync(s => s.Id.Equals(id));
        }
        public async Task InsertAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
           await context.SaveChangesAsync();
        }
        public async Task UpdateAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            await context.SaveChangesAsync();
        }
        public async Task DeleteAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            await context.SaveChangesAsync();
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> predicate)
        {
            return entities.Where(predicate);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate = null)
        {
            return (predicate is null)
                ?await entities.AnyAsync()
                :await entities.AnyAsync(predicate);
        }

        public IQueryable<T> All()
        {
            return entities;
        }
    }
}
