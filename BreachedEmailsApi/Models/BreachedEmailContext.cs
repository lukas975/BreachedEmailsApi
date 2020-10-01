using Microsoft.EntityFrameworkCore;

namespace BreachedEmailsApi.Models
{
    public class BreachedEmailContext : DbContext
    {
        public BreachedEmailContext(DbContextOptions<BreachedEmailContext> options)
            : base(options)
        {
        }

        public DbSet<BreachedEmail> BreachedEmails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BreachedEmail>().HasData(new BreachedEmail
            {
                Email = "email1@email.com"
            }, new BreachedEmail
            {
                Email = "email2@email.com"
            });
        }
    }
}
