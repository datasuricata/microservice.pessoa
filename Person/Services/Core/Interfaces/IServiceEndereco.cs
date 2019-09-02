using Person.Arguments.Request;
using Person.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Person.Services.Core.Interfaces {
    public interface IServiceEndereco {

        Task<Endereco> PorId(string id);
        Task<Endereco> PorPessoa(string id);
        Task<Endereco> PorCEP(string param);

        Task<IEnumerable<Endereco>> Listar();
        Task<IEnumerable<Endereco>> ListarPorPessoa(string id);

        Task RegistrarEndereco(EnderecoRequest param);
        Task RegistrarCidade(CidadeRequest param);
        Task RegistrarEstado(EstadoRequest param);

        Task AtualizarEndereco(EnderecoRequest param);
        Task AtualizarCidade(CidadeRequest param);
        Task AtualizarEstado(EstadoRequest param);
    }
}