using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GameSystem.EntityFrame.DataBase;
using GameSystem.EntityFrame.Models;
using GameSystem.Models;
using GameSystem.Areas.Admin.Filter;
using Newtonsoft.Json;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using GameSystem.Commons;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GameSystem.Controllers.Admin
{
    [Area(areaName: "Admin")]
    public class HomeController : BaseController
    {
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View("Login");
        }

        [AllowAnonymous]
        [HttpPost]
        public JsonResult Login2([FromBody]User user)
        {
            if (user == null)
            {
                return Json(new
                {
                    Code = -1,
                    Message = "账号或密码错误"
                });
            }
            else
            {
                string accountConfig = ConfigHelper.GetValue("Account");
                var linq = from q in accountConfig.Split("|").AsEnumerable()
                           select new User
                           {
                               Account = q.Split(",")[0],
                               Password = q.Split(",")[1]
                           };
                if (linq.Count(s => s.Account.Equals(user.Account) && s.Password.Equals(user.Password)) > 0)
                {
                    HttpContext.Session.Set<User>("user", user);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"登录成，用户信息是：{ JsonConvert.SerializeObject(HttpContext.Session.Get<User>("user")) }");
                    return Json(new
                    {
                        Code = 0,
                        Message = "登录成功"
                    });
                }
                else
                {
                    return Json(new
                    {
                        Code = -1,
                        Message = "账号或密码错误"
                    });
                }
            }
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
    }
}
