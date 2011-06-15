using System;
using System.Threading;
using System.Web;
using System.Web.UI;
using IISProcessScheduler.Configuration.IIS;

namespace IISProcessScheduler.Shared.UserControls
{
    [Themeable(true)]
    public partial class ServiceController : System.Web.UI.UserControl
    {
        public string StartImage { get; set; }
        public string PauseImage { get; set; }
        public string StopImage { get; set; }
        public string RefreshImage { get; set; }
        public string StartImageDisabled { get; set; }
        public string PauseImageDisabled { get; set; }
        public string StopImageDisabled { get; set; }

        public enum EnumServiceState
        {
            Started,
            Stopped,
            Paused
        }

        private EnumServiceState _serviceState;

        public EnumServiceState ServiceState
        {
            set { _serviceState = value; }
            get { return _serviceState; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public delegate void OnChangeServiceStateDelegate(object sender, EventArgs e);
        public static event OnChangeServiceStateDelegate OnChangeServiceState;


        protected override void OnPreRender(EventArgs e)
        {
            imageRefresh.ImageUrl = RefreshImage;

            switch (PreWarmUp.ServiceState)
            {
                case EnumServiceState.Paused:
                    imagePlay.ImageUrl = StartImage;
                    imageStop.ImageUrl = StopImage;
                    imagePause.ImageUrl = PauseImageDisabled;
                    imagePlay.Enabled = true;
                    imageStop.Enabled = true;
                    imagePause.Enabled = false;
                    break;
                case EnumServiceState.Started:
                    imagePlay.ImageUrl = StartImageDisabled;
                    imageStop.ImageUrl = StopImage;
                    imagePause.ImageUrl = PauseImage;
                    imagePlay.Enabled = false;
                    imageStop.Enabled = true;
                    imagePause.Enabled = true;
                    break;
                case EnumServiceState.Stopped:
                    imagePlay.ImageUrl = StartImage;
                    imageStop.ImageUrl = StopImageDisabled;
                    imagePause.ImageUrl = PauseImageDisabled;
                    imagePlay.Enabled = true;
                    imageStop.Enabled = false;
                    imagePause.Enabled = false;
                    break;
            }

            base.OnPreRender(e);
        }

        protected override void AddedControl(Control control, int index)
        {
            base.AddedControl(control, index);
        }

        protected void imagePlay_Click(object sender, ImageClickEventArgs e)
        {
            if (PreWarmUp.Started == DateTime.MinValue)
            {
                System.Security.Principal.WindowsImpersonationContext impersonationContext;
                impersonationContext =
                    ((System.Security.Principal.WindowsIdentity)HttpContext.Current.User.Identity).Impersonate();

                IISHelpers.SetAutoStartProvider();
                IISHelpers.SetAutoStart(HttpContext.Current.Request.ApplicationPath, true);

                impersonationContext.Undo();

                Thread.Sleep(1000); // allow IIS to warmup.
                Response.Redirect("./");
                return;
            }


            this.ServiceState = EnumServiceState.Started;
            if (OnChangeServiceState != null)
            {
                OnChangeServiceState(this, e);
            }
        }

        protected void imagePause_Click(object sender, ImageClickEventArgs e)
        {
            this.ServiceState = EnumServiceState.Paused;
            if (OnChangeServiceState != null)
            {
                OnChangeServiceState(this, e);
            }
        }

        protected void imageStop_Click(object sender, ImageClickEventArgs e)
        {
            System.Security.Principal.WindowsImpersonationContext impersonationContext;
            impersonationContext =
                ((System.Security.Principal.WindowsIdentity)HttpContext.Current.User.Identity).Impersonate();

            IISHelpers.SetAutoStartProvider();
            IISHelpers.SetAutoStart(HttpContext.Current.Request.ApplicationPath, false);

            impersonationContext.Undo();

            Thread.Sleep(1000); // allow the application host to reset.
            Response.Redirect("./");

            //this.ServiceState = EnumServiceState.Stopped;
            //if (OnChangeServiceState != null)
            //{
            //    OnChangeServiceState(this, e);
            //}
        }
    }
}