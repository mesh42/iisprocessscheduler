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
using System.Configuration;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.XPath;

namespace IISProcessScheduler.Configuration
{
    /// <summary>
    /// Generic Configuration Section Handler, utilizing XML (de)serialization.
    /// </summary>
    public class XmlSerializationSectionHandler : IConfigurationSectionHandler
    {
        /// <summary>
        /// Creates a configuration section handler.
        /// </summary>
        /// <param name="parent">Parent object.</param>
        /// <param name="configContext">Configuration context object.</param>
        /// <param name="section">Section XML node.</param>
        /// <returns>The created section handler object.</returns>
        public object Create(object parent, object configContext, XmlNode section)
        {
            // get the name of the type from the type= attribute on the root node
            XPathNavigator xpn = section.CreateNavigator();
            string TypeName = (string)xpn.Evaluate("string(@type)");
            if (TypeName == "")
            {
                throw new ConfigurationErrorsException(
                    "The type attribute is not present on the root node of the <" +
                    section.Name + "> configuration section ", section);
            }

            // make sure this string evaluates to a valid type
            Type t = Type.GetType(TypeName);
            if (t == null)
            {
                throw new ConfigurationErrorsException(
                    "The type attribute \'" + TypeName + "\' specified in the root node of the " +
                    "the <" + section.Name + "> configuration section is not a valid type.",
                    section);
            }
            XmlSerializer xs = new XmlSerializer(t);

            // attempt to deserialize an object of this type from the provided XML section
            XmlNodeReader xnr = new XmlNodeReader(section);
            try
            {
                return xs.Deserialize(xnr);
            }
            catch (Exception ex)
            {
                string s = ex.Message;
                Exception iex = ex.InnerException;
                while (iex != null)
                {
                    s += "; " + iex.Message;
                    iex = iex.InnerException;
                }
                throw new ConfigurationErrorsException(
                    "Unable to deserialize an object of type \'" + TypeName +
                    "\' from  the <" + section.Name + "> configuration section: " +
                    s, ex, section);
            }
        }

        /// <summary>
        /// Serializes the object.
        /// </summary>
        /// <param name="o">The o.</param>
        /// <returns></returns>
        public static string SerializeObject(object o)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            XmlTextWriter xtw = new XmlTextWriter(sw);
            xtw.Formatting = Formatting.Indented;
            xtw.WriteRaw(null);

            XmlSerializerNamespaces xsn = new XmlSerializerNamespaces();
            xsn.Add("", "");

            XmlSerializer xs = new XmlSerializer(o.GetType());
            xs.Serialize(xtw, o, xsn);
            string s = sb.ToString();

            // <Foo> becomes <Foo type="MyClass.Foo">
            s = Regex.Replace(s, "(<" + o.GetType().Name + ")(>)", "$1 type=\"" + o.GetType().FullName + "\"$2");
            return s;
        }
    }
}
