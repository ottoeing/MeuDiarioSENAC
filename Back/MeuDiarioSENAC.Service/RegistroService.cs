using MeuDiarioSENAC.Business;
using MeuDiarioSENAC.Data;
using MeuDiarioSENAC.Model;

namespace MeuDiarioSENAC.Service;

public class RegistroService
{
    private readonly RegistroBusiness registroBusiness;
    private readonly RegistroDAO registroDao;

    public RegistroService()
    {
        registroDao = new RegistroDAO();
        registroBusiness = new RegistroBusiness();
    }

    public void Cadastrar(string titulo, string conteudo, int usuarioId)
    {
        registroDao.CadastrarRegistro(titulo, conteudo, usuarioId);
    }

    public void InserirRegistro(Registro registro)
    {
        registroDao.CadastrarRegistro(registro.Titulo, registro.Conteudo, registro.UsuarioId);
    }

    public void AtualizarRegistro(Registro registro)
    {
        registroDao.AtualizarRegistro(registro);
    }

    public List<Registro> Listar()
    {
        return registroDao.ListarRegistros();
    }

    public List<Registro> ListarPorUsuario(int usuarioId)
    {
        return registroDao.ListarRegistrosPorUsuario(usuarioId);
    }

    public List<Registro> PesquisarPorUsuario(int usuarioId, int id)
    {
        return registroDao.PesquisarRegistroPorUsuario(usuarioId, id);
    }

    public void RemoverPorUsuario(int usuarioId, int id)
    {
        registroDao.RemoverRegistroPorUsuario(usuarioId, id);
    }

    public bool TituloFoiInformado(Registro registro)
    {
        return registroBusiness.TituloFoiInformado(registro);
    }

    public bool TituloTemTamanhoValido(Registro registro)
    {
        return registroBusiness.TituloMaxChar(registro);
    }

    public bool ConteudoFoiInformado(Registro registro)
    {
        return registroBusiness.ConteudoFoiInformado(registro);
    }

    public bool ConteudoTemTamanhoValido(Registro registro)
    {
        return registroBusiness.ConteudoMaxChar(registro);
    }

    public bool DataValida(Registro registro)
    {
        return registroBusiness.DataAgora(registro);
    }
}
