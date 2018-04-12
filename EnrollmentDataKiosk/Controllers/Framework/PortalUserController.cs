using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataKioskStacks.APIs;
using DataKioskStacks.APIServer.APIObjs;
using EnrollmentDataKiosk.Manager;
using EnrollmentDataKiosk.Models.PortalModel;
using WebCribs.TechCracker.WebCribs.TechCracker;

namespace EnrollmentDataKiosk.Controllers.Framework
{
    public class PortalUserController : Controller
    {
        //
        // GET: /PortalUser/
        public ActionResult Index()
        {

            ViewBag.Reply = Session["Reply"] as string;
            ViewBag.Error = Session["Error"] as string;
            Session["Reply"] = "";
            Session["Error"] = "";

            try
            {
                var items = PortalUser.GetUserList() ?? new List<RegisteredUserReportObj>();
                if (!items.Any())
                {
                    ViewBag.Error = "No Registered User Info Found!";
                    return View(new List<RegisteredUserReportObj>());
                }
                if (!User.IsInRole("Super Admin"))
                {
                    items = items.FindAll(m => m.RoleId != 1);
                    if (!items.Any())
                    {
                        ViewBag.Error = "No User Info Found!";
                        return View(new List<RegisteredUserReportObj>());
                    }
                }
                var users = items.Where(item => !item.MyRole.Contains("Super Admin")).ToList();

                Session["_portalUsersInfos"] = users;
                return View(users);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                BugManager.LogApplicationBug(ex.StackTrace, ex.Source, ex.Message);
                return View(new List<RegisteredUserReportObj>());
            }

        }


        #region CRUD


        public ActionResult AddUser()
        {
            ViewBag.Errors = Session["CreateErrors"] as List<string>;
            ViewBag.Error = Session["CreateError"] as string;
            Session["CreateErrors"] = "";
            Session["CreateError"] = "";

            if (Session["_NewPortalUser"] == null)
            {
                string msg;
                var user = new AuthPortalUser
                {
                    //MyRoleIds = new[] { 0 },
                    //AllRoles = new List<NameAndValueObject>(),
                    Action = "AddUser"
                };
                //var roles = PortalRole.GetRoles();
                //if (roles == null || !roles.Any())
                //{
                //    ViewBag.Error = "Invalid User Roles";
                //    return View("AddUser", new AuthPortalUser { Action = "AddUser" });
                //    //return View(new AuthPortalUser());
                //}

                //foreach (var item in roles)
                //{
                //    if (item.Name == "*" || item.Name == "AgentUser") { continue; }
                //    //if (!User.IsInRole("PortalAdmin"))
                //    //{
                //    //    if (item.Name == "PortalAdmin" || item.Name == "SiteAdmin") { continue; }
                //    //}
                //    user.AllRoles.Add(new NameAndValueObject { Id = item.RoleId, Name = item.Name });
                //}
                //user.SexId = 1;
                Session["_NewPortalUser"] = user;
                return View("AddUser", user);
            }

            var model = Session["_NewPortalUser"] as AuthPortalUser;
            if (model != null)
            {
                model.Action = "AddUser";
            }
            return View("AddUser", model);

        }

        [HttpPost]
        public ActionResult AddUser(AuthPortalUser model)
        {

            var errorLists = new List<string>();
            try
            {
                Session["_NewPortalUser"] = model;

                if (!ModelState.IsValid)
                {
                    model.Password = "";
                    model.ConfirmPassword = "";
                    Session["_NewPortalUser"] = model;

                    errorLists = (from value in ViewData.ModelState.Values
                                  where value.Errors.Count > 0
                                  from error in value.Errors
                                  where !string.IsNullOrEmpty(error.ErrorMessage)
                                  select error.ErrorMessage).ToList();

                    Session["CreateErrors"] = errorLists;
                    return Redirect(Url.RouteUrl(new { action = "AddUser" }));
                }

                var userData = MvcApplication.GetUserData(User.Identity.Name) ?? new UserData();
                var helper = new UserRegistrationObj
                {
                    ConfirmPassword = model.ConfirmPassword,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    Surname = model.LastName,
                    MobileNumber = model.MobileNo,
                    RoleId = model.RoleId,
                    OrganizationId = model.OrganizationId,
                    UserTypeId = model.UserTypeId,
                    Username = model.UserName,
                    Password = model.Password,
                    Sex = model.SexId,
                    RegisteredByUserId = 1,
                    //RegisteredByUserId = Convert.ToInt32(userData.UserId),
                };
                
                var retId = PortalUser.AddUser(helper);
                if (retId == null)
                {
                    Session["CreateError"] = "Unable to add user. Please try again later";
                    return Redirect(Url.RouteUrl(new { action = "AddUser" }));
                }
                if (!retId.Status.IsSuccessful)
                {
                    Session["CreateError"] = string.IsNullOrEmpty(retId.Status.Message.FriendlyMessage) ? "Unable to add user! Please try again later" : retId.Status.Message.FriendlyMessage;
                    return Redirect(Url.RouteUrl(new { action = "AddUser" }));
                }

                Session["_NewPortalUser"] = null;
                Session["Reply"] = "User Information was added successfully";
                return Redirect(Url.RouteUrl(new { action = "Index" }));
            }
            catch (Exception ex)
            {
                Session["CreateError"] = ex.Message;
                return Redirect(Url.RouteUrl(new { action = "AddUser" }));
                //return View();
            }
        }


        public ActionResult ModifyUser(string id)
        {
            ViewBag.Errors = Session["EditErrors"] as List<string>;
            ViewBag.Error = Session["EditError"] as string;
            Session["EditErrors"] = null;
            Session["EditError"] = null;

            try
            {
                long userId;
                Int64.TryParse(id, out userId);

                if (userId < 1)
                {
                    Session["Error"] = "Invalid selection";
                    //ViewBag.Error = "Invalid selection";
                    return Redirect(Url.RouteUrl(new { action = "Index" }));
                }

                if (Session["_portalUsersInfos"] == null)
                {
                    Session["Error"] = "Your session has expired! Please try again later";
                    return Redirect(Url.RouteUrl(new { action = "Index" }));
                }
                var users = Session["_portalUsersInfos"] as List<RegisteredUserReportObj>;
                if (users == null || !users.Any())
                {
                    //ViewBag.Error = "Your session has expired! Please try again later";
                    Session["Error"] = "Your session has expired! Please try again later";
                    return Redirect(Url.RouteUrl(new { action = "Index" }));
                }

                var user = users.Find(m => m.UserId == userId);
                if (user == null || user.UserId < 1)
                {
                    //ViewBag.Error = "Your session has expired! Please try again later";
                    Session["Error"] = "Your session has expired! Please try again later";
                    return Redirect(Url.RouteUrl(new { action = "Index" }));
                }

                var helper = new AuthPortalUser
                {
                    ConfirmPassword = "",
                    Email = user.Email,
                    FailedPasswordCount = user.FailedPasswordCount,
                    FirstName = user.FirstName,
                    IsApproved = user.IsApproved,
                    IsLockedOut = user.IsLockedOut,
                    LastName = user.Surname,
                    LastLockedOutTimeStamp = user.LastLockedOutTimeStamp,
                    LastLoginTimeStamp = user.LastLoginTimeStamp,
                    LastPasswordChangeTimeStamp = user.PasswordChangeTimeStamp,
                    MobileNo = user.MobileNumber,
                    //MyRoleIds = user.MyRoleIds,
                    //MyRoles = portalUser.MyRoles,
                    OrganizationId = user.OrganizationId,
                    UserTypeId = user.UserTypeId,
                    RoleId = user.RoleId,
                    UserName = user.Username,
                    Password = user.Password,
                    //SelectedRoles = user.SelectedRoles,
                    SexId = user.SexId,
                    TimeStampRegistered = user.TimeStampRegistered,
                    UserId = user.UserId,
                    //Roles = portalUser.MyRoleIds.ToList(),

                    Action = "ModifyUser"
                };

                Session["_selectedPortalUser"] = user;
                return View("AddUser", helper);

            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                BugManager.LogApplicationBug(ex.StackTrace, ex.Source, ex.Message);
                return null;
            }

        }

        [HttpPost]
        public ActionResult ModifyUser(string id, AuthPortalUser model)
        {
            try
            {
                long userId;
                Int64.TryParse(id, out userId);

                if (userId < 1)
                {
                    ViewBag.Error = "Invalid selection";
                    return Redirect(Url.RouteUrl(new { action = "Index" }));
                }

                if (model == null)
                {
                    Session["Error"] = "Your session has expired! Please, re-login";
                    return Redirect(Url.RouteUrl(new { action = "Index" }));
                }

                if (Session["_selectedPortalUser"] == null)
                {
                    Session["Error"] = "Your session has expired! Please, re-login";
                    return Redirect(Url.RouteUrl(new { action = "Index" }));
                }

                var thisUser = Session["_selectedPortalUser"] as RegisteredUserReportObj;
                if (thisUser == null || thisUser.UserId < 1)
                {
                    Session["Error"] = "Your session has expired! Please, re-login";
                    return Redirect(Url.RouteUrl(new { action = "Index" }));
                }

                ModelState.Clear();
                if (!ModelState.IsValid)
                {
                    ViewBag.Error = "Please fill all required fields";
                    return Redirect(Url.RouteUrl(new { action = "Index" }));
                }

                var helper = new UserRegistrationObj
                {
                    Password = "password.",
                    ConfirmPassword = "password.",
                    Email = model.Email,
                    FirstName = model.FirstName,
                    Surname = model.LastName,
                    MobileNumber = model.MobileNo,
                    RoleId = model.RoleId,
                    UserTypeId = model.UserTypeId,
                    OrganizationId = model.OrganizationId,
                    //MyRoleIds = model.MyRoleIds,
                    //MyRoles = selRoles.ToArray(),
                    Username = model.UserName,
                    //SelectedRoles = string.Join(";", selRoles),
                    Sex = model.SexId,
                    UserId = thisUser.UserId,
                    //IsActive = model.IsApproved
                };

                var retId = PortalUser.UpdateUser(helper);
                if (retId == null)
                {
                    Session["EditError"] = "Unable to update this user";
                    return Redirect(Url.RouteUrl(new { action = "ModifyUser" }));
                }

                if (!retId.IsSuccessful)
                {

                    Session["EditError"] = string.IsNullOrEmpty(retId.Message.FriendlyMessage) ? "Unable to update this user" : retId.Message.FriendlyMessage;
                    return Redirect(Url.RouteUrl(new { action = "ModifyUser" }));

                    //Session["Error"] = string.IsNullOrEmpty(retId.Message.FriendlyMessage) ? "Unable to update this organization" : retId.Message.FriendlyMessage;
                    //return Redirect(Url.RouteUrl(new { action = "Index" }));
                }

                //if (retId == null)
                //{
                //    Session["Error"] = "Unable to complete your request! Please try again later";
                //    return Redirect(Url.RouteUrl(new { action = "Index" }));
                //}

                //if (!retId.IsSuccessful)
                //{
                //    Session["Error"] = string.IsNullOrEmpty(retId.Message.FriendlyMessage) ? "Unable to update this user" : retId.Message.FriendlyMessage;
                //    return Redirect(Url.RouteUrl(new { action = "Index" }));
                //}

                Session["_selectedPortalUser"] = null;
                Session["Reply"] = "User information was updated successfully";
                return Redirect(Url.RouteUrl(new { action = "Index" }));


            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                BugManager.LogApplicationBug(ex.StackTrace, ex.Source, ex.Message);
                return null;
            }
        }

        #endregion


    }
}