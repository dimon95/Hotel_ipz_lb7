/* (C) 2014-2016, Sergei Zaychenko, NURE, Kharkiv, Ukraine */

using System;
using System.Collections.Generic;

using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;


namespace Hotel.Dependencies
{
    class SemanticLoggingInterceptionBehavior : IInterceptionBehavior
    {

        public IMethodReturn Invoke ( IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext )
        {
            Log.ServiceMethodStarted( input.MethodBase.DeclaringType.Name, input.MethodBase.Name );

            var result = getNext()( input, getNext );
            if ( result.Exception != null )
                Log.Failure( result.Exception.Message );

            else
                Log.ServiceMethodFinished( input.MethodBase.DeclaringType.Name, input.MethodBase.Name );

            return result;
        }

        public IEnumerable<Type> GetRequiredInterfaces ()
        {
            return Type.EmptyTypes;
        }

        public bool WillExecute
        {
            get { return true; }
        }

        [ Dependency ]
        protected HotelEventSource Log { get; set; }
    }
}
