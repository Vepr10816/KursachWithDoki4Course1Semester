using OfficeOpenXml;
using OfficeOpenXml.Style;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.IO;
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
        //экземпляр класса кправления БД
        DBHelper dbhelper = new DBHelper();

        //Электронная почта пользователя
        string employeeMail = "";

        //Экземпляр класса управления почтовым клиентом
        EmailHelper emailHelper = new EmailHelper();

        //Коллекция заказов
        List<string> orders = new List<string>();

        //Запрос
        string Query = "";

        /// <summary>
        /// Инициализация компонентов страницы Отдела продаж
        /// </summary>
        /// <param name="employeeEmail">Уникальный индетификатор сотрудника</param>
        public SalesDepartmentPage(string employeeEmail)
        {
            InitializeComponent();
            employeeMail = employeeEmail;
            Refresh();
        }

        /// <summary>
        /// Обновление DataGrid-ов страницы 
        /// </summary>
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

        /// <summary>
        /// Обработчик добавления заказа
        /// </summary>
        /// <param name="sender">ссылка на элемент управления</param>
        /// <param name="e">данные события</param>
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

            string path = (System.IO.Directory.GetCurrentDirectory() + @"\BackupFiles").Replace(@"\bin", "").Replace(@"\Debug", "") + $@"\{DateTime.Now.ToString().Replace(".", "").Replace(":", "").Replace(" ", "")}.xlsx";

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage(new FileInfo("MyWorkbook.xlsx")))
            {
                var reportExcel = Generate();
                File.WriteAllBytes(path, reportExcel);
            }
            Workbook workbook = new Workbook();
            workbook.LoadFromFile(path);

            PrintDialog dialog = new PrintDialog();
            dialog.UserPageRangeEnabled = true;
            PageRange rang = new PageRange(1, 3);
            dialog.PageRange = rang;
            PageRangeSelection seletion = PageRangeSelection.UserPages;
            dialog.PageRangeSelection = seletion;

            PrintDocument pd = workbook.PrintDocument;
            if (dialog.ShowDialog() == true)
            {
                pd.Print();
            }
            Refresh();
        }

        /// <summary>
        /// Обработчик кнопки удаления заказа
        /// </summary>
        /// <param name="sender">ссылка на элемент управления</param>
        /// <param name="e">данные события</param>
        private void btnDelOrder_Click(object sender, RoutedEventArgs e)
        {
            string clientEmail = dbhelper.EecuteQueryReaderOne($@"select * from car where carnumber = '{dbhelper.GetValueData(dgOrders, "carnumber")}'", "clientnumber");
            ModalWindowAddClient appointment = new ModalWindowAddClient(dbhelper.EecuteQueryReaderOne($@"select email from client where clientnumber = '{clientEmail}'", "email"), 1, dbhelper.GetValueData(dgOrders, "ordernumber"));
            appointment.ShowDialog();
            Refresh();
            //dbhelper.DeleteInto("orderclient", $@"{dbhelper.EecuteQueryReaderOne($@"select * from contract where contractnumber = '{dbhelper.GetValueData(dgOrders, "contractnumber")}'", "ordernumber")}");
            //dbhelper.EecuteQueryReaderOne($@"select * from contract where contractnumber = '{dbhelper.GetValueData(dgOrders, "contractnumber")}'", "ordernumber")
        }

        /// <summary>
        /// Обработчик кнопки удаления контракта
        /// </summary>
        /// <param name="sender">ссылка на элемент управления</param>
        /// <param name="e">данные события</param>
        private void btnDelContract_Click(object sender, RoutedEventArgs e)
        {
            ModalWindowAddClient appointment = new ModalWindowAddClient(dbhelper.EecuteQueryReaderOne($@"select email from client where clientnumber = '{dbhelper.GetValueData(dgContracts, "clientnumber")}'", "email"), 1, dbhelper.EecuteQueryReaderOne($@"select * from contract where contractnumber = '{dbhelper.GetValueData(dgContracts, "contractnumber")}'", "ordernumber"));
            appointment.ShowDialog();
            Refresh();
        }

        /// <summary>
        /// Обработчик кнопки удаления услуги
        /// </summary>
        /// <param name="sender">ссылка на элемент управления</param>
        /// <param name="e">данные события</param>
        private void btnDelService_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы точно хотите удалить данную услугу?!?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                dbhelper.DeleteInto("Service", $@"{dbhelper.GetValueData(dgServices, "servicename")}");
                Refresh();
            }
        }

        /// <summary>
        /// Обработчик изменения значений в DataGrid услуг
        /// </summary>
        /// <param name="sender">ссылка на элемент управления</param>
        /// <param name="e">данные события</param>
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


        private byte[] Generate()
        {
            string ordernumber = dbhelper.GetValueData(dgOrders, "ordernumber");

            string query = "select users.lastname || ' ' || users.firstname || ' ' || users.middlename as FIO, ordersum  from orderclient " +
                            "inner join car on orderclient.carnumber = car.carnumber " +
                            "inner join client on car.clientnumber = client.clientnumber " +
                            "inner join users on client.email = users.email " +
                            $@"where ordernumber = '{ordernumber}'";
            string FIO = dbhelper.EecuteQueryReaderOne(query, "fio");
            string ordersum = dbhelper.EecuteQueryReaderOne(query, "ordersum");

            query = "select service.servicename, service.price from serviceorder " +
                    "inner join service on serviceorder.servicename = service.servicename " +
                    $@"where ordernumber = '{ordernumber}'";
            List<string> services = new List<string>();
            List<string> price = new List<string>();

            services = dbhelper.EecuteQueryReader(query, services, "servicename");
            price = dbhelper.EecuteQueryReader(query, price, "price"); ;

            var package = new ExcelPackage();

            var sheet = package.Workbook.Worksheets
            .Add("AutoService Report");

            sheet.Cells["B1"].Value = "Организация:";
            sheet.Cells["E1"].Value = "Сервис-ПРО";
            sheet.Cells["B2"].Value = "Адрес:";
            sheet.Cells["E2"].Value = "Улица пушкина дом колотушкаина";
            sheet.Cells["C4"].Value = "ЧЕК";
            sheet.Cells["C6"].Value = $"От {DateTime.Now}";

            sheet.Column(1).Width = 14;
            sheet.Column(2).Width = 14;
            sheet.Column(3).Width = 12;
            sheet.Column(5).Width = 14;

            sheet.Cells["B8"].Value = "Номер заказа:";
            sheet.Cells["E8"].Value = ordernumber;

            sheet.Cells["B10"].Value = "Клиент:";
            sheet.Cells["E10"].Value = FIO;

            sheet.Cells["C12"].Value = $"Заказанные услуги";

            int count = 0;
            int l = 0;
            for (int i = 14; i < 14+services.Count*2; i += 2)
            {
                sheet.Cells[$@"B{i}"].Value = $@"{l+1}.{services[l]}";
                sheet.Cells[$@"E{i}"].Value = $@"{price[l]} руб.";
                count = i;
                l++;
            }

            sheet.Cells[$@"C{count + 2}"].Value = $@"Итог: {ordersum} руб.";

            sheet.Cells[$@"B{count + 2 + 2}"].Value = $"Чек выдал(а):  {dbhelper.EecuteQueryReaderOne($@"select users.lastname || ' ' || users.firstname || ' ' || users.middlename as FIO from users where email = '{employeeMail}'","fio")}";

            string[] excelCells = new string[] { "B", "C", "D", "E", "F", "G" };

            for (int i = 1; i < count + 6; i++)
            {
                sheet.Cells[$@"B{i}"].Style.Border.Left.Style = ExcelBorderStyle.Double;
                sheet.Cells[$@"G{i}"].Style.Border.Right.Style = ExcelBorderStyle.Double;
                for (int j = 0; j < excelCells.Length; j++)
                {
                    if (i % 2 == 1)
                    {
                        sheet.Cells[$@"{excelCells[j]}{i}"].Style.Border.Bottom.Style = ExcelBorderStyle.Double;
                    }
                    if (i == 1)
                    {
                        sheet.Cells[$@"{excelCells[j]}{i}"].Style.Border.Top.Style = ExcelBorderStyle.Double;
                    }
                }
            }

            sheet.Protection.IsProtected = true;
            return package.GetAsByteArray();
        }
    }
}
