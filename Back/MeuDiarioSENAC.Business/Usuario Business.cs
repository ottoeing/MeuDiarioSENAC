namespace MeuDiarioSENAC.Business;
using MeuDiarioSENAC.Model;

public class UsuarioBusiness
{
    public bool NomeFoiInformado(Usuario usuario)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(usuario.Nome))
            {
                return false;
            }
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao verificar se o nome foi informado: {ex.Message}");
            return false;
        }
    }

    public bool EmailFoiInformado(Usuario usuario)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(usuario.Email))
            {
                return false;
            }
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao verificar se o email foi informado: {ex.Message}");
            return false;
        }
    }

    public bool SenhaFoiInformada(Usuario usuario)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(usuario.Senha))
            {
                return false;
            }
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao verificar se a senha foi informada: {ex.Message}");
            return false;
        }
    }
}