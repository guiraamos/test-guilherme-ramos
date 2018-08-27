using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Web.Repository;

namespace web.Repository
{
    public class RepositoryBase<TEntity> : IDisposable, IRepositoryBase<TEntity> where TEntity : class
    {
        internal Context _context;
        internal DbSet<TEntity> _dbSet;

        public RepositoryBase(Context context)
        {
            this._context = context;
            this._dbSet = context.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }

        /// <summary> 
        /// Método que deleta um objeto no banco de dados da aplicação. 
        /// </summary> 
        public void Delete(TEntity entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            _dbSet.Remove(entity);
            _context.SaveChanges();
        }

        /// <summary> 
        /// Método que deleta um ou varios objetos no banco de dados da aplicação, mediante uma expressão LINQ. 
        /// </summary> 
        public void DeleteAll(Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> query = _dbSet;
            List<TEntity> listDelete = query.Where(filter).ToList();

            foreach (var item in listDelete)
            {
                _dbSet.Remove(item);
            }
            _context.SaveChanges();
        }

        /// <summary> 
        /// Método que adiciona uma lista de novos objetos ao banco de dados da aplicação. 
        /// </summary> 
        public void AddAll(List<TEntity> entity)
        {
            foreach (var item in entity)
            {
                _dbSet.Add(item);
            }
            _context.SaveChanges();
        }

        /// <summary> 
        /// Método que busca uma lista de objetos no banco de dados da aplicação e retorna-a no tipo IEnumerable<TEntity>. 
        /// </summary> 
        public virtual List<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public void Dispose()
        {
            _dbSet = null;
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public void Edit(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TEntity> Query()
        {
            return _dbSet.AsQueryable<TEntity>();
        }
    }
}
