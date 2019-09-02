using Person.Core;

namespace Person.Arguments.Response {
    public class EmpresaResponse {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string InscricaoEstadual { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
        public string Responsavel { get; set; }
        public string CPFResponsavel { get; set; }

        public string PessoaId { get; set; }
        public PessoaResponse Pessoa { get; set; }

        #region - casting -

        public static explicit operator EmpresaResponse(Empresa v) {
            return v == null ? null : new EmpresaResponse {
                Id = v.Id,
                Celular = v.Celular,
                CPFResponsavel = v.CPFResponsavel,
                Email = v.Email,
                InscricaoEstadual = v.InscricaoEstadual,
                Nome = v.Nome,
                Pessoa = (PessoaResponse)v.Pessoa,
                PessoaId = v.PessoaId,
                Responsavel = v.Responsavel,
                Telefone = v.Telefone,
            };
        }

        #endregion
    }
}