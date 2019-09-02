using Newtonsoft.Json;

namespace Person.Arguments.Request {
    public class UsuarioRequest {

        public string UsuarioId { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public string Email { get; set; }

        [JsonIgnore]
        public string ProprietarioId { get; set; }
    }
}