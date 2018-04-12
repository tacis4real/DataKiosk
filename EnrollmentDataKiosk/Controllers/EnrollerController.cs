using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataKioskStacks.APIServer.APIObjs;
using DataKioskStacks.DataContract;
using DataKioskStacks.Service.Contract;

namespace EnrollmentDataKiosk.Controllers
{
    public class EnrollerController : Controller
    {
        ////
        //// GET: /Enroller/
        //public ActionResult Index()
        //{
        //    return View();
        //}

        public ActionResult AddEnroller()
        {
            ViewBag.Errors = Session["CreateErrors"] as List<string>;
            ViewBag.Error = Session["CreateError"] as string;
            Session["CreateErrors"] = "";
            Session["CreateError"] = "";

            if (Session["_Enroller"] == null)
            {
                var enroller = new EnrollerRegObj();
                return View(enroller);
            }

            var model = Session["_Enroller"] as EnrollerRegObj;
            return View(model);
        }


        [HttpPost]
        public ActionResult AddEnroller(EnrollerRegObj model)
        {
            var errorLists = new List<string>();
            try
            {
                Session["_Enroller"] = model;
                if (!ModelState.IsValid)
                {
                    Session["_Enroller"] = model;

                    errorLists = (from value in ViewData.ModelState.Values
                                  where value.Errors.Count > 0
                                  from error in value.Errors
                                  where !string.IsNullOrEmpty(error.ErrorMessage)
                                  select error.ErrorMessage).ToList();

                    Session["CreateErrors"] = errorLists;
                    return Redirect(Url.RouteUrl(new { action = "AddEnroller" }));
                }

                var helper = new Enroller
                {
                    Surname = model.Surname,
                    FirstName = model.FirstName,
                    OtherNames = model.OtherNames,
                    Email = model.Email,
                    MobileNumber = model.MobileNumber,
                    Address = model.Address,
                    Sex = model.Sex,
                    EnrollerRegId = model.EnrollerRegId,
                    ClientStationId = model.ClientStationId,
                    //OrganizationId = model.OrganizationId,
                    UserName = model.UserName,
                    Password = model.Password,
                    RegisteredByUserId = 1
                };

                string msg;
                var retId = ServiceProvider.Instance().GetEnrollerServices().AddEnroller(helper, out  msg);
                if (retId < 1)
                {
                    ViewBag.ErrorMessage = string.IsNullOrEmpty(msg) ? "Unable to add new station's operator" : msg;
                    return View(model);
                }

                Session["_Enroller"] = null;
                Session["Reply"] = "Station's Operator Information was added successfully";
                return Redirect(Url.RouteUrl(new { action = "AddEnroller" }));

            }
            catch (Exception ex)
            {
                Session["CreateError"] = ex.Message;
                return Redirect(Url.RouteUrl(new { action = "AddEnroller" }));
            }
        }
	}
}