using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameSystem.EntityFrame.DataBase;
using GameSystem.EntityFrame.Models;
using GameSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GameSystem.Factory;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GameSystem.Areas.Admin.Controllers
{
    [Area(areaName: "Admin")]
    public class KeyWordController : Controller
    {
        BaseDbContext mySQLDbContext;
        public KeyWordController()
        {
            mySQLDbContext = DataBaseFactory.GetDbContext();
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult WordList()
        {
            int pageIndex = int.Parse(HttpContext.Request.Form["page"].ToString());
            int pageSize = int.Parse(HttpContext.Request.Form["limit"].ToString());
            string keyWord = HttpContext.Request.Form["word"].ToString();
            IQueryable<KeyWord> query = mySQLDbContext.KeyWords;
            if (!string.IsNullOrEmpty(keyWord))
            {
                query = query.Where(s => s.Word.Contains(keyWord));
            }
            PaginResult<KeyWord> result = new PaginResult<KeyWord>();
            result.Code = 0;
            result.Count = query.Count();
            result.Msg = "success";
            result.Data = query.OrderByDescending(s=>s.CreateDate).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return Json(result);
        }

        [HttpPost]
        public string DeleteWord(int id)
        {
            try
            {
                mySQLDbContext.KeyWords.Remove(new KeyWord() { Id = id });
                mySQLDbContext.SaveChanges();
                return "删除成功";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string ModifyWord(int id)
        {
            try
            {
                mySQLDbContext.KeyWords.Update(new KeyWord()
                {
                    Id = id,
                    Word = HttpContext.Request.Form["word"].ToString()
                });
                mySQLDbContext.SaveChangesAsync();
                return "修改成功";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
