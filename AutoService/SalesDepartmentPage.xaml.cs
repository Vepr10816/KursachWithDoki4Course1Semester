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
    /// Логика взаимодействия для SalesDepartmentPage.xaml
    /// </summary>
    public partial class SalesDepartmentPage : Page
    {
        DBHelper dbhelper = new DBHelper();
        string employeeMail = "";
        EmailHelper emailHelper = new EmailHelper();
        List<string> orders = new List<string>();
        string Query = "";
        public SalesDepartmentPage(string employeeEmail)
        {
            InitializeComponent();
            employeeMail = employeeEmail;
            Refresh();
        }

        private void Refresh()
        {
            orders.Clear();
            orders = dbhelper.EecuteQueryReader("select * from contract", orders, "ordernumber");
            Query = "select * from orderclient where ";
            foreach(string order in orders)
            {
                Query += $@" ordernumber != '{order}' and ";
            }
            Query += "ordernumber != '{order}'";
            dbhelper.Refresh(dgOrders, Query, grdSalesDepartament);
            dbhelper.Refresh(dgContracts, "select * from Contract", grdSalesDepartament);
            dbhelper.Refresh(dgServices, "select * from service", grdSalesDepartament);
        }

        private void btnAddOrder_Click(object sender, RoutedEventArgs e)
        {
            string contractNumber = "";
            string clientEmail = dbhelper.EecuteQueryReaderOne($@"select * from car where carnumber = '{dbhelper.GetValueData(dgOrders, "carnumber")}'", "clientnumber");
            do
            {
                Random rnd = new Random();
                contractNumber = rnd.Next(111111111, 999999999).ToString();
            }
            while (dbhelper.EecuteQueryReaderOne($@"SELECT count(ContractNumber) FROM Contract where ContractNumber = '{contractNumber}'", "count") == "1");
            string[] atributes = {contractNumber, DateTime.Now.ToString("yyyy.MM.dd"), dbhelper.GetValueData(dgOrders, "ordernumber"),
                                    clientEmail,  
                                    employeeMail};
            dbhelper.InsertInto("Contract", atributes);
            emailHelper.SendMailOrder(dbhelper.EecuteQueryReaderOne($@"select email from client where clientnumber = '{clientEmail}'", "email"), $@"Успешное оформление заказа № {dbhelper.GetValueData(dgOrders, "ordernumber")}", "<h1>Ваш заказ оформлен, ожидаем вас в нашем автосервисе<h1>").GetAwaiter();
            Refresh();
        }

        private void btnDelOrder_Click(object sender, RoutedEventArgs e)
        {
            string clientEmail = dbhelper.EecuteQueryReaderOne($@"select * from car where carnumber = '{dbhelper.GetValueData(dgOrders, "carnumber")}'", "clientnumber");
            ModalWindowAddClient appointment = new ModalWindowAddClient(dbhelper.EecuteQueryReaderOne($@"select email from client where clientnumber = '{clientEmail}'", "email"), 1, dbhelper.GetValueData(dgOrders, "ordernumber"));
            appointment.ShowDialog();
            Refresh();
            //dbhelper.DeleteInto("orderclient", $@"{dbhelper.EecuteQueryReaderOne($@"select * from contract where contractnumber = '{dbhelper.GetValueData(dgOrders, "contractnumber")}'", "ordernumber")}");
            //dbhelper.EecuteQueryReaderOne($@"select * from contract where contractnumber = '{dbhelper.GetValueData(dgOrders, "contractnumber")}'", "ordernumber")
        }

        private void btnDelContract_Click(object sender, RoutedEventArgs e)
        {
            ModalWindowAddClient appointment = new ModalWindowAddClient(dbhelper.EecuteQueryReaderOne($@"select email from client where clientnumber = '{dbhelper.GetValueData(dgContracts, "clientnumber")}'", "email"), 1, dbhelper.EecuteQueryReaderOne($@"select * from contract where contractnumber = '{dbhelper.GetValueData(dgContracts, "contractnumber")}'", "ordernumber"));
            appointment.ShowDialog();
            Refresh();
        }

        private void btnDelService_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы точно хотите удалить данную услугу?!?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                dbhelper.DeleteInto("Service", $@"{dbhelper.GetValueData(dgServices, "servicename")}");
                Refresh();
            }
        }

        private void dgServices_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            string[] atributes = { dbhelper.GetValueData(dgServices, "servicename"), dbhelper.GetValueData(dgServices, "price"), dbhelper.GetValueData(dgServices, "servicedescription"), dbhelper.GetValueData(dgServices, "servicename") };
            if (dbhelper.EecuteQueryReaderOne($@"select count(servicename) from service where servicename = '{dbhelper.GetValueData(dgServices, "servicename")}'", "count") == "1")
            {
                dbhelper.UpdateInto("Service", atributes);
            }
            else
            {
                dbhelper.InsertInto("Service", atributes.Take(atributes.Length - 1).ToArray());
            }
           Refresh();
        }
    }
}
