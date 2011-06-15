/* **********************************************************************************
 *
 * Copyright (c) TCPX. All rights reserved.
 *
 * This source code is subject to terms and conditions of the Microsoft Public
 * License (Ms-PL). A copy of the license can be found in the license.txt file
 * included in this distribution.
 *
 * You must not remove this notice, or any other, from this software.
 *
 * **********************************************************************************/
using System;
using System.Collections.Generic;
using System.Threading;
using System.Web;
using IISProcessScheduler.Configuration.IIS;

namespace IISProcessScheduler
{
    public partial class Default : System.Web.UI.Page
    {
        public enum EnumProcessResult
        {
            Error,
            Timeout,
            Healthy
        }

        public interface IProcessInfo
        {
            string Resource { get; set; }
            EnumProcessResult Result { get; set; }
            string Description { get; set; }
            string StatusCode { get; set; }
            double TimeElapsed { get; set; }
            DateTime Date { get; set; }
        }

        public class TouchUrlProcessInfos : List<IProcessInfo>
        {
            public TouchUrlProcessInfos() : base()
            {
                //this.Add(new TouchUrlProcessInfo("hello world!....."));
            }
        }

        public class TouchUrlProcessInfo : IProcessInfo
        {
            public string Resource { get; set; }
            public string Description { get; set; }
            public string StatusCode { get; set; }

            public TouchUrlProcessInfo(string resource, EnumProcessResult processResult, string statusCode, double timeElapsed, string description)
            {
                Resource = resource;
                Result = processResult;
                StatusCode = statusCode;
                TimeElapsed = timeElapsed;
                Description = description;
                Date = DateTime.Now;
            }

            public EnumProcessResult Result { get; set; }


            public double TimeElapsed { get; set; }



            public DateTime Date { get; set; }


        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (PreWarmUp.Started != DateTime.MinValue)
            {
                lblWarmupState.Text = string.Format("Service started at {0}", PreWarmUp.Started);
                dlHistory.DataSource = PreWarmUp.LogItems;
                dlHistory.DataBind();
            }
        }

        //protected void lnkEnableWarmUp_Click(object sender, EventArgs e)
        //{
        //    System.Security.Principal.WindowsImpersonationContext impersonationContext;
        //    impersonationContext =
        //        ((System.Security.Principal.WindowsIdentity)User.Identity).Impersonate();

        //    IISHelpers.SetAutoStartProvider();
        //    IISHelpers.SetAutoStart(Request.ApplicationPath, true);

        //    impersonationContext.Undo();

        //    Thread.Sleep(1000); // allow IIS to warmup.
        //    Response.Redirect("./");
        //}
    }
}