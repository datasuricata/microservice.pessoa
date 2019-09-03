using Person.Core;
using Person.Services.Helpers;

namespace Person.Arguments.Response {
    public class DocumentoResponse {

        public string Id { get; set; }
        public string Dados { get; set; }
        public string ImagemUri { get; set; }
        public string Tipo { get; set; }
        public bool Aprovado { get; set; }


        public static explicit operator DocumentoResponse(Documento v) {
            return v == null ? null : new DocumentoResponse {
                Id = v.Id,
                Aprovado = v.Aprovado,
                Dados = v.Dados,
                ImagemUri = v.ImagemUri,
                Tipo = v.Tipo.EnumDisplay(),
            };
        }
    }
}