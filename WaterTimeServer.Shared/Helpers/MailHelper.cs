using MailKit.Security;
using MimeKit;
using Microsoft.Extensions.Configuration;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace WaterTimeServer.Shared.Helpers;

public sealed class ConfiguracaoEmail
{
    public string ServidorSmtp { get; init; } = string.Empty;
    public int Porta { get; init; }
    public bool UsarSsl { get; init; }
    public string Usuario { get; init; } = string.Empty;
    public string Senha { get; init; } = string.Empty;
    public string EnderecoRemetentePadrao { get; init; } = string.Empty;
    public string NomeRemetentePadrao { get; init; } = string.Empty;
}

public static class EmailHelper
{
    // Using a simple object as a lock since `Lock` does not exist and would
    // cause a compilation error. The lock is only used to ensure that the
    // configuration object is replaced atomically.
    private static readonly object _sincronizador = new();
    private static ConfiguracaoEmail _configuracao;

    static EmailHelper()
    {
        _configuracao = new ConfiguracaoEmail
        {
            ServidorSmtp = "mail.lojadeamigos.com.br",
            Porta = 587,
            UsarSsl = true,
            Usuario = "no-reply@lojadeamigos.com.br",
            Senha = "bO3$q2h86",
            EnderecoRemetentePadrao = "no-reply@lojadeamigos.com.br",
            NomeRemetentePadrao = "Sistema"
        };
    }

    public static void Configurar(
        ConfiguracaoEmail novaConfiguracao)
    {
        ArgumentNullException.ThrowIfNull(novaConfiguracao);

        lock (_sincronizador)
        {
            _configuracao = novaConfiguracao;
        }
    }

    public static async Task EnviarAsync(

        string enderecoDestinatario,
        string assunto,
        string corpoHtml)
    {
        if (_configuracao is null)
        {
            throw new InvalidOperationException(
                "EmailHelper não foi configurado.");
        }

        MimeMessage mensagem = new MimeMessage
        {
            Subject = assunto
        };

        mensagem.From.Add(
            new MailboxAddress(
                _configuracao.NomeRemetentePadrao,
                _configuracao.EnderecoRemetentePadrao));

        mensagem.To.Add(
            new MailboxAddress(
                string.Empty,
                enderecoDestinatario));

        mensagem.Body = new BodyBuilder
        {
            HtmlBody = corpoHtml
        }.ToMessageBody();

        using SmtpClient cliente = new SmtpClient();

        SecureSocketOptions opcoesSocket =
            _configuracao.UsarSsl
                ? SecureSocketOptions.StartTls
                : SecureSocketOptions.Auto;

        try
        {

            await cliente.ConnectAsync(
                _configuracao.ServidorSmtp,
                _configuracao.Porta,
                opcoesSocket);

            if (!string.IsNullOrWhiteSpace(
                _configuracao.Usuario))
            {
                await cliente.AuthenticateAsync(
                    _configuracao.Usuario,
                    _configuracao.Senha);
            }

        }
        catch (Exception e)
        {
            throw;
        }

        await cliente.SendAsync(mensagem);
            await cliente.DisconnectAsync(true);        
    }
}
