using Person.Core;
using Person.Services.Helpers;

namespace Person.Arguments.Response {
    public class EnderecoResponse {

        public string Id { get; set; }
        public string Tipo { get; set; }

        public bool Principal { get; set; }

        public int Numero { get; set; }
        public int? Complemento { get; set; }

        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string CEP { get; set; }

        public Cidade Cidade { get; set; }
        public Estado Estado { get; set; }

        public string PessoaId { get; set; }

        #region - casting -

        public static explicit operator EnderecoResponse(Endereco v) {
            return v == null ? null : new EnderecoResponse {
                Id = v.Id,
                Bairro = v.Bairro,
                CEP = v.CEP,
                Cidade = v.Cidade,
                Complemento = v.Complemento,
                Estado = v.Estado,
                Logradouro = v.Logradouro,
                Numero = v.Numero,
                PessoaId = v.PessoaId,
                Principal = v.Principal,
                Tipo = v.Tipo.EnumDisplay(),
            };
        }

        #endregion
    }
}