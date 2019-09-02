using Person.Core;
using Person.Services.Helpers;

namespace Person.Arguments.Response {
    public class LoginResponse {

        public string Id { get; set; }
        public string Email { get; set; }
        public string Tipo { get; set; }
        public string Token { get; set; }

        public static explicit operator LoginResponse(Usuario v) {
            return v == null ? null : new LoginResponse {
                Id = v.Id,
                Tipo = v.Tipo.EnumDisplay(),
                Email = v.Email,
            };
        }
    }

    public static class SecurityResponse {
        public static LoginResponse InjectToken(this LoginResponse argument, string token) {
            argument.Token = token;
            return argument;
        }
    }
}
