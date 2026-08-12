using MySql.Data.MySqlClient;

namespace MeuDiarioSENAC.Data;

public class MeuDiarioSENACContext : DbContext
{
    private readonly string stringConexao = "Server=127.0.0.1;Port=3306;Database=diario;Uid=root;Pwd=1234;SslMode=Preferred;AllowPublicKeyRetrieval=True;ConnectionTimeout=30;";

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql(stringConexao, ServerVersion.AutoDetect(stringConexao));
    }
}
