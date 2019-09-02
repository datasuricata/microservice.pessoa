using FluentValidation;
using Person.Core;

namespace Person.Services.Validators.Core {
    public class EmpresaValidator : AbstractValidator<Empresa> {

        public EmpresaValidator() {
            RuleFor(r => r.CPFResponsavel).NotEmpty().NotNull()
                .WithMessage("CPF do responsável legal deve ser informado.");

            RuleFor(r => r.Responsavel).NotEmpty().NotNull()
                .WithMessage("Nome do responsável deve ser informado.");

            RuleFor(r => r.Email).EmailAddress()
                .WithMessage("Endereço de e-mail inválido.");

            RuleFor(r => r.InscricaoEstadual).NotEmpty().NotNull()
                .WithMessage("Inscrição estadual é obrigatória.");

            RuleFor(r => r.Pessoa).NotEmpty()
                .WithMessage("Vincular uma pessoa é obrigatório. Contate o suporte.");
        }
    }
}
