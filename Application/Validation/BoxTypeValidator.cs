using Application.Request.BoxType;
using Application.Request.Feedback;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validation
{
    public class BoxTypeValidator : AbstractValidator<BoxTypeRequest>
    {
        public BoxTypeValidator()
        {

            RuleFor(x => x.BoxName)
            .NotEmpty().WithMessage("Box Name is required.");

            RuleFor(x => x.Capacity)
            .NotNull().WithMessage("Capacity is required.")
            .GreaterThan(0).WithMessage("Capacity must be a positive number.");


            RuleFor(x => x.ShippingCost)
                .NotNull().WithMessage("Shipping Cost is required.")
                .GreaterThan(0).WithMessage("Shipping Cost must be a positive number.");


        }
    }
}
