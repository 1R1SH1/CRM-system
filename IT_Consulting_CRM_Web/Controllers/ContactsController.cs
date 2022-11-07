using IT_Consulting_CRM_Web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace IT_Consulting_CRM_Web.Controllers
{
    public class ContactsController : Controller
    {

        private static List<Contacts> _contact = new List<Contacts>();

        public IActionResult Contact(Contacts contacts)
        {
                _contact = JsonConvert.DeserializeObject<List<Contacts>>(CRUD.Read("Contacts"));
                //var c = _contact.FirstOrDefault();
                return View(_contact);
        }

        [HttpGet]
        public IActionResult AddContact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddContact(Contacts contacts)
        {
            if (contacts.Id == 0)
            {
                CRUD.Create("Contacts", JsonConvert.SerializeObject(contacts));
            }
            return RedirectToAction("Contact", "Contacts");
        }

        [HttpGet]
        public IActionResult UpdateContact()
        {
            return View();
        }

        public IActionResult UpdateContact(Contacts contacts,
                                        string Image,
                                        string name,
                                        string surName,
                                        string lastName,
                                        string phone,
                                        string eMail,
                                        string address,
                                        string contactInformation)
        {
            contacts.Image = Image;
            contacts.Name = name;
            contacts.SurName = surName;
            contacts.LastName = lastName;
            contacts.Phone = phone;
            contacts.EMail = eMail;
            contacts.Address = address;
            contacts.ContactsInformation = contactInformation;
            if (contacts.Id == 0)
            {
                CRUD.Create("Contacts", JsonConvert.SerializeObject(contacts));
            }
            else
            {
                CRUD.Update("Contacts", JsonConvert.SerializeObject(contacts));
            }
            return RedirectToAction("Contact", "Contacts");
        }

        public IActionResult Delete(int id)
        {
            CRUD.Delete($"Contacts/{id}");
            return RedirectToAction("Contact", "Contacts");
        }
    }
}
