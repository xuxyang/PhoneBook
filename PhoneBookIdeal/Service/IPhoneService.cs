using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhoneBookIdeal.Models;

namespace PhoneBookIdeal.Service
{
    public interface IPhoneService
    {
        List<Phone> GetAllPhones();
        Phone GetPhone(int id);
        void UpdatePhone(Phone phone);
        void AddPhone(Phone phone);
        void DeletePhone(Phone phone);
        bool PhoneExists(int id);
        void Dispose();
    }
}
