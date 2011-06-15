using System;
using System.ComponentModel;

namespace IISProcessScheduler.Shared.UserControls
{
    public partial class ServiceController : System.Web.UI.UserControl
    {
        [Browsable(true)]
        [Description("CSS Style for this image.")]
        public string CssClassPlay { get; set; }

        [Browsable(true)]
        [Description("CSS Style for this image.")]
        public string CssClassPause { get; set; }

        [Browsable(true)]
        [Description("CSS Style for this image.")]
        public string CssClassStop { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.imageStop.CssClass = CssClassStop;
            this.imagePause.CssClass = CssClassPause;
            this.imageStop.CssClass = CssClassStop;
        }
    }
}