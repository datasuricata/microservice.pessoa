using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Person.Arguments.Request;
using Person.Arguments.Response;
using Person.Controllers.Base;
using Person.Core.Enums;
using Person.Services.Core.Interfaces;
using Person.Services.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Person.Controllers {
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class DocumentoController : BaseController {

        private readonly IServiceDocumento _service;

        public DocumentoController(IServiceDocumento service) {
            _service = service;
        }

        [HttpGet("porId/{id}")]
        public async Task<IActionResult> PorId(string id) {
            var result = await _service.PorId(id);
            return Result((DocumentoResponse)result);
        }

        [HttpGet("porPessoa/{id}")]
        public async Task<IActionResult> PorPessoa(string id) {
            var result = await _service.PorPessoa(id);
            return Result((DocumentoResponse)result);
        }

        [HttpGet("listar")]
        public async Task<IActionResult> Listar() {
            var result = await _service.Listar();
            return Result(result.ToList().ConvertAll(c => (DocumentoResponse)c));
        }

        [HttpGet("listarPorTipo/{tipo}")]
        public async Task<IActionResult> ListarIds(TipoDocumento tipo) {
            var result = await _service.ListarPorTipo(tipo);
            return Result(result.ToList().ConvertAll(c => (DocumentoResponse)c));
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar([FromBody]DocumentoRequest param) {
            await _service.Registrar(param.InjectAccount(LoggedLess, nameof(param.ProprietarioId)));
            return Result(new { Message = "Registrado." });
        }

        [HttpPut("cadeado/{id}")]
        public async Task<IActionResult> Cadeado(string id) {
            await _service.Cadeado(id);
            return Result(new { Message = "Documento processado." });
        }

        [HttpPut("atualizar")]
        public async Task<IActionResult> Atualizar([FromBody]DocumentoRequest param) {
            await _service.Atualizar(param.InjectAccount(LoggedLess, nameof(param.ProprietarioId)));
            return Result(new { Message = "Atualizado." });
        }
    }
}