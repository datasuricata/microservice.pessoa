using System.ComponentModel.DataAnnotations;

namespace Person.Core.Enums {
    public enum TipoDocumento {
        [Display(Description = "Indefinido")]
        Indefinido = 0,

        [Display(Description = "Registro Geral")]
        RG = 1,

        [Display(Description = "Cadastro de Pessoa Física")]
        CPF = 2,

        [Display(Description = "Cadastro Nacional da Pessoa Jurídica")]
        CNPJ = 3,

        [Display(Description = "Carteira Nacional de Habilitação")]
        CNH = 5,
    }
}
