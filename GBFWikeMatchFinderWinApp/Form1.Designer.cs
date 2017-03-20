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
            this.CLB_List = new System.Windows.Forms.CheckedListBox();
            this.BTN_GO = new System.Windows.Forms.Button();
            this.BTN_STOP = new System.Windows.Forms.Button();
            this.lb_status = new System.Windows.Forms.Label();
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
            this.GBF_notifyIcon.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Gbf_notifyIcon_MouseMove);
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
            // CLB_List
            // 
            this.CLB_List.FormattingEnabled = true;
            this.CLB_List.Items.AddRange(new object[] {
            "ミカエル",
            "ガブリエル",
            "ウリエル",
            "ラファエル"});
            this.CLB_List.Location = new System.Drawing.Point(12, 12);
            this.CLB_List.Name = "CLB_List";
            this.CLB_List.Size = new System.Drawing.Size(120, 89);
            this.CLB_List.TabIndex = 1;
            // 
            // BTN_GO
            // 
            this.BTN_GO.Location = new System.Drawing.Point(170, 78);
            this.BTN_GO.Name = "BTN_GO";
            this.BTN_GO.Size = new System.Drawing.Size(75, 23);
            this.BTN_GO.TabIndex = 2;
            this.BTN_GO.Text = "開始尋找";
            this.BTN_GO.UseVisualStyleBackColor = true;
            this.BTN_GO.Click += new System.EventHandler(this.BTN_GO_Click);
            // 
            // BTN_STOP
            // 
            this.BTN_STOP.Location = new System.Drawing.Point(170, 108);
            this.BTN_STOP.Name = "BTN_STOP";
            this.BTN_STOP.Size = new System.Drawing.Size(75, 23);
            this.BTN_STOP.TabIndex = 3;
            this.BTN_STOP.Text = "停止";
            this.BTN_STOP.UseVisualStyleBackColor = true;
            this.BTN_STOP.Click += new System.EventHandler(this.BTN_STOP_Click);
            // 
            // lb_status
            // 
            this.lb_status.AutoSize = true;
            this.lb_status.Location = new System.Drawing.Point(170, 13);
            this.lb_status.Name = "lb_status";
            this.lb_status.Size = new System.Drawing.Size(41, 12);
            this.lb_status.TabIndex = 4;
            this.lb_status.Text = "停止中";
            // 
            // Form_GBF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.lb_status);
            this.Controls.Add(this.BTN_STOP);
            this.Controls.Add(this.BTN_GO);
            this.Controls.Add(this.CLB_List);
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
        private System.Windows.Forms.CheckedListBox CLB_List;
        private System.Windows.Forms.Button BTN_GO;
        private System.Windows.Forms.Button BTN_STOP;
        private System.Windows.Forms.Label lb_status;
    }
}

