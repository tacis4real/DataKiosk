using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Caching;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using DataKioskStacks.APIs;
using EnrollmentDataKiosk.App_Start;
using EnrollmentDataKiosk.Models.PortalModel;
using WebCribs.TechCracker.WebCribs.TechCracker;

namespace EnrollmentDataKiosk
{
    public class MvcApplication : HttpApplication
    {

        public static Hashtable ThisPortalDefaultSettings;
        public static Hashtable RemoteServerAuthTable;

        void Application_BeginRequest(object sender, EventArgs e)
        {
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            HttpContext.Current.Response.Cache.SetNoStore();
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }



        void Application_AuthenticateRequest()
        {

            var myCookie = FormsAuthentication.FormsCookieName;
            var myAuthCookie = Context.Request.Cookies[myCookie];

            if (null == myAuthCookie)
            {
                return;
            }

            FormsAuthenticationTicket myAuthTicket;
            try
            {
                myAuthTicket = FormsAuthentication.Decrypt(myAuthCookie.Value);
            }
            catch (Exception ex)
            {
                BugManager.LogApplicationBug(ex.StackTrace, ex.Source, ex.Message);
                return;
            }

            if (null == myAuthTicket)
            {
                return;
            }


            var userDataSplit = myAuthTicket.UserData.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            var size = userDataSplit.Length;

            //if (!userDataSplit.Any() || userDataSplit.Length != 3)
            //if (!userDataSplit.Any())
            //{
            //    if ((size != 3 || size != 4))
            //    {
            //        return;
            //    }
            //}


            if (!userDataSplit.Any() || userDataSplit.Length != 3)
            {
                return;
            }

            if (!DataCheck.IsNumeric(userDataSplit[0].Trim()))
            {
                return;
            }

            //switch (size)
            //{
            //    case 3:
            //        var roles = userDataSplit[2].Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            //        var id = new FormsIdentity(myAuthTicket);
            //        //IPrincipal principal = new StcPrincipal(id, roles);
            //        //Context.User = principal;
            //        break;

            //    case 4:
            //        var clientRoles = userDataSplit[3].Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            //        var clientId = new FormsIdentity(myAuthTicket);
            //        //IPrincipal clientPrincipal = new StcPrincipal(clientId, clientRoles);
            //        //Context.User = clientPrincipal;
            //        break;
            //}

            //var roles = userDataSplit[2].Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            var role = userDataSplit[2];

            var id = new FormsIdentity(myAuthTicket);
            IPrincipal principal = new StcPrincipal(id, role);
            Context.User = principal;



        }
        void Application_PreRequestHandlerExecute(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session != null)
            {
                //This extends cache timeout due to recent access of the cache data
                var sKey = Session["UserINFO"] as string;
                var dKey = Session["UserDATAINFO"] as string;

                //var mKey = Session["ClientINFO"] as string;
                //var nKey = Session["ClientDATAINFO"] as string;

                if (!string.IsNullOrEmpty(sKey))
                {
                    var sUser = HttpContext.Current.Cache[sKey] as string;
                }
                if (!string.IsNullOrEmpty(dKey))
                {
                    var xcode = "DATAKIOSK_USER_DATA_" + dKey;
                    //var sUserData = HttpContext.Current.Cache[xcode] as UserData;
                }

                
            }
            else
            {
                foreach (var nItem in HttpContext.Current.Cache.OfType<string>())
                {
                    HttpContext.Current.Cache.Remove(nItem);
                }
                //foreach (var nItem in HttpContext.Current.Cache.OfType<UserData>())
                //{
                //    if (nItem != null)
                //    {
                //        var xcode = "DATAKIOSK_USER_DATA_" + nItem.Username;
                //        HttpContext.Current.Cache.Remove(xcode);
                //    }
                //}
                
            }
        }

        void Session_Start(object sender, EventArgs e)
        {
            ThisPortalDefaultSettings = new Hashtable();
        }

        void Session_End(object sender, EventArgs e)
        {
            ThisPortalDefaultSettings = new Hashtable();

        }


        public static bool IsUserAlreadyLoggedIn(string code, out string msg)
        {
            var storedUser = Convert.ToString(HttpContext.Current.Cache[code]);
            if (string.IsNullOrEmpty(storedUser))
            {
                var timeout = new TimeSpan(0, 0, HttpContext.Current.Session.Timeout, 0, 0);
                HttpContext.Current.Cache.Insert(code, code, null, DateTime.MaxValue, timeout, CacheItemPriority.High, null);
                msg = "";
                return false;
            }

            msg = "Multiple Login Not Allowed!";
            return true;

        }

        public static bool SetUserData(UserData userData)
        {
            try
            {
                if (userData == null)
                {
                    return false;
                }

                var usercode = "DATAKIOSK_USER_DATA_" + userData.Email;


                if (HttpContext.Current.Cache[usercode] != null)
                {
                    HttpContext.Current.Cache.Remove(usercode);
                }

                var timeout = new TimeSpan(0, 0, HttpContext.Current.Session.Timeout, 0, 0);
                HttpContext.Current.Cache.Insert(usercode, userData, null, DateTime.MaxValue, timeout, CacheItemPriority.High, null);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public static UserData GetUserData(string username)
        {
            try
            {
                if (string.IsNullOrEmpty(username))
                {
                    return null;
                }
                var usercode = "DATAKIOSK_USER_DATA_" + username;
                var userData = HttpContext.Current.Cache[usercode] as UserData;
                if (userData == null || userData.UserId < 1)
                {
                    return null;
                }
                return userData;

            }
            catch (Exception)
            {
                return null;
            }

        }
        public static void ResetLogin(string code)
        {
            try
            {
                HttpContext.Current.Cache[code] = null;

            }
            catch (Exception)
            {

            }
        }
        public static void ResetUserData(string username)
        {
            try
            {
                HttpContext.Current.Cache["DATAKIOSK_USER_DATA_" + username] = null;
            }
            catch (Exception)
            {

            }
        }
    }
}
