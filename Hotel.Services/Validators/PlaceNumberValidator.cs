using System;

using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
namespace Hotel.Services.Validators
{
   
    [AttributeUsage ( AttributeTargets.Property |
                      AttributeTargets.Field |
                      AttributeTargets.Parameter )]
     class PlaceNumberValidator : ValidatorAttribute
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