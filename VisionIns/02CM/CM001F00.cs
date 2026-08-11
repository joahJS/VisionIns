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
using System.Data.SqlClient;

namespace VisionIns
{
    public partial class CM001F00 : DevExpress.XtraEditors.XtraForm
    {
        private string PROCEDURE_ID = "DP_CM001F00";
        public CM001F00()
        {
            InitializeComponent();
        }

        #region [화면 로드]
        private void Form_Load(object sender, EventArgs e)
        {
            Init();
            this.ActiveControl = CboSearch;
            CboSearch.Focus();
        }
        private void Init()
        {
            //ComnEtcFunc.SetFrom(this);
            ComnEtcFunc.InitControllerRule(layoutControl1);
            ComnGridFunc.GridStyleBasicSetting(GridViewRetr);
            BtnRetr.PerformClick();
        }
        #endregion

        public void RETR()
        {
            //if (ComnEtcFunc.GetAuthInfo("USE", this.Name)) return;
            GetInfo();
        }

        public void ADD()
        {
            //if (ComnEtcFunc.GetAuthInfo("ADD", this.Name)) return;

            CM001F01 frm = new CM001F01();
            frm.Owner = this;
            frm._ADDMOD = CM001F01.ADDMOD.ADD;
            frm.DataRowSendEvent += new CM001F01.SendDataHandler(GetDataRow);
            frm.Show();
        }
       
        public void DELETE()
        {
            //if (ComnEtcFunc.GetAuthInfo("DEL", this.Name)) return;

            string sITCOD = GridViewRetr.GetFocusedRowCellValue("ITCOD")?.ToString();
            string sITNAM = GridViewRetr.GetFocusedRowCellValue("ITNAM")?.ToString();
            
            int i = GridViewRetr.GetFocusedDataSourceRowIndex();

            if (XtraMessageBox.Show("품목코드 : " + sITCOD + "\r\n품명 : " + sITNAM +
                  " \r\n선택된 항목을 삭제하시겠습니까? \r\n", "품목정보 삭제여부", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;

            try
            {
                Dictionary<string, string> dicParams = new Dictionary<string, string>();
                dicParams.Add("CMD", "DEL");
                dicParams.Add("ITCOD", sITCOD);

                DataTable dtResult = ComnEtcFunc.GetInfo(dicParams, this.PROCEDURE_ID);
                if (dtResult.Rows.Count > 0)
                {
                    string sErrorGb = dtResult.Rows[0]["RESULT"]?.ToString();
                    string sMsg = dtResult.Rows[0]["MSG"]?.ToString();
                    if (sErrorGb.Equals("1"))
                    {
                        XtraMessageBox.Show(sMsg, "품목 삭제 완료");
                        //ComnEtcFunc.SetLogInfo(Name, Text, ComnEtcFunc.CONNECT_TYPE.조회);
                        BtnRetr.PerformClick();
                        if (i > 0)
                        {
                            GridViewRetr.MoveBy(i - 1);
                        }
                        else
                        {
                            GridViewRetr.MoveBy(0);
                        }
                        return;
                    }
                    else if (sErrorGb.Equals("0"))
                    {
                        XtraMessageBox.Show(sMsg, "품목 삭제 실패");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString());
                return;
            }
        }

        public void XLS()
        {
           // if (ComnEtcFunc.GetAuthInfo("XLS", this.Name)) return;
            ComnEtcFunc.ExportExcelFile("품목정보", GridRetr, this.Name, this.Text);
        }
        
        public void CLOSE() { Close(); }

        #region[조회]

        // 조회(F5)
        private void BtnRetr_Click(object sender, EventArgs e)
        {
            RETR();
        }
        
        // 제품 조회 메서드
        private void GetInfo()
        {
            ComnMethod.parameterDic.Clear();
            ComnMethod.parameterDic.Add("CMD", "LIST");
            ComnMethod.parameterDic.Add("FIND_IDX", CboSearch.SelectedIndex.ToString());
            ComnMethod.parameterDic.Add("FIND_WORD", TxtSearch.EditValue?.ToString());
            GridRetr.DataSource = DBConn.GetDataTable(PROCEDURE_ID, ComnMethod.parameterDic);

            if(GridViewRetr.RowCount > 0)
            {
                GridViewRetr.Focus();
            }
            else
            {
                TxtSearch.Focus();
            }

            //ComnMethod.SetLogInfo(this.Name, this.Text, ComnMethod.CONNECT_TYPE.조회);
        }
        
        private void ChkUse_CheckedChanged(object sender, EventArgs e)
        {
            BtnRetr.PerformClick();
        }
        private void TxtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnRetr.PerformClick();
            }
        }
        #endregion

        #region [추가 - 수정]

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            ADD();
        }
        private void GridViewRetr_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Clicks == 2)
            {
                //if (ComnEtcFunc.GetAuthInfo("UPD", this.Name)) return;

                string sItCod = GridViewRetr.GetFocusedRowCellValue("ITCOD")?.ToString();
                if (string.IsNullOrEmpty(sItCod))
                {
                    XtraMessageBox.Show("품번 정보가 존재하지 않습니다. \r\n다시 선택해주세요. ");
                    return;
                }

                CM001F01 frm = new CM001F01();
                frm.Owner = this;
                frm._ITCOD = sItCod;
                frm._ADDMOD = CM001F01.ADDMOD.MOD;
                frm.DataRowSendEvent += new CM001F01.SendDataHandler(GetDataRow);
                frm.Show();
            }
        }
        public void GetDataRow(string row)
        {
            BtnRetr.PerformClick();
            int i = GridViewRetr.LocateByDisplayText(0, GridColITCOD, row + "");
            GridViewRetr.FocusedRowHandle = i;
        }
        #endregion        

        #region [삭제]

        private void BtnDel_Click(object sender, EventArgs e)
        {
            DELETE();
        }

        #endregion

        #region [엑셀]
        private void BtnExcel_Click(object sender, EventArgs e)
        {
            XLS();
        }
        #endregion

        #region [단축키 - 닫기] 
        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                BtnClose.PerformClick();
            }
            else if (e.KeyCode == Keys.F5)
            {
                BtnRetr.PerformClick();
            }
            else if (e.KeyCode == Keys.F1)
            {
                BtnAdd.PerformClick();
            }
            else if (e.KeyCode == Keys.F4)
            {
                BtnDel.PerformClick();
            }
            else if (e.KeyCode == Keys.F8)
            {
                BtnExcel.PerformClick();
            }
        }
        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
        
        #endregion
        
        
    }
}