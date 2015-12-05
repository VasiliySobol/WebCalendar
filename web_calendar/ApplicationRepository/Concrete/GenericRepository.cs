using web_calendar.DAL.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Data.Entity.Infrastructure;
using System.Web;

namespace web_calendar.DAL.Concrete
{
    public abstract class GenericRepository<C, T> : IGenericRepository<T>
        where T : class
        where C : DbContext, new()
    {

        private C _entities = new C();
        public C Context
        {

            get { return _entities; }
            set { _entities = value; }
        }

        public T FindById(int id)
        {
            return _entities.Set<T>().Find(id);
        }

        public M FindOtherById<M>(int id) where M: class
        {
            return _entities.Set<M>().Find(id);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _entities.Set<T>().ToList();
        }

        public virtual void Modify(T instance)
        {
            _entities.Entry(instance).State = EntityState.Modified;
        }
        public virtual void Modify<M>(M instance) where M : class
        {
            _entities.Entry(instance).State = EntityState.Modified;
        }

        public virtual T FindFirst(Func<T, bool> filter)
        {
            return _entities.Set<T>().Where(filter).FirstOrDefault();
        }

        public virtual IEnumerable<T> FindAll(Func<T, bool> filter)
        {
            return _entities.Set<T>().Where(filter);
        }

        public virtual void Add(T entity)
        {
            _entities.Set<T>().Add(entity);
        }

        public virtual void AddOther<M>(M entity) where M : class
        {
            _entities.Set<M>().Add(entity);
        }

        public virtual void Delete(T entity)
        {
            _entities.Set<T>().Remove(entity);
        }

        public void Delete<M>(M instance) where M : class
        {
            _entities.Set<M>().Remove(instance);
        }

        public virtual void SaveChanges()
        {
            _entities.SaveChanges();
        }
    }
}
