using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Sw.Hospital.HealthExaminationSystem.Comm
{
	/// <summary>
	/// XML 工具
	/// </summary>
	public static class XmlTool
	{
		/// <summary>
		/// 初始化 XML 工具
		/// </summary>
		static XmlTool()
		{
			Settings = new XmlWriterSettings
			{
				Indent = false,
				Encoding = new UTF8Encoding(false)
			};
			Factory = new XmlSerializerFactory();
			XmlSerializerNamespaces = new XmlSerializerNamespaces();
			XmlSerializerNamespaces.Add(string.Empty, string.Empty);
			Settings1 = new XmlWriterSettings
			{
				Indent = false,
				Encoding = new UTF8Encoding(false),
				OmitXmlDeclaration = true
			};
		}

		/// <summary>
		/// XML 序列化设置
		/// </summary>
		public static XmlWriterSettings Settings { get; set; }

		/// <summary>
		/// XML 序列化工厂
		/// </summary>
		public static XmlSerializerFactory Factory { get; set; }

		/// <summary>
		/// XML 命名空间
		/// </summary>
		public static XmlSerializerNamespaces XmlSerializerNamespaces { get; set; }

		/// <summary>
		/// XML 序列化设置
		/// </summary>
		public static XmlWriterSettings Settings1 { get; set; }

		/// <summary>
		/// 序列化
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="input"></param>
		/// <returns></returns>
		public static string Serialize<T>(T input)
		{
			using (var stream = new MemoryStream())
			{
				using (var writer = XmlWriter.Create(stream, Settings))
				{
					var serializer = Factory.CreateSerializer(typeof(T));
					if (serializer != null)
					{
                        XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces(
                            new XmlQualifiedName[] {
                                new XmlQualifiedName(string.Empty,"")
                            });
						serializer.Serialize(writer, input, namespaces);
						return Settings.Encoding.GetString(stream.ToArray());
					}
				}
			}

			return null;
		}

		/// <summary>
		/// 序列化
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="input"></param>
		/// <returns></returns>
		public static string Serialize1<T>(T input)
		{
			using (var stream = new MemoryStream())
			{
				using (var writer = XmlWriter.Create(stream, Settings1))
				{
					var serializer = Factory.CreateSerializer(typeof(T));
					if (serializer != null)
					{
						serializer.Serialize(writer, input, XmlSerializerNamespaces);
						return Settings.Encoding.GetString(stream.ToArray());
					}
				}
			}

			return null;
		}

		/// <summary>
		/// 反序列化
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="input"></param>
		/// <returns></returns>
		public static T Deserialize<T>(string input) where T : class
		{
			using (var reader = new StringReader(input))
			{
				using (var reader1 = XmlReader.Create(reader))
				{
					var serializer = Factory.CreateSerializer(typeof(T));
					if (serializer != null)
					{
						if (serializer.CanDeserialize(reader1))
						{
							var obj = serializer.Deserialize(reader1);
							return obj as T;
						}
					}
				}
			}

			return null;
		}
	}
}