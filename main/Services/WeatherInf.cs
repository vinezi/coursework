using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace main.Services
{
    class WeatherInf
    {
        private const string APIKEY = "LyGJIzhovFEZDamKoA9B5LUgppX4E5oo";
        private const string APIKEY_RES = "9BGnt2vsAWQG13vLMq3ZpzQnZb4W98me";
        private readonly string PATH = $"{Environment.CurrentDirectory}\\";
        private const string FILE_IP = @"IP.xml";
        private const string FILE_CONFIG_CITY = @"config_city.xml";
        private const string FILE_CONFIG_DAY = @"config_day.xml";
        private const string FILE_CONFIG_HOUR = @"config_hour.xml";


        private int _getConverter(int old)
        {
            return (old - 32) * 5 / 9;
        }

        private string _getInfo(string request, string fileName, bool updateFlag)
        {
            FileInfo fileInf = new FileInfo(PATH + fileName);
            if (fileInf.Exists && fileInf.Length == 0 || updateFlag) //true update
                fileInf.Delete();
            fileInf = new FileInfo(PATH + fileName);
            if (!fileInf.Exists)
            {
                WebClient wc = new WebClient();
                wc.Encoding = Encoding.UTF8;
                wc.DownloadFile(request, PATH + fileName);
            }
            string response = File.ReadAllText(PATH + fileName);
            return response;
        }

        private (string ip, bool isUpdate) _getIP()
        {
            string oldInf = _getInfo("https://api.ipify.org", FILE_IP, false);
            string newInf = _getInfo("https://api.ipify.org", FILE_IP, true);
            if (oldInf == newInf)
                return (oldInf, false);
            else
                return (newInf, true); //обнова тру
        }

        private (string cityKey, string cityName) _getKeyAndName()
        {
            var infoIP = _getIP();
            string IP = infoIP.Item1;
            bool updateFlag = infoIP.Item2;
            string correctXml, tmpURL = "http://dataservice.accuweather.com/locations/v1/cities/ipaddress?apikey=" + APIKEY + "&q=" + IP + "&language=ru-RU";
            if (updateFlag)
                correctXml = _getInfo(tmpURL, FILE_CONFIG_CITY, true);
            else
                correctXml = _getInfo(tmpURL, FILE_CONFIG_CITY, false);
            Match match;
            match = Regex.Match(correctXml, "\"Key\":\"(.*?)\",");
            string cityKey = match.Groups[1].Value; //ключ города
            match = Regex.Match(correctXml, "\"LocalizedName\":\"(.*?)\",");
            string cityName = match.Groups[1].Value;//имя города
            return (cityKey, cityName);
        }

        private (int min, int max) _getAverageTemperature()
        {
            Match match;
            var Inf = _getKeyAndName();
            string key = Inf.Item1;
            string correctXml, tmpURL = "http://dataservice.accuweather.com/forecasts/v1/daily/1day/" + key + "?apikey=" + APIKEY + "&language=ru-RU";
            correctXml = _getInfo(tmpURL, FILE_CONFIG_DAY, false);
            match = Regex.Match(correctXml, "\"Date\":\"(.*?)T");
            string oldDate = match.Groups[1].Value;
            string nowDate = DateTime.Now.ToString("yyyy-MM-dd");
            if (oldDate != nowDate)
                correctXml = _getInfo(tmpURL, FILE_CONFIG_DAY, true);
            match = Regex.Match(correctXml, "\"Minimum\":{\"Value\":(.*?).0,\"");
            int min = Int32.Parse(match.Groups[1].Value); //мин
            match = Regex.Match(correctXml, "\"Maximum\":{\"Value\":(.*?).0,\"");
            int max = Int32.Parse(match.Groups[1].Value);  // макс
            return (min, max);
        }

        private (string weatherType, int temperature) _getWeatherTypeAndTemperature()
        {
            Match match;
            var Inf = _getKeyAndName();
            string key = Inf.Item1;
            string correctXml, tmpURL = "http://dataservice.accuweather.com/forecasts/v1/hourly/1hour/" + key + "?apikey=" + APIKEY + "&language=ru-ru";
            correctXml = _getInfo(tmpURL, FILE_CONFIG_HOUR, false);
            match = Regex.Match(correctXml, "{\"DateTime\":\"(.*?):");
            string oldDate = match.Groups[1].Value;
            int offset = int.Parse(DateTime.Now.ToString("HH"));       //смещение +1 час
            string offsetShift = "";
            if (offset == 23)
                offsetShift += 00;
            else
                offsetShift += (offset + 1).ToString();                
            string nowDate = DateTime.Now.ToString("yyyy-MM-ddT") + offsetShift;
            if (oldDate != nowDate)
                correctXml = _getInfo(tmpURL, FILE_CONFIG_HOUR, true);
            match = Regex.Match(correctXml, "\"IconPhrase\":\"(.*?)\",");
            string weatherType = match.Groups[1].Value; //type
            match = Regex.Match(correctXml, "{\"Value\":(.*?).0,\"");
            int temperature = Int32.Parse(match.Groups[1].Value);  // now t
            return (weatherType, temperature);
        }

        public (int temperature, string weatherType, int min, int max, string cityName) AllData()
        {
            var tmp = _getWeatherTypeAndTemperature();
            int temperature = _getConverter(tmp.Item2);
            string weatherType = tmp.Item1;
            var tmp1 = _getAverageTemperature();
            int min = _getConverter(tmp1.Item1);
            int max = _getConverter(tmp1.Item2);
            var tmp2 = _getKeyAndName();
            string cityName = tmp2.Item2;
            return (temperature, weatherType, min, max, cityName);
        }

    }
}
/*
        private (string cityKey, string cityName) old_getKeyAndName()
        {
            string tmpURL = "http://dataservice.accuweather.com/locations/v1/cities/ipaddress?apikey=" + APIKEY + "&q=" + _getIP() + "&language=ru-RU";
            string fileName = @"file1.xml";
            Match match;
            FileInfo fileInf = new FileInfo(PATH + fileName);

            if (!fileInf.Exists)
            {
                WebClient wc = new WebClient();
                wc.Encoding = Encoding.UTF8;
                wc.DownloadFile(tmpURL, PATH + fileName);
            }
            string correctXml = File.ReadAllText(PATH + fileName);

            match = Regex.Match(correctXml, "\"Key\":\"(.*?)\",");
            string cityKey = match.Groups[1].Value; //ключ города
            match = Regex.Match(correctXml, "\"LocalizedName\":\"(.*?)\",");
            string cityName = match.Groups[1].Value;//имя города
            return (cityKey, cityName);
        }

        private (int min, int max) old_getAverageTemperature()
        {
            var Inf = _getKeyAndName();
            string key = Inf.Item1;
            string tmpURL = "http://dataservice.accuweather.com/forecasts/v1/daily/1day/" + key + "?apikey=" + APIKEY + "&language=ru-RU";
            string fileName = @"file2.xml";
            Match match;
            FileInfo fileInf = new FileInfo(PATH + fileName);

            if (!fileInf.Exists)
            {
                WebClient wc = new WebClient();
                wc.Encoding = Encoding.UTF8;
                wc.DownloadFile(tmpURL, PATH + fileName);
            }
            string correctXml = File.ReadAllText(PATH + fileName);

            match = Regex.Match(correctXml, "\"Minimum\":{\"Value\":(.*?).0,\""); 
            int min = Int32.Parse(match.Groups[1].Value); //мин

            match = Regex.Match(correctXml, "\"Maximum\":{\"Value\":(.*?).0,\"");
            int max = Int32.Parse(match.Groups[1].Value);  // макс

            return (min, max);
        }

        private (string weatherType, int temperature) old_getWeatherTypeAndTemperature()
        {
            var Inf = _getKeyAndName();
            string key = Inf.Item1;
            string tmpURL = "http://dataservice.accuweather.com/forecasts/v1/hourly/1hour/" + key + "?apikey=" + APIKEY + "&language=ru-ru";
            string fileName = @"file3.xml";
            Match match;
            FileInfo fileInf = new FileInfo(PATH + fileName);

            if (!fileInf.Exists)
            {
                WebClient wc = new WebClient();
                wc.Encoding = Encoding.UTF8;
                wc.DownloadFile(tmpURL, PATH + fileName);
            }
            string correctXml = File.ReadAllText(PATH + fileName);

            match = Regex.Match(correctXml, "\"IconPhrase\":\"(.*?)\",");
            string weatherType = match.Groups[1].Value; //type

            match = Regex.Match(correctXml, "{\"Value\":(.*?).0,\"");
            int temperature = Int32.Parse(match.Groups[1].Value);  // now t

            return (weatherType, temperature);
        }
*/

