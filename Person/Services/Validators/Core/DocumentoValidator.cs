using FluentValidation;
using Person.Core;

namespace Person.Services.Validators.Core {
    public class DocumentoValidator : AbstractValidator<Documento> {

        public DocumentoValidator() {
            RuleFor(r => r.Dados).NotNull().NotEmpty()
                .WithMessage("Informe os dados do documento.");

            RuleFor(r => r.Pessoa).NotNull()
                .WithMessage("Vincular uma pessoa é obrigatório. Contate o suporte.");
        }
    }
}