/* (C) 2014-2016, Sergei Zaychenko, NURE, Kharkiv, Ukraine */

using System;

using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace Hotel.Utils.Validators
{

    [ AttributeUsage( AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter ) ]
    public class NonEmptyStringValidator : ValidatorAttribute
    {
        protected override Validator DoCreateValidator ( Type targetType )
        {
            return new StringLengthValidator(
                        1, 
                        RangeBoundaryType.Inclusive, 
                        int.MaxValue, 
                        RangeBoundaryType.Ignore 
                   );
        }
    }
}
