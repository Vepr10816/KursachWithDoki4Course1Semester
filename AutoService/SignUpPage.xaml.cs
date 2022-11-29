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
    /// Логика взаимодействия для SignUpPage.xaml
    /// </summary>
    public partial class SignUpPage : Page
    {
        public SignUpPage()
        {
            InitializeComponent();
        }

        private void btnReg_Click(object sender, RoutedEventArgs e)
        {
            DBHelper dbhelper = new DBHelper();
            ValidationData valid = new ValidationData();
            if (pbPassword.Password == tbPassword.Password )
            {
                if (valid.IsValidEmail(tbLogin.Text) != false && valid.IsValidPassword(tbPassword.Password) != false && tbLastName.Text != "" && tbFirstName.Text != "" && tbMiddleName.Text != "" && !ClientNumber.Text.Contains("_") && !passportSeries.Text.Contains("_") && !passportNumber.Text.Contains("_"))
                {
                    string[] atributes = { tbLogin.Text, valid.Encrypt(tbPassword.Password), tbLastName.Text, tbFirstName.Text, tbMiddleName.Text, "Клиент" };
                    dbhelper.InsertInto("Users", atributes);
                    string[] atributes2 = { ClientNumber.Text.Replace("+7", ""), passportSeries.Text, passportNumber.Text, tbLogin.Text };
                    dbhelper.InsertInto("Client", atributes2);
                    FrameManager.MainFrame.GoBack();
                }
                else
                {
                    valid.Show("Все поля должны быть правильно заполнены").GetAwaiter(); ;
                }
            }
            else
            {
                valid.Show("Пароли не совпадают").GetAwaiter();
            }
        }
    }
}
