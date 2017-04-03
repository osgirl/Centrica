using System;
using System.Net;
using System.Web.Http;
using FibancciService.Models;
using FibancciService.RabbitMQ;
using FibancciService.FibonacciAlgorithm;
using System.Numerics;
using System.Linq;

namespace FibancciService.Controllers
{
    public class FibonacciRangeController : ApiController
    {
        // GET: Fibonacci
        [HttpPost]
        public IHttpActionResult GetFibonacciSeries([FromBody] FibonacciRange req)
        {

            try
            {

                //RabbitMQClient client = new RabbitMQClient();
                using (FibonacciContext context = new FibonacciContext())
                {
                    Fibonacci fibStart = context.Fibonaccis.Find(req.StartPosition + 1);
                    Fibonacci fibEnd = context.Fibonaccis.Find(req.EndPosition + 1);
                    GenerateFibonacciPosRange fr = new GenerateFibonacciPosRange();
                    BigInteger[] results = new BigInteger[req.EndPosition - req.StartPosition + 1];
                   

                    if (fibStart != null && fibEnd != null)
                    {
                        
                        IQueryable<Fibonacci> rtn = from temp in context.Fibonaccis select temp;
                        var list = rtn.ToList();

                        var seriesToAdd = list.GetRange(req.StartPosition, req.EndPosition - req.StartPosition + 1);
                        
                        //filling in the cache for future use.
                        //creating the response.
                        seriesToAdd.ForEach(fn =>
                                                  { 
                                                      fr.fibSeries.Add(BigInteger.Parse(fn.FibonacciNumber));
                                                      req.FibonacciSeries += fn.FibonacciNumber + ", ";
                                                  });

                        
                       
                        
                    }
                    else if (fibStart != null && fibEnd == null)
                    {
                        IQueryable<Fibonacci> rtn = from temp in context.Fibonaccis select temp;
                        var list = rtn.ToList();
                        int fibNums = context.Fibonaccis.Count();

                        
                        list.ForEach(fn =>
                        {
                            fr.fibSeries.Add(BigInteger.Parse(fn.FibonacciNumber));                            
                        });

                        results = fr.GetSeriesBetweenPosition(req.StartPosition, req.EndPosition);
                        foreach (BigInteger num in results)
                        {
                            req.FibonacciSeries += num.ToString() + ", ";
                        }

                        fr.SaveSeries(context);
                    }
                    else
                    {
                        results = fr.GetSeriesBetweenPosition(req.StartPosition, req.EndPosition);

                        results.ToList().ForEach(fn =>
                        {
                            req.FibonacciSeries += fn.ToString(); 
                        });

                        fr.SaveSeries(context);
                    }

                    //req.result = results.ToString();
                    //client.SendFibonacciSeries(req);

                    //client.Close();
                }
            }
            catch (Exception)
            {
                return StatusCode(HttpStatusCode.BadRequest);
            }

            return Ok(req);
        }
    }
}