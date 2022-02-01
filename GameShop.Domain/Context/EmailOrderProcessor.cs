using System.Net;
using System.Net.Mail;
using System.Text;
using GameShop.Domain.Repositories;
using GameShop.Domain.Entities;
namespace GameShop.Domain.Context
{
    public class EmailSettings
    {
        public string MailToAddress = "vadym2zolotko@gmail.com";
        public string MailFromAddress = "vadim1zolotko@gmail.com";
        public bool UseSsl = true;
        public string Username = "vadim1zolotko@gmail.com";
        public string Password = "hgeohfrnrqytdcou";
        public string ServerName = "smtp.gmail.com";
        public int ServerPort = 587;
        public bool WriteAsFile = true;
        public string FileLocation = @"C:\KPI\BD";
    }

    public class EmailOrderProcessor : IOrderProcessor
    {
        private EmailSettings emailSettings;

        public EmailOrderProcessor(EmailSettings settings)
        {
            emailSettings = settings;
        }

        public void ProcessOrder(Cart cart, ShippingDetails shippingInfo)
        {
            emailSettings.WriteAsFile = true;
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.EnableSsl = emailSettings.UseSsl;
                smtpClient.Host = emailSettings.ServerName;
                smtpClient.Port = emailSettings.ServerPort;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials
                    = new NetworkCredential(emailSettings.Username, emailSettings.Password);

                if (emailSettings.WriteAsFile)
                {
                    smtpClient.DeliveryMethod
                        = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smtpClient.PickupDirectoryLocation = emailSettings.FileLocation;
                    smtpClient.EnableSsl = false;
                }

                StringBuilder body = new StringBuilder()
                    .AppendLine("new order processed")
                    .AppendLine("---")
                    .AppendLine("Games:");

                foreach (var line in cart.Lines)
                {
                    var subtotal = line.Game.Price * line.Count;
                    body.AppendFormat("{0} x {1} (Total: {2:c}",
                        line.Count, line.Game.Name, subtotal);
                }

                body.AppendFormat("Total Price: {0:c}", cart.CalculateValue())
                    .AppendLine("---")
                    .AppendLine("Deliver:")
                    .AppendLine(shippingInfo.Name)
                    .AppendLine(shippingInfo.Line1)
                    .AppendLine(shippingInfo.Line2 ?? "")
                    .AppendLine(shippingInfo.City)
                    .AppendLine(shippingInfo.Country)
                    .AppendLine("---");
                MailMessage mailMessage = new MailMessage(
                                       emailSettings.MailFromAddress,	// От кого
                                       emailSettings.MailToAddress,		// Кому
                                       "ORDER HAS BEEN SENT!",		// Тема
                                       body.ToString()); 				// Тело письма

                if (emailSettings.WriteAsFile)
                {
                    mailMessage.BodyEncoding = Encoding.UTF8;
                }

                smtpClient.Send(mailMessage);
            }
        }
    }
}
