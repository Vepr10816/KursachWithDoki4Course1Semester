using System;
using System.Collections.Generic;
using System.Data;
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
    /// Логика взаимодействия для ClientPage.xaml
    /// </summary>
    public partial class ClientPage : Page
    {

        //экземпляр класса управления БД
        DBHelper dbhelper = new DBHelper();

        //экземпляр класса валидации
        ValidationData valid = new ValidationData();

        //Уникалтьный индетификатор клиента
        string Client = "";

        /// <summary>
        /// Обработчик выбора значения в DataGrid автомобилей
        /// </summary>
        /// <param name="sender">ссылка на элемент управления</param>
        /// <param name="e">данные события</param>
        private void Data_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgCars.SelectedItem == null)
            {
                return;
            }
            DataRowView row = (DataRowView)dgCars.SelectedItem;
            tbNumber.Text = row[0].ToString();
            tbBrand.Text = row[1].ToString();
            tbModel.Text = row[2].ToString();
            tbSTS.Text = row[3].ToString();
            tbVIN.Text = row[4].ToString();

        }
        
        /// <summary>
        /// Инциализация копонентов страницы клиента
        /// </summary>
        /// <param name="clientNumber">уникальный индетификатор клиента</param>
        public ClientPage(string clientNumber)
        {
            InitializeComponent();
            dbhelper.Refresh(dgCars, $@"select * from Car where clientnumber = '{clientNumber}'", grdClientCar);
            Client = clientNumber;
        }

        /// <summary>
        /// Обработчик кнопки очищения текстовых полей
        /// </summary>
        /// <param name="sender">ссылка на элемент управления</param>
        /// <param name="e">данные события</param>
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            dbhelper.Refresh(dgCars, $@"select * from Car where clientnumber = '{Client}'", grdClientCar);
        }

        /// <summary>
        /// Обработчик кнопки добавления машины
        /// </summary>
        /// <param name="sender">ссылка на элемент управления</param>
        /// <param name="e">данные события</param>
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (tbNumber.Text.Length == 6 && tbSTS.Text.Length == 10 && tbVIN.Text.Length == 17 && tbBrand.Text != "" && tbModel.Text != "")
            {
                string[] atributes = { tbNumber.Text, tbBrand.Text, tbModel.Text, tbSTS.Text, tbVIN.Text, Client};
                dbhelper.InsertInto("Car", atributes);
                dbhelper.Refresh(dgCars, $@"select * from Car where clientnumber = '{Client}'", grdClientCar);
            }
            else
            {
                valid.Show("Все поля должны быть заполнены: длина номера - 6, длина стс - 10, длина VIN - 17").GetAwaiter();
            }
        }

        /// <summary>
        /// Обработчик обновления данных по машине
        /// </summary>
        /// <param name="sender">ссылка на элемент управления</param>
        /// <param name="e">данные события</param>
        private void btnUpd_Click(object sender, RoutedEventArgs e)
        {
            if (dgCars.SelectedIndex == -1)
                return;
            if (tbNumber.Text.Length == 6 && tbSTS.Text.Length == 10 && tbVIN.Text.Length == 17 && tbBrand.Text != "" && tbModel.Text != "")
            {
                string[] atributes = { dbhelper.GetValueData(dgCars, "carnumber"), tbBrand.Text, tbModel.Text, tbSTS.Text, tbVIN.Text, Client, tbNumber.Text };
                dbhelper.UpdateInto("Car", atributes);
                dbhelper.Refresh(dgCars, $@"select * from Car where clientnumber = '{Client}'", grdClientCar);
            }
            else
            {
                valid.Show("Все поля должны быть заполнены: длина номера - 6, длина стс - 10, длина VIN - 17").GetAwaiter();
            }
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            if (dgCars.SelectedIndex == -1)
                return;
            if (MessageBox.Show("Вы точно хотите удалить данную машину?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                dbhelper.DeleteInto("Car", $@"{dbhelper.GetValueData(dgCars, "carnumber")}");
                dbhelper.Refresh(dgCars, $@"select * from Car where clientnumber = '{Client}'", grdClientCar);
            }
        }

        /// <summary>
        /// Обработчик перехода на страницу оформления заказа
        /// </summary>
        /// <param name="sender">ссылка на элемент управления</param>
        /// <param name="e">данные события</param>
        private void btnGoOrder_Click(object sender, RoutedEventArgs e)
        {
            FrameManager.MainFrame.Navigate(new ClientOrderPage(Client));
        }

        /// <summary>
        /// Обработчик кнопки перехода на страницу истории заказов
        /// </summary>
        /// <param name="sender">ссылка на элемент управления</param>
        /// <param name="e">данные события</param>
        private void btnGetOrder_Click(object sender, RoutedEventArgs e)
        {
            FrameManager.MainFrame.Navigate(new ClientOrderCreationPage(Client));
        }
    }
}
