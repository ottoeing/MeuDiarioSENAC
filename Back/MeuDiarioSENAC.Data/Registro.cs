namespace MeuDiarioSENAC.Data;

public class Registro
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Conteudo { get; set; } = string.Empty;
    public DateTime Data { get; set; }
    public int UsuarioId { get; set; }
    public Usuario? Usuario { get; set; }

    public Registro() { }

    public Registro(int id, string titulo, string conteudo, DateTime data)
    {
        Id = id;
        Titulo = titulo;
        Conteudo = conteudo;
        Data = data;
    }

    public override string ToString()
    {
        return $"ID: {Id}, Título: {Titulo}, Conteúdo: {Conteudo}, Data: {Data}";
    }
}