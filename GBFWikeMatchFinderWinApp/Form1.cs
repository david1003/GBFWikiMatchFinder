using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Media;
using System.Net;

namespace GBFWikeMatchFinderWinApp
{
    public partial class Form_GBF : Form
    {
        //最後處理時間(加一小時換成日本時間)
        private static DateTime _lastMatchTime = DateTime.Now.AddHours(1);
        public Form_GBF()
        {
            InitializeComponent();
        }

        private void Gbf_notifyIcon_MouseMove(object sender, MouseEventArgs e)
        {
            //GBF_notifyIcon.ShowBalloonTip(3000);
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

        private void GOFind()
        {
            //開始時間
            Clipboard.SetText(DateTime.Now.ToString() + "GOFind");

            Console.OutputEncoding = Encoding.Unicode;
            string userInput = string.Empty;

            List<string> FindList = new List<string>();
            foreach (var item in CLB_List.CheckedItems)
            {
                FindList.Add(item.ToString());
            }


            while (true)
            {
                DateTime timeNow = DateTime.Now;
                Thread.Sleep(3000);
                FindMatch(FindList);
            }
        }

        private void FindMatch(List<string> FindList)
        {

            string url =
                    "http://gbf-wiki.com/index.php?%BB%CD%C2%E7%C5%B7%BB%CA%A5%DE%A5%EB%A5%C1%A5%D0%A5%C8%A5%EB_%B5%DF%B1%E7%CA%E7%BD%B8%C8%C4";

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
                    foreach (var Finditem in FindList)
                    {
                        if (capture.Value.Contains(Finditem))
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
                                            WriteLog($"發現{Finditem}，ID:{matchId}");
                                            GBF_notifyIcon.BalloonTipTitle = $"發現{Finditem}";
                                            GBF_notifyIcon.BalloonTipText = $"ID:{matchId}";
                                            GBF_notifyIcon.ShowBalloonTip(10000);
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
        }

        private void WriteLog(string message)
        {
            var now = DateTime.Now;
            Console.WriteLine("{0}:{1}:{2}\t{3}", now.ToString("HH"), now.ToString("mm"), now.ToString("ss"), message);
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
        Thread _thread;
        public void Start()
        {
            if (_thread == null || !_thread.IsAlive)
            {

                _thread = new Thread(GOFind);
                _thread.SetApartmentState(ApartmentState.STA);
                _thread.IsBackground = true;
                _thread.Start();
                Console.WriteLine("Thread started running");
            }
        }
        public void Pause()
        {
            /* Sets the state of the event to nonsignaled, 
             * causing threads to block.
             */
            _pauseEvent.Reset();
            Console.WriteLine("Thread paused");
        }

        public void Resume()
        {
            /* Sets the state of the event to signaled, 
             * allowing one or more waiting threads to proceed.
             */
            _pauseEvent.Set();
            Console.WriteLine("Thread resuming ");
        }
        public void Stop()
        {
            if (_thread != null && _thread.IsAlive)
            {

                // Signal the shutdown event
                _shutdownEvent.Set();
                Console.WriteLine("Thread Stopped ");

                // Make sure to resume any paused threads
                _pauseEvent.Set();

                // Wait for the thread to exit
                _thread.Abort();
                _thread.Join();
            }
        }

        #endregion


        #region 按鈕事件
        private void BTN_GO_Click(object sender, EventArgs e)
        {
            lb_status.Text = "尋找中";
            Start();
        }
        private void BTN_STOP_Click(object sender, EventArgs e)
        {
            lb_status.Text = "停止中";
            Stop();
        }

        private void 開始ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lb_status.Text = "尋找中";
            Start();
        }

        private void 停止ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lb_status.Text = "停止中";
            Stop();
        }

        private void 離開ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lb_status.Text = "停止中";
            Stop();
            this.Close();
        }

        #endregion
    }
}
