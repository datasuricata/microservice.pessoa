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
    public class PessoaController : BaseController {

        private readonly IServicePessoa _service;

        public PessoaController(IServicePessoa service) {
            _service = service;
        }

        [HttpGet("porId/{id}")]
        public async Task<IActionResult> PorId(string id) {
            var result = await _service.PorId(id);
            return Result((PessoaResponse)result);
        }

        [HttpGet("porDocumento/{value}")]
        public async Task<IActionResult> PorDocumento(string value) {
            var result = await _service.PorDocumento(value);
            return Result((PessoaResponse)result);
        }

        [HttpGet("like/{param}")]
        public async Task<IActionResult> Like(string param) {
            var result = await _service.Like(param);
            return Result(result.ToList().ConvertAll(c => (PessoaResponse)c));
        }

        [HttpGet("listar")]
        public async Task<IActionResult> Listar() {
            var result = await _service.Listar();
            return Result(result.ToList().ConvertAll(c => (PessoaResponse)c));
        }

        [HttpPost("listar/porIds")]
        public async Task<IActionResult> ListarIds([FromBody]IEnumerable<string> ids) {
            var result = await _service.ListarPorIds(ids);
            return Result(result.ToList().ConvertAll(c => (PessoaResponse)c));
        }

        [HttpPost("registrar/pessoa")]
        public async Task<IActionResult> Registrar([FromBody]PessoaRequest param) {
            await _service.RegistrarPessoa(param.InjectAccount(LoggedLess, nameof(param.ProprietarioId)));
            return Result(new { Message = "Registrado." });
        }

        [HttpPost("registrar/conjuge")]
        public async Task<IActionResult> Conjugue([FromBody]PessoaRequest param) {
            await _service.RegistrarConjuge(param.InjectAccount(LoggedLess, nameof(param.ProprietarioId)));
            return Result(new { Message = "Registrado." });
        }

        [HttpPut("atualizar/pessoa")]
        public async Task<IActionResult> AtualizarPessoa([FromBody]PessoaRequest param) {
            await _service.AtualizarPessoa(param.InjectAccount(LoggedLess, nameof(param.ProprietarioId)));
            return Result(new { Message = "Atualizado." });
        }

        [HttpPut("atualizar/conjuge")]
        public async Task<IActionResult> AtualizarConjuge([FromBody]PessoaRequest param) {
            await _service.AtualizarConjuge(param.InjectAccount(LoggedLess, nameof(param.ProprietarioId)));
            return Result(new { Message = "Atualizado." });
        }

        [HttpPut("etapa/{id}")]
        public async Task<IActionResult> Etapa(string id, [FromBody]EtapaAtual param) {
            await _service.TrocarEtapa(id, param);
            return Result(new { Message = "Atualizado." });
        }
    }
}