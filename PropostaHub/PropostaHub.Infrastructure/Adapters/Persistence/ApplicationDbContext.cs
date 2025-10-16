using Microsoft.EntityFrameworkCore;
using PropostaHub.Core.Domain.Entities;

namespace PropostaHub.Infrastructure.Adapters.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Proposta> Propostas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Proposta>(entity =>
            {
                entity.ToTable("Propostas");

                entity.HasKey(p => p.Id);

                entity.Property(p => p.NomeCliente)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(p => p.Cpf)
                    .IsRequired()
                    .HasMaxLength(11);

                entity.Property(p => p.ValorSeguro)
                    .HasColumnType("decimal(18,2)"); // This requires the Metadata.Builders namespace

                entity.Property(p => p.Status)
                    .HasConversion<int>();

                entity.Property(p => p.DataCriacao)
                    .IsRequired();
            });
        }
    }
}
