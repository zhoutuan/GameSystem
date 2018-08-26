using System;
namespace GameSystem.EntityFrame.Models
{
    public class UserInfo
    {
        public UserInfo()
        {
        }

        public int Id { get; set; }

        public string Account { get; set; }

        public string Password { get; set; }

        public DateTime CreateDate { get; set; }

        public string RegIP { get; set; }

        public string LastLoginIP { get; set; }

        public int NoLoginDay { get; set; }

        public DateTime LastLoginDate { get; set; }
    }
}
