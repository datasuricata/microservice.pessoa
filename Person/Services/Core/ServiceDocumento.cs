using Person.Arguments.Request;
using Person.Core;
using Person.Core.Enums;
using Person.Infra.Persistence;
using Person.Infra.Persistence.Specs;
using Person.Services.Base;
using Person.Services.Core.Interfaces;
using Person.Services.Validators.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Person.Services.Core {
    public class ServiceDocumento : ServiceBase, IServiceDocumento {

        private readonly IRepository<Pessoa> _repoPessoa;
        private readonly IRepository<Documento> _repoDocumento;

        public ServiceDocumento(IServiceProvider provider,
            IRepository<Pessoa> repoPessoa,
            IRepository<Documento> repoDocumento) : base(provider) {
            _repoPessoa = repoPessoa;
            _repoDocumento = repoDocumento;
        }

        public async Task<Documento> PorId(string id) {
            return await _repoDocumento.PorId(true, id, i => i.Pessoa);
        }

        public async Task<IEnumerable<Documento>> ListarPorPessoa(string id) {
            return await _repoDocumento.ListarPor(true, x => x.PessoaId == id, i => i.Pessoa);
        }

        public async Task<IEnumerable<Documento>> Listar() {
            return await _repoDocumento.Listar(true);
        }

        public async Task<IEnumerable<Documento>> ListarPorTipo(TipoDocumento tipo) {
            return await _repoDocumento.ListarPor(true, x => x.Tipo == tipo);
        }

        public async Task Registrar(DocumentoRequest param) {

            var id = param.UsuarioId ?? param.ProprietarioId;
            var documento = new Documento(param.Dados, param.ImagemUri, param.Tipo) {
                Pessoa = await _repoPessoa.Por(true, PessoaSpec.PorUsuario(id)),
            };

            _notify.Validate(documento, new DocumentoValidator());
            await _repoDocumento.Registrar(documento);
        }

        public async Task Atualizar(DocumentoRequest param) {
            var documento = await _repoDocumento.PorId(false, param.Id, i => i.Pessoa);
            documento.Atualizar(param.Dados, param.ImagemUri, param.Tipo);

            _notify.Validate(documento, new DocumentoValidator());
            _repoDocumento.Atualizar(documento);
        }

        public async Task Cadeado(string id) {
            var documento = await _repoDocumento.PorId(false, id);
            documento.Cadeado();
            _repoDocumento.Atualizar(documento);
        }
    }
}