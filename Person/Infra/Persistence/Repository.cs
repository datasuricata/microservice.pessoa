using Microsoft.EntityFrameworkCore;
using Person.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Person.Infra.Persistence {
    public class Repository<T> : IRepository<T> where T : EntityBase {

        #region - attributes -

        private readonly AppDbContext context;

        #endregion

        #region - ctor -

        public Repository(AppDbContext context) {
            this.context = context;
        }

        #endregion

        #region - methods -

        public virtual IQueryable<T> Queryable(bool readOnly, params Expression<Func<T, object>>[] includeProperties) {
            IQueryable<T> query = context.Set<T>();

            if (includeProperties.Any())
                query = Include(context.Set<T>(), includeProperties);

            if (readOnly)
                query.AsNoTracking();

            return query;
        }

        public virtual T Atualizar(T entity) {
            context.Set<T>().Attach(entity);
            context.Entry(entity);
            return entity;
        }

        public virtual bool Existe(Func<T, bool> where, params Expression<Func<T, object>>[] includeProperties) {
            if (includeProperties.Any())
                return Include(context.Set<T>(), includeProperties).Any(where);
            return context.Set<T>().Any(where);
        }

        #endregion

        #region - async -

        public virtual async Task<T> PorId(bool readOnly, string id, params Expression<Func<T, object>>[] includeProperties) {
            if (includeProperties.Any())
                return await Queryable(readOnly, includeProperties).FirstOrDefaultAsync(x => x.Id == id);
            return await context.Set<T>().FindAsync(id);
        }

        public virtual async Task<T> Por(bool readOnly, Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includeProperties) {
            return await Queryable(readOnly, includeProperties).FirstOrDefaultAsync(where);
        }

        public virtual async Task Registrar(T entity) {
            await context.Set<T>().AddAsync(entity);
        }

        public virtual async Task<IEnumerable<T>> Listar(bool readOnly, params Expression<Func<T, object>>[] includeProperties) {
            return await Queryable(readOnly, includeProperties).ToListAsync();
        }

        public virtual async Task<IEnumerable<T>> ListarPor(bool readOnly, Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includeProperties) {
            return await Queryable(readOnly, includeProperties).Where(where).ToListAsync();
        }

        #endregion

        #region - private -

        /// <summary>
        /// Realiza include populando o objeto passado por parametro
        /// </summary>
        /// <param name="query">Informe o objeto do tipo IQuerable</param>
        /// <param name="includeProperties">Ínforme o array de expressões que deseja incluir</param>
        /// <returns></returns>
        private IQueryable<T> Include(IQueryable<T> query, params Expression<Func<T, object>>[] includeProperties) {
            foreach (var property in includeProperties)
                query = query.Include(property);

            return query;
        }

        #endregion
    }
}
