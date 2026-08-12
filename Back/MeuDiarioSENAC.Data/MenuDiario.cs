namespace MeuDiarioSENAC.Data;

public class MenuDiario
{
    private ServicoDiario _servico;
    private bool rodando;

    public MenuDiario(ServicoDiario servico)
    {
        _servico = servico;
        rodando = true;
    }

    public void Executar()
    {
        while (rodando)
        {
            MostrarMenuPrincipal();
        }
    }

    private void MostrarMenuPrincipal()
    {
        Console.Clear();
        Console.WriteLine("╔════════════════════════════════════╗");
        Console.WriteLine("║       📔 DIÁRIO DE REGISTROS       ║");
        Console.WriteLine("╚════════════════════════════════════╝\n");

        MostrarOpcao(1, "Listar registros");
        MostrarOpcao(2, "Pesquisar registro por ID");
        MostrarOpcao(3, "Cadastrar novo registro");
        MostrarOpcao(4, "Remover registro");

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Pressione ENTER vazio para sair");
        Console.ResetColor();
        Console.WriteLine();

        Console.Write("Escolha uma opção: ");
        string opcao = Console.ReadLine();

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
            default:
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nOpção inválida!");
                Console.ResetColor();
                Thread.Sleep(1500);
                break;
        }
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
        List<Registro> registros = _servico.ObterRegistros();

        Console.WriteLine("╔════ LISTA DE REGISTROS ════╗\n");

        if (registros == null || registros.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Nenhum registro encontrado.");
            Console.ResetColor();
        }
        else
        {
            foreach (Registro registro in registros)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"ID: {registro.IdRegistro} | {registro.Data:yyyy-MM-dd}");
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
        string entrada = Console.ReadLine();

        if (!int.TryParse(entrada, out int idRegistro))
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nID inválido.");
            Console.ResetColor();
            Thread.Sleep(1500);
            return;
        }

        List<Registro> registros = _servico.PesquisarRegistro(idRegistro);

        Console.WriteLine();

        if (registros == null || registros.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Nenhum registro encontrado com esse ID.");
            Console.ResetColor();
        }
        else
        {
            Registro registro = registros[0];
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"ID: {registro.IdRegistro} | {registro.Data:yyyy-MM-dd}");
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
        Console.Clear();
        Console.WriteLine("╔════ CADASTRAR REGISTRO ════╗\n");

        Console.Write("Título: ");
        string titulo = Console.ReadLine();

        Console.Write("Conteúdo: ");
        string conteudo = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(titulo) || string.IsNullOrWhiteSpace(conteudo))
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nTítulo e conteúdo são obrigatórios.");
            Console.ResetColor();
            Thread.Sleep(1500);
            return;
        }

        _servico.CadastrarRegistro(titulo.Trim(), conteudo.Trim());

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\nRegistro cadastrado com sucesso!");
        Console.ResetColor();
        Thread.Sleep(1500);
    }

    private void RemoverRegistro()
    {
        Console.Clear();
        Console.WriteLine("╔════ REMOVER REGISTRO ════╗\n");

        Console.Write("Digite o ID do registro a remover: ");
        string entrada = Console.ReadLine();

        if (!int.TryParse(entrada, out int idRegistro))
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nID inválido.");
            Console.ResetColor();
            Thread.Sleep(1500);
            return;
        }

        _servico.RemoverRegistro(idRegistro);

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\nRegistro removido (se existir).");
        Console.ResetColor();
        Thread.Sleep(1500);
    }
}