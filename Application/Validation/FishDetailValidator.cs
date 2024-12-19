using Application.Request.FishDetail;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validation
{
    public class FishDetailValidator : AbstractValidator<FishDetailRequest>
    {
        public FishDetailValidator() 
        {
            RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.");

            RuleFor(x => x.Age)
                .NotNull().WithMessage("Age is required.")
                .GreaterThan(0).WithMessage("Age must be greater than 0.");

            RuleFor(x => x.Weight)
                .NotNull().WithMessage("Weight is required.")
                .GreaterThan(0).WithMessage("Weight must be greater than 0.");

            RuleFor(x => x.Length)
                .NotNull().WithMessage("Length is required.")
                .GreaterThan(0).WithMessage("Length must be greater than 0.");
        }
    }
}
