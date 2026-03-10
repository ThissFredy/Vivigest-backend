using Microsoft.EntityFrameworkCore;
using Vivigest_backend.Domain.Entities;

namespace Vivigest_backend.Infrastructure.Persistance
{
    public class VivigestDbContext : DbContext
    {
        // Builder for the ApplicationDbContext class
        public VivigestDbContext (DbContextOptions<VivigestDbContext> options) : base(options)
        {
        }

        public DbSet<DocumentType> DocumentTypes { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Rol> Rols { get; set; }
        public DbSet<UserRol> UserRoles { get; set; }
        public DbSet<AuthorizedPerson> AuthorizedPersons { get; set; }
        public DbSet<Complex> Complexes { get; set; }
        public DbSet<Correspondence> Correspondences { get; set; }
        public DbSet<CorrespondenceState> CorrespondenceStates { get; set; }
        public DbSet<CorrespondenceType> CorrespondenceTypes { get; set; }
        public DbSet<Incident> Incidents { get; set; }
        public DbSet<IncidentType> IncidentTypes { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<PaymentPeriod> PaymentPeriods { get; set; }
        public DbSet<RelationshipType> RelationshipTypes { get; set; }
        public DbSet<Residence> Residences { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Tower> Towers { get; set; }
        public DbSet<Unit> Units { get; set; }

        public DbSet<Visit> Visits { get; set; }
        public DbSet<Visitor> Visitors { get; set; }


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
