using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AutoService
{
    /// <summary>
    /// Управление почтовым клиеном
    /// </summary>
    class EmailHelper
    {
        /// <summary>
        /// Отправка электронного письма 
        /// </summary>
        /// <param name="email">Электронная почта</param>
        /// <param name="r">Код для восстановления</param>
        public async Task SendMail(string email, int r)
        {
            // отправитель - устанавливаем адрес и отображаемое в письме имя
            MailAddress from = new MailAddress("vepritskiy1999@mail.ru", "Сервис-Про");
            // кому отправляем
            MailAddress to = new MailAddress(email);
            // создаем объект сообщения
            MailMessage m = new MailMessage(from, to);
            // тема письма
            m.Subject = "Код для восстановления пароля";
            // текст письма
            m.Body = $@"<h1>Ваш код для восстановления пароля: <b>{r}</b><p>";
            // письмо представляет код html
            m.IsBodyHtml = true;
            // адрес smtp-сервера и порт, с которого будем отправлять письмо
            SmtpClient smtp = new SmtpClient("smpt.mail.ru", 25);
            // логин и пароль
            smtp.Credentials = new NetworkCredential("vepritskiy1999@mail.ru", "NAFzSL6mwayXkA5sd6bW");
            smtp.EnableSsl = true;
            try
            {
                await smtp.SendMailAsync(m);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            Console.Read();
        }

        /// <summary>
        /// Отправка электронного письма
        /// </summary>
        /// <param name="email">электронная почта</param>
        /// <param name="title">заголовок</param>
        /// <param name="message">сообщение</param>
        public async Task SendMailOrder(string email, string title, string message)
        {
            // отправитель - устанавливаем адрес и отображаемое в письме имя
            MailAddress from = new MailAddress("vepritskiy1999@mail.ru", "Сервис-Про");
            // кому отправляем
            MailAddress to = new MailAddress(email);
            // создаем объект сообщения
            MailMessage m = new MailMessage(from, to);
            // тема письма
            m.Subject = title;
            // текст письма
            m.Body = message;
            // письмо представляет код html
            m.IsBodyHtml = true;
            // адрес smtp-сервера и порт, с которого будем отправлять письмо
            SmtpClient smtp = new SmtpClient("smpt.mail.ru", 25);
            // логин и пароль
            smtp.Credentials = new NetworkCredential("vepritskiy1999@mail.ru", "NAFzSL6mwayXkA5sd6bW");
            smtp.EnableSsl = true;
            try
            {
                await smtp.SendMailAsync(m);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            Console.Read();
        }
    }
}
