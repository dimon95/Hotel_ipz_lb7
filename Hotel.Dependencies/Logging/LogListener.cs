/* (C) 2014-2016, Sergei Zaychenko, NURE, Kharkiv, Ukraine */

using System;
using System.Diagnostics.Tracing;

using Microsoft.Practices.EnterpriseLibrary.SemanticLogging;
using Microsoft.Practices.Unity;

namespace Hotel.Dependencies
{
    public class LogListener : IDisposable
    {
        internal void OnStartup ()
        {
            listenerInfo = FlatFileLog.CreateListener( "hotel_services.log" );
            listenerInfo.EnableEvents( Log, EventLevel.LogAlways, HotelEventSource.Keywords.ServiceTracing );

            listenerErrors = FlatFileLog.CreateListener( "hotel_diagnostic.log" );
            listenerErrors.EnableEvents( Log, EventLevel.LogAlways, HotelEventSource.Keywords.Diagnostic );

            Log.StartupSucceeded();
        }

        public void Dispose ()
        {
            listenerInfo.DisableEvents( Log );
            listenerErrors.DisableEvents( Log );

            listenerInfo.Dispose();
            listenerErrors.Dispose();
        }


        [ Dependency ]
        protected HotelEventSource Log { get; set; }

        private EventListener listenerInfo;
        private EventListener listenerErrors;
    }
}
