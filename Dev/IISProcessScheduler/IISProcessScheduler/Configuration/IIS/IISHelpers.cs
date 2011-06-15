using System.Linq;
using Microsoft.Web.Administration;


namespace IISProcessScheduler.Configuration.IIS
{
    public static class IISHelpers
    {
        public static void SetAutoStartProvider()
        {
            var manager = new ServerManager();
            Microsoft.Web.Administration.Configuration config = manager.GetApplicationHostConfiguration();
            var configSection = config.GetSection("system.applicationHost/serviceAutoStartProviders");
            if (configSection.GetCollection().Any(provider => provider["name"].ToString() == "IISProcessSchedulerPreWarmUp"))
            {
                return;
            }
            var element = configSection.GetCollection().CreateElement("add");
            element.SetAttributeValue("name", "IISProcessSchedulerPreWarmUp");
            element.SetAttributeValue("type", "IISProcessScheduler.PreWarmUp, IISProcessScheduler");
            configSection.GetCollection().Add(element);
            manager.CommitChanges();

        }

        public static void SetAutoStart(string siteUri, bool enabled)
        {
            var manager = new ServerManager();
            Microsoft.Web.Administration.Configuration config = manager.GetApplicationHostConfiguration();
            var configSection = config.GetSection("system.applicationHost/sites");
            foreach (var application in from site in configSection.GetCollection()
                                        from application in site.GetCollection()
                                        where application.Schema.Name == "application" && application["path"].ToString() == siteUri
                                        select application)
            {
                application.SetAttributeValue("serviceAutoStartEnabled",enabled);
                application.SetAttributeValue("serviceAutoStartProvider", "IISProcessSchedulerPreWarmUp");
            }
            manager.CommitChanges();
        }
    }
}