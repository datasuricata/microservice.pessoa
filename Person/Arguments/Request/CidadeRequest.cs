using Newtonsoft.Json;

namespace Person.Arguments.Request {
    public class CidadeRequest {

        public string Nome { get; set; }
        public string EstadoId { get; set; }

        public bool Capital { get; set; }

        [JsonIgnore]
        public string Id { get; set; }
    }
}