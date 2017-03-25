using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Media;
using System.Net;
using System.IO;
using Tweetinvi;
using Tweetinvi.Models;
using Tweetinvi.Streaming;
using Stream = System.IO.Stream;

namespace GBFWikeMatchFinderWinApp
{
    public partial class Form_GBF : Form
    {
        //最後處理時間(加一小時換成日本時間)
        private static DateTime _lastMatchTime = DateTime.Now.AddHours(1);
        private IFilteredStream _twitterStream;

        
        public Form_GBF()
        {
            InitializeComponent();

            ConsoleTextBoxWriter CTBW = new ConsoleTextBoxWriter(TB_MB);

            //var ds = GetWikiFindListDs();
            //clb_Wiki.DataSource = new BindingSource(ds, null);
            //clb_Wiki.DisplayMember = "Key";
            //clb_Wiki.ValueMember = "Value";

            var dsTwitter = GetTwitterFindListDs();
            clb_Twitter.DataSource = new BindingSource(dsTwitter, null);
            clb_Twitter.DisplayMember = "Key";
            clb_Twitter.ValueMember = "Value";

        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.GBF_notifyIcon.Visible = true;
                this.Hide();
            }
            else
            {
                this.GBF_notifyIcon.Visible = false;
            }
        }

        private void Gbf_notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void ExecuteFinder()
        {
            //開始時間
            //Clipboard.SetText(DateTime.Now.ToString() + " ExecuteFinder");
            Console.OutputEncoding = Encoding.Unicode;


            if (clb_Wiki.CheckedItems.Count + clb_Twitter.CheckedItems.Count == 0)
            {
                MessageBox.Show("請選擇要捉取的多人戰");
                return;
            }

            if (clb_Twitter.CheckedItems.Count > 0)
            {
                List<string> selectedValues = new List<string>();
                foreach (KeyValuePair<string, string> kvp in clb_Twitter.CheckedItems)
                {
                    selectedValues.Add(kvp.Value);
                }

                ExecuteTwitterFinder(selectedValues);
            }

            if (clb_Wiki.CheckedItems.Count > 0)
            {
                Dictionary<string, List<string>> groupByNameResult = new Dictionary<string, List<string>>();

                foreach (KeyValuePair<string, string> kvp in clb_Wiki.CheckedItems)
                {
                    if (groupByNameResult.ContainsKey(kvp.Value))
                    {
                        groupByNameResult[kvp.Value].Add(kvp.Key);
                    }
                    else
                    {
                        groupByNameResult.Add(kvp.Value, new List<string>()
                        {
                            kvp.Key
                        });
                    }
                }

                while (true)
                {
                    Thread.Sleep(3500);
                    try
                    {
                        WikiFindMatch(groupByNameResult);

                    }
                    catch (Exception ex)
                    {
                        ErrLog("FindMatch錯誤。 " + ex.ToString());
                        throw;
                    }
                }
            }
        }
        #region Wiki

        private Dictionary<string, string> GetWikiFindListDs()
        {
            string fourUrl =
                "http://gbf-wiki.com/index.php?%BB%CD%C2%E7%C5%B7%BB%CA%A5%DE%A5%EB%A5%C1%A5%D0%A5%C8%A5%EB_%B5%DF%B1%E7%CA%E7%BD%B8%C8%C4";
            string otherUrl =
                "http://gbf-wiki.com/index.php?%C4%CC%BE%EF%A5%DE%A5%EB%A5%C1%A5%D0%A5%C8%A5%EB_%B5%DF%B1%E7%CA%E7%BD%B8%C8%C4";

            var dict = new Dictionary<string, string>();
            dict.Add("ミカエル", fourUrl);
            dict.Add("ガブリエル", fourUrl);
            dict.Add("ウリエル", fourUrl);
            dict.Add("ラファエル", fourUrl);

            dict.Add("プロバハ", otherUrl);
            dict.Add("グランデ", otherUrl);
            dict.Add("黄龍", otherUrl);
            dict.Add("黒麒麟", otherUrl);

            return dict;
        }

        private void WikiFindMatch(Dictionary<string, List<string>> dicNameList)
        {
            
            WriteLog("偵測中");

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
                                                InvokeFoundNotify(match.Groups["matchid"].ToString(), match.Groups["name"].ToString());
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
            }
        }

        #endregion

        #region Twitter

        private Dictionary<string, string> GetTwitterFindListDs()
        {
            var dict = new Dictionary<string, string>();
            dict.Add("ミカエル", @"Lv100\s*ミカエル");
            dict.Add("ガブリエル", @"Lv100\s*ガブリエル");
            dict.Add("ウリエル", @"Lv100\s*ウリエル");
            dict.Add("ラファエル", @"Lv100\s*ラファエル");

            dict.Add("プロバハ", @"Lv100\s*プロトバハムート");
            dict.Add("グランデ", @"Lv100\s*ジ・オーダー・グランデ");
            dict.Add("黄龍", @"Lv100\s*黄龍");
            dict.Add("黒麒麟", @"Lv100\s*黒麒麟");

            return dict;
        }

        private void ExecuteTwitterFinder(List<string> selectedValues)
        {
            Auth.SetUserCredentials("K06bhz6lAMtpkZ7SaIYXSbJZD", "hsesgtAUeGsVJaWZ1VIqQYGaEYttAgZgqgstUWiIzOFUyedmsh", "2252101940-U4JtUqEOYYQa38cm0pG2blGf8uey1asNC2hppZO", "MQVHUgIbibEfJXGtlG0fBLNmI44WUQoQCIw43a9G8TToR");

            TweetinviEvents.QueryBeforeExecute += (sender, args) =>
            {
                //Console.WriteLine(args.QueryURL);
            };

            _twitterStream = Tweetinvi.Stream.CreateFilteredStream();

            //stream.AddTrack("参加者募集！参戦ID");
            _twitterStream.AddTrack("Lv100");
            _twitterStream.AddTweetLanguageFilter(LanguageFilter.Japanese);
            //stream.AddTweetLanguageFilter(LanguageFilter.Chinese);

            //組合name的pattern
            string namePatternString = $"(?<name>{string.Join("|", selectedValues)})";
            //Regex namePattern = new Regex(namePatternString);
            //Regex idPattern = new Regex(@"参戦ID：(?<matchid>([a-zA-Z0-9]){8})");

            Regex pattern = new Regex(@"参戦ID：(?<matchid>([a-zA-Z0-9]){8})\s*" + namePatternString);

            _twitterStream.MatchingTweetReceived += (sender, args) =>
            {
                var tweet = args.Tweet;
                var match = pattern.Match(tweet.FullText);
                if (match.Success)
                {
                    InvokeFoundNotify(match.Groups["matchid"].ToString(), match.Groups["name"].ToString());
                }
            };

            _twitterStream.StartStreamMatchingAnyCondition();
        }

        #endregion

        public delegate void DelegateFoundNotify(string matchId, string name);
        private void InvokeFoundNotify(string matchId, string name)
        {
            Invoke(new DelegateFoundNotify(FoundNotify), matchId, name);
        }

        private void FoundNotify(string matchId, string name)
        {
            string displayText = $"發現 {name}，ID: {matchId}";
            Clipboard.SetText(matchId);
            PlaySound();
            GBF_notifyIcon.ShowBalloonTip(15000, "大食怪天線", displayText, ToolTipIcon.Info);
            WriteLog(displayText);
            
        }

        private void WriteLog(string message)
        {
            var now = DateTime.Now;
            Console.WriteLine("{0}:{1}:{2}\t{3}", now.ToString("HH"), now.ToString("mm"), now.ToString("ss"), message);
        }
        private static void ErrLog(string message)
        {
            var now = DateTime.Now;
            FileStream fs = new FileStream(Environment.CurrentDirectory + "\\ErrLog.txt", FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);
            //開始寫入
            sw.Write("{0}:{1}:{2}\t{3}", now.ToString("HH"), now.ToString("mm"), now.ToString("ss"), message);
            //清空緩衝區
            sw.Flush();
            //關閉流
            sw.Close();
            fs.Close();
        }
        private void PlaySound()
        {
            SoundPlayer typewriter = new SoundPlayer();
            typewriter.SoundLocation = Environment.CurrentDirectory + "\\alert.wav";
            typewriter.Play();
        }

        #region 執行緒控制

        ManualResetEvent _shutdownEvent = new ManualResetEvent(false);
        ManualResetEvent _pauseEvent = new ManualResetEvent(true);
        Thread _threadWiki;
        public void Start()
        {
            if (_threadWiki == null || !_threadWiki.IsAlive)
            {

                _threadWiki = new Thread(ExecuteFinder);
                _threadWiki.SetApartmentState(ApartmentState.STA);
                _threadWiki.IsBackground = true;
                _threadWiki.Start();
                WriteLog("開始尋找");
            }
        }
        public void Pause()
        {
            /* Sets the state of the event to nonsignaled, 
             * causing threads to block.
             */
            _pauseEvent.Reset();
            _twitterStream.PauseStream();
            WriteLog("暫停尋找 ");
        }

        public void Resume()
        {
            /* Sets the state of the event to signaled, 
             * allowing one or more waiting threads to proceed.
             */
            _pauseEvent.Set();
            _twitterStream.ResumeStream();
            WriteLog("繼續尋找 ");
        }
        public void Stop()
        {
            if (_threadWiki != null && _threadWiki.IsAlive)
            {

                // Signal the shutdown event
                _shutdownEvent.Set();

                // Make sure to resume any paused threads
                _pauseEvent.Set();

                // Wait for the thread to exit
                _threadWiki.Abort();
                _threadWiki.Join();
                _twitterStream.StopStream();
                WriteLog("停止尋找 ");
            }
        }

        #endregion


        #region 按鈕事件
        private void BTN_GO_Click(object sender, EventArgs e)
        {
            Start();
        }
        private void BTN_STOP_Click(object sender, EventArgs e)
        {
            Stop();
        }

        private void 開始ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Start();
        }

        private void 停止ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stop();
        }

        private void 離開ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stop();
            this.Close();
        }

        #endregion
    }
}
