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

namespace MyLabProject.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.OData.Builder;
    using System.Web.OData.Extensions;
    using MyLabProject.Models.db.people.model;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<PersonDynamics>("PersonDynamics");
    builder.EntitySet<Person>("People"); 
    config.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class PersonDynamicsController : ODataController
    {
        private PeopleContext db = new PeopleContext();

        // GET: odata/PersonDynamics
        [EnableQuery]
        public IQueryable<PersonDynamics> GetPersonDynamics()
        {
            return db.PeopleDynamicFields;
        }

        // GET: odata/PersonDynamics(5)
        [EnableQuery]
        public SingleResult<PersonDynamics> GetPersonDynamics([FromODataUri] Guid key)
        {
            return SingleResult.Create(db.PeopleDynamicFields.Where(personDynamics => personDynamics.Id == key));
        }

        // PUT: odata/PersonDynamics(5)
        public async Task<IHttpActionResult> Put([FromODataUri] Guid key, Delta<PersonDynamics> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            PersonDynamics personDynamics = await db.PeopleDynamicFields.FindAsync(key);
            if (personDynamics == null)
            {
                return NotFound();
            }

            patch.Put(personDynamics);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonDynamicsExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(personDynamics);
        }

        // POST: odata/PersonDynamics
        public async Task<IHttpActionResult> Post(PersonDynamics personDynamics)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PeopleDynamicFields.Add(personDynamics);
            await db.SaveChangesAsync();

            return Created(personDynamics);
        }

        // PATCH: odata/PersonDynamics(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] Guid key, Delta<PersonDynamics> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            PersonDynamics personDynamics = await db.PeopleDynamicFields.FindAsync(key);
            if (personDynamics == null)
            {
                return NotFound();
            }

            patch.Patch(personDynamics);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonDynamicsExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(personDynamics);
        }

        // DELETE: odata/PersonDynamics(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] Guid key)
        {
            PersonDynamics personDynamics = await db.PeopleDynamicFields.FindAsync(key);
            if (personDynamics == null)
            {
                return NotFound();
            }

            db.PeopleDynamicFields.Remove(personDynamics);
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

        private bool PersonDynamicsExists(Guid key)
        {
            return db.PeopleDynamicFields.Count(e => e.Id == key) > 0;
        }
    }
}
