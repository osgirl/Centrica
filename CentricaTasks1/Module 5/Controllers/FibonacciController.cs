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
        public IHttpActionResult MakePayment([FromBody] Fibonacci req)
        {

            try
            {
                RabbitMQClient client = new RabbitMQClient();
                FibonacciCalcNumAtPos fr = new FibonacciCalcNumAtPos();
                BigInteger result = fr.GetNumberAtPosition(req.Position);
                req.res = result;

                client.SendFibonacciNum(req);


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