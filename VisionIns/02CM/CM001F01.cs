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
using System.IO;
using System.Text.RegularExpressions;
using static ComnFunc;
using static GridFunc;

namespace VisionIns
{
    public partial class CM001F01 : DevExpress.XtraEditors.XtraForm
    {
        private string PROCEDURE_ID = "DP_CM001F01";

        public enum ADDMOD { ADD, MOD }
        public ADDMOD _ADDMOD;
        private enum AfterSAVE { NEW, EXIT }
        public string _ITCOD;
        public delegate void SendDataHandler(string sVal);
        public event SendDataHandler DataRowSendEvent;

        public CM001F01()
        {
            InitializeComponent();
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
        }

        #region [화면 로드 및 조회]

        private void CM001F01_Load(object sender, EventArgs e)
        {
            //Init();
            SetValue();
        }
        private void Init()
        {
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            ComnEtcFunc.gp_SetColorFocused(layoutControl1);
        }
        private void SetValue()
        {
            if (_ADDMOD.Equals(ADDMOD.MOD)) // 수정
            {
                if (string.IsNullOrEmpty(_ITCOD))
                {
                    XtraMessageBox.Show("품번이 존재하지 않습니다."); // 기본키
                    DialogResult = DialogResult.Cancel;
                    return;
                }
                Tx_ITCOD.EditValue = _ITCOD;

                DataTable dt = GetPumMokInfo(_ITCOD);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        Tx_ITCOD.EditValue = dt.Rows[0]["ITCOD"]?.ToString();
                        Tx_ITNAM.EditValue = dt.Rows[0]["ITNAM"]?.ToString();
                        Tx_ISPEC.EditValue = dt.Rows[0]["ISPEC"]?.ToString();
                        Tx_IMGPATH.EditValue = dt.Rows[0]["IMGPATH"]?.ToString();
                        Ck_USEYN.EditValue = dt.Rows[0]["USEYN"]?.ToString();
                        
                        byte[] Img = Convert.IsDBNull(dt.Rows[0]["ITIMG"]) ? null : (byte[])dt.Rows[0]["ITIMG"];
                        Pic_ItCod.Image = byteArrayToImage(Img);
                    }
                }
                Bt_SaveMul.Enabled = false;
                Tx_ITNAM.Select();
            }
            else // 추가
            {
                Init();
                Ck_USEYN.EditValue = "Y";
            }
            
        }

        // 바이트 변수를 이미지 변수로 변환 메서드
        private Image byteArrayToImage(byte[] byteArrayIn)
        {
            Image returnImage = null;
            try
            {
                MemoryStream ms = new MemoryStream(byteArrayIn, 0, byteArrayIn.Length);
                ms.Write(byteArrayIn, 0, byteArrayIn.Length);
                returnImage = Image.FromStream(ms, true);
            }
            catch
            {

            }
            return returnImage;
        }

        private DataTable GetPumMokInfo(string itcod)
        {
            Dictionary<string, string> dicParams = new Dictionary<string, string>();
            dicParams.Clear();
            dicParams.Add("CMD", "ITCOD_BOUND");
            dicParams.Add("ITCOD", itcod);
            DataTable dt = DBConn.GetDataTable(DBConn.dbCon, PROCEDURE_ID, dicParams);
            return dt;
        }
        #endregion

        #region [저장 & 초기화]

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            ComnEtcFunc.ClearAllForm(this);
            _ADDMOD = ADDMOD.ADD;
            Bt_SaveMul.Enabled = true;
        }

        private void BtnContinue_Click(object sender, EventArgs e)
        {
            SaveRefInfo(AfterSAVE.NEW);
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            SaveRefInfo(AfterSAVE.EXIT);
        }

        // 저장(F3) / 연속저장(F2)
        private void SaveRefInfo(AfterSAVE afterSAVE)
        {
            string ITCOD = Tx_ITCOD.EditValue?.ToString();
            string ITNAM = Tx_ITNAM.EditValue?.ToString();
            string ISPEC = Tx_ISPEC.EditValue?.ToString();
            string IMGPATH = Tx_IMGPATH.EditValue?.ToString();
            string USEYN = Ck_USEYN.EditValue?.ToString();

            byte[] upImage = null;
            if(Pic_ItCod.Image != null)
                upImage = ComnEtcFunc.ImageToByteArray(Pic_ItCod.Image);

            if (string.IsNullOrEmpty(ITNAM))
            {
                XtraMessageBox.Show("품명을 입력해주세요", "품목등록");
                Tx_ITNAM.Focus();
                return;
            }
            
            try
            {
                Dictionary<string, object> dicParams = new Dictionary<string, object>();
                dicParams.Clear();
                dicParams.Add("CMD", "SAVE");
                dicParams.Add("ITCOD", ITCOD);
                dicParams.Add("ITNAM", ITNAM);
                dicParams.Add("ISPEC", ISPEC);
                dicParams.Add("IMGPATH", IMGPATH);
                dicParams.Add("ITIMG", upImage);
                dicParams.Add("USEYN", USEYN);
                dicParams.Add("CUSER", LoginUser.USRCD);
                
                DataTable dtResult = DBConn.GetDataTable(DBConn.dbCon, PROCEDURE_ID, dicParams);

                if (dtResult != null)
                {
                    if (dtResult.Rows.Count > 0)
                    {
                        string msg = dtResult.Rows[0]["MSG"]?.ToString();
                        XtraMessageBox.Show(msg);
                    }
                }
                DataRowSendEvent(ITCOD);
                if (afterSAVE.Equals(AfterSAVE.EXIT))
                {
                    Close();
                }
                else if (afterSAVE.Equals(AfterSAVE.NEW))
                {
                    ComnEtcFunc.ClearAllForm(this);
                    _ADDMOD = ADDMOD.ADD;
                    Bt_SaveMul.Enabled = true;
                    SetValue();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }
        
        #endregion

        #region [단축키 및 종료]
        private void CM001F01_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                Bt_Reset.PerformClick();
            }
            if (e.KeyCode == Keys.F2)
            {
                Bt_SaveMul.PerformClick();
            }
            if (e.KeyCode == Keys.F3)
            {
                Bt_Save.PerformClick();
            }
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion

        private void layoutControlGroup3_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Tag.Equals("ADD"))
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "Images Files(*.jpg; *.jpeg; *.gif; *.bmp; *.png)| *.jpg; *.jpeg; *.gif; *.bmp; *.png";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    Pic_ItCod.Image = new Bitmap(ofd.FileName);
                    Pic_ItCod.Tag = ofd.FileName;
                }
            }
            else if (e.Button.Properties.Tag.Equals("DEL"))
                Pic_ItCod.EditValue = null;
        }
    }
}