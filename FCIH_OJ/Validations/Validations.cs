using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using FCIH_OJ.Models;
using WebMatrix.WebData;
using System.Web.Security;
using System.Web.Mvc;
using System.Web;

namespace FCIH_OJ.Validations
{
    public class Unique : ValidationAttribute
    {
        UsersContext UsersContext = new UsersContext();
        private const string DefaultErrorMessage = "Not Unique";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //if field not empty
            if (value != null)
            {
                // if the field is user username or user email
                if (validationContext.DisplayName == "UserName" || validationContext.DisplayName == "Email")
                {
                    // fetch results where email or username equals the value
                    var results = UsersContext.UserProfiles.Where(a => a.UserName == value || a.Email == value).SingleOrDefault();

                    // if not empty resutls throw exception with error message 
                    if (results != null)
                        return new ValidationResult(ErrorMessage ?? DefaultErrorMessage);
                }
            }

            return ValidationResult.Success;
        }
    }

    public class AnonymousOnly : AuthorizeAttribute
    {
        // Custom property
        public string AccessLevel { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return !httpContext.User.Identity.IsAuthenticated;
        }
    }

}
