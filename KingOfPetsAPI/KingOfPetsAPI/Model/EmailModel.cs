using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace KingOfPetsAPI.Model
{
        public class EmailModel : DbContext
        {
            public DbSet<Email> Emails { get; set; }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlite("Data Source=Database.db");
            }
        }

        public class Email
        {
            public int id { get; set; }
            public string email { get; set; }
        }
}
