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
using System.Xml.Serialization;

namespace IISProcessScheduler.Configuration
{
    public class TouchUrl
    {
        [XmlAttribute]
        public string Url { get; set; }
        [XmlAttribute]
        public int Interval { get; set; }
        [XmlAttribute]
        public int TimeOut { get; set; }
    }
}