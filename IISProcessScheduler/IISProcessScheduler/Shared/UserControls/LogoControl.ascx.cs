using System;
using System.Web.UI;

namespace IISProcessScheduler.Shared.UserControls
{
     [Themeable(true)]
    public partial class LogoControl : System.Web.UI.UserControl
    {
        public string LogoImage { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            imageLogo.ImageUrl = LogoImage;
        }
    }
}