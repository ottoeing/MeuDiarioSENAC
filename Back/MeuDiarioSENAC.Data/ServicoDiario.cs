namespace MeuDiarioSENAC.Data;

public class ServicoDiario
{
    private RegistroDAO repositorio;
    public List<Registro> registros;

    public ServicoDiario()
    {
        repositorio = new RegistroDAO();
        CarregarRegistros();
    }

    private void CarregarRegistros()
    {
        registros = repositorio.ListarRegistros();
    }

    public void CadastrarRegistro(string titulo, string conteudo)
    {
        repositorio.CadastrarRegistro(titulo, conteudo);
        CarregarRegistros();
    }

    public List<Registro> PesquisarRegistro(int idRegistro)
    {
        return repositorio.PesquisarRegistro(idRegistro);
    }

    public void RemoverRegistro(int idRegistro)
    {
        repositorio.RemoverRegistro(idRegistro);
        CarregarRegistros();
    }

    public List<Registro> ObterRegistros()
    {
        return new List<Registro>(registros);
    }
}