using Microsoft.EntityFrameworkCore;
using Migracao.Data;
using System;

namespace Migracao
{
    class Program
    {
        static void Main(string[] args)
        {
            using var db = new ApplicationContext();
            db.Database.Migrate();
        }
    }
}
