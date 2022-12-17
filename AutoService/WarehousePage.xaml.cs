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
    /// Логика взаимодействия для WarehousePage.xaml
    /// </summary>
    public partial class WarehousePage : Page
    {
        //экземпляр класса управления БД
        DBHelper dbhelper = new DBHelper();

        /// <summary>
        /// Инциализация компонентов страницы Складского отдела
        /// </summary>
        public WarehousePage()
        {
            InitializeComponent();
            dbhelper.Refresh(dgWarehouse, "select * from warehouse; ", grdWarehousePage);
            DataGridsRefresh();
        }

        /// <summary>
        /// Обработчик обновления данных DataGrid скаладов
        /// </summary>
        /// <param name="sender">ссылка на элемент управления</param>
        /// <param name="e">данные события</param>
        private void dgWarehouse_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            string[] atributes = { dbhelper.GetValueData(dgWarehouse, "warehousename"), dbhelper.GetValueData(dgWarehouse, "warehouseaddress"),dbhelper.GetValueData(dgWarehouse, "warehousename") };
            if (dbhelper.EecuteQueryReaderOne($@"select count(warehousename) from warehouse where warehousename = '{dbhelper.GetValueData(dgWarehouse, "warehousename")}'", "count") == "1")
            {
                dbhelper.UpdateInto("Warehouse", atributes);
            }
            else
            {
                dbhelper.InsertInto("Warehouse", atributes.Take(atributes.Length - 1).ToArray());
            }
            DataGridsRefresh();
        }

        /// <summary>
        /// Обработчик кнопки удаления склада
        /// </summary>
        /// <param name="sender">ссылка на элемент управления</param>
        /// <param name="e">данные события</param>
        private void btnDelDisposable_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы точно хотите удалить данный Склад?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                dbhelper.DeleteInto("Warehouse", $@"{dbhelper.GetValueData(dgWarehouse, "warehousename")}");
                DataGridsRefresh();
            }
        }

        /// <summary>
        /// Обработчик обновления данных DataGrid ячеек
        /// </summary>
        /// <param name="sender">ссылка на элемент управления</param>
        /// <param name="e">данные события</param>
        private void dgCells_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            string[] atributes = { dbhelper.GetValueData(dgCells, "cellnumber"), dbhelper.GetTextComboBox(dgCells, 0), dbhelper.GetValueData(dgCells, "cellnumber") };
            if (dbhelper.EecuteQueryReaderOne($@"select count(cellnumber) from cell where cellnumber = '{dbhelper.GetValueData(dgCells, "cellnumber")}'", "count") == "1")
            {
                dbhelper.UpdateInto("Cell", atributes);
            }
            else
            {
                dbhelper.InsertInto("Cell", atributes.Take(atributes.Length - 1).ToArray());
            }
            DataGridsRefresh();
        }

        /// <summary>
        /// Обработчик кнопки удаления ячейки склада
        /// </summary>
        /// <param name="sender">ссылка на элемент управления</param>
        /// <param name="e">данные события</param>
        private void btnDelCell_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы точно хотите удалить данную ячейку?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                dbhelper.DeleteInto("Cell", $@"{dbhelper.GetValueData(dgCells, "cellnumber")}");
                DataGridsRefresh();
            }
        }

        /// <summary>
        /// Обновление всех DataGrid-ов страницы
        /// </summary>
        private void DataGridsRefresh()
        {
            dbhelper.Refresh(dgWarehouse, "select * from warehouse; ", grdWarehousePage);
            dbhelper.Refresh(dgCells, "select * from Cell; ", grdWarehousePage);
            dbhelper.Refresh(dgInvoice, "select * from Invoice; ", grdWarehousePage);
            dbhelper.BindComboBox(cbWarehouseName, "select * from warehouse", "warehousename", "warehousename");
            dbhelper.BindComboBox(cbseriesnumber, "select * from Disposable", "seriesnumber", "seriesnumber");
            dbhelper.BindComboBox(cbcellnumber, "select (Cell.warehousename || ' - '|| Cell.cellnumber) as cl, cellnumber from cell", "cl", "cellnumber");
            dbhelper.BindComboBox(cbcellemail, "select lastname ||' '|| firstname||' '|| middlename as colum, email from Users where RoleName = 'Сотрудник склада'", "colum", "email");
        }

        /// <summary>
        /// Обработчик обновления данных DataGrid наклодной
        /// </summary>
        /// <param name="sender">ссылка на элемент управления</param>
        /// <param name="e">данные события</param>
        private void dgInvoice_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            string[] atributes = { dbhelper.GetValueData(dgInvoice, "invoicenmber"), 
                dbhelper.GetValueData(dgInvoice, "invoicedate"), 
                dbhelper.GetValueData(dgInvoice,"quantity"), 
                dbhelper.GetTextComboBox(dgInvoice, 0),
                dbhelper.EecuteQueryReaderOne($@"select * from Cell where (Cell.warehousename || ' - '|| Cell.cellnumber) = '{dbhelper.GetTextComboBox(dgInvoice, 1)}'", "cellnumber"),
                dbhelper.EecuteQueryReaderOne($@"select * from Users where (lastname ||' '|| firstname||' '|| middlename) = '{dbhelper.GetTextComboBox(dgInvoice, 2)}'", "email"),
                dbhelper.GetValueData(dgInvoice, "invoicenmber") };
            if (dbhelper.EecuteQueryReaderOne($@"select count(invoicenmber) from invoice where invoicenmber = '{dbhelper.GetValueData(dgInvoice, "invoicenmber")}'", "count") == "1")
            {
                dbhelper.UpdateInto("Invoice", atributes);
            }
            else
            {
                dbhelper.InsertInto("Invoice", atributes.Take(atributes.Length - 1).ToArray());
            }
            DataGridsRefresh();
        }

        /// <summary>
        /// Обработчик кнопки удаления накладной
        /// </summary>
        /// <param name="sender">ссылка на элемент управления</param>
        /// <param name="e">данные события</param>
        private void btnDelInvoice_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы точно хотите удалить данную накладную?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                dbhelper.DeleteInto("Invoice", $@"{dbhelper.GetValueData(dgInvoice, "invoicenmber")}");
                DataGridsRefresh();
            }
        }
    }
}
