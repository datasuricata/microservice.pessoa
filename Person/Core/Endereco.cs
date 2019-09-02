using Person.Core.Base;
using Person.Core.Enums;
using Person.Services.Helpers;

namespace Person.Core {
    public class Endereco : EntityBase {

        #region - attributes -

        public TipoConstrucao Tipo { get; private set; }

        public bool Principal { get; set; }

        public int Numero { get; private set; }
        public int Complemento { get; private set; }

        public string Logradouro { get; private set; }
        public string Bairro { get; private set; }
        public string CEP { get; private set; }

        public Cidade Cidade { get; set; }
        public Estado Estado { get; set; }

        public string PessoaId { get; set; }
        public Pessoa Pessoa { get; set; }

        #endregion

        #region - ctor - 

        // for entity, dont remove
        protected Endereco() {
        }

        public Endereco(TipoConstrucao tipo, bool principal, int numero, int complemento, string logradouro, string bairro, string cEP) {
            Tipo = tipo;
            Principal = principal;
            Numero = numero;
            Complemento = complemento;
            Logradouro = logradouro.CleanFormat();
            Bairro = bairro;
            CEP = cEP.CleanFormat();
        }

        #endregion

        #region - methods -


        public void DefinirTipo(TipoConstrucao tipo) {
            Tipo = tipo;
        }

        public void Atualizar(TipoConstrucao tipo, bool principal, int numero, int complemento, string logradouro, string bairro, string cEP) {
            Tipo = tipo;
            Principal = principal;
            Numero = numero;
            Complemento = complemento;
            Logradouro = logradouro.CleanFormat();
            Bairro = bairro;
            CEP = cEP.CleanFormat();
        }

        #endregion
    }
}
