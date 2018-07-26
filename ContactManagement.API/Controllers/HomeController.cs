using ContactManagement.WebAPI.Models;
using System.Linq;
using System.Web.Mvc;

namespace ContactManagement.WebAPI.Controllers
{
    public class HomeController : Controller
    {
        APIClient _APIClient = new APIClient();
        public ActionResult Index()
        {
            var errorMsg = TempData["ErrorMsg"];
            if (errorMsg != null)
            {
                ViewBag.ErrorMsg = errorMsg;
            }
            var allContacts = _APIClient.GetAllContacts();
            if (allContacts == null)
            {
                allContacts = Enumerable.Empty<ContactDetailModel>();
                ModelState.AddModelError(string.Empty, "Something went wrong.");
            }
            else if(allContacts.Count() == 0)
            {
                ModelState.AddModelError(string.Empty, "No Records Found.");
            }
            return View(allContacts);
        }

        public ActionResult Edit(int _Id)
        {
            ContactDetailModel _ContactDetailModel = null;
            _ContactDetailModel = _APIClient.GetContactById(_Id);
            if (_ContactDetailModel == null)
            {
                TempData["ErrorMsg"] = "No Matching Record Found";
                return RedirectToAction("Index");
            }
            return View(_ContactDetailModel);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ContactDetailModel _ContactDetailModel)
        {
            if (!_APIClient.CreateContact(_ContactDetailModel))
            {
                return View("Error");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]

        public ActionResult Edit(ContactDetailModel _ContactDetailModel)
        {
            if(!_APIClient.UpdateContact(_ContactDetailModel))
            {
                return View("Error");
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int _Id)
        {
            if(!_APIClient.DeleteContact(_Id))
            {
                return View("Error");
            }
            return RedirectToAction("Index");
        }
    }
}
