using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Person.Infra.Persistence {
    public interface IRepository<T> where T : class {

        bool Existe(Func<T, bool> where, params Expression<Func<T, object>>[] includes);

        IQueryable<T> Queryable(bool leitura, params Expression<Func<T, object>>[] includes);
        T Atualizar(T entity);

        Task<T> PorId(bool leitura, string id, params Expression<Func<T, object>>[] includes);
        Task<T> Por(bool leitura, Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includes);

        Task Registrar(T entity);

        Task<IEnumerable<T>> Listar(bool leitura, params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>> ListarPor(bool leitura, Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includes);
    }
}
