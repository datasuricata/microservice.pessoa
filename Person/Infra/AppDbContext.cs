using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Person.Core;
using Person.Core.Base;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Person.Infra {
    public class AppDbContext : DbContext {

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Estado> Estados { get; set; }
        public DbSet<Cidade> Cidades { get; set; }
        public DbSet<Documento> Documentos { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {
        }

        protected override void OnModelCreating(ModelBuilder options) {

            options.HasDefaultSchema("Core");

            // # config conventions by metadata model loop
            foreach (var entityType in options.Model.GetEntityTypes()) {

                // # config equivalent
                // # convention Remove Pluralizing Table Names Covention
                entityType.Relational().TableName = entityType.DisplayName();

                // # config equivalent
                // # convention Remove OneToMany Cascade Delete Convention
                // # convention Remove ManyToMany Cascade Delete Convention
                entityType.GetForeignKeys()
                    .Where(x => !x.IsOwnership && x.DeleteBehavior == DeleteBehavior.Cascade)
                    .ToList()
                    .ForEach(x => x.DeleteBehavior = DeleteBehavior.Restrict);
            }

            base.OnModelCreating(options);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options) {

            // use this to enable log viewer
            //options.EnableSensitiveDataLogging();

            base.OnConfiguring(options);
        }

        /// <summary>
        /// Apply some things on entities during the async commit
        /// </summary>
        /// <returns>Override SaveChanges</returns>
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken)) {

            foreach (var entry in ChangeTracker.Entries().Where(entry =>
                            entry.Entity.GetType().GetProperty(nameof(EntityBase.CriadoEm)) != null ||
                            entry.Entity.GetType().GetProperty(nameof(EntityBase.AtualizadoEm)) != null)) {
                if (entry.Property(nameof(EntityBase.CriadoEm)) != null)
                    if (entry.State == EntityState.Added)
                        entry.Property(nameof(EntityBase.CriadoEm)).CurrentValue = DateTimeOffset.Now;
                    else if (entry.State == EntityState.Modified)
                        entry.Property(nameof(EntityBase.CriadoEm)).IsModified = false;

                if (entry.Property(nameof(EntityBase.AtualizadoEm)) != null)
                    if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
                        entry.Property(nameof(EntityBase.AtualizadoEm)).CurrentValue = DateTimeOffset.Now;
            }

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}
