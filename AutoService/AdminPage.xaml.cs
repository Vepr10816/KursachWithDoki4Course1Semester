using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
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
    /// Логика взаимодействия для AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        //экземпляр класса управления БД
        DBHelper dbhelper = new DBHelper();

        //экземпляр класса валидации
        ValidationData valid = new ValidationData();

        //Коллекция годов регистрации
        List<string> dateRegistration = new List<string>();

        /// <summary>
        /// Обработчик выбра значения в DataGrid
        /// </summary>
        /// <param name="sender">ссылка на элемент управления</param>
        /// <param name="e">данные события</param>
        private void Data_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgUsers.SelectedItem == null)
            {
                return;
            }
            DataRowView row = (DataRowView)dgUsers.SelectedItem;
            tbEmail.Text = row[0].ToString();
            tbPassword.Text = row[1].ToString();
            tbFirstName.Text = row[2].ToString();
            tbLastName.Text = row[3].ToString();
            tbMiddleName.Text = row[4].ToString();
            cbPost.Text = row[6].ToString();

        }

        /// <summary>
        /// Инициализация компонентов страницы администратора
        /// </summary>
        public AdminPage()
        {
            InitializeComponent();
            BackupData();
            dbhelper.Refresh(dgUsers, "select * from Users_View; ", grdAdmin);
            dateRegistration = dbhelper.EecuteQueryReader("select distinct extract(year from registrationdate) from Users;", dateRegistration, "date_part");
            foreach(string years in dateRegistration)
                cbYearSearch.Items.Add(years.Replace("0:00:00",""));
        }

        /// <summary>
        /// Backup по определенной дате
        /// </summary>
        private void BackupData()
        {
            if (DateTime.Now.ToString("dd") == "28")
            {
                dbhelper.Backup((System.IO.Directory.GetCurrentDirectory() + @"\BackupFiles").Replace(@"\bin", "").Replace(@"\Debug", "")+$@"\{DateTime.Now.ToString("d").Replace(".", "")}.backup");
            }
        }

        /// <summary>
        /// Обработчик кнопки импорта
        /// </summary>
        /// <param name="sender">ссылка на элемент управления</param>
        /// <param name="e">данные события</param>
        private void btnImport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog op = new OpenFileDialog();
                op.Title = "Select as csv file";
                op.Filter = "CSV Files|*.csv";
                if (op.ShowDialog() == true)
                {
                    dbhelper.EecuteQuery($@"COPY Users FROM '{op.FileName}' DELIMITER ';' CSV HEADER;");
                    valid.Show("Данные импортированы").GetAwaiter();
                }
                dbhelper.Refresh(dgUsers, "select * from Users_View; ", grdAdmin);
            }
            catch (NotSupportedException)
            {
                valid.Show("Ошибка загрузки файла").GetAwaiter() ;
            }
        }

        /// <summary>
        /// Обработчик кнопки экспорта
        /// </summary>
        /// <param name="sender">ссылка на элемент управления</param>
        /// <param name="e">данные события</param>
        private void btnEport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveFileDialog dialog = new SaveFileDialog()
                {
                    Filter = "DQY Files|*.dqy*"
                };
                dialog.Title = "Save as dqy file";
                if (dialog.ShowDialog() == true)
                {
                    var buffer = Encoding.UTF8.GetBytes("XLODBC\n1\nDRIVER={PostgreSQL Unicode(x64)};DATABASE=AutoService;SERVER=192.168.0.12;PORT=5432;UID=postgres;PASSWORD=1;SSLmode=disable;ReadOnly=0;Protocol=7.4;FakeOidIndex=0;ShowOidColumn=0;RowVersioning=0;ShowSystemTables=0;ConnSettings=;Fetch=100;Socket=4096;UnknownSizes=0;MaxVarcharSize=255;MaxLongVarcharSize=8190;Debug=0;CommLog=0;Optimizer=0;Ksqo=1;UseDeclareFetch=0;TextAsLongVarchar=1;UnknownsAsLongVarchar=0;BoolsAsChar=1;Parse=0;CancelAsFreeStmt=0;ExtraSysTablePrefixes=dd_;LFConversion=1;UpdatableCursors=1;DisallowPremature=0;TrueIsMinus1=0;BI=0;ByteaAsLongVarBinary=0;UseServerSidePrepare=0;LowerCaseIdentifier=0;GssAuthUseGSS=0;XaOpt=1\nselect * from Users");

                    using (var fs = new FileStream(dialog.FileName+".dqy", FileMode.OpenOrCreate,
                        FileAccess.Write, FileShare.None, buffer.Length, true))
                    {
                        fs.Write(buffer, 0, buffer.Length);
                    }
                    valid.Show("Данные экспортированы").GetAwaiter();
                }
                dbhelper.Refresh(dgUsers, "select * from Users_View; ", grdAdmin);
            }
            catch (NotSupportedException)
            {
                valid.Show("Ошибка сохранения файла").GetAwaiter();
            }
        }

        /// <summary>
        /// Обработчик выбора даты регистрации
        /// </summary>
        /// <param name="sender">ссылка на элемент управления</param>
        /// <param name="e">данные события</param>
        private void cbYearSearch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(tbSearch.Text == "" && cbYearSearch.SelectedIndex != -1)
                dbhelper.Refresh(dgUsers, "select email as \"email\", password as \"Пароль\", lastname as \"Фамилия\",firstname as \"Имя\",middlename as \"Отчество\", RegistrationDate as \"Дата регистрации\", rolename as \"Роль\", extract(year from registrationdate) as \"Год\"from Users where extract(year from registrationdate) = " + cbYearSearch.SelectedItem.ToString(),null);
            else if(cbYearSearch.SelectedIndex != -1)
                dbhelper.Refresh(dgUsers, "select email as \"email\", password as \"Пароль\", lastname as \"Фамилия\",firstname as \"Имя\",middlename as \"Отчество\", RegistrationDate as \"Дата регистрации\", rolename as \"Роль\", extract(year from registrationdate) as \"Год\"from Users where extract(year from registrationdate) = " + cbYearSearch.SelectedItem.ToString() + $@" and (email LIKE '%{tbSearch.Text}%' or lastname LIKE '%{tbSearch.Text}%' or firstname LIKE'%{tbSearch.Text}%' or middlename LIKE'%{tbSearch.Text}%' or rolename LIKE'%{tbSearch.Text}%')", null);
        }

        /// <summary>
        /// Обработчик кнопки очищения параметров поиска
        /// </summary>
        /// <param name="sender">ссылка на элемент управления</param>
        /// <param name="e">данные события</param>
        private void btnClearSearch_Click(object sender, RoutedEventArgs e)
        {
            tbSearch.Text = "";
            cbYearSearch.SelectedIndex = -1;
            dbhelper.Refresh(dgUsers, "select * from Users_View; ", grdAdmin);
        }

        /// <summary>
        /// Обработчик ввода данных в строку поиска
        /// </summary>
        /// <param name="sender">ссылка на элемент управления</param>
        /// <param name="e">данные события</param>
        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbSearch.Text == "" && cbYearSearch.SelectedIndex == -1)
                dbhelper.Refresh(dgUsers, "select * from Users_View; ", null);
            else if (cbYearSearch.SelectedIndex != -1)
                dbhelper.Refresh(dgUsers, "select email as \"email\", password as \"Пароль\", lastname as \"Фамилия\",firstname as \"Имя\",middlename as \"Отчество\", RegistrationDate as \"Дата регистрации\", rolename as \"Роль\", extract(year from registrationdate) as \"Год\"from Users where extract(year from registrationdate) = " + cbYearSearch.SelectedItem.ToString() + $@" and (email LIKE '%{tbSearch.Text}%' or lastname LIKE '%{tbSearch.Text}%' or firstname LIKE'%{tbSearch.Text}%' or middlename LIKE'%{tbSearch.Text}%' or rolename LIKE'%{tbSearch.Text}%')", null);
            else if(cbYearSearch.SelectedIndex == -1)
                dbhelper.Refresh(dgUsers, "select email as \"email\", password as \"Пароль\", lastname as \"Фамилия\",firstname as \"Имя\",middlename as \"Отчество\", RegistrationDate as \"Дата регистрации\", rolename as \"Роль\", extract(year from registrationdate) as \"Год\"from Users where" + $@" email LIKE '%{tbSearch.Text}%' or lastname LIKE '%{tbSearch.Text}%' or firstname LIKE'%{tbSearch.Text}%' or middlename LIKE'%{tbSearch.Text}%' or rolename LIKE'%{tbSearch.Text}%'", null);
            else if(tbSearch.Text == "" && cbYearSearch.SelectedIndex != -1)
                dbhelper.Refresh(dgUsers, "select email as \"email\", password as \"Пароль\", lastname as \"Фамилия\",firstname as \"Имя\",middlename as \"Отчество\", RegistrationDate as \"Дата регистрации\", rolename as \"Роль\", extract(year from registrationdate) as \"Год\"from Users where extract(year from registrationdate) = " + cbYearSearch.SelectedItem.ToString(), null);
        }

        /// <summary>
        /// Обработчик кнопки скрытия пользователей
        /// </summary>
        /// <param name="sender">ссылка на элемент управления</param>
        /// <param name="e">данные события</param>
        private void btnHideUser_Click(object sender, RoutedEventArgs e)
        {
            dbhelper.EecuteQuery($@"UPDATE Users SET life = false WHERE email = '{dbhelper.GetValueData(dgUsers,"Email")}';");
            dbhelper.Refresh(dgUsers, "select * from Users_View; ", grdAdmin);
        }

        /// <summary>
        /// Обработчик кнопки раскрытия пользователей
        /// </summary>
        /// <param name="sender">ссылка на элемент управления</param>
        /// <param name="e">данные события</param>
        private void btnVisibleUsers_Click(object sender, RoutedEventArgs e)
        {
            dbhelper.EecuteQuery($@"UPDATE Users SET life = true WHERE life = false;");
            dbhelper.Refresh(dgUsers, "select * from Users_View; ", grdAdmin);
        }

        /// <summary>
        /// Обработчик кнопки добавления пользователей в БД
        /// </summary>
        /// <param name="sender">ссылка на элемент управления</param>
        /// <param name="e">данные события</param>
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (valid.IsValidEmail(tbEmail.Text) != false && valid.IsValidPassword(tbPassword.Text) != false && tbLastName.Text != "" && tbFirstName.Text != "" && tbMiddleName.Text != "" && cbPost.Text !="")
            {
                string[] atributes = { tbEmail.Text, valid.Encrypt(tbPassword.Text), tbLastName.Text, tbFirstName.Text, tbMiddleName.Text, cbPost.Text };
                dbhelper.InsertInto("Users", atributes);
                if (cbPost.SelectedIndex == 2)
                {
                    ModalWindowAddClient appointment = new ModalWindowAddClient(tbEmail.Text,0,"");
                    appointment.ShowDialog();
                }
                dbhelper.Refresh(dgUsers, "select * from Users_View; ", grdAdmin);
            }
        }

        /// <summary>
        /// Обработчик кнопки удаления пользователей из БД
        /// </summary>
        /// <param name="sender">ссылка на элемент управления</param>
        /// <param name="e">данные события</param>
        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            if (dgUsers.SelectedIndex == -1)
                return;
            if (MessageBox.Show("Вы точно хотите удалить выбранного пользователя?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                if (dbhelper.GetValueData(dgUsers, "Роль") == "Клиент")
                {
                    dbhelper.EecuteQuery($@"DELETE FROM Client WHERE Email = '{dbhelper.GetValueData(dgUsers, "Email")}';");
                }
                dbhelper.DeleteInto("Users", $@"{dbhelper.GetValueData(dgUsers, "Email")}");
                dbhelper.Refresh(dgUsers, "select * from Users_View; ", grdAdmin);
            }
        }

        /// <summary>
        /// Обработчик кнопки обновления данных пользователя в БД
        /// </summary>
        /// <param name="sender">ссылка на элемент управления</param>
        /// <param name="e">данные события</param>
        private void btnUpd_Click(object sender, RoutedEventArgs e)
        {
            string password = tbPassword.Text;
            if (dbhelper.EecuteQueryReaderOne($@"select count(password) as aaa from users where password = '{tbPassword.Text}'", "aaa") == "0")
                password = valid.Encrypt(password);
            if (dgUsers.SelectedIndex == -1)
                return;
            if (valid.IsValidEmail(tbEmail.Text) != false && valid.IsValidPassword(tbPassword.Text) != false && tbLastName.Text != "" && tbFirstName.Text != "" && tbMiddleName.Text != "" && cbPost.Text != "")
            {
                string[] atributes = { dbhelper.GetValueData(dgUsers, "Email"), password, tbLastName.Text, tbFirstName.Text, tbMiddleName.Text, cbPost.Text, tbEmail.Text};
                dbhelper.UpdateInto("Users", atributes);
            }
            dbhelper.Refresh(dgUsers, "select * from Users_View; ", grdAdmin);
        }

        /// <summary>
        /// Обработчик кнопки очищения текстовых полей
        /// </summary>
        /// <param name="sender">ссылка на элемент управления</param>
        /// <param name="e">данные события</param>
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            dbhelper.Refresh(dgUsers, "select * from Users_View; ", grdAdmin);
        }

        /// <summary>
        /// Обработчик вводимых и вставляемых значений в текстовое поле
        /// </summary>
        /// <param name="sender">ссылка на элемент управления</param>
        /// <param name="e">данные события</param>
        private void tb1_PreviewTextInputEngNum(object sender, TextCompositionEventArgs e) => new ValidationData().tb1_PreviewTextInputAngNum(sender, e);
        private void tb1_PastingEngNum(object sender, DataObjectPastingEventArgs e) => new ValidationData().tb1_PastingAngNum(sender, e);

        private void tb1_PreviewTextInputRus(object sender, TextCompositionEventArgs e) => new ValidationData().tb1_PreviewTextInputRus(sender, e);
        private void tb1_PastingRus(object sender, DataObjectPastingEventArgs e) => new ValidationData().tb1_PastingRus(sender, e);
    }
}
