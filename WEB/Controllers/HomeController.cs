 
 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace WEB.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (GetIsAuthentic() == "1")
            {
                return View();
            }
            else
            {
                return Login();
            }
        }

        public ActionResult Login()
        {
            return View();
        }

        public string GetIsAuthentic()
        {
            try
            {
                string isAuthentic = "0";
                if (System.Web.HttpContext.Current.Session["IsAuthentic"] != null)
                {
                    isAuthentic = System.Web.HttpContext.Current.Session["IsAuthentic"].ToString();
                }

                return isAuthentic;
            }
            catch (Exception)
            {
                return "0";
            }
        }       
    }
}