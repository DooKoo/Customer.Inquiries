using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Customer.Inquiries.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Customer.Inquiries.DataAccess.Base
{
    public class Repository : IRepository, IDisposable
    {
        protected readonly ApplicationDbContext context;

        public Repository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public virtual IQueryable<T> FindAll<T>(Expression<Func<T, bool>> where = null) where T : BaseEntity
        {
            return (null != where ? context.Set<T>().Where(where) : context.Set<T>());
        }

        public virtual T Find<T>(Expression<Func<T, bool>> where = null) where T : BaseEntity
        {
            return FindAll(where).FirstOrDefault();
        }

        public async Task<T> CreateAsync<T>(T entity) where T : BaseEntity
        {
            return (await context.Set<T>().AddAsync(entity)).Entity;
        }

        public void Update<T>(T entity) where T : BaseEntity
        {
            context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            entity.UpdatedDate = DateTimeOffset.UtcNow;
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
