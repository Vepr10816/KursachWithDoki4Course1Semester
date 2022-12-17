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
    /// Логика взаимодействия для SignInPage.xaml
    /// </summary>
    public partial class SignInPage : Page
    {
        //экземпляр класса управления БД
        DBHelper dbhelper = new DBHelper();

        //экземпляр класса валидации
        ValidationData valid = new ValidationData();

        /// <summary>
        /// Инициализая компонентов страницы авторизации
        /// </summary>
        public SignInPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Обработчик вводимых значений в текстовое поле
        /// </summary>
        /// <param name="sender">ссылка на элемент управления</param>
        /// <param name="e">данные события</param>
        private void tb1_PreviewTextInput(object sender, TextCompositionEventArgs e) => new ValidationData().tb1_PreviewTextInputAngNum(sender, e);

        /// <summary>
        /// Обработчик вставки значений в текстовое поле
        /// </summary>
        /// <param name="sender">ссылка на элемент управления</param>
        /// <param name="e">данные события</param>
        private void tb1_Pasting(object sender, DataObjectPastingEventArgs e) => new ValidationData().tb1_PastingAngNum(sender, e);

        /// <summary>
        /// Обработчик кнопки входа
        /// </summary>
        /// <param name="sender">ссылка на элемент управления</param>
        /// <param name="e">данные события</param>
        private void btnAuth_Click(object sender, RoutedEventArgs e)
        {
            string role = dbhelper.Authorization(tbLogin.Text, valid.Encrypt(tbPassword.Password));
            if (role !=null)
            {
                if(role == "Администратор")
                {
                    FrameManager.MainFrame.Navigate(new AdminPage());
                }
                if (role == "Сотрудник отдела закупок")
                {
                    FrameManager.MainFrame.Navigate(new PurchasePage());
                }
                if (role == "Сотрудник склада")
                {
                    FrameManager.MainFrame.Navigate(new WarehousePage());
                }
                if (role == "Сотрудник отдела продаж")
                {
                    FrameManager.MainFrame.Navigate(new SalesDepartmentPage(tbLogin.Text));
                }
                if (role == "Сотрудник ремонтного отдела")
                {
                    FrameManager.MainFrame.Navigate(new RepairPage(tbLogin.Text));
                }
                if (role == "Клиент")
                {
                    FrameManager.MainFrame.Navigate(new ClientPage(dbhelper.EecuteQueryReaderOne($@"select ClientNumber from client where email = '{tbLogin.Text}'", "ClientNumber")));
                }
            }
        }

        /// <summary>
        /// Обработчик кнопки перехода на страницу регистрации
        /// </summary>
        /// <param name="sender">ссылка на элемент управления</param>
        /// <param name="e">данные события</param>
        private void btnReg_Click(object sender, RoutedEventArgs e)
        {
            FrameManager.MainFrame.Navigate(new SignUpPage());
        }

        /// <summary>
        /// Обработчик кнопки перехода на страницу восстановления пароля
        /// </summary>
        /// <param name="sender">ссылка на элемент управления</param>
        /// <param name="e">данные события</param>
        private void btnRecovery_Click(object sender, RoutedEventArgs e)
        {
            FrameManager.MainFrame.Navigate(new RecoveryPage());
        }
    }
}
