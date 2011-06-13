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

namespace IISProcessScheduler
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (PreWarmUp.Started == DateTime.MinValue)
            {
                // Prewarmup not executed.
                lblWarmupState.Text = "Service is not warmed up.";
                lnkPauseResume.Text = string.Empty;
            }
            else
            {
                lblWarmupState.Text = string.Format("Service started at {0}",PreWarmUp.Started);
                SetLnkText();
            }
            lblHistory.Text = string.Empty;
            foreach (string item in PreWarmUp.LogItems)
            {
                lblHistory.Text += item + "<br />";
            }
        }

        private void SetLnkText()
        {
            lnkPauseResume.Text = PreWarmUp.SchedulingService.IsPaused ? "Resume Scheduler" : "Pause Scheduler";
        }

        protected void lnkPauseResume_Click(object sender, EventArgs e)
        {
            if (PreWarmUp.SchedulingService.IsPaused)
            {
                PreWarmUp.SchedulingService.Resume();
            }
            else
            {
                PreWarmUp.SchedulingService.Pause();
            }
            SetLnkText();
        }
    }
}