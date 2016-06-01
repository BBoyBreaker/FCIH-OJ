using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using FCIH_OJ.Models;

namespace FCIH_OJ.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private UsersContext UserContext = new UsersContext();
        
        //
        // GET: /Admin/ManageUsers

        public ActionResult ManageUsers()
        {
            return View(UserContext.UserProfiles.ToList());
        }

        //
        // GET: /Admin/UserDetails/5

        public ActionResult UserDetails(int id = 0)
        {
            UserProfile Userprofile = UserManager.GetUserById(id);
            if (Userprofile == null)
            {
                return HttpNotFound();
            }
            string[] Roles = UserManager.GetRolesByUserId(Userprofile.UserName);
            AdminUserProfile Adminuserprofile = new AdminUserProfile();
            Adminuserprofile.Roles = Roles;
            Adminuserprofile.UserId = id;
            Adminuserprofile.UserName = Userprofile.UserName;
            Adminuserprofile.Email = Userprofile.Email;
            //other vars
            return View(Adminuserprofile);
        }

        //
        // GET: /Admin/EditUser/5

        public ActionResult EditUser(int id = 0)
        {
            UserProfile Userprofile = UserManager.GetUserById(id);
            if (Userprofile == null)
            {
                return HttpNotFound();
            }
            string[] UserRoles = UserManager.GetRolesByUserId(Userprofile.UserName);
            AdminUserProfile Adminuserprofile = new AdminUserProfile();
            Adminuserprofile.Roles = UserRoles;
            Adminuserprofile.UserId = id;
            Adminuserprofile.UserName = Userprofile.UserName;
            Adminuserprofile.Email = Userprofile.Email;
            ViewData["RolesList"] = Roles.GetAllRoles();
            //other vars
            return View(Adminuserprofile);
        }

        //
        // POST: /Admin/EditUser/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUser(AdminUserProfile Adminuserprofile)
        {
            if (ModelState.IsValid)
            {
                EditProfileModel EditProfileModel = new EditProfileModel();
                EditProfileModel.UserId = Adminuserprofile.UserId;
                EditProfileModel.UserName = Adminuserprofile.UserName;
                EditProfileModel.Email = Adminuserprofile.Email;
                if (Adminuserprofile.NewPassword != null)
                {
                    EditProfileModel.NewPassword = Adminuserprofile.NewPassword;
                    UserManager.EditUser(EditProfileModel);
                }
                if (Roles.GetRolesForUser(Adminuserprofile.UserName).Length != 0 && Adminuserprofile.UserName != "admin")
                {
                    Roles.RemoveUserFromRoles(Adminuserprofile.UserName, Roles.GetRolesForUser(Adminuserprofile.UserName));
                }
                if (Adminuserprofile.Roles != null && Adminuserprofile.Roles.Length != 0 && Adminuserprofile.UserName != "admin")
                {
                    Roles.AddUserToRoles(Adminuserprofile.UserName, Adminuserprofile.Roles);
                }
            }
            return RedirectToAction("ManageUsers", "Admin");
        }

        //
        // GET: /Admin/DeleteUser/5

        public ActionResult DeleteUser(int id = 0)
        {
            UserProfile userprofile = UserManager.GetUserById(id);
            if (userprofile == null)
            {
                return HttpNotFound();
            }
            return View(userprofile);
        }

        //
        // POST: /Admin/DeleteUser/5

        [HttpPost, ActionName("DeleteUser")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserManager.DeleteUserByUserName(UserManager.GetUserById(id).UserName);
            return RedirectToAction("ManageUsers");
        }
    }
}