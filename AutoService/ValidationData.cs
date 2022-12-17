using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace AutoService
{
    /// <summary>
    /// Валидация данных
    /// </summary>
    class ValidationData
    {
        /// <summary>
        /// Валидация на цифры и английские буквы
        /// </summary>
        /// <param name="c"></param>
        /// <returns>bool значение</returns>
        bool IsValidEngNum(char c)
        {
            if (c >= '0' && c <= '9')
                return true;
            if (c >= 'A' && c <= 'Z')
                return true;
            if (c >= 'a' && c <= 'z')
                return true;
            string spec = "!\"#$%&'()*+,-./::<=>?@[\\]:_{|}";
            if (c.ToString().IndexOfAny(spec.ToCharArray()) > -1)
                return true;
            return false;
        }

        /// <summary>
        /// Валидация на цифры и русские буквы
        /// </summary>
        /// <param name="c"></param>
        /// <returns>bool значение</returns>
        bool IsValidRusNum(char c)
        {
            if (c >= '0' && c <= '9')
                return true;
            if (c >= 'А' && c <= 'Я')
                return true;
            if (c >= 'а' && c <= 'я')
                return true;
            string spec = "!\"#$%&'()*+,-./::<=>?@[\\]:_{|}";
            if (c.ToString().IndexOfAny(spec.ToCharArray()) > -1)
                return true;
            return false;
        }

        /// <summary>
        /// Валидация на русские буквы
        /// </summary>
        /// <param name="c"></param>
        /// <returns>bool значение</returns>
        bool IsValidRus(char c)
        {
            if (c >= '0' && c <= '9')
                return true;
            if (c >= 'А' && c <= 'Я')
                return true;
            if (c >= 'а' && c <= 'я')
                return true;
            string spec = "!\"#$%&'()*+,-./::<=>?@[\\]:_{|}";
            if (c.ToString().IndexOfAny(spec.ToCharArray()) > -1)
                return true;
            return false;
        }

        /// <summary>
        /// Валидация на цифры и английские буквы и русские буквы
        /// </summary>
        /// <param name="c"></param>
        /// <returns>bool значение</returns>
        bool IsValidRusEngNum(char c)
        {
            if (c >= '0' && c <= '9')
                return true;
            if (c >= 'A' && c <= 'Z')
                return true;
            if (c >= 'a' && c <= 'z')
                return true;
            if (c >= 'А' && c <= 'Я')
                return true;
            if (c >= 'а' && c <= 'я')
                return true;
            string spec = "!\"#$%&'()*+,-./::<=>?@[\\]:_{|}";
            if (c.ToString().IndexOfAny(spec.ToCharArray()) > -1)
                return true;
            return false;
        }

        /// <summary>
        /// Валидация на цифры 
        /// </summary>
        /// <param name="c"></param>
        /// <returns>bool значение</returns>
        bool IsValidNum(char c)
        {
            if (c >= '0' && c <= '9')
                return true;
            return false;
        }

        /// <summary>
        /// Обработчики заполнения и вставки данных в textbox
        /// </summary>
        /// <param name="sender">ссылка на элемент управления</param>
        /// <param name="e">данные события</param>
        public void tb1_PreviewTextInputAngNum(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !e.Text.All(IsValidEngNum);
        }

        public void tb1_PastingAngNum(object sender, DataObjectPastingEventArgs e)
        {
            var stringData = (string)e.DataObject.GetData(typeof(string));
            if (stringData == null || !stringData.All(IsValidEngNum))
                e.CancelCommand();
        }

        public void tb1_PreviewTextInputRusNum(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !e.Text.All(IsValidRusNum);
        }

        public void tb1_PastingRusNum(object sender, DataObjectPastingEventArgs e)
        {
            var stringData = (string)e.DataObject.GetData(typeof(string));
            if (stringData == null || !stringData.All(IsValidRusNum))
                e.CancelCommand();
        }

        public void tb1_PreviewTextInputRus(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !e.Text.All(IsValidRus);
        }

        public void tb1_PastingRus(object sender, DataObjectPastingEventArgs e)
        {
            var stringData = (string)e.DataObject.GetData(typeof(string));
            if (stringData == null || !stringData.All(IsValidRus))
                e.CancelCommand();
        }

        public void tb1_PreviewTextInputRusEngNum(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !e.Text.All(IsValidRusEngNum);
        }

        public void tb1_PastingRusEngNum(object sender, DataObjectPastingEventArgs e)
        {
            var stringData = (string)e.DataObject.GetData(typeof(string));
            if (stringData == null || !stringData.All(IsValidRusEngNum))
                e.CancelCommand();
        }


        public void tb1_PreviewTextInputNum(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !e.Text.All(IsValidNum);
        }

        public void tb1_PastingNum(object sender, DataObjectPastingEventArgs e)
        {
            var stringData = (string)e.DataObject.GetData(typeof(string));
            if (stringData == null || !stringData.All(IsValidNum))
                e.CancelCommand();
        }

        /// <summary>
        /// Валидация электронной почты
        /// </summary>
        /// <param name="email">Электронная почта</param>
        /// <returns>bool значение</returns>
        public bool IsValidEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                Show("Введен некорректный адрес электронной почты").GetAwaiter(); ;
                return false; // suggested by @TK-421
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                Show("Введен некорректный адрес электронной почты").GetAwaiter();
                return false;
            }
        }

        /// <summary>
        /// Валидация пароля
        /// </summary>
        /// <param name="s">пароль</param>
        /// <returns>bool значение</returns>
        public bool IsValidPassword(string s)
        {
            int contain = 0;
            if (s.Length >= 7)
                contain += 1;
            string zag = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            if (s.IndexOfAny(zag.ToCharArray()) > -1)
                contain += 1;
            string small = "abcdefghijklmnopqrstuvwxyz";
            if (s.IndexOfAny(small.ToCharArray()) > -1)
                contain += 1;
            string numb = "123456789";
            if (s.IndexOfAny(numb.ToCharArray()) > -1)
                contain += 1;
            string spec = "!\"#$%&'()*+,-./::<=>?@[\\]:_{|}";
            if (s.IndexOfAny(spec.ToCharArray()) > -1)
                contain += 1;
            if (contain == 5)
                return true;
            else
            {
                Show("Пароль должен содержать не менее 7-ми символов, хотя бы одну заглавную букву и хотя бы один спец символ, Буквы латинские").GetAwaiter();
                return false;
            }
        }

        /// <summary>
        /// хэширование MD5
        /// </summary>
        /// <param name="decrypted"></param>
        /// <returns></returns>
        public string Encrypt(string decrypted)
        {
            byte[] data = UTF8Encoding.UTF8.GetBytes(decrypted);
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider();
            tripDes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes("123"));
            tripDes.Mode = CipherMode.ECB;
            ICryptoTransform transform = tripDes.CreateEncryptor();
            byte[] result = transform.TransformFinalBlock(data, 0, data.Length);
            return Convert.ToBase64String(result);
        }

        public string Decrypt(string encrypted)
        {
            byte[] data = Convert.FromBase64String(encrypted);
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider();
            tripDes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes("123"));
            tripDes.Mode = CipherMode.ECB;
            ICryptoTransform transform = tripDes.CreateDecryptor();
            byte[] result = transform.TransformFinalBlock(data, 0, data.Length);
            return UTF8Encoding.UTF8.GetString(result);
        }

        /// <summary>
        /// Вывод ошибок
        /// </summary>
        /// <param name="errorMessage">сообщение об ошибке</param>
        /// <returns></returns>
        public async Task Show(string errorMessage)
        {
            Grid gridError = Application.Current.MainWindow.FindName("ErrorGrid") as Grid;
            RowDefinition errorRow = Application.Current.MainWindow.FindName("ErrorRow") as RowDefinition;
            gridError.Children[0].Visibility = Visibility.Visible;
            Label label = gridError.Children[0] as Label;
            label.Content = errorMessage;
            gridError.Background = new SolidColorBrush(Colors.Black);

            gridError.VerticalAlignment = VerticalAlignment.Stretch;
            await Task.Delay(3000);
            gridError.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#445c93"));
            gridError.VerticalAlignment = VerticalAlignment.Bottom;
            label.Visibility = Visibility.Hidden;
        }
    }
}
