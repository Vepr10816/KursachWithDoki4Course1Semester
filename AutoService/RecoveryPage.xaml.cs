using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AutoService
{
    /// <summary>
    /// Логика взаимодействия для RecoveryPage.xaml
    /// </summary>
    public partial class RecoveryPage : Page
    {
        //экземпляр класса управления БД
        DBHelper dbhelper = new DBHelper();

        //экземпляр класса работы с электронной почтой
        EmailHelper emailHelper = new EmailHelper();

        //экземпляр класса валидации
        ValidationData valid = new ValidationData();

        //экземпляр класса генерации рандомных чисел
        Random r = new Random();

        //Сгенерированный код
        int code;

        /// <summary>
        /// Инициализация страницы востанновления пароля
        /// </summary>
        public RecoveryPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Обработчик кнопки отправки электронного письма
        /// </summary>
        /// <param name="sender">ссылка на элемент управления</param>
        /// <param name="e">данные события</param>
        private void btnSendMessage_Click(object sender, RoutedEventArgs e)
        {
            code = r.Next(100000, 999999);
            if (dbhelper.EecuteQueryReaderOne($@"select count(Email) FROM Users where Email = '{tbEmail.Text}'", "count") == "1")
            {
                emailHelper.SendMail(tbEmail.Text, code).GetAwaiter();
                tbEmail.IsEnabled = false;
                btnSendMessage.IsEnabled = false;
            }
            else
                valid.Show("Такой почты нет в системе").GetAwaiter();
        }

        /// <summary>
        /// Обработчик кнопки проверки кода
        /// </summary>
        /// <param name="sender">ссылка на элемент управления</param>
        /// <param name="e">данные события</param>
        private void btnCheckCode_Click(object sender, RoutedEventArgs e)
        {
            if (tbCode.Text == code.ToString())
            {
                tbCode.IsEnabled = false;
                btnCheckCode.IsEnabled = false;
            }
            else
                valid.Show("Неверный код для восстановления").GetAwaiter();
        }

        /// <summary>
        /// Обработчик кнопки восстановления пароля
        /// </summary>
        /// <param name="sender">ссылка на элемент управления</param>
        /// <param name="e">данные события</param>
        private void btnRecovery_Click(object sender, RoutedEventArgs e)
        {
            if (tbCode.IsEnabled == true)
            {
                valid.Show("Пройдите все этапы восстановления пароля").GetAwaiter();
                return;
            }
            if(pbPassword.Password == pbPassword2.Password)
            {
                dbhelper.EecuteQuery($@"UPDATE Users SET Password = '{valid.Encrypt(pbPassword.Password)}' WHERE Email = '{tbEmail.Text}';");
                valid.Show("Успешное восстановление пароля").GetAwaiter();
                FrameManager.MainFrame.GoBack();
            }
            else
            {
                valid.Show("Пароли не совпадают").GetAwaiter();
            }
        }
    }
}
