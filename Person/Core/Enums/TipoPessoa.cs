using System.ComponentModel.DataAnnotations;

namespace Person.Core.Enums {
    public enum TipoPessoa {
        [Display(Description = "Pessoa Física")]
        PF = 1,

        [Display(Description = "Pessoa Jurídica")]
        PJ = 2,
    }
}
