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
using static ComnFunc;
using static GridFunc;

namespace VisionIns
{
    public partial class CM002F00 : DevExpress.XtraEditors.XtraForm, MainButtonInterface
    {
        private string PROCEDURE_ID = "DP_CM002F00";

        public CM002F00()
        {
            InitializeComponent();
            gp_GridStyleForSelect(Gv_List1);
            gp_GridStyleForSelect(Gv_List3);
            gp_GridStyleForSelect(Gv_List2);
            gp_GridStyleBasicSetting(Gv_Detl1);
            gp_GridStyleBasicSetting(Gv_Detl2);
            gp_InitControllerRule(layoutControl1);
            gp_InitControllerRule(layoutControl2);
            gp_InitControllerRule(layoutControl3);
            gp_InitControllerRule(layoutControl4);
        }

        private void Form_Load(object sender, EventArgs e)
        {
            Bt_Retr.PerformClick();
            Tx_Word.Focus();
        }

        #region [ 버튼 클릭 이벤트 ]
        private void SimpleButton_Click(object sender, EventArgs e)
        {
            SimpleButton sb = (SimpleButton)sender;

            if (sb.Name.Contains("Bt_Retr")) { RETR(); }
            else if (sb.Name.Contains("Bt_Add")) { ADD(); }
            else if (sb.Name.Contains("Bt_Save")) { SAVE(); }
            else if (sb.Name.Contains("Bt_Delete")) { DELETE(); }
            else if (sb.Name.Contains("Bt_Xls")) { XLS(); }
            else if (sb.Name.Contains("Bt_Close")) { Close(); }
        }

        public void RETR()
        {
            //if (ComnFunc.GetAuthInfo("USE", this.Name)) return;
            int page = xtraTabControl1.SelectedTabPageIndex;

            // 사용자관리
            if (page == 0) { GetUserInfo(); }
            // 그룹권한관리
            else if (page == 1) { GetAuthInfo(); }
            // 프로그램관리
            else if (page == 2) { GetPgmInfo(); }
        }

        public void ADD()
        {
            //if (ComnFunc.GetAuthInfo("ADD", this.Name)) return;

            int page = xtraTabControl1.SelectedTabPageIndex;
            MN002F00 main = (MN002F00)this.MdiParent;
            CM002F01 frm = new CM002F01();
            frm.DataSendEvent += new CM002F01.SendDataHandler(RefreshData);

            if (page == 0) { frm._Type = CM002F01.SaveType.사용자; }            // 사용자관리
            else if (page == 1) { frm._Type = CM002F01.SaveType.그룹권한; }     // 그룹권한관리
            else if (page == 2) { frm._Type = CM002F01.SaveType.프로그램; }     // 프로그램관리

            gp_OpenPopupForm(frm, main, this);
        }

        public void SAVE()
        {
            int page = xtraTabControl1.SelectedTabPageIndex;

            // 사용자관리
            if (page == 0)
            {
                string sUsrcd = Gv_Detl1.GetFocusedRowCellValue("USRCD")?.ToString();
                SaveUserInfo();
                Bt_Retr.PerformClick();
                int i = Gv_List1.LocateByDisplayText(0, Gc_List1_USRCD, sUsrcd);
                Gv_List1.FocusedRowHandle = i;
                //ComnFunc.SetLogInfo(this.Name, this.Text, ComnFunc.CONNECT_TYPE.수정);
            }
            // 그룹권한관리
            else if (page == 1)
            {
                SaveAuthInfo();
                //ComnFunc.SetLogInfo(this.Name, this.Text, ComnFunc.CONNECT_TYPE.수정);
            }
            // 프로그램관리
            else if (page == 2)
            {
                //if (ComnFunc.GetAuthInfo("UPD", this.Name)) return;

                string sPgmid = Gv_List2.GetFocusedRowCellValue("PGMID")?.ToString();
                SavePgmInfo();
                Bt_Retr.PerformClick();
                int i = Gv_List2.LocateByDisplayText(0, Gc_List2_PGMID, sPgmid);
                Gv_List2.FocusedRowHandle = i;
                //ComnFunc.SetLogInfo(this.Name, this.Text, ComnFunc.CONNECT_TYPE.수정);
            }
        }

        public void DELETE()
        {
            //if (ComnFunc.GetAuthInfo("DEL", this.Name)) return;
            int page = xtraTabControl1.SelectedTabPageIndex;

            // 사용자관리
            if (page == 0) { DelUserInfo(); }
            // 그룹권한관리
            else if (page == 1) { DelAuthInfo(); }
            // 프로그램관리
            else if (page == 2) { DelPgmInfo(); }
        }

        public void XLS()
        {
            //if (ComnFunc.GetAuthInfo("XLS", this.Name)) return;
            int page = xtraTabControl1.SelectedTabPageIndex;

            // 사용자관리
            if (page == 0) { gp_ExportExcelFile("사용자목록", Gc_List1, Name, Text); }
            // 그룹권한관리
            else if (page == 1) { gp_ExportExcelFile("그룹권한목록", Gc_Detl3, Name, Text); }
            // 프로그램관리
            else if (page == 2) { gp_ExportExcelFile("프로그램목록", Gc_List2, Name, Text); }
        }

        public void PRINT() { }
        public void CLOSE() { Close(); }
        #endregion

        #region[사용자 관리]

        #region [메서드]
        // 사용자 정보 조회 메서드
        private void GetUserInfo()
        {
            DBConn._DicParams.Add("CMD", "UserInfo_Retr");
            DBConn._DicParams.Add("FIND_IDX", Cb_Idx.SelectedIndex.ToString());
            DBConn._DicParams.Add("FIND_WORD", Tx_Word.EditValue?.ToString());
            DBConn._DicParams.Add("USE_GB", Rg_UseYn.EditValue?.ToString());
            DataTable dt = DBConn.GetDataTable(PROCEDURE_ID, DBConn._DicParams);
            if (dt != null)
            {
                Gc_List1.DataSource = dt;
                GetProgramInfo();
            }

            if (Gv_List1.RowCount > 0) { Gv_List1.Focus(); }
            else if (Gv_List1.RowCount == 0) { Tx_Word.SelectAll(); Tx_Word.Focus(); }
        }

        // 사용자별 프로그램 권한정보 조회
        private void GetProgramInfo()
        {
            string USRCD = Gv_List1.GetFocusedRowCellValue("USRCD")?.ToString();

            DBConn._DicParams.Add("CMD", "Program_Auth_Retr");
            DBConn._DicParams.Add("USRCD", USRCD);

            DataTable dtP = DBConn.GetDataTable(PROCEDURE_ID, DBConn._DicParams);
            if (dtP != null) { Gc_Detl1.DataSource = dtP; }
        }

        // 저장
        private void SaveUserInfo()
        {
            DataTable dt = Gc_Detl1.DataSource as DataTable;
            string sUsrcd = Gv_Detl1.GetFocusedRowCellValue("USRCD")?.ToString();
            StringBuilder strSql = new StringBuilder();

            try
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    strSql.Clear();
                    strSql.AppendLine(" UPDATE zPGMAUT SET ");
                    strSql.AppendLine(" USEYN = '" + dt.Rows[i]["USEYN"].ToString() + "' ");
                    strSql.AppendLine(" , ADD_Y = '" + dt.Rows[i]["ADD_Y"].ToString() + "' ");
                    strSql.AppendLine(" , UPD_Y = '" + dt.Rows[i]["UPD_Y"].ToString() + "' ");
                    strSql.AppendLine(" , DEL_Y = '" + dt.Rows[i]["DEL_Y"].ToString() + "' ");
                    strSql.AppendLine(" , XLS_Y = '" + dt.Rows[i]["XLS_Y"].ToString() + "' ");
                    strSql.AppendLine(" , PRT_Y = '" + dt.Rows[i]["PRT_Y"].ToString() + "' ");
                    if (dt.Rows[i]["USEYN"].ToString().Equals("N"))
                    {
                        strSql.AppendLine(" , FAVID = 'N' ");
                    }
                    strSql.AppendLine(" , MDATE = CONVERT(VARCHAR(20), GETDATE(), 20)");
                    strSql.AppendLine(" , MUSER = '" + LoginUser.USRCD + "'");
                    strSql.AppendLine(" WHERE USRCD = '" + sUsrcd + "' ");
                    strSql.AppendLine(" AND PGMID = '" + dt.Rows[i]["PGMID"].ToString() + "' ");

                    strSql.AppendLine(" SELECT A.PGMID, B.PGMNM, A.USEYN, A.ADD_Y, A.UPD_Y, A.DEL_Y, A.XLS_Y, A.PRT_Y, A.FAVID ");
                    strSql.AppendLine(" FROM zPGMAUT A");
                    strSql.AppendLine(" LEFT OUTER JOIN zPGMLST B");
                    strSql.AppendLine(" ON A.PGMID = B.PGMID");
                    strSql.AppendLine(" WHERE A.USRCD = '" + sUsrcd + "' ");
                    strSql.AppendLine(" AND A.PGMID = '" + dt.Rows[i]["PGMID"].ToString() + "' ");
                    strSql.AppendLine(" ORDER BY A.PGMID");

                    DataTable dtR = DBConn.GetDataTable(strSql.ToString());
                }
                gp_PrintMessage("성공적으로 저장되었습니다.", Text, MessageType.알림);
            }
            catch (Exception ex)
            {
                gp_PrintMessage(ex.Message, Text, MessageType.오류);
            }
        }

        // 사용자 정보 삭제 메서드
        private void DelUserInfo()
        {
            string sUsrCd = Gv_List1.GetFocusedRowCellValue("USRCD")?.ToString();
            string sUsrId = Gv_List1.GetFocusedRowCellValue("USRID")?.ToString();
            int rowIndex = Gv_List1.GetFocusedDataSourceRowIndex();

            if (string.IsNullOrEmpty(sUsrId))
            {
                gp_PrintMessage("사용자를 선택해 주세요.", Text, MessageType.알림);
                return;
            }

            if (!gp_PrintQuestion("사용자ID : " + sUsrId +
                  " \r\n선택된 항목을 삭제하시겠습니까? \r\n삭제한 데이터는 복구할 수 없습니다.", Text, MessageType.질문))
                return;

            try
            {
                Cursor = Cursors.WaitCursor;
                DBConn._DicParams.Add("CMD", "UserInfo_Delete");
                DBConn._DicParams.Add("USRCD", sUsrCd);
                DataTable dtResult = DBConn.GetDataTable(PROCEDURE_ID, DBConn._DicParams);
                if (dtResult.Rows.Count > 0)
                {
                    string sScsGb = dtResult.Rows[0]["RESULT"]?.ToString();
                    string sMsg = dtResult.Rows[0]["MSG"]?.ToString();
                    if (sScsGb.Equals("1"))
                    {
                        gp_PrintMessage(sMsg, Text, MessageType.알림);
                        gp_SetLogInfo(Name, Text, CONNECT_TYPE.삭제);
                        Bt_Retr.PerformClick();
                        if (rowIndex > 0) { Gv_List1.MoveBy(rowIndex - 1); }
                        else { Gv_List1.MoveBy(0); }
                    }
                    else
                        gp_PrintMessage(sMsg, Text, MessageType.오류);
                }
                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                gp_PrintMessage(ex.Message, Text, MessageType.오류);
                Cursor = Cursors.Default;
            }
        }

        // 정보 추가 후 조회갱신 메서드
        private void RefreshData(CM002F01.SaveType type, string str)
        {
            if (type == CM002F01.SaveType.사용자)
            {
                Bt_Retr.PerformClick();
                int i = Gv_List1.LocateByDisplayText(0, Gc_List1_USRCD, str);
                Gv_List1.FocusedRowHandle = i;
            }
            else if (type == CM002F01.SaveType.그룹권한)
            {

            }
            else if (type == CM002F01.SaveType.프로그램)
            {
                Bt_Retr_P.PerformClick();
                int i = Gv_List2.LocateByDisplayText(0, Gc_List2_PGMID, str);
                Gv_List2.FocusedRowHandle = i;
            }
        }
        #endregion

        #region [ 그리드 뷰 이벤트 ]
        // 그리드 뷰 Row 더블클릭 시 수정 폼 로드 이벤트
        private void Gv_List1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Clicks == 2)
            {
                //if (!gp_GetAuthInfo("UPD", Name)) return;
                string USRCD = Gv_List1.GetFocusedRowCellValue("USRCD")?.ToString();

                int page = xtraTabControl1.SelectedTabPageIndex;
                MN002F00 main = (MN002F00)this.MdiParent;
                CM002F01 frm = new CM002F01();
                frm.DataSendEvent += new CM002F01.SendDataHandler(RefreshData);
                frm._Type = CM002F01.SaveType.사용자;            // 사용자관리
                frm._USRCD = USRCD;
                gp_OpenPopupForm(frm, main, this);
            }
        }

        // 그리드 뷰 선택 Row 변경 시 서브 그리드 뷰 데이터 자동 조회 이벤트
        private void Gv_List1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            GetProgramInfo();
        }

        // 사용여부 라디오버튼 값 변경 시 조회 갱신 이벤트
        private void Rg_UseYn_EditValueChanged(object sender, EventArgs e)
        {
            Bt_Retr.PerformClick();
        }

        // 권한이 "Y"면 셀 색상 노란색으로 변경 이벤트
        private void Gv_Detl1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column == Gc_Detl1_USEYN || e.Column == Gc_Detl1_ADD_Y || e.Column == Gc_Detl1_UPD_Y || e.Column == Gc_Detl1_DEL_Y || e.Column == Gc_Detl1_PRT_Y || e.Column == Gc_Detl1_XLS_Y)
            {
                string sVal = e.CellValue.ToString();
                if (sVal.Equals("Y"))
                {
                    e.Appearance.BackColor = Color.Yellow;
                }
            }
        }
        #endregion

        #region[행전체선택/해제]
        //선택된 행 일괄 선택/해제
        private void Gv_Detl1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Clicks == 2)
            {
                int count = 0;
                if (Gv_Detl1.GetFocusedRowCellValue("USEYN")?.ToString() == "N") { count = count + 1; }
                if (Gv_Detl1.GetFocusedRowCellValue("ADD_Y")?.ToString() == "N") { count = count + 1; }
                if (Gv_Detl1.GetFocusedRowCellValue("UPD_Y")?.ToString() == "N") { count = count + 1; }
                if (Gv_Detl1.GetFocusedRowCellValue("DEL_Y")?.ToString() == "N") { count = count + 1; }
                if (Gv_Detl1.GetFocusedRowCellValue("PRT_Y")?.ToString() == "N") { count = count + 1; }
                if (Gv_Detl1.GetFocusedRowCellValue("XLS_Y")?.ToString() == "N") { count = count + 1; }

                if (count > 0)
                {
                    Gv_Detl1.SetFocusedRowCellValue("USEYN", "Y");
                    Gv_Detl1.SetFocusedRowCellValue("ADD_Y", "Y");
                    Gv_Detl1.SetFocusedRowCellValue("UPD_Y", "Y");
                    Gv_Detl1.SetFocusedRowCellValue("DEL_Y", "Y");
                    Gv_Detl1.SetFocusedRowCellValue("PRT_Y", "Y");
                    Gv_Detl1.SetFocusedRowCellValue("XLS_Y", "Y");
                }
                else if (count == 0)
                {
                    Gv_Detl1.SetFocusedRowCellValue("USEYN", "N");
                    Gv_Detl1.SetFocusedRowCellValue("ADD_Y", "N");
                    Gv_Detl1.SetFocusedRowCellValue("UPD_Y", "N");
                    Gv_Detl1.SetFocusedRowCellValue("DEL_Y", "N");
                    Gv_Detl1.SetFocusedRowCellValue("PRT_Y", "N");
                    Gv_Detl1.SetFocusedRowCellValue("XLS_Y", "N");
                }
            }
        }
        #endregion

        #endregion

        #region [그룹권한 관리]

        #region[메서드]
        // 조회
        private void GetAuthInfo()
        {
            Dictionary<string, string> dicParams = new Dictionary<string, string>();

            dicParams.Clear();
            dicParams.Add("CMD", "Auth_Retr");
            dicParams.Add("FIND_IDX", Cb_Idx_G.SelectedIndex.ToString());
            dicParams.Add("FIND_WORD", Tx_Word_G.EditValue?.ToString());

            DataTable dt = DBConn.GetDataTable(DBConn.dbCon, this.PROCEDURE_ID, dicParams);

            if (dt.Rows.Count > 0)
            {
                Gc_List3.DataSource = dt;
            }

            if (Gv_List3.RowCount > 0)
            {
                //ComnMethod.SetLogInfo(this.Name, this.Text, ComnMethod.CONNECT_TYPE.조회);
                Gv_List3.Focus();
            }

            DataTable dtD = new DataTable();

            string sLvlcd = Gv_List3.GetFocusedRowCellValue("LVLCD").ToString();

            Dictionary<string, string> dicParamsD = new Dictionary<string, string>();
            dicParamsD.Clear();
            dicParamsD.Add("CMD", "AuthDetail");
            dicParamsD.Add("LVLCD", sLvlcd);

            dtD = null;
            dtD = DBConn.GetDataTable(DBConn.dbCon, PROCEDURE_ID, dicParamsD);
            if (dtD != null)
                Gc_Detl3.DataSource = dtD;

        }

        //저장
        private void SaveAuthInfo()
        {
            DataTable dt = Gc_Detl3.DataSource as DataTable;

            string sLvlCd = Gv_List3.GetFocusedRowCellValue("LVLCD")?.ToString();

            StringBuilder strSql = new StringBuilder();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                strSql.Clear();
                strSql.AppendLine(" UPDATE zLVLAUT SET ");
                strSql.AppendLine(" USEYN = '" + dt.Rows[i]["USEYN"].ToString() + "' ");
                strSql.AppendLine(" , ADD_Y = '" + dt.Rows[i]["ADD_Y"].ToString() + "' ");
                strSql.AppendLine(" , UPD_Y = '" + dt.Rows[i]["UPD_Y"].ToString() + "' ");
                strSql.AppendLine(" , DEL_Y = '" + dt.Rows[i]["DEL_Y"].ToString() + "' ");
                strSql.AppendLine(" , XLS_Y = '" + dt.Rows[i]["XLS_Y"].ToString() + "' ");
                strSql.AppendLine(" , PRT_Y = '" + dt.Rows[i]["PRT_Y"].ToString() + "' ");
                strSql.AppendLine(" , MDATE = CONVERT(VARCHAR(20), GETDATE(), 20)");
                strSql.AppendLine(" WHERE LVLCD = '" + sLvlCd + "' ");
                strSql.AppendLine(" AND PGMID = '" + dt.Rows[i]["PGMID"].ToString() + "' ");

                strSql.AppendLine(" SELECT A.PGMID, B.PGMNM, A.USEYN, A.ADD_Y, A.UPD_Y, A.DEL_Y, A.XLS_Y, A.PRT_Y ");
                strSql.AppendLine(" FROM zLVLAUT A");
                strSql.AppendLine(" LEFT OUTER JOIN zPGMLST B");
                strSql.AppendLine(" ON A.PGMID = B.PGMID");
                strSql.AppendLine(" WHERE A.LVLCD = '" + sLvlCd + "' ");
                strSql.AppendLine(" AND A.PGMID = '" + dt.Rows[i]["PGMID"].ToString() + "' ");
                strSql.AppendLine(" ORDER BY A.PGMID");

                DataTable dtR = DBConn.GetDataTable(DBConn.dbCon, strSql.ToString());
            }

        }

        // 삭제
        private void DelAuthInfo()
        {
            string sLvlCd = Gv_List3.GetFocusedRowCellValue("LVLCD")?.ToString();
            string sLvlNm = Gv_List3.GetFocusedRowCellValue("LVLNM")?.ToString();
            int rowIndex = Gv_List3.GetFocusedDataSourceRowIndex();

            if (XtraMessageBox.Show("권한CD : " + sLvlCd + "   권한명 : " + sLvlNm +
                  " \r\n선택된 항목을 삭제하시겠습니까? \r\n삭제한 데이터는 복구할 수 없습니다."
                , "권한정보 삭제", MessageBoxButtons.YesNo) != DialogResult.Yes)
            {
                return;
            }
            try
            {
                Cursor = Cursors.WaitCursor;
                Dictionary<string, string> dicParams = new Dictionary<string, string>();
                dicParams.Clear();
                dicParams.Add("CMD", "Auth_Delete");
                dicParams.Add("LVLCD", sLvlCd);
                dicParams.Add("LVLNM", sLvlNm);

                DataTable dtResult = DBConn.GetDataTable(DBConn.dbCon, this.PROCEDURE_ID, dicParams);
                Cursor = Cursors.WaitCursor;
                if (dtResult.Rows.Count > 0)
                {
                    string sScsGb = dtResult.Rows[0]["RESULT"]?.ToString();
                    string sMsg = dtResult.Rows[0]["MSG"]?.ToString();
                    XtraMessageBox.Show(sMsg);
                    if (sScsGb.Equals("1"))
                    {
                        //ComnMethod.SetLogInfo(this.Name, this.Text, ComnMethod.CONNECT_TYPE.삭제);
                        Bt_Retr_G.PerformClick();
                        if (rowIndex > 0)
                        {
                            Gv_List3.MoveBy(rowIndex - 1);
                        }
                        else
                        {
                            Gv_List3.MoveBy(0);
                        }
                    }
                }
                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor = Cursors.WaitCursor;
                Cursor = Cursors.Default;
            }
        }

        // 사용자 정보 추가 후 조회갱신 메서드
        private void GetData_G(string str)
        {
            //Bt_Retr_G.PerformClick();
            //int i = Gv_List3.LocateByDisplayText(0, gridColLvlCd, str);
            //Gv_List3.FocusedRowHandle = i;
        }
        #endregion

        #region[그리드뷰]
        //상세정보 자동 조회기능
        private void Gv_List3_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            Lk_Pggrp.EditValue = "";
            DataTable dt = new DataTable();

            if (e.FocusedRowHandle < 0)
            {
                dt = (DataTable)Gc_Detl3.DataSource;
                dt.Clear();
                Gc_Detl3.DataSource = dt;
                return;
            }
            string sLvlcd = Gv_List3.GetFocusedRowCellValue("LVLCD")?.ToString();

            Dictionary<string, string> dicParams = new Dictionary<string, string>();
            dicParams.Clear();
            dicParams.Add("CMD", "AuthDetail");
            dicParams.Add("LVLCD", sLvlcd);

            dt = null;
            dt = DBConn.GetDataTable(DBConn.dbCon, PROCEDURE_ID, dicParams);
            if (dt != null)
                Gc_Detl3.DataSource = dt;
        }

        // 권한이 "Y"면 셀 색상 노란색으로 변경 이벤트
        private void GridViewDetail_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            //if (e.Column == gridColUse || e.Column == gridColAdd || e.Column == gridColUpd || e.Column == gridColDel || e.Column == gridColPrt || e.Column == gridColXls)
            //{
            //    string sVal = e.CellValue.ToString();
            //    if (sVal.Equals("Y"))
            //    {
            //        e.Appearance.BackColor = Color.Yellow;
            //    }
            //}
        }
        #endregion

        #region [시스템 구분별 조회]
        private void Lk_Pggrp_EditValueChanged(object sender, EventArgs e)
        {
            DataTable dtD = new DataTable();

            string sLvlcd = Gv_List3.GetFocusedRowCellValue("LVLCD")?.ToString();
            string sPggrp = Lk_Pggrp.EditValue?.ToString();

            Dictionary<string, string> dicParamsD = new Dictionary<string, string>();
            dicParamsD.Clear();
            dicParamsD.Add("CMD", "Detail_Retr");
            dicParamsD.Add("LVLCD", sLvlcd);
            dicParamsD.Add("PGGRP", sPggrp);

            dtD = null;
            dtD = DBConn.GetDataTable(DBConn.dbCon, PROCEDURE_ID, dicParamsD);
            if (dtD != null)
                Gc_Detl3.DataSource = dtD;
        }
        #endregion

        #region[전체선택/해제, 행전체선택/해제]
        //전체선택/해제
        private void layoutControlGroup4_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            //if (e.Button.Properties.Tag.Equals("CHECK"))
            //{
            //    for (int i = 0; i < GridViewDetail.RowCount; i++)
            //    {
            //        GridViewDetail.SetRowCellValue(i, gridColUse, "Y");
            //        GridViewDetail.SetRowCellValue(i, gridColAdd, "Y");
            //        GridViewDetail.SetRowCellValue(i, gridColUpd, "Y");
            //        GridViewDetail.SetRowCellValue(i, gridColDel, "Y");
            //        GridViewDetail.SetRowCellValue(i, gridColPrt, "Y");
            //        GridViewDetail.SetRowCellValue(i, gridColXls, "Y");
            //    }
            //}
            //else if (e.Button.Properties.Tag.Equals("CLEAR"))
            //{
            //    for (int i = 0; i < GridViewDetail.RowCount; i++)
            //    {
            //        GridViewDetail.SetRowCellValue(i, gridColUse, "N");
            //        GridViewDetail.SetRowCellValue(i, gridColAdd, "N");
            //        GridViewDetail.SetRowCellValue(i, gridColUpd, "N");
            //        GridViewDetail.SetRowCellValue(i, gridColDel, "N");
            //        GridViewDetail.SetRowCellValue(i, gridColPrt, "N");
            //        GridViewDetail.SetRowCellValue(i, gridColXls, "N");
            //    }
            //}
        }

        //선택된 행 일괄 선택/해제
        private void GridViewDetail_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            //if (e.Clicks == 2)
            //{
            //    int count = 0;
            //    if (GridViewDetail.GetFocusedRowCellValue(gridColUse)?.ToString() == "N")
            //        count = count + 1;
            //    if (GridViewDetail.GetFocusedRowCellValue(gridColAdd)?.ToString() == "N")
            //        count = count + 1;
            //    if (GridViewDetail.GetFocusedRowCellValue(gridColUpd)?.ToString() == "N")
            //        count = count + 1;
            //    if (GridViewDetail.GetFocusedRowCellValue(gridColDel)?.ToString() == "N")
            //        count = count + 1;
            //    if (GridViewDetail.GetFocusedRowCellValue(gridColPrt)?.ToString() == "N")
            //        count = count + 1;
            //    if (GridViewDetail.GetFocusedRowCellValue(gridColXls)?.ToString() == "N")
            //        count = count + 1;

            //    if (count > 0)
            //    {
            //        GridViewDetail.SetFocusedRowCellValue(gridColUse, "Y");
            //        GridViewDetail.SetFocusedRowCellValue(gridColAdd, "Y");
            //        GridViewDetail.SetFocusedRowCellValue(gridColUpd, "Y");
            //        GridViewDetail.SetFocusedRowCellValue(gridColDel, "Y");
            //        GridViewDetail.SetFocusedRowCellValue(gridColPrt, "Y");
            //        GridViewDetail.SetFocusedRowCellValue(gridColXls, "Y");
            //    }
            //    else if (count == 0)
            //    {
            //        GridViewDetail.SetFocusedRowCellValue(gridColUse, "N");
            //        GridViewDetail.SetFocusedRowCellValue(gridColAdd, "N");
            //        GridViewDetail.SetFocusedRowCellValue(gridColUpd, "N");
            //        GridViewDetail.SetFocusedRowCellValue(gridColDel, "N");
            //        GridViewDetail.SetFocusedRowCellValue(gridColPrt, "N");
            //        GridViewDetail.SetFocusedRowCellValue(gridColXls, "N");
            //    }
            //}
        }
        #endregion

        #endregion

        #region[프로그램관리]

        #region [메서드]
        //프로그램 권한 조회
        private void GetPgmInfo()
        {
            DBConn._DicParams.Add("CMD", "PgmInfo_Retr");
            DBConn._DicParams.Add("FIND_IDX", Cb_Idx_P.SelectedIndex.ToString());
            DBConn._DicParams.Add("FIND_WORD", Tx_Word_P.EditValue?.ToString());
            DataTable dt = DBConn.GetDataTable(PROCEDURE_ID, DBConn._DicParams);
            if (dt != null)
                Gc_List2.DataSource = dt;

            //사용자 그리드 조회
            string sPgmid = Gv_List2.GetFocusedRowCellValue("PGMID")?.ToString();
            DBConn._DicParams.Clear();
            DBConn._DicParams.Add("CMD", "User_Auth_Retr");
            DBConn._DicParams.Add("PGMID", sPgmid);
            DataTable dtU = DBConn.GetDataTable(PROCEDURE_ID, DBConn._DicParams);
            if (dtU != null)
                Gc_Detl2.DataSource = dtU;

            if (Gv_List2.RowCount > 0)
            {
                //ComnMethod.SetLogInfo(this.Name, this.Text, ComnMethod.CONNECT_TYPE.조회);
                Gv_List2.Focus();
            }
            else if (Gv_List2.RowCount == 0)
            {
                Tx_Word.SelectAll();
                Tx_Word.Focus();
            }
        }

        //프로그램 권한 정보 저장
        private void SavePgmInfo()
        {
            DataTable dt = Gc_Detl2.DataSource as DataTable;

            string sPgmid = Gv_List2.GetFocusedRowCellValue("PGMID")?.ToString();

            StringBuilder strSql = new StringBuilder();

            try
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    strSql.Clear();
                    strSql.AppendLine(" UPDATE zPGMAUT SET ");
                    strSql.AppendLine(" USEYN = '" + dt.Rows[i]["USEYN"].ToString() + "' ");
                    strSql.AppendLine(" , ADD_Y = '" + dt.Rows[i]["ADD_Y"].ToString() + "' ");
                    strSql.AppendLine(" , UPD_Y = '" + dt.Rows[i]["UPD_Y"].ToString() + "' ");
                    strSql.AppendLine(" , DEL_Y = '" + dt.Rows[i]["DEL_Y"].ToString() + "' ");
                    strSql.AppendLine(" , XLS_Y = '" + dt.Rows[i]["XLS_Y"].ToString() + "' ");
                    strSql.AppendLine(" , PRT_Y = '" + dt.Rows[i]["PRT_Y"].ToString() + "' ");
                    if (dt.Rows[i]["USEYN"].ToString().Equals("N"))
                    {
                        strSql.AppendLine(" , FAVID = 'N' ");
                    }
                    strSql.AppendLine(" , MDATE = CONVERT(VARCHAR(20), GETDATE(), 20)");
                    strSql.AppendLine(" , MUSER = '" + LoginUser.USRCD + "'");
                    strSql.AppendLine(" WHERE PGMID = '" + sPgmid + "' ");
                    strSql.AppendLine(" AND USRCD = '" + dt.Rows[i]["USRCD"].ToString() + "' ");

                    strSql.AppendLine(" SELECT A.PGMID, B.PGMNM, A.USEYN, A.ADD_Y, A.UPD_Y, A.DEL_Y, A.XLS_Y, A.PRT_Y, A.FAVID ");
                    strSql.AppendLine(" FROM zPGMAUT A");
                    strSql.AppendLine(" LEFT OUTER JOIN zPGMLST B");
                    strSql.AppendLine(" ON A.PGMID = B.PGMID");
                    strSql.AppendLine(" WHERE A.PGMID = '" + sPgmid + "' ");
                    strSql.AppendLine(" AND A.USRCD = '" + dt.Rows[i]["USRCD"].ToString() + "' ");
                    strSql.AppendLine(" ORDER BY A.PGMID");

                    DataTable dtR = DBConn.GetDataTable(strSql.ToString());
                }
                gp_PrintMessage("성공적으로 저장되었습니다.", Text, MessageType.알림);
            }
            catch (Exception ex)
            {
                gp_PrintMessage(ex.Message, Text, MessageType.오류);
            }
        }

        // 프로그램별 사용자권한 정보 삭제 메서드
        private void DelPgmInfo()
        {
            string sPgmid = Gv_List2.GetFocusedRowCellValue("PGMID")?.ToString();
            string sPgmnm = Gv_List2.GetFocusedRowCellValue("PGMNM")?.ToString();
            int rowIndex = Gv_List2.GetFocusedDataSourceRowIndex();

            if (string.IsNullOrEmpty(sPgmid))
            {
                gp_PrintMessage("프로그램을 선택해 주세요.", Text, MessageType.알림);
                return;
            }

            if (!gp_PrintQuestion("프로그램ID : " + sPgmid + "\r\n프로그램명 : " + sPgmnm +
                  " \r\n선택된 항목을 삭제하시겠습니까? \r\n삭제한 데이터는 복구할 수 없습니다.", Text, MessageType.질문))
                return;

            try
            {
                Cursor = Cursors.WaitCursor;
                DBConn._DicParams.Add("CMD", "Pgm_Del");
                DBConn._DicParams.Add("PGMID", sPgmid);
                DBConn._DicParams.Add("PGMNM", sPgmnm);
                DataTable dtResult = DBConn.GetDataTable(PROCEDURE_ID, DBConn._DicParams);
                if (dtResult.Rows.Count > 0)
                {
                    string sScsGb = dtResult.Rows[0]["RESULT"]?.ToString();
                    string sMsg = dtResult.Rows[0]["MSG"]?.ToString();
                    if (sScsGb.Equals("1"))
                    {
                        //ComnMethod.SetLogInfo(this.Name, this.Text, ComnMethod.CONNECT_TYPE.삭제);
                        gp_PrintMessage(sMsg, Text, MessageType.알림);
                        Bt_Retr_P.PerformClick();
                        if (rowIndex > 0) { Gv_List2.MoveBy(rowIndex - 1); }
                        else { Gv_List2.MoveBy(0); }
                    }
                    else { gp_PrintMessage(sMsg, Text, MessageType.오류); }
                }
                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                gp_PrintMessage(ex.Message, Text, MessageType.오류);
                Cursor = Cursors.Default;
            }
        }
        #endregion

        #region [그리드 뷰 이벤트]
        //프로그램 그리드 뷰 Row 더블클릭 시 수정 폼 로드 이벤트
        private void Gv_List2_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            //if (ComnEtcFunc.GetAuthInfo("UPD", this.Name)) return;

            if (e.Clicks == 2)
            {
                string PGMID = Gv_List2.GetFocusedRowCellValue("PGMID")?.ToString();

                int page = xtraTabControl1.SelectedTabPageIndex;
                MN002F00 main = (MN002F00)this.MdiParent;
                CM002F01 frm = new CM002F01();
                frm.DataSendEvent += new CM002F01.SendDataHandler(RefreshData);
                frm._Type = CM002F01.SaveType.프로그램;            // 프로그램관리
                frm._PGMID = PGMID;
                gp_OpenPopupForm(frm, main, this);
            }
        }

        private void Gv_List2_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //사용자 그리드 조회
            DataTable dtU = new DataTable();

            if (e.FocusedRowHandle < 0)
            {
                dtU = (DataTable)Gc_Detl2.DataSource;
                dtU.Clear();
                Gc_Detl2.DataSource = dtU;
                return;
            }

            string sPgmid = Gv_List2.GetFocusedRowCellValue("PGMID")?.ToString();
            DBConn._DicParams.Add("CMD", "User_Auth_Retr");
            DBConn._DicParams.Add("PGMID", sPgmid);

            dtU = null;
            dtU = DBConn.GetDataTable(PROCEDURE_ID, DBConn._DicParams);
            if (dtU != null)
                Gc_Detl2.DataSource = dtU;
        }

        private void Gv_Detl2_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column == Gc_Detl2_USEYN || e.Column == Gc_Detl2_ADD_Y || e.Column == Gc_Detl2_UPD_Y || e.Column == Gc_Detl2_DEL_Y || e.Column == Gc_Detl2_PRT_Y || e.Column == Gc_Detl2_XLS_Y)
            {
                string sVal = e.CellValue.ToString();
                if (sVal.Equals("Y"))
                {
                    e.Appearance.BackColor = Color.Yellow;
                }
            }
        }
        #endregion

        #region[행전체선택/해제]
        //선택된 행 일괄 선택/해제
        private void Gv_Detl2_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Clicks == 2)
            {
                int count = 0;
                if (Gv_Detl2.GetFocusedRowCellValue("USEYN")?.ToString() == "N") { count = count + 1; }
                if (Gv_Detl2.GetFocusedRowCellValue("ADD_Y")?.ToString() == "N") { count = count + 1; }
                if (Gv_Detl2.GetFocusedRowCellValue("UPD_Y")?.ToString() == "N") { count = count + 1; }
                if (Gv_Detl2.GetFocusedRowCellValue("DEL_Y")?.ToString() == "N") { count = count + 1; }
                if (Gv_Detl2.GetFocusedRowCellValue("PRT_Y")?.ToString() == "N") { count = count + 1; }
                if (Gv_Detl2.GetFocusedRowCellValue("XLS_Y")?.ToString() == "N") { count = count + 1; }

                if (count > 0)
                {
                    Gv_Detl2.SetFocusedRowCellValue("USEYN", "Y");
                    Gv_Detl2.SetFocusedRowCellValue("ADD_Y", "Y");
                    Gv_Detl2.SetFocusedRowCellValue("UPD_Y", "Y");
                    Gv_Detl2.SetFocusedRowCellValue("DEL_Y", "Y");
                    Gv_Detl2.SetFocusedRowCellValue("PRT_Y", "Y");
                    Gv_Detl2.SetFocusedRowCellValue("XLS_Y", "Y");
                }
                else if (count == 0)
                {
                    Gv_Detl2.SetFocusedRowCellValue("USEYN", "N");
                    Gv_Detl2.SetFocusedRowCellValue("ADD_Y", "N");
                    Gv_Detl2.SetFocusedRowCellValue("UPD_Y", "N");
                    Gv_Detl2.SetFocusedRowCellValue("DEL_Y", "N");
                    Gv_Detl2.SetFocusedRowCellValue("PRT_Y", "N");
                    Gv_Detl2.SetFocusedRowCellValue("XLS_Y", "N");
                }
            }
        }
        #endregion

        #endregion

        #region [ 키 이벤트 ] 
        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            //Main_Test frm = (Main_Test)this.MdiParent;
            //SimpleButton[] sbt = new SimpleButton[] { frm.Bt_Retr, frm.Bt_Add, frm.Bt_Delete, frm.Bt_Xls, frm.Bt_Close };
            int page = xtraTabControl1.SelectedTabPageIndex;
            SimpleButton[] sbt = new SimpleButton[6];
            if (page == 0)
                sbt = new SimpleButton[] { Bt_Retr, Bt_Add, Bt_Save, Bt_Delete, Bt_Xls, Bt_Close };
            else if (page == 1)
                sbt = new SimpleButton[] { Bt_Retr_G, Bt_Add_G, Bt_Save_G, Bt_Delete_G, Bt_Xls_G, Bt_Close_G };
            else if (page == 2)
                sbt = new SimpleButton[] { Bt_Retr_P, Bt_Add_P, Bt_Save_P, Bt_Delete_P, Bt_Xls_P, Bt_Close_P };

            gp_CommonButtonKeyEvent(sbt, e);
        }
        #endregion

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (xtraTabControl1.SelectedTabPageIndex == 0)
            {
                Bt_Retr.PerformClick();
            }
            else if (xtraTabControl1.SelectedTabPageIndex == 1)
            {
                Bt_Retr_G.PerformClick();
            }
            else if (xtraTabControl1.SelectedTabPageIndex == 2)
            {
                Bt_Retr_P.PerformClick();
            }
        }

        private void Tx_Word_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Bt_Retr.PerformClick();
            }
        }

        private void Tx_Word_P_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                Bt_Retr_P.PerformClick();
            }
        }
    }
}