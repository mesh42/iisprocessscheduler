using System;

namespace IISProcessScheduler.Shared.UserControls
{
    public partial class ServiceStatusControl : System.Web.UI.UserControl
    {
        public Default.IProcessInfo ProcessInfo
        {
            set
            {
                switch (value.Result)
                {
                    case Default.EnumProcessResult.Error:
                        Image1.ImageUrl = "~/App_Themes/Default/Images/Icons/Error24.png";
                        break;
                    case Default.EnumProcessResult.Timeout:
                        Image1.ImageUrl = "~/App_Themes/Default/Images/Icons/TimeOut24.png";
                        break;
                    case Default.EnumProcessResult.Healthy:
                        Image1.ImageUrl = "~/App_Themes/Default/Images/Icons/Healthy24.png";
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