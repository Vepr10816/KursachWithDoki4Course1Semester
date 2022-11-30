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
    /// Логика взаимодействия для RepairPage.xaml
    /// </summary>
    public partial class RepairPage : Page
    {
        DBHelper dbhelper = new DBHelper();
        ValidationData validation = new ValidationData();
        string email = "";
        public RepairPage(string employeeEmail)
        {
            InitializeComponent();
            dbhelper.Refresh(dgContracts, "select * from orderclient, contract where orderclient.ordernumber = contract.ordernumber", grdRepair);
            email = employeeEmail;
        }

        private void dgContracts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dbhelper.Refresh(dgDiagnostics, $@"select * from diagnostics where contractnumber = '{dbhelper.GetValueData(dgContracts, "contractnumber")}'", grdRepair);
        }

        private void dgDiagnostics_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            string[] atributes = { dbhelper.GetValueData(dgDiagnostics, "diagnosticsnumber"),
                dbhelper.GetValueData(dgDiagnostics, "diagnosticsdate"),
                dbhelper.GetValueData(dgDiagnostics, "diagnosticsresults"),
                dbhelper.GetValueData(dgDiagnostics, "diagnosticsdescription"),
                dbhelper.GetValueData(dgContracts, "carnumber"),
                dbhelper.GetValueData(dgContracts, "contractnumber"),
                email,
                dbhelper.GetValueData(dgDiagnostics, "diagnosticsnumber"),
            };
            if (dbhelper.EecuteQueryReaderOne($@"select count(diagnosticsnumber) from diagnostics where diagnosticsnumber = '{dbhelper.GetValueData(dgDiagnostics, "diagnosticsnumber")}'", "count") == "1")
            {
                dbhelper.UpdateInto("diagnostics", atributes);
            }
            else
            {
                dbhelper.InsertInto("diagnostics", atributes.Take(atributes.Length - 1).ToArray());
            }
            dbhelper.Refresh(dgDiagnostics, $@"select * from diagnostics where contractnumber = '{dbhelper.GetValueData(dgContracts, "contractnumber")}'", grdRepair);
            //dbhelper.Refresh(dgDiagnostics, $@"select from diagnostics where carnumber = '{dbhelper.GetValueData(dgContracts, "carnumber")}'", grdRepair);
        }

        private void dgDiagnostics_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tbStatusName.Text = dbhelper.EecuteQueryReaderOne($@"select statuscar from statuscar where diagnosticsnumber = '{dbhelper.GetValueData(dgDiagnostics, "diagnosticsnumber")}'", "statuscar");
            lbTimeStatus.Content = dbhelper.EecuteQueryReaderOne($@"select statustime from statuscar where diagnosticsnumber = '{dbhelper.GetValueData(dgDiagnostics, "diagnosticsnumber")}'", "statustime");
            if (lbTimeStatus.Content.ToString() == "" || lbTimeStatus.Content.ToString() == "00:00")
                lbTimeStatus.Content = DateTime.Now.ToString();
        }

        private void btnAddStatus_Click(object sender, RoutedEventArgs e)
        {
            if (dgDiagnostics.SelectedIndex != -1 && tbStatusName.Text.Replace(" ","")!="")
            {


                if (dbhelper.EecuteQueryReaderOne($@"SELECT count(diagnosticsnumber) FROM statuscar where diagnosticsnumber = '{dbhelper.GetValueData(dgDiagnostics, "diagnosticsnumber")}'", "count") == "1")
                {
                    string[] atributes = { dbhelper.EecuteQueryReaderOne($@"select id_statuscar from statuscar where diagnosticsnumber = '{dbhelper.GetValueData(dgDiagnostics, "diagnosticsnumber")}'", "id_statuscar"), tbStatusName.Text, DateTime.Now.ToString(), dbhelper.GetValueData(dgDiagnostics, "diagnosticsnumber") };
                    dbhelper.UpdateInto("statuscar", atributes);
                }
                else
                {
                    string[] atributes = { tbStatusName.Text, DateTime.Now.ToString(), dbhelper.GetValueData(dgDiagnostics, "diagnosticsnumber") };
                    dbhelper.InsertInto("statuscar", atributes);
                }
                lbTimeStatus.Content = DateTime.Now.ToString();
                validation.Show("Успешное обновление статуса").GetAwaiter();
            }
        }

        private void btnDelOrder_Click(object sender, RoutedEventArgs e)
        {
            if (dgDiagnostics.SelectedIndex != -1)
            {
                if (MessageBox.Show("Вы точно хотите закрыть ремонт?!?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    dbhelper.DeleteInto("orderclient", dbhelper.GetValueData(dgContracts, "ordernumber"));
                }
            }
            dbhelper.Refresh(dgContracts, "select * from orderclient, contract where orderclient.ordernumber = contract.ordernumber", grdRepair);
            dbhelper.Refresh(dgDiagnostics, $@"select * from diagnostics where contractnumber = ' '", grdRepair);
            tbStatusName.Text = "";
            lbTimeStatus.Content = DateTime.Now.ToString();
        }
    }
}
