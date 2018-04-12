﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EnrollmentDataKiosk.Portal;

namespace EnrollmentDataKiosk.Controllers.Framework
{
    public class SignOutController : Controller
    {

        //
        // GET: /Admin/SignOut/
        public ActionResult Index(string retUrl)
        {
            //empty all persistent variables & sessions
            Session["UserINFO"] = null;
            new FormsAuthenticationService().SignOut();
            return RedirectToAction("Login", "PortalCom", new { returnUrl = retUrl });
        }

	}
}