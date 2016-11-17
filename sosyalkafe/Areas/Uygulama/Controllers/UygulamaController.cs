using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using sosyalkafe.Models;

namespace sosyalkafe.Areas.Uygulama.Controllers
{
    public class UygulamaController : Controller
    {

        sosyalkafeEntities ent = new sosyalkafeEntities();

        // GET: Uygulama/Uygulama
        public ActionResult Index()
        {
            return Redirect(string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~")));
        }

        public ActionResult Gosteri(string firmaadi)
        {
            List<musteri_gonderileri> mgonderileri =
                (from a in ent.musteri_gonderileri
                 where a.firma_kodlari.aktif == 1 &&
                 a.firma_kodlari.firmalar.firma_kullaniciadi == firmaadi
                 select a).ToList();

            ViewBag.listem = mgonderileri;
            return View();
        }


    }
}