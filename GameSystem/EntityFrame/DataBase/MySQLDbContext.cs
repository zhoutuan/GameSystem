using System;
using Microsoft.EntityFrameworkCore;
using GameSystem.EntityFrame.Models;
namespace GameSystem.EntityFrame.DataBase
{
    public class MySQLDbContext : DbContext
    {
        public MySQLDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseMySQL("Server=39.108.213.157;port=3306;database=game;uid=anpei;password=zhoutuan199264;SslMode=None;CharSet=utf8;pooling=true;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserInfo>().ToTable("T_UserInfo");
            modelBuilder.Entity<KeyWord>().ToTable("T_KeyWord");
        }

        public DbSet<UserInfo> UserInfos { get; set; }

        public DbSet<KeyWord> KeyWords { get; set; }
    }
}
