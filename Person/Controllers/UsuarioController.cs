using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Person.Arguments.Request;
using Person.Arguments.Response;
using Person.Controllers.Base;
using Person.Services.Core.Interfaces;
using Person.Services.Helpers;
using System.Linq;
using System.Threading.Tasks;

namespace Person.Controllers {
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UsuarioController : BaseController {

        private readonly IServiceUsuario _service;

        public UsuarioController(IServiceUsuario service) {
            _service = service;
        }

        [HttpGet("eu")]
        public async Task<IActionResult> Me() {
            var result = await Logged();
            return Result((UsuarioResponse)result);
        }

        [HttpGet("porId/{id}")]
        public async Task<IActionResult> PorId(string id) {
            var result = await _service.PorId(id);
            return Result((UsuarioResponse)result);
        }

        [HttpGet("porEmail/{email}")]
        public async Task<IActionResult> PorEmail(string email) {
            var result = await _service.PorEmail(email);
            return Result((UsuarioResponse)result);
        }

        [HttpGet("listar")]
        public async Task<IActionResult> Listar() {
            var result = await _service.ListarUsuarios();
            return Result(result.ToList().ConvertAll(c => (UsuarioResponse)c));
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginRequest param) {
            var result = await _service.Autenticar(param);
            return Result(result);
        }

        [AllowAnonymous]
        [HttpPost("regitrar/cliente")]
        public async Task<IActionResult> Cliente([FromBody]UsuarioRequest param) {
            await _service.Registrar(param, Core.Enums.TipoUsuario.Cliente);
            return Result(new { Message = "Registrado." });
        }

        [AllowAnonymous]
        [HttpPost("regitrar/prestador")]
        public async Task<IActionResult> Prestador([FromBody]UsuarioRequest param) {
            await _service.Registrar(param, Core.Enums.TipoUsuario.Prestador);
            return Result(new { Message = "Registrado." });
        }

        [HttpPost("regitrar/administrativo")]
        public async Task<IActionResult> Administrativo([FromBody]UsuarioRequest param) {
            await _service.Registrar(param.InjectAccount(LoggedLess,
                nameof(param.ProprietarioId)), Core.Enums.TipoUsuario.Administrativo);

            return Result(new { Message = "Registrado." });
        }

        [HttpPut("atualizar")]
        public async Task<IActionResult> Atualizar([FromBody]UsuarioRequest request) {
            await _service.Atualizar(request.InjectAccount(LoggedLess,
                nameof(request.ProprietarioId)));

            return Result(new { Message = "Atualizado." });
        }
    }
}