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
    public class DistrictController : ApiController
    {
        private notarysEntities db = new notarysEntities();

        // GET api/District
        public IEnumerable<district> Getdistricts()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.districts.AsEnumerable();
        }
        // GET api/District/5
        public district Getdistrict(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            district District = db.districts.Find(id);
            if (District == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return District;
        }

        // PUT api/District/5
        public HttpResponseMessage Putdistrict(district district)
        {
            if (ModelState.IsValid)
            {
                db.districts.Add(district);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, district);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = district.idDistrict }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            
        }

        // POST api/District
        public HttpResponseMessage Postdistrict(district district)
        {
            

            db.Entry(district).State = EntityState.Modified;

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

        // DELETE api/District/5
        public HttpResponseMessage Deletedistrict(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            district district = db.districts.Find(id);
            if (district == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.districts.Remove(district);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, district);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}