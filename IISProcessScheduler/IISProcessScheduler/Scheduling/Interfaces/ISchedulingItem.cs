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
using IISProcessScheduler.Scheduling.Enumerations;
using System;

namespace IISProcessScheduler.Scheduling.Interfaces
{
    public interface ISchedulingItem
    {
        [XmlIgnore]
        string Id { get; set; }
        [XmlIgnore]
        object SynchronizationToken { get; }
        DateTime StartTime { get; set; }
        TimeSpan? Interval { get; set; }
        int? Loops { get; set; }
        [XmlIgnore]
        DateTimeOffset? ExpirationTime { get; }
        [XmlIgnore]
        SchedulingItemStateType SchedulingState { get; }
        [XmlIgnore]
        SchedulingApi Run { get; }
        void Cancel();
        bool Pause();
        bool Continue();
    }
}