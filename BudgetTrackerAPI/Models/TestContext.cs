using Microsoft.EntityFrameworkCore;


// DB SQLite https://www.youtube.com/watch?v=h9c7TZb2QuU
namespace BudgetTrackerAPI.Models
{
    public class TestContext : DbContext
    {
        public DbSet<Test> Test { get; set; }

        public string dbPath { get; }

        public TestContext() {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            dbPath = System.IO.Path.Join(path, "budgetTracker.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite($"Data Source={dbPath}");
        }
    }
}
