using FluentValidation;
using LoginComponent.Models.Request.Auth;

namespace LoginComponent.Validations
{
    public class SingUpRequestValidation : AbstractValidator<SingUpRequest>
    {
        public SingUpRequestValidation() 
        {
            RuleFor(password => password.Password)
                .Equal(passConf => passConf.ConfirmPassword)
                    .WithMessage("Password and confirm password do not match")
                    .WithErrorCode("400")
                .MinimumLength(8)
                    .WithMessage("Password is weak")
                    .WithErrorCode("400")
                .Matches("[A-Z]")
                    .WithMessage("Must include uppercase latin characters")
                    .WithErrorCode("422")
                .Matches("[a-z]")
                    .WithMessage("422")
                    .WithErrorCode("Must include lowercase latin characters")
                 .Matches("[0-9]")
                    .WithMessage("422")
                    .WithErrorCode("Must include numbers")
                .Matches("^[A-Za-z0-9]+$")
                    .WithMessage("Only latin characters and numbers are allowed")
                    .WithErrorCode("422");
        }
    }
}
