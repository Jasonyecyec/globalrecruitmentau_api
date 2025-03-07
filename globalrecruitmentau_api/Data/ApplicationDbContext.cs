namespace globalrecruitmentau_api.Data;

using globalrecruitmentau_api.Models;
using Microsoft.EntityFrameworkCore;


    public class ApplicationDbContext: DbContext
    {
        public DbSet<Sample> Sample { get; set; } // Reference to Models/YourEntity.cs

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }

