using System.ComponentModel.DataAnnotations;

namespace Person.Core.Enums {
    public enum RegimeCasamento {
        [Display(Description = "Comunhão Total")]
        Total = 1,

        [Display(Description = "Comunhão Parcial")]
        Parcial = 2,

        [Display(Description = "Separação de Bens")]
        Bens = 3
    }
}
