using Application.Request.KoiSize;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validation
{
    public class KoiSizeValidator : AbstractValidator<KoiSizeRequest>
    {
        public KoiSizeValidator() 
        {
            RuleFor(x => x.SizeCmMin)
            .GreaterThanOrEqualTo(0).WithMessage("SizeCmMin must be greater than or equal to 0.");

            RuleFor(x => x.SizeCmMax)
                .GreaterThan(0).WithMessage("SizeCmMax must be greater than 0.")
                .GreaterThanOrEqualTo(x => x.SizeCmMin).WithMessage("SizeCmMax must be greater than or equal to SizeCmMin.");

            RuleFor(x => x.SizeInchMin)
                .GreaterThanOrEqualTo(0).WithMessage("SizeInchMin must be greater than or equal to 0.");

            RuleFor(x => x.SizeInchMax)
                .GreaterThan(0).WithMessage("SizeInchMax must be greater than 0.")
                .GreaterThanOrEqualTo(x => x.SizeInchMin).WithMessage("SizeInchMax must be greater than or equal to SizeInchMin.");

            RuleFor(x => x.SpaceRequired)
                .GreaterThan(0).WithMessage("SpaceRequired must be greater than 0.");
        }
    }
}
