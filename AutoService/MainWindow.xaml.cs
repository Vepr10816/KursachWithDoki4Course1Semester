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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Инициализация компонентов главного окна
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            FrameManager.MainFrame = MainFrame;
            MainFrame.Navigate(new SignInPage());//ClientPage("4567865467"));//SalesDepartmentPage("diagram14@mail.ru"));//ClientPage("4567865467"));//new PurchasePage());//SignInPage());//new SignInPage());new DiagramAdmin() SalesDepartmentPage("diagram14@mail.ru"));
        }

        /// <summary>
        /// Обработчик кнопки перехода на прошлую страницу
        /// </summary>
        /// <param name="sender">ссылка на элемент управления</param>
        /// <param name="e">данные события</param>
        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            FrameManager.MainFrame.GoBack();
        }

        /// <summary>
        /// Отрисовка кнопок перехода в зависимости от страницы
        /// </summary>
        /// <param name="sender">ссылка на элемент управления</param>
        /// <param name="e">данные события</param>
        private void MainFrame_ContentRendered(object sender, EventArgs e)
        {
            if (((Page)MainFrame.Content).Title.ToString() == "DiagramAdmin")
            {
                PageName.Text = "Статистика пользователей";
                btnDiagram.Visibility = Visibility.Hidden;
                PageName.Visibility = Visibility.Visible;
                btnGoBack.Content = "Назад";
            }
            else if(((Page)MainFrame.Content).Title.ToString() == "AdminPage")
            {
                btnDiagram.Visibility = Visibility.Visible;
                btnDControl.Visibility = Visibility.Visible;
                PageName.Text = "Сервис-ПРО";
                btnGoBack.Content = "Выйти";
            }
            else if (((Page)MainFrame.Content).Title.ToString() == "AdminDataBaseControlPage")
            {
                PageName.Text = "Управление базой данных";
                btnDControl.Visibility = Visibility.Hidden;
                PageName.Visibility = Visibility.Visible;
                btnGoBack.Content = "Назад";
            }
            else if (((Page)MainFrame.Content).Title.ToString() == "ClientPage")
            {
                PageName.Text = "Мои машины";
                btnDControl.Visibility = Visibility.Hidden;
                PageName.Visibility = Visibility.Visible;
                btnGoBack.Content = "выйти";
            }
            else
            {
                PageName.Text = "Сервис-ПРО";
                btnDiagram.Visibility = Visibility.Hidden;
                btnDControl.Visibility = Visibility.Hidden;
                PageName.Visibility = Visibility.Visible;
                btnGoBack.Content = "Выйти";
            }
            if (MainFrame.CanGoBack)
            {
                btnGoBack.Visibility = Visibility.Visible;
            }
            else
            {
                btnGoBack.Visibility = Visibility.Hidden;
            }
        }

        /// <summary>
        /// Переход на страницу диаграмм
        /// </summary>
        /// <param name="sender">ссылка на элемент управления</param>
        /// <param name="e">данные события</param>
        private void btnDiagram_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new DiagramAdmin());
        }

        /// <summary>
        /// Переход на страницу управления БД
        /// <param name="sender">ссылка на элемент управления</param>
        /// <param name="e">данные события</param>
        private void btnDControl_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new AdminDataBaseControlPage());
        }

    }
}
