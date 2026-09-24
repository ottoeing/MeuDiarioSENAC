
using MeuDiarioSENAC.ConsoleApp;
using MeuDiarioSENAC.Service;

var usuarioService = new UsuarioService();
var registroService = new RegistroService();
var menuDiario = new MenuDiario(usuarioService, registroService);
menuDiario.Executar();