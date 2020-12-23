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

        public string GetValuteList()
        {
            string fq = "";
            using (WebClient wc = new WebClient())
                line = wc.DownloadString("http://www.cbr.ru/scripts/XML_daily.asp");
            Regex rg = new Regex("<Name>(.*?)</Name>");
            MatchCollection wef = rg.Matches(line);
            //for (int i = 0; i < line.Length(); i++)
            ////Match match = Regex.Match(line, "<Name>(.*?)</Name>");
            //return match.Groups[1].Value;
            ////fq += match.Groups[1].Value;
            ////return fq;
            //return match.Groups[1].Value ;
            for (int i = 0; i < wef.Count; i++)
            {
                fq += wef[i].Value;
            }
            return fq;
        }
    }
}
