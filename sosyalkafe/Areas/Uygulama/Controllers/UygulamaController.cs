using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace sosyalkafe.Areas.Uygulama.Controllers
{
    public class UygulamaController : Controller
    {
        // GET: Uygulama/Uygulama
        public ActionResult Index()
        {
            string urlAddress = "https://www.instagram.com/explore/tags/hdss/"; //tagı dinamik olarak değiştir.
            string regexi = string.Format("display_src\": \"(.*?)\"");

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
            MatchCollection mth = Regex.Matches(data, regexi);

            List<string> listem = new List<string>();
            foreach (Match item in mth)
            {
                listem.Add(item.Groups[1].Value);
            }
            ViewBag.listem = listem;
            


            return View();
        }
    }
}