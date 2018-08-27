using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GameSystem.Models;
using Newtonsoft.Json;
using GameSystem.EntityFrame.DataBase;
using GameSystem.EntityFrame.Models;
using GameSystem.Commons;

namespace GameSystem.Controllers
{
    public class HomeController : Controller
    {
        MySQLDbContext mySQLDbContext;
        public HomeController()
        {
            mySQLDbContext = new MySQLDbContext();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        //[Route("/API/User/Register")]
        [HttpPost]
        public JsonResult Register([FromBody]RegisterUser user)
        {
            System.Console.WriteLine("注册用户信息:" + JsonConvert.SerializeObject(user));
            if (user == null)
            {
                return Json(new
                {
                    Code = -1,
                    Message = "请求参数错误"
                });
            }
            if (user.Account == null || string.IsNullOrEmpty(user.Account))
            {
                return Json(new
                {
                    Code = -1,
                    Message = "账号不能为空"
                });
            }
            else if (user.Password == null || string.IsNullOrEmpty(user.Password))
            {
                return Json(new
                {
                    Code = -1,
                    Message = "注册密码不能为空"
                });
            }
            else if (user.RePassword == null || string.IsNullOrEmpty(user.RePassword.Trim()))
            {
                return Json(new
                {
                    Code = -1,
                    Message = "重复密码不能为空"
                });
            }
            else if (user.Password != user.RePassword)
            {
                return Json(new
                {
                    Code = -1,
                    Message = "密码与重复密码不一致"
                });
            }
            else
            {
                var keyWord = mySQLDbContext.KeyWords.FirstOrDefault(s => user.Account.Contains(s.Word));
                if (keyWord == null)
                {
                    var userInfo = mySQLDbContext.UserInfos.FirstOrDefault(s => s.Account.Equals(user.Account));
                    if (userInfo == null)
                    {
                        mySQLDbContext.UserInfos.Add(new UserInfo()
                        {
                            Account = user.Account,
                            Password = user.Password,
                            CreateDate = DateTime.Now,
                            LastLoginDate = DateTime.Now,
                            LastLoginIP = this.HttpContext.GetClientUserIp(),
                            RegIP = this.HttpContext.GetClientUserIp()
                        });
                        mySQLDbContext.SaveChanges();
                        return Json(new
                        {
                            Code = 0,
                            Message = "注册成功"
                        });
                    }
                    else
                    {
                        return Json(new
                        {
                            Code = -1,
                            Message = "账号已存在"
                        });
                    }
                }
                else
                {
                    return Json(new
                    {
                        Code = -1,
                        Message = "账号存在敏感字符"
                    });
                }
            }
        }
        [HttpPost]
        public JsonResult Login([FromBody]UserInfo user)
        {
            System.Console.WriteLine("登录账号信息：" + JsonConvert.SerializeObject(user));
            if (user == null)
            {
                return Json(new
                {
                    Code = -1,
                    Message = "请求参数不能为空"
                });
            }
            else
            {
                if (string.IsNullOrWhiteSpace(user.Account))
                {
                    return Json(new
                    {
                        Code = -1,
                        Message = "账号不能为空"
                    });
                }
                else if (string.IsNullOrWhiteSpace(user.Password))
                {
                    return Json(new
                    {
                        Code = -1,
                        Message = "密码不能为空"
                    });
                }
                else
                {
                    var userInfo = mySQLDbContext.UserInfos.FirstOrDefault(s => s.Account.Equals(user.Account) && s.Password.Equals(user.Password));
                    if (userInfo == null)
                    {
                        return Json(new
                        {
                            Code = -1,
                            Message = "账号或密码错误"
                        });
                    }
                    else
                    {
                        userInfo.LastLoginDate = DateTime.Now;
                        userInfo.LastLoginIP = this.HttpContext.GetClientUserIp();
                        mySQLDbContext.UserInfos.Update(userInfo);
                        mySQLDbContext.SaveChangesAsync();
                        return Json(new
                        {
                            Code = 0,
                            Message = "登录成功"
                        });
                    }
                }
            }
        }
    }
}
