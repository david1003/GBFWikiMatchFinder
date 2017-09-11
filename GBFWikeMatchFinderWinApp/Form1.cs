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
using GBFFinderLibrary;
using GBFWikeMatchFinderWinApp.Finder;
using Tweetinvi;
using Tweetinvi.Models;
using Tweetinvi.Streaming;
using Stream = System.IO.Stream;

namespace GBFWikeMatchFinderWinApp
{
    public partial class Form_GBF : Form
    {
        List<IMultiBattleFinder> _executeFinders = new List<IMultiBattleFinder>();

        
        public Form_GBF()
        {
            InitializeComponent();

            ConsoleTextBoxWriter CTBW = new ConsoleTextBoxWriter(TB_MB);

            var ds = GetWikiFindListDs();
            clb_Wiki.DataSource = new BindingSource(ds, null);
            clb_Wiki.DisplayMember = "Name";
            clb_Wiki.ValueMember = "Value";

            var dsTwitter = GetTwitterMultiBattles();
            clb_Twitter.DataSource = new BindingSource(dsTwitter, null);
            clb_Twitter.DisplayMember = "Name";
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
                List<MultiBattleDefine> selectedBattles = new List<MultiBattleDefine>();
                foreach (MultiBattleDefine battleDefine in clb_Twitter.CheckedItems)
                {
                    selectedBattles.Add(battleDefine);
                }
                IMultiBattleFinder finder = new TwitterFinder();
                ExecuteFinder(selectedBattles, finder);
            }

            if (clb_Wiki.CheckedItems.Count > 0)
            {
                List<MultiBattleDefine> selectedBattles = new List<MultiBattleDefine>();
                foreach (MultiBattleDefine battleDefine in clb_Wiki.CheckedItems)
                {
                    selectedBattles.Add(battleDefine);
                }
                IMultiBattleFinder finder = new WikiFinder();
                ExecuteFinder(selectedBattles, finder);
            }
        }

        #region GetMultiBattles

        public List<MultiBattleDefine> GetTwitterMultiBattles()
        {
            var battles = new List<MultiBattleDefine>();
            battles.Add(new MultiBattleDefine("ミカエル", @"ミカエル", 100));
            battles.Add(new MultiBattleDefine("ガブリエル", @"ガブリエル", 100));
            battles.Add(new MultiBattleDefine("ウリエル", @"ウリエル", 100));
            battles.Add(new MultiBattleDefine("ラファエル", @"ラファエル", 100));
            battles.Add(new MultiBattleDefine("プロバハ", @"プロトバハムート", 100));
            battles.Add(new MultiBattleDefine("グランデ", @"ジ・オーダー・グランデ", 100));
            battles.Add(new MultiBattleDefine("黄龍", @"黄龍", 100));
            battles.Add(new MultiBattleDefine("黒麒麟", @"黒麒麟", 100));
            battles.Add(new MultiBattleDefine("セレスト", @"セレスト", 75));
            battles.Add(new MultiBattleDefine("シュヴァリエ", @"シュヴァリエ", 75));

            return battles;
        }

        private List<MultiBattleDefine> GetWikiFindListDs()
        {
            string fourUrl =
                "http://gbf-wiki.com/index.php?%BB%CD%C2%E7%C5%B7%BB%CA%A5%DE%A5%EB%A5%C1%A5%D0%A5%C8%A5%EB_%B5%DF%B1%E7%CA%E7%BD%B8%C8%C4";
            string otherUrl =
                "http://gbf-wiki.com/index.php?%C4%CC%BE%EF%A5%DE%A5%EB%A5%C1%A5%D0%A5%C8%A5%EB_%B5%DF%B1%E7%CA%E7%BD%B8%C8%C4";

            var battles = new List<MultiBattleDefine>();

            battles.Add(new MultiBattleDefine("ミカエル", fourUrl, 100));
            battles.Add(new MultiBattleDefine("ガブリエル", fourUrl, 100));
            battles.Add(new MultiBattleDefine("ウリエル", fourUrl, 100));
            battles.Add(new MultiBattleDefine("ラファエル", fourUrl, 100));
            battles.Add(new MultiBattleDefine("プロバハ", otherUrl, 100));
            battles.Add(new MultiBattleDefine("グランデ", otherUrl, 100));
            battles.Add(new MultiBattleDefine("黄龍", otherUrl, 100));
            battles.Add(new MultiBattleDefine("黒麒麟", otherUrl, 100));
            return battles;
        }

        #endregion

        #region Delegates

        public delegate void DelegateFoundNotify(string matchId, string name);
        private void InvokeFoundNotify(string matchId, string name, string fullText)
        {
            Invoke(new DelegateFoundNotify(FoundNotify), matchId, name);
        }

        private void FoundNotify(string matchId, string name)
        {
            try
            {
                string displayText = $"發現 {name}，ID: {matchId}";
                Clipboard.SetText(matchId);
                PlaySound();
                GBF_notifyIcon.ShowBalloonTip(15000, "大食怪天線", displayText, ToolTipIcon.Info);
                WriteLog(displayText);
            }
            catch (Exception ex)
            {
                WriteLog($"error:{ex.ToString()}");
            }
        }

        public delegate void DelegateWriteLog(string msg);
        private void InvokeWriteLog(string msg)
        {
            Invoke(new DelegateWriteLog(WriteLog), msg);
        }

        private void WriteLog(string message)
        {
            var now = DateTime.Now;
            Console.WriteLine("{0}:{1}:{2}\t{3}", now.ToString("HH"), now.ToString("mm"), now.ToString("ss"), message);
        }

        #endregion

        #region Utlis

        private void ExecuteFinder(List<MultiBattleDefine> selectedBattles, IMultiBattleFinder finder)
        {
            finder.OnBattleFound += InvokeFoundNotify;
            finder.OnWriteLog += InvokeWriteLog;
            finder.Execute(selectedBattles);
            _executeFinders.Add(finder);
        }

        private void PlaySound()
        {
            SoundPlayer player = new SoundPlayer();
            player.SoundLocation = Environment.CurrentDirectory + "\\alert.wav";            
            player.Play();
        }

        public void Start()
        {
            ExecuteFinder();
        }


        public void Stop()
        {
            _executeFinders.ForEach(s =>
            {
                s.Stop();
            });

            _executeFinders.Clear();
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

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            Stop();
            base.OnFormClosed(e);
        }

        #endregion
    }
}
