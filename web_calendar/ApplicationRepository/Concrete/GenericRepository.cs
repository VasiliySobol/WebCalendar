using ApplicationRepository.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationRepository.Concrete
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

        public virtual IEnumerable<T> GetAll()
        {
            return _entities.Set<T>().ToList();
        }

        public virtual void Modify(T instance)
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

        public virtual void Delete(T entity)
        {
            _entities.Set<T>().Remove(entity);
        }

        public virtual void SaveChanges()
        {
            _entities.SaveChanges();
        }
    }
}
