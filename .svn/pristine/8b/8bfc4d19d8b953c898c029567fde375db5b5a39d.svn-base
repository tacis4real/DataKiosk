using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.Hosting;
using DataKioskStacks.Service.Contract;
using WebCribs.TechCracker.WebCribs.TechCracker;

namespace DataKioskStacks.Repository.Helpers
{
    public class EnrollHelper
    {

        public static List<byte[]> ExtractFingerTemplates()
        {

            var retItems = new List<byte[]>();
            try
            {
                //if (Utils.CapturedTemplates == null)
                //{
                //    return new List<byte[]>();
                //}

                //var templates = Utils.CapturedTemplates;
                //if (templates == null) return new List<byte[]>();
               

                //foreach (var template in templates)
                //{
                //    if (template != null)
                //    {
                //        var fingerprintData = new MemoryStream();
                //        template.Serialize(fingerprintData);
                //        retItems.Add(fingerprintData.ToArray());
                //    }
                //}

                //if (!retItems.Any())
                //{
                //    return new List<byte[]>();
                //}

                return retItems;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static string PrepareImagePath(string org, string stationId, string enrollerId, out string msg)
        {

            try
            {
                var year = DateTime.Now.Year.ToString(CultureInfo.InvariantCulture);
                var imgName = DateTime.Now.Ticks;
                var fileName = imgName + ".jpeg";

                #region Folder Structure For Storage

                #region Local

                //var stationKey = (ServiceProvider.Instance().GetStationInfoService().GetStationInfos() ?? new List<StationInfo>())[0].StationKey;
                //var username = Utils.CurrentUser.UserName;

                //var fileName = imgName;
                ////TODO:Resourse path
                //string folderPath = "/Station-" + stationKey + "/" + username + "/" + year + "/" + "/Image/";
                //var imageResPath = folderPath + fileName;
                //var dir = InternetCon.GetBasePath() + ConfigurationManager.AppSettings["BioResource"];
                //var imagePath = Path.GetFullPath(dir + imageResPath);
                //if (!Directory.Exists(@dir + @folderPath))
                //{
                //    try
                //    {
                //        Directory.CreateDirectory(@dir + @folderPath);
                //    }
                //    catch (Exception ex)
                //    {
                //        ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                //        msg = ex.Message;
                //        return;
                //    }
                //}
                //while (File.Exists(imagePath))
                //{
                //    fileName = DateTime.Now.Ticks + ".jpeg";
                //    imageResPath = folderPath + fileName;
                //    imagePath = Path.GetFullPath(dir + imageResPath);
                //}


                #endregion


                #region Remote

                org = org.Replace(" ", "_");
                stationId = stationId.Replace(" ", "_");
                enrollerId = enrollerId.Replace(" ", "_");
                string resPath = "~/KioskResources/";
                string folderPath = org + "/" + stationId + "/" + enrollerId + "/" + year + "/" + "/Image/";
                var imageResPath = folderPath + fileName;

                //var imagePath = Path.GetFullPath(resPath + imageResPath);
                //var dir = InternetCon.GetBasePath() + ConfigurationManager.AppSettings["BioResource"];

                var dirPath = HostingEnvironment.MapPath(resPath + folderPath);
                var imagePath = HostingEnvironment.MapPath(resPath + imageResPath);
                if (@dirPath != null && !Directory.Exists(@dirPath))
                {
                    try
                    {
                        Directory.CreateDirectory(@dirPath);
                    }
                    catch (Exception ex)
                    {
                        BugManager.LogApplicationBug(ex.StackTrace, ex.Source, ex.Message);
                        msg = ex.Message;
                        return null;
                    }
                }
                while (File.Exists(imagePath))
                {
                    fileName = DateTime.Now.Ticks + ".jpeg";
                    imageResPath = folderPath + fileName;
                    imagePath = HostingEnvironment.MapPath(resPath + imageResPath);
                    //imagePath = Path.GetFullPath(@dirPath + imageResPath);
                }


                //if (dirPath == null || !Directory.Exists(dirPath))
                //{
                //    var newDirPath = Directory.CreateDirectory(resPath);
                //    logoPath = newDirPath.ToString();
                //}

                #endregion


                #endregion

                if (imagePath == null)
                {
                    msg = "Path not exist";
                    return null;
                }

                //image.Save(imagePath, ImageFormat.Jpeg);
                msg = "";
                return imagePath;
            }
            catch (Exception ex)
            {
                BugManager.LogApplicationBug(ex.StackTrace, ex.Source, ex.Message);
                msg = ex.Message;
                return null;
            }

        }
        public static string SaveImage(Image image, string org, string stationId, string enrollerId, string username, out string msg)
        {

            try
            {
                var year = DateTime.Now.Year.ToString(CultureInfo.InvariantCulture);
                var imgName = DateTime.Now.Ticks;
                var fileName = imgName + ".jpeg";
                
                #region Folder Structure For Storage

                #region Local

                //var stationKey = (ServiceProvider.Instance().GetStationInfoService().GetStationInfos() ?? new List<StationInfo>())[0].StationKey;
                //var username = Utils.CurrentUser.UserName;
               
                //var fileName = imgName;
                ////TODO:Resourse path
                //string folderPath = "/Station-" + stationKey + "/" + username + "/" + year + "/" + "/Image/";
                //var imageResPath = folderPath + fileName;
                //var dir = InternetCon.GetBasePath() + ConfigurationManager.AppSettings["BioResource"];
                //var imagePath = Path.GetFullPath(dir + imageResPath);
                //if (!Directory.Exists(@dir + @folderPath))
                //{
                //    try
                //    {
                //        Directory.CreateDirectory(@dir + @folderPath);
                //    }
                //    catch (Exception ex)
                //    {
                //        ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                //        msg = ex.Message;
                //        return;
                //    }
                //}
                //while (File.Exists(imagePath))
                //{
                //    fileName = DateTime.Now.Ticks + ".jpeg";
                //    imageResPath = folderPath + fileName;
                //    imagePath = Path.GetFullPath(dir + imageResPath);
                //}


                #endregion


                #region Remote

                org = org.Replace(" ", "_");
                stationId = stationId.Replace(" ", "_");
                enrollerId = enrollerId.Replace(" ", "_");
                string resPath = "~/KioskResources/";
                string folderPath = org + "/" + stationId + "/" + enrollerId + "/" + year + "/" + "/Image/";
                var imageResPath = folderPath + fileName;
                //var imagePath = Path.GetFullPath(resPath + imageResPath);
                var imagePath = HostingEnvironment.MapPath(resPath + imageResPath);
                //var dir = InternetCon.GetBasePath() + ConfigurationManager.AppSettings["BioResource"];
                var dirPath = HostingEnvironment.MapPath(resPath + folderPath);
                if (!Directory.Exists(@dirPath + @folderPath))
                {
                    try
                    {
                        Directory.CreateDirectory(@dirPath + @folderPath);
                    }
                    catch (Exception ex)
                    {
                        BugManager.LogApplicationBug(ex.StackTrace, ex.Source, ex.Message);
                        msg = ex.Message;
                        return null;
                    }
                }
                while (File.Exists(imagePath))
                {
                    fileName = DateTime.Now.Ticks + ".jpeg";
                    imageResPath = folderPath + fileName;
                    imagePath = Path.GetFullPath(@dirPath + imageResPath);
                }


                //if (dirPath == null || !Directory.Exists(dirPath))
                //{
                //    var newDirPath = Directory.CreateDirectory(resPath);
                //    logoPath = newDirPath.ToString();
                //}

                #endregion
                

                #endregion

                if (imagePath == null)
                {
                    msg = "Path not exist";
                    return null;
                }

                image.Save(imagePath, ImageFormat.Jpeg);
                msg = "";
                return imagePath;
            }
            catch (Exception ex)
            {
                BugManager.LogApplicationBug(ex.StackTrace, ex.Source, ex.Message);
                msg = ex.Message;
                return null;
            }

        }

        public static string SaveImageLocally(Image image, string stationKey, string username, out string msg)
        {

            try
            {
                #region Folder Structure For Storage

                //var stationKey = (ServiceProvider.Instance().GetStationInfoService().GetStationInfos() ?? new List<StationInfo>())[0].StationKey;
                //var username = Utils.CurrentUser.UserName;
                var year = DateTime.Now.Year.ToString(CultureInfo.InvariantCulture);

                var imgName = DateTime.Now.Ticks;
                var fileName = imgName + ".jpeg";
                //var fileName = imgName;
                //TODO:Resourse path
                string folderPath = "/Station-" + stationKey + "/" + username + "/" + year + "/" + "/Image/";
                var imageResPath = folderPath + fileName;
                //var dir = InternetCon.GetBasePath() + ConfigurationManager.AppSettings["BioResource"];
                var dir = "";
                var imagePath = Path.GetFullPath(dir + imageResPath);
                if (!Directory.Exists(@dir + @folderPath))
                {
                    try
                    {
                        Directory.CreateDirectory(@dir + @folderPath);
                    }
                    catch (Exception ex)
                    {
                        BugManager.LogApplicationBug(ex.StackTrace, ex.Source, ex.Message);
                        msg = ex.Message;
                        return null;
                    }
                }
                while (File.Exists(imagePath))
                {
                    fileName = DateTime.Now.Ticks + ".jpeg";
                    imageResPath = folderPath + fileName;
                    imagePath = Path.GetFullPath(dir + imageResPath);
                }


                #endregion

                image.Save(imagePath, ImageFormat.Jpeg);
                msg = "";
                return imagePath;
                //File.WriteAllBytes(@imagePath, imageByteArray);
            }
            catch (Exception ex)
            {
                BugManager.LogApplicationBug(ex.StackTrace, ex.Source, ex.Message);
                msg = "Processing Error Occurred! " +ex.Message;
                return null;
            }

        }



        #region Unsed

        //public static Template[] ConvertByteListToTemplate(List<byte[]> list)
        //{

        //    var template = new Template[10];
        //    try
        //    {

        //        if (!list.Any())
        //        {
        //            return new Template[10];
        //        }

        //        return template;
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
        //        //msg = ex.Message;
        //        return null;
        //    }

        //}


        //private Image RetrieveImage(string path)
        //{


        //    #region Folder Structure For Storage

        //    var stationKey = (ServiceProvider.Instance().GetStationInfoService().GetStationInfos() ?? new List<StationInfo>())[0].StationKey;
        //    var username = Utils.CurrentUser.UserName;
        //    var year = DateTime.Now.Year.ToString(CultureInfo.InvariantCulture);
        //    //var username = "useradmin";


        //    #endregion

        //    const string imgName = "636549040395366983";
        //    const string fileName = imgName + ".jpeg";
        //    //var fileName = imgName;
        //    //TODO:Resourse path
        //    string folderPath = "/Station-" + stationKey + "/" + username + "/" + year + "/" + "/Image/";
        //    var imageResPath = folderPath + fileName;
        //    var dir = InternetCon.GetBasePath() + ConfigurationManager.AppSettings["BioResource"];
        //    var imagePath = Path.GetFullPath(dir + imageResPath);

        //    var img = Image.FromFile(path);
        //    return img;
        //    //var img = Image.FromFile(imagePath);
        //    //using (var img = Image.FromFile(imagePath))
        //    //{
        //    //    picImage.Image = new Bitmap(img);
        //    //}


        //}

        #endregion
        
    }
}
