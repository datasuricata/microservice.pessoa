using Newtonsoft.Json;

namespace Person.Arguments.Request {
    public class EmpresaRequest {

        public string Id { get; set; }
        public string UsuarioId { get; set; }

        public string Nome { get; set; }
        public string InscricaoEstadual { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
        public string Responsavel { get; set; }
        public string CPFResponsavel { get; set; }

        [JsonIgnore]
        public string ProprietarioId { get; set; }
    }
}