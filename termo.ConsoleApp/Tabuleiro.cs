namespace Termo.ConsoleApp;

static class Tabuleiro
{
    // Exibe o cabeçalho e instruções do jogo
    public static void ExibirCabecalho()
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
        Console.WriteLine();

        //bloco explicativo para informar que a interface usa quadrados coloridos no estilo Wordle
        Console.WriteLine("Tabuleiro:");
        Console.WriteLine("Cada letra será exibida dentro de um quadrado colorido.");
    }

    // Exibe a tentativa com cores baseadas na avaliação
    public static void ExibirTentativaColorida(string tentativa, ConsoleColor[] cores)
    {
        Console.Write("Resultado: ");

        // Percorre cada letra da tentativa
        for (int i = 0; i < tentativa.Length; i++)
        {
            // Define a cor de fundo do quadrado para criar o efeito visual parecido com o Wordle
            Console.BackgroundColor = cores[i];

            // Define a cor da letra dentro do quadrado para melhorar a leitura
            Console.ForegroundColor = ConsoleColor.White;

            // Exibe a letra em formato de quadrado
            Console.Write($" {tentativa[i]} ");

            // Reseta a cor para não afetar o restante do console
            Console.ResetColor();
            Console.Write(" ");
        }

        Console.WriteLine();
    }

    //método responsável por exibir o tabuleiro completo com todas as tentativas realizadas até o momento
    public static void ExibirHistoricoTentativas(string[] historicoTentativas, ConsoleColor[][] historicoCores)
    {
        Console.WriteLine("Tabuleiro:");
        Console.WriteLine();

        // Percorre cada linha do tabuleiro
        for (int i = 0; i < JogoTermo.QuantidadeMaximaTentativas; i++)
        {
            // Se ainda não houver tentativa nessa linha, exibe quadrados vazios
            if (historicoTentativas[i] == null)
            {
                ExibirLinhaVazia();
                continue;
            }

            // Recupera a tentativa e suas respectivas cores para desenhar a linha preenchida
            string tentativa = historicoTentativas[i];
            ConsoleColor[] cores = historicoCores[i];

            
            // separa a renderização de uma linha preenchida do restante do tabuleiro.
            ExibirLinhaPreenchida(tentativa, cores);
        }
    }

    
    // desenha uma linha vazia do tabuleiro.
    static void ExibirLinhaVazia()
    {
        for (int j = 0; j < JogoTermo.TamanhoPalavra; j++)
        {
            Console.Write("[   ] ");
        }

        Console.WriteLine();
    }

    
    // desenha uma linha já preenchida com as cores da tentativa.
    static void ExibirLinhaPreenchida(string tentativa, ConsoleColor[] cores)
    {
        // Percorre cada letra da linha atual
        for (int j = 0; j < JogoTermo.TamanhoPalavra; j++)
        {
            // Pinta o fundo do quadrado com a cor calculada na avaliação da tentativa
            Console.BackgroundColor = cores[j];

            // Usa branco para deixar a letra mais visível dentro do quadrado
            Console.ForegroundColor = ConsoleColor.White;

            // Exibe cada letra em formato de bloco
            Console.Write($" {tentativa[j]} ");

            // Reseta as cores após desenhar cada célula do tabuleiro
            Console.ResetColor();
            Console.Write(" ");
        }

        Console.WriteLine();
    }
}
