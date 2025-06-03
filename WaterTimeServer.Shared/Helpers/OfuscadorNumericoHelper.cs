namespace WaterTimeServer.Shared.Helpers;

public static class OfuscadorNumerico
{
    private const int FatorMultiplicacao = 7;
    private const int Acrescimo = 13;
    private static readonly char[] caracteres = "K7JM2GN9PZC4H53BDYQWXA6E8R".ToCharArray();

    public static string OfuscarNumero(int numeroOriginal)
    {
        if (numeroOriginal < 100 || numeroOriginal > 999)
        {
            throw new ArgumentOutOfRangeException(nameof(numeroOriginal), "Numero deve estar entre 100 e 999.");
        }

        int numeroTransformado = (numeroOriginal * FatorMultiplicacao) + Acrescimo;
        string codigoBase = ConverterNumeroParaCodigo(numeroTransformado);
        char checksum = CalcularChecksum(numeroTransformado);

        return codigoBase + checksum;
    }

    public static int? ReverterCodigo(string codigoComChecksum)
    {
        if (string.IsNullOrWhiteSpace(codigoComChecksum) || codigoComChecksum.Length < 2)
        {
            return null;
        }

        string codigoBase = codigoComChecksum[..^1];
        char checksumInformado = codigoComChecksum[^1];

        int numeroTransformado = ConverterCodigoParaNumero(codigoBase);

        if (CalcularChecksum(numeroTransformado) != checksumInformado)
        {
            return null; // checksum invalido, código corrompido
        }

        int numeroOriginal = (numeroTransformado - Acrescimo) / FatorMultiplicacao;

        if ((numeroOriginal * FatorMultiplicacao) + Acrescimo != numeroTransformado)
        {
            return null; // Transformação inválida
        }

        return numeroOriginal;
    }

    private static string ConverterNumeroParaCodigo(int numero)
    {
        string numeroTexto = numero.ToString();
        string codigo = "";

        foreach (char digito in numeroTexto)
        {
            int valor = int.Parse(digito.ToString());
            codigo += caracteres[valor];
        }

        return codigo;
    }

    private static int ConverterCodigoParaNumero(string codigo)
    {
        string numeroTexto = "";

        foreach (char letra in codigo.ToUpperInvariant())
        {
            int indice = Array.IndexOf(caracteres, letra);
            if (indice == -1)
            {
                throw new FormatException("Codigo inválido.");
            }
            numeroTexto += indice.ToString();
        }

        return int.Parse(numeroTexto);
    }

    private static char CalcularChecksum(int numeroTransformado)
    {
        int soma = 0;
        foreach (char digito in numeroTransformado.ToString())
        {
            soma += int.Parse(digito.ToString());
        }

        int indice = soma % caracteres.Length;
        return caracteres[indice];
    }
}