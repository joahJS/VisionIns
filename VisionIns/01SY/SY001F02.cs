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

namespace VisionIns
{
    public partial class SY001F02 : DevExpress.XtraEditors.XtraForm
    {
        public SY001F02()
        {
            InitializeComponent();
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
        }

        private string PROCEDURE_ID = "DP_SY001F02";
        private string PROCEDURE_SV = "DP_SY001F02";

        public SY001F00 P_SY005F00; // 이건 CM00F00의 Add에도 있음
        public string AddModGb { get; set; } // ADD-MOD 구분
        public string Rcdtp { get; set; } // 대분류코드
        public delegate void SendDataHandler(string sArrValue);
        public event SendDataHandler DataSendEvent;
        public string CvCode { get; set; }

        private void CM007F02_Load(object sender, EventArgs e)
        {
            ComnEtcFunc.gp_SetColorFocused(layoutControl1);
            if (AddModGb.Equals("MOD")) // 수정기능일 때
            {
                if (string.IsNullOrEmpty(Rcdtp))
                {
                    XtraMessageBox.Show("대분류코드가 존재하지 않습니다.");
                    DialogResult = DialogResult.Cancel;
                    return;
                }

                Bt_Continue.Enabled = false;

                DataTable dt = GetRefInfo(Rcdtp);
                if (dt.Rows.Count > 0)
                {
                    string sRcdtp = dt.Rows[0]["RCDTP"]?.ToString();
                    string sRefNo = dt.Rows[0]["REFNO"]?.ToString();
                    string sRefNm = dt.Rows[0]["REFNM"]?.ToString();
                    string sUseyn = dt.Rows[0]["USEYN"]?.ToString();
                    string sUptyn = dt.Rows[0]["UPDYN"]?.ToString();
                    string sRef1 = dt.Rows[0]["REFCD1"]?.ToString();
                    string sRef2 = dt.Rows[0]["REFCD2"]?.ToString();
                    string sRef3 = dt.Rows[0]["REFCD3"]?.ToString();
                    string sPrtsq = dt.Rows[0]["PRTSQ"]?.ToString();
                    string sRk = dt.Rows[0]["RK"]?.ToString();
                    string sCUser = dt.Rows[0]["CUSER"]?.ToString();
                    string sCDate = dt.Rows[0]["CDATE"]?.ToString();
                    string sMUser = dt.Rows[0]["MUSER"]?.ToString();
                    string sMDate = dt.Rows[0]["MDATE"]?.ToString();

                    TxtRcdtp.EditValue = sRcdtp;
                    TxtRefnm.EditValue = sRefNm;
                    Ck_UseYn.EditValue = sUseyn;
                    Ck_UptYn.EditValue = sUptyn;
                    TxtRef1.EditValue = sRef1;
                    TxtRef2.EditValue = sRef2;
                    TxtRef3.EditValue = sRef3;
                    TxtPrtsq.EditValue = sPrtsq;
                    TxtRk.EditValue = sRk;

                    Bt_Save.TabStop = true;
                    TxtRcdtp.Properties.ReadOnly = true;
                    TxtRcdtp.Enabled = false;
                    TxtRcdtp.TabStop = false;
                }
                if (P_SY005F00 != null) // 부모창이 있다면
                {
                    P_SY005F00.RST_RCDTP = Rcdtp; // P_CM000F00은 맨 위에 있음
                    // RST_RCDTP는 어딨는지 모르겠음. 밑에 하나 더 있음
                    // Rcdtp는 get set에 있음
                    // P_CM000F00은 맨 밑의 SaveAndExit로 다시 간다.
                }
            }
        }

        private DataTable GetRefInfo(string sRcdtp)
        { // 조회기능
            Dictionary<string, string> dicParams = new Dictionary<string, string>();
            dicParams.Add("CMD", "GET1");
            dicParams.Add("RCDTP", sRcdtp);
            return ComnEtcFunc.GetInfo(dicParams, PROCEDURE_ID);
        }

        private void Bt_Save_Click(object sender, EventArgs e)
        {
            SaveInfo("SAVE");
        }

        private void SaveInfo(string sGb)
        { //그룹코드 저장기능
            string sRcdtp = TxtRcdtp.EditValue?.ToString(); // 새로 생김
            if (string.IsNullOrEmpty(TxtRcdtp.EditValue?.ToString()))
            {
                XtraMessageBox.Show("대분류코드를 입력하세요.");
                TxtRcdtp.Focus();
                return;
            }
            else if (string.IsNullOrEmpty(TxtRefnm.EditValue?.ToString()))
            {
                XtraMessageBox.Show("분류명을 입력하세요.");
                TxtRefnm.Focus();
                return;
            }
            bool IsDup = IsKeyValueDup(sRcdtp);
            if (IsDup)
            { // 코드 중복 검사
                XtraMessageBox.Show("이미 등록되어 있는 코드 입니다. ");
                TxtRcdtp.Focus();
                TxtRcdtp.SelectAll();
                return;
            }

            try
            {
                sRcdtp = TxtRcdtp.EditValue?.ToString();
                string sRefNo = "?";
                string sRefNm = TxtRefnm.EditValue?.ToString();
                string sUseYn = Ck_UseYn.EditValue?.ToString();
                string sUptYn = Ck_UptYn.EditValue?.ToString();
                double dPrtsq = string.IsNullOrEmpty(TxtPrtsq.EditValue?.ToString()) ? 0 :
                    Convert.ToDouble(TxtPrtsq.EditValue?.ToString());
                string sRef1 = TxtRef1.EditValue?.ToString();
                string sRef2 = TxtRef2.EditValue?.ToString();
                string sRef3 = TxtRef3.EditValue?.ToString();
                string sRk = TxtRk.EditValue?.ToString();
                string sUser = LoginUser.USRCD; // 유저코드

                Dictionary<string, string> dicParams = new Dictionary<string, string>();
                dicParams.Add("CMD", "SAVE");
                dicParams.Add("RCDTP", sRcdtp);
                dicParams.Add("REFNO", sRefNo);
                dicParams.Add("REFNM", sRefNm);
                dicParams.Add("USEYN", sUseYn);
                dicParams.Add("UPDYN", sUptYn);
                dicParams.Add("PRTSQ", dPrtsq.ToString());
                dicParams.Add("REF1", sRef1);
                dicParams.Add("REF2", sRef2);
                dicParams.Add("REF3", sRef3);
                dicParams.Add("RK", sRk);
                dicParams.Add("USRID", sUser);

                DataTable dtResult = DBConn.GetDataTable(DBConn.dbCon, PROCEDURE_SV, dicParams);
                string sErrorGb = string.Empty;

                if (dtResult.Rows.Count > 0)
                {
                    sErrorGb = dtResult.Rows[0]["RESULT"].ToString();
                    string sMsg = dtResult.Rows[0]["MSG"].ToString();
                    XtraMessageBox.Show(sMsg);

                    if (sErrorGb.Equals("1"))
                    {
                        if (sGb.Equals("SAVE"))
                        {
                            if (P_SY005F00 != null)
                            {
                                P_SY005F00.sReturn = sRcdtp;
                            }
                            DialogResult = DialogResult.OK;
                        }
                        else if (sGb.Equals("SaveAndNew"))
                        {
                            if (AddModGb.Equals("MOD"))
                            {
                                ComnEtcFunc.ClearAllForm(this);
                                AddModGb = "ADD";
                                TxtRcdtp.Focus();
                            }
                            else if (AddModGb.Equals("ADD"))
                            {
                                ComnEtcFunc.ClearAllForm(this);
                                TxtRcdtp.Focus();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private bool IsKeyValueDup(string sKeyValue)
        { // 기본키 중복 검사
            bool IsDuplicated = false;

            StringBuilder sb = new StringBuilder();

            sb.AppendLine(" SELECT RCDTP                       ");
            sb.AppendLine("   FROM REFFPF                   ");
            sb.AppendLine("  WHERE RCDTP = '" + sKeyValue + "' ");
            if (AddModGb.Equals("MOD"))
            {
                sb.AppendLine("  AND RCDTP <> '" + Rcdtp + "' ");
            }
            DataTable dt = DBConn.GetDataTable(DBConn.dbCon, sb.ToString());

            if (dt.Rows.Count > 0)
                IsDuplicated = true;
            else
                IsDuplicated = false;

            return IsDuplicated;
        }

        private void Bt_Continue_Click(object sender, EventArgs e)
        {
            SaveInfo("SaveAndNew");
        }

        private void Bt_Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        public static void ClearAllForm(Control control)
        { // 초기화 기능
            foreach (Control ctrl in control.Controls)
            {
                TextEdit te = ctrl as TextEdit;
                if (te != null)
                {
                    te.ResetText();
                }
                ClearAllForm(ctrl);
            }
        }

        private void CM007F02_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                Bt_Refresh_Click(null, null);
            }
            else if (e.KeyCode == Keys.F2)
            {
                Bt_Continue_Click(null, null);
            }
            else if (e.KeyCode == Keys.F3)
            {
                Bt_Save_Click(null, null);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Bt_Close.PerformClick();
            }
        }

        private void Bt_Refresh_Click(object sender, EventArgs e)
        {
            ClearAllForm(this);
            Ck_UseYn.EditValue = "Y";
            Ck_UptYn.EditValue = "Y";
            if (AddModGb.Equals("MOD"))
            {
                TxtRcdtp.Properties.ReadOnly = false;
                TxtRcdtp.Enabled = true;
                TxtRcdtp.TabStop = true;
                TxtRcdtp.Focus();
                Bt_Continue.Enabled = true;
                Bt_Continue.TabStop = true;
                Bt_Save.TabStop = false;
                AddModGb = "ADD";
            }
        }
    }
}