using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Text.RegularExpressions;


namespace ValuteServices
{
    public class Valute
    {
        private string line;
        public Valute(string line)
        {
            this.line = line;
        }
        public string GetValuteDollar()
        {
            using (WebClient wc = new WebClient())
                line = wc.DownloadString("http://www.cbr.ru/scripts/XML_daily.asp");
            Match match = Regex.Match(line, "<Name>Доллар США</Name><Value>(.*?)</Value>");
            return match.Groups[1].Value;
        }
        public string GetValuteEuro()
        {
            using (WebClient wc = new WebClient())
                line = wc.DownloadString("http://www.cbr.ru/scripts/XML_daily.asp");
            Match match = Regex.Match(line, "<Name>Евро</Name><Value>(.*?)</Value>");
            return match.Groups[1].Value;
        }

    }
}
