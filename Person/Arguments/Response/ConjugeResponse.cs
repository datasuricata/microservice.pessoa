using Person.Core;
using Person.Services.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace Person.Arguments.Response {
    public class ConjugeResponse {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Profissao { get; set; }
        public string Telefone { get; set; }

        public string EstadoCivil { get; set; }
        public string Tipo { get; set; }
        public string Sexo { get; set; }

        public string DataNascimento { get; set; }

        public List<DocumentoResponse> Documentos { get; set; } = new List<DocumentoResponse>();

        #region - casting -

        public static explicit operator ConjugeResponse(Pessoa v) {
            return v == null ? null : new ConjugeResponse {
                Id = v.Id,
                Nome = v.Nome,
                Profissao = v.Profissao,
                Telefone = v.Telefone,
                DataNascimento = v.DataNascimento.ToString("dd/MM/yyyy"),
                EstadoCivil = v.EstadoCivil.EnumDisplay(),
                Sexo = v.Sexo.EnumDisplay(),
                Tipo = v.Tipo.EnumDisplay(),
                Documentos = v.Documentos.ToList().ConvertAll(c => (DocumentoResponse)c),
            };
        }

        #endregion
    }
}