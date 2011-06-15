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
using IISProcessScheduler.Configuration;
using IISProcessScheduler.Scheduling;
using NUnit.Framework;

namespace IISProcessScheduler.Tests
{
    [TestFixture]
    public class SchedulingSerializationTests
    {
        [Test]
        public void SerializeDateTimeOffsetTest()
        {
            var offset = new DateTimeOffset(DateTime.Now);
            var s = SerializationHelpers.Serialize(offset);
        }

        [Test]
        public void SerializeDateTest()
        {
            var target = DateTime.Now;
            var result = SerializationHelpers.Serialize(target);
        }

        [Test]
        public void SerializeScheduleTest()
        {
            var job = new Job<TouchUrl>();
            var l = job.Run.From(DateTime.Now.AddMinutes(10)).Every.Minutes(10);
            var s = SerializationHelpers.Serialize(l);
        }
    }
}
