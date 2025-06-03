using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public static class CriptografiaHelper
{    
    private const string ChaveMestre = "wEr9@bF2!xLz7$kP4#vNqT1uD6&hSjZ8";
    private const string Sal = "P0aXs3$eQ9n!Lm6@rY5wV2zT8dC4fB7u";

    private static void DerivarChaveEIV(out byte[] chave, out byte[] iv)
    {
        using var derivador = new Rfc2898DeriveBytes(ChaveMestre, Encoding.UTF8.GetBytes(Sal), 100_000, HashAlgorithmName.SHA256);
        chave = derivador.GetBytes(32); 
        iv = derivador.GetBytes(16);    
    }

    public static string Criptografar(string texto)
    {
        DerivarChaveEIV(out byte[] chave, out byte[] iv);

        using var aes = Aes.Create();
        aes.Key = chave;
        aes.IV = iv;

        using var criptografador = aes.CreateEncryptor(aes.Key, aes.IV);
        using var ms = new MemoryStream();
        using (var cs = new CryptoStream(ms, criptografador, CryptoStreamMode.Write))
        using (var sw = new StreamWriter(cs, Encoding.UTF8))
        {
            sw.Write(texto);
        }

        return Convert.ToBase64String(ms.ToArray());
    }

    public static string Descriptografar(string textoCriptografado)
    {
        DerivarChaveEIV(out byte[] chave, out byte[] iv);

        using var aes = Aes.Create();
        aes.Key = chave;
        aes.IV = iv;

        using var descriptografador = aes.CreateDecryptor(aes.Key, aes.IV);
        using var ms = new MemoryStream(Convert.FromBase64String(textoCriptografado));
        using var cs = new CryptoStream(ms, descriptografador, CryptoStreamMode.Read);
        using var sr = new StreamReader(cs, Encoding.UTF8);
        return sr.ReadToEnd();
    }
}
