using System.Security.Cryptography;

namespace Termo.ConsoleApp;

class Program
{
    // Quantidade máxima de tentativas que o jogador possui
    const int QuantidadeMaximaTentativas = 5;
    
    // Tamanho fixo da palavra (regra do jogo)
    const int TamanhoPalavra = 5;

    static void Main(string[] args)
    {
        // Lista de palavras possíveis (todas com 5 letras)
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
            "VERDE",
            "MANGA",
            "ROSAS",
            "GATOS",
            "CACHO",
            "FELIZ",
            "AMIGO",
            "SONHO",
            "MUNDO",
            "FRASE",
            "CHAVE",
            "PILHA",
            "BOLSA",
            "FUMAR",
        };

        // Loop externo responsável por manter o jogo funcionando enquanto o jogador quiser jogar novamente
        while (true)
        {
            // Sorteia uma palavra aleatória da lista
            string palavraSecreta = SortearPalavra(palavras);

            // Matriz que armazena todas as tentativas da partida atual para exibir o histórico em formato de grade
            string[] historicoTentativas = new string[QuantidadeMaximaTentativas];

            // Matriz que armazena as cores correspondentes de cada tentativa digitada
            ConsoleColor[][] historicoCores = new ConsoleColor[QuantidadeMaximaTentativas][];

            // Exibe título e instruções do jogo
            ExibirCabecalho();

            // Variável usada para controlar se o jogador venceu a partida atual
            bool jogadorAcertou = false;

            // Loop principal do jogo (controle de tentativas)
            for (int tentativaAtual = 0; tentativaAtual < QuantidadeMaximaTentativas; tentativaAtual++)
            {
                // Atualização do console antes de cada jogada para mostrar o tabuleiro sempre renovado
                ExibirCabecalho();

                // Exibe todas as tentativas já realizadas em formato de grade estilo Wordle
                ExibirHistoricoTentativas(historicoTentativas, historicoCores);

                Console.WriteLine();
                Console.WriteLine($"Tentativa {tentativaAtual + 1} de {QuantidadeMaximaTentativas}");

                // Obtém uma tentativa válida do usuário
                string tentativa = ObterTentativaValida();

                // Avalia a tentativa e guarda as cores para exibir no histórico
                ConsoleColor[] coresTentativa = AvaliarTentativa(tentativa, palavraSecreta);

                // Armazena a tentativa digitada no histórico da partida
                historicoTentativas[tentativaAtual] = tentativa;

                // Armazena as cores da tentativa para desenhar os quadrados coloridos depois
                historicoCores[tentativaAtual] = coresTentativa;

                // Atualização do console após a jogada para mostrar imediatamente a nova linha preenchida no tabuleiro
                ExibirCabecalho();

                // Exibe novamente a grade completa com a tentativa recém-digitada
                ExibirHistoricoTentativas(historicoTentativas, historicoCores);

                // Verifica condição de vitória
                if (tentativa == palavraSecreta)
                {
                    Console.WriteLine();

                    // Cor verde para indicar sucesso
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("Parabéns! Você acertou a palavra secreta!");
                    Console.ResetColor();

                    // Marca que o jogador venceu para evitar mostrar a mensagem de derrota no fim da rodada
                    jogadorAcertou = true;

                    // Interrompe apenas o loop das tentativas, mantendo o programa ativo para perguntar se deseja jogar novamente
                    break;
                }
            }

            // Caso o jogador use todas as tentativas sem acertar
            if (!jogadorAcertou)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Que pena! Você perdeu.");
                Console.ResetColor();

                // Mostra a palavra correta
                Console.WriteLine($"A palavra secreta era: {palavraSecreta}");
            }

            Console.WriteLine();

            // Pergunta ao jogador se ele deseja iniciar uma nova partida
            Console.Write("Deseja jogar novamente? (S/N): ");
            string? resposta = Console.ReadLine();

            // Normaliza a resposta para facilitar a comparação
            string respostaTratada = resposta?.Trim().ToUpper() ?? "N";

            // Se a resposta for diferente de S, o loop externo é encerrado e o programa finaliza
            if (respostaTratada != "S")
                break;
        }

        Console.WriteLine("Pressione ENTER para sair...");
        Console.ReadLine();
    }

    // Exibe o cabeçalho e instruções do jogo
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
        Console.WriteLine();

        // Novo bloco explicativo para informar que a interface usa quadrados coloridos no estilo Wordle
        Console.WriteLine("Tabuleiro:");
        Console.WriteLine("Cada letra será exibida dentro de um quadrado colorido.");
    }

    // Sorteia uma palavra aleatória da lista
    static string SortearPalavra(string[] palavras)
    {
        int indice = RandomNumberGenerator.GetInt32(0, palavras.Length);
        return palavras[indice];
    }

    
    // Obtém e valida a tentativa do usuário
    static string ObterTentativaValida()
    {
        while (true)
        {
            Console.Write("Digite uma palavra de 5 letras: ");
            string? entrada = Console.ReadLine();

            // Verifica se a entrada é vazia
            if (string.IsNullOrWhiteSpace(entrada))
            {
                Console.WriteLine("Entrada inválida.");
                continue;
            }

            // Remove espaços e converte para maiúsculo
            string tentativa = entrada.Trim().ToUpper();

            // Valida o tamanho da palavra
            if (tentativa.Length != TamanhoPalavra)
            {
                Console.WriteLine("A palavra deve ter exatamente 5 letras.");
                continue;
            }

            // Valida se contém apenas letras
            if (!ContemSomenteLetras(tentativa))
            {
                Console.WriteLine("Digite somente letras.");
                continue;
            }

            return tentativa;// Retorna tentativa válida
        }
    }

    // Verifica se a string contém apenas letras (sem números ou símbolos)
    static bool ContemSomenteLetras(string texto)
    {
        foreach (char c in texto)
        {
            if (!char.IsLetter(c))
                return false;
        }

        return true;
    }

    // Exibe a tentativa com cores baseadas na avaliação
    static void ExibirTentativaColorida(string tentativa, string palavraSecreta)
    {
        Console.Write("Resultado: ");

        // Avalia cada letra e retorna as cores correspondentes
        ConsoleColor[] cores = AvaliarTentativa(tentativa, palavraSecreta);

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

    // Novo método responsável por exibir o tabuleiro completo com todas as tentativas realizadas até o momento
    static void ExibirHistoricoTentativas(string[] historicoTentativas, ConsoleColor[][] historicoCores)
    {
        Console.WriteLine("Tabuleiro:");
        Console.WriteLine();

        // Percorre cada linha do tabuleiro
        for (int i = 0; i < QuantidadeMaximaTentativas; i++)
        {
            // Se ainda não houver tentativa nessa linha, exibe quadrados vazios
            if (historicoTentativas[i] == null)
            {
                for (int j = 0; j < TamanhoPalavra; j++)
                {
                    Console.Write("[   ] ");
                }

                Console.WriteLine();
                continue;
            }

            // Recupera a tentativa e suas respectivas cores para desenhar a linha preenchida
            string tentativa = historicoTentativas[i];
            ConsoleColor[] cores = historicoCores[i];

            // Percorre cada letra da linha atual
            for (int j = 0; j < TamanhoPalavra; j++)
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

    // Avalia a tentativa comparando com a palavra secreta
    static ConsoleColor[] AvaliarTentativa(string tentativa, string palavraSecreta)
    {
        // Array que guarda as cores de cada letra
        ConsoleColor[] cores = new ConsoleColor[TamanhoPalavra];

        // Controla quais letras da palavra secreta já foram utilizadas
        bool[] letrasJaUsadas = new bool[TamanhoPalavra];

        // Primeira passagem: verifica letras na posição correta (verde)
        for (int i = 0; i < TamanhoPalavra; i++)
        {
            if (tentativa[i] == palavraSecreta[i])
            {
                cores[i] = ConsoleColor.DarkGreen;
                letrasJaUsadas[i] = true;
            }
        }

         // Segunda passagem: verifica letras fora de posição (amarelo) ou inexistentes (vermelho)
        for (int i = 0; i < TamanhoPalavra; i++)
        {
            // Se já foi marcada como verde, ignora
            if (cores[i] == ConsoleColor.DarkGreen)
                continue;

            bool letraExisteEmOutraPosicao = false;

            // Procura a letra em outra posição da palavra secreta
            for (int j = 0; j < TamanhoPalavra; j++)
            {
                if (!letrasJaUsadas[j] && tentativa[i] == palavraSecreta[j])
                {
                    letraExisteEmOutraPosicao = true;
                    letrasJaUsadas[j] = true;
                    break;
                }
            }

             // Define a cor conforme o resultado
            if (letraExisteEmOutraPosicao)
                cores[i] = ConsoleColor.DarkYellow;
            else
                cores[i] = ConsoleColor.DarkRed;
        }
        return cores;
    }
}