using Microsoft.EntityFrameworkCore;
using Vivigest_backend.Domain.Entities;

namespace Vivigest_backend.Infrastructure.Persistance
{
    public class VivigestDbContext : DbContext
    {
        // Builder for the ApplicationDbContext class
        public VivigestDbContext(DbContextOptions<VivigestDbContext> options) : base(options)
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
        public DbSet<ChargeAccount> ChargeAccounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ==========================================
            // 1. PRIMARY KEYS (Todas explícitas)
            // ==========================================
            modelBuilder.Entity<DocumentType>().HasKey(dt => dt.IdDocumentType);
            modelBuilder.Entity<Person>().HasKey(p => p.IdPerson);
            modelBuilder.Entity<Rol>().HasKey(r => r.IdRol);
            modelBuilder.Entity<User>().HasKey(u => u.IdUser);
            modelBuilder.Entity<UserRol>().HasKey(ur => new { ur.IdUser, ur.IdRol }); // Compuesta

            modelBuilder.Entity<AuthorizedPerson>().HasKey(ap => ap.IdAuthorized);
            modelBuilder.Entity<Complex>().HasKey(c => c.IdComplex);
            modelBuilder.Entity<Correspondence>().HasKey(c => c.IdCorrespondence);
            modelBuilder.Entity<CorrespondenceState>().HasKey(cs => cs.IdCorrespondenceState);
            modelBuilder.Entity<CorrespondenceType>().HasKey(ct => ct.IdCorrespondenceType);
            modelBuilder.Entity<Incident>().HasKey(i => i.IdIncident);
            modelBuilder.Entity<IncidentType>().HasKey(it => it.IdIncidentType);
            modelBuilder.Entity<Payment>().HasKey(p => p.IdPayment);
            modelBuilder.Entity<PaymentMethod>().HasKey(pm => pm.IdPaymentMethod);
            modelBuilder.Entity<PaymentPeriod>().HasKey(pp => pp.IdPaymentPeriod);
            modelBuilder.Entity<RelationshipType>().HasKey(rt => rt.IdRelationshipType);
            modelBuilder.Entity<Residence>().HasKey(r => r.IdResidence);
            modelBuilder.Entity<State>().HasKey(s => s.IdState);
            modelBuilder.Entity<Tower>().HasKey(t => t.IdTower);
            modelBuilder.Entity<Unit>().HasKey(u => u.IdUnit);
            modelBuilder.Entity<Visit>().HasKey(v => v.IdVisit);
            modelBuilder.Entity<Visitor>().HasKey(v => v.IdVisitor);

            // Llave Primaria de ChargeAccount
            modelBuilder.Entity<ChargeAccount>().HasKey(ca => ca.IdChargeAccount);

            // ==========================================
            // RELACIONES ADICIONALES (Sin colecciones bidireccionales)
            // ==========================================

            // ChargeAccount
            modelBuilder.Entity<ChargeAccount>().HasOne(ca => ca.Unit).WithMany().HasForeignKey(ca => ca.IdUnit).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<ChargeAccount>().HasOne(ca => ca.PaymentPeriod).WithMany().HasForeignKey(ca => ca.IdPaymentPeriod).OnDelete(DeleteBehavior.Restrict);

            // AuthorizedPerson
            modelBuilder.Entity<AuthorizedPerson>().HasOne(ap => ap.RelationshipType).WithMany().HasForeignKey(ap => ap.IdRelationshipType);
            modelBuilder.Entity<AuthorizedPerson>().HasOne(ap => ap.State).WithMany().HasForeignKey(ap => ap.IdState);

            // Unit
            modelBuilder.Entity<Unit>().HasOne(u => u.State).WithMany().HasForeignKey(u => u.IdState);

            // Correspondence
            modelBuilder.Entity<Correspondence>().HasOne(c => c.CorrespondenceType).WithMany().HasForeignKey(c => c.IdCorrespondenceType);
            modelBuilder.Entity<Correspondence>().HasOne(c => c.CorrespondenceState).WithMany().HasForeignKey(c => c.IdCorrespondenceState);

            // Incident
            modelBuilder.Entity<Incident>().HasOne(i => i.IncidentType).WithMany().HasForeignKey(i => i.IdIncidentType);

            // Payment
            modelBuilder.Entity<Payment>().HasOne(p => p.Unit).WithMany().HasForeignKey(p => p.IdUnit);
            modelBuilder.Entity<Payment>().HasOne(p => p.PaymentPeriod).WithMany().HasForeignKey(p => p.IdPaymentPeriod);
            modelBuilder.Entity<Payment>().HasOne(p => p.PaymentMethod).WithMany().HasForeignKey(p => p.IdPaymentMethod);
            modelBuilder.Entity<Payment>().HasOne(p => p.RegisteredBy).WithMany().HasForeignKey(p => p.IdRegisterdBy).OnDelete(DeleteBehavior.Restrict);

            // Visit
            modelBuilder.Entity<Visit>().HasOne(v => v.Visitor).WithMany().HasForeignKey(v => v.IdVisitor);
            modelBuilder.Entity<Visit>().HasOne(v => v.Unit).WithMany().HasForeignKey(v => v.IdUnit);
            modelBuilder.Entity<Visit>().HasOne(v => v.User).WithMany().HasForeignKey(v => v.IdUserRegister).OnDelete(DeleteBehavior.Restrict);

            // ==========================================
            // 2. RELATIONS OF USERS AND PERSONS
            // ==========================================

            // User - UserRol (1 a Muchos)
            modelBuilder.Entity<UserRol>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRols)
                .HasForeignKey(ur => ur.IdUser);

            // UserRol - Rol (Muchos a 1, Unidireccional)
            modelBuilder.Entity<UserRol>()
                .HasOne(ur => ur.Rol)
                .WithMany()
                .HasForeignKey(ur => ur.IdRol);

            // Person - DocumentType (Muchos a 1, Unidireccional)
            modelBuilder.Entity<Person>()
                .HasOne(p => p.DocumentType)
                .WithMany()
                .HasForeignKey(p => p.IdDocumentType)
                .OnDelete(DeleteBehavior.Restrict);

            // User - Person (1 a 1)
            modelBuilder.Entity<User>()
                .HasOne(u => u.Person)
                .WithOne()
                .HasForeignKey<User>(u => u.IdPerson)
                .OnDelete(DeleteBehavior.Restrict);

            // ==========================================
            // 3. RELATIONS COMPLEXES AND UNITS
            // ==========================================

            // Tower - Complex (Muchos a 1)
            modelBuilder.Entity<Tower>()
                .HasOne(t => t.Complex)
                .WithMany()
                .HasForeignKey(t => t.IdComplex)
                .OnDelete(DeleteBehavior.Cascade);

            // Unit - Tower (Muchos a 1)
            modelBuilder.Entity<Unit>()
                .HasOne(u => u.Tower)
                .WithMany()
                .HasForeignKey(u => u.IdTower)
                .OnDelete(DeleteBehavior.Cascade);

            // ==========================================
            // 4. RELACIONES DE GESTIÓN (Residencias, Visitas, Autorizados)
            // ==========================================

            // Residence - User (Muchos a 1)
            modelBuilder.Entity<Residence>()
                .HasOne(r => r.User)
                .WithMany()
                .HasForeignKey(r => r.IdUser)
                .OnDelete(DeleteBehavior.Restrict);

            // Residence - Unit (Muchos a 1)
            modelBuilder.Entity<Residence>()
                .HasOne(r => r.Unit)
                .WithMany()
                .HasForeignKey(r => r.IdUnit)
                .OnDelete(DeleteBehavior.Restrict);

            // AuthorizedPerson - Relaciones Unidireccionales
            modelBuilder.Entity<AuthorizedPerson>()
                .HasOne(ap => ap.ResidentUser)
                .WithMany()
                .HasForeignKey(ap => ap.IdResidentUser)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AuthorizedPerson>()
                .HasOne(ap => ap.Person)
                .WithMany()
                .HasForeignKey(ap => ap.IdPerson)
                .OnDelete(DeleteBehavior.Restrict);

            // ==========================================
            // 5. RELACIONES DE OPERACIÓN (Correspondencia, Incidentes)
            // ==========================================

            // Correspondence - Unit
            modelBuilder.Entity<Correspondence>()
                .HasOne(c => c.Unit)
                .WithMany()
                .HasForeignKey(c => c.IdUnit)
                .OnDelete(DeleteBehavior.Restrict);

            // Correspondence - User (Quien registra)
            modelBuilder.Entity<Correspondence>()
                .HasOne(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.IdRegisteredBy)
                .OnDelete(DeleteBehavior.Restrict);

            // Incident - User (Quien reporta)
            modelBuilder.Entity<Incident>()
                .HasOne(i => i.UserReportee)
                .WithMany()
                .HasForeignKey(i => i.IdUserReportee)
                .OnDelete(DeleteBehavior.Restrict);

            // ==========================================
            // 6. CONFIGURACIÓN DE PROPIEDADES (Ejemplo: Campos Requeridos, Longitudes, etc.)
            // ==========================================

            // Para el monto de los Pagos (18 dígitos, 2 decimales)
            modelBuilder.Entity<Payment>()
                .Property(p => p.Amount)
                .HasPrecision(18, 2);

            // Para el área en metros cuadrados de las Unidades (18 dígitos, 2 decimales)
            modelBuilder.Entity<Unit>()
                .Property(u => u.AreaM2Unit)
                .HasPrecision(18, 2);

            // ChargeAccount
            modelBuilder.Entity<ChargeAccount>()
                .Property(ca => ca.Amount)
                .HasPrecision(18, 2);
        }
    }
}