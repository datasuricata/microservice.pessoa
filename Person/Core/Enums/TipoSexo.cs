using System.ComponentModel.DataAnnotations;

namespace Person.Core.Enums {
    public enum TipoSexo {
        [Display(Description = "Indefinido")]
        Indefinido = 0,

        [Display(Description = "Masculino")]
        M = 1,

        [Display(Description = "Feminino")]
        F = 2,
    }
}