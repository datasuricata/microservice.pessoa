using Person.Core.Base;
using Person.Services.Helpers;

namespace Person.Core {
    public class Cidade : EntityBase {

        public string Nome { get; private set; }
        public bool Capital { get; private set; }

        public string EstadoId { get; set; }
        public Estado Estado { get; set; }

        protected Cidade() {
        }

        public Cidade(string nome, bool capital) {
            Nome = nome.CleanFormat();
            Capital = capital;
        }

        public void Atualizar(string nome, bool capital) {
            Nome = nome.CleanFormat();
            Capital = capital;
        }
    }
}