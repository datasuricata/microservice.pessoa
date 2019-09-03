using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Person.Arguments.Request;
using Person.Arguments.Response;
using Person.Core;
using Person.Core.Enums;
using Person.Infra.Persistence;
using Person.Services.Base;
using Person.Services.Core.Interfaces;
using Person.Services.Validators.Core;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Person.Services.Core {
    public class ServiceUsuario : ServiceBase, IServiceUsuario {

        /// <summary>
        /// Use this to retrive JWT key from config
        /// </summary>
        private readonly IConfiguration _app;
        private readonly IRepository<Usuario> _repoUsuario;

        public ServiceUsuario(IConfiguration app, IRepository<Usuario> repoUsuario, IServiceProvider provider) : base(provider) {
            _app = app;
            _repoUsuario = repoUsuario;
        }

        public async Task<Usuario> Eu(string id) {
            return await _repoUsuario.PorId(true, id);
        }

        public async Task<Usuario> PorEmail(string email) {
            return await _repoUsuario.Por(true, m => m.Email
                .Equals(email, StringComparison.InvariantCultureIgnoreCase));
        }

        public async Task<Usuario> PorId(string id) {
            return await _repoUsuario.PorId(true, id);
        }

        public async Task<IEnumerable<Usuario>> ListarUsuarios() {
            return await _repoUsuario.ListarPor(true, x => !x.Deletado);
        }

        public async Task Registrar(UsuarioRequest param, TipoUsuario tipo) {

            var usuario = new Usuario(param.Login, param.Senha, param.Email);
            usuario.DefinirTipo(tipo);

            var nick = _repoUsuario.Existe(m => m.Login
            .Equals(param.Login, StringComparison.InvariantCultureIgnoreCase));

            var email = _repoUsuario.Existe(m => m.Email
            .Equals(param.Email, StringComparison.InvariantCultureIgnoreCase));

            #region - validator -

            _notify.When<ServiceUsuario>(nick,
                "Login já esta cadastrado.");

            _notify.When<ServiceUsuario>(email,
                "Email já esta cadastrado.");

            _notify.Validate(usuario, new UsuarioValidator());

            #endregion

            await _repoUsuario.Registrar(usuario);
        }

        public async Task Atualizar(UsuarioRequest param) {

            var id = param.Id ?? param.ProprietarioId;

            var usuario = await _repoUsuario.PorId(false, id);

            if (usuario != null) {
                usuario.Atualizar(param.Login, param.Email);

                _notify.Validate(usuario, new UsuarioValidator());
                _repoUsuario.Atualizar(usuario);
            }
        }

        #region - security -

        public async Task<LoginResponse> Autenticar(LoginRequest param) {
            var usuario = await _repoUsuario.Por(true, u => u.Login
            .Equals(param.Login, StringComparison.InvariantCultureIgnoreCase) || u.Email
            .Equals(param.Login, StringComparison.InvariantCultureIgnoreCase));

            #region - validator -

            _notify.When<ServiceUsuario>(usuario == null,
               "Usuário não encontrado.");

            _notify.When<ServiceUsuario>(usuario?.Senha != param.Senha,
                "Senha não confere, verifique e tente novamente.");

            #endregion

            if (!_notify.IsValid) return null;

            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_app["SecurityKey"]);

            var payload = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Email, usuario.Email),
                    new Claim(ClaimTypes.NameIdentifier, usuario.Id),
                }),
                Expires = DateTime.UtcNow.AddHours(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature),
                IssuedAt = DateTime.UtcNow,
            };

            var token = handler.CreateToken(payload);
            return ((LoginResponse)usuario).InjectToken(handler.WriteToken(token));
        }

        #endregion
    }
}