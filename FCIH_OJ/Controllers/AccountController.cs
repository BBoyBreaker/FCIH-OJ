using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using FCIH_OJ.Models;
using FCIH_OJ.Validations;

namespace FCIH_OJ.Controllers
{
    public class AccountController : Controller
    {

        private UsersContext UsersContext = new UsersContext();

        //
        // GET: /Account/Login

        [AnonymousOnly]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login

        [HttpPost]
        [AnonymousOnly]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid && UserManager.Login(model.UserName, model.Password))
            {
                return RedirectToLocal(returnUrl);
            }

            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return View(model);
        }

        //
        // POST: /Account/LogOff

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult LogOff()
        {
            UserManager.Logout();
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/Register

        [AnonymousOnly]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register

        [HttpPost]
        [AnonymousOnly]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                UserManager.Register(model);
                UserManager.Login(model.UserName, model.Password);
                return RedirectToAction("Index", "Home");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/View
        [Authorize]
        public ActionResult View()
        {
            UserProfile Userprofile = UserManager.GetUserById((int)WebSecurity.CurrentUserId);
            return View(Userprofile);
        }

        //
        // GET: /Account/Manage
        [Authorize]
        public ActionResult Manage()
        {
            UserProfile Userprofile = UserManager.GetUserById((int)WebSecurity.CurrentUserId);
            EditProfileModel model = new EditProfileModel();
            model.UserName = Userprofile.UserName;
            model.UserId = Userprofile.UserId;
            model.Email = Userprofile.Email;
            ViewBag.ReturnUrl = Url.Action("Manage");
            return View(model);
        }

        //
        // POST: /Account/Manage

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Manage(EditProfileModel model)
        {
            ViewBag.ReturnUrl = Url.Action("Manage");
            if (ModelState.IsValid)
            {
                UserProfile Userprofile = UserManager.GetUserById((int)WebSecurity.CurrentUserId);
                model.Email = Userprofile.Email;
                model.UserName = Userprofile.UserName;
                model.UserId = Userprofile.UserId;
                UserManager.EditUser(model);
                ModelState.AddModelError("", "Profile Updated Successfully.");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //helpers
        #region Helpers
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        #endregion
    }
}
