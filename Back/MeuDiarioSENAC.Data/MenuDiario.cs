using System.Text.RegularExpressions;

namespace MeuDiarioSENAC.Data;

public class MenuDiario
{
    private readonly RegistroDAO dao;
    private Usuario? usuarioAtual;
    private bool rodando;

    public MenuDiario(RegistroDAO dao)
    {
        this.dao = dao;
        rodando = true;
    }

    public void Executar()
    {
        while (rodando)
        {
            if (usuarioAtual is null)
            {
                MostrarTelaAutenticacao();
                continue;
            }

            MostrarMenuPrincipal();
        }
    }

    private void MostrarTelaAutenticacao()
    {
        Console.Clear();
        Console.WriteLine("╔════════════════════════════════════╗");
        Console.WriteLine("║      📔 ACESSO AO DIÁRIO           ║");
        Console.WriteLine("╚════════════════════════════════════╝\n");

        MostrarOpcao(1, "Registrar usuário");
        MostrarOpcao(2, "Fazer login");
        MostrarOpcao(0, "Sair");

        Console.Write("Escolha uma opção: ");
        string? opcao = Console.ReadLine();

        switch (opcao)
        {
            case "1":
                RegistrarUsuario();
                break;
            case "2":
                FazerLogin();
                break;
            case "0":
                rodando = false;
                Console.WriteLine("\nSaindo...");
                Thread.Sleep(1000);
                break;
            default:
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nOpção inválida!");
                Console.ResetColor();
                Thread.Sleep(1500);
                break;
        }
    }

    private void RegistrarUsuario()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("╔════ REGISTRAR USUÁRIO ════╗\n");

            Console.Write("Nome: ");
            string nome = Console.ReadLine() ?? string.Empty;

            Console.Write("Email: ");
            string email = Console.ReadLine() ?? string.Empty;

            Console.Write("Senha: ");
            string senha = Console.ReadLine() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(nome) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(senha))
            {
                MostrarMensagemAviso("Todos os campos são obrigatórios.");
                continue;
            }

            if (!ValidarNome(nome))
            {
                MostrarMensagemAviso("Nome inválido. Use apenas letras e espaços.");
                continue;
            }

            if (!ValidarEmail(email))
            {
                MostrarMensagemAviso("Email inválido. Use um endereço com @ e domínio, sem acentos.");
                continue;
            }

            if (!ValidarSenha(senha))
            {
                MostrarMensagemAviso("Senha inválida. Mínimo de 6 caracteres.");
                continue;
            }

            var usuario = dao.RegistrarUsuario(nome.Trim(), email.Trim(), senha);

            if (usuario is null)
            {
                MostrarMensagemAviso("Usuário já existe ou dados inválidos.");
                continue;
            }

            usuarioAtual = usuario;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nUsuário '{usuario.Nome}' registrado com sucesso!");
            Console.ResetColor();
            Thread.Sleep(1500);
            return;
        }
    }

    private void FazerLogin()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("╔════ LOGIN ════╗\n");

            Console.Write("Email: ");
            string email = Console.ReadLine() ?? string.Empty;

            Console.Write("Senha: ");
            string senha = Console.ReadLine() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(senha))
            {
                MostrarMensagemAviso("Email e senha são obrigatórios.");
                continue;
            }

            if (!ValidarEmail(email))
            {
                MostrarMensagemAviso("Email inválido. Use um endereço com @ e domínio, sem acentos.");
                continue;
            }

            var usuario = dao.LogarUsuario(email, senha);

            if (usuario is null)
            {
                MostrarMensagemAviso("Credenciais inválidas.");
                continue;
            }

            usuarioAtual = usuario;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nBem-vindo(a), {usuario.Nome}!");
            Console.ResetColor();
            Thread.Sleep(1500);
            return;
        }
    }

    private void MostrarMenuPrincipal()
    {
        Console.Clear();
        Console.WriteLine("╔════════════════════════════════════╗");
        Console.WriteLine("║       📔 DIÁRIO DE REGISTROS       ║");
        Console.WriteLine("╚════════════════════════════════════╝\n");
        Console.WriteLine($"Usuário: {usuarioAtual?.Nome ?? "Desconhecido"} ");

        MostrarOpcao(1, "Listar registros");
        MostrarOpcao(2, "Pesquisar registro por ID");
        MostrarOpcao(3, "Cadastrar novo registro");
        MostrarOpcao(4, "Remover registro");
        MostrarOpcao(0, "Sair da conta");

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Pressione ENTER vazio para sair do aplicativo");
        Console.ResetColor();
        Console.WriteLine();

        Console.Write("Escolha uma opção: ");
        string? opcao = Console.ReadLine();

        if (string.IsNullOrEmpty(opcao))
        {
            rodando = false;
            Console.WriteLine("\nSaindo...");
            Thread.Sleep(1000);
            return;
        }

        switch (opcao)
        {
            case "1":
                ListarRegistros();
                break;
            case "2":
                PesquisarRegistro();
                break;
            case "3":
                CadastrarRegistro();
                break;
            case "4":
                RemoverRegistro();
                break;
            case "0":
                usuarioAtual = null;
                Console.WriteLine("\nConta desconectada.");
                Thread.Sleep(1200);
                break;
            default:
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nOpção inválida!");
                Console.ResetColor();
                Thread.Sleep(1500);
                break;
        }
    }

    private static void MostrarMensagemAviso(string mensagem)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"\n{mensagem}");
        Console.ResetColor();
        Thread.Sleep(1500);
    }

    private static bool ValidarNome(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
            return false;

        var valor = nome.Trim();

        if (valor.Length < 2)
            return false;

        if (valor.Any(char.IsDigit))
            return false;

        return Regex.IsMatch(valor, "^[a-zA-ZÀ-ÿ ]+$");
    }

    private static bool ValidarSenha(string senha)
    {
        return !string.IsNullOrWhiteSpace(senha) && senha.Trim().Length >= 6;
    }

    private static bool ValidarEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;

        var valor = email.Trim();

        if (valor.Any(char.IsWhiteSpace) || valor.Any(ch => ch > 127))
            return false;

        if (!valor.Contains('@'))
            return false;

        if (valor.Count(c => c == '@') != 1)
            return false;

        var partes = valor.Split('@');
        if (partes[0].Length == 0 || partes[1].Length == 0)
            return false;

        if (!partes[1].Contains('.'))
            return false;

        return Regex.IsMatch(valor, "^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$");
    }

    private void MostrarOpcao(int numero, string descricao)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write($"[{numero}] ");
        Console.ResetColor();
        Console.WriteLine(descricao);
    }

    private void ListarRegistros()
    {
        Console.Clear();
        List<Registro> registros = dao.ListarRegistrosPorUsuario(usuarioAtual!.Id);


        Console.WriteLine("╔════ LISTA DE REGISTROS ════╗\n");

        if (registros == null || registros.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Nenhum registro encontrado para este usuário.");
            Console.ResetColor();
        }
        else
        {
            foreach (Registro registro in registros)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"ID: {registro.Id} | {registro.Data:yyyy-MM-dd}");
                Console.ResetColor();
                Console.WriteLine($"Título: {registro.Titulo}");
                Console.WriteLine($"Conteúdo: {registro.Conteudo}");
                Console.WriteLine(new string('-', 40));
            }
        }

        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Pressione ENTER para voltar");
        Console.ResetColor();
        Console.ReadLine();
    }

    private void PesquisarRegistro()
    {
        Console.Clear();
        Console.WriteLine("╔════ PESQUISAR REGISTRO ════╗\n");

        Console.Write("Digite o ID do registro: ");
        string? entrada = Console.ReadLine();

        if (!int.TryParse(entrada, out int idRegistro))
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nID inválido.");
            Console.ResetColor();
            Thread.Sleep(1500);
            return;
        }

        List<Registro> registros = dao.PesquisarRegistroPorUsuario(usuarioAtual!.Id, idRegistro);

        Console.WriteLine();

        if (registros == null || registros.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Nenhum registro encontrado para esse ID neste usuário.");
            Console.ResetColor();
        }
        else
        {
            Registro registro = registros[0];
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"ID: {registro.Id} | {registro.Data:yyyy-MM-dd}");
            Console.ResetColor();
            Console.WriteLine($"Título: {registro.Titulo}");
            Console.WriteLine($"Conteúdo: {registro.Conteudo}");
        }

        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Pressione ENTER para voltar");
        Console.ResetColor();
        Console.ReadLine();
    }

    private void CadastrarRegistro()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("╔════ CADASTRAR REGISTRO ════╗\n");

            Console.Write("Título: ");
            string titulo = Console.ReadLine() ?? string.Empty;

            Console.Write("Conteúdo: ");
            string conteudo = Console.ReadLine() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(titulo) || string.IsNullOrWhiteSpace(conteudo))
            {
                MostrarMensagemAviso("Título e conteúdo são obrigatórios.");
                continue;
            }

            dao.CadastrarRegistro(titulo.Trim(), conteudo.Trim(), usuarioAtual!.Id);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nRegistro cadastrado com sucesso!");
            Console.ResetColor();
            Thread.Sleep(1500);
            return;
        }
    }

    private void RemoverRegistro()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("╔════ REMOVER REGISTRO ════╗\n");

            Console.Write("Digite o ID do registro a remover: ");
            string entrada = Console.ReadLine() ?? string.Empty;

            if (!int.TryParse(entrada, out int idRegistro))
            {
                MostrarMensagemAviso("ID inválido.");
                continue;
            }

            dao.RemoverRegistroPorUsuario(usuarioAtual!.Id, idRegistro);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nRegistro removido (se existir).");
            Console.ResetColor();
            Thread.Sleep(1500);
            return;
        }
    }
}