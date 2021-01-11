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
using ValuteServices;
using main.Services;

namespace main.Resources
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
            WeatherInf weatherInf = new WeatherInf();
            try
            {
                var alldata = weatherInf.AllData();
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\nНекоторые данные не доступны!");
                tbrMinPart.Text = "Ошибка загрузки погоды";
            }
            try
            {
                Valute Valute = new Valute("");
                tbrCourseDollar.Text = "$ " + Valute.GetValuteDollar();
                tbrCourseEuro.Text = "\t€ " + Valute.GetValuteEuro();
            }
            catch (Exception)
            {
                tbrCourseDollar.Text = "";
                tbrCourseEuro.Text = "Ошибка загрузки валют";
            }
            
        }


    }
}
