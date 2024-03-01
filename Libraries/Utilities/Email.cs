using AppListaDeCompras.Models;
using System.Net.Mail;

namespace AppListaDeCompras.Libraries.Utilities
{
	public class Email
	{
		public static void SendEmailWithAccessCode(User user)
		{
			var smtp = App.Current.MainPage.Handler.MauiContext.Services.GetRequiredService<SmtpClient>();

			MailMessage msg = new MailMessage();
			msg.From = new MailAddress("michaelrhcp@gmail.com");
			msg.To.Add(user.Email);
			msg.Subject = "App Lista de Compras - Código de Acesso";
			msg.Body = $"Seu código de acesso é {user.AccessCodeTemp}";

			smtp.Send(msg);
		}
	}
}
