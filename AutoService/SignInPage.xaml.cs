﻿using System;
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
    /// Логика взаимодействия для SignInPage.xaml
    /// </summary>
    public partial class SignInPage : Page
    {
        DBHelper dbhelper = new DBHelper();
        ValidationData valid = new ValidationData();
        public SignInPage()
        {
            InitializeComponent();
        }

        private void tb1_PreviewTextInput(object sender, TextCompositionEventArgs e) => new ValidationData().tb1_PreviewTextInputAngNum(sender, e);
        private void tb1_Pasting(object sender, DataObjectPastingEventArgs e) => new ValidationData().tb1_PastingAngNum(sender, e);

        private void btnAuth_Click(object sender, RoutedEventArgs e)
        {
            string role = dbhelper.Authorization(tbLogin.Text, tbPassword.Password);
            if (role !=null)
            {
                if(role == "Администратор")
                {
                    FrameManager.MainFrame.Navigate(new AdminPage());
                }
                if (role == "Сотрудник отдела закупок")
                {
                    FrameManager.MainFrame.Navigate(new PurchasePage());
                }
                if (role == "Сотрудник склада")
                {
                    FrameManager.MainFrame.Navigate(new WarehousePage());
                }
                if (role == "Сотрудник отдела продаж")
                {
                    FrameManager.MainFrame.Navigate(new SalesDepartmentPage(tbLogin.Text));
                }
                if (role == "Клиент")
                {
                    FrameManager.MainFrame.Navigate(new ClientPage(dbhelper.EecuteQueryReaderOne($@"select ClientNumber from client where email = '{tbLogin.Text}'", "ClientNumber")));
                }
            }
            else
            {
                ValidationData valid = new ValidationData();
                role = dbhelper.Authorization(tbLogin.Text, valid.Encrypt(tbPassword.Password));
                if(role == "Клиент")
                {
                    FrameManager.MainFrame.Navigate(new ClientPage(dbhelper.EecuteQueryReaderOne($@"select ClientNumber from client where email = '{tbLogin.Text}'", "ClientNumber")));
                }
            }
        }

        private void btnReg_Click(object sender, RoutedEventArgs e)
        {
            FrameManager.MainFrame.Navigate(new SignUpPage());
        }

        private void btnRecovery_Click(object sender, RoutedEventArgs e)
        {
            FrameManager.MainFrame.Navigate(new RecoveryPage());
        }
    }
}