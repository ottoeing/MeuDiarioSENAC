using MeuDiarioSENAC.Model;

namespace MeuDiarioSENAC.Data;

public class UsuarioDAO
{
    private readonly MeuDiarioSENACContext conexao;

    public UsuarioDAO()
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
}