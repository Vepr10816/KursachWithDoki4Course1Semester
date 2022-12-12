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
    /// Логика взаимодействия для PurchasePage.xaml
    /// </summary>
    public partial class PurchasePage : Page
    {
        DBHelper dbhelper = new DBHelper();
        public PurchasePage()
        {
            InitializeComponent();
            DataGridsRefresh();
        }

        private void btnDelPurchase_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы точно хотите удалить данную Поставку?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                dbhelper.DeleteInto("PurchaseOrder", $@"{dbhelper.GetValueData(dgPurchaseOrder, "purchaseordernumber")}");
                DataGridsRefresh();
            }
        }
        private void dgPurchaseOrder_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            if(e.EditAction == DataGridEditAction.Commit)
            {
                string[] atributes = { dbhelper.GetValueData(dgPurchaseOrder, "purchaseordernumber"), dbhelper.GetValueData(dgPurchaseOrder, "purchaseorderdate"), dbhelper.GetValueData(dgPurchaseOrder, "quantity"), dbhelper.GetValueData(dgPurchaseOrder, "rotalprice").Replace(",", "."), dbhelper.GetValueData(dgPurchaseOrder, "article"), dbhelper.GetValueData(dgPurchaseOrder, "purchaseordernumber") };
                if (dbhelper.EecuteQueryReaderOne($@"select count(purchaseordernumber) from purchaseorder where purchaseordernumber = '{dbhelper.GetValueData(dgPurchaseOrder, "purchaseordernumber")}'","count")=="1")
                {
                    dbhelper.UpdateInto("PurchaseOrder", atributes);
                }
                else
                {
                    dbhelper.InsertInto("PurchaseOrder", atributes.Take(atributes.Length - 1).ToArray());
                }
                DataGridsRefresh();
            }
        }

        private void dgSuplier_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                string[] atributes = { dbhelper.GetValueData(dgSuplier, "inn"), dbhelper.GetValueData(dgSuplier, "supliername"), dbhelper.GetValueData(dgSuplier, "paymentaccount"), dbhelper.GetValueData(dgSuplier, "inn") };
                if (dbhelper.EecuteQueryReaderOne($@"select count(inn) from suplier where inn = '{dbhelper.GetValueData(dgSuplier, "inn")}'", "count") == "1")
                {
                    dbhelper.UpdateInto("Suplier", atributes);
                }
                else
                {
                    dbhelper.InsertInto("Suplier", atributes.Take(atributes.Length - 1).ToArray());
                }
                DataGridsRefresh();
            }
        }

        private void btnDelSuplier_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы точно хотите удалить данную Поставку?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                dbhelper.DeleteInto("Suplier", $@"{dbhelper.GetValueData(dgSuplier, "inn")}");
                DataGridsRefresh();
            }
        }

        private void tb1_PreviewTextInputNum(object sender, TextCompositionEventArgs e) => new ValidationData().tb1_PreviewTextInputNum(sender, e);
        private void tb1_PastingNum(object sender, DataObjectPastingEventArgs e) => new ValidationData().tb1_PastingNum(sender, e);

        private void dgPurchaseContract_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                string[] atributes = { dbhelper.GetValueData(dgPurchaseContract, "purchasecontractnumber"), dbhelper.GetValueData(dgPurchaseContract, "purchasecontractdate"),
                    dbhelper.GetTextComboBox(dgPurchaseContract,1),
                    dbhelper.EecuteQueryReaderOne($@"select * from Suplier where supliername = '{dbhelper.GetTextComboBox(dgPurchaseContract, 2)}'", "inn"),
                    dbhelper.EecuteQueryReaderOne($@"select * from Users where (lastname ||' '|| firstname||' '|| middlename) = '{dbhelper.GetTextComboBox(dgPurchaseContract, 3)}'", "email"),
                    dbhelper.GetValueData(dgPurchaseContract, "purchasecontractnumber") };
                if (dbhelper.EecuteQueryReaderOne($@"select count(purchasecontractnumber) from purchasecontract where purchasecontractnumber = '{dbhelper.GetValueData(dgPurchaseOrder, "purchasecontractnumber")}'", "count") == "1")
                {
                    dbhelper.UpdateInto("purchasecontract", atributes);
                }
                else
                {
                    dbhelper.InsertInto("purchasecontract", atributes.Take(atributes.Length - 1).ToArray());
                }
                DataGridsRefresh();
            }
        }

        private void btnDelPurchaseContract_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы точно хотите удалить данный контракт на Поставку?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                dbhelper.DeleteInto("purchasecontract", $@"{dbhelper.GetValueData(dgPurchaseContract, "purchasecontractnumber")}");
                DataGridsRefresh();
            }
        }

        private void dgDisposable_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                string[] atributes = { 
                    dbhelper.GetValueData(dgDisposable, "seriesnumber"),
                    dbhelper.GetValueData(dgDisposable, "disposablename"),
                    dbhelper.GetValueData(dgDisposable, "disposableweight"),
                    dbhelper.GetValueData(dgDisposable, "disposablecolor"),
                    dbhelper.GetValueData(dgDisposable, "disposablematerial"),
                    dbhelper.GetValueData(dgDisposable, "disposablesertificate"),
                    dbhelper.GetTextComboBox(dgDisposable,1),
                    dbhelper.GetValueData(dgDisposable, "seriesnumber"),
                };
                if (dbhelper.EecuteQueryReaderOne($@"select count(seriesnumber) from Disposable where seriesnumber = '{dbhelper.GetValueData(dgDisposable, "seriesnumber")}'", "count") == "1")
                {
                    dbhelper.UpdateInto("Disposable", atributes);
                }
                else
                {
                    dbhelper.InsertInto("Disposable", atributes.Take(atributes.Length - 1).ToArray());
                }
                DataGridsRefresh();
            }
        }

        private void btnDelDisposable_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы точно хотите удалить данный Расходник?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                dbhelper.DeleteInto("Disposable", $@"{dbhelper.GetValueData(dgDisposable, "seriesnumber")}");
                DataGridsRefresh();
            }
        }

        private void DataGridsRefresh()
        {
            dbhelper.Refresh(dgDisposable, "select * from Disposable", grdPurchase);
            dbhelper.Refresh(dgPurchaseContract, "select * from PurchaseContract", grdPurchase);
            dbhelper.Refresh(dgSuplier, "select * from suplier; ", grdPurchase);
            dbhelper.Refresh(dgPurchaseOrder, "select * from purchaseorder; ", grdPurchase);
            dbhelper.BindComboBox(cbPurchaseOrder, "select * from purchaseorder", "purchaseordernumber", "purchaseordernumber");
            dbhelper.BindComboBox(cbSuplier, "select * from Suplier", "supliername", "inn");
            dbhelper.BindComboBox(cbEmployee, "select lastname ||' '|| firstname||' '|| middlename as colum, email from Users where RoleName = 'Сотрудник отдела закупок'", "colum", "email");
            dbhelper.BindComboBox(cbPurchaseContractNumber, "select * from PurchaseContract", "purchasecontractnumber", "purchasecontractnumber");
        }
    }
}
