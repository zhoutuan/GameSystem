using System;
using Microsoft.EntityFrameworkCore;
using GameSystem.EntityFrame.DataBase;
using GameSystem.Commons;

namespace GameSystem.Factory
{
    public class DataBaseFactory
    {
        private DataBaseFactory()
        {
            Console.WriteLine("DataBaseFactory");
        }

        private static BaseDbContext dbContext = null;

        public static BaseDbContext GetDbContext()
        {
            if (dbContext == null)
            {
                string conStr = ConfigHelper.GetValue("/DataBase/ConnectStr");
                Console.WriteLine("字符串："+ conStr);
                dbContext = new MySQLDbContext(conStr);
                //dbContext = new MySQLDbContext("Server=39.108.213.157;port=3306;database=game;uid=anpei;password=zhoutuan199264;SslMode=None;CharSet=utf8;pooling=true;");
            }
            return dbContext;
        }
    }
}
