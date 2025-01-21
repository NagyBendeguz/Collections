using Collections.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Collections.Data
{
    public class CollectionsContext : IdentityDbContext
    {
        public DbSet<Collection> Collections { get; set; }

        public DbSet<Entities.Models.Object> Objects { get; set; }

        public DbSet<Rating> Ratings { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

        public DbSet<IdentityUser> Users { get; set; }

        public CollectionsContext(DbContextOptions<CollectionsContext> ctx) : base(ctx)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // An error was generated for warning 'Microsoft.EntityFrameworkCore.Migrations.PendingModelChangesWarning': The model for context 'CollectionsContext' has pending changes. Add a new migration before updating the database. This exception can be suppressed or logged by passing event ID 'RelationalEventId.PendingModelChangesWarning' to the 'ConfigureWarnings' method in 'DbContext.OnConfiguring' or 'AddDbContext'.
            // https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-9.0/breaking-changes
            optionsBuilder.ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning));
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Collection>()
                .HasMany(c => c.Objects)
                .WithOne(o => o.Collection)
                .HasForeignKey(o => o.CollectionId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Entities.Models.Object>()
                .HasMany(o => o.Ratings)
                .WithOne(r => r.Object)
                .HasForeignKey(r => r.ObjectId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Category>()
                .HasMany(ca => ca.Collections)
                .WithOne(co => co.Category)
                .HasForeignKey(co => co.CategoryId)
                .OnDelete(DeleteBehavior.NoAction);

            base.OnModelCreating(builder);
        }
    }
}
