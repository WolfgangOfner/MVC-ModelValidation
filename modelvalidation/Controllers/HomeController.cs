using System;
using System.Web.Mvc;
using ModelValidation.Models;

namespace ModelValidation.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult RegisterCustomer()
        {
            // set Birthday to prevent displaying 01.01.0001
            return View(new Customer { Birthday = new DateTime(1980, 1, 1) });
        }
        
        [HttpPost]
        public ViewResult RegisterCustomer(Customer customer)
        {
            if (string.IsNullOrEmpty(customer.Name))
            {
                ModelState.AddModelError("Name", "Please enter your name");
            }

            if (ModelState.IsValidField("Birthday") && DateTime.Now < customer.Birthday)
            {
                ModelState.AddModelError("Birthday", "Please enter a date in the past");
            }

            if (!customer.TermsAndConditionAccepted)
            {
                ModelState.AddModelError("TermsAndConditionAccepted", "You must accept the terms");
            }

            if (ModelState.IsValidField("Name") && ModelState.IsValidField("Birthday") && customer.Name == "Wolfgang"
                && customer.Birthday.Date == DateTime.Now.Date.Add(new TimeSpan(-1, 0, 0, 0)))
            {
                ModelState.AddModelError("", "Wolfgang's birthday can't be yesterday");
            }

            return ModelState.IsValid ? View("RegisterComplete", customer) : View();
        }
        
        public JsonResult ValidateName(string Name)
        {
            return Name == "Wolfgang" ? 
                Json("The customer name can't be Wolfgang", JsonRequestBehavior.AllowGet) 
                : Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}