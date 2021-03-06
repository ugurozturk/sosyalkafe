﻿using System.Web.Mvc;

namespace sosyalkafe.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Admin_guncelle",
                "Admin/guncelle/{firmaadi}",
                new { controller = "admin", action = "Guncelle", firmaadi = UrlParameter.Optional }
            );

            context.MapRoute(
                "Admin_ayarlar",
                "Admin/ayarlar/{action}/{firmaadi}",
                new { controller = "ayarlar", action = "Index", firmaadi = UrlParameter.Optional }
            );

            context.MapRoute(
                "Admin_resimonay",
                "Admin/{action}/{firmaadi}",
                new { controller = "admin", action = "Index", firmaadi = UrlParameter.Optional }
            );
            
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new {controller = "admin", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}