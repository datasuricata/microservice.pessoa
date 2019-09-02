using Person.Arguments.Request;
using Person.Core;
using Person.Infra.Persistence;
using Person.Infra.Persistence.Specs;
using Person.Services.Base;
using Person.Services.Core.Interfaces;
using Person.Services.Validators.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Person.Services.Core {
    public class ServiceEndereco : ServiceBase, IServiceEndereco {

        private readonly IRepository<Pessoa> _repoPessoa;
        private readonly IRepository<Estado> _repoEstado;
        private readonly IRepository<Cidade> _repoCidade;
        private readonly IRepository<Endereco> _repoEndereco;

        public ServiceEndereco(IServiceProvider provider,
            IRepository<Cidade> repoCidade, 
            IRepository<Estado> repoEstado, 
            IRepository<Pessoa> repoPessoa, 
            IRepository<Endereco> repoEndereco) : base(provider) {
            _repoPessoa = repoPessoa;
            _repoEstado = repoEstado;
            _repoCidade = repoCidade;
            _repoEndereco = repoEndereco;
        }

        public async Task<Endereco> PorId(string id) {
            return await _repoEndereco.PorId(false, id, 
                i => i.Cidade, 
                i => i.Estado);
        }

        public async Task<Endereco> PorPessoa(string id) {
            return await _repoEndereco.Por(true, x => x.PessoaId == id,
                i => i.Cidade,
                i => i.Estado);
        }

        public async Task<Endereco> PorCEP(string param) {
            return await _repoEndereco.Por(true, x => x.CEP == param);
        }

        public async Task<IEnumerable<Endereco>> Listar() {
            return await _repoEndereco.Listar(true,
                i => i.Cidade,
                i => i.Estado);
        }

        public async Task<IEnumerable<Endereco>> ListarPorPessoa(string id) {
            return await _repoEndereco.Listar(true, x => x.PessoaId == id,
                i => i.Cidade,
                i => i.Estado);
        }


        public async Task RegistrarEndereco(EnderecoRequest param) {

            var id = param.UsuarioId ?? param.ProprietarioId;
            var endereco = new Endereco(param.Tipo, param.Principal, param.Numero,
                param.Complemento, param.Logradouro, param.Bairro, param.CEP) {
                Pessoa = await _repoPessoa.Por(false, PessoaSpec.PorUsuario(id)),
                Cidade = await _repoCidade.PorId(false, param.CidadeId),
                Estado = await _repoEstado.PorId(false, param.EstadoId),
            };

            _notify.Validate(endereco, new EnderecoValidator());
            await _repoEndereco.Registrar(endereco);
        }

        public async Task RegistrarCidade(CidadeRequest param) {
            var estado = await _repoEstado.PorId(false, param.EstadoId);
            var cidade = new Cidade(param.Nome, param.Capital) {
                Estado = estado,
            };

            #region - validator -

            _notify.When<ServiceEndereco>(string.IsNullOrEmpty(cidade.Nome),
                "Nome é obrigatório.");

            _notify.When<ServiceEndereco>(estado == null,
                "Estado não localizado.");

            #endregion

            await _repoCidade.Registrar(cidade);
        }

        public async Task RegistrarEstado(EstadoRequest param) {
            var estado = new Estado(param.Nome, param.Sigla);

            #region - validator -

            _notify.When<ServiceEndereco>(string.IsNullOrEmpty(estado.Nome),
                "Nome é obrigatório.");

            _notify.When<ServiceEndereco>(string.IsNullOrEmpty(estado.Sigla),
                "Sigla é obrigatória.");

            #endregion

            await _repoEstado.Registrar(estado);
        }


        public async Task AtualizarEndereco(EnderecoRequest param) {
            var endereco = await _repoEndereco.PorId(false, param.Id);

            endereco.Atualizar(param.Tipo, param.Principal, param.Numero, 
                param.Complemento, param.Logradouro, param.Bairro, param.CEP);

            endereco.Cidade = await _repoCidade.PorId(false, param.CidadeId);
            endereco.Estado = await _repoEstado.PorId(false, param.EstadoId);

            _notify.Validate(endereco, new EnderecoValidator());
            _repoEndereco.Atualizar(endereco);
        }

        public async Task AtualizarCidade(CidadeRequest param) {
            var cidade = await _repoCidade.PorId(false, param.Id);

            cidade.Atualizar(param.Nome, param.Capital);

            #region - validator -

            _notify.When<ServiceEndereco>(string.IsNullOrEmpty(cidade.Nome),
                "Nome é obrigatório.");

            #endregion

            _repoCidade.Atualizar(cidade);
        }

        public async Task AtualizarEstado(EstadoRequest param) {

            var estado = await _repoEstado.PorId(false, param.Id);
            estado.Atualizar(param.Nome, param.Sigla);

            #region - validator -

            _notify.When<ServiceEndereco>(string.IsNullOrEmpty(estado.Nome),
                "Nome é obrigatório.");

            _notify.When<ServiceEndereco>(string.IsNullOrEmpty(estado.Sigla),
                "Sigla é obrigatória.");

            #endregion

            await _repoEstado.Registrar(estado);
        }
    }
}