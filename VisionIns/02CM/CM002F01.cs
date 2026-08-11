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
using System.IO;
using System.Security.Cryptography;

namespace VisionIns
{
    public partial class CM002F01 : DevExpress.XtraEditors.XtraForm
    {
        private string PROCEDURE_ID = "DP_CM002F00";
        public enum SaveType { 사용자, 프로그램, 그룹권한 };
        public SaveType _Type { get; set; }
        public string _USRCD { get; set; }
        public string _PGMID { get; set; }
        public delegate void SendDataHandler(SaveType type, string sVal);
        public event SendDataHandler DataSendEvent;

        public CM002F01()
        {
            InitializeComponent();
            gp_InitControllerRule(layoutControl1);
            gp_InitControllerRule(layoutControl2);
            gp_SetDateToValue(Dt_ENTDT);
            ComnEtcFunc.SetBoundLookUp(Lk_USLVL, "REFFPF", "USLVL", "");     // 그룹권한
            ComnEtcFunc.SetBoundLookUp(Lk_DEPCD, "REFFPF", "DEPTCD", "");     // 부서정보
            ComnEtcFunc.SetBoundLookUp(Lk_JKWCD, "REFFPF", "JKWICD", "");     // 직위정보
            ComnEtcFunc.SetBoundLookUp(Lk_SYSGU, "REFFPF", "SYSGU", "");     // (구)시스템구분
            ComnEtcFunc.SetBoundLookUp(Lk_PGGRP, "REFFPF", "PGGRP", "");     // 시스템구분 (구 업무그룹)
        }

        private void Form_Load(object sender, EventArgs e)
        {
            if (_Type == SaveType.사용자) { xtraTabControl1.SelectedTabPageIndex = 0; }
            else if (_Type == SaveType.프로그램) { xtraTabControl1.SelectedTabPageIndex = 1; }

            // 추가
            if (string.IsNullOrEmpty(_USRCD) && string.IsNullOrEmpty(_PGMID))
                INIT();

            // 수정
            else if (!string.IsNullOrEmpty(_USRCD) || !string.IsNullOrEmpty(_PGMID))
                MODINIT();
        }

        private void SimpleButton_Click(object sender, EventArgs e)
        {
            SimpleButton sb = (SimpleButton)sender;

            if (sb.Name.Contains("Bt_Reset")) { INIT(); }
            else if (sb.Name.Contains("Bt_Save")) { SAVE(); }
            else if (sb.Name.Contains("Bt_Close")) { Close(); }

            else if (sb.Name.Contains("Bt_PwReset")) { RESET(); }
            // 프로그램 ID 체크
            else if (sb.Name.Contains("Bt_PIDCK")) { PROGRAM_CHECK(); }
        }

        // 수정 폼 로드
        private void MODINIT()
        {
            // 사용자코드가 빈 값이 아니라면, 사용자 수정
            if (!string.IsNullOrEmpty(_USRCD))
            {
                DBConn._DicParams.Add("CMD", "USER_DIALOG");
                DBConn._DicParams.Add("USRCD", _USRCD);

                DataTable dt = DBConn.GetDataTable(PROCEDURE_ID, DBConn._DicParams);
                if (dt != null)
                {
                    // 사용자정보
                    Tx_USRCD.EditValue = dt.Rows[0]["USRCD"].ToString();
                    Tx_USRID.EditValue = dt.Rows[0]["USRID"].ToString();
                    Tx_USRNM.EditValue = dt.Rows[0]["USRNM"].ToString();
                    Ck_USEYN.EditValue = dt.Rows[0]["USEYN"].ToString();

                    // 상세정보
                    Dt_ENTDT.EditValue = string.IsNullOrEmpty(dt.Rows[0]["ENTDT"].ToString()) ? DateTime.Now.ToString("yyyy-MM-dd") : dt.Rows[0]["ENTDT"].ToString();
                    Lk_DEPCD.EditValue = dt.Rows[0]["DEPTCD"].ToString();
                    Lk_JKWCD.EditValue = dt.Rows[0]["JKWICD"].ToString();
                    Tx_JKMNM.EditValue = dt.Rows[0]["JKMUNM"].ToString();
                    //Tx_EMAIL.EditValue = dt.Rows[0]["EMAIL"].ToString();
                    Tx_MBLNO.EditValue = dt.Rows[0]["MOBLNO"].ToString();
                    Lk_USLVL.EditValue = dt.Rows[0]["USLVL"].ToString();
                    Tx_RK_U.EditValue = dt.Rows[0]["RK"].ToString();

                    //byte[] Img = Convert.IsDBNull(dt.Rows[0]["USIGN"]) ? null : (byte[])dt.Rows[0]["USIGN"];
                    //Pe_USIGN.Image = gp_ByteArrayToImage(Img);
                }
                Tx_USRID.Enabled = false;
                Tx_USRNM.Enabled = false;
            }
            // 프로그램ID가 빈 값이 아니라면, 프로그램 수정
            else if (!string.IsNullOrEmpty(_PGMID))
            {
                DBConn._DicParams.Add("CMD", "PgmInfo_Bound");
                DBConn._DicParams.Add("PGMID", _PGMID);

                DataTable dt = DBConn.GetDataTable(PROCEDURE_ID, DBConn._DicParams);
                if (dt != null)
                {
                    Tx_PGMID.EditValue = dt.Rows[0]["PGMID"]?.ToString();
                    Tx_PGMNM.EditValue = dt.Rows[0]["PGMNM"]?.ToString();
                    Lk_SYSGU.EditValue = dt.Rows[0]["SYSGU"]?.ToString();
                    Lk_PGGRP.EditValue = dt.Rows[0]["PGGRP"];
                    Tx_PGTAG.EditValue = dt.Rows[0]["PGTAG"]?.ToString();
                    Tx_PGLVL.EditValue = dt.Rows[0]["PGLVL"]?.ToString();
                    Ck_USEYN.EditValue = dt.Rows[0]["USEYN"]?.ToString();
                    Tx_RK_P.EditValue = dt.Rows[0]["RK"]?.ToString();
                }

                Tx_PGMID.Enabled = false;
                Bt_PIDCK.Enabled = false;
                this.ActiveControl = Tx_PGMNM;
            }
            Ck_Mode.Enabled = false;
        }

        // 초기화
        private void INIT()
        {
            if (_Type == SaveType.사용자)
            {
                Tx_USRCD.Enabled = true;
                Tx_USRID.EditValue = "";
                Tx_USRID.Enabled = true;
                Tx_USRNM.Enabled = true;
                Tx_USRNM.EditValue = "";
                Ck_USEYN.Checked = true;

                // 상세정보
                Lk_DEPCD.EditValue = "";
                Dt_ENTDT.EditValue = DateTime.Now;
                Tx_EMAIL.EditValue = "";
                Tx_MBLNO.EditValue = "";
                Tx_JKMNM.EditValue = "";
                Lk_JKWCD.EditValue = "";
                Lk_USLVL.EditValue = "";
                Tx_RK_U.EditValue = "";

                Tx_USRCD.Enabled = false;
                Tx_USRID.Enabled = true;
                Tx_USRNM.Enabled = true;
                Ck_Mode.Enabled = true;
                Bt_PwReset.Enabled = false;
                this.ActiveControl = Tx_USRID;
                Tx_USRID.Focus();
            }
            else if (_Type == SaveType.프로그램)
            {
                Tx_PGMID.EditValue = "";
                Tx_PGMNM.EditValue = "";
                Lk_SYSGU.EditValue = "";
                Lk_PGGRP.EditValue = "";
                Tx_PGTAG.EditValue = "";
                Tx_PGLVL.EditValue = "";
                Ck_USEYN.Checked = true;
                Tx_RK_P.EditValue = "";

                Tx_PGMID.Enabled = true;
                Tx_PGMNM.Enabled = true;
                Ck_Mode.Enabled = true;
                Bt_PIDCK.Enabled = true;

                this.ActiveControl = Tx_PGMID;
            }

            _USRCD = string.Empty;
            _PGMID = string.Empty;
        }

        // 저장
        private void SAVE()
        {
            if (_Type == SaveType.사용자)
            {
                string USRCD = _USRCD;
                string USRID = Tx_USRID.EditValue?.ToString();
                string WEBPW = Encrypt("qwer1!", ComnString.AES_Key);
                string USRNM = Tx_USRNM.EditValue?.ToString();
                string USEYN = Ck_USEYN.EditValue.ToString();
                string ENTDT = Dt_ENTDT.EditValue?.ToString().Substring(0, 10);
                string DEPCD = Lk_DEPCD.EditValue?.ToString();
                string JKWCD = Lk_JKWCD.EditValue?.ToString();
                string JKMNM = Tx_JKMNM.EditValue?.ToString();
                string EMAIL = Tx_EMAIL.EditValue?.ToString();
                string MBLNO = Tx_MBLNO.EditValue?.ToString();
                string USLVL = Lk_USLVL.EditValue?.ToString();
                string RK = Tx_RK_U.EditValue?.ToString();
                //byte[] USIGN_byte = null;
                //Image USIGN_image = Pe_USIGN.Image;
                //if (USIGN_image != null)
                //    USIGN_byte = gp_ImageToByteArray(USIGN_image);

                // 필수입력항목 검사(빈 칸 검사)
                if (string.IsNullOrWhiteSpace(USRID))
                {
                    gp_PrintMessage("사용자ID를 입력하세요.", Text, MessageType.알림);
                    Tx_USRID.Focus();
                    return;
                }
                else
                {
                    DBConn._DicParams.Add("CMD", "ID_CHECK");
                    DBConn._DicParams.Add("USRID", USRID);
                    DataTable dtP = DBConn.GetDataTable(PROCEDURE_ID, DBConn._DicParams);
                    if (dtP != null)
                    {
                        if (dtP.Rows.Count > 0 && string.IsNullOrEmpty(_USRCD))
                        {
                            gp_PrintMessage("이미 존재하는 아이디입니다.", Text, MessageType.알림);
                            Tx_USRID.SelectAll();
                            Tx_USRID.Focus();
                            return;
                        }
                    }
                }

                if (string.IsNullOrWhiteSpace(USRNM))
                {
                    gp_PrintMessage("사용자명을 입력하세요.", Text, MessageType.알림);
                    Tx_USRNM.Focus();
                    return;
                }

                try
                {
                    DBConn._DicParams_Object.Add("CMD", "UserInfo_Save");
                    DBConn._DicParams_Object.Add("USRCD", USRCD);
                    DBConn._DicParams_Object.Add("USRID", USRID);
                    DBConn._DicParams_Object.Add("WEBPW", WEBPW);
                    DBConn._DicParams_Object.Add("USRNM", USRNM);
                    DBConn._DicParams_Object.Add("USEYN", USEYN);
                    DBConn._DicParams_Object.Add("ENTDT", ENTDT);
                    DBConn._DicParams_Object.Add("DEPTCD", DEPCD);
                    DBConn._DicParams_Object.Add("HOUSE", "");
                    DBConn._DicParams_Object.Add("MOBLNO", MBLNO);
                    //DBConn._DicParams_Object.Add("EMAIL", EMAIL);
                    DBConn._DicParams_Object.Add("JKWICD", JKWCD);
                    DBConn._DicParams_Object.Add("JKMUNM", JKMNM);
                    DBConn._DicParams_Object.Add("USLVL", USLVL);
                    //DBConn._DicParams_Object.Add("USIGN", USIGN_byte); // 도장
                    DBConn._DicParams_Object.Add("RK", RK);
                    DBConn._DicParams_Object.Add("CUSER", LoginUser.USRCD);
                    DataTable dt = DBConn.GetDataTable(PROCEDURE_ID, DBConn._DicParams_Object);
                    if (dt != null)
                    {
                        if (dt.Rows[0]["RESULT"].ToString().Equals("1"))
                        {
                            gp_PrintMessage(dt.Rows[0]["MSG"].ToString(), Text, MessageType.알림);
                            USRCD = dt.Rows[0]["USRCD"]?.ToString();
                            if (string.IsNullOrEmpty(_USRCD))
                                LvSave(USRCD); // 권한 저장
                            DataSendEvent(_Type, USRCD);
                            // 연속저장
                            if (Ck_Mode.Checked)
                            {
                                INIT();
                                Tx_USRID.Focus();
                            }
                            // 저장
                            else
                                Close();
                        }
                        else if (dt.Rows[0]["RESULT"].ToString().Equals("0"))
                        {
                            gp_PrintMessage(dt.Rows[0]["MSG"].ToString(), Text, MessageType.오류);
                        }
                    }
                }
                catch (Exception ex)
                {
                    gp_PrintMessage(ex.Message, Text, MessageType.오류);
                }
            }
            else if (_Type == SaveType.프로그램)
            {
                string PGMID = string.IsNullOrEmpty(_PGMID) ? Tx_PGMID.EditValue?.ToString() : _PGMID;
                string PGMNM = Tx_PGMNM.EditValue?.ToString();
                string SYSGU = Lk_SYSGU.EditValue?.ToString();
                string PGGRP = Lk_PGGRP.EditValue?.ToString();
                string PGTAG = Tx_PGTAG.EditValue?.ToString();
                string PGLVL = Tx_PGLVL.EditValue?.ToString();
                string USEYN = Ck_USEYN.EditValue?.ToString();
                string RK_P = Tx_RK_P.EditValue?.ToString();

                // 필수입력항목 검사(빈 칸 검사)
                if (string.IsNullOrEmpty(PGMID))
                {
                    gp_PrintMessage("프로그램ID를 입력하세요.", Text, MessageType.알림);
                    Tx_PGMID.Focus();
                    return;
                }
                if (Tx_PGMID.Enabled == true)
                {
                    gp_PrintMessage("프로그램ID 중복체크를 해주세요.", Text, MessageType.알림);
                    return;
                }
                if (string.IsNullOrEmpty(PGMNM))
                {
                    gp_PrintMessage("프로그램명을 입력하세요.", Text, MessageType.알림);
                    Tx_PGMNM.Focus();
                    return;
                }

                try
                {
                    DBConn._DicParams.Add("CMD", "PGM_Save");
                    DBConn._DicParams.Add("PGMID", PGMID);
                    DBConn._DicParams.Add("PGMNM", PGMNM);
                    DBConn._DicParams.Add("SYSGU", SYSGU);
                    DBConn._DicParams.Add("PGGRP", PGGRP);
                    if (string.IsNullOrWhiteSpace(PGTAG)) { DBConn._DicParams.Add("PGTAG", "0"); }
                    else { DBConn._DicParams.Add("PGTAG", PGTAG); }
                    DBConn._DicParams.Add("PGLVL", PGLVL);
                    DBConn._DicParams.Add("USEYN", USEYN);
                    DBConn._DicParams.Add("RK", RK_P);
                    DBConn._DicParams.Add("CUSER", LoginUser.USRCD);
                    DataTable dt = DBConn.GetDataTable(PROCEDURE_ID, DBConn._DicParams);
                    if (dt != null)
                    {
                        if (dt.Rows[0]["RESULT"].ToString().Equals("1"))
                        {
                            if (string.IsNullOrEmpty(_PGMID))
                            {
                                UserPgm();
                                GroupPgm();
                            }

                            if (string.IsNullOrEmpty(_USRCD) && string.IsNullOrEmpty(_PGMID))
                                gp_SetLogInfo(Name, Text, CONNECT_TYPE.등록);
                            else
                                gp_SetLogInfo(Name, Text, CONNECT_TYPE.수정);

                            gp_PrintMessage(dt.Rows[0]["MSG"].ToString(), Text, MessageType.알림);
                            DataSendEvent(_Type, _PGMID);
                            // 연속저장
                            if (Ck_Mode.Checked)
                            {
                                INIT();
                                Tx_PGMID.Focus();
                            }
                            // 저장
                            else
                                Close();

                        }
                        else if (dt.Rows[0]["RESULT"].ToString().Equals("0"))
                            gp_PrintMessage(dt.Rows[0]["MSG"].ToString(), Text, MessageType.오류);
                    }
                }
                catch (Exception ex)
                {
                    gp_PrintMessage(ex.Message, Text, MessageType.오류);
                }
            }
        }

        // 사용자 추가 시 사용자 권한정보 저장
        private void LvSave(string USRCD)
        {
            DBConn._DicParams.Add("CMD", "PgmInfo");
            DataTable dtP = DBConn.GetDataTable(PROCEDURE_ID, DBConn._DicParams);
            StringBuilder strSql = new StringBuilder();
            if (dtP.Rows.Count > 0)
            {
                foreach (DataRow dr in dtP.Rows)
                {
                    string PGMID = dr["PGMID"]?.ToString();

                    strSql.Clear();
                    strSql.AppendLine(" INSERT INTO zPGMAUT  (USRCD, PGMID, USEYN, ADD_Y, UPD_Y, XLS_Y, PRT_Y, FAVID, FAVSQ, CDATE, CUSER)");
                    strSql.AppendLine(" VALUES ( '" + USRCD + "' ");
                    strSql.AppendLine("             , '" + PGMID + "' ");
                    strSql.AppendLine("             , 'Y' ");
                    strSql.AppendLine("             , 'Y' ");
                    strSql.AppendLine("             , 'Y' ");
                    strSql.AppendLine("             , 'Y' ");
                    strSql.AppendLine("             , 'Y' ");
                    strSql.AppendLine("             , 'N' ");
                    strSql.AppendLine("             , '0' ");
                    strSql.AppendLine("             , CONVERT(VARCHAR(20), GETDATE(), 20) ");
                    strSql.AppendLine("             , '" + LoginUser.USRCD + "') ");
                    strSql.AppendLine(" SELECT '" + USRCD + "' ");
                    strSql.AppendLine(" , PGMID, USEYN, ADD_Y, UPD_Y, DEL_Y, XLS_Y, PRT_Y ");
                    strSql.AppendLine(" FROM zPGMAUT ");
                    DataTable dtR = DBConn.GetDataTable(DBConn.dbCon, strSql.ToString());
                }
            }

            //// 현재 그룹권한관리 없음
            //string USLVL = Lk_USLVL.EditValue?.ToString();
            //Dictionary<string, object> dicParamsP = new Dictionary<string, object>();
            //DBConn._DicParams_Object.Add("CMD", "PgmInfo_Save");
            //DBConn._DicParams_Object.Add("USLVL", USLVL);
            //DataTable dtP = DBConn.GetDataTable("usp_SY020_U", DBConn._DicParams_Object);
            //StringBuilder strSql = new StringBuilder();

            //if (dtP.Rows.Count > 0)
            //{
            //    for (int i = 0; i < dtP.Rows.Count; i++)
            //    {
            //        string PGMID = dtP.Rows[i]["PGMID"].ToString();
            //        string USE_Y = dtP.Rows[i]["USE_Y"].ToString();

            //        string IUDPX = dtP.Rows[i]["IUDPX"].ToString();
            //        string ADD_Y = IUDPX[0].ToString();
            //        string UPD_Y = IUDPX[1].ToString();
            //        string DEL_Y = IUDPX[2].ToString();
            //        string PRT_Y = IUDPX[3].ToString();
            //        string XLS_Y = IUDPX[4].ToString();

            //        strSql.Clear();
            //        strSql.AppendLine(" MERGE INTO zPGMAUT AS A ");
            //        strSql.AppendLine(" USING ( SELECT USRCD = '" + USRCD + "' ");
            //        strSql.AppendLine("              , PGMID = '" + PGMID + "'");
            //        strSql.AppendLine("                ) AS B");
            //        strSql.AppendLine(" ON (A.USRCD = B.USRCD AND A.PGMID = B.PGMID) ");
            //        strSql.AppendLine(" WHEN MATCHED THEN UPDATE SET ");
            //        strSql.AppendLine("                         USE_Y = '" + USE_Y + "'");
            //        strSql.AppendLine("                       , IUDPX = '" + IUDPX + "'");
            //        strSql.AppendLine("                       , MDATE = CONVERT(VARCHAR(20), GETDATE(), 120) ");
            //        strSql.AppendLine("                       , MUSER = '" + LoginUser.USRCD + "'");
            //        strSql.AppendLine(" WHEN NOT MATCHED THEN");
            //        strSql.AppendLine(" INSERT (USRCD, PGMID, USE_Y, IUDPX, CUSER)");
            //        strSql.AppendLine(" VALUES ( '" + USRCD + "' ");
            //        strSql.AppendLine("             , '" + PGMID + "' ");
            //        strSql.AppendLine("             , '" + USE_Y + "' ");
            //        strSql.AppendLine("             , '" + IUDPX + "' ");
            //        strSql.AppendLine("             , '" + LoginUser.USRCD + "'); ");
            //        strSql.AppendLine(" SELECT '" + USRCD + "' ");
            //        strSql.AppendLine(" , PGMID, USE_Y ");
            //        strSql.AppendLine(" FROM zPGMAUT ");
            //        DataTable dtR = DBConn.GetDataTable(strSql.ToString());
            //    }
            //}
        }

        // 프로그램 추가 시 사용자 권한관리에 추가
        private void UserPgm()
        {
            string sPgmId = Tx_PGMID.EditValue?.ToString();
            DBConn._DicParams.Add("CMD", "UserPgmInfo_Save");
            DataTable dtP = DBConn.GetDataTable(PROCEDURE_ID, DBConn._DicParams);
            StringBuilder strSql = new StringBuilder();

            for (int i = 0; i < dtP.Rows.Count; i++)
            {
                strSql.Clear();
                strSql.AppendLine(" INSERT INTO zPGMAUT  (USRCD, PGMID, USEYN, ADD_Y, UPD_Y, DEL_Y, PRT_Y, XLS_Y, FAVID, FAVSQ, CDATE, CUSER)");
                strSql.AppendLine(" VALUES ( '" + dtP.Rows[i]["USRCD"].ToString() + "' ");
                strSql.AppendLine("             , '" + sPgmId + "' ");
                strSql.AppendLine("             , 'Y' ");
                strSql.AppendLine("             , 'Y' ");
                strSql.AppendLine("             , 'Y' ");
                strSql.AppendLine("             , 'Y' ");
                strSql.AppendLine("             , 'Y' ");
                strSql.AppendLine("             , 'Y' ");
                strSql.AppendLine("             , 'N' ");
                strSql.AppendLine("             , '0' ");
                strSql.AppendLine("             , CONVERT(VARCHAR(20), GETDATE(), 20) ");
                strSql.AppendLine("             , '" + LoginUser.USRCD + "') ");

                strSql.AppendLine(" SELECT USRCD ");
                strSql.AppendLine(" , '" + sPgmId + "' ");
                strSql.AppendLine(" , USEYN, ADD_Y, UPD_Y, DEL_Y, XLS_Y, PRT_Y ");
                strSql.AppendLine(" FROM zPGMAUT ");

                DataTable dtR = DBConn.GetDataTable(DBConn.dbCon, strSql.ToString());
            }
        }

        // 프로그램 추가 시 그룹 권한관리에 추가
        private void GroupPgm()
        {
            string sPgmId = Tx_PGMID.EditValue?.ToString();
            DBConn._DicParams.Add("CMD", "GroupPgmInfo_Save");
            DataTable dtP = DBConn.GetDataTable(PROCEDURE_ID, DBConn._DicParams);
            StringBuilder strSql = new StringBuilder();

            for (int i = 0; i < dtP.Rows.Count; i++)
            {
                strSql.Clear();
                strSql.AppendLine(" INSERT INTO zLVLAUT  (LVLCD, PGMID, USEYN, ADD_Y, UPD_Y, DEL_Y,  PRT_Y, XLS_Y, CDATE, CUSER)");
                strSql.AppendLine(" VALUES ( '" + dtP.Rows[i]["LVLCD"].ToString() + "' ");
                strSql.AppendLine("             , '" + sPgmId + "' ");
                strSql.AppendLine("             , 'Y' ");
                strSql.AppendLine("             , 'Y' ");
                strSql.AppendLine("             , 'Y' ");
                strSql.AppendLine("             , 'Y' ");
                strSql.AppendLine("             , 'Y' ");
                strSql.AppendLine("             , 'Y' ");
                strSql.AppendLine("             , CONVERT(VARCHAR(20), GETDATE(), 20) ");
                strSql.AppendLine("             , '" + LoginUser.USRCD + "') ");

                strSql.AppendLine(" SELECT LVLCD ");
                strSql.AppendLine(" , '" + sPgmId + "' ");
                strSql.AppendLine(" , USEYN, ADD_Y, UPD_Y, DEL_Y, XLS_Y, PRT_Y ");
                strSql.AppendLine(" FROM zLVLAUT ");

                DataTable dtR = DBConn.GetDataTable(DBConn.dbCon, strSql.ToString());
            }
        }

        // 암호초기화
        private void RESET()
        {
            if (!gp_PrintQuestion("암호를 초기화 하시겠습니까?", Text, MessageType.질문))
                return;

            DBConn._DicParams.Clear();
            DBConn._DicParams.Add("CMD", "PW_RESET");
            DBConn._DicParams.Add("USRCD", _USRCD);
            DataTable dt = DBConn.GetDataTable(PROCEDURE_ID, DBConn._DicParams);
            if (dt != null)
                gp_PrintMessage("암호가 초기화되었습니다", Text, MessageType.알림);
        }

        // 프로그램 ID 중복체크
        private void PROGRAM_CHECK()
        {
            string PGMID = Tx_PGMID.EditValue?.ToString();

            if (string.IsNullOrEmpty(PGMID))
            {
                gp_PrintMessage("프로그램ID를 입력해주세요.", Text, MessageType.알림);
                Tx_PGMID.Focus();
                return;
            }

            DBConn._DicParams.Add("CMD", "PID_Check");
            DBConn._DicParams.Add("PGMID", PGMID);
            DataTable dt = DBConn.GetDataTable(PROCEDURE_ID, DBConn._DicParams);
            if (dt.Rows.Count > 0)
            {
                gp_PrintMessage("이미 존재하는 프로그램 ID입니다.", Text, MessageType.경고);
                Tx_PGMID.Focus();
                Tx_PGMID.SelectAll();
                return;
            }
            else
            {
                gp_PrintMessage("사용 가능한 프로그램 ID입니다.", Text, MessageType.알림);
                Tx_PGMID.Enabled = false;
                Tx_PGMNM.Focus();
            }
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (xtraTabControl1.SelectedTabPage.Tag.Equals("USER")) { _Type = SaveType.사용자; }
            else if (xtraTabControl1.SelectedTabPage.Tag.Equals("PROGRAM")) { _Type = SaveType.프로그램; }

            // 수정모드일 시, 페이지 고정
            if (!string.IsNullOrEmpty(_USRCD)) { xtraTabControl1.SelectedTabPageIndex = 0; }
            if (!string.IsNullOrEmpty(_PGMID)) { xtraTabControl1.SelectedTabPageIndex = 1; }
        }

        // 도장 추가 / 삭제
        private void layoutControlGroup6_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            // +
            if (e.Button.Properties.Tag.Equals("ADD"))
            {
                var fileContent = string.Empty;
                var filePath = string.Empty;

                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.InitialDirectory = "c:\\";
                    openFileDialog.Filter = "image files (*.jpg;*.jpeg;*.gif;*.png;*.jpg;*.bmp)|*.jpg;*.jpeg;*.gif;*.png;*.jpg;*.bmp|All files (*.*)|*.*";
                    openFileDialog.FilterIndex = 0;
                    openFileDialog.RestoreDirectory = true;

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        filePath = openFileDialog.FileName;
                        var fileStream = openFileDialog.OpenFile();

                        using (StreamReader reader = new StreamReader(fileStream))
                        {
                            Pe_USIGN.Image = new Bitmap(openFileDialog.FileName);
                            Pe_USIGN.Tag = openFileDialog.FileName;
                        }
                    }
                }
            }
            // -
            else if (e.Button.Properties.Tag.Equals("DEL"))
            {
                Pe_USIGN.Image = null;
            }
            else
                return;
        }

        #region [ 키 이벤트 ] 
        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            //Main_Test frm = (Main_Test)this.MdiParent;
            //SimpleButton[] sbt = new SimpleButton[] { frm.Bt_Retr, frm.Bt_Add, frm.Bt_Delete, frm.Bt_Xls, frm.Bt_Close };
            SimpleButton[] sbt = new SimpleButton[] { Bt_Reset, Bt_Save, Bt_Close };
            gp_CommonButtonKeyEvent(sbt, e);
        }
        #endregion
    }
}