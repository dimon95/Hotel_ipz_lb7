/* (C) 2014-2016, Sergei Zaychenko, NURE, Kharkiv, Ukraine */

using System;
using System.Collections.Generic;
using System.Data.Entity;

using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace Hotel.Dependencies
{
    class TransactionInterceptionBehavior< TDbContext> : IInterceptionBehavior where TDbContext : DbContext
    {
        public IMethodReturn Invoke ( IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext )
        {
            dbContext.Database.BeginTransaction();

            var result = getNext()( input, getNext );
            if ( result.Exception != null )
                dbContext.Database.CurrentTransaction.Rollback();

            else
            {
                dbContext.SaveChanges();
                dbContext.Database.CurrentTransaction.Commit();
            }

            return result;
        }

        public IEnumerable< Type > GetRequiredInterfaces ()
        {
            return Type.EmptyTypes;
        }

        public bool WillExecute
        {
            get { return true; }
        }

        [ Dependency ]
        protected TDbContext dbContext { get; set; }
    }
}
