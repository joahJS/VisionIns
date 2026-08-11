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
using System.Diagnostics;
using DevExpress.XtraSplashScreen;
using System.Runtime.InteropServices;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace VisionIns_Update
{
    public partial class Version_Check_Form : DevExpress.XtraEditors.XtraForm
    {
        public Version_Check_Form()
        {
            InitializeComponent();
        }

        public string PROCEDURE_ID = "usp_SYS_VersionCheck";
        public string BIG_CATE = "VERSION";
        public string INIT_NAME = "version";

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        class iniUtil
        {
            private string iniPath;
            public iniUtil(string path)
            {
                this.iniPath = path;  //INI 파일 위치를 생성할때 인자로 넘겨 받음
            }

            public String GetIniValue(String Section, String Key)
            {
                StringBuilder temp = new StringBuilder(255);
                int i = GetPrivateProfileString(Section, Key, "", temp, 255, iniPath);
                return temp.ToString();
            }

            // INI 값을 셋팅
            public void SetIniValue(String Section, String Key, String Value)
            {
                WritePrivateProfileString(Section, Key, Value, iniPath);
            }
        }

        private void Version_Check_Form_Load(object sender, EventArgs e)
        {
            string internal_IP = GetInternalIP();
            string myIp = new WebClient().DownloadString("http://ipinfo.io/ip").Trim();
            if (string.IsNullOrEmpty(myIp)) //null 또는 빈값일때 Get Internal IP를 가져오게 한다.
            {
                myIp = GetInternalIP();
            }

            //if (myIp.Contains("210.223.249") && internal_IP.Contains("192.168.40.253")) //내부일때(서버)
            //{
            //    DBConn.sqlConnection = "server = localhost; database = MES_SNC2; Integrated Security = True;";
            //}
            //else if (myIp.Contains("210.223.249")) //내부일때(기본내부)
            //{
            //    DBConn.sqlConnection = "server = 192.168.40.253,1433; uid = pineit; pwd = pineit0401; database = MES_SNC2";
            //}
            //else //외부일때
            //{
                DBConn.sqlConnection = "server = 121.66.17.30,16433; uid = pineit; pwd = pineit0401; database = VISION_INS";
           // }

            SplashScreenManager.ShowForm(typeof(UpdateScreen), true, true);
            SplashScreenManager.CloseForm();
            DBConn.dbCon = DBConn.DbConn();

            string filePath = Application.StartupPath + @"\version.ini";
            Lb_VerChkMsg.Text = "최신버전 업데이트 체크를 진행하겠습니다.";

            iniUtil ini = new iniUtil(filePath);
            string CurVer = ini.GetIniValue(BIG_CATE, INIT_NAME);      // 현재 버전 담을 변수
            string VersionId = "";                                     // 최신 버전 담을 변수

            // 최신 버전 정보를 얻기 위한 프로시저 접근
            Dictionary<string, string> dicParams = new Dictionary<string, string>();
            dicParams.Add("CMD", "LAST_VERSION");

            // dicParams의 최신 버전을 체크하고, 더 높은 버전이 있다면 dt 반환
            DataTable dt = DBConn.GetDataTable(DBConn.dbCon, this.PROCEDURE_ID, dicParams);

            if (dt.Rows.Count > 0)
            {
                VersionId = dt.Rows[0]["VERSION_ID"]?.ToString();
            }

            if (!CheckVersion(filePath))
            {
                Lb_VerChkMsg.Text = "현재 버전은 최신 버전이 아닙니다.\n업데이트 버튼을 눌러 업데이트를 진행하세요.";
                Lb_VerInfo.Text = "현재 버전 : " + CurVer + "   →   최신 버전 : " + VersionId;
            }
            else
            {
                Lb_VerChkMsg.Text = "현재 버전은 최신버전입니다.\n로그인 화면으로 이동합니다. 잠시만 기다려주세요...";
                Lb_VerInfo.Text = "현재 버전 : " + VersionId + " ver.";
                Bt_Update.Visible = false;
                Bt_Close.Visible = false;
                DelaySystem(2000);
                CloseUpdate();
            }
        }

        private bool CheckVersion(string IniPath)
        {
            iniUtil ini = new iniUtil(IniPath);
            string Version = ini.GetIniValue(BIG_CATE, INIT_NAME);
            string UpdateYn = GetUpdateRemark(Version);
            if (!string.IsNullOrEmpty(UpdateYn))
            {
                if (UpdateYn.Equals("Y"))
                    return true;
                else
                    return false;
            }
            else
            {
                return false;
            }
        }

        private string GetUpdateRemark(string VersionId)
        {
            Lb_VerChkMsg.Text = "업데이트 체크를 진행중입니다...";

            Dictionary<string, string> dicParams = new Dictionary<string, string>();

            dicParams.Add("CMD", "VER_CHECK");
            dicParams.Add("VERSION_ID", VersionId);

            // 현재 버전(sVersionId)과 DB의 최신 버전이 같으면 N, 다르면 Y 반환
            DataTable dtResult = DBConn.GetDataTable(DBConn.dbCon, this.PROCEDURE_ID, dicParams);
            if (dtResult.Rows.Count > 0)
            {
                return dtResult.Rows?[0]["UPDATE_YN"]?.ToString();
            }
            else
            {
                return null;
            }
        }

        private void DelaySystem(int MS)
        { /* 함수명 : DelaySystem * 1000ms = 1초 * 전달인자 : 얼마나 지연시킬것인가에 대한 변수 * */
            DateTime dtAfter = DateTime.Now;        // 현재시간 할당
            TimeSpan dtDuration = new TimeSpan(0, 0, 0, 0, MS);     // 시간 간격을 저장, 이 코드에서는 2000밀리초
            DateTime dtThis = dtAfter.Add(dtDuration);          // dtThis에 현재시간(dtAfter)에 2000밀리초를 합한 시간을 할당
            while (dtThis >= dtAfter)       // dtAfter(현재시간)이 dtThis(딜레이 시간)에 도달할 때까지 반복
            {
                System.Windows.Forms.Application.DoEvents();  //현재 시간 얻어 오기
                dtAfter = DateTime.Now;
            }
        }

        private void CloseUpdate()
        {
            string path = string.Format(@"{0}\{1}", Application.StartupPath, "VISOIN_INS.exe");
            Application.Exit();
            Process.Start(path);
        }

        #region [ 업데이트 버튼 ]
        private void Bt_Update_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(UpdateScreen), true, false);

            try
            {
                byte[] file = null;         // 파일이 Byte 타입으로 DB에 저장되어 있음
                Dictionary<string, string> dicParams = new Dictionary<string, string>();

                dicParams.Add("CMD", "LAST_VERSION");

                // dicParams의 최신 버전을 체크하고, 더 높은 버전이 있다면 높은 버전의 dt.Row 반환
                DataTable dt = DBConn.GetDataTable(DBConn.dbCon, this.PROCEDURE_ID, dicParams);
                if (dt.Rows.Count > 0)
                {
                    string VersionId = dt.Rows[0]["VERSION_ID"]?.ToString();
                    file = (byte[])dt.Rows[0]["FILE_NO"];       // FILE_NO에 해당하는 파일을 할당
                    UpdateFile(file, VersionId);                // 할당받은 파일로 업데이트
                    DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm();
                    Version_Check_Form_Load(null, null);
                }
                else         // DB zSYS_VERSION에 버전 정보가 없음
                {
                    throw new Exception("DB에 버전이 존재하지 않습니다.\r\n관리자에게 문의하세요.");
                }
            }
            catch (Exception ex)
            {
                DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm();
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void UpdateFile(byte[] file, string VersionId)
        {
            string filePath = "";

            string fileDir = Application.StartupPath;
            filePath = fileDir + @"\VISOIN_INS.exe";
            FileStream fs;

            fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write);
            fs.Write(file, 0, file.Length);
            fs.Close();

            IniSet(BIG_CATE, INIT_NAME, VersionId);        // 파일 업데이트 후 ini파일의 버전정보 갱신
        }

        private void IniSet(string sBicCate, string key, string var)
        {
            string filePath = Application.StartupPath + @"\version.ini";
            iniUtil ini = new iniUtil(filePath);
            ini.SetIniValue(sBicCate, key, var);
        }
        #endregion

        private void Bt_Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private string GetInternalIP()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());

            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }

            throw new Exception("IPv4 주소를 찾을 수 없습니다.");
        }
        
    }
}