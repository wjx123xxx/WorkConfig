// *******************************************************************
// * 文件名称： JsonHelper.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-01-23 17:16:21
// *******************************************************************

using System;
using System.IO;
using System.Xml;

using Newtonsoft.Json;

using Formatting = Newtonsoft.Json.Formatting;

namespace Wx.Utility.WxCommon
{
    /// <summary>
    /// Json简易使用类
    /// </summary>
    public class JsonHelper
    {
        #region Fields

        private static readonly JsonSerializerSettings _settings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All,
            PreserveReferencesHandling = PreserveReferencesHandling.All
        };

        #endregion

        #region Public Methods

        /// <summary>
        /// 反序列化json串，根据json串中自带的类型串指定类型
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static object DeserializeObject(string json)
        {
            return JsonConvert.DeserializeObject(json, _settings);
        }

        /// <summary>
        /// 将json串反序列化成指定类型的实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json">json串</param>
        /// <returns></returns>
        public static T DeserializeObject<T>(string json)
        {
            try
            {
                return (T)DeserializeObject(json);
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }

        /// <summary>
        /// 加载一个xml文件转换成一个指定的实体
        /// </summary>
        /// <typeparam name="T">指定实体类型</typeparam>
        /// <param name="filePath">xml文件路径</param>
        /// <returns>反序列化的实体</returns>
        public static T LoadFromXmlFile<T>(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException(filePath);
            }

            var xmlRoot = new XmlDocument();
            xmlRoot.Load(filePath);
            var json = JsonConvert.SerializeXmlNode(xmlRoot, Formatting.Indented, true);
            return DeserializeObject<T>(json);
        }

        /// <summary>
        /// 将一个实体序列化到一个指定路径的xml文件中
        /// </summary>
        /// <param name="entity">保存的实体</param>
        /// <param name="filePath">xml文件路径</param>
        /// <param name="rootNode">xml文件根节点名称，默认值为<c>WxRootNode</c></param>
        /// <returns>保存结果</returns>
        public static bool SaveToXmlFile(object entity, string filePath, string rootNode = "WxRootNode")
        {
            using (var sw = new StringWriter())
            using (var xtw = new XmlTextWriter(sw))
            {
                try
                {
                    xtw.Formatting = System.Xml.Formatting.Indented;
                    xtw.Indentation = 1;
                    xtw.IndentChar = '\t';

                    var json = SerializeObject(entity);
                    var xmlNode = JsonConvert.DeserializeXmlNode(json, rootNode);
                    xmlNode.WriteTo(xtw);

                    File.WriteAllText(filePath, sw.ToString());
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 将实体序列化成json串
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static string SerializeObject(object entity)
        {
            if (entity == null)
            {
                return string.Empty;
            }

            return JsonConvert.SerializeObject(entity, _settings);
        }

        #endregion
    }
}