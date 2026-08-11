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
using DevExpress.XtraSplashScreen;
using DevExpress.XtraBars;
using System.Threading;
using DevExpress.XtraBars.Navigation;
using System.Runtime.InteropServices;
using static ComnFunc;
using System.IO;

namespace VisionIns
{
    public partial class MN002F00 : DevExpress.XtraEditors.XtraForm
    {
        #region [Field]
        Thread _TimeThread;
        private bool _ReLogin = false;
        #endregion

        public MN002F00()
        {
            InitializeComponent();
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            _main = this;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //gp_SetLogInfo(Name, Text, CONNECT_TYPE.접속);
            GetCurTime();
            barStaticItemVersion.Caption = ComnString.TXT_VERSION + Application.ProductVersion;
            barStaticItemUSRNM.Caption = ComnString.TXT_CONNECT_USER + LoginUser.USRNM;
            //if (DBConn.dbCon.Database.Contains("SCRAP"))
            //    barStaticItemDB.Caption = "★☆★ 개발테스트 서버 ★☆★";
            if (ComnString.CONNECTION_STRING.Contains("121.66.17.30"))
                barStaticItemDB.Caption = "★☆★ 개발테스트 서버 ★☆★";
            else if (ComnString.CONNECTION_STRING.Contains("localhost"))
                barStaticItemDB.Caption = "★☆★ 로컬 서버 ★☆★";

            // 계근 이미지 임시 저장 경로 삭제
            string tempFolder = string.Concat(Application.StartupPath, @"\image\");
            if (Directory.Exists(tempFolder))
                Directory.Delete(tempFolder, true);

            // 배경화면
            //this.BackgroundImage = Properties.Resources.Ci_background_v4;
            this.BackgroundImageLayout = ImageLayout.Stretch;

            gp_SetMainButtonVisible(this, false, false, false, false, false, false, false);
            //FavList();
            
            // 아코디언 컨트롤 별도스킨 지정
            accordionControl1.LookAndFeel.UseDefaultLookAndFeel = false;
            //accordionControl1.LookAndFeel.SkinName = "The Bezier";
            //accordionControl1.LookAndFeel.SkinName = "McSkin";
            //accordionControl1.LookAndFeel.SkinName = "Dark Side";
            accordionControl1.LookAndFeel.SkinName = "Seven";
            //accordionControl1.LookAndFeel.SkinName = "Blueprint";

            // 로그인 사용자가 관리자라면
            if (LoginUser.USRCD.Equals("00000"))
            {
                accordionControlElement14.Visible = true;   // 공통코드관리
                //accordionControlElement3.Visible = true;    // 권한관리
                //accordionControlElement33.Visible = true;   // 환경설정
                accordionControlElement38.Visible = true;   // 버전관리
            }
        }
        #region [시간 표시]
        public void GetCurTime()
        {
            if (_TimeThread == null)
            {
                _TimeThread = new Thread(new ThreadStart(GetTime));
            }

            if (_TimeThread.IsAlive)
            {
                _TimeThread.Abort();
            }

            _TimeThread.IsBackground = true;
            _TimeThread.Start();
        }
        private void GetTime()
        {
            while (true)
            {
                try
                {
                    barStaticTime.Caption = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    Thread.Sleep(500);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        #endregion

        #region [창 열기]
        // AccordionControl 메뉴 클릭 이벤트
        private void Element_Click(object sender, EventArgs e)
        {
            XtraForm frm;
            AccordionControlElement ace = (AccordionControlElement)sender;
            string tag = ace.Tag.ToString();
            frm = null;

            // 메인
            if (tag.ToString() == "MN002F00") { frm = new MN002F00(); }        //
            else if (tag.ToString() == "CM001F00") { frm = new CM001F00(); }   // 품목
            else if (tag.ToString() == "CM002F00") { frm = new CM002F00(); }   // 
            else if (tag.ToString() == "CM003F00") { frm = new CM003F00(); }   // 

            else if (tag.ToString() == "SY000F00") { frm = new SY000F00(); }   // 
            else if (tag.ToString() == "SY001F00") { frm = new SY001F00(); }   // 

            else if (tag.ToString() == "MT001F00") { frm = new MT001F00(); }   // 
            else if (tag.ToString() == "MT002F00") { frm = new MT002F00(); }   // 
            

            // 모든창 닫기
            else if (tag.ToString() == "FormClose") { AllFormClose(); }
            // 로그아웃
            else if (tag.ToString() == "LogOut") { frm = new MN001F00(); _ReLogin = true; }
            // 프로그램 종료
            else if (tag.ToString() == "MainClose") { Application.Exit(); }

            if (tag.ToString() != "MainClose" && tag.ToString() != "FormClose")
            {

                if (frm != null && (frm.Name.Equals("MN002F00")))  // 비밀번호 변경
                    gp_FormCheck_Popup(frm, this);
                else if (tag.ToString() == "LogOut")
                {
                    if (gp_PrintQuestion("로그아웃 하시겠습니까?", "로그아웃", MessageType.질문))
                        frm.Show();
                    else
                        _ReLogin = false;
                }
                // 2026.07.13
                else if (frm != null && (frm.Name.Equals("MT001F00") || frm.Name.Equals("MT002F00")))
                {
                    FormCheck2(frm);
                }
                //
                else if (frm != null && (frm.Name.Equals("CM001F00") || frm.Name.Equals("CM001F99")))
                {
                    foreach(XtraForm xf in this.MdiChildren)
                    {
                        if (xf.Name.Equals("CM001F00") || xf.Name.Equals("CM001F99"))
                        {
                            //gp_PrintMessage("회사등록 또는 거래처등록 메뉴를 종료한 후 재시도바랍니다.", Text, MessageType.알림);
                            xf.Dispose();
                            gp_FormCheck(frm, this);
                            return;
                        }
                    }
                    gp_FormCheck(frm, this);
                }
                else
                    gp_FormCheck(frm, this);
            }
            if (_ReLogin)
                Dispose();
        }

        private void FormCheck2(XtraForm frm)
        {
            if (FormIsExist(frm.GetType()))
            {
                frm.Dispose();
                return;
            }
            else
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {

                }
            }
        }

        private bool FormIsExist(Type tp)
        {
            foreach (XtraForm ff in this.MdiChildren)
            {
                if (ff.GetType() == tp)
                {
                    ff.Focus();
                    ff.BringToFront();
                    return true;
                }
            }
            return false;
        }

        // 열려있는 모든창 닫기
        private void AllFormClose()
        {
            foreach (XtraForm child in this.MdiChildren)
                child.Dispose();
            // 아코디언 컨트롤 폼은 제외
            foreach (Form child in OwnedForms)
            {
                if (child is AccordionControlForm)
                    continue;
                else
                    child.Dispose();
            }
                
            barStaticItem4.Caption = "메인메뉴  |  MN001F01";
            gp_SetMainButtonVisible(this, false, false, false, false, false, false, false);
        }

        private void HOMEPAGE_LOAD()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" SELECT RETNM FROM REFFPF WHERE RCDTP = '*SYS' AND REFNO = 'PINEIT'");
            DataTable dt = DBConn.GetDataTable(strSql.ToString());
            if (dt != null && dt.Rows.Count > 0)
                ComnString.PINEIT_HOME_PAGE_URL = dt.Rows[0]["RETNM"]?.ToString();

            try
            {
                System.Diagnostics.Process.Start(ComnString.PINEIT_HOME_PAGE_URL);
            }
            catch (Win32Exception noBrowser)
            {
                if (noBrowser.ErrorCode == -2147467259)
                    gp_PrintMessage(noBrowser.Message, Text, MessageType.오류);
            }
            catch (Exception ex)
            {
                gp_PrintMessage(ex.Message, Text, MessageType.오류);
            }
        }
        #endregion

        // 창 활성화
        private void MN002F00_MdiChildActivate(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild != null)
                barStaticItem4.Caption = this.ActiveMdiChild.Text + "  |  " + this.ActiveMdiChild.Name;
            else
                barStaticItem4.Caption = "메인메뉴  |  MN001F01";
        }

        // 메인 폼 종료
        private void MainForm_Closed(object sender, FormClosedEventArgs e)
        {
            //gp_SetLogInfo(Name, Text, CONNECT_TYPE.종료);
            if (!_ReLogin)
            {
                DBConn.DbDisConn(DBConn.dbCon);
                if (Application.MessageLoop)
                {
                    // WinForms app
                    Application.Exit();
                }
                else
                {
                    // Console app
                    Environment.Exit(1);
                }
            }
        }

        #region [레이아웃 저장]
        private void SaveLayout_ItemClick(object sender, ItemClickEventArgs e)
        {
            string FormName = barManager1.ActiveMdiChild?.Name;
            if (string.IsNullOrEmpty(FormName))
                return;

            if (XtraMessageBox.Show(barManager1.ActiveMdiChild.Text + "의 Layout정보를 저장하시겠습니까?", "Layout저장여부", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;

            System.Windows.Forms.Form frm = barManager1.ActiveMdiChild;
            gp_SetFormText(frm);
        }
        #endregion

        #region [레이아웃 초기화]
        private void InitLayout_ItemClick(object sender, ItemClickEventArgs e)
        {
            string sPgmId = barManager1.ActiveMdiChild?.Name;
            if (string.IsNullOrEmpty(sPgmId))
            {
                return;
            }
            if (XtraMessageBox.Show("선택한 프로그램의 레이아웃 설정값이 사라지게 됩니다. \r\n그래도 초기화 하시겠습니까?"
               , "레이아웃 초기화 여부", MessageBoxButtons.YesNo) != DialogResult.Yes)
            {
                return;
            }
            string sId = LoginUser.USRID;
            if (System.IO.Directory.Exists(Application.StartupPath + @"\xaml\" + sId))
            {
                string[] files = System.IO.Directory.GetFiles(Application.StartupPath + @"\xaml\" + sId);

                foreach (string s in files)
                {
                    Cursor = Cursors.WaitCursor;
                    string fileName = System.IO.Path.GetFileName(s);
                    if (fileName.Contains(sPgmId))
                    {
                        string deletefile = Application.StartupPath + @"\xaml\" + sId + @"\" + fileName;
                        System.IO.File.Delete(deletefile);

                        Cursor = Cursors.Default;
                    }
                    Cursor = Cursors.Default;
                }
            }
            XtraMessageBox.Show("초기화가 완료되었습니다.\r\n해당 폼이 다시 실행됩니다.");
            string sFormName = barManager1.ActiveMdiChild.Name;
            barManager1.ActiveMdiChild.Dispose();
            XtraForm frm = (XtraForm)gp_GetAssemblyForm(sFormName);
            gp_FormCheck(frm, this);
            Cursor = Cursors.Default;
        }
        #endregion

        private void accordionControl1_CustomDrawElement(object sender, CustomDrawElementEventArgs e)
        {
            /*
            List<AccordionControlElement> temp = accordionControl1.GetElements();
            foreach (AccordionControlElement ace in temp)
            {
                if (ace.Style == ElementStyle.Group)
                {
                    e.DrawHeaderBackground();
                    e.DrawContextButtons();
                    e.DrawExpandCollapseButton();
                    e.DrawText();
                    e.DrawImage();
                    e.Graphics.FillRectangle(Brushes.RoyalBlue, new Rectangle(e.ObjectInfo.HeaderBounds.Location, new Size(10, e.ObjectInfo.HeaderBounds.Height)));
                    e.Handled = true;
                }
                else
                {
                    e.Handled = false;
                }
            }
            */
        }

        // 아코디언 컨트롤 앞에 사각형 그리기 (그룹과 아이템을 나눌 방법이 모호하여 우선 미사용)
        private void accordionControl1_PaintEx(object sender, AccordionControlPaintEventArgs e)
        {
            e.Cache.DrawRectangle(Pens.Gray, e.ClipRectangle);
        }

        private void SimpleButton_Click(object sender, EventArgs e)
        {
            SimpleButton sb = (SimpleButton)sender;

            if (sb.Name.Contains("Retr")) { RETR(); }
            else if (sb.Name.Contains("Add")) { ADD(); }
            else if (sb.Name.Contains("Save")) { SAVE(); }
            else if (sb.Name.Contains("Delete")) { DELETE(); }
            else if (sb.Name.Contains("Xls")) { XLS(); }
            else if (sb.Name.Contains("Print")) { PRINT(); }
            else if (sb.Name.Contains("Close")) { CLOSE(); }
        }

        #region [ 즐겨찾기 ]
        // 즐겨찾기 추가
        private void AddFavoritesMenu()
        {
            Control ct = this.ActiveMdiChild;

            if (ct != null)
            {
                string sCt = ct.Name.ToString();
                //string st = ct.Text.ToString();

                Dictionary<string, string> dicParamsA = new Dictionary<string, string>();
                dicParamsA.Clear();
                dicParamsA.Add("CMD", "LIST");
                dicParamsA.Add("PGMID", sCt);
                dicParamsA.Add("USRCD", LoginUser.USRCD);

                DataTable dtA = DBConn.GetDataTable(DBConn.dbCon, "DP_MN003F00", dicParamsA);

                if (dtA.Rows.Count > 0)
                {
                    gp_PrintMessage("이미 즐겨찾기에 등록되어있는 메뉴입니다.", Text, MessageType.알림);
                    return;
                }
                else
                {
                    Dictionary<string, string> dicParams = new Dictionary<string, string>();
                    dicParams.Clear();
                    dicParams.Add("CMD", "FAV_ADD");
                    dicParams.Add("USRCD", LoginUser.USRCD);
                    dicParams.Add("FORM", sCt);

                    DataTable dt = DBConn.GetDataTable(DBConn.dbCon, "DP_MN003F00", dicParams);
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            if (! gp_PrintQuestion("현재 활성화된 메뉴를 즐겨찾기에 추가하시겠습니까?", Text, MessageType.질문))
                                return;
                            string pgmnm = dt.Rows[0]["PGMNM"].ToString();
                            string pgmid = dt.Rows[0]["PGMID"].ToString();
                            //var item = new BarButtonItem() { Caption = pgmnm, Tag = pgmid };
                            var item = new AccordionControlElement() { Text = pgmnm, Tag = pgmid, Style = ElementStyle.Item };
                            //item.Appearance.Hovered.BackColor = Color.LightBlue;
                            accordionControlElement23.Elements.Add(item);
                            FavList();
                            gp_PrintMessage("즐겨찾기에 추가되었습니다.", Text, MessageType.알림);
                        }
                        else
                        {
                            gp_PrintMessage("즐겨찾기를 추가할 창을 포커스 후 재시도 바랍니다.", Text, MessageType.알림);
                            return;
                        }
                    }
                }
            }
            else
            {
                gp_PrintMessage("현재 열려있는 창이 없습니다.", Text, MessageType.알림);
                return;
            }
        }

        // 즐겨찾기 리스트
        public void FavList()
        {
            accordionControlElement23.Elements.Clear();
            accordionControlElement23.Elements.Add(accordionControlElement31);
            accordionControlElement23.Elements.Add(accordionControlElement32);
            accordionControlElement23.Elements.Add(accordionControlSeparator2);

            Dictionary<string, string> dicParams = new Dictionary<string, string>();
            dicParams.Clear();
            dicParams.Add("CMD", "FAV_List");
            dicParams.Add("USRCD", LoginUser.USRCD);

            DataTable dt = DBConn.GetDataTable(DBConn.dbCon, "DP_MN003F00", dicParams);
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string pgmnm = dt.Rows[i]["PGMNM"].ToString();
                    string pgmid = dt.Rows[i]["PGMID"].ToString();
                    //var item = new BarButtonItem() { Caption = pgmnm, Tag = pgmid };
                    var item = new AccordionControlElement() { Text = pgmnm, Tag = pgmid, Style = ElementStyle.Item };
                    //item.Appearance.Hovered.BackColor = Color.LightBlue;
                    //barSubItem15.ItemLinks.Insert(barSubItem1.ItemLinks[0], item);
                    accordionControlElement23.Elements.Add(item);
                    //item.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.Item_Click);
                    item.Click += new EventHandler(this.Element_Click);
                }
            }

        }
        #endregion

        private void RETR()
        {
            XtraForm frm = (XtraForm)this.ActiveMdiChild;
            // frm이 MainButtonInterface 인터페이스를 구현하고 있으면 RETR 호출
            if (frm != null && frm is MainButtonInterface buttonInterface)
                buttonInterface.RETR();  // 인터페이스에 정의된 RETR 호출
        }

        private void ADD()
        {
            XtraForm frm = (XtraForm)this.ActiveMdiChild;
            if (frm != null && frm is MainButtonInterface buttonInterface)
                buttonInterface.ADD();
        }

        private void SAVE()
        {
            XtraForm frm = (XtraForm)this.ActiveMdiChild;
            if (frm != null && frm is MainButtonInterface buttonInterface)
                buttonInterface.SAVE();
        }

        private void DELETE()
        {
            XtraForm frm = (XtraForm)this.ActiveMdiChild;
            if (frm != null && frm is MainButtonInterface buttonInterface)
                buttonInterface.DELETE();
        }

        private void XLS()
        {
            XtraForm frm = (XtraForm)this.ActiveMdiChild;
            if (frm != null && frm is MainButtonInterface buttonInterface)
                buttonInterface.XLS();
        }

        private void PRINT()
        {
            XtraForm frm = (XtraForm)this.ActiveMdiChild;
            if (frm != null && frm is MainButtonInterface buttonInterface)
                buttonInterface.PRINT();
        }

        private void CLOSE()
        {
            XtraForm frm = (XtraForm)this.ActiveMdiChild;
            if (frm != null && frm is MainButtonInterface buttonInterface)
                buttonInterface.CLOSE();
        }

        private void tabbedView1_Floating(object sender, DevExpress.XtraBars.Docking2010.Views.DocumentEventArgs e)
        {
            
        }

        private void tabbedView1_BeginFloating(object sender, DevExpress.XtraBars.Docking2010.Views.DocumentCancelEventArgs e)
        {

        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            HOMEPAGE_LOAD();
        }
    }
}