using Microsoft.Web.Administration;


namespace IISProcessScheduler.Configuration.IIS
{
    public static class IISHelpers
    {
        public static void SetAutoStartProvider()
        {
            var manager = new ServerManager();
            Microsoft.Web.Administration.Configuration config = manager.GetApplicationHostConfiguration();
            ConfigurationSection configSection = config.GetSection("system.applicationHost/serviceAutoStartProviders");
            foreach (var provider in configSection.GetCollection())
            {
                if (provider["name"].ToString() == "IISProcessSchedulerPreWarmUp")
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
            ConfigurationSection configSection = config.GetSection("system.applicationHost/sites");
            foreach (var site in configSection.GetCollection())
            {
                foreach (var application in site.GetCollection())
                {
                    if (application.Schema.Name == "application" && application["path"].ToString() == siteUri)
                    {
                        application.SetAttributeValue("serviceAutoStartEnabled",enabled);
                        application.SetAttributeValue("serviceAutoStartProvider", "IISProcessSchedulerPreWarmUp");
                    }
                }
            }
            manager.CommitChanges();
        }
    }
}