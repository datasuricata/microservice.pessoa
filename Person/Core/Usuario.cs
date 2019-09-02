using Person.Core.Base;
using Person.Core.Enums;
using Person.Services.Helpers;

namespace Person.Core {
    public class Usuario : EntityBase {

        #region - attributes -

        public string Login { get; private set; }
        public string Senha { get; private set; }
        public string Email { get; private set; }

        public TipoUsuario Tipo { get; private set; }

        public Pessoa Pessoa { get; set; }

        #endregion

        #region - ctor -

        // for entity, dont remove
        protected Usuario() {
        }

        public Usuario(string login, string senha, string email) {
            Login = login;
            Senha = senha.EncryptToMD5().EncryptToSHA1();
            Email = email;
        }

        #endregion

        #region - methods -

        public void DefinirTipo(TipoUsuario tipo) {
            Tipo = tipo;
        }

        public void Atualizar(string login, string email) {
            Login = login;
            Email = email;
        }

        #endregion
    }
}
