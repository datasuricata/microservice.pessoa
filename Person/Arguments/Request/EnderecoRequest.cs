using Newtonsoft.Json;
using Person.Core.Enums;

namespace Person.Arguments.Request {
    public class EnderecoRequest {

        public string Id { get; set; }
        public TipoConstrucao Tipo { get; set; }
        public bool Principal { get; set; }
        public int Numero { get; set; }
        public int Complemento { get; set; }

        public string UsuarioId { get; set; }
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string CEP { get; set; }
        public string CidadeId { get; set; }
        public string EstadoId { get; set; }

        [JsonIgnore]
        public string ProprietarioId { get; set; }
    }
}
