namespace Termo.ConsoleApp;

static class ValidadorEntrada
{
    // Obtém e valida a tentativa do usuário
    public static string ObterTentativaValida(int tamanhoPalavra)
    {
        while (true)
        {
            Console.Write($"Digite uma palavra de {tamanhoPalavra} letras: ");
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
            if (tentativa.Length != tamanhoPalavra)
            {
                Console.WriteLine($"A palavra deve ter exatamente {tamanhoPalavra} letras.");
                continue;
            }

            // Valida se contém apenas letras
            if (!ContemSomenteLetras(tentativa))
            {
                Console.WriteLine("Digite somente letras.");
                continue;
            }

            return tentativa; // Retorna tentativa válida
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
}