using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Text.RegularExpressions;


namespace center
{
    public class vinezi
    {
        private string line;
        public vinezi(string line)
        {
            this.line = line;
        }
        public string GetValute()
        {
            using (WebClient wc = new WebClient())
                line = wc.DownloadString("http://www.cbr.ru/scripts/XML_daily.asp");
            Match match = Regex.Match(line, "<Name>Доллар США</Name><Value>(.*?)</Value>");
            return match.Groups[1].Value;
        }
    }
}
