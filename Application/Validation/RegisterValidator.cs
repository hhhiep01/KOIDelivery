using Application.Request.UserAccount;
using FluentValidation;


namespace Application.Validation
{
    public class RegisterValidator :  AbstractValidator<UserRegisterRequest>
    {
        public RegisterValidator()
        {
            RuleFor(user => user.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("A valid email is required.");
            RuleFor(user => user.Password)
                 .NotEmpty().WithMessage("Password is required.")
                 .MinimumLength(7).WithMessage("Password must be more than 6 characters.");
            RuleFor(user => user.FirstName)
                .NotEmpty().WithMessage("FistName is required.");
            RuleFor(user => user.LastName)
               .NotEmpty().WithMessage("LastName is required.");

        }
    }
}
