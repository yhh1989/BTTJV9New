using System;
using System.Data;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Sw.Hospital.HealthExaminationSystem.Common.Helpers
{
   public  class XmlConvertor
    {

       
        private XmlConvertor()
        {
        }

        /// <summary>
        /// 转换XML字符串到指定类型的对象
        /// </summary>
        /// <typeparam name="T">指定的对象类型</typeparam>
        /// <param name="xml">XML字符串</param>
        /// <returns>从XML字符串转换过来的对象</returns>
        public static T XmlToObject<T>(string xml) where T : class
        {
            return XmlConvertor.XmlToObject(xml, typeof(T)) as T;
        }

        /// <summary>
        /// 转换XML字符串到指定类型的对象
        /// </summary>
        /// <param name="xml">XML字符串</param>
        /// <param name="type">指定的对象类型</param>
        /// <returns>从XML字符串转换过来的对象</returns>
        public static object XmlToObject(string xml, Type type)
        {
            if (null == xml)
            {
                throw new ArgumentNullException("xml");
            }
            if (null == type)
            {
                throw new ArgumentNullException("type");
            }

            object obj = null;
            XmlSerializer serializer = new XmlSerializer(type);
            StringReader strReader = new StringReader(xml);
            XmlReader reader = new XmlTextReader(strReader);

            try
            {
                obj = serializer.Deserialize(reader);
            }
            catch (InvalidOperationException ie)
            {
                throw new InvalidOperationException("Can not convert xml to object", ie);
            }
            finally
            {
                reader.Close();
            }
            return obj;
        }

        /// <summary>
        /// 转换object对象到具体的XML字符串
        /// </summary>
        /// <param name="obj">待序列化的对象</param>
        /// <param name="toBeIndented"><c>true</c>缩进, 否则<c>false</c>.</param>
        /// <returns>XML字符串</returns>
        public static string ObjectToXml(object obj, bool toBeIndented = false)
        {
            if (null == obj)
            {
                throw new ArgumentNullException("obj");
            }
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");

            UTF8Encoding encoding = new UTF8Encoding(false);
            XmlSerializer serializer = new XmlSerializer(obj.GetType());
            MemoryStream stream = new MemoryStream();
            XmlTextWriter writer = new XmlTextWriter(stream, encoding);
            writer.Formatting = (toBeIndented ? Formatting.Indented : Formatting.None);

            try
            {
                serializer.Serialize(writer, obj, ns);
            }
            catch (InvalidOperationException)
            {
                throw new InvalidOperationException("Can not convert object to xml.");
            }
            finally
            {
                writer.Close();
            }

            string xml = encoding.GetString(stream.ToArray());
            return xml;
        }

        /// <summary>
        /// 格式化XML
        /// </summary>
        /// <param name="xml">待格式化的XML</param>
        /// <returns></returns>
        public static string FormatXml(string xml)
        {
            XmlDocument xd = new XmlDocument();
            xd.LoadXml(xml);
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            XmlTextWriter xtw = null;
            try
            {
                xtw = new XmlTextWriter(sw);
                xtw.Formatting = Formatting.Indented;
                xtw.Indentation = 1;
                xtw.IndentChar = '\t';
                xd.WriteTo(xtw);
            }
            finally
            {
                if (xtw != null)
                    xtw.Close();
            }
            return sb.ToString();
        }
        /**/
        /// <summary>
        /// 将Xml内容字符串转换成DataSet对象
        /// </summary>
        /// <param name="xmlStr">Xml内容字符串</param>
        /// <returns>DataSet对象</returns>
        public static DataSet CXmlToDataSet(string xmlStr)
        {
            if (!string.IsNullOrEmpty(xmlStr))
            {
                StringReader StrStream = null;
                XmlTextReader Xmlrdr = null;
                try
                {
                    DataSet ds = new DataSet();
                    //读取字符串中的信息
                    StrStream = new StringReader(xmlStr);
                    //获取StrStream中的数据
                    Xmlrdr = new XmlTextReader(StrStream);

                    //ds获取Xmlrdr中的数据                
                    ds.ReadXml(Xmlrdr);
                    return ds;
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    //释放资源
                    if (Xmlrdr != null)
                    {
                        Xmlrdr.Close();
                        StrStream.Close();
                        StrStream.Dispose();
                    }
                }
            }
            else
            {
                return null;
            }
        }
    }
}
