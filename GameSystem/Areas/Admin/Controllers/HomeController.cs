using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GameSystem.EntityFrame.DataBase;
using GameSystem.EntityFrame.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GameSystem.Controllers.Admin
{
    [Area(areaName: "Admin")]
    public class HomeController : Controller
    {
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
                int i= mySQLDbContext.SaveChanges();
                Console.WriteLine($"effect count : { i }");
            }
            return View();
        }
    }
}
