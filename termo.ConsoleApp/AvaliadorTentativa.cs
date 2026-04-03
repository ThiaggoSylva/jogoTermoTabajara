namespace Termo.ConsoleApp;

static class AvaliadorTentativa
{
    // Avalia a tentativa comparando com a palavra secreta
    public static ConsoleColor[] AvaliarTentativa(string tentativa, string palavraSecreta, int tamanhoPalavra)
    {
        // Array que guarda as cores de cada letra
        ConsoleColor[] cores = new ConsoleColor[tamanhoPalavra];

        // Controla quais letras da palavra secreta já foram utilizadas
        bool[] letrasJaUsadas = new bool[tamanhoPalavra];

        // Primeira passagem: verifica letras na posição correta (verde)
        for (int i = 0; i < tamanhoPalavra; i++)
        {
            if (tentativa[i] == palavraSecreta[i])
            {
                cores[i] = ConsoleColor.DarkGreen;
                letrasJaUsadas[i] = true;
            }
        }

        // Segunda passagem: verifica letras fora de posição (amarelo) ou inexistentes (vermelho)
        for (int i = 0; i < tamanhoPalavra; i++)
        {
            // Se já foi marcada como verde, ignora
            if (cores[i] == ConsoleColor.DarkGreen)
                continue;

            bool letraExisteEmOutraPosicao = false;

            // Procura a letra em outra posição da palavra secreta
            for (int x = 0; x < tamanhoPalavra; x++)
            {
                if (!letrasJaUsadas[x] && tentativa[i] == palavraSecreta[x])
                {
                    letraExisteEmOutraPosicao = true;
                    letrasJaUsadas[x] = true;
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