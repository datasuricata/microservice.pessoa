using FluentValidation;
using Person.Core;

namespace Person.Services.Validators.Core {
    public class PessoaValidator : AbstractValidator<Pessoa> {

        public PessoaValidator() {
            RuleFor(r => r.Nome).NotNull().NotEmpty()
                .WithMessage("Nome é obrigatório.");

            RuleFor(r => r.UsuarioId).NotNull().NotEmpty()
                .WithMessage("Vincular um usuário é obrigatório. Contate o suporte.");

            RuleFor(r => r.EstadoCivil).IsInEnum()
                .WithMessage("Informe o estado civil.");

            RuleFor(r => r.Etapa).IsInEnum()
                .WithMessage("Informe a etapa de cadastro. Contate o suporte.");

            RuleFor(r => r.Tipo).IsInEnum()
                .WithMessage("Informe o tipo de pessoa.");
        }
    }
}