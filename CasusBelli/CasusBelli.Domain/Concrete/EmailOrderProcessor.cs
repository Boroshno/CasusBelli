using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Net.Mail;
using System.Text;
using CasusBelli.Domain.Abstract;
using CasusBelli.Domain.Entities;

namespace CasusBelli.Domain.Concrete
{
    public class EMailSettings
    {
        public string mailToAddress = "admin@casusbelli.com.ua";
        public string mailFromAddress = "admin@casusbelli.com.ua";
        public string subject = "New order";
        public string body = "";
    }
    public class EmailOrderProcessor:IOrderProcessor
    {
        private EMailSettings mailSettings;

        public EmailOrderProcessor(EMailSettings mailSettingsParam)
        {
            mailSettings = mailSettingsParam;
        }

        public void ProcessOrder(OrderDetails orderDetails)
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(mailSettings.mailToAddress);
            mail.From = new MailAddress(mailSettings.mailFromAddress);
            mail.Subject = mailSettings.subject;
            mail.Body = CreateBody(orderDetails);
            mail.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "mail.casusbelli.com.ua";
            smtp.Port = 25;
            smtp.Credentials = new System.Net.NetworkCredential
            ("admin@casusbelli.com.ua", "aZqe14$3");// Enter seders User name and password
            smtp.Send(mail);
        }

        private string CreateBody(OrderDetails orderDetails)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Нове замовлення:");
            sb.AppendLine();
            sb.AppendFormat("Клієнти цікавиться в покупці: {0}", orderDetails.ProductSubType.SubTypeName);
            sb.AppendLine();
            sb.AppendFormat("Ім'я: {0}", orderDetails.Name);
            sb.AppendLine();
            sb.AppendFormat("З міста {0}. Нова Пошта №{1}", orderDetails.City, orderDetails.NovaPoshta);
            sb.AppendFormat("Пошта: {0}", orderDetails.Email);
            sb.AppendLine();
            sb.AppendFormat("Телефон: {0}", orderDetails.Phone);
            sb.AppendLine();
            sb.AppendFormat("Його повідомлення: {0}", orderDetails.Message);
            return sb.ToString();
        }
    }
}
