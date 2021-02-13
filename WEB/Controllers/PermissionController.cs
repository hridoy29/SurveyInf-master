using System;
using System.Collections.Generic;
using System.Web.Mvc;
using DbExecutor;
using Survey.Entity;
using SurveyDAL;
using SurveyEntity;

namespace Security.UI.Controllers
{


    public class PermissionController : Controller
    {
        public class temp
        {
            public int ModuleId { get; set; }
            public string ModuleName { get; set; }
            public int ScreenId { get; set; }
            public string ScreenName { get; set; }
            public string Description { get; set; }
            public List<LU_ScreenDetail> DetailList { get; set; }
        }
        public JsonResult GetAllScreen()
        {
            try
            {
                List<temp> t = new List<temp>();
                var list = Facade.LU_ScreenDAO.Get();
                for (int i = 0; i < list.Count; i++)
                {
                    temp tt = new temp();
                    var lst2 = Facade.LU_ScreenDetailDAO.GetByScreenId(list[i].ScreenId);
                    tt.ModuleId = list[i].ModuleId;
                    tt.ModuleName = list[i].ModuleName;
                    tt.ScreenId = list[i].ScreenId;
                    tt.ScreenName = list[i].ScreenName;
                    tt.Description = list[i].Description;
                    tt.DetailList = lst2;
                    t.Add(tt);


                }
                return Json(t, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                //error_Log error = new error_Log();
                //error.ErrorMessage = ex.Message;
                //error.ErrorType = ex.GetType().ToString();
                //error.FileName = "PermissionController";
                //  new ErrorLogController().CreateErrorLog(error);
                return Json(null, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult GetDetailByScreenId(int screenId)
        {
            try
            {
                var List = Facade.LU_ScreenDetailDAO.GetByScreenId(screenId);
                return Json(List, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                //error_Log error = new error_Log();
                //error.ErrorMessage = ex.Message;
                //error.ErrorType = ex.GetType().ToString();
                //error.FileName = "PermissionController";
                //new ErrorLogController().CreateErrorLog(error);
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetPermissionByRoleId(int roleId)
        {
            try
            {
                var permissionList = Facade.TRN_PermissionDAO.Get(roleId);

                return Json(permissionList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetDetailByPermissionId(int permissionId)
        {
            try
            {
                var List = Facade.TRN_PermissionDAO.Get(permissionId);
                return Json(List, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                //error_Log error = new error_Log();
                //error.ErrorMessage = ex.Message;
                //error.ErrorType = ex.GetType().ToString();
                //error.FileName = "PermissionController";
                //new ErrorLogController().CreateErrorLog(error);
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        /*
        [HttpPost]
        public int PermissionDeleteByRoleId(int roleId)
        {
            int ret = 0;
            try
            {
                ret = Facade.Permission.DeleteByRoleId(roleId);
            }
            catch (Exception ex)
            {
                error_Log error = new error_Log();
                error.ErrorMessage = ex.Message;
                error.ErrorType = ex.GetType().ToString();
                error.FileName = "PermissionController";
                new ErrorLogController().CreateErrorLog(error);
            }

            return ret;
        }
        
        [HttpPost]
        public int SavePermission(s_Permission permission)
        {
            int ret = 0;
            try
            {
                permission.CreateDate = DateTime.Now;
                permission.UpdateDate = DateTime.Now;
                ret = Facade.Permission.Add(permission);
            }
            catch (Exception ex)
            {
                error_Log error = new error_Log();
                error.ErrorMessage = ex.Message;
                error.ErrorType = ex.GetType().ToString();
                error.FileName = "PermissionController";
                new ErrorLogController().CreateErrorLog(error);
            }
            return ret;
        }

        [HttpPost]
        public int SavePermissionDtl(s_PermissionDetail detail)
        {
            int ret = 0;
            try
            {
                return Facade.PermissionDetail.Add(detail);

            }
            catch (Exception ex)
            {
                error_Log error = new error_Log();
                error.ErrorMessage = ex.Message;
                error.ErrorType = ex.GetType().ToString();
                error.FileName = "PermissionController";
                new ErrorLogController().CreateErrorLog(error);
            }
            return ret;
        }
        */

        [HttpPost]
        public int SavePermissionWithDetails(int roleId, List<TRN_Permission> PermissionLst, List<TRN_PermissionDetail> DetailList)
        {
            int ret = 0;
            Int64 ret1 = 0;
            using (System.Transactions.TransactionScope ts = new System.Transactions.TransactionScope())
            {

                try
                {
                    ret = Facade.TRN_PermissionDAO.DeleteByRoleId(roleId);
                    foreach (TRN_Permission aPermission in PermissionLst)
                    {
                        aPermission.UserGroupId = roleId;
                        aPermission.CreateDate = DateTime.Now;
                        aPermission.UpdateDate = DateTime.Now;
                        ret1 = Facade.TRN_PermissionDAO.Post(aPermission, "INSERT");

                        foreach (TRN_PermissionDetail aPermissionDetail in DetailList)
                        {
                            if (aPermission.ScreenId == aPermissionDetail.ScreenId)
                            {
                                aPermissionDetail.PermissionId = ret1;
                                Facade.TRN_PermissionDetailDAO.Post(aPermissionDetail, "INSERT");
                            }
                        }
                    }

                    ts.Complete();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return ret;

            }
            //Screen Lock

        }




        public JsonResult GetUsersPermissionDetails(string searchCriteria, string orderBy)
        {
            try
            {
                var list = Facade.TRN_PermissionDetailDAO.GetDynamic(searchCriteria, orderBy);
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                //error_Log error = new error_Log();
                //error.ErrorMessage = ex.Message;
                //error.ErrorType = ex.GetType().ToString();
                //error.FileName = "PermissionController";
                //new ErrorLogController().CreateErrorLog(error);
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
