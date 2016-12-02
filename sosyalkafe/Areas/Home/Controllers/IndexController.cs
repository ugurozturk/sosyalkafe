using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using sosyalkafe.Models;

namespace sosyalkafe.Areas.Home.Controllers
{
    public class IndexController : Controller
    {
        // GET: Home/Index
        public ActionResult Index()
        {
            Session.Abandon();
            return View();
        }

        [HttpPost]
        public ActionResult FirmaGirisi(string username, string password)
        {
            if (username.Length > 12) //Güvenlik amaçlı
            {
                return View();
            }
            
            sosyalkafeEntities ent = new sosyalkafeEntities();
            var firma = (from a in ent.firmalar
                         where a.firma_kullaniciadi == username &&
                         a.firma_sifresi == password
                         select a).FirstOrDefault();

            if (firma != null)
            {
                Session["firmaid"] = firma.firma_id;
                Session["firmakullanici"] = firma.firma_kullaniciadi;
            }
            else
            {
                return RedirectToAction("Index"); //TODO LOGLA
            }

            return Redirect("~/admin");
        }
    }
}