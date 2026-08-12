namespace MeuDiarioSENAC.Data;

public class Registro
{
    public int IdRegistro { get; set; }
    public string Titulo { get; set; }
    public string Conteudo { get; set; }
    public DateTime Data { get; set; }
    public Registro() { }

    public Registro(int idRegistro, string titulo, string conteudo, DateTime data)
    {
        IdRegistro = idRegistro;
        Titulo = titulo;
        Conteudo = conteudo;
        Data = data;
    }

    public override string ToString()
    {
        return $"ID: {IdRegistro}, Título: {Titulo}, Conteúdo: {Conteudo}, Data: {Data}";
    }
}