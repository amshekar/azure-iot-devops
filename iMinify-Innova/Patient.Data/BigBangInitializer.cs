using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Patients.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace Patients.Data
{
    public class BigBangInitializer : CreateDatabaseIfNotExists<PatientDbContext>
    {
        protected override void Seed(PatientDbContext context)
        {
            Initialize(context);
            base.Seed(context);
        }

        public void Initialize(PatientDbContext context)
        {
            try
            {
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                userManager.UserValidator = new UserValidator<ApplicationUser>(userManager)
                {
                    AllowOnlyAlphanumericUserNames = false
                };
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

                if (!roleManager.RoleExists("Admin"))
                {
                    roleManager.Create(new IdentityRole("Admin"));
                }

                if (!roleManager.RoleExists("Member"))
                {
                    roleManager.Create(new IdentityRole("Member"));
                }

                var user = new ApplicationUser()
                {
                    Email = "test@test.com",
                    UserName = "test@test.com",
                    FullName = "test",
                    LastName = "test"
                };

                var userResult = userManager.Create(user, "Admin@123");

                if (userResult.Succeeded)
                {
                    userManager.AddToRole<ApplicationUser, string>(user.Id, "Admin");
                }

                IList<Patient> patients = new List<Patient>();
                patients.Add(new Patient() { FullName = "Mr John", Gender = "male", DOB = "06/03/1980", PhoneNo = "8899556658", CareSpec = "Cardiac", Address = "TCS, Hyderabad", Photo = null });
                patients.Add(new Patient() { FullName = "Miss Lee", Gender = "female", DOB = "06/01/1990", PhoneNo = "8899556659", CareSpec = "Cardiac", Address = "Tcs, Pune", Photo = null });
                patients.Add(new Patient() { FullName = "Mr BradPit", Gender = "male", DOB = "06/04/1970", PhoneNo = "8899556657", CareSpec = "Cardiac", Address = "TCS Park, Mumbai", Photo = null });
                patients.Add(new Patient() { FullName = "Enjeline Jose", Gender = "female", DOB = "06/04/1985", PhoneNo = "8899556656", CareSpec = "Cardiac", Address = "Tcs, Chennai", Photo = null });

                foreach (Patient item in patients)                
                    context.Patients.Add(item);
                  
                context.Commit();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
