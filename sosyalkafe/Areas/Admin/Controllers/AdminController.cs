﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using sosyalkafe.Controllers;
using sosyalkafe.Models;

namespace sosyalkafe.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        sosyalkafeEntities ent = new sosyalkafeEntities();
        // GET: Admin/Admin
        public ActionResult Index()
        {
            if (Session["firmaid"] == null)
            {
                return Redirect("~");
            }

            string _firmaid = Session["firmaid"].ToString();
            int firmaid = int.Parse(_firmaid);

            var firmakodu = (from a in ent.firma_kodlari
                             where a.aktif == 1 &&
                             a.firma_id == firmaid
                             select a).FirstOrDefault();
            if (firmakodu != null)
            {
                ViewBag.firmaKodu = firmakodu.firma_kod_adi;
            }
            else
            {
                ViewBag.firmaKodu = ""; // Firmanın herhangi bir tagı yoksa
            }
            

            var musGonderileri = (from a in ent.musteri_gonderileri
                                                  where a.aktif == null && 
                                                  a.firma_kodlari.firma_id == firmaid &&
                                                  a.firma_kodlari.aktif == 1
                                  select a).ToList();


            return View(musGonderileri);
        }
        
        public JsonResult tagGuncelle(string firmaid, string tag)
        {
            //TODO Firma id ile gelen yerde ki firma id kontrol et
            //TODO Tagı güncelleyenin ip adresi alınacak
            string _firmaid = Session["firmaid"].ToString();
            int orjfirmaid = int.Parse(_firmaid);
            
            var kontrol = (from a in ent.firma_kodlari
                           where a.firma_kod_adi == tag &&
                           a.firma_id == orjfirmaid
                           select a).FirstOrDefault();

            if (kontrol != null)
            {
                kontrol.aktif = 1;
                (from a in ent.firma_kodlari
                 where a.firma_kodlari_id != kontrol.firma_kodlari_id &&
                 a.firma_id == orjfirmaid
                 select a).ToList().ForEach(x => x.aktif = 0);
                ent.SaveChanges();
                
            }
            else
            {
                (from a in ent.firma_kodlari
                 where a.firma_id == orjfirmaid
                 select a).ToList().ForEach(x => x.aktif = 0);

                firma_kodlari fkodu = new firma_kodlari();
                fkodu.aktif = 1;
                fkodu.firma_id = orjfirmaid;
                fkodu.firma_kod_adi = tag;
                fkodu.kod_tipi_id = 1;
                fkodu.kayit_tarihi = DateTime.Now;

                ent.firma_kodlari.Add(fkodu);
                ent.SaveChanges();
            }
            string firmaKullaniciAdi = Session["firmakullanici"].ToString();
            Guncelle(firmaKullaniciAdi);
            return Json(new { Result = "OK" });
        }
        
        public void Guncelle(string firmaadi)
        {
            GenericController generic = new GenericController();
            generic.ResimEkle(firmaadi);
        }

        public void TumunuGuncelle()
        {
            var lfirmalar = (from a in ent.firmalar
                            where a.aktif == 1
                            select a).ToList();
            foreach (firmalar item in lfirmalar)
            {
                GenericController generic = new GenericController();
                generic.ResimEkle(item.firma_kullaniciadi);
            }
            
        }

        public JsonResult resimOnay(string id)
        {
            //TODO resim onaylayanın ip adresi alınacak
            int resimid = 0;

            int.TryParse(id, out resimid);
            if (resimid == 0)
            {
                //TODO logla
                return Json(new { Result = "ERROR" });
            }

            musteri_gonderileri mgon = (from a in ent.musteri_gonderileri
                                        where a.musteri_gonderi_id == resimid
                                        select a).FirstOrDefault();
            mgon.aktif = 1;
            ent.SaveChanges();

            return Json(new { Result = "OK" });
        }

        public JsonResult resimiptal(string id)
        {
            //TODO resmi iptal edenin ip adresi alınacak
            int resimid = 0;

            int.TryParse(id, out resimid);
            if (resimid == 0)
            {
                //TODO logla
                return Json(new { Result = "ERROR" });
            }

            musteri_gonderileri mgon = (from a in ent.musteri_gonderileri
                                        where a.musteri_gonderi_id == resimid
                                        select a).FirstOrDefault();
            mgon.aktif = 0;
            ent.SaveChanges();

            return Json(new { Result = "OK" });
        }
    }
}