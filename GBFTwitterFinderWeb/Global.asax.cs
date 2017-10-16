using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using GBFTwitterFinderWeb.Finder;
using GBFTwitterFinderWeb.Hubs;
using GBFTwitterFinderWeb.Models;

namespace GBFTwitterFinderWeb
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static TwitterFinder _twitterFinder;
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //_twitterFinder = _twitterFinder ?? new TwitterFinder();
            //_twitterFinder.OnBattleFound += (name, id, fullText) =>
            //{
            //    TwitterHub.SendBattleInfo(fullText);
            //};
            //_twitterFinder.Execute(GlobalStorage.BossDatas);
        }

        protected void Application_End()
        {
            _twitterFinder?.Stop();
        }
    }
}
