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
using System.Windows.Threading;
using center;
using main.Servicec;

namespace main.Resourses
{
    /// <summary>
    /// Логика взаимодействия для TabMAIN.xaml
    /// </summary>
    public partial class TabMAIN : UserControl
    {
        DispatcherTimer timerdigital;
        public TabMAIN()
        {
            InitializeComponent();
            timerdigital = new DispatcherTimer();
            timerdigital.Interval = TimeSpan.FromSeconds(1.0);
            timerdigital.Start();
            timerdigital.Tick += new EventHandler(delegate (object s, EventArgs a)
            {
                tbDigital.Text = DateTime.Now.ToString("HH:mm:ss");
                tbDigital1.Text = DateTime.Now.ToString("ddd, dd MMMM yyyy");
            });
            //MM  tbDigital.Text = DateTime.Now.ToString("ddd, dd MMMM \n yyyy \n HH:mm:ss");
            WeatherInf weatherInf = new WeatherInf();
            var alldata = weatherInf.AllData();

            //tst.Content = alldata.Item1;
            //tst2.Content = alldata.Item2;
            //tst3.Content = alldata.Item3;
            //tst4.Content = alldata.Item4;
            //tst5.Content = alldata.Item5;

            //Run run = new Run(alldata.Item1.ToString());
            //run.FontSize = 40;
            //tbWeather.Inlines.Add(run);

            string part = "°C";
            tbrTemperature.Text = alldata.Item1.ToString();
            tbrTemperaturePart.Text = part;

            tbrWeatherType.Text = " " + alldata.Item2;

            tbrMin.Text = "\n" + alldata.Item3.ToString();
            tbrMinPart.Text = part;

            tbrMax.Text = "/" + alldata.Item4.ToString();
            tbrMaxPart.Text = part + " ";
            tbrCityName.Text = alldata.Item5;
        }
        //private void btest(object sender, RoutedEventArgs e)
        //{
        //    vinezi Valute = new vinezi("");
        //    lbValut.Content = Valute.GetValute();
        //}

    }
}
