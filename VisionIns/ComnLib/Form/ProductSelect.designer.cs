namespace VisionIns
{
    partial class ProductSelect
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
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.CboSearch = new DevExpress.XtraEditors.ComboBoxEdit();
            this.TxtSearch = new DevExpress.XtraEditors.TextEdit();
            this.BtnRetr = new DevExpress.XtraEditors.SimpleButton();
            this.BtnChoice = new DevExpress.XtraEditors.SimpleButton();
            this.BtnClose = new DevExpress.XtraEditors.SimpleButton();
            this.GridProduct = new DevExpress.XtraGrid.GridControl();
            this.GridViewProduct = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.GridColITCOD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.GridColITNAM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.GridColISPEC = new DevExpress.XtraGrid.Columns.GridColumn();
            this.GridColUseyn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.RepoChkUseyn = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CboSearch.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtSearch.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridProduct)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridViewProduct)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RepoChkUseyn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            this.SuspendLayout();
            //
            // layoutControl1
            //
            this.layoutControl1.Controls.Add(this.CboSearch);
            this.layoutControl1.Controls.Add(this.TxtSearch);
            this.layoutControl1.Controls.Add(this.BtnRetr);
            this.layoutControl1.Controls.Add(this.BtnChoice);
            this.layoutControl1.Controls.Add(this.BtnClose);
            this.layoutControl1.Controls.Add(this.GridProduct);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(760, 560);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            //
            // CboSearch
            //
            this.CboSearch.Location = new System.Drawing.Point(12, 12);
            this.CboSearch.Name = "CboSearch";
            this.CboSearch.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.CboSearch.Properties.Items.AddRange(new object[] {
            "품번",
            "품명",
            "규격"});
            this.CboSearch.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.CboSearch.Size = new System.Drawing.Size(129, 20);
            this.CboSearch.StyleController = this.layoutControl1;
            this.CboSearch.TabIndex = 4;
            this.CboSearch.EditValue = "품명";
            //
            // TxtSearch
            //
            this.TxtSearch.Location = new System.Drawing.Point(153, 12);
            this.TxtSearch.Name = "TxtSearch";
            this.TxtSearch.Size = new System.Drawing.Size(120, 20);
            this.TxtSearch.StyleController = this.layoutControl1;
            this.TxtSearch.TabIndex = 5;
            this.TxtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtSearch_KeyDown);
            //
            // BtnRetr
            //
            this.BtnRetr.Location = new System.Drawing.Point(514, 12);
            this.BtnRetr.Name = "BtnRetr";
            this.BtnRetr.Size = new System.Drawing.Size(76, 22);
            this.BtnRetr.StyleController = this.layoutControl1;
            this.BtnRetr.TabIndex = 6;
            this.BtnRetr.Text = "조회(F5)";
            this.BtnRetr.Click += new System.EventHandler(this.BtnRetr_Click);
            //
            // BtnChoice
            //
            this.BtnChoice.Location = new System.Drawing.Point(594, 12);
            this.BtnChoice.Name = "BtnChoice";
            this.BtnChoice.Size = new System.Drawing.Size(76, 22);
            this.BtnChoice.StyleController = this.layoutControl1;
            this.BtnChoice.TabIndex = 7;
            this.BtnChoice.Text = "선택(F3)";
            this.BtnChoice.Click += new System.EventHandler(this.BtnChoice_Click);
            //
            // BtnClose
            //
            this.BtnClose.Location = new System.Drawing.Point(674, 12);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(74, 22);
            this.BtnClose.StyleController = this.layoutControl1;
            this.BtnClose.TabIndex = 8;
            this.BtnClose.Text = "닫기(ESC)";
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            //
            // GridProduct
            //
            this.GridProduct.Location = new System.Drawing.Point(12, 60);
            this.GridProduct.MainView = this.GridViewProduct;
            this.GridProduct.Name = "GridProduct";
            this.GridProduct.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.RepoChkUseyn});
            this.GridProduct.Size = new System.Drawing.Size(736, 488);
            this.GridProduct.TabIndex = 9;
            this.GridProduct.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GridViewProduct});
            //
            // GridViewProduct
            //
            this.GridViewProduct.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.GridViewProduct.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GridViewProduct.ColumnPanelRowHeight = 30;
            this.GridViewProduct.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.GridColITCOD,
            this.GridColITNAM,
            this.GridColISPEC,
            this.GridColUseyn});
            this.GridViewProduct.GridControl = this.GridProduct;
            this.GridViewProduct.IndicatorWidth = 40;
            this.GridViewProduct.Name = "GridViewProduct";
            this.GridViewProduct.OptionsBehavior.Editable = false;
            this.GridViewProduct.OptionsView.ColumnAutoWidth = false;
            this.GridViewProduct.OptionsView.ShowGroupPanel = false;
            this.GridViewProduct.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.GridViewProduct_RowClick);
            this.GridViewProduct.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GridViewProduct_KeyDown);
            //
            // GridColITCOD
            //
            this.GridColITCOD.AppearanceCell.Options.UseTextOptions = true;
            this.GridColITCOD.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GridColITCOD.AppearanceHeader.Options.UseTextOptions = true;
            this.GridColITCOD.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GridColITCOD.Caption = "품번";
            this.GridColITCOD.FieldName = "ITCOD";
            this.GridColITCOD.Name = "GridColITCOD";
            this.GridColITCOD.OptionsColumn.AllowEdit = false;
            this.GridColITCOD.Visible = true;
            this.GridColITCOD.VisibleIndex = 0;
            this.GridColITCOD.Width = 100;
            //
            // GridColITNAM
            //
            this.GridColITNAM.AppearanceHeader.Options.UseTextOptions = true;
            this.GridColITNAM.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GridColITNAM.Caption = "품명";
            this.GridColITNAM.FieldName = "ITNAM";
            this.GridColITNAM.Name = "GridColITNAM";
            this.GridColITNAM.OptionsColumn.AllowEdit = false;
            this.GridColITNAM.Visible = true;
            this.GridColITNAM.VisibleIndex = 1;
            this.GridColITNAM.Width = 260;
            //
            // GridColISPEC
            //
            this.GridColISPEC.AppearanceHeader.Options.UseTextOptions = true;
            this.GridColISPEC.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GridColISPEC.Caption = "규격";
            this.GridColISPEC.FieldName = "ISPEC";
            this.GridColISPEC.Name = "GridColISPEC";
            this.GridColISPEC.OptionsColumn.AllowEdit = false;
            this.GridColISPEC.Visible = true;
            this.GridColISPEC.VisibleIndex = 2;
            this.GridColISPEC.Width = 240;
            //
            // GridColUseyn
            //
            this.GridColUseyn.AppearanceCell.Options.UseTextOptions = true;
            this.GridColUseyn.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GridColUseyn.AppearanceHeader.Options.UseTextOptions = true;
            this.GridColUseyn.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GridColUseyn.Caption = "사용여부";
            this.GridColUseyn.ColumnEdit = this.RepoChkUseyn;
            this.GridColUseyn.FieldName = "USEYN";
            this.GridColUseyn.Name = "GridColUseyn";
            this.GridColUseyn.OptionsColumn.AllowEdit = false;
            this.GridColUseyn.Visible = true;
            this.GridColUseyn.VisibleIndex = 3;
            this.GridColUseyn.Width = 80;
            //
            // RepoChkUseyn
            //
            this.RepoChkUseyn.AutoHeight = false;
            this.RepoChkUseyn.Name = "RepoChkUseyn";
            this.RepoChkUseyn.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.RepoChkUseyn.ValueChecked = "Y";
            this.RepoChkUseyn.ValueUnchecked = "N";
            //
            // Root
            //
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup1,
            this.layoutControlGroup2});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(760, 560);
            this.Root.TextVisible = false;
            //
            // layoutControlGroup1
            //
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.emptySpaceItem1,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(742, 48);
            this.layoutControlGroup1.Text = "검색조건";
            //
            // layoutControlItem1
            //
            this.layoutControlItem1.Control = this.CboSearch;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(141, 26);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            //
            // layoutControlItem2
            //
            this.layoutControlItem2.Control = this.TxtSearch;
            this.layoutControlItem2.Location = new System.Drawing.Point(141, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(132, 26);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            //
            // emptySpaceItem1
            //
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(273, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(229, 26);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            //
            // layoutControlItem3
            //
            this.layoutControlItem3.Control = this.BtnRetr;
            this.layoutControlItem3.Location = new System.Drawing.Point(502, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(80, 26);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            //
            // layoutControlItem4
            //
            this.layoutControlItem4.Control = this.BtnChoice;
            this.layoutControlItem4.Location = new System.Drawing.Point(582, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(80, 26);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            //
            // layoutControlItem5
            //
            this.layoutControlItem5.Control = this.BtnClose;
            this.layoutControlItem5.Location = new System.Drawing.Point(662, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(80, 26);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            //
            // layoutControlGroup2
            //
            this.layoutControlGroup2.GroupBordersVisible = false;
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem6});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 48);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Size = new System.Drawing.Size(742, 492);
            this.layoutControlGroup2.Text = "품목리스트";
            //
            // layoutControlItem6
            //
            this.layoutControlItem6.Control = this.GridProduct;
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(722, 472);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            //
            // ProductSelect
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(760, 560);
            this.Controls.Add(this.layoutControl1);
            this.KeyPreview = true;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.Name = "ProductSelect";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "품목검색";
            this.Load += new System.EventHandler(this.ProductSelect_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ProductSelect_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.CboSearch.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtSearch.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridProduct)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridViewProduct)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RepoChkUseyn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.ComboBoxEdit CboSearch;
        private DevExpress.XtraEditors.TextEdit TxtSearch;
        private DevExpress.XtraEditors.SimpleButton BtnRetr;
        private DevExpress.XtraEditors.SimpleButton BtnChoice;
        private DevExpress.XtraEditors.SimpleButton BtnClose;
        private DevExpress.XtraGrid.GridControl GridProduct;
        private DevExpress.XtraGrid.Views.Grid.GridView GridViewProduct;
        private DevExpress.XtraGrid.Columns.GridColumn GridColITCOD;
        private DevExpress.XtraGrid.Columns.GridColumn GridColITNAM;
        private DevExpress.XtraGrid.Columns.GridColumn GridColISPEC;
        private DevExpress.XtraGrid.Columns.GridColumn GridColUseyn;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit RepoChkUseyn;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
    }
}
