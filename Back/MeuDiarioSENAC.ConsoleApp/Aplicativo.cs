namespace MeuDiarioSENAC.Data;

public class Aplicativo
{
    private ServicoDiario servicoDiario;
    private MenuDiario menuDiario;

    public Aplicativo()
    {
        servicoDiario = new ServicoDiario();
        menuDiario = new MenuDiario(servicoDiario);
    }

    public void Executar()
    {
        menuDiario.Executar();
    }
}