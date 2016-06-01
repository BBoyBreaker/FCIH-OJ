using FCIH_OJ.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Web;
using System.Web.Security;
using WebMatrix.WebData;

namespace FCIH_OJ.Models
{
    public class UsersContext : DbContext
    {
        public UsersContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
    }

    [Table("UserProfile")]
    public class UserProfile
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }

    public class LoginModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterModel
    {
        [Required]
        [Display(Name = "UserName")]
        [Unique(ErrorMessage = "This UserName already taken.")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class EditProfileModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }

        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("NewPassword", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class AdminUserProfile
    {
        public int UserId { get; set; }
        public string UserName { get; set; }

        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }

        [Display(Name = "Roles")]
        public string[] Roles { get; set; }
    }

    //manager class
    public class UserManager
    {
        private static UsersContext UsersContext = new UsersContext();

        public static bool Register(RegisterModel RegisteredUser)
        {
            UserProfile user = new UserProfile();
            user.UserName = RegisteredUser.UserName;
            user.Password = RegisteredUser.Password;
            user.Email = RegisteredUser.Email;
            WebSecurity.CreateUserAndAccount(RegisteredUser.UserName, RegisteredUser.Password, new {
                Email = RegisteredUser.Email ,
                Password = RegisteredUser.Password
            });

            return true;
        }

        public static bool Login(string UserName, string Password)
        {
            return WebSecurity.Login(UserName, Password); ;
        }

        public static void Logout()
        {
            WebSecurity.Logout();
        }

        public static bool EditUser(EditProfileModel NewUserProfile)
        {
            bool changed = true;
            MembershipUser Membershipuser = Membership.GetUser(NewUserProfile.UserName);
            UserProfile Userprofile = UserManager.GetUserById(NewUserProfile.UserId);
            if (NewUserProfile.NewPassword != null)
            {
                changed = WebSecurity.ChangePassword(Userprofile.UserName, Userprofile.Password, NewUserProfile.NewPassword);
                Userprofile.Password = NewUserProfile.NewPassword;
            }

            Userprofile.UserName = NewUserProfile.UserName;
            Userprofile.Email = NewUserProfile.Email;
            // other fields
            // Userprofile.Image = NewUserProfile.Image;
            // Membership.UpdateUser(Membershipuser);

            UsersContext.UserProfiles.Attach(Userprofile);
            UsersContext.Entry(Userprofile).State = EntityState.Modified;
            UsersContext.SaveChanges();


            return changed;
        }

        public static UserProfile GetUserById(int ID)
        {
            return UsersContext.UserProfiles.Find(ID);
        }

        public static string[] GetRolesByUserId(string UserName)
        {
            return Roles.GetRolesForUser(UserName);
        }

        public static void DeleteUserByUserName(string UserName)
        {
            if (Roles.GetRolesForUser(UserName).Length != 0)
            {
                Roles.RemoveUserFromRoles(UserName, Roles.GetRolesForUser(UserName));
            }
            ((SimpleMembershipProvider)Membership.Provider).DeleteAccount(UserName); // deletes record from webpages_Membership table
            ((SimpleMembershipProvider)Membership.Provider).DeleteUser(UserName, true); // deletes record from UserProfile table
        }
    }
}
