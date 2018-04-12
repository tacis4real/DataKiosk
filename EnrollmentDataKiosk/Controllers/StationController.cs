using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataKioskStacks.APIServer.APIObjs;
using DataKioskStacks.DataContract;
using DataKioskStacks.Service.Contract;
using WebCribs.TechCracker.WebCribs.TechCracker;

namespace EnrollmentDataKiosk.Controllers
{
    public class StationController : Controller
    {
        //
        // GET: /Station/
        //public ActionResult Index()
        //{
        //    return View();
        //}


        public ActionResult AddStation()
        {


            #region Test Authorization

            //var ben = ServiceProvider.Instance().GetBeneficiaryServices().GetBeneficiary(1);
            //if (ben == null)
            //{
                
            //}

            //var authParameter = new AccessParameter
            //{
            //    DeviceIP = "192.168.8.38",
            //    DeviceId = "7C0507CF9FC0",
            //    StationName = "Upa River Curtain",
            //    StationId = "UBA-RV",
            //    Surname = "Agbure",
            //    FirstName = "Shelly",
            //    UserName = "shelshel",
            //    MobileNumber = "08034256634",
            //    Email = "shelly@gmail.com"
            //};

            //var authParameter = new StationRegObj
            //{
            //    DeviceIP = "192.168.8.38",
            //    DeviceId = "7C0507CF9FC0",
            //    StationName = "Upa River Curtain",
            //    StationId = "UBA-RV",
            //    Surname = "Agbure",
            //    FirstName = "Shelly",
            //    UserName = "shelshel",
            //    MobileNumber = "08034256634",
            //    Email = "shelly@gmail.com"
            //};

            //var resp = ServiceProvider.Instance().GetClientStationServices().RegisterStation(authParameter);
            //return View(); 
            #endregion
            

            ViewBag.Errors = Session["CreateErrors"] as List<string>;
            ViewBag.Error = Session["CreateError"] as string;
            Session["CreateErrors"] = "";
            Session["CreateError"] = "";
            if (Session["_NewStation"] == null)
            {
                var station = new ClientStation { APIAccessKey = "4567382910", Status = 1 };
                return View(station);
            }

            var model = Session["_NewStation"] as ClientStation;
            return View(model);
        }

        [HttpPost]
        public ActionResult AddStation(ClientStation model)
        {

            var errorLists = new List<string>();
            try
            {

                Session["_NewStation"] = model;
                if (!ModelState.IsValid)
                {
                    Session["_NewStation"] = model;

                    errorLists = (from value in ViewData.ModelState.Values
                                  where value.Errors.Count > 0
                                  from error in value.Errors
                                  where !string.IsNullOrEmpty(error.ErrorMessage)
                                  select error.ErrorMessage).ToList();

                    Session["CreateErrors"] = errorLists;
                    return Redirect(Url.RouteUrl(new { action = "AddStation" }));
                }

                var helper = new ClientStation
                {
                    StationName = model.StationName,
                    OrganizationId = model.OrganizationId,
                    StateId = model.StateId,
                    LocalAreaId = model.LocalAreaId,
                    StationId = model.StationId,
                    RegisteredByUserId = 1,
                };

                string msg;
                var retId = ServiceProvider.Instance().GetClientStationServices().AddClientStation(helper, out  msg);

                if (retId < 1)
                {
                    ViewBag.ErrorMessage = string.IsNullOrEmpty(msg) ? "Unable to add new client station" : msg;
                    return View(model);
                }

                Session["_NewStation"] = null;
                Session["Reply"] = "Station Information was added successfully";
                return Redirect(Url.RouteUrl(new { action = "AddStation" }));

            }
            catch (Exception ex)
            {
                Session["CreateError"] = ex.Message;
                return Redirect(Url.RouteUrl(new { action = "AddStation" }));
            }
        }


	}
}