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
    /// Логика взаимодействия для RepairPage.xaml
    /// </summary>
    public partial class RepairPage : Page
    {
        //экземпляр класса управления БД
        DBHelper dbhelper = new DBHelper();

        //экземпляр класса валидацции 
        ValidationData validation = new ValidationData();

        //уникальный индетификатор сотрудника
        string email = "";

        /// <summary>
        /// Инициализаии компонентов страницы ремонтного отдела
        /// </summary>
        /// <param name="employeeEmail">уникальный индетификатор сотрудника</param>
        public RepairPage(string employeeEmail)
        {
            InitializeComponent();
            dbhelper.Refresh(dgContracts, "select * from orderclient, contract where orderclient.ordernumber = contract.ordernumber", grdRepair);
            email = employeeEmail;
        }

        /// <summary>
        /// Обработчик выбора значения DataGrid контрактов
        /// </summary>
        /// <param name="sender">ссылка на элемент управления</param>
        /// <param name="e">данные события</param>
        private void dgContracts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dbhelper.Refresh(dgDiagnostics, $@"select * from diagnostics where contractnumber = '{dbhelper.GetValueData(dgContracts, "contractnumber")}'", grdRepair);
        }

        /// <summary>
        /// Обработчик изменения значения DataGrid диагностики
        /// </summary>
        /// <param name="sender">ссылка на элемент управления</param>
        /// <param name="e">данные события</param>
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
        }

        /// <summary>
        /// Обработчик выбора значения DataGrid диагностики
        /// </summary>
        /// <param name="sender">ссылка на элемент управления</param>
        /// <param name="e">данные события</param>
        private void dgDiagnostics_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tbStatusName.Text = dbhelper.EecuteQueryReaderOne($@"select statuscar from statuscar where diagnosticsnumber = '{dbhelper.GetValueData(dgDiagnostics, "diagnosticsnumber")}'", "statuscar");
            lbTimeStatus.Content = dbhelper.EecuteQueryReaderOne($@"select statustime from statuscar where diagnosticsnumber = '{dbhelper.GetValueData(dgDiagnostics, "diagnosticsnumber")}'", "statustime");
            if (lbTimeStatus.Content.ToString() == "" || lbTimeStatus.Content.ToString() == "00:00")
                lbTimeStatus.Content = DateTime.Now.ToString();
        }

        /// <summary>
        /// Обработчик кнопки обновления статуса
        /// </summary>
        /// <param name="sender">ссылка на элемент управления</param>
        /// <param name="e">данные события</param>
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

        /// <summary>
        /// Обработчик кнопки завершения заказа
        /// </summary>
        /// <param name="sender">ссылка на элемент управления</param>
        /// <param name="e">данные события</param>
        private void btnDelOrder_Click(object sender, RoutedEventArgs e)
        {
            if (dgDiagnostics.SelectedIndex != -1)
            {
                if (MessageBox.Show("Вы точно хотите закрыть ремонт?!?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
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

                    dbhelper.DeleteInto("orderclient", dbhelper.GetValueData(dgContracts, "ordernumber"));
                }
            }
            dbhelper.Refresh(dgContracts, "select * from orderclient, contract where orderclient.ordernumber = contract.ordernumber", grdRepair);
            dbhelper.Refresh(dgDiagnostics, $@"select * from diagnostics where contractnumber = ' '", grdRepair);
            tbStatusName.Text = "";
            lbTimeStatus.Content = DateTime.Now.ToString();
        }


        private byte[] Generate()
        {
            string contractNumber = dbhelper.GetValueData(dgContracts, "contractnumber");

            string carName = dbhelper.EecuteQueryReaderOne("select carmodel || ' ' || carbrand as carname from contract "+ 
                                                            "inner join orderclient on contract.ordernumber = orderclient.ordernumber "+
                                                            "inner join car on orderclient.carnumber = car.carnumber "+
                                                            $@"where contractnumber = '{contractNumber}'", "carname");

            List<string> diagnosticscode = new List<string>();
            List<string> diagnosticasresult = new List<string>();

            string query = $@"select * from diagnostics where contractnumber = '{contractNumber}'";

            diagnosticasresult = dbhelper.EecuteQueryReader(query, diagnosticasresult, "diagnosticsresults");
            diagnosticscode = dbhelper.EecuteQueryReader(query, diagnosticscode, "diagnosticsdate");

            var package = new ExcelPackage();



            var sheet = package.Workbook.Worksheets
            .Add("AutoService Report");

            sheet.Cells["B1"].Value = "Организация:";
            sheet.Cells["E1"].Value = "Сервис-ПРО";
            sheet.Cells["B2"].Value = "Адрес:";
            sheet.Cells["E2"].Value = "Улица пушкина дом колотушкаина";
            sheet.Cells["C4"].Value = "Диагностика автомобиля";
            sheet.Cells["C6"].Value = $"От {DateTime.Now}";

            sheet.Column(1).Width = 14;
            sheet.Column(2).Width = 14;
            sheet.Column(3).Width = 12;
            sheet.Column(5).Width = 14;

            sheet.Cells["B8"].Value = "Номер контракта";
            sheet.Cells["E8"].Value = contractNumber;

            sheet.Cells["B10"].Value = "Машина:";
            sheet.Cells["E10"].Value = carName;

            sheet.Cells["C12"].Value = $"Выполненные этапы дигностики";

            int count = 0;
            int l = 0;
            for (int i = 14; i < 14 + diagnosticscode.Count * 2; i += 2)
            {
                sheet.Cells[$@"B{i}"].Value = $@"{l + 1}.{diagnosticscode[l]}";
                sheet.Cells[$@"E{i}"].Value = $@"{diagnosticasresult[l]}";
                count = i;
                l++;
            }

            sheet.Cells[$@"C{count + 2}"].Value = $@"Итог: {tbStatusName.Text}";

            sheet.Cells[$@"B{count + 2 + 2}"].Value = $"Ответственный за диагностику:  {dbhelper.EecuteQueryReaderOne($@"select users.lastname || ' ' || users.firstname || ' ' || users.middlename as FIO from users where email = '{email}'", "fio")}";

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
