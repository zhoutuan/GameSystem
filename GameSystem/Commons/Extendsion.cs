using System;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace GameSystem.Commons
{
    public static class Extension
    {
        /// 获取客户Ip
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetClientUserIp(this HttpContext context)
        {
            var ip = context.Request.Headers["X-Forwarded-For"][0];
            System.Console.WriteLine("用户IP地址:" + ip);
            if (string.IsNullOrEmpty(ip))
            {
                ip = context.Connection.RemoteIpAddress.ToString();
            }
            return ip;
        }

        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) :
                                  JsonConvert.DeserializeObject<T>(value);
        }

        public static bool IsAjax(this HttpRequest request)
        {
            if (request.Headers.Keys.Contains("x-requested-with"))
            {
                return request.Headers["x-requested-with"].ToString().ToUpper() == "XMLHttpRequest";
            }
            else
            {
                return false;
            }
        }
    }
}