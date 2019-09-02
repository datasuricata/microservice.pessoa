using Person.Core;
using System;
using System.Linq.Expressions;

namespace Person.Infra.Persistence.Specs {
    public static class PessoaSpec {
        public static Expression<Func<Pessoa, bool>> PorUsuario(string id) {
            return x => x.UsuarioId == id;
        }
    }
}
