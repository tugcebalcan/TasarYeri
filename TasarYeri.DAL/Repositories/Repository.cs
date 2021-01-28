using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Microsoft.EntityFrameworkCore;
using TasarYeri.DAL.Contexts;
using TasarYeri.DAL.Entities;

namespace TasarYeri.DAL.Repositories
{
    public class Repository<T> where T : class
    {
        private readonly MyContext context;
        private DbSet<T> entities;
        // private Mode<T> Models;

        public Repository(MyContext _context)
        {
            this.context = _context;
            entities = _context.Set<T>();
        }

        public T GetBy(Expression<Func<T, bool>> expression)
        {
            return entities.FirstOrDefault(expression);
        }

        public IQueryable<T> GetAll()
        {
            return entities;
        }
        public IQueryable<T> GetAll(Expression<Func<T, bool>> expression)
        {
            return entities.Where(expression);
        }

        public void Add(T entity)
        {
            context.Add(entity);
            context.SaveChanges();
        }

        public void Remove(T entity)
        {
            context.Remove(entity);
            context.SaveChanges();
        }

        public void Update(T entity)
        {
            context.Update(entity);
            context.SaveChanges();
        }
        //public void Bul(int id)
        //{
        //    entities.Find(id);
        //}

        public void Save()
        {
            context.SaveChanges();
        }
        public T Bul(int id)
        {
            return entities.Find(id);
        }

        public IEnumerable<T> GetAllLazy(Expression<Func<T, bool>> expression,
            string includeProperties = null)

        {
            IQueryable<T> query = entities.Where(expression);
            if (includeProperties != null)
            {
                foreach (var item in includeProperties.Split(new char[]
                         { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(item);
                }
            }
            return query.ToList();
        }

        public IEnumerable<T> GetAllLazy(string includeProperties = null)

        {
            IQueryable<T> query = entities;
            if (includeProperties != null)
            {
                foreach (var item in includeProperties.Split(new char[]
                         { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(item);
                }
            }
            return query.ToList();
        }

        public IQueryable<T> GetInclude(Expression<Func<T, bool>> expression)
        {
            return entities.Include(expression);
        }
        public void RemoveRange(IEnumerable<T> entity)
        {
            context.RemoveRange(entity);
        }

    }



    
}
