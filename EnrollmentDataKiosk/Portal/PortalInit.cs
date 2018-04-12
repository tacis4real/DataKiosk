using System;
using DataKioskStacks.APIs;
using WebCribs.TechCracker.WebCribs.TechCracker;

namespace EnrollmentDataKiosk.Portal
{
    public class PortalInit
    {
        public static bool InitPortal(out string msg)
        {
            try
            {
                //AutomaticMigrator.Update(out msg);
                var user = PortalUser.GetUser("useradmin");
                if (user == null)
                {
                    msg = "";
                    return false;
                }
                msg = "";
                return user.IsApproved;
            }
            catch (Exception ex)
            {
                BugManager.LogApplicationBug(ex.StackTrace, ex.Source, ex.Message);
                msg = ex.Message;
                return false;
            }

        }
    }
   
}