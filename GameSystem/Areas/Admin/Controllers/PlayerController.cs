using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GameSystem.Models;
using GameSystem.EntityFrame.Models;
using GameSystem.EntityFrame.DataBase;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using OfficeOpenXml;
using System.IO;
using Microsoft.AspNetCore.Hosting;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GameSystem.Areas.Admin.Controllers
{
    [Area(areaName: "Admin")]
    public class PlayerController : Controller
    {
        MySQLDbContext mySQLDbContext;
        private IHostingEnvironment _hostingEnvironment;

        public PlayerController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            mySQLDbContext = new MySQLDbContext();
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
        //{"code":0,"msg":"","count":1000,"data":[{"id":10000,"username":"user-0","sex":"女","city":"城市-0","sign":"签名-0","experience":255,"logins":24,"wealth":82830700,"classify":"作家","score":57},{"id":10001,"username":"user-1","sex":"男","city":"城市-1","sign":"签名-1","experience":884,"logins":58,"wealth":64928690,"classify":"词人","score":27},{"id":10002,"username":"user-2","sex":"女","city":"城市-2","sign":"签名-2","experience":650,"logins":77,"wealth":6298078,"classify":"酱油","score":31},{"id":10003,"username":"user-3","sex":"女","city":"城市-3","sign":"签名-3","experience":362,"logins":157,"wealth":37117017,"classify":"诗人","score":68},{"id":10004,"username":"user-4","sex":"男","city":"城市-4","sign":"签名-4","experience":807,"logins":51,"wealth":76263262,"classify":"作家","score":6},{"id":10005,"username":"user-5","sex":"女","city":"城市-5","sign":"签名-5","experience":173,"logins":68,"wealth":60344147,"classify":"作家","score":87},{"id":10006,"username":"user-6","sex":"女","city":"城市-6","sign":"签名-6","experience":982,"logins":37,"wealth":57768166,"classify":"作家","score":34},{"id":10007,"username":"user-7","sex":"男","city":"城市-7","sign":"签名-7","experience":727,"logins":150,"wealth":82030578,"classify":"作家","score":28},{"id":10008,"username":"user-8","sex":"男","city":"城市-8","sign":"签名-8","experience":951,"logins":133,"wealth":16503371,"classify":"词人","score":14},{"id":10009,"username":"user-9","sex":"女","city":"城市-9","sign":"签名-9","experience":484,"logins":25,"wealth":86801934,"classify":"词人","score":75},{"id":10010,"username":"user-10","sex":"女","city":"城市-10","sign":"签名-10","experience":1016,"logins":182,"wealth":71294671,"classify":"诗人","score":34},{"id":10011,"username":"user-11","sex":"女","city":"城市-11","sign":"签名-11","experience":492,"logins":107,"wealth":8062783,"classify":"诗人","score":6},{"id":10012,"username":"user-12","sex":"女","city":"城市-12","sign":"签名-12","experience":106,"logins":176,"wealth":42622704,"classify":"词人","score":54},{"id":10013,"username":"user-13","sex":"男","city":"城市-13","sign":"签名-13","experience":1047,"logins":94,"wealth":59508583,"classify":"诗人","score":63},{"id":10014,"username":"user-14","sex":"男","city":"城市-14","sign":"签名-14","experience":873,"logins":116,"wealth":72549912,"classify":"词人","score":8},{"id":10015,"username":"user-15","sex":"女","city":"城市-15","sign":"签名-15","experience":1068,"logins":27,"wealth":52737025,"classify":"作家","score":28},{"id":10016,"username":"user-16","sex":"女","city":"城市-16","sign":"签名-16","experience":862,"logins":168,"wealth":37069775,"classify":"酱油","score":86},{"id":10017,"username":"user-17","sex":"女","city":"城市-17","sign":"签名-17","experience":1060,"logins":187,"wealth":66099525,"classify":"作家","score":69},{"id":10018,"username":"user-18","sex":"女","city":"城市-18","sign":"签名-18","experience":866,"logins":88,"wealth":81722326,"classify":"词人","score":74},{"id":10019,"username":"user-19","sex":"女","city":"城市-19","sign":"签名-19","experience":682,"logins":106,"wealth":68647362,"classify":"词人","score":51},{"id":10020,"username":"user-20","sex":"男","city":"城市-20","sign":"签名-20","experience":770,"logins":24,"wealth":92420248,"classify":"诗人","score":87},{"id":10021,"username":"user-21","sex":"男","city":"城市-21","sign":"签名-21","experience":184,"logins":131,"wealth":71566045,"classify":"词人","score":99},{"id":10022,"username":"user-22","sex":"男","city":"城市-22","sign":"签名-22","experience":739,"logins":152,"wealth":60907929,"classify":"作家","score":18},{"id":10023,"username":"user-23","sex":"女","city":"城市-23","sign":"签名-23","experience":127,"logins":82,"wealth":14765943,"classify":"作家","score":30},{"id":10024,"username":"user-24","sex":"女","city":"城市-24","sign":"签名-24","experience":212,"logins":133,"wealth":59011052,"classify":"词人","score":76},{"id":10025,"username":"user-25","sex":"女","city":"城市-25","sign":"签名-25","experience":938,"logins":182,"wealth":91183097,"classify":"作家","score":69},{"id":10026,"username":"user-26","sex":"男","city":"城市-26","sign":"签名-26","experience":978,"logins":7,"wealth":48008413,"classify":"作家","score":65},{"id":10027,"username":"user-27","sex":"女","city":"城市-27","sign":"签名-27","experience":371,"logins":44,"wealth":64419691,"classify":"诗人","score":60},{"id":10028,"username":"user-28","sex":"女","city":"城市-28","sign":"签名-28","experience":977,"logins":21,"wealth":75935022,"classify":"作家","score":37},{"id":10029,"username":"user-29","sex":"男","city":"城市-29","sign":"签名-29","experience":647,"logins":107,"wealth":97450636,"classify":"酱油","score":27}]}
        [HttpPost]
        public JsonResult PlayerList()
        {
            PaginResult<UserInfo> user = new PaginResult<UserInfo>();
            DbSet<UserInfo> uSet = mySQLDbContext.UserInfos;
            Expression<Func<UserInfo, bool>> expression = (u) => 1 == 1;
            string uid = HttpContext.Request.Form["uid"];
            string uaccount = HttpContext.Request.Form["uaccount"].ToString();
            string cdate = HttpContext.Request.Form["cdate"].ToString();
            int pageIndex = int.Parse(HttpContext.Request.Form["page"].ToString());
            int pageSize = int.Parse(HttpContext.Request.Form["limit"].ToString());
            IQueryable<UserInfo> queryable = uSet;
            if (!string.IsNullOrEmpty(uid))
            {
                queryable = queryable.Where(s => s.Id == int.Parse(uid));
            }
            if (!string.IsNullOrEmpty(uaccount.Trim()))
            {
                queryable = queryable.Where((s) => s.Account.Contains(uaccount));
            }
            if (!string.IsNullOrEmpty(cdate))
            {
                DateTime sdate = DateTime.Parse(cdate.Split("~".ToCharArray())[0].Trim());
                DateTime edate = DateTime.Parse(cdate.Split("~".ToCharArray())[1].Trim());
                queryable = queryable.Where((u) => u.CreateDate >= sdate && u.CreateDate <= edate);

            }
            user.Code = 0;
            user.Msg = "success";
            user.Count = queryable.Count();
            user.Data = queryable.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return Json(user);
        }

        [HttpPost]
        public IActionResult ExportExcel()
        {
            string sWebRootFolder = _hostingEnvironment.WebRootPath;
            string sFileName = $"{Guid.NewGuid()}.xlsx";
            FileInfo file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));
            using (ExcelPackage package = new ExcelPackage(file))
            {
                // 添加worksheet
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Players");
                //添加头
                worksheet.Cells[1, 1].Value = "玩家ID";
                worksheet.Cells[1, 2].Value = "用户账号";
                worksheet.Cells[1, 3].Value = "注册时间";
                worksheet.Cells[1, 4].Value = "最后登陆时间";
                worksheet.Cells[1, 5].Value = "未登天数";
                worksheet.Cells[1, 6].Value = "注册IP";
                worksheet.Cells[1, 7].Value = "最后登陆IP";
                //添加值
                /*worksheet.Cells["A2"].Value = 1000;
                worksheet.Cells["B2"].Value = "LineZero";
                worksheet.Cells["C2"].Value = "http://www.cnblogs.com/linezero/";

                worksheet.Cells["A3"].Value = 1001;
                worksheet.Cells["B3"].Value = "LineZero GitHub";
                worksheet.Cells["C3"].Value = "https://github.com/linezero";
                worksheet.Cells["C3"].Style.Font.Bold = true;*/
                List<UserInfo> lstUserInfo = mySQLDbContext.UserInfos.ToList();
                for (int i = 0; i < lstUserInfo.Count; i++)
                {
                    worksheet.Cells[i + 2, 1].Value = lstUserInfo[i].Id;
                    worksheet.Cells[i + 2, 2].Value = lstUserInfo[i].Account;
                    worksheet.Cells[i + 2, 3].Value = lstUserInfo[i].CreateDate.ToString("yyyy-MM-dd HH:mm:ss");
                    worksheet.Cells[i + 2, 4].Value = lstUserInfo[i].LastLoginDate.ToString("yyyy-MM-dd HH:mm:ss");
                    worksheet.Cells[i + 2, 5].Value = lstUserInfo[i].NoLoginDay;
                    worksheet.Cells[i + 2, 6].Value = lstUserInfo[i].RegIP;
                    worksheet.Cells[i + 2, 7].Value = lstUserInfo[i].LastLoginIP;
                }
                package.Save();
            }
            return File(sFileName, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }
    }
}