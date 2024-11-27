using Application.Request.Order;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validation
{
    public class CreateOrderValidator : AbstractValidator<OrderRequest>
    {
        public CreateOrderValidator()
        {
            RuleFor(order => order.FeedbackStars)
            .InclusiveBetween(1.0f, 5.0f)
            .WithMessage("Feedback stars must be between 1 and 5.");
        }
    }
}
