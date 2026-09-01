using Microsoft.EntityFrameworkCore;

namespace MeuDiarioSENAC.Data;

public class RegistroDAO
{
    private readonly MeuDiarioSENACContext conexao;

    public RegistroDAO()
    {
        conexao = new MeuDiarioSENACContext();
    }

    public Usuario? RegistrarUsuario(string nome, string email, string senha)
    {
        if (string.IsNullOrWhiteSpace(nome) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(senha))
        {
            return null;
        }

        email = email.Trim();
        nome = nome.Trim();

        if (conexao.Usuarios.Any(u => u.Email.ToLower() == email.ToLower()))
        {
            return null;
        }

        var usuario = new Usuario
        {
            Nome = nome,
            Email = email,
            Senha = senha
        };

        conexao.Usuarios.Add(usuario);
        conexao.SaveChanges();
        return usuario;
    }

    public Usuario? LogarUsuario(string email, string senha)
    {
        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(senha))
        {
            return null;
        }

        return conexao.Usuarios
            .FirstOrDefault(u => u.Email.ToLower() == email.Trim().ToLower() && u.Senha == senha);
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

    public List<Registro> ListarRegistrosPorUsuario(int usuarioId)
    {
        return conexao.Registros
            .Where(r => r.UsuarioId == usuarioId)
            .Include(r => r.Usuario)
            .OrderBy(r => r.Data)
            .ToList();
    }

    public List<Registro> PesquisarRegistroPorUsuario(int usuarioId, int id)
    {
        return conexao.Registros
            .Where(r => r.UsuarioId == usuarioId && r.Id == id)
            .Include(r => r.Usuario)
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