using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Invoice.Domain;
using Invoice.Integration;
using Microsoft.AspNetCore.Mvc;
using Invoice.UI.Models;

namespace Invoice.UI.Controllers
{
    public class HomeController : Controller
    {
        private AuthenticatorFacade authenticator;
        private ImportService importService;
        private User currentUser;

        public HomeController(AuthenticatorFacade authenticator, User currentUser, ImportService importService)
        {
            this.authenticator = authenticator;
            this.currentUser = currentUser;
            this.importService = importService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Connect()
        {
            var url = authenticator.GetRequestTokenAuthorizeUrl(currentUser.Identifier);
            return Redirect(url);
        }
        public IActionResult Authorize(string oauth_token, string oauth_verifier, string org)
        {
            var accessToken = authenticator.RetrieveAndStoreAccessToken(
                currentUser.Identifier, oauth_token, oauth_verifier);
            if (accessToken == null)
                return View("NoAuthorized");

            return View();
        }

        public IActionResult Import()
        {
            importService.importVendors();
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