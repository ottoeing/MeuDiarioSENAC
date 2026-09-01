using Microsoft.EntityFrameworkCore;

namespace MeuDiarioSENAC.Data;

public class MeuDiarioSENACContext : DbContext
{
    private const string StringConexao = "Server=127.0.0.1;Port=3306;Database=diario;Uid=root;Pwd=1234;SslMode=Preferred;AllowPublicKeyRetrieval=True;ConnectionTimeout=30;";

    public DbSet<Registro> Registros => Set<Registro>();
    public DbSet<Usuario> Usuarios => Set<Usuario>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseMySql(StringConexao, ServerVersion.AutoDetect(StringConexao));
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>()
            .HasMany(u => u.Registros)
            .WithOne(r => r.Usuario)
            .HasForeignKey(r => r.UsuarioId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Usuario>()
            .HasIndex(u => u.Email)
            .IsUnique();

        modelBuilder.Entity<Registro>()
            .Property(r => r.Titulo)
            .HasMaxLength(200);

        modelBuilder.Entity<Registro>()
            .Property(r => r.Conteudo)
            .HasColumnType("TEXT");
    }
}

