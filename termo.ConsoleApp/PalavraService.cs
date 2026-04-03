using System.Security.Cryptography;

namespace Termo.ConsoleApp;

static class PalavraService
{
    // Sorteia uma palavra aleatória da lista
    public static string SortearPalavra(string[] palavras)
    {
        int indice = RandomNumberGenerator.GetInt32(0, palavras.Length);
        return palavras[indice];
    }
}