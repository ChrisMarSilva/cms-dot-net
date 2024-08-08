//using System.Net;
//using System.Net.Mail;

using MailKit.Net.Smtp;
using MimeKit;

Console.WriteLine("INI"); 
try
{
    //var autentica = "T";
    //var usuario = "chris.mar.silva@gmail.com";
    //var senha = "uriv euvg gdxc htbi";
    //var host = "smtp.gmail.com";
    //var port = 465; // 465 (com SSL) // 587 (com TLS)

    //var autentica = "S";
    //var usuario = "cristiano.martins@jdconsultores.com.br";
    //var senha = "JD@cm2024";
    //var host = "10.10.20.12";
    //var port = 25;

    //var autentica = "S";
    //var usuario = "matheus.silva@jdconsultores.com.br";
    //var senha = "12345";
    //var host = "10.10.20.12";
    //var port = 25;

    var autentica = "S";
    var usuario = "cristiano.martins@jdconsultores.com.br";
    var senha = "JD@cm2024";
    var host = "10.10.20.12";  // var host = "smtp.jdconsultores.com.br";
    var port = 25; 

    Console.WriteLine("Dados MimeMessage");
    var message = new MimeMessage();
    message.From.Add(new MailboxAddress(name: usuario, address: usuario));
    message.To.Add(new MailboxAddress(name: usuario, address: usuario));
    message.Subject = "Test subject";
    message.Body = new TextPart("plain") { Text = "Test body" };

    using (var client = new SmtpClient())
    {
        Console.WriteLine("Connect");
        client.Connect(host: host, port: port, useSsl: autentica == "T");

        Console.WriteLine("Authenticate");
        client.Authenticate(userName: usuario, password: senha); // Note: only needed if the SMTP server requires authentication

        Console.WriteLine("Send");
        client.Send(message);

        Console.WriteLine("Disconnect");
        client.Disconnect(quit: true);
    }

    //Console.WriteLine("Dados MailAddress");
    //var to = new MailAddress(usuario);
    //var from = new MailAddress(usuario);

    //Console.WriteLine("Dados MailMessage");
    //var email = new MailMessage(from, to);
    //email.Subject = "Testing out email sending";
    //email.Body = "Hello all the way from the land of C#";

    //Console.WriteLine("Dados SmtpClient");
    //var client = new SmtpClient();
    //client.Host = host;
    //client.Port = port;
    //client.UseDefaultCredentials = false;
    //client.Credentials = new NetworkCredential(usuario, senha);
    //client.DeliveryMethod = SmtpDeliveryMethod.Network;
    //client.EnableSsl = autentica == "T";  // (authType == "SSL" || authType == "TLS");

    //Console.WriteLine("send...");
    //client.Send(email);
    //Console.WriteLine("ok");
}
//catch (SmtpException ex)
//{
//    Console.WriteLine($"ERRO-1: {ex.Message}");
//}
catch (Exception ex)
{
    Console.WriteLine($"ERRO-2: {ex.Message}");
}
finally 
{
    Console.WriteLine("FIM");
    Console.ReadLine();
}   