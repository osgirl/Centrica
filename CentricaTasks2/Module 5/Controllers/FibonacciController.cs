using System;
using System.Net;
using System.Web.Http;
using FibancciService.Models;
using FibancciService.RabbitMQ;
using FibancciService.FibonacciAlgorithm;
using System.Numerics;

namespace FibancciService.Controllers
{
    public class FibonacciController : ApiController
    {
        // GET: Fibonacci
        [HttpPost]
        public IHttpActionResult GetFibonacciSeries([FromBody] Fibonacci req)
        {

            try
            {
                using (FibonacciContext context = new FibonacciContext())
                {
                    
                    Fibonacci fib = context.Fibonaccis.Find(req.Position + 1);
                    if (fib == null)
                    {
                        //RabbitMQClient client = new RabbitMQClient();
                        FibonacciCalcNumAtPos fr = new FibonacciCalcNumAtPos();
                        BigInteger result = fr.GetNumberAtPosition(req.Position);
                        req.FibonacciNumber = result.ToString();
                        fr.SaveSeries(context);
                      
                        //client.SendFibonacciNum(req);

                         
                        //client.Close();
                    }
                    else
                    {
                        req.FibonacciNumber = fib.FibonacciNumber;
                    }
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