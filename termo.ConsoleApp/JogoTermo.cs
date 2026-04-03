using System;

namespace Termo.ConsoleApp;

static class JogoTermo
{
    // Quantidade máxima de tentativas que o jogador possui
    public const int QuantidadeMaximaTentativas = 5;

    // Tamanho fixo da palavra (regra do jogo)
    public const int TamanhoPalavra = 5;

    // Lista de palavras possíveis (todas com 5 letras)
    static readonly string[] palavras =
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

    public static void Executar()
    {
        // Loop externo responsável por manter o jogo funcionando enquanto o jogador quiser jogar novamente
        while (true)
        {
            ExecutarPartida();

            // Se a resposta for diferente de S, o loop externo é encerrado e o programa finaliza
            if (!DesejaJogarNovamente())
                break;
        }

        Console.WriteLine("Pressione ENTER para sair...");
        Console.ReadLine();
    }

    // concentra toda a execução de uma partida individual.
    static void ExecutarPartida()
    {
        // Sorteia uma palavra aleatória da lista
        string palavraSecreta = PalavraService.SortearPalavra(palavras);

        // Matriz que armazena todas as tentativas da partida atual para exibir o histórico em formato de grade
        string[] historicoTentativas = new string[QuantidadeMaximaTentativas];

        // Matriz que armazena as cores correspondentes de cada tentativa digitada
        ConsoleColor[][] historicoCores = new ConsoleColor[QuantidadeMaximaTentativas][];

        // Exibe título e instruções do jogo
        Tabuleiro.ExibirCabecalho();

        // Variável usada para controlar se o jogador venceu a partida atual
        bool jogadorAcertou = false;

        // Loop principal do jogo (controle de tentativas)
        for (int tentativaAtual = 0; tentativaAtual < QuantidadeMaximaTentativas; tentativaAtual++)
        {
            ExecutarTentativa(
                tentativaAtual,
                palavraSecreta,
                historicoTentativas,
                historicoCores,
                ref jogadorAcertou
            );

            if (jogadorAcertou)
                break;
        }

        // Caso o jogador use todas as tentativas sem acertar
        if (!jogadorAcertou)
            ExibirMensagemDerrota(palavraSecreta);
    }

    // isola o fluxo de uma única tentativa do jogador.
    static void ExecutarTentativa(
        int tentativaAtual,
        string palavraSecreta,
        string[] historicoTentativas,
        ConsoleColor[][] historicoCores,
        ref bool jogadorAcertou)
    {
        // Atualização do console antes de cada jogada para mostrar o tabuleiro sempre renovado
        Tabuleiro.ExibirCabecalho();

        // Exibe todas as tentativas já realizadas em formato de grade estilo Wordle
        Tabuleiro.ExibirHistoricoTentativas(historicoTentativas, historicoCores);

        Console.WriteLine();
        Console.WriteLine($"Tentativa {tentativaAtual + 1} de {QuantidadeMaximaTentativas}");

        // Obtém uma tentativa válida do usuário
        string tentativa = ValidadorEntrada.ObterTentativaValida(TamanhoPalavra);

        // Avalia a tentativa e guarda as cores para exibir no histórico
        ConsoleColor[] coresTentativa = AvaliadorTentativa.AvaliarTentativa(tentativa, palavraSecreta, TamanhoPalavra);

        // Armazena a tentativa digitada no histórico da partida
        historicoTentativas[tentativaAtual] = tentativa;

        // Armazena as cores da tentativa para desenhar os quadrados coloridos depois
        historicoCores[tentativaAtual] = coresTentativa;

        // Atualização do console após a jogada para mostrar imediatamente a nova linha preenchida no tabuleiro
        Tabuleiro.ExibirCabecalho();

        // Exibe novamente a grade completa com a tentativa recém-digitada
        Tabuleiro.ExibirHistoricoTentativas(historicoTentativas, historicoCores);

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
        }
    }

    // centraliza a exibição da mensagem de derrota.
    static void ExibirMensagemDerrota(string palavraSecreta)
    {
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine("Que pena! Você perdeu.");
        Console.ResetColor();

        // Mostra a palavra correta
        Console.WriteLine($"A palavra secreta era: {palavraSecreta}");
    }

    // encapsula a pergunta ao jogador para reiniciar a partida.
    static bool DesejaJogarNovamente()
    {
        Console.WriteLine();

        // Pergunta ao jogador se ele deseja iniciar uma nova partida
        Console.Write("Deseja jogar novamente? (S/N): ");
        string? resposta = Console.ReadLine();

        // Normaliza a resposta para facilitar a comparação
        string respostaTratada = resposta?.Trim().ToUpper() ?? "N";

        return respostaTratada == "S";
    }
}