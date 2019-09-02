using System.ComponentModel.DataAnnotations;

namespace Person.Core.Enums {
    public enum TipoConstrucao {
        [Display(Description = "Indefinido")]
        Indefinido = 0,

        [Display(Description = "Residencial")]
        Residencial = 1,

        [Display(Description = "Comercial")]
        Comercial = 2,
    }
}
