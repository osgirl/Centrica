    using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using FibancciService.Models;


namespace FibancciService.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using FibancciService.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Fibonacci>("Fibonaccis");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class FibonaccisController : ODataController
    {
        private FibonacciContext db = new FibonacciContext();

        // GET: odata/Fibonaccis
        [EnableQuery]
        public IQueryable<Fibonacci> GetFibonaccis()
        {
            return db.Fibonaccis;
        }

        // GET: odata/Fibonaccis(5)
        [EnableQuery]
        public SingleResult<Fibonacci> GetFibonacci([FromODataUri] int key)
        {
            return SingleResult.Create(db.Fibonaccis.Where(fibonacci => fibonacci.Id == key));
        }

        // PUT: odata/Fibonaccis(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<Fibonacci> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Fibonacci fibonacci = db.Fibonaccis.Find(key);
            if (fibonacci == null)
            {
                return NotFound();
            }

            patch.Put(fibonacci);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FibonacciExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(fibonacci);
        }

        // POST: odata/Fibonaccis
        public IHttpActionResult Post(Fibonacci fibonacci)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Fibonaccis.Add(fibonacci);
            db.SaveChanges();

            return Created(fibonacci);
        }

        // PATCH: odata/Fibonaccis(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<Fibonacci> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Fibonacci fibonacci = db.Fibonaccis.Find(key);
            if (fibonacci == null)
            {
                return NotFound();
            }

            patch.Patch(fibonacci);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FibonacciExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(fibonacci);
        }

        // DELETE: odata/Fibonaccis(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            Fibonacci fibonacci = db.Fibonaccis.Find(key);
            if (fibonacci == null)
            {
                return NotFound();
            }

            db.Fibonaccis.Remove(fibonacci);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FibonacciExists(int key)
        {
            return db.Fibonaccis.Count(e => e.Id == key) > 0;
        }
    }
}
