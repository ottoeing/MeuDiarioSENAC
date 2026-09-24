using Microsoft.EntityFrameworkCore;
using MeuDiarioSENAC.Model;

namespace MeuDiarioSENAC.Data;

public class RegistroDAO
{
    private readonly MeuDiarioSENACContext conexao;

    public RegistroDAO()
    {
        conexao = new MeuDiarioSENACContext();
    }

    public void CadastrarRegistro(string titulo, string conteudo, int usuarioId)
    {
        var usuario = conexao.Usuarios.Find(usuarioId);

        if (usuario is null)
        {
            return;
        }

        var registro = new Registro
        {
            Titulo = titulo,
            Conteudo = conteudo,
            Data = DateTime.Now,
            UsuarioId = usuario.Id,
            Usuario = usuario
        };

        conexao.Registros.Add(registro);
        conexao.SaveChanges();
    }

    public void AtualizarRegistro(Registro registro)
    {
        var registroExistente = conexao.Registros.FirstOrDefault(
            r => r.Id == registro.Id && r.UsuarioId == registro.UsuarioId);

        if (registroExistente is null)
        {
            return;
        }

        registroExistente.Titulo = registro.Titulo;
        registroExistente.Conteudo = registro.Conteudo;
        conexao.SaveChanges();
    }

    public List<Registro> ListarRegistros()
    {
        return conexao.Registros
            .AsNoTracking()
            .Include(r => r.Usuario)
            .OrderBy(r => r.Data)
            .ToList();
    }
    public List<Registro> ListarRegistrosPorUsuario(int usuarioId)
    {
        return conexao.Registros
            .AsNoTracking()
            .Include(r => r.Usuario)
            .Where(r => r.UsuarioId == usuarioId)
            .OrderBy(r => r.Data)
            .ToList();
    }

    public List<Registro> PesquisarRegistroPorUsuario(int usuarioId, int id)
    {
        return conexao.Registros
            .AsNoTracking()
            .Include(r => r.Usuario)
            .Where(r => r.UsuarioId == usuarioId && r.Id == id)
            .ToList();
    }

    public void RemoverRegistroPorUsuario(int usuarioId, int id)
    {
        var registro = conexao.Registros.FirstOrDefault(r => r.Id == id && r.UsuarioId == usuarioId);

        if (registro is not null)
        {
            conexao.Registros.Remove(registro);
            conexao.SaveChanges();
        }
    }
}