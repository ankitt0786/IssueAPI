using Microsoft.EntityFrameworkCore;
using IssueAPI.Models;

namespace IssueAPI.Data
{
    public class IssueContext: DbContext
    {
        public IssueContext(DbContextOptions<IssueContext> options) : base(options) { }

        public DbSet<Issue> Issues { get; set; }
    }
}
