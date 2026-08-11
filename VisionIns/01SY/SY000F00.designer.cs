namespace VisionIns
{
    partial class SY000F00
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SY000F00));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn_VERSION_ID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn_UPLOAD_DT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn_FILE_NAME = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn_FILE_BYTE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemN0 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.gridColumn_VERSION_RMK = new DevExpress.XtraGrid.Columns.GridColumn();
            this.bt_Close = new DevExpress.XtraEditors.SimpleButton();
            this.bt_Add = new DevExpress.XtraEditors.SimpleButton();
            this.bt_Retr = new DevExpress.XtraEditors.SimpleButton();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemN0)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gridControl1);
            this.layoutControl1.Controls.Add(this.bt_Close);
            this.layoutControl1.Controls.Add(this.bt_Add);
            this.layoutControl1.Controls.Add(this.bt_Retr);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1590, 868);
            this.layoutControl1.TabIndex = 2;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gridControl1
            // 
            this.gridControl1.Location = new System.Drawing.Point(24, 75);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemN0});
            this.gridControl1.Size = new System.Drawing.Size(1542, 769);
            this.gridControl1.TabIndex = 9;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn_VERSION_ID,
            this.gridColumn_UPLOAD_DT,
            this.gridColumn_FILE_NAME,
            this.gridColumn_FILE_BYTE,
            this.gridColumn_VERSION_RMK});
            this.gridView1.DetailHeight = 288;
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            // 
            // gridColumn_VERSION_ID
            // 
            this.gridColumn_VERSION_ID.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn_VERSION_ID.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn_VERSION_ID.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn_VERSION_ID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn_VERSION_ID.Caption = "버전";
            this.gridColumn_VERSION_ID.FieldName = "VERSION_ID";
            this.gridColumn_VERSION_ID.Name = "gridColumn_VERSION_ID";
            this.gridColumn_VERSION_ID.OptionsColumn.AllowEdit = false;
            this.gridColumn_VERSION_ID.Visible = true;
            this.gridColumn_VERSION_ID.VisibleIndex = 0;
            this.gridColumn_VERSION_ID.Width = 100;
            // 
            // gridColumn_UPLOAD_DT
            // 
            this.gridColumn_UPLOAD_DT.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn_UPLOAD_DT.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn_UPLOAD_DT.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn_UPLOAD_DT.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn_UPLOAD_DT.Caption = "업로드일자";
            this.gridColumn_UPLOAD_DT.FieldName = "UPLOAD_DT";
            this.gridColumn_UPLOAD_DT.Name = "gridColumn_UPLOAD_DT";
            this.gridColumn_UPLOAD_DT.OptionsColumn.AllowEdit = false;
            this.gridColumn_UPLOAD_DT.Width = 100;
            // 
            // gridColumn_FILE_NAME
            // 
            this.gridColumn_FILE_NAME.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn_FILE_NAME.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn_FILE_NAME.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn_FILE_NAME.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn_FILE_NAME.Caption = "파일이름";
            this.gridColumn_FILE_NAME.FieldName = "FILE_NAME";
            this.gridColumn_FILE_NAME.Name = "gridColumn_FILE_NAME";
            this.gridColumn_FILE_NAME.OptionsColumn.AllowEdit = false;
            this.gridColumn_FILE_NAME.Visible = true;
            this.gridColumn_FILE_NAME.VisibleIndex = 1;
            this.gridColumn_FILE_NAME.Width = 100;
            // 
            // gridColumn_FILE_BYTE
            // 
            this.gridColumn_FILE_BYTE.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn_FILE_BYTE.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn_FILE_BYTE.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn_FILE_BYTE.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn_FILE_BYTE.Caption = "파일크기";
            this.gridColumn_FILE_BYTE.ColumnEdit = this.repositoryItemN0;
            this.gridColumn_FILE_BYTE.FieldName = "FILE_BYTE";
            this.gridColumn_FILE_BYTE.Name = "gridColumn_FILE_BYTE";
            this.gridColumn_FILE_BYTE.OptionsColumn.AllowEdit = false;
            this.gridColumn_FILE_BYTE.Width = 100;
            // 
            // repositoryItemN0
            // 
            this.repositoryItemN0.AutoHeight = false;
            this.repositoryItemN0.Mask.EditMask = "n0";
            this.repositoryItemN0.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.repositoryItemN0.Mask.UseMaskAsDisplayFormat = true;
            this.repositoryItemN0.Name = "repositoryItemN0";
            // 
            // gridColumn_VERSION_RMK
            // 
            this.gridColumn_VERSION_RMK.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn_VERSION_RMK.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn_VERSION_RMK.Caption = "비고";
            this.gridColumn_VERSION_RMK.FieldName = "VERSION_RMK";
            this.gridColumn_VERSION_RMK.Name = "gridColumn_VERSION_RMK";
            this.gridColumn_VERSION_RMK.OptionsColumn.AllowEdit = false;
            this.gridColumn_VERSION_RMK.Visible = true;
            this.gridColumn_VERSION_RMK.VisibleIndex = 2;
            this.gridColumn_VERSION_RMK.Width = 1000;
            // 
            // bt_Close
            // 
            this.bt_Close.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bt_Close.ImageOptions.Image")));
            this.bt_Close.Location = new System.Drawing.Point(1493, 12);
            this.bt_Close.Name = "bt_Close";
            this.bt_Close.Size = new System.Drawing.Size(85, 28);
            this.bt_Close.StyleController = this.layoutControl1;
            this.bt_Close.TabIndex = 4;
            this.bt_Close.Text = "닫기(ESC)";
            this.bt_Close.Click += new System.EventHandler(this.Bt_Close_Click);
            // 
            // bt_Add
            // 
            this.bt_Add.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bt_Add.ImageOptions.Image")));
            this.bt_Add.Location = new System.Drawing.Point(1404, 12);
            this.bt_Add.Name = "bt_Add";
            this.bt_Add.Size = new System.Drawing.Size(85, 28);
            this.bt_Add.StyleController = this.layoutControl1;
            this.bt_Add.TabIndex = 5;
            this.bt_Add.Text = "추가(F1)";
            this.bt_Add.Click += new System.EventHandler(this.Bt_Add_Click);
            // 
            // bt_Retr
            // 
            this.bt_Retr.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bt_Retr.ImageOptions.Image")));
            this.bt_Retr.Location = new System.Drawing.Point(1315, 12);
            this.bt_Retr.Name = "bt_Retr";
            this.bt_Retr.Size = new System.Drawing.Size(85, 28);
            this.bt_Retr.StyleController = this.layoutControl1;
            this.bt_Retr.TabIndex = 6;
            this.bt_Retr.Text = "조회(F5)";
            this.bt_Retr.Click += new System.EventHandler(this.Bt_Retr_Click);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup2,
            this.emptySpaceItem2,
            this.layoutControlItem3,
            this.layoutControlItem2,
            this.layoutControlItem1});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(1590, 868);
            this.Root.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem6});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 32);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Size = new System.Drawing.Size(1570, 816);
            this.layoutControlGroup2.Text = "버전리스트";
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.gridControl1;
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(1546, 773);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(1303, 32);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.bt_Retr;
            this.layoutControlItem3.Location = new System.Drawing.Point(1303, 0);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(89, 32);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(89, 32);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(89, 32);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.bt_Add;
            this.layoutControlItem2.Location = new System.Drawing.Point(1392, 0);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(89, 32);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(89, 32);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(89, 32);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.bt_Close;
            this.layoutControlItem1.Location = new System.Drawing.Point(1481, 0);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(89, 32);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(89, 32);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(89, 32);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // SY000F00
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1590, 868);
            this.Controls.Add(this.layoutControl1);
            this.KeyPreview = true;
            this.Name = "SY000F00";
            this.Text = "버전관리";
            this.Load += new System.EventHandler(this.SY000F00_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SY000F00_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemN0)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_VERSION_ID;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_UPLOAD_DT;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_FILE_NAME;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_FILE_BYTE;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemN0;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_VERSION_RMK;
        private DevExpress.XtraEditors.SimpleButton bt_Close;
        private DevExpress.XtraEditors.SimpleButton bt_Add;
        private DevExpress.XtraEditors.SimpleButton bt_Retr;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
    }
}