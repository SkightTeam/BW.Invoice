using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Invoice.Integration;
using Microsoft.AspNetCore.Mvc;
using Invoice.UI.Models;

namespace Invoice.UI.Controllers
{
    public class HomeController : Controller
    {
        private AuthenticatorFacade authenticator;

        public HomeController(AuthenticatorFacade authenticator)
        {
            this.authenticator = authenticator;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Connect()
        {
            var url = authenticator.GetRequestTokenAuthorizeUrl("demo_user");
            return Redirect(url);
        }
        public ActionResult Authorize(string oauth_token, string oauth_verifier, string org)
        {
            var accessToken = authenticator.RetrieveAndStoreAccessToken(
                "demo_user", oauth_token, oauth_verifier);
            if (accessToken == null)
                return View("NoAuthorized");

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "BW Invoice";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}