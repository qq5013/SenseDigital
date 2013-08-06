using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Js.Com
{
    public class ConfigurationDispose
    {
        public ConfigurationDispose()
        {
        }

        #region GetConfiguration
        /// <summary>
        /// 取得appSettings里的值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        public static string GetConfiguration(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
        #endregion

        #region GetConfigurationList
        /// <summary>
        /// 取得appSettings里的值列表
        /// </summary>
        /// <param name="filePath">配置文件路径</param>
        /// <returns>值列表</returns>
        public static KeyValueConfigurationCollection GetConfigurationList(string filePath)
        {
            AppSettingsSection appSection = null;                       //AppSection对象
            Configuration configuration = null;                         //Configuration对象     
            KeyValueConfigurationCollection k = null;                   //返回的键值对类型

            configuration = ConfigurationManager.OpenExeConfiguration(filePath);

            //取得AppSettings节
            appSection = (AppSettingsSection)configuration.Sections["appSettings"];

            //取得AppSetting节的键值对
            k = appSection.Settings;

            return k;

        }
        #endregion

        #region SetConfiguration
        /// <summary>
        /// 设置appSetting的值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="filePath">App.config文件路径</param>
        public static void SetConfiguration(string key, string value, string filePath)
        {
            Configuration configuration = null;                 //Configuration对象
            AppSettingsSection appSection = null;               //AppSection对象 

            configuration = ConfigurationManager.OpenExeConfiguration(filePath);

            //取得AppSetting节
            appSection = configuration.AppSettings;

            //赋值并保存
            appSection.Settings[key].Value = value;
            configuration.Save();


        }
        #endregion

        #region SetConfiguration
        /// <summary>
        /// 设置appSetting的值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public static void SetConfiguration(string key, string value)
        {
            AppSettingsSection appSection = null;               //AppSection对象 

            appSection = (AppSettingsSection)ConfigurationManager.GetSection("appSettings");

            appSection.Settings[key].Value = value;

        }
        #endregion
    }

}
