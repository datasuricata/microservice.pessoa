using Person.Arguments.Request;
using Person.Core;
using Person.Core.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Person.Services.Core.Interfaces {
    public interface IServiceDocumento {
        Task<Documento> PorId(string id);

        Task<IEnumerable<Documento>> Listar();
        Task<IEnumerable<Documento>> ListarPorPessoa(string id);
        Task<IEnumerable<Documento>> ListarPorTipo(TipoDocumento tipo);

        Task Registrar(DocumentoRequest param);
        Task Atualizar(DocumentoRequest param);
        Task Cadeado(string id);
    }
}