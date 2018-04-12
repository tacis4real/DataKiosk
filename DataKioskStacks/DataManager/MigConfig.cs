using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataKioskStacks.DataContract;
using DataKioskStacks.DataContract.Admin;
using DataKioskStacks.DataManager;
using WebCribs.TechCracker.WebCribs.TechCracker;

namespace DataKioskStacks.Migrations
{
    internal partial class Configuration
    {


        public void ProcessSeed(DataKioskEntities context)
        {
            LoadDefaultRoles(context);
            ProcessLookUpFromFiles(context);
            ProcessStateLookUps(context);
            LoadUserTypes(context);
            LoadDefaultOrganization(context);
        }



        private void LoadDefaultOrganization(DataKioskEntities context)
        {
            if (!context.Organizations.Any())
            {
                var organization = new Organization
                {
                    Name = "UBA", 
                    Address = "UBA House Leventis",
                    Email = "ubagroup@gmail.com",
                    PhoneNumber = "08034000000",
                    RegisteredByUserId = 1,
                    TimeStampRegistered = DateTime.Now,
                    Status = 1
                };
                context.Organizations.AddOrUpdate(m => m.Name, organization);
                context.SaveChanges();
            }
        }
        private void LoadDefaultRoles(DataKioskEntities context)
        {
            if (!context.Roles.Any())
            {
                var role = new Role { Name = "Super Admin", Status = true };
                context.Roles.AddOrUpdate(m => m.Name, role);
                context.SaveChanges();

                role = new Role { Name = "Portal Admin", Status = true };
                context.Roles.AddOrUpdate(m => m.Name, role);
                context.SaveChanges();

                role = new Role { Name = "Bank User", Status = true };
                context.Roles.AddOrUpdate(m => m.Name, role);
                context.SaveChanges();

                role = new Role { Name = "Bank Admin", Status = true };
                context.Roles.AddOrUpdate(m => m.Name, role);
                context.SaveChanges();

                //role = new Role { Name = "*", Status = true };
                //context.Roles.AddOrUpdate(m => m.Name, role);
                //context.SaveChanges();

            }
        }

        private void LoadUserTypes(DataKioskEntities context)
        {
            if (!context.UserTypes.Any())
            {
                var userType = new UserType { Name = "EpayPlus" };
                context.UserTypes.AddOrUpdate(m => m.Name, userType);
                context.SaveChanges();

                userType = new UserType { Name = "Organization" };
                context.UserTypes.AddOrUpdate(m => m.Name, userType);
                context.SaveChanges();
            }
        }

        private void ProcessStateLookUps(DataKioskEntities context)
        {
            try
            {
                if (!context.States.Any())
                {
                    var state = new State { Name = "Abia" };
                    context.States.AddOrUpdate(m => m.Name, state);
                    context.SaveChanges();

                    state = new State { Name = "Adamawa" };
                    context.States.AddOrUpdate(m => m.Name, state);
                    context.SaveChanges();

                    state = new State { Name = "Anambra" };
                    context.States.AddOrUpdate(m => m.Name, state);
                    context.SaveChanges();

                    state = new State { Name = "Akwa Ibom" };
                    context.States.AddOrUpdate(m => m.Name, state);
                    context.SaveChanges();

                    state = new State { Name = "Bauchi" };
                    context.States.AddOrUpdate(m => m.Name, state);
                    context.SaveChanges();

                    state = new State { Name = "Bayelsa" };
                    context.States.AddOrUpdate(m => m.Name, state);
                    context.SaveChanges();

                    state = new State { Name = "Benue" };
                    context.States.AddOrUpdate(m => m.Name, state);
                    context.SaveChanges();

                    state = new State { Name = "Borno" };
                    context.States.AddOrUpdate(m => m.Name, state);
                    context.SaveChanges();

                    state = new State { Name = "Cross River" };
                    context.States.AddOrUpdate(m => m.Name, state);
                    context.SaveChanges();

                    state = new State { Name = "Delta" };
                    context.States.AddOrUpdate(m => m.Name, state);
                    context.SaveChanges();

                    state = new State { Name = "Ebonyi" };
                    context.States.AddOrUpdate(m => m.Name, state);
                    context.SaveChanges();

                    state = new State { Name = "Enugu" };
                    context.States.AddOrUpdate(m => m.Name, state);
                    context.SaveChanges();

                    state = new State { Name = "Edo" };
                    context.States.AddOrUpdate(m => m.Name, state);
                    context.SaveChanges();

                    state = new State { Name = "Ekiti" };
                    context.States.AddOrUpdate(m => m.Name, state);
                    context.SaveChanges();

                    state = new State { Name = "Gombe" };
                    context.States.AddOrUpdate(m => m.Name, state);
                    context.SaveChanges();

                    state = new State { Name = "Imo" };
                    context.States.AddOrUpdate(m => m.Name, state);
                    context.SaveChanges();

                    state = new State { Name = "Jigawa" };
                    context.States.AddOrUpdate(m => m.Name, state);
                    context.SaveChanges();

                    state = new State { Name = "Kaduna" };
                    context.States.AddOrUpdate(m => m.Name, state);
                    context.SaveChanges();

                    state = new State { Name = "Kano" };
                    context.States.AddOrUpdate(m => m.Name, state);
                    context.SaveChanges();

                    state = new State { Name = "Katsina" };
                    context.States.AddOrUpdate(m => m.Name, state);
                    context.SaveChanges();

                    state = new State { Name = "Kebbi" };
                    context.States.AddOrUpdate(m => m.Name, state);
                    context.SaveChanges();

                    state = new State { Name = "Kogi" };
                    context.States.AddOrUpdate(m => m.Name, state);
                    context.SaveChanges();

                    state = new State { Name = "Kwara" };
                    context.States.AddOrUpdate(m => m.Name, state);
                    context.SaveChanges();

                    state = new State { Name = "Lagos" };
                    context.States.AddOrUpdate(m => m.Name, state);
                    context.SaveChanges();

                    state = new State { Name = "Nasarawa" };
                    context.States.AddOrUpdate(m => m.Name, state);
                    context.SaveChanges();

                    state = new State { Name = "Niger" };
                    context.States.AddOrUpdate(m => m.Name, state);
                    context.SaveChanges();

                    state = new State { Name = "Ogun" };
                    context.States.AddOrUpdate(m => m.Name, state);
                    context.SaveChanges();

                    state = new State { Name = "Ondo" };
                    context.States.AddOrUpdate(m => m.Name, state);
                    context.SaveChanges();

                    state = new State { Name = "Osun" };
                    context.States.AddOrUpdate(m => m.Name, state);
                    context.SaveChanges();

                    state = new State { Name = "Oyo" };
                    context.States.AddOrUpdate(m => m.Name, state);
                    context.SaveChanges();

                    state = new State { Name = "Plateau" };
                    context.States.AddOrUpdate(m => m.Name, state);
                    context.SaveChanges();

                    state = new State { Name = "Rivers" };
                    context.States.AddOrUpdate(m => m.Name, state);
                    context.SaveChanges();

                    state = new State { Name = "Sokoto" };
                    context.States.AddOrUpdate(m => m.Name, state);
                    context.SaveChanges();

                    state = new State { Name = "Taraba" };
                    context.States.AddOrUpdate(m => m.Name, state);
                    context.SaveChanges();

                    state = new State { Name = "Yobe" };
                    context.States.AddOrUpdate(m => m.Name, state);
                    context.SaveChanges();

                    state = new State { Name = "FCT Abuja" };
                    context.States.AddOrUpdate(m => m.Name, state);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                BugManager.LogApplicationBug(ex.StackTrace, ex.Source, ex.Message);
            }
        }

        private void ProcessLookUpFromFiles(DataKioskEntities context)
        {
            try
            {
                var basePath = GetBasePath();
                if (string.IsNullOrEmpty(basePath)) { return; }
                if (!context.LocalAreas.Any())
                {
                    var stateLgas = GetFromResources(basePath + "\\SqlFiles\\lga_lookups2.sql");
                    if (!string.IsNullOrEmpty(stateLgas))
                    {
                        context.Database.ExecuteSqlCommand(stateLgas);
                    }
                }
            }
            catch (Exception ex)
            {
                BugManager.LogApplicationBug(ex.StackTrace, ex.Source, ex.Message);
            }
        }


        public static string GetBasePath()
        {
            var currentDomain = AppDomain.CurrentDomain;

            string dirPath;
            if (currentDomain.BaseDirectory.Contains("\\bin\\Debug"))
            {
                dirPath = currentDomain.BaseDirectory.Replace("\\bin\\Debug\\", "");
            }
            else if (currentDomain.BaseDirectory.Contains("\\bin\\Release"))
            {
                dirPath = currentDomain.BaseDirectory.Replace("\\bin\\Release\\", "");
            }
            else
            {
                dirPath = currentDomain.BaseDirectory;
            }
            return dirPath;
        }

        internal string GetFromResources(string resourceName)
        {
            try
            {
                using (var reader = new StreamReader(resourceName))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (Exception)
            {
                return "";
            }
        }



    }
}
