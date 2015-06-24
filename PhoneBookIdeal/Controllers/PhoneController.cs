using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using PhoneBookIdeal.Models;
using PhoneBookIdeal.Service;

namespace PhoneBookIdeal.Controllers
{
    public class PhoneController : ApiController
    {
        private IPhoneService service;

        public PhoneController(IPhoneService phoneService)
        {
            service = phoneService;
        }

        // GET api/Phone
        public IQueryable<Phone> GetPhones()
        {
            return service.GetAllPhones().AsQueryable();
        }

        // GET api/Phone/5
        [ResponseType(typeof(Phone))]
        public IHttpActionResult GetPhone(int id)
        {
            Phone phone = service.GetPhone(id);
            if (phone == null)
            {
                return NotFound();
            }

            return Ok(phone);
        }

        // PUT api/Phone/5
        public IHttpActionResult PutPhone(int id, Phone phone)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != phone.ID)
            {
                return BadRequest();
            }

            try
            {
                service.UpdatePhone(phone);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!service.PhoneExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/Phone
        [ResponseType(typeof(Phone))]
        public IHttpActionResult PostPhone(Phone phone)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            service.AddPhone(phone);

            return CreatedAtRoute("DefaultApi", new { id = phone.ID }, phone);
        }

        // DELETE api/Phone/5
        [ResponseType(typeof(Phone))]
        public IHttpActionResult DeletePhone(int id)
        {
            Phone phone = service.GetPhone(id);
            if (phone == null)
            {
                return NotFound();
            }

            service.DeletePhone(phone);

            return Ok(phone);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                service.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}