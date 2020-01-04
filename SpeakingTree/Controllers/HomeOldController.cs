using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SpeakingTree.Models;
using SpeakingTree.Facade;
using SpeakingTree.Extensions;

namespace SpeakingTree.Controllers
{
    public class HomeOldController : Controller
    {
        HomeFacade homeFacade = new HomeFacade();

        public ActionResult Test()
        {
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }

        #region Login & Register

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            int role = homeFacade.ValidateUser(user.Id, user.Password);
            if (role > 0)
            {
                Session["Role"] = role;
                Session["UserId"] = user.Id;
                Session["Username"] = homeFacade.GetUserName(user.Id);
                switch (role)
                {
                    case 1: return RedirectToAction("SuperAdmin", "Admin");
                    case 2: return RedirectToAction("Index", "Admin");
                    case 3: return RedirectToAction("Index", "School");
                    case 4: return RedirectToAction("Index", "Student");
                }
            }
            else
            {
                ViewBag.LoginError = "Username OR Password is not valid";
            }
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            User user = new User();
            var roles = homeFacade.GetRoles();
            user.lstRole = new SelectList(roles.ToList(), "ID", "Name");

            var states = homeFacade.GetStates();
            user.lstState = new SelectList(states.ToList(), "ID", "Name");

            var districts = homeFacade.GetDistricts("");
            user.lstDistrict = new SelectList(districts.ToList(), "ID", "Name");

            user.UserType = "School";
            user.Id = " ";
            user.Password = " ";
            //user.school.ExamOption = "On";

            return View(user);
        }

        [HttpPost]
        public ActionResult Register(User user)
        {
            //if (ModelState.IsValid)
            {
                if (homeFacade.SaveUser(ViewModelExtension.ToBusinessUser(user), out string userid, out string password) > 0)
                {
                    //Send email
                    //Helpers.EmailHelper e = new Helpers.EmailHelper();
                    //e.SendEmailToUser(user.EmailId, "uid", "", true, "", "");
                    return RedirectToAction("RegistrationSuccess", new { userId = userid, password = user.Password });
                }
            }

            var states = homeFacade.GetStates();
            user.lstState = new SelectList(states.ToList(), "ID", "Name");

            var districts = homeFacade.GetDistricts("");
            user.lstDistrict = new SelectList(districts.ToList(), "ID", "Name");



            return View(user);
        }

        [HttpGet]
        public ActionResult RegistrationSuccess(string userId, string password)
        {
            ViewBag.RegistrationId = userId;
            ViewBag.Password = password;

            return View();
        }

        public JsonResult GetDistrictsByState(string stateId)
        {
            List<SelectListItem> states = new List<SelectListItem>();
            var districts = homeFacade.GetDistricts(stateId);
            if (districts != null)
                return Json(new SelectList(districts.ToList(), "ID", "Name"));
            else
                return null;
        }

        [HttpGet]
        public ActionResult Logout()
        {
            Session.Clear();
            TempData["LogoutMessage"] = "You have successfully logout";
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult ForgotPassword(string errMsg = "")
        {
            ViewBag.ForgotPasswordError = errMsg;
            return View();
        }
        [HttpPost]
        public ActionResult ForgotPassword(User user)
        {

            return RedirectToAction("ShowPassword", new { user.Id, user.ContactNo });
        }

        [HttpGet]
        public ActionResult ShowPassword(string id, string ContactNo)
        {
            User user = new User();
            user.Password = homeFacade.GetUserPassword(id, ContactNo);
            if (string.IsNullOrEmpty(user.Password))
            {

                return RedirectToAction("ForgotPassword", new { errMsg = "Password could not recovered with the given username and contact number" });
            }
            return View(user);
        }

        [HttpGet]
        public ActionResult ResetPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ResetPassword(User user)
        {
            return View();
        }

        #endregion

        #region Contact us

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public ActionResult Enquiry()
        {
            return View();

        }

        [HttpPost]
        public ActionResult Enquiry(Enquiry enquiry)
        {
            if (homeFacade.SaveEnquiry(ViewModelExtension.ToBusinessEnquiry(enquiry)) > 0)
            {
                //Send email
            }
            return View(enquiry);
        }

        #endregion

        #region Menu items

        [HttpGet]
        public ActionResult GrammarQuest()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Gallery()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Downloads()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Careers()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        #endregion
    }
}