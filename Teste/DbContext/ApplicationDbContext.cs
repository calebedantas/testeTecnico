using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext   
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Associado> Associados { get; set; }
    public DbSet<Empresa> Empresas { get; set; }
    public DbSet<AssociadoEmpresa> AssociadoEmpresas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Associado>()
            .HasIndex(a => a.Cpf)
            .IsUnique();

        modelBuilder.Entity<Empresa>()
            .HasIndex(e => e.Cnpj)
            .IsUnique();

        modelBuilder.Entity<AssociadoEmpresa>()
            .HasKey(ae => new { ae.AssociadoId, ae.EmpresaId });

        modelBuilder.Entity<AssociadoEmpresa>()
            .HasOne(ae => ae.Associado)
            .WithMany(a => a.AssociadoEmpresas)
            .HasForeignKey(ae => ae.AssociadoId);

        modelBuilder.Entity<AssociadoEmpresa>()
            .HasOne(ae => ae.Empresa)
            .WithMany(e => e.AssociadoEmpresas)
            .HasForeignKey(ae => ae.EmpresaId);
    }
}
