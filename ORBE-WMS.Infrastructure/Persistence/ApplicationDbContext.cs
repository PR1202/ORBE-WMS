using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ORBE_WMS.Domain.Entities;

namespace ORBE_WMS.Infrastructure.Persistence;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Armazem> Armazens => Set<Armazem>();
    public DbSet<Depositante> Depositantes => Set<Depositante>();
    public DbSet<UsuarioArmazem> UsuarioArmazens => Set<UsuarioArmazem>();
    public DbSet<ItemEstoque> ItensEstoque => Set<ItemEstoque>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // UsuarioArmazem: chave composta (UsuarioId + ArmazemId)
        builder.Entity<UsuarioArmazem>(entity =>
        {
            entity.HasKey(ua => new { ua.UsuarioId, ua.ArmazemId });

            entity.HasOne(ua => ua.Usuario)
                  .WithMany(u => u.Armazens)
                  .HasForeignKey(ua => ua.UsuarioId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(ua => ua.Armazem)
                  .WithMany(a => a.Usuarios)
                  .HasForeignKey(ua => ua.ArmazemId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(ua => ua.ArmazemId);
        });

        // Depositante: pertence a um Armazem
        builder.Entity<Depositante>(entity =>
        {
            entity.HasOne(d => d.Armazem)
                  .WithMany(a => a.Depositantes)
                  .HasForeignKey(d => d.ArmazemId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(d => d.ArmazemId);
            entity.HasIndex(d => d.CNPJ);
        });

        // ItemEstoque: pertence a um Armazem e a um Depositante
        builder.Entity<ItemEstoque>(entity =>
        {
            entity.HasOne(i => i.Armazem)
                  .WithMany(a => a.ItensEstoque)
                  .HasForeignKey(i => i.ArmazemId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(i => i.Depositante)
                  .WithMany(d => d.ItensEstoque)
                  .HasForeignKey(i => i.DepositanteId)
                  .OnDelete(DeleteBehavior.Restrict); // Evitar dupla cascade

            entity.HasIndex(i => i.ArmazemId);
            entity.HasIndex(i => i.DepositanteId);
            entity.HasIndex(i => i.CodigoProduto);

            entity.Property(i => i.Quantidade)
                  .HasPrecision(18, 4);
        });

        // Armazem: índice no CNPJ
        builder.Entity<Armazem>(entity =>
        {
            entity.HasIndex(a => a.CNPJ).IsUnique().HasFilter("[CNPJ] IS NOT NULL");
        });

        // Seed: roles padrão do sistema
        // ConcurrencyStamp deve ser estático para evitar PendingModelChangesWarning
        builder.Entity<IdentityRole>().HasData(
            new IdentityRole
            {
                Id = "role-admin",
                Name = "Admin",
                NormalizedName = "ADMIN",
                ConcurrencyStamp = "00000000-0000-0000-0000-000000000001"
            }
        );
    }
}
