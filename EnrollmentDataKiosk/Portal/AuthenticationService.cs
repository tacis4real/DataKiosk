using System;
using System.Web.Security;

namespace EnrollmentDataKiosk.Portal
{
    #region Authentication Service
    public interface IFormsAuthenticationService
    {
        string SignIn(string userName, bool createPersistentCookie, string userData);
        void SignOut();
    }

    public interface IEppFormsAuthenticationService
    {
        FormsAuthenticationTicket SignIn(string userName, bool createPersistentCookie, string userData);
        void SignOut();
    }

    public class FormsAuthenticationService : IFormsAuthenticationService
    {
        public string SignIn(string userName, bool createPersistentCookie, string userData)
        {
            if (string.IsNullOrEmpty(userName)) throw new ArgumentException("User name cannot be null");
            var ticket = new FormsAuthenticationTicket(1, userName, DateTime.Now, DateTime.Now.AddMinutes(20), createPersistentCookie, userData, FormsAuthentication.FormsCookiePath);
            var encTicket = FormsAuthentication.Encrypt(ticket);
            FormsAuthentication.SetAuthCookie(userName, createPersistentCookie);
            return encTicket;

        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();
        }
        
    }

    public class EppFormsAuthenticationService : IEppFormsAuthenticationService
    {
        public FormsAuthenticationTicket SignIn(string userName, bool createPersistentCookie, string userData)
        {
            if (string.IsNullOrEmpty(userName)) throw new ArgumentException("User name cannot be null");
            var ticket = new FormsAuthenticationTicket(1, userName, DateTime.Now, DateTime.Now.AddMinutes(20), createPersistentCookie, userData, FormsAuthentication.FormsCookiePath);
            FormsAuthentication.SetAuthCookie(userName, createPersistentCookie);
            return ticket;

        }

        public void SignOut()
        {

            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();
        }
        
    }

    #endregion

}