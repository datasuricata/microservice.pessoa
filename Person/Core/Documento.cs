using Person.Core.Base;
using Person.Core.Enums;
using Person.Services.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Person.Core {
    public class Documento : EntityBase {

        #region - attributes -

        public string Dados { get; private set; }
        public string ImagemUri { get; private set; }
        public bool Aprovado { get; private set; }
        public TipoDocumento Tipo { get; private set; }

        public string PessoaId { get; set; }
        public Pessoa Pessoa { get; set; }

        #endregion

        #region - ctor -

        // for entity, dont remove
        protected Documento() {
        }

        public Documento(string dados, string imagemUri, TipoDocumento tipo) {
            Dados = dados.CleanFormat();
            ImagemUri = imagemUri;
            Tipo = tipo;
        }

        #endregion

        #region  - methods - 

        public void Cadeado() {
            Aprovado = !Aprovado;
        }

        public void DefinirTipo(TipoDocumento tipo) {
            Tipo = tipo;
        }

        public void Atualizar(string dados, string imagemUri, TipoDocumento tipo) {
            Dados = dados.CleanFormat();
            ImagemUri = imagemUri;
            Tipo = tipo;
        }

        #endregion
    }
}
