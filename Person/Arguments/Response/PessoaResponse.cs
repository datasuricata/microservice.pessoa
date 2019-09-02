using Person.Core;
using Person.Services.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace Person.Arguments.Response {
    public class PessoaResponse {

        public string Id { get; set; }
        public string Nome { get; set; }
        public string Profissao { get; set; }
        public string Telefone { get; set; }

        public string EstadoCivil { get; set; }
        public string Etapa { get; set; }
        public string Tipo { get; set; }
        public string Sexo { get; set; }

        public string DataNascimento { get; set; }

        public string UsuarioId { get; set; }
        public UsuarioResponse Usuario { get; set; }

        public EmpresaResponse Empresa { get; set; }
        public ConjugeResponse Conjuge { get; set; }

        public List<EnderecoResponse> Enderecos { get; set; } = new List<EnderecoResponse>();
        public List<DocumentoResponse> Documentos { get; set; } = new List<DocumentoResponse>();


        public static explicit operator PessoaResponse(Pessoa v) {
            return v == null ? null : new PessoaResponse {
                Id = v.Id,
                UsuarioId = v.UsuarioId,
                Nome = v.Nome,
                Profissao = v.Profissao,
                Telefone = v.Telefone,
                EstadoCivil = v.EstadoCivil.EnumDisplay(),
                Etapa = v.EstadoCivil.EnumDisplay(),
                Tipo = v.Tipo.EnumDisplay(),
                Sexo = v.Sexo.EnumDisplay(),
                DataNascimento = v.DataNascimento.ToString("dd/MM/yyyy"),
                Usuario = (UsuarioResponse)v.Usuario,
                Conjuge = (ConjugeResponse)v.Conjuge,
                Empresa = (EmpresaResponse)v.Empresa,
                Documentos = v.Documentos.ToList().ConvertAll(c => (DocumentoResponse)c),
                Enderecos = v.Enderecos.ToList().ConvertAll(c => (EnderecoResponse)c),
            };
        }
    }
}