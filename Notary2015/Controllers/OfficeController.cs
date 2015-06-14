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
    public class OfficeController : ApiController
    {
        private notarysEntities db = new notarysEntities();

        // GET api/Office
        public IEnumerable<office> Getoffices()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.offices.AsEnumerable();
        }

        // GET api/Office/5
        public office Getoffice(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            office office = db.offices.Find(id);
            if (office == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return office;
        }

        // PUT api/Office/5
        public HttpResponseMessage Putoffice(int id, office office)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != office.idOffice)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(office).State = EntityState.Modified;

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

        // POST api/Office
        public HttpResponseMessage Postoffice(office office)
        {
            if (ModelState.IsValid)
            {
                db.offices.Add(office);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, office);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = office.idOffice }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Office/5
        public HttpResponseMessage Deleteoffice(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            office office = db.offices.Find(id);
            if (office == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.offices.Remove(office);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, office);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}