using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using sosyalkafe.Controllers;

namespace sosyalkafe.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin/Admin
        public ActionResult Index()
        {
            return View();
        }

        public void Guncelle(string firmaadi)
        {
            GenericController generic = new GenericController();
            generic.ResimEkle(firmaadi);
        }
    }
}