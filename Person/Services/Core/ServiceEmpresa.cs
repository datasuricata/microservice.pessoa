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
    public class ServiceEmpresa : ServiceBase, IServiceEmpresa {

        private readonly IRepository<Pessoa> _repoPessoa;
        private readonly IRepository<Empresa> _repoEmpresa;

        public ServiceEmpresa(IServiceProvider provider,
            IRepository<Pessoa> repoPessoa, 
            IRepository<Empresa> repoEmpresa) : base(provider) {
            _repoPessoa = repoPessoa;
            _repoEmpresa = repoEmpresa;
        }

        
        public async Task<Empresa> PorId(string id) {
            return await _repoEmpresa.PorId(true, id, i => i.Pessoa);
        }

        public async Task<Empresa> PorPessoa(string id) {
            return await _repoEmpresa.Por(true, x => x.PessoaId == id, i => i.Pessoa);
        }

        public async Task<IEnumerable<Empresa>> Listar() {
            return await _repoEmpresa.Listar(true);
        }

        public async Task Registrar(EmpresaRequest param) {

            var id = param.UsuarioId ?? param.ProprietarioId;
            var empresa = new Empresa(param.Nome, param.InscricaoEstadual, param.Telefone,
                param.Celular, param.Email, param.Responsavel, param.CPFResponsavel) {
                Pessoa = await _repoPessoa.Por(true, PessoaSpec.PorUsuario(id)),
            };

            _notify.Validate(empresa, new EmpresaValidator());
            await _repoEmpresa.Registrar(empresa);
        }

        public async Task Atualizar(EmpresaRequest param) {
            var empresa = await _repoEmpresa.PorId(false, param.Id, i => i.Pessoa);

            empresa.Atualizar(param.Nome, param.InscricaoEstadual, param.Telefone, 
                param.Celular, param.Email, param.Responsavel, param.CPFResponsavel);

            _notify.Validate(empresa, new EmpresaValidator());
            _repoEmpresa.Atualizar(empresa);
        }
    }
}