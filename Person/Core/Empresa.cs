using Person.Core.Base;
using Person.Services.Helpers;

namespace Person.Core {
    public class Empresa : EntityBase {

        public string Nome { get; private set; }
        public string InscricaoEstadual { get; private set; }
        public string Telefone { get; private set; }
        public string Celular { get; private set; }
        public string Email { get; private set; }
        public string Responsavel { get; private set; }
        public string CPFResponsavel { get; private set; }

        public string PessoaId { get; set; }
        public Pessoa Pessoa { get; set; }

        protected Empresa() {
        }

        public Empresa(string nome, string inscricaoEstadual, string telefone, string celular, string email, string responsavel, string cPFResponsavel) {
            Nome = nome;
            InscricaoEstadual = inscricaoEstadual.CleanFormat();
            Telefone = telefone.CleanFormat();
            Celular = celular.CleanFormat();
            Email = email;
            Responsavel = responsavel;
            CPFResponsavel = cPFResponsavel.CleanFormat();
        }

        public void Atualizar(string nome, string inscricaoEstadual, string telefone, string celular, string email, string responsavel, string cPFResponsavel) {
            Nome = nome;
            InscricaoEstadual = inscricaoEstadual.CleanFormat();
            Telefone = telefone.CleanFormat();
            Celular = celular.CleanFormat();
            Email = email;
            Responsavel = responsavel;
            CPFResponsavel = cPFResponsavel.CleanFormat();
        }
    }
}