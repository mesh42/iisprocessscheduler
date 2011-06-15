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
using System.IO;
using System.Xml.Serialization;

namespace IISProcessScheduler.Configuration
{
    public static class SerializationHelpers
    {
        public static string Serialize<T>(T objectToSerialize)
        {
            if (objectToSerialize == null) throw new ArgumentNullException("objectToSerialize");

            using (var mem = new MemoryStream())
            {
                var x = new XmlSerializer(objectToSerialize.GetType());
                x.Serialize(mem, objectToSerialize);
                var array = new byte[mem.Length];
                mem.Position = 0;
                mem.Read(array, 0, (int)mem.Length);
                return System.Text.Encoding.UTF8.GetString(array);
            }
        }

        public static T Deserialize<T>(string message, bool appendXmlHeader)
        {
            if (message == null) throw new ArgumentNullException("message");
            if (appendXmlHeader)
                message = "<?xml version=\"1.0\"?>" + message;
            using (var mem = new MemoryStream())
            {
                byte[] a = System.Text.Encoding.UTF8.GetBytes(message);
                mem.Write(a, 0, a.Length);
                mem.Position = 0;
                var ser = new XmlSerializer(typeof(T));
                return (T)ser.Deserialize(mem);
            }
        }
    }
}
