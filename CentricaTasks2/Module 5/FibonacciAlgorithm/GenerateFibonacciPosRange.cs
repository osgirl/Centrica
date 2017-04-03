using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Web;
using FibancciService.Models;

namespace FibancciService.FibonacciAlgorithm
{
    public class GenerateFibonacciPosRange
    {

        public List<BigInteger> fibSeries = new List<BigInteger>();
        public BigInteger[]  GetSeriesBetweenPosition(int pos1, int pos2)
        {
            BigInteger[] res = new BigInteger[pos2 - pos1 + 1];
            int j=0;
            BigInteger a =0 , b =1, c =1;
            if (fibSeries.Count() >= pos1 && fibSeries.Count() >= pos2 )
            {
                //data already exists;
                j = 0;
              
            }
            else if (fibSeries.Count() >= pos1 && fibSeries.Count() < pos2)
            {
                // eg: fibonacci series exits from F0 to F30, but pos1 is 25 and pos2 is 40
                //in this case startPos 30
                // a = F28, b= F27
                int startPos = fibSeries.Count();
                a = fibSeries[startPos - 2];
                b = fibSeries[startPos - 1];
                c = 0;
                j = 0;
                for (int i = startPos; i <= pos2; i++)
                {
                        res[j] = a+b;
                        j++;
                        a = b;
                        b = c;
                        c = res[j];
                        fibSeries.Add(c);                   
                }

            }
            //When the fibinacci store is empty.
            else
            {
                fibSeries.Add(a);
                fibSeries.Add(b);
                
                for (int k = 2; k <= pos2; k++)
                {
                    c = a + b;
                    fibSeries.Add(c);
                    a = b;
                    b = c;
                }
            }

            res = fibSeries.GetRange(pos1, pos2 - pos1 + 1).ToArray();
            // return the asked data
            //j = 0;
            //for (int i = pos1; i <= pos2; i++)
            //{                
            //    res[j] = fibSeries[i];
            //    j++;
            //}

            return res;
        }

        public void SaveSeries(FibonacciContext context)
        {
            int startPos = 0;
            int endPos = 0;
            if (context.Fibonaccis != null && context.Fibonaccis.Count() < fibSeries.Count())
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