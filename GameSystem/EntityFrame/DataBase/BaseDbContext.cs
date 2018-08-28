using System;
using GameSystem.EntityFrame.Models;
using Microsoft.EntityFrameworkCore;

namespace GameSystem.EntityFrame.DataBase
{
    public abstract class BaseDbContext : DbContext
    {
        protected string _constr;
        public BaseDbContext(string connectString)
        {
            this._constr = connectString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<UserInfo> UserInfos { get; set; }

        public DbSet<KeyWord> KeyWords { get; set; }
    }
}
