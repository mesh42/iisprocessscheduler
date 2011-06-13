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

namespace IISProcessScheduler.Configuration {
    /// <summary>
    /// Xml Serializer Helper 
    /// </summary>
    public static class XmlSerialization
    {
        /// <summary>
        /// Saves / serializes the items to a specified file.
        /// </summary>
        public static void Save(object o, FileInfo file) {
            XmlSerializer x = new System.Xml.Serialization.XmlSerializer(o.GetType());
            FileStream f = file.OpenWrite();
            x.Serialize(f, o);
            f.Close();
        }

        /// <summary>
        /// Loads the specified file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static object Load(FileInfo file, Type type)
        {
            XmlSerializer serializer = new XmlSerializer(type);
            TextReader stream = file.OpenText();
            object o  = serializer.Deserialize(stream);
            stream.Close();
            return o;
        }

        public static T Load<T>(FileInfo file)
        {
            var serializer = new XmlSerializer(typeof(T));
            var stream = file.OpenText();
            var o = serializer.Deserialize(stream);
            stream.Close();
            return (T) o;
        }
    }
}
