using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.ModelBinding;
using System.Web.OData;
using System.Web.OData.Query;
using System.Web.OData.Routing;
using MyLabProject.Models.db.people.model;
using MyLabProject.Models.dbmodels;
using MyLabProject.FilterParser;

namespace MyLabProject.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.OData.Builder;
    using System.Web.OData.Extensions;
    using MyLabProject.Models.db.people.model;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Person>("People");
    builder.EntitySet<PersonDynamics>("PeopleDynamicFields"); 
    config.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class PeopleController : ODataController
    {
        private PeopleContext db = new PeopleContext();

        [EnableQuery]
        [HttpGet]
        public async Task<IHttpActionResult> GetPeopleFunction(ODataQueryOptions<Person> options)
        {
            db.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
            MyFilterValueSupplier<object> filterSupplier = new MyFilterValueSupplier<object>();
            List<FilterValue> filters = new List<FilterValue>();
            if (options.Filter != null)
            {
                options.Filter.FilterClause.Expression.Accept(filterSupplier);
                filters = filterSupplier.filterValueList;
            }
            var dyns = (from dynamics in db.PeopleDynamicFields
                        select dynamics);

            //We skip $select. This will put our result in memory.
            IQueryable results = options.ApplyTo(db.People.AsQueryable());
            var t = await results.ToListAsync();
            return Ok(t);
        }
        // GET: odata/People
        [EnableQuery]
        [HttpGet]
        public async Task<IQueryable<Person>> GetPeople(ODataQueryOptions<Person> options)
        {
            db.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
            MyFilterValueSupplier<object> filterSupplier = new MyFilterValueSupplier<object>();
            List<FilterValue> filters = new List<FilterValue>();
            if (options.Filter != null)
            {
                options.Filter.FilterClause.Expression.Accept(filterSupplier);
                filters = filterSupplier.filterValueList;
            }
            var dyns = (from dynamics in db.PeopleDynamicFields
                       select dynamics);

            //We skip $select. This will put our result in memory.
            IQueryable results = options.ApplyTo(db.People.AsQueryable());
            var t = await results.ToListAsync();
            return results as IQueryable<Person>;
        }

        // GET: odata/People(5)
        [EnableQuery]
        public SingleResult<Person> GetPerson([FromODataUri] Guid key)
        {
            return SingleResult.Create(db.People.Where(person => person.Id == key));
        }

        // PUT: odata/People(5)
        public async Task<IHttpActionResult> Put([FromODataUri] Guid key, Delta<Person> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Person person = await db.People.FindAsync(key);
            if (person == null)
            {
                return NotFound();
            }

            patch.Put(person);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(person);
        }

        // POST: odata/People
        public async Task<IHttpActionResult> Post(Person person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.People.Add(person);
            await db.SaveChangesAsync();

            return Created(person);
        }

        // PATCH: odata/People(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] Guid key, Delta<Person> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Person person = await db.People.FindAsync(key);
            if (person == null)
            {
                return NotFound();
            }

            patch.Patch(person);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(person);
        }

        // DELETE: odata/People(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] Guid key)
        {
            Person person = await db.People.FindAsync(key);
            if (person == null)
            {
                return NotFound();
            }

            db.People.Remove(person);
            await db.SaveChangesAsync();

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

        private bool PersonExists(Guid key)
        {
            return db.People.Count(e => e.Id == key) > 0;
        }
    }
}
