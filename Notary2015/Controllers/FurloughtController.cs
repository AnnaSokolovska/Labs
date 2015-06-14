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
    public class FurloughtController : ApiController
    {
        private notarysEntities db = new notarysEntities();

        // GET api/Furlought
        public IEnumerable<furlought> Getfurloughts()
        {
            return db.furloughts.AsEnumerable();
        }

        // GET api/Furlought/5
        public furlought Getfurlought(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            furlought furlought = db.furloughts.Find(id);
            if (furlought == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return furlought;
        }

        // PUT api/Furlought/5
        public HttpResponseMessage Putfurlought(int id, furlought furlought)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != furlought.idFurlought)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(furlought).State = EntityState.Modified;

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

        // POST api/Furlought
        public HttpResponseMessage Postfurlought(furlought furlought)
        {
            if (ModelState.IsValid)
            {
                db.furloughts.Add(furlought);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, furlought);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = furlought.idFurlought }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Furlought/5
        public HttpResponseMessage Deletefurlought(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            furlought furlought = db.furloughts.Find(id);
            if (furlought == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.furloughts.Remove(furlought);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, furlought);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}