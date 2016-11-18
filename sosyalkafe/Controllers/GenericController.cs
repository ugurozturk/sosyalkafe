using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using sosyalkafe.Models;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;

namespace sosyalkafe.Controllers
{
    public class GenericController : Controller
    {
        sosyalkafeEntities ent = new sosyalkafeEntities();

        // GET: Generic
        public ActionResult Index()
        {
            return null;
        }

        public void ResimEkle(string firmaadi)
        {
            string firmaAdi = "";
            if (firmaadi != null)
            {
                firmaAdi = firmaadi;
            }

            
            string regexi = "\"likes\": (.*?) \"is_video\":";
            string likecountregexi = "\"likes\": {\"count\": (.*?)}";
            string thumbimageregexi = "thumbnail_src\": \"(.*?)\"";

            firma_kodlari firmaKodlari = (from a in ent.firma_kodlari
                                          where a.firmalar.firma_kullaniciadi == firmaAdi &&
                                          a.aktif == 1
                                          select a).ToList().LastOrDefault();

            if (firmaKodlari == null)
            {
                return;
            }

            string urlAddress = "https://www.instagram.com/explore/tags/" + firmaKodlari.firma_kod_adi;


            string data = "";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlAddress);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = null;

                if (response.CharacterSet == null)
                {
                    readStream = new StreamReader(receiveStream);
                }
                else
                {
                    readStream = new StreamReader(receiveStream, System.Text.Encoding.GetEncoding(response.CharacterSet));
                }

                data = readStream.ReadToEnd();

                response.Close();
                readStream.Close();
            }

            List<Tuple<string, string>> listem = new List<Tuple<string, string>>();
            MatchCollection mth = Regex.Matches(data, regexi);


            //Filtreleme ve Değerlendirme
            foreach (Match item in mth)
            {
                MatchCollection likecount = Regex.Matches(item.Groups[0].Value, likecountregexi);
                MatchCollection thumbimage = Regex.Matches(item.Groups[0].Value, thumbimageregexi);

                int ilikecount = 0;
                int.TryParse(likecount[0].Groups[1].Value, out ilikecount);

                if (ilikecount > 10) //Like sayısı 10 dan fazla ise
                {
                    bool resimVarmiSonuc = resimVarmi(thumbimage[0].Groups[1].Value);

                    if (resimVarmiSonuc)
                    {
                        continue; //Resim Varsa eklemeyi atla.
                    }

                    listem.Add(new Tuple<string, string>(
                        likecount[0].Groups[1].Value,
                        thumbimage[0].Groups[1].Value
                        ));
                }
            }


            //Verileri veritabanına yazdırma
            foreach (Tuple<string, string> veri in listem)
            {
                musteri_gonderileri mgonderi = new musteri_gonderileri();
                mgonderi.musteri_id = 1; //TODO sonradan dinamik hale getir.
                mgonderi.resim_adres = veri.Item2;
                mgonderi.aktif = 1; //TODO Kafenin isteğine bağlı olacak
                mgonderi.firma_kodlari_id = firmaKodlari.firma_kodlari_id;

                ent.musteri_gonderileri.Add(mgonderi);
                ent.SaveChanges();
            }
        }

        public bool resimVarmi(string resimAdres)
        {
            //Farklı Sunucudan çekilen aynı resimlerin çakılması önlendi.
            resimAdres = resimAdres.Substring(resimAdres.IndexOf("cdninstagram.com"), resimAdres.LastIndexOf("?ig_cache_key"));
            
            var veri = (from a in ent.musteri_gonderileri
                        where a.resim_adres.Contains(resimAdres)
                        select a).FirstOrDefault();

            if (veri == null)
            {
                return false;
            }

            return true;
        }
    }
}