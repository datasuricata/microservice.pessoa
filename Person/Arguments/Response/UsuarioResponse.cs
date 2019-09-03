using Person.Core;
using Person.Services.Helpers;

namespace Person.Arguments.Response {
    public class UsuarioResponse {
        public string Id { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public string Email { get; set; }
        public string Tipo { get; set; }

        public static explicit operator UsuarioResponse(Usuario v) {
            return v == null ? null : new UsuarioResponse {
                Id = v.Id,
                Email = v.Email,
                Login = v.Login,
                Senha = v.Senha,
                Tipo = v.Tipo.EnumDisplay(),
            };
        }
    }
}
