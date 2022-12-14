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
    /// Логика взаимодействия для ClientOrderCreationPage.xaml
    /// </summary>
    public partial class ClientOrderCreationPage : Page
    {
        //экземпляр класса управления БД
        DBHelper dbhelper = new DBHelper();

        //экземпляр класса валидации
        ValidationData valid = new ValidationData();

        //Коллекция выбранных услуг
        List<string> addService = new List<string>();

        /// <summary>
        /// Инициализация компонентов страницы оформления заказа
        /// </summary>
        /// <param name="clientNumber">уникальный индетификатор клиента</param>
        public ClientOrderCreationPage(String clientNumber)
        {
            InitializeComponent();
            dbhelper.Refresh(dgService, "select * from service", grdOrderCreate);
            List<string> cars = new List<string>();
            dbhelper.EecuteQueryReader($@"select (CarBrand || ' ' || CarModel) as cr from Car where ClientNumber = '{clientNumber}'", cars, "cr");
            foreach(string car in cars)
            {
                cbCars.Items.Add(car);
            }
        }

        /// <summary>
        /// Обработчик сheckbox выбранных услуг
        /// </summary>
        /// <param name="sender">ссылка на элемент управления</param>
        /// <param name="e">данные события</param>
        private void chDG_Checked(object sender, RoutedEventArgs e)
        {
            lbPriceEnding.Content = Convert.ToDouble(lbPriceEnding.Content.ToString()) + Convert.ToDouble(dbhelper.GetValueData(dgService, "price"));
            addService.Add(dbhelper.GetValueData(dgService, "servicename"));
        }

        /// <summary>
        /// ООбработчик вheckbox отмененных услуг
        /// </summary>
        /// <param name="sender">ссылка на элемент управления</param>
        /// <param name="e">данные события</param>
        private void chDG_Unchecked(object sender, RoutedEventArgs e)
        {
            lbPriceEnding.Content = Convert.ToDouble(lbPriceEnding.Content.ToString()) - Convert.ToDouble(dbhelper.GetValueData(dgService, "price"));
            addService.Remove(dbhelper.GetValueData(dgService, "servicename"));
        }

        /// <summary>
        /// Обработчик выбора значения в DataGrid
        /// </summary>
        /// <param name="sender">ссылка на элемент управления</param>
        /// <param name="e">данные события</param>
        private void dgService_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lbDescription.Document.Blocks.Clear();
            lbDescription.AppendText(dbhelper.EecuteQueryReaderOne($@"select ServiceDescription from Service where ServiceName = '{dbhelper.GetValueData(dgService, "servicename")}'","ServiceDescription"));
        }

        /// <summary>
        /// ООбработчик кнопки оформления заказа
        /// </summary>
        /// <param name="sender">ссылка на элемент управления</param>
        /// <param name="e">данные события</param>
        private void btnAddOrder_Click(object sender, RoutedEventArgs e)
        {
            if (lbPriceEnding.Content.ToString() == "0")
                valid.Show("Выберете услугу").GetAwaiter();
            if(dpOrder.SelectedDate.ToString()!="" && cbCars.SelectedIndex != -1)
            {
                string ContractNumber = "";
                do
                {
                    Random rnd = new Random();
                    ContractNumber = rnd.Next(111111111, 999999999).ToString();
                }
                while (dbhelper.EecuteQueryReaderOne($@"SELECT count(ordernumber) FROM OrderClient where ordernumber = '{ContractNumber}'", "count") == "1");
                string[] atributes = {ContractNumber,  lbPriceEnding.Content.ToString(),  dpOrder.SelectedDate.Value.Date.ToString("yyyy.MM.dd"),
                                      dbhelper.EecuteQueryReaderOne($@"select carnumber from car where (CarBrand || ' ' || CarModel) = '{cbCars.SelectedItem.ToString()}'","carnumber") };
                dbhelper.InsertInto("orderclient", atributes);

                foreach(string service in addService)
                {
                    string[] atributes2 = {service, ContractNumber};
                    dbhelper.InsertInto("ServiceOrder", atributes2);
                }
                valid.Show("Заказ оформлен").GetAwaiter();
                FrameManager.MainFrame.GoBack();
            }
        }
    }
}
