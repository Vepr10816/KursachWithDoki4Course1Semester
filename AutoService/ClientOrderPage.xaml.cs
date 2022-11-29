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
        DBHelper dbhelper = new DBHelper();
        List<string> carNumbersClient = new List<string>();
        string Query = "";
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

        private void dgHistory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lbServices.Items.Clear();
            carNumbersClient.Clear();
            dbhelper.EecuteQueryReader($@"select * from serviceorder where ordernumber = '{dbhelper.GetValueData(dgHistory, "ordernumber")}'",carNumbersClient, "servicename");
            foreach(string service in carNumbersClient)
            {
                lbServices.Items.Add(service);
            }

            if (dbhelper.EecuteQueryReaderOne("select count(ordernumber) from contract", "count") == "0")
                labelStatus.Content = "В обработке";
            else
            {
                labelStatus.Content = "Заказ оформлен чек выслан на поту";
            }
        }

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
