using System;
using System.Net;
using System.Web.Http;
using FibancciService.Models;
using FibancciService.RabbitMQ;
using FibancciService.FibonacciAlgorithm;
using System.Numerics;

namespace FibancciService.Controllers
{
    public class FibonacciRangeController : ApiController
    {
        // GET: Fibonacci
        [HttpPost]
        public IHttpActionResult GenerateFibonacciSeries([FromBody] FibonacciRange req)
        {
           
            try
            {
                RabbitMQClient client = new RabbitMQClient();
                
                GenerateFibonacciPosRange fr = new GenerateFibonacciPosRange();
                BigInteger[] results = new BigInteger[req.EndPosition - req.StartPosition + 1];
                results = fr.GetSeriesBetweenPosition(req.StartPosition, req.EndPosition);
                foreach ( decimal num in results)
                {
                    req.result += num.ToString() + ", ";
                }
                //req.result = results.ToString();
                client.SendFibonacciSeries(req);

                client.Close();
            }
            catch (Exception)
            {
                return StatusCode(HttpStatusCode.BadRequest);
            }

            return Ok(req);
        }
    }
}