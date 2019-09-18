using Microsoft.EntityFrameworkCore;
using Person.Arguments.Request;
using Person.Core;
using Person.Core.Enums;
using Person.Infra.Persistence;
using Person.Infra.Persistence.Specs;
using Person.Services.Base;
using Person.Services.Core.Interfaces;
using Person.Services.Helpers;
using Person.Services.Validators.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Person.Services.Core {
    public class ServicePessoa : ServiceBase, IServicePessoa {

        private readonly IRepository<Pessoa> _repoPessoa;

        public ServicePessoa(IServiceProvider provider,
            IRepository<Pessoa> repoPessoa,
            IRepository<Empresa> repoEmpresa) : base(provider) {
            _repoPessoa = repoPessoa;
        }

        public async Task<Pessoa> PorId(string id) {
            return await _repoPessoa.PorId(true, id,
                i => i.Usuario,
                i => i.Empresa,
                i => i.Conjuge,
                i => i.Enderecos,
                i => i.Documentos);
        }

        public async Task<Pessoa> PorDocumento(string param) {
            return await _repoPessoa.Por(true, x => x.Documentos.Any(a => a.Dados == param.CleanFormat()),
                i => i.Usuario,
                i => i.Empresa,
                i => i.Conjuge,
                i => i.Enderecos,
                i => i.Documentos);
        }

        public async Task<IEnumerable<Pessoa>> Like(string param) {
            return await _repoPessoa.Queryable(true,
                i => i.Usuario,
                i => i.Empresa,
                i => i.Conjuge,
                i => i.Enderecos,
                i => i.Documentos)
                .Where(x => EF.Functions.Like(x.Nome, $"%{param}%") || EF.Functions.Like(x.Profissao, $"%{param}%"))
                .ToListAsync();
        }

        public async Task<IEnumerable<Pessoa>> ListarPorIds(IEnumerable<string> Ids) {
            return await _repoPessoa.ListarPor(true, x => Ids.Any(a => a == x.Id));
        }

        public async Task<IEnumerable<Pessoa>> Listar() {
            return await _repoPessoa.Listar(true);
        }

        public async Task RegistrarPessoa(PessoaRequest param) {

            var id = param.UsuarioId ?? param.ProprietarioId;

            var pessoa = new Pessoa(param.Nome, param.Profissao,
                param.Telefone, param.EstadoCivil, EtapaAtual.Pendente,
                param.Tipo, param.Sexo, param.DataNascimento) {
                UsuarioId = id
            };

            _notify.Validate(pessoa, new PessoaValidator());
            await _repoPessoa.Registrar(pessoa);
        }

        public async Task RegistrarConjuge(PessoaRequest param) {

            var id = param.UsuarioId ?? param.ProprietarioId;
            var pessoa = await _repoPessoa.Por(false, PessoaSpec.PorUsuario(id));

            pessoa.Conjuge = new Pessoa(param.Nome, param.Profissao,
                param.Telefone, param.EstadoCivil, EtapaAtual.Aprovado,
                param.Tipo, param.Sexo, param.DataNascimento);

            // todo validate ...
            _repoPessoa.Atualizar(pessoa);
        }

        public async Task AtualizarPessoa(PessoaRequest param) {

            var id = param.UsuarioId ?? param.ProprietarioId;
            var pessoa = await _repoPessoa.Por(false, x => x.Id == id || x.UsuarioId == id,
                i => i.Usuario);

            pessoa.AtualizarPessoa(param.Nome, param.Profissao, param.Telefone,
                param.EstadoCivil, param.Tipo, param.Sexo, param.DataNascimento);

            pessoa.AuditarEtapa();

            // todo validate ...
            _repoPessoa.Atualizar(pessoa);
        }

        public async Task AtualizarConjuge(PessoaRequest param) {

            var id = param.UsuarioId ?? param.ProprietarioId;
            var pessoa = await _repoPessoa.Por(false, x => x.Id == id || x.UsuarioId == id,
                i => i.Usuario);

            pessoa.Conjuge.AtualizarConjuge(param.Nome, param.Profissao, param.Telefone,
                param.EstadoCivil, param.Tipo, param.Sexo, param.DataNascimento);

            // todo validate ...
            _repoPessoa.Atualizar(pessoa);
        }

        public async Task TrocarEtapa(string id, EtapaAtual etapa) {
            var pessoa = await _repoPessoa.Por(false, x => x.Id == id);
            pessoa.DefinirEtapa(etapa);
            _repoPessoa.Atualizar(pessoa);
        }
    }
}