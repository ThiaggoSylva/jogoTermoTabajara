namespace Termo.ConsoleApp;

class Program
{
    static void Main(string[] args)
    {
        // Novo ponto de entrada simplificado:
        // agora o Program apenas delega a execução para a classe principal do jogo.
        JogoTermo.Executar();
    }
}