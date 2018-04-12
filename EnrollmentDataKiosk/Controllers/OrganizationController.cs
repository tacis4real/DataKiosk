using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataKioskStacks.APIServer.APIObjs;
using DataKioskStacks.DataContract.Admin;
using DataKioskStacks.Service.Contract;
using WebCribs.TechCracker.WebCribs.TechCracker;

namespace EnrollmentDataKiosk.Controllers
{
    public class OrganizationController : Controller
    {
        //
        // GET: /Organization/
        public ActionResult Index()
        {

            ViewBag.Reply = Session["Reply"] as string;
            ViewBag.Error = Session["Error"] as string;
            Session["Reply"] = "";
            Session["Error"] = "";

            try
            {
                var items = ServiceProvider.Instance().GetOrganizationServices().GetOrganizationListObjs() ?? new List<RegisteredOrganizationReportObj>();
                if (!items.Any())
                {
                    ViewBag.Error = "No Registered Organization Info Found!";
                    return View(new List<RegisteredOrganizationReportObj>());
                }

                var organizationLists = items;
                Session["_registeredOrganizationInfos"] = organizationLists;
                return View(organizationLists);

            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                BugManager.LogApplicationBug(ex.StackTrace, ex.Source, ex.Message);
                return View(new List<RegisteredOrganizationReportObj>());
            }
        }


        public ActionResult AddOrganization()
        {
            ViewBag.Errors = Session["CreateErrors"] as List<string>;
            ViewBag.Error = Session["CreateError"] as string;
            Session["CreateErrors"] = "";
            Session["CreateError"] = "";
            if (Session["_NewOrganization"] == null)
            {
                var org = new OrganizationRegObj { Action = "AddOrganization" };
                return View("AddOrganization", org);
                //return View(org);
            }

            var model = Session["_NewOrganization"] as OrganizationRegObj;
            if (model != null)
            {
                model.Action = "AddOrganization";
            }
            return View("AddOrganization", model);
            //return View(model);
        }

        [HttpPost]
        public ActionResult AddOrganization(OrganizationRegObj model)
        {

            var errorLists = new List<string>();
            try
            {

                Session["_NewOrganization"] = model;
                if (!ModelState.IsValid)
                {
                    Session["_NewOrganization"] = model;

                    errorLists = (from value in ViewData.ModelState.Values
                                  where value.Errors.Count > 0
                                  from error in value.Errors
                                  where !string.IsNullOrEmpty(error.ErrorMessage)
                                  select error.ErrorMessage).ToList();

                    Session["CreateErrors"] = errorLists;
                    return Redirect(Url.RouteUrl(new { action = "AddOrganization" }));
                }

                var helper = new Organization
                {
                    Name = model.Name,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    Address = model.Address,
                    Status = model.Status,
                    RegisteredByUserId = 1
                };

                string msg;
                var retId = ServiceProvider.Instance().GetOrganizationServices().AddOrganization(helper, out  msg);
                if (retId < 1)
                {
                    Session["CreateError"] = string.IsNullOrEmpty(msg) ? "Unable to add new organization " : msg;
                    return Redirect(Url.RouteUrl(new { action = "AddOrganization" }));
                    //ViewBag.ErrorMessage = string.IsNullOrEmpty(msg) ? "Unable to add new organization " : msg;
                    //return View("AddOrganization", model);
                }

                Session["_NewOrganization"] = null;
                Session["Reply"] = "Organization Information was added successfully";
                return Redirect(Url.RouteUrl(new { action = "Index" }));

            }
            catch (Exception ex)
            {
                Session["CreateError"] = ex.Message;
                return Redirect(Url.RouteUrl(new { action = "AddOrganization" }));
            }
        }
        

        #region EDIT

        public ActionResult ModifyOrganization(string id)
        {

            ViewBag.Errors = Session["EditErrors"] as List<string>;
            ViewBag.Error = Session["EditError"] as string;
            Session["EditErrors"] = null;
            Session["EditError"] = null;

            try
            {
                long organizationId;
                Int64.TryParse(id, out organizationId);

                if (organizationId < 1)
                {
                    Session["Error"] = "Invalid selection";
                    //ViewBag.Error = "Invalid selection";
                    return Redirect(Url.RouteUrl(new { action = "Index" }));
                }
                if (Session["_registeredOrganizationInfos"] == null)
                {
                    //ViewBag.Error = "Your session has expired! Please try again later";
                    Session["Error"] = "Your session has expired! Please try again later";
                    return Redirect(Url.RouteUrl(new { action = "Index" }));
                }
                var organizations = Session["_registeredOrganizationInfos"] as List<RegisteredOrganizationReportObj>;
                if (organizations == null || !organizations.Any())
                {
                    //ViewBag.Error = "Your session has expired! Please try again later";
                    Session["Error"] = "Your session has expired! Please try again later";
                    return Redirect(Url.RouteUrl(new { action = "Index" }));
                }
                var organization = organizations.Find(m => m.OrganizationId == organizationId);
                if (organization == null || organization.OrganizationId < 1)
                {
                    //ViewBag.Error = "Your session has expired! Please try again later";
                    Session["Error"] = "Your session has expired! Please try again later";
                    return Redirect(Url.RouteUrl(new { action = "Index" }));
                }
                
                var helper = new OrganizationRegObj
                {
                    OrganizationId = organization.OrganizationId,
                    Name = organization.Name,
                    PhoneNumber = (organization.PhoneNumber),
                    Email = organization.Email,
                    Address = organization.Address,
                    Status = organization.Status,
                    Action = "ModifyOrganization"
                };

                Session["_selectedOrganization"] = organization;
                return View("AddOrganization", helper);

            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                BugManager.LogApplicationBug(ex.StackTrace, ex.Source, ex.Message);
                return null;
            }
        }

        [HttpPost]
        public ActionResult ModifyOrganization(string id, OrganizationRegObj model)
        {
            try
            {
                long organizationId;
                Int64.TryParse(id, out organizationId);

                if (organizationId < 1)
                {
                    ViewBag.Error = "Invalid selection";
                    return Redirect(Url.RouteUrl(new { action = "Index" }));
                }

                if (model == null)
                {
                    Session["Error"] = "Your session has expired! Please, re-login";
                    return Redirect(Url.RouteUrl(new { action = "Index" }));
                }

                if (Session["_selectedOrganization"] == null)
                {
                    Session["Error"] = "Your session has expired! Please, re-login";
                    return Redirect(Url.RouteUrl(new { action = "Index" }));
                }

                var thisOrganization = Session["_selectedOrganization"] as RegisteredOrganizationReportObj;
                if (thisOrganization == null || thisOrganization.OrganizationId < 1)
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
                
                var helper = new Organization
                {
                    OrganizationId = thisOrganization.OrganizationId,
                    Name = model.Name,
                    PhoneNumber = model.PhoneNumber,
                    Email = model.Email,
                    Address = model.Address,
                    RegisteredByUserId = thisOrganization.RegisteredByUserId,
                    Status = model.Status
                };

                var retId = ServiceProvider.Instance().GetOrganizationServices().UpdateOrganization(helper);
                if (retId == null)
                {
                    Session["EditError"] = "Unable to update this organization";
                    return Redirect(Url.RouteUrl(new { action = "ModifyOrganization" }));
                }

                if (!retId.IsSuccessful)
                {

                    Session["EditError"] = string.IsNullOrEmpty(retId.Message.FriendlyMessage) ? "Unable to update this organization" : retId.Message.FriendlyMessage;
                    return Redirect(Url.RouteUrl(new { action = "ModifyOrganization" }));

                    //Session["Error"] = string.IsNullOrEmpty(retId.Message.FriendlyMessage) ? "Unable to update this organization" : retId.Message.FriendlyMessage;
                    //return Redirect(Url.RouteUrl(new { action = "Index" }));
                }

                Session["_selectedOrganization"] = null;
                Session["Reply"] = "Organization information was updated successfully";
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





















        public ActionResult RegisterOrganization()
        {
            ViewBag.Errors = Session["CreateErrors"] as List<string>;
            ViewBag.Error = Session["CreateError"] as string;
            Session["CreateErrors"] = "";
            Session["CreateError"] = "";
            if (Session["_NewOrganization"] == null)
            {
                var org = new Organization();
                return View(org);
            }

            var model = Session["_NewOrganization"] as Organization;
            return View(model);
        }
	}
}