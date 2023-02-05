using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.GenericRepository
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        T GetById(Guid Id);
        T Insert(T obj);
        T Update(T obj);
        bool Delete(T obj);
    }
}
