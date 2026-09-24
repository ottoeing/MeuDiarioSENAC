namespace MeuDiarioSENAC.Business;
using MeuDiarioSENAC.Model;

public class RegistroBusiness
{
    public bool TituloFoiInformado(Registro registro)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(registro.Titulo))
            {
                return false;
            }
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao verificar se o título foi informado: {ex.Message}");
            return false;
        }
    }
    public bool TituloMaxChar(Registro registro)
    {
        try
        {
            if (registro.Titulo.Length > 50)
            {
                return false;
            }
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao verificar se o título tem no máximo 50 caracteres: {ex.Message}");
            return false;
        }
    }

    public bool ConteudoFoiInformado(Registro registro)
    {
        try
        {
            return !string.IsNullOrWhiteSpace(registro.Conteudo);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao verificar se o conteúdo foi informado: {ex.Message}");
            return false;
        }
    }

    public bool DataAgora(Registro registro)
    {
        try
        {
            if (registro.Data > DateTime.Now)
            {
                return false;
            }
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao verificar se a data é anterior ou igual à data atual: {ex.Message}");
            return false;
        }
    }
    public bool ConteudoMaxChar(Registro registro)
    {
        try 
        {
            if (registro.Conteudo.Length > 3000)
            {
                return false;
            }
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao verificar se o conteúdo tem no máximo 3000 caracteres: {ex.Message}");
            return false;
        }
    }
}
