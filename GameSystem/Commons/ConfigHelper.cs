using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
namespace GameSystem.Commons
{
    public class ConfigHelper
    {
        static IConfigurationRoot config = null;
        public ConfigHelper()
        {
            Console.WriteLine("执行了构造函数");
        }
        /// <summary>
        /// 根据路径获取属性的值  参数格式 /Root/A/B
        /// </summary>
        /// <returns>string</returns>
        /// <param name="path">Path.</param>
        public static string GetValue(string path)
        {
            config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", true, true).Build();
            string value = "";
            if (path.Trim() == "") return value;
            if (path.Substring(0, 1) == "/")
                path = path.Substring(1, path.Length - 1);
            string[] strArr = path.Split("/");
            IConfigurationSection section = config.GetSection("root");
            for (int i = 0; i < strArr.Length; i++)
            {
                if (i != strArr.Length - 1)
                {
                    section = section.GetSection(strArr[i]);
                }
                else
                {
                    value = section.GetSection(strArr[i]).Value;
                }
            }
            return value;
        }
    }
}
