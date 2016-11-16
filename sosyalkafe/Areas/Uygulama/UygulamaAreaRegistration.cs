using System.Web.Mvc;

namespace sosyalkafe.Areas.Uygulama
{
    public class UygulamaAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Uygulama";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Uygulama_default",
                "Uygulama/{controller}/{action}/{id}",
                new { controller = "Uygulama", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}