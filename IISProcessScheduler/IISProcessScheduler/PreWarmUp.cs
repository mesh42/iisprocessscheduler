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
using System.Configuration;
using System.IO;
using System.Net;
using System.Threading;
using System.Web.Hosting;
using IISProcessScheduler.Configuration;
using IISProcessScheduler.Scheduling;

namespace IISProcessScheduler
{
    public class PreWarmUp : IProcessHostPreloadClient
    {
        public static DateTime Started { get; internal set; }
        public static SchedulingService SchedulingService{ get; internal set;}
        public static List<string> LogItems { get; set; }
        private SchedulingItem _rssSchedulingItem;

        private static IISProcessBehavior _iisProcessBehavior;

        public void Preload(string[] parameters)
        {
            _iisProcessBehavior = (IISProcessBehavior)ConfigurationManager.GetSection("IISProcessBehavior");
            LogItems = new List<string>(25);
            SchedulingService = new SchedulingService();
            SchedulingService.Start();
            foreach (var touchUrl in _iisProcessBehavior.TouchUrls)
            {
                var job = new Job<TouchUrl> {Data = touchUrl};
                job.Run.From(DateTime.Now.AddMinutes(_iisProcessBehavior.WarmUpInterval + touchUrl.Interval)).Every.Minutes(touchUrl.Interval);
                SchedulingService.Submit<TouchUrl>(job,ScheduledTouchUrl);
            }
            Started = DateTime.Now;
        }

        private static void ScheduledTouchUrl(Job<TouchUrl> schedulingItem, TouchUrl touchUrl)
        {
            try
            {
                var request = (HttpWebRequest) WebRequest.Create(touchUrl.Url);
                request.Timeout = touchUrl.TimeOut;

                System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
                watch.Start();
                var response = (HttpWebResponse) request.GetResponse();
                var reader = new StreamReader(response.GetResponseStream());
                string str = reader.ReadLine();
                while (str != null)
                {
                    //Console.WriteLine(str);
                    str = reader.ReadLine();
                }
                 
                watch.Stop();
                LogItems.Insert(0, string.Format("{0} - HTTP GET {1} : {2} - {3} (processed in {4}ms)",
                                                 new[]
                                                     {
                                                         DateTime.Now.ToString(),
                                                         touchUrl.Url,
                                                         response.StatusCode.ToString(), response.StatusDescription,
                                                         watch.ElapsedMilliseconds.ToString()
                                                     }
                                       ));
                if (LogItems.Count > _iisProcessBehavior.LogDisplayHistory)
                {
                    LogItems.RemoveAt(_iisProcessBehavior.LogDisplayHistory);
                }
            }
            catch (Exception ex)
            {
                LogItems.Insert(0,string.Format("{0} - EXCEPTION: {1} ",DateTime.Now,ex.Message));
            }
        }
    }
}