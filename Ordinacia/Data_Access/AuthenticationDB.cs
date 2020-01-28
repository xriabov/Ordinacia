using System.Data.Entity;

namespace Ordinacia.Data_Access
{
    public class AuthenticationDB: DbContext
    {
        public AuthenticationDB() : base("Data")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.Roles)
                .WithMany(r => r.Users)
                .Map(m =>
                {
                    m.ToTable("UserRoles");
                    m.MapLeftKey("UserID");
                    m.MapRightKey("RoleID");
                });
        }

        public DbSet<User> Usrs { get; set; }
        public DbSet<Role> Rls { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Specification> Specifications { get; set; }
        public DbSet<Insurance> Insurances { get; set; }
    }
}