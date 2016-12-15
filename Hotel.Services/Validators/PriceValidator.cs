/* (C) 2014-2016, Sergei Zaychenko, NURE, Kharkiv, Ukraine */

using System;

using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace Hotel.Services.Validators
{

    [ AttributeUsage( AttributeTargets.Property  | 
                      AttributeTargets.Field     | 
                      AttributeTargets.Parameter ) 
    ]
    public class PriceValidator : ValidatorAttribute
    {
        protected override Validator DoCreateValidator ( Type targetType )
        {
           return new RangeValidator( 
                            0.0M, 
                            RangeBoundaryType.Inclusive, 
                            decimal.MaxValue, 
                            RangeBoundaryType.Ignore 
                      );
        }
    }
}
