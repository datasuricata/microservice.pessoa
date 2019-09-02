using Person.Arguments.Request;
using Person.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Person.Services.Core.Interfaces {
    public interface IServicePessoa {
        Task<Pessoa> PorId(string id);
        Task<Pessoa> PorDocumento(string param);

        Task<IEnumerable<Pessoa>> Like(string param);
        Task<IEnumerable<Pessoa>> Listar();
        Task<IEnumerable<Pessoa>> ListarPorIds(IEnumerable<string> Ids);

        Task RegistrarPessoa(PessoaRequest param);
        Task RegistrarConjuge(PessoaRequest param);

        Task AtualizarPessoa(PessoaRequest param);
        Task AtualizarConjuge(PessoaRequest param);
    }
}