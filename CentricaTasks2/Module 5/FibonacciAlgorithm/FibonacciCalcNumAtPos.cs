using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Numerics;
using FibancciService.Models;

namespace FibancciService.FibonacciAlgorithm
{
    public class FibonacciCalcNumAtPos
    {
        public static List<BigInteger> fibSeries = new List<BigInteger>();
        public BigInteger GetNumberAtPosition(int pos)        
        {
            BigInteger a = 0, b = 1, c = 1;
            if (pos == 0)
            {
                return 0;
            }

            if (pos < 0)
                return -1;

            if (pos == 1)
            {
                return 1;
            }

            if (fibSeries.Count >= pos)
            {
                return fibSeries[pos];
            }
            else if (fibSeries.Count > 0 && fibSeries.Count < pos)
            {
                int startPos = fibSeries.Count;
                a = fibSeries[startPos - 2];
                b = fibSeries[startPos - 1];
                c = 0;
                for (int i = startPos; i <= pos; i++)
                {
                    c = a + b;
                    fibSeries.Add(c);
                    a = b;
                    b = c;

                }
                return c;
            }
            else
            {
                a = 0; b = 1; c = 0;
                fibSeries.Add(a);
                fibSeries.Add(b);
               
                for (int i = 2; i <= pos; i++)
                {
                    c = a + b;
                    fibSeries.Add(c);
                    a = b;
                    b = c;

                }
                return c;
            }
        }

        public void SaveSeries(FibonacciContext context)
        {
            int startPos = 0;
            int endPos = 0;
            if (context.Fibonaccis!=null && context.Fibonaccis.Count() < fibSeries.Count())
            {
                startPos = context.Fibonaccis.Count();
                endPos = fibSeries.Count();
                int y = endPos - startPos;
                var seriesToAdd = fibSeries.GetRange(startPos, y);
                int i = startPos;
                seriesToAdd.ForEach(fn =>
                                          context.Fibonaccis.Add(new Fibonacci() { FibonacciNumber = fn.ToString(), Position = i++ }));
                        context.SaveChanges();
            }
        }
    }
}