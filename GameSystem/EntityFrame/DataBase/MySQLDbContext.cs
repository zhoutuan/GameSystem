using System;
using Microsoft.EntityFrameworkCore;
using GameSystem.EntityFrame.Models;
namespace GameSystem.EntityFrame.DataBase
{
    public class MySQLDbContext : BaseDbContext
    {
        public MySQLDbContext(string str) : base(str)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //"Server=39.108.213.157;port=3306;database=game;uid=anpei;password=zhoutuan199264;SslMode=None;CharSet=utf8;pooling=true;"
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseMySQL(base._constr);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserInfo>().ToTable("T_UserInfo");
            modelBuilder.Entity<KeyWord>().ToTable("T_KeyWord");
        }
    }
}
