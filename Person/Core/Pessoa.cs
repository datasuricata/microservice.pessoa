using Person.Core.Base;
using Person.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Person.Core {
    public class Pessoa : EntityBase {

        #region - attributes -

        public string Nome { get; private set; }
        public string Profissao { get; private set; }
        public string Telefone { get; private set; }

        public EstadoCivil EstadoCivil { get; private set; }
        public EtapaAtual Etapa { get; private set; }
        public TipoPessoa Tipo { get; private set; }
        public TipoSexo Sexo { get; private set; }

        public DateTime DataNascimento { get; private set; }

        public string UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public Empresa Empresa { get; set; }
        public Pessoa Conjuge { get; set; }

        public ICollection<Endereco> Enderecos { get; set; } = new List<Endereco>();
        public ICollection<Documento> Documentos { get; set; } = new List<Documento>();

        #endregion

        #region - ctor -

        // for entity, dont remove
        protected Pessoa() {
        }

        public Pessoa(string nome, string profissao, string telefone, EstadoCivil estadoCivil, EtapaAtual etapa, TipoPessoa tipo, TipoSexo sexo, DateTime dataNascimento) {
            Nome = nome;
            Profissao = profissao;
            Telefone = telefone;
            EstadoCivil = estadoCivil;
            Etapa = etapa;
            Tipo = tipo;
            Sexo = sexo;
            DataNascimento = dataNascimento;
        }

        #endregion

        #region - methods -

        public void AuditarEtapa() {
            if (Documentos.ToList().TrueForAll(a => a.Aprovado))
                DefinirEtapa(EtapaAtual.Aprovado);

            if (Documentos.ToList().TrueForAll(a => !a.Aprovado))
                DefinirEtapa(EtapaAtual.Recusado);
        }

        public void DefinirEtapa(EtapaAtual etapa) {
            Etapa = etapa;
        }

        public void AtualizarPessoa(string nome, string profissao, string telefone, EstadoCivil estadoCivil, TipoPessoa tipo, TipoSexo sexo, DateTime dataNascimento) {
            Nome = nome;
            Profissao = profissao;
            Telefone = telefone;
            EstadoCivil = estadoCivil;
            Tipo = tipo;
            Sexo = sexo;
            DataNascimento = dataNascimento;
        }

        public void AtualizarConjuge(string nome, string profissao, string telefone, EstadoCivil estadoCivil, TipoPessoa tipo, TipoSexo sexo, DateTime dataNascimento) {
            Nome = nome;
            Profissao = profissao;
            Telefone = telefone;
            EstadoCivil = estadoCivil;
            Tipo = tipo;
            Sexo = sexo;
            DataNascimento = dataNascimento;
        }

        #endregion
    }
}