using Application.Request.Feedback;
using Application.Request.Order;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validation
{
    public class FeedbackValidator : AbstractValidator<FeedbackRequest>
    {
        public FeedbackValidator() 
        {
            RuleFor(feedback => feedback.FeedbackStars)
            .InclusiveBetween(1, 5)
            .WithMessage("FeedbackStars must be a value between 1 and 5.");
        }
    }
}
