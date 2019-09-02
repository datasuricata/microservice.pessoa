using FluentValidation;
using Person.Core;
using System.Text.RegularExpressions;

namespace Person.Services.Validators.Core {
    public class UsuarioValidator : AbstractValidator<Usuario> {

        public UsuarioValidator() {
            RuleFor(r => r.Login)
                .NotNull().NotEmpty().WithMessage("Login é obrigatório.")
                .Must(ContainSpaces).WithMessage("Login não pode conter espaçamentos.") // todo inverter validador ?
                .Must(ContainSpecial).WithMessage("Login não pode conter caracteres especiais."); // todo inverter validador ?

            RuleFor(r => r.Senha)
                .NotNull().NotEmpty().WithMessage("Senha é obrigatória.");

            RuleFor(r => r.Email)
                .EmailAddress().WithMessage("E-mail inválido.");
        }

        public bool ContainSpaces(string login) {
            return login.Contains(" ");
        }

        public bool ContainSpecial(string login) {
            return new Regex("^[a-zA-Z0-9 ]*$").IsMatch(login);
        }
    }
}