using System;

using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace Hotel.Services.Validators
{
     [AttributeUsage( AttributeTargets.Property |
                      AttributeTargets.Field |
                      AttributeTargets.Parameter )]
    public class BedCountValidator : ValidatorAttribute
    {
        protected override Validator DoCreateValidator ( Type targetType )
        {
            return new RangeValidator(
                             1,
                             RangeBoundaryType.Inclusive,
                             2,
                             RangeBoundaryType.Inclusive
                       );
        }
    }
}