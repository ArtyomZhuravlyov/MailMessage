using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace mailMessage
{
    class Program
    {
        static void Main(string[] args)
        {
            EmailSettings emailSettings = new EmailSettings();

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
                    .AppendLine("Новый заказ обработан")
                    .AppendLine("---")
                    .AppendLine("Товары:");



                body.AppendFormat("Общая стоимость: {0:c}", 20)
                    .AppendLine("---")
                    .AppendLine("Доставка:");


                MailMessage mailMessage = new MailMessage(
                                       emailSettings.MailFromAddress,	// От кого
                                       emailSettings.MailToAddress,		// Кому
                                       "Новый заказ отправлен!",		// Тема
                                       body.ToString()); 				// Тело письма

                if (emailSettings.WriteAsFile)
                {
                    mailMessage.BodyEncoding = Encoding.UTF8;
                }

                smtpClient.Send(mailMessage);
            }
        }
    }

    public class EmailSettings
    {
        public string MailToAddress = "artyom.zhuravlyov.spbk@mail.ru";
        public string MailFromAddress = "art.zhuravlew@yandex.ru";
        public bool UseSsl = true;
        public string Username = "artyom.zhuravlyov.spbk@mail.ru";
        public string Password = "3a22f5c9";
        public string ServerName = "smtp.mail.ru";
        public int ServerPort = 465;
        public bool WriteAsFile = true;
        public string FileLocation = @"C:\Users\artem zhuravlev\Desktop\";
    }
}
