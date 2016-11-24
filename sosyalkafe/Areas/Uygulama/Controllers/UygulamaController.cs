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
                 a.firma_kodlari.firmalar.firma_kullaniciadi == firmaadi &&
                 a.aktif == 1
                 orderby a.musteri_gonderi_id descending
                 select a).Take(30).ToList();

            List<musteri_gonderileri> top3gonderi =
                (from a in mgonderileri
                 where a.populerlik_puani != null
                 orderby a.populerlik_puani descending
                 select a).Take(3).ToList();

            //Çektiğim top3 ü listeden sil.
            foreach (musteri_gonderileri item in top3gonderi)
            {
                mgonderileri.Remove(item);
            }

            //Kalan listeden 3ü çek.
            mgonderileri = mgonderileri.Take(3).ToList();

            //hepsini birleştir.
            mgonderileri.InsertRange(0, top3gonderi);
            
            ViewBag.listem = mgonderileri;
            if (mgonderileri.Count>0)
            {
                ViewBag.firmakodu = mgonderileri[0].firma_kodlari.firma_kod_adi;
            }
            
            return View();
        }
    }
}