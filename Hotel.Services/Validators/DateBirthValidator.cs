using System;

using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;


namespace Hotel.Services.Validators
{
    [AttributeUsage( AttributeTargets.Property |
                      AttributeTargets.Field |
                      AttributeTargets.Parameter )]
    public class DateBirthValidator : ValidatorAttribute
    {
        protected override Validator DoCreateValidator ( Type targetType )
        {
            return new RangeValidator(
                             Utils.DateOfBirth.GetMinDateOfBirth(),
                             RangeBoundaryType.Inclusive,
                             Utils.DateOfBirth.GetMinEighteenDate(),
                             RangeBoundaryType.Inclusive
                       );
        }
    }
}