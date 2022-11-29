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
        DBHelper dbhelper = new DBHelper();
        public AdminDataBaseControlPage()
        {
            InitializeComponent();
            DirectoryInfo dir = new DirectoryInfo((System.IO.Directory.GetCurrentDirectory() + @"\BackupFiles").Replace(@"\bin", "").Replace(@"\Debug", ""));
            FileInfo[] Files = dir.GetFiles();
            foreach (FileInfo file in Files)
                cbDateRollback.Items.Add(file.Name.Insert(2, ".").Insert(5, ".").Replace(".backup",""));
        }

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

        private void btnRollback_Click(object sender, RoutedEventArgs e)
        {
            dbhelper.Restore((System.IO.Directory.GetCurrentDirectory() + @"\BackupFiles").Replace(@"\bin", "").Replace(@"\Debug", "")+@"\"+cbDateRollback.SelectedItem.ToString().Replace(".","")+".backup");
        }
    }
}
