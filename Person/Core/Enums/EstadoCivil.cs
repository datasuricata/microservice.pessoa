using System.ComponentModel.DataAnnotations;

namespace Person.Core.Enums {
    public enum EstadoCivil {
        [Display(Description = "Solteiro")]
        Solteiro = 1,

        [Display(Description = "Casado")]
        Casado = 2,

        [Display(Description = "Uniao Estavel")]
        Uniao = 3,

        [Display(Description = "Separado")]
        Separado = 4,

        [Display(Description = "Divorciado")]
        Divorciado = 5,

        [Display(Description = "Viuvo")]
        Viuvo = 6
    }
}