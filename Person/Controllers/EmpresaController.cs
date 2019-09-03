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
    public class EmpresaController : BaseController {

        private readonly IServiceEmpresa _service;

        public EmpresaController(IServiceEmpresa service) {
            _service = service;
        }

        [HttpGet("porId/{id}")]
        public async Task<IActionResult> PorId(string id) {
            var result = await _service.PorId(id);
            return Result((EmpresaResponse)result);
        }

        [HttpGet("porPessoa/{id}")]
        public async Task<IActionResult> PorDocumento(string id) {
            var result = await _service.PorPessoa(id);
            return Result((EmpresaResponse)result);
        }

        [HttpGet("listar")]
        public async Task<IActionResult> Listar() {
            var result = await _service.Listar();
            return Result(result.ToList().ConvertAll(c => (EmpresaResponse)c));
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar([FromBody]EmpresaRequest param) {
            await _service.Registrar(param.InjectAccount(LoggedLess, nameof(param.ProprietarioId)));
            return Result(new { Message = "Registrado." });
        }

        [HttpPut("atualizar")]
        public async Task<IActionResult> Atualizar([FromBody]EmpresaRequest param) {
            await _service.Atualizar(param.InjectAccount(LoggedLess, nameof(param.ProprietarioId)));
            return Result(new { Message = "Atualizado." });
        }
    }
}