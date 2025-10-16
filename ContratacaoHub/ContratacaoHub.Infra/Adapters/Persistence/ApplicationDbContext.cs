using ContratacaoHub.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContratacaoHub.Infra.Adapters.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Contratacao> Contratacoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Contratacao>(entity =>
            {
                entity.ToTable("Contratacoes");
                entity.HasKey(c => c.Id);

                entity.Property(c => c.PropostaId).IsRequired();
                entity.Property(c => c.NomeCliente).IsRequired().HasMaxLength(200);
                entity.Property(c => c.ValorContratado).HasColumnType("decimal(18,2)");
                entity.Property(c => c.DataContratacao).IsRequired();

                // Index para buscar contratacoes por proposta
                entity.HasIndex(c => c.PropostaId).IsUnique();
            });
        }
    }
}
