using System.ComponentModel.DataAnnotations;

namespace Person.Core.Enums {
    public enum TipoUsuario {

        [Display(Description = "Cliente")]
        Cliente = 0,

        [Display(Description = "Prestador")]
        Prestador = 1,

        [Display(Description = "Administrativo")]
        Administrativo = 2,

        [Display(Description = "Ninja das Sombras")]
        Root = 999,
    }
}
