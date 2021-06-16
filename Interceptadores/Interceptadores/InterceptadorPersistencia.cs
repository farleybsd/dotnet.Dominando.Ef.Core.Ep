using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interceptadores.Interceptadores
{
    public class InterceptadorPersistencia : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(
           DbContextEventData eventData,
           InterceptionResult<int> result)
        {

            System.Console.WriteLine(eventData.Context.ChangeTracker.DebugView.LongView);

            return result;
        }
    }
}
