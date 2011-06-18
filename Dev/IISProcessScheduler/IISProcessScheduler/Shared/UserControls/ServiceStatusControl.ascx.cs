using System;
using System.Web.UI;

namespace IISProcessScheduler.Shared.UserControls
{
    [Themeable(true)]
    public partial class ServiceStatusControl : System.Web.UI.UserControl
    {
        public string ImageError { get; set; }
        public string ImageTimeout { get; set; }
        public string ImageHealthy { get; set; }


        public Default.IProcessInfo ProcessInfo
        {
            set
            {
                switch (value.Result)
                {
                    case Default.EnumProcessResult.Error:
                        Image1.ImageUrl = ImageError;
                        break;
                    case Default.EnumProcessResult.Timeout:
                        Image1.ImageUrl = ImageTimeout;
                        break;
                    case Default.EnumProcessResult.Healthy:
                        Image1.ImageUrl = ImageHealthy;
                        break;

                }
                Image1.AlternateText = Server.UrlEncode(value.Description);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}