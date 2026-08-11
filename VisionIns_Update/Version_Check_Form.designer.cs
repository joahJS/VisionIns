namespace VisionIns_Update
{
    partial class Version_Check_Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Version_Check_Form));
            this.Lb_VerChkMsg = new DevExpress.XtraEditors.LabelControl();
            this.Lb_VerInfo = new DevExpress.XtraEditors.LabelControl();
            this.Bt_Update = new DevExpress.XtraEditors.SimpleButton();
            this.Bt_Close = new DevExpress.XtraEditors.SimpleButton();
            this.SuspendLayout();
            // 
            // Lb_VerChkMsg
            // 
            this.Lb_VerChkMsg.Appearance.ForeColor = System.Drawing.Color.White;
            this.Lb_VerChkMsg.Appearance.Options.UseForeColor = true;
            this.Lb_VerChkMsg.Location = new System.Drawing.Point(164, 114);
            this.Lb_VerChkMsg.Margin = new System.Windows.Forms.Padding(2);
            this.Lb_VerChkMsg.Name = "Lb_VerChkMsg";
            this.Lb_VerChkMsg.Size = new System.Drawing.Size(74, 14);
            this.Lb_VerChkMsg.TabIndex = 1;
            this.Lb_VerChkMsg.Text = "버전체크 메시지";
            // 
            // Lb_VerInfo
            // 
            this.Lb_VerInfo.Appearance.ForeColor = System.Drawing.Color.White;
            this.Lb_VerInfo.Appearance.Options.UseForeColor = true;
            this.Lb_VerInfo.Location = new System.Drawing.Point(164, 156);
            this.Lb_VerInfo.Margin = new System.Windows.Forms.Padding(2);
            this.Lb_VerInfo.Name = "Lb_VerInfo";
            this.Lb_VerInfo.Size = new System.Drawing.Size(40, 14);
            this.Lb_VerInfo.TabIndex = 2;
            this.Lb_VerInfo.Text = "버전정보";
            // 
            // Bt_Update
            // 
            this.Bt_Update.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.Bt_Update.Appearance.BorderColor = System.Drawing.Color.Transparent;
            this.Bt_Update.Appearance.ForeColor = System.Drawing.Color.Transparent;
            this.Bt_Update.Appearance.Options.UseBackColor = true;
            this.Bt_Update.Appearance.Options.UseBorderColor = true;
            this.Bt_Update.Appearance.Options.UseForeColor = true;
            this.Bt_Update.AppearanceHovered.BackColor = System.Drawing.Color.Transparent;
            this.Bt_Update.AppearanceHovered.Options.UseBackColor = true;
            this.Bt_Update.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.Bt_Update.Location = new System.Drawing.Point(228, 190);
            this.Bt_Update.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.Bt_Update.Margin = new System.Windows.Forms.Padding(2);
            this.Bt_Update.Name = "Bt_Update";
            this.Bt_Update.Size = new System.Drawing.Size(76, 25);
            this.Bt_Update.TabIndex = 3;
            this.Bt_Update.Click += new System.EventHandler(this.Bt_Update_Click);
            // 
            // Bt_Close
            // 
            this.Bt_Close.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.Bt_Close.Location = new System.Drawing.Point(320, 190);
            this.Bt_Close.Margin = new System.Windows.Forms.Padding(2);
            this.Bt_Close.Name = "Bt_Close";
            this.Bt_Close.Size = new System.Drawing.Size(71, 25);
            this.Bt_Close.TabIndex = 4;
            this.Bt_Close.Click += new System.EventHandler(this.Bt_Close_Click);
            // 
            // Version_Check_Form
            // 
            this.Appearance.BackColor = System.Drawing.Color.Black;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayoutStore = System.Windows.Forms.ImageLayout.Stretch;
            this.BackgroundImageStore = global::VisionIns_Update.Properties.Resources.update2;
            this.ClientSize = new System.Drawing.Size(464, 283);
            this.Controls.Add(this.Bt_Close);
            this.Controls.Add(this.Bt_Update);
            this.Controls.Add(this.Lb_VerInfo);
            this.Controls.Add(this.Lb_VerChkMsg);
            this.FormBorderEffect = DevExpress.XtraEditors.FormBorderEffect.None;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Version_Check_Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Version_Check_Form";
            this.Load += new System.EventHandler(this.Version_Check_Form_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraEditors.LabelControl Lb_VerChkMsg;
        private DevExpress.XtraEditors.LabelControl Lb_VerInfo;
        private DevExpress.XtraEditors.SimpleButton Bt_Update;
        private DevExpress.XtraEditors.SimpleButton Bt_Close;
    }
}