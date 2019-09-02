using FluentValidation;
using Person.Core;

namespace Person.Services.Validators.Core {
    public class EnderecoValidator : AbstractValidator<Endereco> {

        public EnderecoValidator() {
            RuleFor(r => r.Logradouro).NotNull().NotEmpty()
                .WithMessage("Informe o logradouro.");

            RuleFor(r => r.CEP).NotNull().NotEmpty()
                .WithMessage("CEP é obrigatório.");

            RuleFor(r => r.Bairro).NotNull().NotEmpty()
                .WithMessage("Informe o bairro.");

            RuleFor(r => r.Cidade).NotNull()
                .WithMessage("Informe a cidade.");

            RuleFor(r => r.Estado).NotNull()
                .WithMessage("Informe o estado.");

            RuleFor(r => r.Pessoa).NotNull()
                .WithMessage("Vincular uma pessoa é obrigatório. Contate o suporte.");
        }
    }
}