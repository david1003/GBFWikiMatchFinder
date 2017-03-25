namespace GBFWikeMatchFinderWinApp
{
    partial class Form_GBF
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_GBF));
            this.GBF_notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.GBF_contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.開始ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.停止ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.離開ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clb_Wiki = new System.Windows.Forms.CheckedListBox();
            this.BTN_GO = new System.Windows.Forms.Button();
            this.BTN_STOP = new System.Windows.Forms.Button();
            this.TB_MB = new System.Windows.Forms.TextBox();
            this.clb_Twitter = new System.Windows.Forms.CheckedListBox();
            this.lblWiki = new System.Windows.Forms.Label();
            this.lblTwitter = new System.Windows.Forms.Label();
            this.GBF_contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // GBF_notifyIcon
            // 
            this.GBF_notifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.GBF_notifyIcon.BalloonTipText = "GBF Tool";
            this.GBF_notifyIcon.BalloonTipTitle = "GBF Tool";
            this.GBF_notifyIcon.ContextMenuStrip = this.GBF_contextMenuStrip;
            this.GBF_notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("GBF_notifyIcon.Icon")));
            this.GBF_notifyIcon.Text = "GBF Tool";
            this.GBF_notifyIcon.Visible = true;
            this.GBF_notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Gbf_notifyIcon_MouseDoubleClick);
            // 
            // GBF_contextMenuStrip
            // 
            this.GBF_contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.開始ToolStripMenuItem,
            this.停止ToolStripMenuItem,
            this.離開ToolStripMenuItem});
            this.GBF_contextMenuStrip.Name = "GBF_contextMenuStrip";
            this.GBF_contextMenuStrip.Size = new System.Drawing.Size(99, 70);
            // 
            // 開始ToolStripMenuItem
            // 
            this.開始ToolStripMenuItem.Name = "開始ToolStripMenuItem";
            this.開始ToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.開始ToolStripMenuItem.Text = "開始";
            this.開始ToolStripMenuItem.Click += new System.EventHandler(this.開始ToolStripMenuItem_Click);
            // 
            // 停止ToolStripMenuItem
            // 
            this.停止ToolStripMenuItem.Name = "停止ToolStripMenuItem";
            this.停止ToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.停止ToolStripMenuItem.Text = "停止";
            this.停止ToolStripMenuItem.Click += new System.EventHandler(this.停止ToolStripMenuItem_Click);
            // 
            // 離開ToolStripMenuItem
            // 
            this.離開ToolStripMenuItem.Name = "離開ToolStripMenuItem";
            this.離開ToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.離開ToolStripMenuItem.Text = "離開";
            this.離開ToolStripMenuItem.Click += new System.EventHandler(this.離開ToolStripMenuItem_Click);
            // 
            // clb_Wiki
            // 
            this.clb_Wiki.FormattingEnabled = true;
            this.clb_Wiki.Location = new System.Drawing.Point(2, 25);
            this.clb_Wiki.Name = "clb_Wiki";
            this.clb_Wiki.Size = new System.Drawing.Size(120, 191);
            this.clb_Wiki.TabIndex = 1;
            // 
            // BTN_GO
            // 
            this.BTN_GO.Location = new System.Drawing.Point(285, 25);
            this.BTN_GO.Name = "BTN_GO";
            this.BTN_GO.Size = new System.Drawing.Size(75, 23);
            this.BTN_GO.TabIndex = 2;
            this.BTN_GO.Text = "開始尋找";
            this.BTN_GO.UseVisualStyleBackColor = true;
            this.BTN_GO.Click += new System.EventHandler(this.BTN_GO_Click);
            // 
            // BTN_STOP
            // 
            this.BTN_STOP.Location = new System.Drawing.Point(366, 25);
            this.BTN_STOP.Name = "BTN_STOP";
            this.BTN_STOP.Size = new System.Drawing.Size(75, 23);
            this.BTN_STOP.TabIndex = 3;
            this.BTN_STOP.Text = "停止";
            this.BTN_STOP.UseVisualStyleBackColor = true;
            this.BTN_STOP.Click += new System.EventHandler(this.BTN_STOP_Click);
            // 
            // TB_MB
            // 
            this.TB_MB.Location = new System.Drawing.Point(285, 54);
            this.TB_MB.Multiline = true;
            this.TB_MB.Name = "TB_MB";
            this.TB_MB.Size = new System.Drawing.Size(207, 162);
            this.TB_MB.TabIndex = 5;
            // 
            // clb_Twitter
            // 
            this.clb_Twitter.FormattingEnabled = true;
            this.clb_Twitter.Location = new System.Drawing.Point(142, 25);
            this.clb_Twitter.Name = "clb_Twitter";
            this.clb_Twitter.Size = new System.Drawing.Size(120, 191);
            this.clb_Twitter.TabIndex = 6;
            // 
            // lblWiki
            // 
            this.lblWiki.AutoSize = true;
            this.lblWiki.Location = new System.Drawing.Point(0, 9);
            this.lblWiki.Name = "lblWiki";
            this.lblWiki.Size = new System.Drawing.Size(28, 12);
            this.lblWiki.TabIndex = 7;
            this.lblWiki.Text = "Wiki";
            // 
            // lblTwitter
            // 
            this.lblTwitter.AutoSize = true;
            this.lblTwitter.Location = new System.Drawing.Point(140, 10);
            this.lblTwitter.Name = "lblTwitter";
            this.lblTwitter.Size = new System.Drawing.Size(38, 12);
            this.lblTwitter.TabIndex = 8;
            this.lblTwitter.Text = "Twitter";
            // 
            // Form_GBF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(498, 222);
            this.Controls.Add(this.lblTwitter);
            this.Controls.Add(this.lblWiki);
            this.Controls.Add(this.clb_Twitter);
            this.Controls.Add(this.TB_MB);
            this.Controls.Add(this.BTN_STOP);
            this.Controls.Add(this.BTN_GO);
            this.Controls.Add(this.clb_Wiki);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_GBF";
            this.Text = "大食怪天線";
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.GBF_contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon GBF_notifyIcon;
        private System.Windows.Forms.ContextMenuStrip GBF_contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem 開始ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 停止ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 離開ToolStripMenuItem;
        private System.Windows.Forms.CheckedListBox clb_Wiki;
        private System.Windows.Forms.Button BTN_GO;
        private System.Windows.Forms.Button BTN_STOP;
        private System.Windows.Forms.TextBox TB_MB;
        private System.Windows.Forms.CheckedListBox clb_Twitter;
        private System.Windows.Forms.Label lblWiki;
        private System.Windows.Forms.Label lblTwitter;
    }
}

