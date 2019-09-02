using Newtonsoft.Json;

namespace Person.Arguments.Request {
    public class EstadoRequest {
        public string Nome { get; set; }
        public string Sigla { get; set; }

        [JsonIgnore]
        public string Id { get; set; }
    }
}
