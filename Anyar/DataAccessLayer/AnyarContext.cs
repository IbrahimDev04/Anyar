using Microsoft.EntityFrameworkCore;
using Anyar.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace Anyar.DataAccessLayer
{
    public class AnyarContext : IdentityDbContext<AppUser>
    {
        public AnyarContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Slider> sliders { get; set; }
        public DbSet<Service> services { get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<Employee> employees { get; set; }
        public DbSet<Job> jobs { get; set; }
        public DbSet<AppUser> appUsers { get; set; }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {


            foreach (var entry in ChangeTracker.Entries())
            {
                if(entry.Entity is BaseEntity baseEntity)
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            ((BaseEntity)entry.Entity).CreatedTime = DateTime.Now;
                            ((BaseEntity)entry.Entity).IsDeleted = false;
                            break;
                        case EntityState.Modified:
                            ((BaseEntity)entry.Entity).UpdateTime = DateTime.Now;
                            break;
                    }
                }
                
            }

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}
