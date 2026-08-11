using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;

namespace VisionIns
{
    public partial class SY001F00 : DevExpress.XtraEditors.XtraForm
    {
        private string PROCEDURE_ID = "DP_SY001F00";
        private string PROCEDURE_ID_DEL = "DP_SY001F00_DE";
        public string RST_RCDTP; // 리턴 받는 코드값
        public string sReturn = string.Empty;
        public string sReturnDtl = string.Empty;

        public SY001F00()
        {
            InitializeComponent();
        }

        bool isUseOK = false;
        bool isAddOK = false;
        bool isUpdOK = false;
        bool isDelOK = false;
        bool isXlsOK = false;
        bool isPrtOK = false;
        
        private void SY001F00_Load(object sender, EventArgs e)
        {
            //CheckAuthority();
            ComnEtcFunc.gp_SetColorFocused(layoutControl1);
            ComnGridFunc.GridStyleForSelect(GridViewCode);
            ComnGridFunc.GridStyleForSelect(GridViewCodeDtl);
            //ComnTestFunc.SetBoundGridLookUp(RepoUser, "zUSRLST", "USRID", "USRNM");
            //ComnTestFunc.SetBoundGridLookUp(RepoUser2, "zUSRLST", "USRID", "USRNM");
            Cb_Serch.Focus();
            Bt_Retr.PerformClick();
        }
        
        

        private void Bt_Retr_Click(object sender, EventArgs e)
        {
            //if (ComnEtcFunc.GetAuthInfo("USE", this.Name)) return;

            string sSerch = Cb_Serch.SelectedIndex.ToString();
            string sSerchWord = Tx_SerchWord.EditValue?.ToString();
            DataTable dt = GetRefInfo(sSerch, sSerchWord);
            GridCode.DataSource = dt;
            GridViewCode.IndicatorWidth = 40;

            if (!string.IsNullOrEmpty(sReturn))
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["RCDTP"].ToString() == sReturn)
                    {
                        GridViewCode.FocusedRowHandle = i;
                    }
                }
            }

            if (GridViewCode.RowCount > 0)
            {
                GetDetail();
                GridViewCode.Focus();
            }
            else
            {
                Tx_SerchWord.SelectAll();
                Tx_SerchWord.Focus();
            }
        }

        private void InitBound()
        {
            Dictionary<string, string> dicParams = new Dictionary<string, string>();

            //dicParams.Clear();
            //dicParams.Add("CMD", "BOUND");
            //dicParams.Add("REFCD", "CHGTP");
            //DataTable dt = DBConn.GetDataTable(DBConn.dbCon, this.PROCEDURE_ID, dicParams);
            //RepoChgTp.DataSource = dt;
            //RepoChgTp.ValueMember = "CD";
            //RepoChgTp.DisplayMember = "NM";
        }

        private DataTable GetRefInfo(string sFindIdx, string sFindWord)
        {
            // 데이터 테이블
            Dictionary<string, string> dicParams = new Dictionary<string, string>();

            dicParams.Add("CMD", "LIST1");
            dicParams.Add("FIND_IDX", sFindIdx);
            dicParams.Add("FIND_WORD", sFindWord);

            return DBConn.GetDataTable(DBConn.dbCon, PROCEDURE_ID, dicParams);
        }

        private void GetDetail()
        {
            string sRcdtp = GridViewCode.GetFocusedRowCellValue("RCDTP")?.ToString();

            Dictionary<string, string> dicParams = new Dictionary<string, string>();
            dicParams.Add("CMD", "LIST2");
            dicParams.Add("RCDTP", sRcdtp);

            GridCodeDtl.DataSource = ComnEtcFunc.GetInfo(dicParams, this.PROCEDURE_ID);
        }

        private void GridViewCode_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            GetDetail();
        }

        private void Bt_Add_Click(object sender, EventArgs e)
        {
            //if (ComnEtcFunc.GetAuthInfo("ADD", this.Name)) return;

            if (Rg_CodeType.SelectedIndex == 0)
            {
                SY001F02 frm = new SY001F02();
                frm.DataSendEvent += new SY001F02.SendDataHandler(GetData);
                frm.AddModGb = "ADD";
                frm.P_SY005F00 = this;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    Bt_Retr_Click(null, null);
                }
            }
            else if (Rg_CodeType.SelectedIndex == 1)
            {
                string sRcdtp = GridViewCode.GetFocusedRowCellValue("RCDTP")?.ToString();

                SY001F01 frm = new SY001F01();
                frm.DataSendEvent += new SY001F01.SendDataHandler(GetData2);
                frm.AddModGb = "ADD";
                frm.P_SY005F00 = this;
                frm.Rcdtp = sRcdtp;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    Bt_Retr_Click(null, null);
                }
            }
            // REFFPF의 PRTSQ숫자가 항목코드의 갯수와 같아지면 더 이상 못 만듬.
        }

        private void GetData(string sValue) // sValue -> 보내는 것
        {
            Bt_Retr_Click(null, null);
            string sRcdtp = GridViewCode.GetFocusedRowCellValue("RCDTP")?.ToString();
            if (Rg_CodeType.SelectedIndex == 0) // 그룹코드
            {
                GridViewCode.FocusedRowHandle = GridViewCode.LocateByDisplayText(0, GridColRcdtp, sValue);
                GetDtlRef(sRcdtp);
            }
            else if (Rg_CodeType.SelectedIndex == 1)
            {
                GridViewCode.FocusedRowHandle = GridViewCode.LocateByDisplayText(0, GridColDtlRefno, sValue);
                GetDtlRef(sRcdtp);
            }
        }

        private void GetData2(string[] sArrValue) // sValue -> 보내는 것
        {
            Bt_Retr_Click(null, null);
            string sRcdtp = sArrValue[0];
            string sRefNo = sArrValue[1];
            GridViewCodeDtl.FocusedRowHandle = GridViewCodeDtl.LocateByDisplayText(0, GridColDtlRefno, sRefNo);
        }

        private DataTable GetDtlRef(string sRcdTp)
        { // 공통코드 테이블
            Dictionary<string, string> dicParams = new Dictionary<string, string>();

            dicParams.Add("CMD", "LIST2");
            dicParams.Add("FIND_IDX", sRcdTp);

            return DBConn.GetDataTable(DBConn.dbCon, PROCEDURE_ID, dicParams);
        }

        private void Bt_Delete_Click(object sender, EventArgs e)
        {
            //if (ComnEtcFunc.GetAuthInfo("DEL", this.Name)) return;

            string sRCDTP = string.Empty;
            string sRefno = string.Empty;

            if (Rg_CodeType.SelectedIndex == 0)
            {
                sRCDTP = GridViewCode.GetFocusedRowCellValue("RCDTP")?.ToString();

                if (string.IsNullOrEmpty(sRCDTP))
                {
                    XtraMessageBox.Show("삭제하려는 행을 정확히 선택해주세요");
                    return;
                }

                Dictionary<string, string> dicParams = new Dictionary<string, string>();
                dicParams.Add("RCDTP", sRCDTP);

                string sMSG = string.Format(
                    "대분류코드 : {0}" +
                    "\r\n분류명 : {1}" +
                    "\r\n위 사항을 삭제 시 관련 항목코드도 삭제되어집니다." +
                    "\r\n그래도 진행하시겠습니까?"
                    , dicParams["RCDTP"]
                    , GridViewCode.GetFocusedRowCellValue(GridColRefNm));
                if (XtraMessageBox.Show(sMSG, "대표코드 삭제여부", MessageBoxButtons.YesNo) != DialogResult.Yes)
                    return;

                DeleteCommonCode(dicParams, "MASTER");

            }
            else if (Rg_CodeType.SelectedIndex == 1)
            {
                sRCDTP = GridViewCodeDtl.GetFocusedRowCellValue("RCDTP")?.ToString();
                sRefno = GridViewCodeDtl.GetFocusedRowCellValue("REFNO")?.ToString();

                if (!string.IsNullOrEmpty(sRCDTP))
                {

                    if (string.IsNullOrEmpty(sRCDTP))
                    {
                        XtraMessageBox.Show("삭제하려는 행을 정확히 선택해주세요");
                        return;
                    }
                    Dictionary<string, string> dicParams = new Dictionary<string, string>();

                    dicParams.Add("RCDTP", sRCDTP);
                    dicParams.Add("REFNO", GridViewCodeDtl.GetFocusedRowCellValue(GridColRefno)?.ToString());
                    string sMSG = string.Format("대표코드 : {0}\r\n항목코드 : {1}\r\n위 사항을 삭제하시겠습니까?", dicParams["RCDTP"], dicParams["REFNO"]);

                    if (XtraMessageBox.Show(sMSG, "항목코드 삭제여부", MessageBoxButtons.YesNo) != DialogResult.Yes)
                        return;

                    DeleteCommonCode(dicParams, "DETAIL");
                }
                else
                {
                    GridViewCodeDtl.DeleteRow(GridViewCodeDtl.FocusedRowHandle);
                    GridViewCodeDtl.UpdateCurrentRow();
                }
            }
            int iFocused = GridViewCode.FocusedRowHandle;

        }

        private void DeleteCommonCode(Dictionary<string, string> dicParams, string sGB)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                if (sGB.Equals("MASTER"))
                {
                    dicParams.Add("CMD", "MASTER");
                }
                else if (sGB.Equals("DETAIL"))
                {
                    dicParams.Add("CMD", "DETAIL");
                }

                string sRCDTP = dicParams["RCDTP"];
                int i = GridViewCode.GetFocusedDataSourceRowIndex();
                DataTable dt = DBConn.GetDataTable(DBConn.dbCon, this.PROCEDURE_ID_DEL, dicParams);
                if (dt.Rows.Count > 0)
                {
                    string sScsGb = dt.Rows[0]["RESULT"]?.ToString();
                    string sMsg = dt.Rows[0]["MSG"]?.ToString();
                    XtraMessageBox.Show(sMsg);
                    if (sScsGb.Equals("1"))
                    {
                        //ComnEtcFunc.GetLog("DEL", ComnEtcFunc.EXS_ID, ComnEtcFunc.Client_IP, Name, Text);
                        Bt_Retr.PerformClick();
                        if (i > 0)
                        {
                            GridViewCode.MoveBy(i - 1);
                        }
                        else
                        {
                            GridViewCode.MoveBy(0);
                        }
                    }
                    else
                    {
                        return;
                    }
                }

                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                XtraMessageBox.Show(ex.ToString());
            }
        }
        private void Bt_Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void GridViewCode_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            GetDetail();
            Rg_CodeType.SelectedIndex = 0;

            if (e.Clicks == 2)
            {
                //if (ComnEtcFunc.GetAuthInfo("UPD", this.Name)) return;

                string sRcdtp = GridViewCode.GetFocusedRowCellValue("RCDTP")?.ToString();

                SY001F02 frm = new SY001F02();
                frm.Rcdtp = sRcdtp;
                frm.P_SY005F00 = this;
                frm.AddModGb = "MOD";
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    Bt_Retr_Click(null, null);
                }
            }
        }

        private void GridViewCodeDtl_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            Rg_CodeType.SelectedIndex = 1;

            if (e.Clicks == 2)
            {
                //if (ComnEtcFunc.GetAuthInfo("UPD", this.Name)) return;

                string sRcdtp = GridViewCodeDtl.GetFocusedRowCellValue("RCDTP")?.ToString();
                string sRefno = GridViewCodeDtl.GetFocusedRowCellValue("REFNO")?.ToString();
                string sRefnm = GridViewCodeDtl.GetFocusedRowCellValue("REFNM")?.ToString();

                SY001F01 frm = new SY001F01();
                frm.DataSendEvent += new SY001F01.SendDataHandler(GetData2);
                frm.Rcdtp = sRcdtp;
                frm.Refno = sRefno;
                frm.Refnm = sRefnm;
                frm.P_SY005F00 = this;
                frm.AddModGb = "MOD";
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    GetDetail();
                }
            }
        }

        private void SY001F00_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                Bt_Retr.PerformClick();
            }
            else if (e.KeyCode == Keys.F1)
            {
                Bt_Add.PerformClick();
            }
            else if (e.KeyCode == Keys.F4)
            {
                Bt_Delete.PerformClick();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Bt_Close.PerformClick();
            }
        }
        
        private void Tx_SerchWord_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Bt_Retr.PerformClick();
            }
        }
    }
}