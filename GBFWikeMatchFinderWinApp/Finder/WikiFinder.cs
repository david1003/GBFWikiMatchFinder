using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace GBFWikeMatchFinderWinApp.Finder
{
    public class WikiFinder : IMultiBattleFinder
    {
        public event Action<string, string> OnBattleFound;
        public event Action<string> OnWriteLog;

        private Thread _thread;

        //最後處理時間(加一小時換成日本時間)
        private DateTime _lastMatchTime = DateTime.Now.AddHours(1);

        public void Execute(List<MultiBattleDefine> selectedBattles)
        {
            Dictionary<string, List<string>> battleGroupByResult = new Dictionary<string, List<string>>();

            foreach (MultiBattleDefine battleDefine in selectedBattles)
            {
                if (battleGroupByResult.ContainsKey(battleDefine.Value))
                {
                    battleGroupByResult[battleDefine.Value].Add(battleDefine.Name);
                }
                else
                {
                    battleGroupByResult.Add(battleDefine.Value, new List<string>()
                        {
                            battleDefine.Name
                        });
                }
            }

            _thread = new Thread(() => ExecuteWikiFinder(battleGroupByResult));
            _thread.Start();
        }

        private void ExecuteWikiFinder(Dictionary<string, List<string>> battleGroupByResult)
        {
            WriteLog(" start.");
            while (true)
            {
                try
                {
                    WikiFindMatch(battleGroupByResult);

                }
                catch (Exception ex)
                {
                    WriteLog($"error. {ex.ToString()}");
                }
                Thread.Sleep(3500);
            }
        }

        private void WikiFindMatch(Dictionary<string, List<string>> dicNameList)
        {
            foreach (var kvp in dicNameList)
            {
                string content;
                using (var client = new WebClient())
                {
                    client.Encoding = Encoding.GetEncoding("EUC-JP");
                    content = client.DownloadString(kvp.Key);
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
                        foreach (var item in kvp.Value)
                        {
                            if (capture.Value.Contains(item))
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
                                                OnBattleFound?.Invoke(matchId, item);
                                                _lastMatchTime = matchDt;
                                            }
                                        }
                                        else
                                        {
                                            WriteLog($" date convert error. {matchDtString}");
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void WriteLog(string msg)
        {
            OnWriteLog?.Invoke($"{nameof(WikiFinder)} {msg}");
        }

        //public void Pause()
        //{
        //    throw new NotImplementedException();
        //}

        public void Stop()
        {
            _thread.Abort();
            WriteLog(" stopped");
        }
    }
}
