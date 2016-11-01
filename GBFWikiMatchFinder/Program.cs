using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Threading.Timer;

namespace GBFWikiMatchFinder
{
    class Program
    {
        //最後處理時間(加一小時換成日本時間)
        private static DateTime _lastMatchTime = DateTime.Now.AddHours(1);
        [STAThread]
        static void Main(string[] args)
        {
            //3s一次
            //int interval = 3000; 
            TimeSpan interval = TimeSpan.FromMilliseconds(3000);
            
            WriteLog("準備開始偵測...");
            //Timer timer = new Timer(FindMatch, replaceDayString, 2000, interval);
            //FindMatch(replaceDayString);
            
            DateTime timeLastCall = DateTime.Now;

            //因為clipboard只能run在STA, 只好自已寫TIMER

            while (true)
            {
                DateTime timeNow = DateTime.Now;
                if (timeNow - timeLastCall > interval)
                {
                    FindMatch();
                    timeLastCall = timeNow;
                }
            }
            ////Console.WriteLine(content);
            //Console.ReadLine();
        }

        private static void FindMatch()
        {
            
            string url =
                    "http://gbf-wiki.com/index.php?%C4%CC%BE%EF%A5%DE%A5%EB%A5%C1%A5%D0%A5%C8%A5%EB%B5%DF%B1%E7%CA%E7%BD%B8%C8%C4";

            WriteLog("偵測中");
            string content;
            using (var client = new WebClient())
            {
                client.Encoding = Encoding.GetEncoding("EUC-JP");
                content = client.DownloadString(url);
            }

            //最後一行li沒有換行
            var listExpress = "(?<list>(<li class=\"pcmt\">.*</li>))";
            Regex regex = new Regex(listExpress);
            //li用
            var liExpress = "<input.*>.*(?<matchid>([a-zA-Z0-9]){8}).*.*--.*<span class=\"comment_date\">(?<date>.*)<span";
            var liRegex = new Regex(liExpress);

            var matches = regex.Matches(content);
            foreach (Match match in matches)
            {
                var listGroup = match.Groups["list"];
                foreach (Capture capture in listGroup.Captures)
                {
                    if (capture.Value.Contains("グランデ") || capture.Value.Contains("黒麒麟"))
                    {
                        foreach (Match liMatch in liRegex.Matches(capture.Value))
                        {
                            //把曜日處理掉
                            var matchDtString = liMatch.Groups["date"].Value;
                            Regex regDay = new Regex(@"\(\w\)");
                            matchDtString = regDay.Replace(matchDtString, string.Empty);
                            var matchId = liMatch.Groups["matchid"].Value;

                            DateTime matchDt = DateTime.MinValue;
                            if (!string.IsNullOrWhiteSpace(matchId))
                            {
                                if (DateTime.TryParse(matchDtString, out matchDt))
                                {
                                    if (matchDt.CompareTo(_lastMatchTime) > 0)
                                    {
                                        WriteLog("發現丁丁，ID:" + matchId);
                                        Clipboard.SetText(matchId);
                                        PlaySound();
                                        _lastMatchTime = matchDt;
                                    }
                                }
                                else
                                {
                                    WriteLog("日期轉換錯誤。 " + matchDtString);
                                }
                            }
                        }
                    }
                }
            }
        }

        private static void WriteLog(string message)
        {
            var now = DateTime.Now;
            Console.WriteLine("{0}:{1}:{2}\t{3}", now.ToString("HH"), now.ToString("mm"), now.ToString("ss"), message);
        }

        private static void PlaySound()
        {
            SoundPlayer typewriter = new SoundPlayer();
            typewriter.SoundLocation = Environment.CurrentDirectory + "\\alert.wav";
            typewriter.Play();
        }
    }
}
