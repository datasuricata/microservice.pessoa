using Person.Core.Base;
using Person.Services.Helpers;

namespace Person.Core {
    public class Estado : EntityBase {

        public string Nome { get; private set; }
        public string Sigla { get; private set; }

        protected Estado() {
        }

        public Estado(string nome, string sigla) {
            Nome = nome.CleanFormat();
            Sigla = sigla.CleanFormat();
        }

        public void Atualizar(string nome, string sigla) {
            Nome = nome.CleanFormat();
            Sigla = sigla.CleanFormat();
        }
    }
}
