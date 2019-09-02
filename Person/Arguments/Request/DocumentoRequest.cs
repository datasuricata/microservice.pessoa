using Newtonsoft.Json;
using Person.Core.Enums;

namespace Person.Arguments.Request {
    public class DocumentoRequest {

        public string UsuarioId { get; set; }
        public string Dados { get; set; }
        public string ImagemUri { get; set; }
        public bool Aprovado { get; set; }
        public TipoDocumento Tipo { get; set; }

        [JsonIgnore]
        public string Id { get; set; }

        [JsonIgnore]
        public string ProprietarioId { get; set; }
    }
}
