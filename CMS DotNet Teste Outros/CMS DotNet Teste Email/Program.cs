using System.Net;
using System.Net.Mail;

Console.WriteLine("INI"); 
try
{
    //var usuario = "chris.mar.silva@gmail.com";
    //var senha = "uriv euvg gdxc htbi";
    //var host = "smtp.gmail.com";
    //var port = 587; // 465 (com SSL) // 587 (com TLS)

    var usuario = "cristiano.martins@jdconsultores.com.br";
    var senha = "JD@cm2024";
    //var host = "10.10.20.12";
    var host = "smtp.jdconsultores.com.br";
    var port = 25;

    Console.WriteLine("Dados MailAddress");
    var to = new MailAddress(usuario);
    var from = new MailAddress(usuario);

    Console.WriteLine("Dados MailMessage");
    var email = new MailMessage(from, to);
    email.Subject = "Testing out email sending";
    email.Body = "Hello all the way from the land of C#";

    Console.WriteLine("Dados SmtpClient");
    var client = new SmtpClient();
    client.Host = host;
    client.Port = port;
    client.UseDefaultCredentials = false;
    client.Credentials = new NetworkCredential(usuario, senha);
    client.DeliveryMethod = SmtpDeliveryMethod.Network;
    client.EnableSsl = true;

    Console.WriteLine("send...");
    client.Send(email);

    Console.WriteLine("ok");
}
catch (SmtpException ex)
{
    Console.WriteLine($"ERRO-1: {ex.Message}");
}
catch (Exception ex)
{
    Console.WriteLine($"ERRO-2: {ex.Message}");
}
finally 
{
    Console.WriteLine("FIM");
    Console.ReadLine();
}   