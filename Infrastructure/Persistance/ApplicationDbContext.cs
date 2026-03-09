using Microsoft.EntityFrameworkCore;
using Vivigest_backend.Domain.Entities;

namespace Vivigest_backend.Infrastructure.Persistance
{
    public class ApplicationDbContext : DbContext
    {
        // Builder for the ApplicationDbContext class
        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<DocumentType> DocumentTypes { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Rol> Rols { get; set; }
        public DbSet<UserRol> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Primary key DocumentType
            modelBuilder.Entity<DocumentType>()
                .HasKey(dt => dt.IdDocumentType);

            // Primary key Person
            modelBuilder.Entity<Person>()
                .HasKey(p => p.IdPerson);

            // Primary key Rol
            modelBuilder.Entity<Rol>()
                .HasKey(r => r.IdRol);

            // Primary key User
            modelBuilder.Entity<User>()
                .HasKey(u => u.IdUser);


            // Primary key UserRol
            modelBuilder.Entity<UserRol>()
                .HasKey(ur => new { ur.IdUser, ur.IdRol });

            // Behavor User - UserRol
            modelBuilder.Entity<UserRol>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRols)
                .HasForeignKey(ur => ur.IdUser);

            // Behavor UserRol - Rol
            modelBuilder.Entity<UserRol>()
                .HasOne(ur => ur.Rol)
                .WithMany()
                .HasForeignKey(ur => ur.IdRol);


        }
    }
}
