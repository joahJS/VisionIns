using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using static ComnFunc;
using static GridFunc;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.BandedGrid;
using System.Drawing;
using System.IO;

namespace VisionIns
{
    public partial class MT001F00 : DevExpress.XtraEditors.XtraForm
    {
        private string PROCEDURE_ID = "DP_MT001F00";
        private int _RefreshTimer = 0;

        public MT001F00()
        {
            InitializeComponent();

            gridView1.RowCellStyle += SetResultCellStyle;
            gridView2.RowCellStyle += SetResultCellStyle;

            Lb_CDATE.Text = DateTime.Now.ToString("yyyy-MM-dd");
            Lb_CTIME.Text = DateTime.Now.ToString("HH:mm:ss");

            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;

            this.FormBorderEffect = DevExpress.XtraEditors.FormBorderEffect.None;
            DevExpress.XtraEditors.WindowsFormsSettings.FormThickBorder = false;

        }

        //전체화면 esc로 닫기
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.Close();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void pnlHeader_Paint(object sender, PaintEventArgs e)
        {

        }

        private void MT001F00_Load(object sender, EventArgs e)
        {
            timer1.Start();
            Retr();
        }

        //테스트용
        private void Retr()
        {
            Dictionary<string, string> dicParams = new Dictionary<string, string>();
            dicParams.Clear();
            dicParams.Add("CMD", "CNT");

            DataTable dt_CNT = DBConn.GetDataTable(DBConn.dbCon, this.PROCEDURE_ID, dicParams);

            inspectSumItem1Text.Text = dt_CNT.Rows[0]["TOTAL_CNT"]?.ToString();
            labelControl3.Text = dt_CNT.Rows[0]["OK_CNT"]?.ToString();
            inspectSumItem3Header.Text = dt_CNT.Rows[0]["NG_CNT"]?.ToString();
            inspectSumItem4Header.Text = dt_CNT.Rows[0]["NG_RATE"]?.ToString();


            dicParams.Clear();
            dicParams.Add("CMD", "LIST");

            DataTable dt = DBConn.GetDataTable(DBConn.dbCon, this.PROCEDURE_ID, dicParams);

            if (dt != null) 
            {
                if (dt.Rows.Count > 0)
                {
                    string result = dt.Rows[0]["RSLT"].ToString();

                    pnlResultText.Text = result;

                    if (result == "OK")
                    {
                        pnlResultText.ForeColor = ColorTranslator.FromHtml("#5ED845");
                        pnlResultImg.Image = Properties.Resources.circle_check_solid_full;
                    }
                    else if (result == "NG")
                    {
                        pnlResultText.ForeColor = ColorTranslator.FromHtml("#FF4D45");
                        pnlResultImg.Image = Properties.Resources.circle_xmark_solid_full;
                    }

                    DataTable dtClone = dt.Clone();
                    dtClone.ImportRow(dt.Rows[0]);
                    gcWorkInfoTb.DataSource = dtClone;

                    byte[] imgBytes = dtClone.Rows[0]["IIMG"] as byte[];

                    if (imgBytes != null && imgBytes.Length > 0)
                    {
                        using (MemoryStream ms = new MemoryStream(imgBytes))
                        using (Image img = Image.FromStream(ms))
                        {
                            picLiveImage.Image = new Bitmap(img);
                        }
                    }
                    else
                    {
                        picLiveImage.Image = null;
                    }

                    gcRecentHistoryTb.DataSource = dt;
                }
            }
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            Lb_CDATE.Text = DateTime.Now.ToString("yyyy-MM-dd");
            Lb_CTIME.Text = DateTime.Now.ToString("HH:mm:ss");

            _RefreshTimer++;
            if (_RefreshTimer > 10)
            {
                Retr();
                _RefreshTimer = 0;
            }
        }

        private void inspectSumList_Paint(object sender, PaintEventArgs e)
        {

        }

        private void inspectSumText3_Click(object sender, EventArgs e)
        {

        }

        private void labelControl3_Click(object sender, EventArgs e)
        {

        }

        private void gcWorkInfoTb_Click(object sender, EventArgs e)
        {

        }

        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Clicks == 2)
            {
                int rowHandle = e.RowHandle;

                if (rowHandle < 0)
                    return;

                DataRow row = gridView1.GetDataRow(e.RowHandle);

                if (row == null)
                    return;

                string inspNo = gridView1.GetFocusedRowCellValue("SLINO")?.ToString();

                if (string.IsNullOrEmpty(inspNo))
                    return;

                MT001F01 frm = new MT001F01(row);
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.ShowDialog(this);
            }
        }

        private void gridView2_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Clicks == 2)
            {
                int rowHandle = e.RowHandle;

                if (rowHandle < 0)
                    return;

                DataRow row = gridView2.GetDataRow(e.RowHandle);

                if (row == null)
                    return;

                string inspNo = gridView2.GetFocusedRowCellValue("SLINO")?.ToString();

                if (string.IsNullOrEmpty(inspNo))
                    return;

                MT001F01 frm = new MT001F01(row);
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.ShowDialog(this);
            }
        }

        private void gcWorkInfoTb_Click_1(object sender, EventArgs e)
        {

        }


        //검사결과 열이 OK냐 NG냐에 따라 폰트색상 변환
        private void SetResultCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            // 검사결과 컬럼만 적용
            if (e.Column.FieldName != "RSLT")
                return;

            string result = e.CellValue?.ToString().Trim().ToUpper();

            if (result == "OK")
            {
                e.Appearance.ForeColor = ColorTranslator.FromHtml("#5ED845");
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
            }
            else if (result == "NG")
            {
                e.Appearance.ForeColor = ColorTranslator.FromHtml("#FF4D45");
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
