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
    public class DateOrderValidator : ValidatorAttribute
    {
        protected override Validator DoCreateValidator ( Type targetType )
        {
            return new RangeValidator(
                             Utils.BookingDate.GetToday(),
                             RangeBoundaryType.Inclusive,
                             Utils.BookingDate.GetMax(),
                             RangeBoundaryType.Inclusive
                       );
        }
    }
}