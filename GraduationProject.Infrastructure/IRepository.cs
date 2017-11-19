using GraduationProject.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraduationProject.Infrastructure
{
    public interface IRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetAll();
        T Get(long id);
        T Insert(T entity);
        T Update(T entity);
        int Delete(T entity);
    }
}
