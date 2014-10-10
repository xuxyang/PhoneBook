using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using PhoneBookIdeal.Models;

namespace PhoneBookIdeal.Service
{
    public class PhoneService: IPhoneService
    {
        private PhoneDBContext db;

        public PhoneService(PhoneDBContext entity)
        {
            db = entity;
        }

        public List<Phone> GetAllPhones()
        {
            return db.Phones.ToList();
        }

        public Phone GetPhone(int id)
        {
            return db.Phones.Find(id);
        }

        public void UpdatePhone(Phone phone)
        {
            db.Entry(phone).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void AddPhone(Phone phone)
        {
            db.Phones.Add(phone);
            db.SaveChanges();
        }

        public void DeletePhone(Phone phone)
        {
            db.Phones.Remove(phone);
            db.SaveChanges();
        }

        public bool PhoneExists(int id)
        {
            return db.Phones.Count(e => e.ID == id) > 0;
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}