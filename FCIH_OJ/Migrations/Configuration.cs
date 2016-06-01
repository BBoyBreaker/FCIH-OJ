namespace FCIH_OJ.Migrations
{
    using FCIH_OJ.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Security;
    using WebMatrix.WebData;

    internal sealed class Configuration : DbMigrationsConfiguration<FCIH_OJ.Models.UsersContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(FCIH_OJ.Models.UsersContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //

            //////////////////////////////////////////////////////
            // user module

            WebSecurity.InitializeDatabaseConnection("DefaultConnection", "UserProfile", "UserId", "UserName", autoCreateTables: true);

            SimpleRoleProvider roles = new SimpleRoleProvider(Roles.Provider);
            SimpleMembershipProvider membership = new SimpleMembershipProvider(Membership.Provider);
            
            if (!roles.RoleExists("admin"))
            {
                roles.CreateRole("admin");
            }
            if (!WebSecurity.UserExists("admin"))
            {
                WebSecurity.CreateUserAndAccount("admin", "admin", new {
                    Email = "admin@admin.com" ,
                    Password = "admin"
                });
            }
            if (!roles.GetRolesForUser("admin").Contains("admin"))
            {
                roles.AddUsersToRoles(new[] { "admin" }, new[] { "admin" });
            } 
            //end of user module
            //////////////////////////////////////////////////////
            //other modules


        }
    }
}
