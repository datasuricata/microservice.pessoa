using Person.Arguments.Request;
using Person.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Person.Services.Core.Interfaces {
    public interface IServiceEmpresa {
        Task<Empresa> PorId(string id);
        Task<Empresa> PorPessoa(string id);
        Task<IEnumerable<Empresa>> Listar();
        Task Registrar(EmpresaRequest param);
        Task Atualizar(EmpresaRequest param);
    }
}
