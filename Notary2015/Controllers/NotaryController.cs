using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Notary2015.Controllers
{
    public class NotaryController : ApiController
    {
        private notarysEntities db = new notarysEntities();

        // GET api/Notary
        public IEnumerable<notary> Getnotaries()
        {
            db.Configuration.ProxyCreationEnabled = false;
            //var notaries = db.notaries.Include(n => n.district).Include(n => n.office);
            return db.notaries.AsEnumerable();
        }

        // GET api/Notary/5
        public notary Getnotary(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            notary notary = db.notaries.Find(id);
            if (notary == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return notary;
        }

        // PUT api/Notary/5
        public HttpResponseMessage Putnotary( notary notary)
        {
                db.notaries.Add(notary);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, notary);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = notary.idNotary }));
                return response;
         
        }

        // POST api/Notary
        public HttpResponseMessage Postnotary(notary notary)
        {
            db.Configuration.ProxyCreationEnabled = false;

            db.Entry(notary).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // DELETE api/Notary/5
        public HttpResponseMessage Deletenotary(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            notary notary = db.notaries.Find(id);
            if (notary == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.notaries.Remove(notary);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, notary);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}