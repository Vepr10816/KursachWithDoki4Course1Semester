using Npgsql;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;

namespace AutoService
{
    /// <summary>
    /// Класс формирования запросов в БД
    /// </summary>
    public class DBHelper
    {
        //Строка подключения
        public static string conn_param = "Server=192.168.0.12; Port=5432;User Id=postgres;Password=1;Database=AutoService; Timeout=300; CommandTimeout=300"; //25.41.59.168 192.168.169.204 192.168.0.16

        //Перенаправление подключения pgsql
        public NpgsqlConnection connection = new NpgsqlConnection(conn_param);

        //Считыватель значения БД
        NpgsqlDataReader dataReader = null;

        // инструкция SQL для выполнения в базе данных PostgreSQL
        NpgsqlCommand command = null;

        //Экземпляр класса валидации
        ValidationData valid = new ValidationData();

        /// <summary>
        /// формирования запроса на авторизацию
        /// </summary>
        /// <param name="login">логин пользователя</param>
        /// <param name="password">пароль авторизация</param>
        /// <returns>роль пользователя</returns>
        public string Authorization(string login, string password)
        {
            try
            {
                connection.Open();
                command = new NpgsqlCommand($@"select Sign_In('{login}','{password}')", connection);
                dataReader = command.ExecuteReader();
                string roleName = "";
                while (dataReader.Read())
                {
                    roleName = dataReader["sign_in"].ToString();
                }
                if (roleName == "false")
                {
                    valid.Show("Неправильный логин или пароль").GetAwaiter();
                    return null;
                }
                else
                    return roleName;
            }
            catch (NpgsqlException ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
                return null;
            }
            finally
            {
                dataReader.Close();
                connection.Close();
            }
        }

        /// <summary>
        /// Обновление всей страницы
        /// </summary>
        /// <param name="sender">ссылка на элемент управления</param>
        /// <param name="query">запрос</param>
        /// <param name="grd">Grid</param>
        public void Refresh(object sender, string query, Grid grd)
        {
            try
            {
                connection.Close();
                connection.Open();
                command = new NpgsqlCommand(query, connection);
                DataTable datatbl = new DataTable();
                datatbl.Load(command.ExecuteReader());
                (sender as DataGrid).ItemsSource = datatbl.DefaultView;
                if (grd != null)
                {
                    try
                    {
                        foreach (Control ctl in grd.Children)
                        {
                            if (ctl.GetType() == typeof(DatePicker))
                                ((DatePicker)ctl).SelectedDate = null;
                            if (ctl.GetType() == typeof(RichTextBox))
                            {
                                FlowDocument document = new FlowDocument();
                                Paragraph paragraph = new Paragraph();
                                paragraph.Inlines.Add(new Bold(new Run("")));
                                document.Blocks.Add(paragraph);
                                ((RichTextBox)ctl).Document = document;
                            }
                            if (ctl.GetType() == typeof(TextBox))
                            {
                                ((TextBox)ctl).Text = String.Empty;
                            }
                            if (ctl.GetType() == typeof(ComboBox))
                            {
                                ((ComboBox)ctl).SelectedIndex = -1;
                            }
                        }
                    }
                    catch { }
                }
            }
            catch (NpgsqlException ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Выполнение запроса
        /// </summary>
        /// <param name="query">запрос</param>
        public void EecuteQuery(string query)
        {
            try
            {
                connection.Open();
                command = new NpgsqlCommand(query, connection);
                dataReader = command.ExecuteReader();

            }
            catch (NpgsqlException ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public List<string> EecuteQueryReader(string query, List<string> data, string tablename)
        {
            if (query == "select distinct extract(year from registrationdate) from Users;")
            {
                List<int> dataint = new List<int>();
                try
                {
                    connection.Open();
                    command = new NpgsqlCommand(query, connection);
                    dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        dataint.Add(Convert.ToInt32(dataReader[tablename].ToString()));
                    }
                    dataint.Sort();
                    foreach(int a in dataint)
                    {
                        data.Add(a.ToString());
                    }
                    return data;
                }
                catch (NpgsqlException ex)
                {
                    System.Windows.MessageBox.Show(ex.Message);
                    return data;
                }
                finally
                {
                    connection.Close();
                    dataReader.Close();
                }
            }
            else
            {
                try
                {
                    connection.Open();
                    command = new NpgsqlCommand(query, connection);
                    dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        data.Add(dataReader[tablename].ToString());
                    }
                    return data;
                }
                catch (NpgsqlException ex)
                {
                    System.Windows.MessageBox.Show(ex.Message);
                    return data;
                }
                finally
                {
                    connection.Close();
                    dataReader.Close();
                }
            }
        }

        /// <summary>
        /// Считывание определенного значения из БД
        /// </summary>
        /// <param name="query">запрос</param>
        /// <param name="tablename">название таблицы</param>
        /// <returns>значение из БД</returns>
        public string EecuteQueryReaderOne(string query, string tablename)
        {
            string data = "";
            try
            {
                connection.Close();
                connection.Open();
                command = new NpgsqlCommand(query, connection);
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    data = dataReader[tablename].ToString();
                }
                return data;
            }
            catch (NpgsqlException ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
                return data;
            }
            finally
            {
                dataReader.Close();
                connection.Close();
            }
        }

        //Путь к dump pgsql
        string strPG_dumpPath = "SET PGPASSWORD=1\r\n\r\ncd /D C:\\Program Files\r\n\r\ncd PostgreSQL\r\n\r\ncd 13\r\n\r\ncd bin\r\n\r\n";
        
        //Ip сервера
        string strServer = "192.168.0.12";

        //Порт
        string strPort = "5432";

        //Название ДБ
        string strDatabaseName = "AutoService";

        /// <summary>
        /// Создание файла Backup
        /// </summary>
        /// <param name="pathSave">Путь сохранения</param>
        public void Backup(string pathSave)
        {
            try
            {
                StreamWriter sw = new StreamWriter("DBBackup.bat");
                // Do not change lines / spaces b/w words.
                StringBuilder strSB = new StringBuilder(strPG_dumpPath);

                if (strSB.Length != 0)
                {
                    //strSB.Append("SET PGPASSWORD=1");
                    strSB.Append("pg_dump.exe --host " + strServer + " --port " + strPort + " --username postgres --format custom --blobs --verbose --file ");
                    strSB.Append("\"" + pathSave + "\"");
                    strSB.Append(" \"" + strDatabaseName+"\"" + "\r\n\r\n");
                    sw.WriteLine(strSB);
                    sw.Dispose();
                    sw.Close();
                    Process processDB = Process.Start("DBBackup.bat");
                    MessageBox.Show("Резервная копия успешно сохранена");
                }
                else
                {
                    MessageBox.Show("Пожалуйста, укажите место для создания резервной копии");
                }
            }
            catch 
            { }
        }

        /// <summary>
        /// Восстановление БД
        /// </summary>
        /// <param name="pathFile">путь к резервной копии</param>
        public void Restore(string pathFile)
        {
            strDatabaseName = "ProbaRestore";
            connection.Open();
            try
            {
                if (new NpgsqlCommand("SELECT 1 as count FROM pg_database WHERE datname='ProbaRestore'", connection).ExecuteScalar().ToString() == "1")
                {
                    command = new NpgsqlCommand("DROP DATABASE \"ProbaRestore\";", connection);
                    command.ExecuteNonQuery();
                }
                command = new NpgsqlCommand("Create DATABASE \"ProbaRestore\";", connection);
                command.ExecuteNonQuery();
                connection.Close();

                if (strDatabaseName != "")
                {
                    if (pathFile != "")
                    {
                        StreamWriter sw = new StreamWriter("DBRestore.bat");
                        // Do not change lines / spaces b/w words.
                        StringBuilder strSB = new StringBuilder(strPG_dumpPath);
                        if (strSB.Length != 0)
                        {
                            strSB.Append("pg_restore.exe --host " + strServer +
                               " --port " + strPort + " --username postgres --dbname");
                            strSB.Append(" \"" + strDatabaseName + "\"");
                            strSB.Append(" --verbose ");
                            strSB.Append("\"" + pathFile + "\"");
                            sw.WriteLine(strSB);
                            sw.Dispose();
                            sw.Close();
                            Process processDB = Process.Start("DBRestore.bat");
                            MessageBox.Show("Успешный backup данных");
                        }
                        else
                        {
                            MessageBox.Show("Пожалуйста, введите путь сохранения, чтобы получить резервную копию");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please enter the Database name to Restore!");
                }
            }
            catch { MessageBox.Show("База данных занята другой программой"); connection.Close(); }
        }

        /// <summary>
        /// Выборка Данных из DataGrid
        /// </summary>
        /// <param name="grd">DataGrid</param>
        /// <param name="value">Наименование столбца</param>
        /// <returns></returns>
        public string GetValueData(DataGrid grd, string value)
        {
            try
            {
                IList rows = grd.SelectedItems;
                DataRowView row = (DataRowView)grd.SelectedItems[0];
                return row[value].ToString();
            }
            catch
            {
                return "";
            }
        }

        public void InsertInto(string tableName, string[] atributes)
        {
            string query = "";
            query = $@"call {tableName}_insert(";
            for (int i = 0; i < atributes.Length; i++)
            {
                if (i != atributes.Length - 1)
                    query += $@"'{atributes[i]}',";
                else
                    query += $@"'{atributes[i]}');";
            }
            try
            {
                connection.Open();
                command = new NpgsqlCommand(query, connection);
                command.ExecuteNonQuery();
            }
            catch (NpgsqlException ex)
            {
                valid.Show(ex.Message).GetAwaiter();
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Формирование запроса на удаление
        /// </summary>
        /// <param name="tableName">Название таблицы</param>
        /// <param name="ID">Уникальный индетификатор</param>
        public void DeleteInto(string tableName, string ID)
        {
            string query = $@"call {tableName}_Delete('{ID}')";
            try
            {
                connection.Open();
                command = new NpgsqlCommand(query, connection);
                command.ExecuteNonQuery();
            }
            catch (NpgsqlException ex)
            {
                valid.Show(ex.Message).GetAwaiter();
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Формирование запроса на обновление данных в БД
        /// </summary>
        /// <param name="tableName">название таблицы</param>
        /// <param name="atributes">данные для добавления</param>
        public void UpdateInto(string tableName, string[] atributes)
        {
            string query = $@"call {tableName}_Update(";
            for (int i = 0; i < atributes.Length; i++)
            {
                if (i != atributes.Length - 1)
                    query += $@"'{atributes[i]}',";
                else
                    query += $@"'{atributes[i]}');";
            }
            try
            {
                connection.Open();
                command = new NpgsqlCommand(query, connection);
                command.ExecuteNonQuery();
            }
            catch (NpgsqlException ex)
            {
                valid.Show(ex.Message).GetAwaiter();
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Биндинг Combobox
        /// </summary>
        /// <param name="comboBox">ComboBox</param>
        /// <param name="query">Запрос</param>
        /// <param name="tableNameForDisplay">Выводиме значения</param>
        /// <param name="tableNameForBinding">Значения выводимых значений</param>
        public void BindComboBox(DataGridComboBoxColumn comboBox,string query, string tableNameForDisplay ,string tableNameForBinding)
        {
            command = new NpgsqlCommand(query, connection);
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter();
            adapter.SelectCommand = command;
            DataTable table = new DataTable();
            adapter.Fill(table);

            comboBox.ItemsSource = table.DefaultView;
            comboBox.DisplayMemberPath = tableNameForDisplay;
            comboBox.SelectedValuePath = tableNameForBinding;
            comboBox.SelectedValueBinding = new Binding(tableNameForBinding);
        }

        /// <summary>
        /// Получение значения ячеки DataGrid
        /// </summary>
        /// <param name="grd">DataGrid</param>
        /// <param name="column">Наименование столбца DataGrid</param>
        /// <returns>значение ячейки</returns>
        public string GetTextComboBox(DataGrid grd, int column)
        {
            DataGridRow row = grd.ItemContainerGenerator.ContainerFromIndex(grd.SelectedIndex) as DataGridRow;
            ComboBox ele = grd.Columns[column].GetCellContent(row) as ComboBox;
            return ele.Text;
        }
    }
}
