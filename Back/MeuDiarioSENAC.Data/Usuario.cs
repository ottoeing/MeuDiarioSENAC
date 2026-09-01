namespace MeuDiarioSENAC.Data;

public class Usuario
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;
    public List<Registro> Registros { get; set; } = new();

    public Usuario() { }

    public Usuario(int id, string nome, string email, string senha)
    {
        Id = id;
        Nome = nome;
        Email = email;
        Senha = senha;
    }

    public override string ToString()
    {
        return $"ID: {Id}, Nome: {Nome}, Email: {Email}";
    }
}