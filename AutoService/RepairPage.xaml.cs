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
    /// Логика взаимодействия для RepairPage.xaml
    /// </summary>
    public partial class RepairPage : Page
    {
        DBHelper dbhelper = new DBHelper();
        public RepairPage()
        {
            InitializeComponent();
            dbhelper.Refresh(dgContracts, "select * from orderclient, contract where orderclient.ordernumber = contract.ordernumber", grdRepair);
        }

        private void dgContracts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
