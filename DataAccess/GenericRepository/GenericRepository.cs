using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.GenericRepository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DataContext db;
        public GenericRepository(DataContext _db)
        {
                db = _db;
        }
        public bool Delete(T obj)
        {
            db.Set<T>().Remove(obj);
            return true;
        }

        public IQueryable<T> GetAll()
        {
            IQueryable<T> list=db.Set<T>();
            return list;
        }

        public T GetById(Guid Id)
        {
            return db.Set<T>().Find(Id);
        }

        public T Insert(T obj)
        {
            db.Set<T>().Add(obj);
            return obj;
        }

        public T Update(T obj)
        {
            db.Entry(obj).State = EntityState.Modified;
            return obj;
        }
    }
}
