using GraduationProject.Data;
using System;
using System.Collections.Generic;
using System.Text;
using GraduationProject.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace GraduationProject.Infrastructure
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext _ctx;
        private DbSet<T> entities;
        public Repository(ApplicationDbContext ctx)
        {
            _ctx = ctx;
            entities = _ctx.Set<T>();
        }
        public int Delete(T entity)
        {
            entities.Remove(entity);
            return _ctx.SaveChanges();
        }

        public T Get(long id)
        {
            return entities.SingleOrDefault(e => e.Id == id);
        }

        public IQueryable<T> GetAll()
        {
            return entities.AsQueryable();
        }

        public T Insert(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");
            
            entities.Add(entity);
            _ctx.SaveChanges();
            return entity;
        }

        public T Update(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            _ctx.Update(entity);
            _ctx.SaveChanges();
            return entity;
        }
    }
}
