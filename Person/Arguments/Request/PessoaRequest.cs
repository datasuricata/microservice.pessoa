using Newtonsoft.Json;
using Person.Core.Enums;
using System;

namespace Person.Arguments.Request {
    public class PessoaRequest {

        public string UsuarioId { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Profissao { get; set; }
        public string Telefone { get; set; }

        public EstadoCivil EstadoCivil { get; set; }
        public TipoPessoa Tipo { get; set; }
        public TipoSexo Sexo { get; set; }

        public DateTime DataNascimento { get; set; }

        [JsonIgnore]
        public string ProprietarioId { get; set; }
    }
}