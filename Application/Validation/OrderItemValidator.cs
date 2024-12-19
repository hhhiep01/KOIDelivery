using Application.Request.OrderItem;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validation
{
    public class OrderItemValidator : AbstractValidator<OrderItemDetail>
    {
        public OrderItemValidator() 
        {
            RuleFor(x => x.Quantity)
            .NotNull().WithMessage("Quantity is required.")
            .GreaterThan(0).WithMessage("Quantity must be a positive number.");

        }
    }
}
