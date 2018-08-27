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

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GameSystem.Controllers.Admin
{
    [AdminAuthorize]
    [Area(areaName: "Admin")]
    public class HomeController : Controller
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
                if (user.Account == "admin" && user.Password == "123456")
                {
                    return Json(new
                    {
                        Code = 0,
                        Message = "登录成功"
                    });
                    //this.HttpContext.Session.Set("user", Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(user)));
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
            using (MySQLDbContext mySQLDbContext = new MySQLDbContext())
            {
                mySQLDbContext.UserInfos.Add(new UserInfo()
                {
                    Account = "aaa",
                    CreateDate = DateTime.Now,
                    LastLoginIP = "127.0.0.1",
                    Password = "1234566",
                    NoLoginDay = 5,
                    RegIP = "127.0.0.1",
                    LastLoginDate = DateTime.Now.AddHours(2)
                });
                int i = mySQLDbContext.SaveChanges();
                Console.WriteLine($"effect count : { i }");
            }
            return View();
        }
    }
}
