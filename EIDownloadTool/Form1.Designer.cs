namespace EIDownloadTool
{
    partial class Form1
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
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.textbox_MainURL = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnGetPages = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.StatusNow = new System.Windows.Forms.ToolStripStatusLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.HackListView = new System.Windows.Forms.ListView();
            this.L_ImagePages = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.L_ImageURL = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.L_Type = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ProcessBarState = new System.Windows.Forms.ProgressBar();
            this.label_Page = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label5 = new System.Windows.Forms.Label();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textbox_MainURL
            // 
            this.textbox_MainURL.Location = new System.Drawing.Point(100, 12);
            this.textbox_MainURL.Name = "textbox_MainURL";
            this.textbox_MainURL.Size = new System.Drawing.Size(292, 22);
            this.textbox_MainURL.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "請輸入主網址";
            // 
            // btnGetPages
            // 
            this.btnGetPages.Location = new System.Drawing.Point(398, 11);
            this.btnGetPages.Name = "btnGetPages";
            this.btnGetPages.Size = new System.Drawing.Size(104, 23);
            this.btnGetPages.TabIndex = 2;
            this.btnGetPages.Text = "開始下載";
            this.btnGetPages.UseVisualStyleBackColor = true;
            this.btnGetPages.Click += new System.EventHandler(this.btnGetPages_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusNow});
            this.statusStrip1.Location = new System.Drawing.Point(0, 259);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(569, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // StatusNow
            // 
            this.StatusNow.Name = "StatusNow";
            this.StatusNow.Size = new System.Drawing.Size(44, 17);
            this.StatusNow.Text = "無動作";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 12);
            this.label2.TabIndex = 4;
            // 
            // HackListView
            // 
            this.HackListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.HackListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.L_ImagePages,
            this.L_ImageURL,
            this.L_Type});
            this.HackListView.Location = new System.Drawing.Point(20, 87);
            this.HackListView.MultiSelect = false;
            this.HackListView.Name = "HackListView";
            this.HackListView.Size = new System.Drawing.Size(522, 140);
            this.HackListView.TabIndex = 5;
            this.HackListView.Tag = "taskmgr.exe";
            this.HackListView.UseCompatibleStateImageBehavior = false;
            this.HackListView.View = System.Windows.Forms.View.Details;
            // 
            // L_ImagePages
            // 
            this.L_ImagePages.Text = "ImagePages";
            this.L_ImagePages.Width = 151;
            // 
            // L_ImageURL
            // 
            this.L_ImageURL.Text = "ImageURL";
            this.L_ImageURL.Width = 201;
            // 
            // L_Type
            // 
            this.L_Type.Text = "Type";
            // 
            // ProcessBarState
            // 
            this.ProcessBarState.Location = new System.Drawing.Point(25, 61);
            this.ProcessBarState.Name = "ProcessBarState";
            this.ProcessBarState.Size = new System.Drawing.Size(517, 18);
            this.ProcessBarState.TabIndex = 6;
            // 
            // label_Page
            // 
            this.label_Page.AutoSize = true;
            this.label_Page.Location = new System.Drawing.Point(294, 71);
            this.label_Page.Name = "label_Page";
            this.label_Page.Size = new System.Drawing.Size(0, 12);
            this.label_Page.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Brown;
            this.label3.Location = new System.Drawing.Point(248, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(294, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "Please Input Like : http://g.e-hentai.org/g/592133/123743543/";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 238);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(160, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "Get More Search: never-nop.com";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(463, 238);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(83, 12);
            this.linkLabel1.TabIndex = 11;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Powered By UM";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Brown;
            this.label5.Location = new System.Drawing.Point(266, 238);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(193, 12);
            this.label5.TabIndex = 12;
            this.label5.Text = "[註]標題長度過長將以日期命名目錄";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(569, 281);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label_Page);
            this.Controls.Add(this.ProcessBarState);
            this.Controls.Add(this.HackListView);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnGetPages);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textbox_MainURL);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "E-hentai Download Tool  Powered By UM Ver.05";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textbox_MainURL;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGetPages;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel StatusNow;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ColumnHeader L_ImagePages;
        private System.Windows.Forms.ColumnHeader L_ImageURL;
        public System.Windows.Forms.ListView HackListView;
        private System.Windows.Forms.ProgressBar ProcessBarState;
        private System.Windows.Forms.ColumnHeader L_Type;
        private System.Windows.Forms.Label label_Page;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label label5;
    }
}

