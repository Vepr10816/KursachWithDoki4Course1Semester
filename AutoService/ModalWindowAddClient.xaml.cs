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
using System.Windows.Shapes;

namespace AutoService
{
    /// <summary>
    /// Логика взаимодействия для ModalWindowAddClient.xaml
    /// </summary>
    public partial class ModalWindowAddClient : Window
    {
        //экземпляр класса управления БД
        DBHelper dbhelper = new DBHelper();

        //экземпляр класса работы с электронной почтой
        EmailHelper emailHelper = new EmailHelper();

        //экземпляр класса валидации
        ValidationData valid = new ValidationData();

        static string email = "";
        int Who = 0;
        string id = "";
        
        /// <summary>
        /// Инициализация компонентов модального окна
        /// </summary>
        /// <param name="Email">электронная почта клиента</param>
        /// <param name="who">разграничение на администратора и клиента</param>
        /// <param name="ID">уникальный индетификатор пользователя</param>
        public ModalWindowAddClient(string Email, int who, string ID)
        {
            InitializeComponent();
            email = Email;
            if (who == 1)
            {
                rtMessage.Visibility = Visibility.Visible;
                tbTitle.Text = "Введите обоснование отмены";
            }
            Who = who;
            id = ID;
        }

        /// <summary>
        /// Обработчик сохранения данных
        /// </summary>
        /// <param name="sender">ссылка на элемент управления</param>
        /// <param name="e">данные события</param>
        private void AddClient_Click(object sender, RoutedEventArgs e)
        {
            if (Who == 0)
            {
                if (!ClientNumber.Text.Contains("_") && !passportSeries.Text.Contains("_") && !passportNumber.Text.Contains("_"))
                {
                    string[] atributes = { ClientNumber.Text.Replace("+7", ""), passportSeries.Text, passportNumber.Text, email };
                    dbhelper.InsertInto("Client", atributes);
                    this.Close();
                }
                else
                {

                    valid.Show("Заполните все поля").GetAwaiter();
                }
            }
            else
            {
                if(new TextRange(rtMessage.Document.ContentStart, rtMessage.Document.ContentEnd).Text != "")
                {
                    dbhelper.DeleteInto("orderclient", id);
                    emailHelper.SendMailOrder(email, $@"Заказ отменен", $@"<h1>Ваш заказ № {id} отменен.<h1><p>{new TextRange(rtMessage.Document.ContentStart, rtMessage.Document.ContentEnd).Text}</p>").GetAwaiter();
                    this.Close();
                }

            }
        }

        /// <summary>
        /// Обработчик закрытия модального окна
        /// </summary>
        /// <param name="sender">ссылка на элемент управления</param>
        /// <param name="e">данные события</param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Who == 0)
            {
                if (dbhelper.EecuteQueryReaderOne($@"select count(Email) FROM Client where Email = '{email}'", "count") == "0")
                {
                    dbhelper.DeleteInto("Users", email);
                }
            }
        }
    }
}
