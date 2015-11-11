using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace web_calendar.DAL.Interface
{
    public interface IGenericRepository<T> where T : class
    {
        T FindById(int id);

        M FindOtherById<M>(int id) where M : class;

        IEnumerable<T> GetAll();

        void Add(T instance);

        void AddOther<M>(M entity) where M : class;

        void Modify(T instance);

        void Delete(T instance);

        T FindFirst(Func<T, bool> filter);

        IEnumerable<T> FindAll(Func<T, bool> filter);

        void SaveChanges();
    }
}
