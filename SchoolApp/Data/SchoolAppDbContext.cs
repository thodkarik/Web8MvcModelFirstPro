using Microsoft.EntityFrameworkCore;

namespace SchoolApp.Data
{
    public class SchoolAppDbContext : DbContext
    {
        public SchoolAppDbContext()
        {

        }

        public SchoolAppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Teacher> Teachers { get; set; } = null!;
        public DbSet<Student> Students { get; set; } = null!;
        public DbSet<Course> Courses { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");
                entity.HasKey(e => e.Id);   // Optional if 'Id' is the convention
                entity.Property(e => e.Username).HasMaxLength(50);  // define max length is MAX
                entity.Property(e => e.Email).HasMaxLength(100);
                entity.Property(e => e.Password).HasMaxLength(60);
                entity.Property(e => e.Firstname).HasMaxLength(255);
                entity.Property(e => e.Lastname).HasMaxLength(255);
                entity.Property(e => e.UserRole).HasMaxLength(20).HasConversion<string>();

                entity.Property(e => e.InsertedAt)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("GETUTCDATE()");

                entity.Property(e => e.ModifiedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("GETUTCDATE()");

                entity.HasIndex(e => e.Username, "IX_Users_Username").IsUnique();
                entity.HasIndex(e => e.Email, "IX_Users_Email").IsUnique();
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("Students");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Am).HasMaxLength(10);
                entity.Property(e => e.Institution).HasMaxLength(255);
                entity.Property(e => e.Department).HasMaxLength(255);

                entity.Property(e => e.InsertedAt)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("GETUTCDATE()");

                entity.Property(e => e.ModifiedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("GETUTCDATE()");

                entity.HasIndex(e => e.Am, "IX_Students_Am").IsUnique();
                entity.HasIndex(e => e.UserId, "IX_Students_UserId").IsUnique();
                entity.HasIndex(e => e.Institution, "IX_Students_Institution");

                //entity.HasOne(e => e.User)
                //      .WithOne(p => p.Student)
                //      .HasForeignKey<Student>(e => e.UserId)
                //      .HasConstraintName("FK_Students_Users");
            });
            
        }
    }
}
