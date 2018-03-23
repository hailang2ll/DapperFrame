#if NETSTANDARD2_0
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
#endif
using DapperFrame.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace DapperFrame.Helpers
{
    /// <summary>
    /// 记取appsettings.json文件信息
    /// </summary>
    public class JsonConfigurationHelper
    {
#if NETSTANDARD2_0
        private static IConfiguration config { get; set; }
        static JsonConfigurationHelper()
        {
            if (config == null)
            {
                //未找到文件直接抛出异常
                config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .Build();
            }
        }

        /// <summary>
        /// 获取一个实例数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T GetAppSettings<T>(string key) where T : class, new()
        {
            //ConfigurationBuilder需要添加包："Microsoft.Extensions.Configuration"
            //AddJsonFile需要添加包："Microsoft.Extensions.Configuration.Json"

            //ServiceCollection需要添加包： "Microsoft.Extensions.DependencyInjection"
            //AddOptions需要添加包： "Microsoft.Extensions.Options.ConfigurationExtensions"

            var appconfig = new ServiceCollection()
                .AddOptions()
                .Configure<T>(config.GetSection(key))
                .BuildServiceProvider()
                .GetService<IOptions<T>>()
                .Value;
            return appconfig;
        }

        /// <summary>
        /// 获取单个数据值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetAppSettings(string key)
        {
            string value = config.GetSection(key).Value;
            return value;
        }
#elif NET45

        private static XmlDocument xmlDoc;
        /// <summary>
        /// 构造函数
        /// </summary>
        static JsonConfigurationHelper()
        {
            if (xmlDoc == null)
            {
                xmlDoc = TableConfigXmlDoc();
            }
        }

        //加载配置文件
        public static XmlDocument TableConfigXmlDoc()
        {
            try
            {
                xmlDoc = new XmlDocument();
                string fileHandlerPath = Path.Combine(GetTableConfigValue("TableConfig", AppDomain.CurrentDomain.SetupInformation.ApplicationBase), "TableConfig.xml");
                xmlDoc.Load(fileHandlerPath);
            }
            catch (Exception ex)
            {
                xmlDoc = null;
                throw new Exception("加载配置文件TableConfig.xml错误:" + ex.ToString());
            }
            return xmlDoc;
        }

        public static List<T> GetAppSettings<T>() where T : new()
        {
            if (xmlDoc == null)
            {
                xmlDoc = TableConfigXmlDoc();
            }

            List<T> list = new List<T>();
            PropertyInfo[] propinfos = null;
            XmlNodeList nodelist = xmlDoc.SelectNodes("ArrayOfTableConfiguration/TableConfiguration");
            foreach (XmlNode node in nodelist)
            {
                T entity = new T();
                //初始化propertyinfo
                if (propinfos == null)
                {
                    Type objtype = entity.GetType();
                    propinfos = objtype.GetProperties();
                }
                //填充entity类的属性
                foreach (PropertyInfo propinfo in propinfos)
                {
                    XmlNode cnode = node.SelectSingleNode(propinfo.Name);
                    string v = cnode.InnerText;
                    if (v != null)
                        propinfo.SetValue(entity, Convert.ChangeType(v, propinfo.PropertyType), null);
                }
                list.Add(entity);
            }
            return list;
        }

        public static string GetTableConfigValue(string key, string defaultValue)
        {
            var value = System.Configuration.ConfigurationManager.AppSettings[key];
            if (string.IsNullOrWhiteSpace(value))
                value = defaultValue;
            return value;
        }


#endif
    }
}
