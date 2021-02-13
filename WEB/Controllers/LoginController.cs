using SurveyDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace WEB.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        [HttpPost]
        public string SetIsAuthentic(string isAuthentic)
        {
            try
            {
                System.Web.HttpContext.Current.Session["IsAuthentic"] = isAuthentic;
                return "success";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        public JsonResult GetWebUserForLogin(string email, string passcode)
        {
            try
            {
                var list = Facade.TRN_PermissionDetailDAO.GetWebUserForLogin(email, passcode);
                string contentType = "application/json";
                return Json(list, contentType, Encoding.UTF8, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetByUserGroupId(int userGroupId)
        {
            try
            {
                var list = Facade.TRN_PermissionDetailDAO.GetByUserGroupId(userGroupId);
                string contentType = "application/json";
                return Json(list, contentType, Encoding.UTF8, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        //public JsonResult GetMaintenanceInfo()
        //{
        //    try
        //    {
        //        var maintenanceInfo = System.Configuration.ConfigurationManager.AppSettings["IsMaintenance"].ToString() + "~" 
        //            + System.Configuration.ConfigurationManager.AppSettings["MaintenanceMessage"].ToString() + "~" 
        //            + System.Configuration.ConfigurationManager.AppSettings["MaintenancePin"].ToString();

        //        return Json(maintenanceInfo, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(null, JsonRequestBehavior.AllowGet);
        //    }
        //}

        //[HttpPost]
        //public int PostAuditLog(string loginEmpNo, string empNo, string module, string subModule, string actionSummary)
        //{
        //    int ret = 0;

        //    try
        //    {
        //        ret = Facade.PerformanceReview_EmployeeDAO.PostAuditLog(loginEmpNo, empNo, module, subModule, actionSummary);
        //        return ret;
        //    }
        //    catch (Exception)
        //    {
        //        return ret;
        //    }
        //}
    }
}