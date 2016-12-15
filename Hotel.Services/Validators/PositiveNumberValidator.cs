using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace Hotel.Services.Validators
{
    [AttributeUsage( AttributeTargets.Property |
                      AttributeTargets.Field |
                      AttributeTargets.Parameter )]
    public class PositiveNumberValidator : ValidatorAttribute
    {
        protected override Validator DoCreateValidator ( Type targetType )
        {
            return new RangeValidator(
                             1,
                             RangeBoundaryType.Inclusive,
                             Int32.MaxValue,
                             RangeBoundaryType.Ignore
                       );
        }
    }
}