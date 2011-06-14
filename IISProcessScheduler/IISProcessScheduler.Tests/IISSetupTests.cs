using System.Linq;
using IISProcessScheduler.Configuration.IIS;
using Microsoft.Web.Administration;
using NUnit.Framework;
using Site = System.Security.Policy.Site;

namespace IISProcessScheduler.Tests
{
    [TestFixture]
    public class IISSetupTests
    {
        [Test]
        public void GetSiteTest()
        {
            IISHelpers.SetAutoStartProvider();
            IISHelpers.SetAutoStart("/IISProcessScheduler",true);
        }



        [Test]
        public void GetConfigurrationAttribute()
        {
            ServerManager manager = new ServerManager();
            Microsoft.Web.Administration.Configuration config = manager.GetApplicationHostConfiguration();
            ConfigurationSection configSection = config.GetSection("system.applicationHost/serviceAutoStartProviders");
            
            foreach (var item in configSection.GetCollection())
            {
                if (item.Attributes["type"].Value.ToString() == "IISProcessScheduler.PreWarmUp, IISProcessScheduler")
                {
                    
                }

            }


            ConfigurationAttributeCollection configAttributeCollection =
                configSection.Attributes;



        

        }

        [Test]
        public void RegisterWarmup()
        {
            //Microsoft.Web.Administration.

            using (ServerManager svr = ServerManager.OpenRemote("localhost"))
            {
                Application scheduler;

               foreach (var app in
                    from site in svr.Sites
                    from app in site.Applications
                    where app.Path == "/IISProcessScheduler"
                    select app)
                {
                    scheduler = app;
                }




                //Site currentSite = svr.Sites.Where(item => item.Name == args.WebSite.Id).FirstOrDefault();
                //currentSite.Applications[0].VirtualDirectories[0].PhysicalPath = args.WebSite.PhysicalPath;
                //svr.CommitChanges();
            }
        }
    }
}
