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
    public class LicenseController : ApiController
    {
        private notarysEntities db = new notarysEntities();

        // GET api/License
        public IEnumerable<license> Getlicenses()
        {
            return db.licenses.AsEnumerable();
        }

        // GET api/License/5
        public license Getlicense(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            license license = db.licenses.Find(id);
            if (license == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return license;
        }

        // PUT api/License/5
        public HttpResponseMessage Putlicense(int id, license license)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != license.idLicense)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(license).State = EntityState.Modified;

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

        // POST api/License
        public HttpResponseMessage Postlicense(license license)
        {
            if (ModelState.IsValid)
            {
                db.licenses.Add(license);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, license);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = license.idLicense }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/License/5
        public HttpResponseMessage Deletelicense(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            license license = db.licenses.Find(id);
            if (license == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.licenses.Remove(license);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, license);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}