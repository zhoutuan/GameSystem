using System;
using Microsoft.AspNetCore.Http;

namespace GameSystem.Commons{
    public static class Extension {
         /// 获取客户Ip
         /// </summary>
         /// <param name="context"></param>
         /// <returns></returns>
         public static string GetClientUserIp(this HttpContext context)
         {
             var ip = context.Request.Headers["X-Forwarded-For"][0];
             System.Console.WriteLine("用户IP地址:"+ip);
             if (string.IsNullOrEmpty(ip))
             {
                 ip = context.Connection.RemoteIpAddress.ToString();
             }
             return ip;
         }
    }
}