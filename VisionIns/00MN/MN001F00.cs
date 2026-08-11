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

namespace VisionIns
{
    public partial class MN001F00 : DevExpress.XtraEditors.XtraForm
    {
        public MN001F00()
        {
            InitializeComponent();
            //gp_SetColorFocused(layoutControl1);
            DBConn.dbCon = DBConn.DbConn();
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);

            bt_Login.ShowFocusRectangle = DevExpress.Utils.DefaultBoolean.False;
            bt_Close.ShowFocusRectangle = DevExpress.Utils.DefaultBoolean.False;

        }

        private void Login_FormLoad(object sender, EventArgs e)
        {
            // 유저정보 Load
            string RememerCheck = gp_GetIniValue(INI_NAME.USER, ComnString.INI_USER_SECTION_LOGIN, ComnString.INI_USER_SECTION_LOGIN_KEY_CHECK);
            if (RememerCheck.Equals("True"))
            {
                tx_ID.Text = gp_GetIniValue(INI_NAME.USER, ComnString.INI_USER_SECTION_LOGIN, ComnString.INI_USER_SECTION_LOGIN_KEY_ID);
                tx_PW.Text = gp_GetIniValue(INI_NAME.USER, ComnString.INI_USER_SECTION_LOGIN, ComnString.INI_USER_SECTION_LOGIN_KEY_PW);
                chk_Remember.Checked = true;
            }

            // 새 버전정보 가져오기 (22.11.22)
            // Application.ProductVersion : AssemblyInfo.cs에서 AssemblyFileVersion의 버전정보를 가져옴
            lb_Version.Text = string.Format("Ver.{0}", Application.ProductVersion);
        }        

        private void TryLogin(object sender, EventArgs e)
        {
            string ID = tx_ID.EditValue?.ToString();
            string PW = tx_PW.EditValue?.ToString();
            
            bool result = gp_Login(ID, PW);
            if (result)
            {
                if (chk_Remember.Checked)
                {
                    gp_SetIniValue(INI_NAME.USER
                        , ComnString.INI_USER_SECTION_LOGIN
                        , ComnString.INI_USER_SECTION_LOGIN_KEY_ID
                        , ID);
                    gp_SetIniValue(INI_NAME.USER
                        , ComnString.INI_USER_SECTION_LOGIN
                        , ComnString.INI_USER_SECTION_LOGIN_KEY_PW
                        , PW);
                    gp_SetIniValue(INI_NAME.USER
                        , ComnString.INI_USER_SECTION_LOGIN
                        , ComnString.INI_USER_SECTION_LOGIN_KEY_CHECK
                        , chk_Remember.Checked.ToString());
                }
                this.Visible = false;
                ComnString.INI_VERSION = lb_Version.Text;

                MN002F00 frm = new MN002F00();
                frm.Show();
            }
            else
            {
                XtraMessageBox.Show(ComnString.TXT_LOGIN_FAIL);
            }
        }

        private void ExitProgram(object sender, EventArgs e)
        {
            DBConn.DbDisConn(DBConn.dbCon);
            Dispose();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

    }
}