using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace FibancciService.Models
{
    public class FibonacciServiceDbContextInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges <FibonacciContext>
    {
        protected override void Seed(FibonacciContext context)
        {
            var fibs = new List<Fibonacci>() 
            {
                new Fibonacci() { Position = 0, FibonacciNumber = "0"},
                new Fibonacci() { Position = 1, FibonacciNumber = "1"},
                new Fibonacci() { Position = 2, FibonacciNumber = "1"}
            };

            fibs.ForEach(fn => context.Fibonaccis.Add(fn));
            context.SaveChanges();
            base.Seed(context);
        }
    }
}