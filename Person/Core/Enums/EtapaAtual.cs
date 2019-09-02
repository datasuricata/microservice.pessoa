using System.ComponentModel.DataAnnotations;

namespace Person.Core.Enums {
    public enum EtapaAtual {

        [Display(Description = "Pendente")]
        Pendente = 1,

        [Display(Description = "Aprovado")]
        Aprovado = 2,

        [Display(Description = "Recusado")]
        Recusado = 3,

        [Display(Description = "Bloqueado")]
        Bloqueado = 4,
    }
}
