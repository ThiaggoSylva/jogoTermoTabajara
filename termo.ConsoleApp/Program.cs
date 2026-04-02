using System.Security.Cryptography;

namespace Termo.ConsoleApp;

class Program
{
    const int QuantidadeMaximaTentativas = 5;
    const int TamanhoPalavra = 5;

    static void Main(string[] args)
    {
        string[] palavras =
        {
            "TERMO",
            "LIVRO",
            "PRATO",
            "TIGRE",
            "NUVEM",
            "CASAL",
            "FESTA",
            "PRAIA",
            "MOUSE",
            "VERDE"
        };

        string palavraSecreta = SortearPalavra(palavras);

        ExibirCabecalho();

        for (int tentativaAtual = 1; tentativaAtual <= QuantidadeMaximaTentativas; tentativaAtual++)
        {
            Console.WriteLine();
            Console.WriteLine($"Tentativa {tentativaAtual} de {QuantidadeMaximaTentativas}");

            string tentativa = ObterTentativaValida();

            ExibirTentativaColorida(tentativa, palavraSecreta);

            if (tentativa == palavraSecreta)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("Parabéns! Você acertou a palavra secreta!");
                Console.ResetColor();

                Console.WriteLine("Pressione ENTER para sair...");
                Console.ReadLine();
                return;
            }
        }

        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine("Que pena! Você perdeu.");
        Console.ResetColor();

        Console.WriteLine($"A palavra secreta era: {palavraSecreta}");
        Console.WriteLine("Pressione ENTER para sair...");
        Console.ReadLine();

    }

    static void ExibirCabecalho()
    {
        Console.Clear();
        Console.WriteLine("-----------------------------------");
        Console.WriteLine("             TERMO");
        Console.WriteLine("-----------------------------------");
        Console.WriteLine("Descubra a palavra secreta de 5 letras.");
        Console.WriteLine();
        Console.WriteLine("Cores:");
        Console.WriteLine("Vermelho escuro = letra inexistente");
        Console.WriteLine("Amarelo escuro  = letra existe, mas em outra posição");
        Console.WriteLine("Verde escuro    = letra correta na posição correta");
    }

    static string SortearPalavra(string[] palavras)
    {
        int indice = RandomNumberGenerator.GetInt32(0, palavras.Length);
        return palavras[indice];
    }

    static string ObterTentativaValida()
    {
        while (true)
        {
            Console.Write("Digite uma palavra de 5 letras: ");
            string? entrada = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(entrada))
            {
                Console.WriteLine("Entrada inválida.");
                continue;
            }

            string tentativa = entrada.Trim().ToUpper();

            if (tentativa.Length != TamanhoPalavra)
            {
                Console.WriteLine("A palavra deve ter exatamente 5 letras.");
                continue;
            }

            if (!ContemSomenteLetras(tentativa))
            {
                Console.WriteLine("Digite somente letras.");
                continue;
            }

            return tentativa;
        }
    }

    static bool ContemSomenteLetras(string texto)
    {
        foreach (char c in texto)
        {
            if (!char.IsLetter(c))
                return false;
        }

        return true;
    }


    static void ExibirTentativaColorida(string tentativa, string palavraSecreta)
    {
        Console.Write("Resultado: ");

        ConsoleColor[] cores = AvaliarTentativa(tentativa, palavraSecreta);

        for (int i = 0; i < tentativa.Length; i++)
        {
            Console.ForegroundColor = cores[i];
            Console.Write(tentativa[i]);
            Console.ResetColor();
            Console.Write(" ");
        }

        Console.WriteLine();
    }

}