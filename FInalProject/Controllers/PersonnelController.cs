using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Assignment7MVC.Models;

namespace Assignment7MVC.Controllers
{
    public class PersonnelController : Controller
    {
        private Community_AssistEntities1 db = new Community_AssistEntities1();
        // GET: Personnel
        public ActionResult Index()
        {
            var peeps = from p in db.People
                        from pa in p.PersonAddresses
                        from c in p.Contacts
                        where c.ContactTypeKey == 1
                        select new
                        {
                            p.PersonKey,
                            p.PersonLastName,
                            p.PersonFirstName,
                            p.PersonEmail,
                            pa.PersonAddressApt,
                            pa.PersonAddressStreet,
                            pa.PersonAddressCity,
                            pa.PersonAddressState,
                            pa.PersonAddressZip,
                            c.ContactNumber
                        };
            List<PersonLite> peepsList = new List<PersonLite>();
            foreach(var p in peeps)
            {
                PersonLite pl = new PersonLite();
                pl.PersonKey = (int) p.PersonKey;
                pl.LastName = p.PersonLastName;
                pl.FirstName = p.PersonFirstName;
                pl.Email = p.PersonEmail;
                pl.Apartment = p.PersonAddressApt;
                pl.Street = p.PersonAddressStreet;
                pl.City = p.PersonAddressCity;
                pl.State = p.PersonAddressState;
                pl.Zipcode = p.PersonAddressZip;
                peepsList.Add(pl);
            }

            return View(peepsList);
        }
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Create([Bind(Include = "LastName, FirstName, Email, Apartment, Street, City, State, Zipcode")] PersonLite pl)
        {
            int result = db.usp_Register(pl.LastName, pl.FirstName, pl.Email, pl.Password, pl.Apartment, pl.Street,
                pl.City, pl.State, pl.Zipcode, pl.HomePhone, pl.WorkPhone);

            return View();               
        }

    }
}