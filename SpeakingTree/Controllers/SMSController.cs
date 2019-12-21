using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nexmo.Api;
//using System.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;



namespace SpeakingTree.Controllers
{
    public class SMSController : Controller
    {

        //public Nexmo.Api.SMS Client Client { get; set; }

        // GET: SMS
        public ActionResult Index()
        {
            return View();
        }

        [System.Web.Mvc.HttpGet]
        public ActionResult Send()
        {
            return View();
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Send(string to, string text)
        {
            try
            {
                var creds = new Nexmo.Api.Request.Credentials
                {
                    ApiKey = "730dedeb",
                    ApiSecret = "o3L2mfmP2FLksiMG"
                };

                var req = new SMS.SMSRequest
                {
                    from = "919726304559",
                    to = "919726304559",
                    text = "Hello from Nexmo"
                };
                SMS.Send(req);
            }
            catch (Exception ex) { }
            return View();
        }
    }
}