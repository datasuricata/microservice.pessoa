using Person.Arguments.Request;
using Person.Arguments.Response;
using Person.Core;
using Person.Core.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Person.Services.Core.Interfaces {
    public interface IServiceUsuario {

        Task<Usuario> Eu(string id);
        Task<Usuario> PorEmail(string email);
        Task<Usuario> PorId(string id);

        Task<IEnumerable<Usuario>> ListarUsuarios();
        Task<LoginResponse> Autenticar(LoginRequest param);

        Task Registrar(UsuarioRequest param, TipoUsuario tipo);
        Task Atualizar(UsuarioRequest param);
    }
}
