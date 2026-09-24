using MeuDiarioSENAC.Business;
using MeuDiarioSENAC.Data;
using MeuDiarioSENAC.Model;

namespace MeuDiarioSENAC.Service;

public class UsuarioService
{
    private readonly UsuarioBusiness usuarioBusiness;
    private readonly UsuarioDAO usuarioDao;

    public UsuarioService()
    {
        usuarioDao = new UsuarioDAO();
        usuarioBusiness = new UsuarioBusiness();
    }

    public Usuario? Registrar(string nome, string email, string senha)
    {
        return usuarioDao.RegistrarUsuario(nome, email, senha);
    }

    public Usuario? Autenticar(string email, string senha)
    {
        return usuarioDao.LogarUsuario(email, senha);
    }

    public bool NomeFoiInformado(Usuario usuario)
    {
        return usuarioBusiness.NomeFoiInformado(usuario);
    }

    public bool EmailFoiInformado(Usuario usuario)
    {
        return usuarioBusiness.EmailFoiInformado(usuario);
    }

    public bool SenhaFoiInformada(Usuario usuario)
    {
        return usuarioBusiness.SenhaFoiInformada(usuario);
    }
}
