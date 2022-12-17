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
    /// Логика взаимодействия для ClientOrderPage.xaml
    /// </summary>
    public partial class ClientOrderPage : Page
    {
        //Экземпляр класса работы с БД
        DBHelper dbhelper = new DBHelper();

        //Коллекция номеров машин
        List<string> carNumbersClient = new List<string>();

        //хранения запроса
        string Query = "";

        //коллекция статусов
        List<string> statuses = new List<string>();

        //коллекция номеров диагностиуи
        List<string> diagnostics = new List<string>();

        /// <summary>
        /// Инициализация компонентов страницы 
        /// </summary>
        /// <param name="clientNumber">уникальный индетификатор клиента</param>
        public ClientOrderPage(String clientNumber)
        {
            InitializeComponent();
            dbhelper.EecuteQueryReader($@"select carnumber from car where clientnumber = '{clientNumber}'", carNumbersClient, "carnumber");
            string query = "select * from orderclient where ";
            foreach(string car in carNumbersClient)
            {
                query += "carnumber = '" + car + "' or ";
            }
            query += "carnumber = ' '";
            dbhelper.Refresh(dgHistory, query, grdOrder);
            Query = query;
        }

        /// <summary>
        /// Обработчик выбора значения DataGrid заказов
        /// </summary>
        /// <param name="sender">ссылка на элемент управления</param>
        /// <param name="e">данные события</param>
        private void dgHistory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            diagnostics.Clear();
            statuses.Clear();
            lbServices.Items.Clear();
            carNumbersClient.Clear();
            labelStatus.Items.Clear();
            dbhelper.EecuteQueryReader($@"select * from serviceorder where ordernumber = '{dbhelper.GetValueData(dgHistory, "ordernumber")}'",carNumbersClient, "servicename");
            foreach(string service in carNumbersClient)
            {
                lbServices.Items.Add(service);
            }

            if (dbhelper.EecuteQueryReaderOne($@"select count(ordernumber) from contract where ordernumber = '{dbhelper.GetValueData(dgHistory, "ordernumber")}'", "count") == "0")
                labelStatus.Items.Clear();
            else
            {
                string contractnumber = dbhelper.EecuteQueryReaderOne($@"select contractnumber from contract where ordernumber = '{dbhelper.GetValueData(dgHistory, "ordernumber")}'", "contractnumber");
                if (dbhelper.EecuteQueryReaderOne($@"select count(diagnosticsnumber) from diagnostics where contractnumber = '{contractnumber}'", "count") == "0")
                    labelStatus.Items.Add("Заказ оформлен чек выслан на поту");
                else
                {
                    labelStatus.Items.Clear();
                    diagnostics = dbhelper.EecuteQueryReader($@"select diagnosticsnumber from diagnostics where contractnumber = '{contractnumber}'", diagnostics, "diagnosticsnumber");
                    foreach (string diag in diagnostics)
                    {
                        statuses.Add(dbhelper.EecuteQueryReaderOne($@"select (statustime || ': ' || statuscar) as aaa from statuscar where diagnosticsnumber = '{diag}'", "aaa"));
                    }

                    //statuses = dbhelper.EecuteQueryReader($@"select (statustime || ': ' || statuscar) as aaa from statuscar where diagnosticsnumber = '{dbhelper.EecuteQueryReaderOne($@"select diagnosticsnumber from diagnostics where contractnumber = '{contractnumber}'", "diagnosticsnumber")}'", statuses,"aaa");
                    foreach (string st in statuses)
                    {
                        labelStatus.Items.Add(st);
                    }
                }
            }
        }

        /// <summary>
        /// Обработчик кнопки отмены заказа
        /// </summary>
        /// <param name="sender">ссылка на элемент управления</param>
        /// <param name="e">данные события</param>
        private void btnCancelOrder_Click(object sender, RoutedEventArgs e)
        {
            if(dgHistory.SelectedIndex != -1)
            {
                if (dbhelper.EecuteQueryReaderOne("select count(ordernumber) from contract", "count") == "0")
                {
                    if (MessageBox.Show("Вы точно хотите удалить выбранный заказ?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        dbhelper.DeleteInto("orderclient", $@"{dbhelper.GetValueData(dgHistory, "ordernumber")}");
                        dbhelper.Refresh(dgHistory, Query, grdOrder);
                    }
                }
                else
                {
                    MessageBox.Show("Данный заказ нельзя отменить, так как уже была произведена оплата, обратитесь в отдел продаж");
                }
            }
        }
    }
}
