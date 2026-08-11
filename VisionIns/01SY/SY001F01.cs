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
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;

namespace VisionIns
{
    public partial class SY001F01 : DevExpress.XtraEditors.XtraForm
    {
        private string PROCEDURE_ID = "DP_SY001F01";

        public SY001F01()
        {
            InitializeComponent();
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
        }

        public SY001F00 P_SY005F00;
        public string AddModGb { get; set; }
        public string Rcdtp { get; set; } // 대분류코드
        public string Refno { get; set; } // 분류코드        
        public string Refnm { get; set; } // 분류명   
        public delegate void SendDataHandler(string[] sArrValue);
        public event SendDataHandler DataSendEvent;

        private void CM007F01_Load(object sender, EventArgs e)
        {
            ComnEtcFunc.gp_SetColorFocused(layoutControl1);
            TxtRcdtp.EditValue = Rcdtp;

            if (AddModGb.Equals("MOD"))
            {
                if (string.IsNullOrEmpty(Rcdtp))
                {
                    XtraMessageBox.Show("그룹코드가 존재하지 않습니다.");
                    DialogResult = DialogResult.Cancel;
                    return;
                }
                if (string.IsNullOrEmpty(Refno))
                {
                    XtraMessageBox.Show("항목코드가 존재하지 않습니다.");
                    DialogResult = DialogResult.Cancel;
                    return;
                }
                DataTable dt = GetRefDtl(Rcdtp, Refno);
                if (dt.Rows.Count > 0)
                {
                    string sRcdtp = dt.Rows[0]["RCDTP"]?.ToString();
                    string sRefNo = dt.Rows[0]["REFNO"]?.ToString();
                    string sRefNm = dt.Rows[0]["REFNM"]?.ToString();
                    string sUseYn = dt.Rows[0]["USEYN"]?.ToString();
                    string sUptYn = dt.Rows[0]["UPDYN"]?.ToString();
                    string sBasYn = dt.Rows[0]["BASYN"]?.ToString();
                    string sRef1 = dt.Rows[0]["REFCD1"]?.ToString();
                    string sRef2 = dt.Rows[0]["REFCD2"]?.ToString();
                    string sRef3 = dt.Rows[0]["REFCD3"]?.ToString();
                    string sPrtsq = dt.Rows[0]["PRTSQ"]?.ToString();
                    string sRk = dt.Rows[0]["RK"]?.ToString();

                    Ck_UseYn.EditValue = sUseYn;
                    Ck_UptYn.EditValue = sUptYn;
                    Ck_BasYn.EditValue = sBasYn;
                    TxtRcdtp.EditValue = sRcdtp;
                    TxtRefno.EditValue = sRefNo;
                    TxtRefnm.EditValue = sRefNm;
                    TxtPrtsq.EditValue = sPrtsq;
                    TxtRef1.EditValue = sRef1;
                    TxtRef2.EditValue = sRef2;
                    TxtRef3.EditValue = sRef3;
                    TxtRk.EditValue = sRk;


                    Bt_Continue.Enabled = false;

                    Bt_Save.TabStop = true;
                    TxtRcdtp.Properties.ReadOnly = true;
                    TxtRcdtp.Enabled = false;
                    TxtRcdtp.TabStop = false;
                    TxtRefno.Properties.ReadOnly = true;
                    TxtRefno.Enabled = false;
                    TxtRefno.TabStop = false;
                }
                if (P_SY005F00 != null)
                {
                    P_SY005F00.RST_RCDTP = Rcdtp;
                }
            }
            else if (AddModGb.Equals("ADD"))
            {
                TxtRcdtp.ReadOnly = true;
                TxtRcdtp.TabStop = false;
            }
        }
        private DataTable GetRefDtl(string sRcdtp, string sRefNo)
        {
            Dictionary<string, string> dicParams = new Dictionary<string, string>();

            dicParams.Add("CMD", "GET1");
            dicParams.Add("RCDTP", sRcdtp);
            dicParams.Add("REFNO", sRefNo);

            return ComnEtcFunc.GetInfo(dicParams, PROCEDURE_ID);
        }

        private void Bt_Refresh_Click(object sender, EventArgs e)
        {
            string saveRcdtp = TxtRcdtp.EditValue?.ToString();
            ClearAllForm(this);
            TxtRcdtp.EditValue = saveRcdtp;
            Ck_UseYn.EditValue = "Y";
            Ck_UptYn.EditValue = "Y";
            TxtRefno.Properties.ReadOnly = false;
            TxtRefno.Enabled = true;
            TxtRefno.TabStop = true;
            TxtRefno.Focus();

            TxtRefno.Properties.ReadOnly = false;
            TxtRefno.Enabled = true;
            TxtRefno.TabStop = true;
            TxtRefno.Focus();

            Bt_Continue.Enabled = true;
            Bt_Continue.TabStop = true;
            Bt_Save.TabStop = false;
            AddModGb = "ADD";
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

        private void Bt_Continue_Click(object sender, EventArgs e)
        {
            SaveRefDtl("SaveAndNew");
        }
        private void Bt_Save_Click(object sender, EventArgs e)
        {
            SaveRefDtl("SAVE");
        }

        private void SaveRefDtl(string sGb)
        {
            string sRcdtp = TxtRcdtp.EditValue?.ToString();
            string sRefno = TxtRefno.EditValue?.ToString();
            string sRefnm = TxtRefnm.EditValue?.ToString();

            bool IsDup = IsKeyValueDup(sRcdtp, sRefno);
            if (IsDup)
            {
                XtraMessageBox.Show("이미 있는 항목코드 값입니다.");
                TxtRefno.Focus();
                TxtRefno.SelectAll();
                return;
            }
            if (string.IsNullOrEmpty(TxtRefno.EditValue?.ToString()))
            {
                XtraMessageBox.Show("분류코드를 입력하세요.");
                TxtRefno.Focus();
                return;
            }
            if (TxtRefno.EditValue?.ToString() == "?")
            {
                XtraMessageBox.Show("잘못된 코드 값입니다.");
                TxtRefno.Focus();
                return;
            }
            if (string.IsNullOrEmpty(TxtRefnm.EditValue?.ToString()))
            {
                XtraMessageBox.Show("분류명을 입력하세요.");
                TxtRefnm.Focus();
                return;
            }

            try
            {
                string sUseYn = Ck_UseYn.EditValue?.ToString();
                string sUptYn = Ck_UptYn.EditValue?.ToString();
                string sBasYn = Ck_BasYn.EditValue?.ToString();
                double dPrtSq = string.IsNullOrEmpty(TxtPrtsq.EditValue?.ToString()) ? 0 :
                    Convert.ToDouble(TxtPrtsq.EditValue?.ToString());
                string sRef1 = TxtRef1.EditValue?.ToString();
                string sRef2 = TxtRef2.EditValue?.ToString();
                string sRef3 = TxtRef3.EditValue?.ToString();
                string sRk = TxtRk.EditValue?.ToString();
                string sUser = LoginUser.USRCD;

                Dictionary<string, string> dicParams = new Dictionary<string, string>();
                dicParams.Add("CMD", "SAVE");
                dicParams.Add("RCDTP", sRcdtp);
                dicParams.Add("REFNO", sRefno);
                dicParams.Add("REFNM", sRefnm);
                dicParams.Add("USEYN", sUseYn);
                dicParams.Add("UPDYN", sUptYn);
                dicParams.Add("BASYN", sBasYn);
                dicParams.Add("PRTSQ", dPrtSq.ToString());
                dicParams.Add("REF1", sRef1);
                dicParams.Add("REF2", sRef2);
                dicParams.Add("REF3", sRef3);
                dicParams.Add("RK", sRk);
                dicParams.Add("USRID", sUser);

                DataTable dtResult = DBConn.GetDataTable(DBConn.dbCon, PROCEDURE_ID, dicParams);
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
                            string[] arrReturn = { sRcdtp, sRefno };
                            DataSendEvent(arrReturn);
                            DialogResult = DialogResult.OK;
                        }
                        else if (sGb.Equals("SaveAndNew"))
                        {
                            if (AddModGb.Equals("MOD"))
                            {
                                if (XtraMessageBox.Show("새로운 거래처정보를 등록합니까?",
                                                "거래처 정보추가", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                {
                                    ComnEtcFunc.ClearAllForm(this);
                                    AddModGb = "ADD";
                                    TxtRefno.Focus();
                                }
                            }
                            else if (AddModGb.Equals("ADD"))
                            {
                                ComnEtcFunc.ClearAllForm(this);
                                TxtRcdtp.EditValue = Rcdtp;
                                TxtPrtsq.Focus();
                            }
                        }
                    }
                    else
                    {
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private bool IsKeyValueDup(string sKeyValue1, string sKeyValue2)
        {
            bool IsDuplicated = false;

            StringBuilder sb = new StringBuilder();

            sb.AppendLine(" SELECT REFNO                        ");
            sb.AppendLine("   FROM REFFPF                       ");
            sb.AppendLine("  WHERE RCDTP = '" + sKeyValue1 + "' ");
            sb.AppendLine("    AND REFNO = '" + sKeyValue2 + "' ");
            if (AddModGb.Equals("MOD"))
            {
                sb.AppendLine("    AND REFNO <> '" + Refno + "' ");
            }

            DataTable dt = DBConn.GetDataTable(DBConn.dbCon, sb.ToString());

            if (dt.Rows.Count > 0)
                IsDuplicated = true;
            else
                IsDuplicated = false;

            return IsDuplicated;
        }

        private void Bt_Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CM007F01_KeyDown(object sender, KeyEventArgs e)
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

    }
}