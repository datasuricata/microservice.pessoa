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
    public class EnderecoController : BaseController {

        private readonly IServiceEndereco _service;

        public EnderecoController(IServiceEndereco service) {
            _service = service;
        }

        [HttpGet("porId/{id}")]
        public async Task<IActionResult> PorId(string id) {
            var result = await _service.PorId(id);
            return Result((EnderecoResponse)result);
        }

        [HttpGet("porPessoa/{id}")]
        public async Task<IActionResult> PorPessoa(string id) {
            var result = await _service.PorPessoa(id);
            return Result((EnderecoResponse)result);
        }

        [HttpGet("porCep/{cep}")]
        public async Task<IActionResult> PorCep(string cep) {
            var result = await _service.PorCEP(cep);
            return Result((EnderecoResponse)result);
        }

        [HttpGet("listar")]
        public async Task<IActionResult> Listar() {
            var result = await _service.Listar();
            return Result(result.ToList().ConvertAll(c => (EnderecoResponse)c));
        }

        [HttpGet("listarPorPessoa/{id}")]
        public async Task<IActionResult> ListarPessoa(string id) {
            var result = await _service.ListarPorPessoa(id);
            return Result(result.ToList().ConvertAll(c => (EnderecoResponse)c));
        }

        [HttpPost("registrar/endereco")]
        public async Task<IActionResult> RegistrarEndereco([FromBody]EnderecoRequest param) {
            await _service.RegistrarEndereco(param.InjectAccount(LoggedLess, nameof(param.ProprietarioId)));
            return Result(new { Message = "Registrado." });
        }

        [HttpPost("registrar/cidade")]
        public async Task<IActionResult> RegistrarCidade([FromBody]CidadeRequest param) {
            await _service.RegistrarCidade(param);
            return Result(new { Message = "Registrado." });
        }

        [HttpPost("registrar/estado")]
        public async Task<IActionResult> RegistrarEstado([FromBody]EstadoRequest param) {
            await _service.RegistrarEstado(param);
            return Result(new { Message = "Registrado." });
        }

        [HttpPut("atualizar/endereco")]
        public async Task<IActionResult> AtualizarEndereco([FromBody]EnderecoRequest param) {
            await _service.AtualizarEndereco(param.InjectAccount(LoggedLess, nameof(param.ProprietarioId)));
            return Result(new { Message = "Atualizado." });
        }

        [HttpPut("atualizar/cidade")]
        public async Task<IActionResult> AtualizarCidade([FromBody]CidadeRequest param) {
            await _service.AtualizarCidade(param);
            return Result(new { Message = "Atualizado." });
        }

        [HttpPut("atualizar/estado")]
        public async Task<IActionResult> AtualizarEstado([FromBody]EstadoRequest param) {
            await _service.AtualizarEstado(param);
            return Result(new { Message = "Atualizado." });
        }
    }
}