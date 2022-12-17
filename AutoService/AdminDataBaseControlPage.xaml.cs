using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для AdminDataBaseControlPage.xaml
    /// </summary>
    public partial class AdminDataBaseControlPage : Page
    {
        //экземпляр класса управления БД
        DBHelper dbhelper = new DBHelper();

        /// <summary>
        /// Инициализация компонентов страницы управления БД
        /// </summary>
        public AdminDataBaseControlPage()
        {
            InitializeComponent();
            DirectoryInfo dir = new DirectoryInfo((System.IO.Directory.GetCurrentDirectory() + @"\BackupFiles").Replace(@"\bin", "").Replace(@"\Debug", ""));
            FileInfo[] Files = dir.GetFiles();
            foreach (FileInfo file in Files)
                cbDateRollback.Items.Add(file.Name.Insert(2, ".").Insert(5, ".").Replace(".backup",""));
        }

        /// <summary>
        /// Обработчик кнопки резерного копирования
        /// </summary>
        /// <param name="sender">ссылка на элемент управления</param>
        /// <param name="e">данные события</param>
        private void btnRezCopy_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog()
            {
                Filter = "BACKUP Files|*.backup*"
            };
            dialog.Title = "Save as backup file";
            if (dialog.ShowDialog() == true)
            {
                dbhelper.Backup(dialog.FileName + ".backup");
            }
           

        }

        /// <summary>
        /// Обработчик кнопки бэкапа
        /// </summary>
        /// <param name="sender">ссылка на элемент управления</param>
        /// <param name="e">данные события</param>
        private void btnBackup_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog()
            {
                Filter = "BACKUP Files|*.backup*"
            };
            dialog.Title = "Choose backup file";
            if (dialog.ShowDialog() == true)
            {
                dbhelper.Restore(dialog.FileName);
            }
        }

        /// <summary>
        /// Обработчик отката системы по дате
        /// </summary>
        /// <param name="sender">ссылка на элемент управления</param>
        /// <param name="e">данные события</param>
        private void btnRollback_Click(object sender, RoutedEventArgs e)
        {
            dbhelper.Restore((System.IO.Directory.GetCurrentDirectory() + @"\BackupFiles").Replace(@"\bin", "").Replace(@"\Debug", "")+@"\"+cbDateRollback.SelectedItem.ToString().Replace(".","")+".backup");
        }
    }
}
