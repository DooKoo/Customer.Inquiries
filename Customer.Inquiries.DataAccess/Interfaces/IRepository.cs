using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Customer.Inquiries.DataAccess.Base;

namespace Customer.Inquiries.DataAccess.Interfaces
{
    public interface IRepository 
    {
        IQueryable<T> FindAll<T>(Expression<Func<T, bool>> where = null) where T : BaseEntity;
        T Find<T>(Expression<Func<T, bool>> where = null) where T : BaseEntity;
        void Update<T>(T entity) where T : BaseEntity;
        Task<T> CreateAsync<T>(T entity) where T : BaseEntity;
        Task SaveChangesAsync();
    }
}
