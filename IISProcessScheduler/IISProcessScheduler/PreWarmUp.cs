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
using IISProcessScheduler.Scheduling;

namespace IISProcessScheduler
{
    public class PreWarmUp : IProcessHostPreloadClient
    {
        public static DateTime Started { get; internal set; }
        public static SchedulingService SchedulingService{ get; internal set;}
        public static List<string> LogItems { get; set; }
        private SchedulingItem _rssSchedulingItem;

        public void Preload(string[] parameters)
        {
            LogItems = new List<string>(25);
            SchedulingService = new SchedulingService();
            SchedulingService.Start();
            var item = new SchedulingItem();
            var interval = int.Parse(ConfigurationManager.AppSettings["Interval"]);
            _rssSchedulingItem = new SchedulingItem();
            _rssSchedulingItem.Run.From(DateTime.Now.AddSeconds(10)).Every.Minutes(interval);
            SchedulingService.Submit(_rssSchedulingItem, Scheduled);
            Started = DateTime.Now;
        }

        private void Scheduled(SchedulingItem schedulingItem)
        {
            try
            {
                var request = (HttpWebRequest) WebRequest.Create(ConfigurationManager.AppSettings["TouchUrl"]);
                request.Timeout = 5000;
                var response = (HttpWebResponse) request.GetResponse();
                var reader = new StreamReader(response.GetResponseStream());
                string str = reader.ReadLine();
                while (str != null)
                {
                    //Console.WriteLine(str);
                    str = reader.ReadLine();
                }
                LogItems.Insert(0, string.Format("{0} - HTTP GET {1} : {2} - {3} ",
                                                 new[]
                                                     {
                                                         DateTime.Now.ToString(),
                                                         ConfigurationManager.AppSettings["TouchUrl"],
                                                         response.StatusCode.ToString(), response.StatusDescription
                                                     }
                                       ));
                if (LogItems.Count > 25)
                {
                    LogItems.RemoveAt(25);
                }
            }
            catch (Exception ex)
            {
                LogItems.Insert(0,string.Format("{0} - EXCEPTION: {1} ",DateTime.Now,ex.Message));
            }
        }
    }
}