using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LiveCharts;
//using LiveCharts.Wpf;

namespace AutoService
{
    /// <summary>
    /// Логика взаимодействия для DiagramAdmin.xaml
    /// </summary>
    public partial class DiagramAdmin : Page
    {
        //экземпляр класса управления БД
        DBHelper dbhelper = new DBHelper();

        //Коллекция значений по оси X
        List<string> axisXData = new List<string>();

        //Коллекция значений по оси Y
        List<string> axisYData = new List<string>();

        /// <summary>
        /// Инициализация компонентов страницы графиков
        /// </summary>
        public DiagramAdmin()
        {
            InitializeComponent();
            // добавим данные линии
            axisXData = dbhelper.EecuteQueryReader("select distinct extract(year from registrationdate) from Users;", axisXData, "date_part");

            foreach (string year in axisXData)
            {
                axisYData.Add(dbhelper.EecuteQueryReaderOne($@"select count(email) from users where extract(year from registrationdate) = {year};", "count"));
            }
            typeDiagram.SelectedIndex = 0;

        }

        /// <summary>
        /// Обработчик выбра значения в ComboBox вида графика
        /// </summary>
        /// <param name="sender">ссылка на элемент управления</param>
        /// <param name="e">данные события</param>
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            chart.ChartAreas.Clear();
            chart.Series.Clear();
            if (typeDiagram.SelectedIndex == 0)
            {
                // Все графики находятся в пределах области построения ChartArea, создадим ее
                chart.ChartAreas.Add(new ChartArea("Default"));
                // Добавим линию, и назначим ее в ранее созданную область "Default"
                chart.Series.Add(new Series("Количество пользователей"));
                chart.Series["Количество пользователей"].ChartArea = "Default";
                chart.Series["Количество пользователей"].ChartType = SeriesChartType.Line;
                chart.Series["Количество пользователей"].Points.DataBindXY(axisXData, axisYData);
                chart.Series["Количество пользователей"].Font = new Font("Courier New", 10000000000000, System.Drawing.FontStyle.Regular);
            }
            if (typeDiagram.SelectedIndex == 1)
            {
                // Все графики находятся в пределах области построения ChartArea, создадим ее
                chart.ChartAreas.Add(new ChartArea("Default"));
                // Добавим линию, и назначим ее в ранее созданную область "Default"
                chart.Series.Add(new Series("Количество пользователей"));
                chart.Series["Количество пользователей"].ChartArea = "Default";
                chart.Series["Количество пользователей"].ChartType = SeriesChartType.Pie;
                chart.Series["Количество пользователей"].IsValueShownAsLabel = true;
                chart.Series["Количество пользователей"].Points.DataBindXY(axisXData, axisYData);
            }
            if (typeDiagram.SelectedIndex == 2)
            {
                // Все графики находятся в пределах области построения ChartArea, создадим ее
                chart.ChartAreas.Add(new ChartArea("Default"));
                // Добавим линию, и назначим ее в ранее созданную область "Default"
                chart.Series.Add(new Series("Количество пользователей"));
                chart.Series["Количество пользователей"].ChartArea = "Default";
                chart.Series["Количество пользователей"].ChartType = SeriesChartType.Column;
                chart.Series["Количество пользователей"].IsValueShownAsLabel = true;
                chart.Series["Количество пользователей"].Points.DataBindXY(axisXData, axisYData);
            }
        }
    }


}

