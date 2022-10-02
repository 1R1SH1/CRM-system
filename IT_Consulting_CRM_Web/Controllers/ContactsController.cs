using IT_Consulting_CRM_Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace IT_Consulting_CRM_Web.Controllers
{
    public class ContactsController : Controller
    {
        private readonly ILogger<ContactsController> _logger;

        public static List<Contacts> Contact = new List<Contacts>();
        public static Contacts Selected { get; set; }

        public static Contacts Template { get; set; }

        public ContactsController(ILogger<ContactsController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            Contact = JsonConvert.DeserializeObject<List<Contacts>>(CRUD.Read("Contacts").ToString());
            return View();
        }

        public IActionResult UpdateContact()
        {
            CRUD.Update("Contacts", JsonConvert.SerializeObject(new Contacts()));
            return RedirectToAction("Index", "Contacts");
        }

        public IActionResult AddContact(string name,
                                        string surName,
                                        string lastName,
                                        string phone,
                                        string eMail,
                                        string address,
                                        string contactInformation)
        {
            Template.Name = name;
            Template.SurName = surName;
            Template.LastName = lastName;
            Template.Phone = phone;
            Template.EMail = eMail;
            Template.Address = address;
            Template.ContactsInformation = contactInformation;
            if (Template.Id == 0)
            {
                CRUD.Create("Contacts", JsonConvert.SerializeObject(Template));
            }
            return RedirectToAction("Index", "Contacts");
        }

        public IActionResult Delete(int id)
        {
            CRUD.Delete($"Contacts/{id}");
            return RedirectToAction("Index", "Contacts");
        }
        public IActionResult Edit(int id)
        {
            if (id != 0)
            {
                Selected = Contact.Find(u => u.Id == id);
            }
            else
            {
                Selected = new Contacts();
            }
            return View();
        }
        [HttpPost]
        public IActionResult UpdateContact(Contacts contact,
                                           string name,
                                           string surName,
                                           string lastName,
                                           string phone,
                                           string eMail,
                                           string address,
                                           string contactInformation)
        {
            Template.Name = name;
            Template.SurName = surName;
            Template.LastName = lastName;
            Template.Phone = phone;
            Template.EMail = eMail;
            Template.Address = address;
            Template.ContactsInformation = contactInformation;
            CRUD.Update($"Contacts?id={Selected.Id}&description={contact}", JsonConvert.SerializeObject(Template));

            return RedirectToAction("Index", "Contacts");
        }
    }
}
