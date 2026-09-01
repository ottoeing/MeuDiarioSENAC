using MeuDiarioSENAC.Data;

var registroDao = new RegistroDAO();
var menuDiario = new MenuDiario(registroDao);
menuDiario.Executar();