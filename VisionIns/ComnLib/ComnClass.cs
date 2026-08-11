using DevExpress.DataAccess.Excel;
using DevExpress.Spreadsheet;
using DevExpress.Utils;
using DevExpress.Utils.Menu;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;
using DevExpress.XtraSplashScreen;
using VisionIns;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using PopupMenuShowingEventArgs = DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs;
using System.Security.Cryptography;
using DevExpress.XtraPrinting;

public static class ComnString
{
    // 개발서버 (테스트DB)
    public static string CONNECTION_STRING = "server = 121.66.17.30,16433; uid = pineit; pwd = pineit0401; database = VISION_INS";
    public static string CONNECTION_STRING_LOCAL = "server = localhost; Database =  Integrated Security = True;";
    public static string CONNECTION_PINEIT = "server = 121.66.17.30,16433; uid = pineit; pwd = pineit0401; database = PINEIT";
    public static string TXT_VERSION = "Copyright PineIT @ 2026 Ver.";
    public static string TXT_CONNECT_USER = "현재 접속자 : ";
    public static string TXT_LOGIN_OK = "로그인에 성공였습니다.";
    public static string TXT_LOGIN_FAIL = "아이디 또는 비밀번호를 확인해 주세요.";
    public static string TXT_SAVE_OK = "저장 되었습니다.";
    public static string TXT_SAVE_FAIL = "저장에 실패하였습니다. \r\n입력정보를 확인해주세요.";
    public static string INI_USER_SECTION_LOGIN = "LOGIN";
    public static string INI_USER_SECTION_LOGIN_KEY_ID = "ID";
    public static string INI_USER_SECTION_LOGIN_KEY_PW = "PW";
    public static string INI_USER_SECTION_LOGIN_KEY_CHECK = "CHECK";
    public static string INI_VERSION_SECTION_VERSION = "VERSION";
    public static string INI_VERSION_SECTION_VERSION_KEY_VER = "version";
    public static string INI_VERSION = "1.00.00";
    public static string SAVE_LAYOUT_LOADING_NAME = "_Layout 저장중...";

    // 메세지 박스 문구
    // 보안
    public static string WS1001 = "사용자 정보 오류입니다. 확인하세요."; // 접속시 ID/PW 오타로 인한 오류 체크
    public static string WS0001 = "권한이 없습니다.";
    public static string WU0001 = "을(를) 입력해 주세요.";
    public static string WU0002 = "중복된 코드입니다. \r\n확인하세요.";
    public static string IF0001 = "처리되었습니다.";
    public static string AES_Key = "skdjvopiehwgekgjpdovjdikwfjpoefj";

    // FTP 정보
    public static string FTP_IP = "ftp://121.66.17.30:16021";
    public static string FTP_ID = "server";
    public static string FTP_PW = "pineit0401!!";
    public static string FTP_PATH = "ftp://121.66.17.30:16021/Columbus/";
    public static string FTP_ROOT_DIR = "ftp://121.66.17.30:16021/Pineit_101";

    // Indicator IOT 정보
    public static string IOT_IP = "172.30.1.240";
    public static string IOT_PORT = "23";

    public static string PINEIT_HOME_PAGE_URL = "www.pineit.co.kr";

    public static string _FOCUSED_MENU = string.Empty;
}

public static class LoginUser
{
    public static string USRID;
    public static string USRNM;
    public static string USRCD;
    public static string USLVL;
}

public class Parameter
{
    public string Key { get; set; }
    public object Value { get; set; }
    public SqlDbType DBtype { get; set; }

    public Parameter()
    {

    }
    public Parameter(string _Key, object _Value)
    {
        Key = _Key;
        Value = _Value;
        DBtype = SqlDbType.NVarChar;
    }
    public Parameter(string _Key, object _Value, SqlDbType _Type)
    {
        Key = _Key;
        Value = _Value;
        DBtype = _Type;
    }
}

class ComnFunc
{
    public static Dictionary<string, string> parameterDic = new Dictionary<string, string>();
    public static List<Parameter> parameterList = new List<Parameter>();
    public static string formName;
    public static SimpleButton sb;
    public static MN002F00 _main;

    #region [ 프로시저 ENUM (미사용) ]
    //public enum PROCEDURE_NAME { 로그인, 로그저장, 버전관리조회, 버전업로드, 채번, 항목검색
    // /* 00SY 시스템    */      , 공통코드관리, 사용자관리, 비밀번호변경, 즐겨찾기관리, 버전관리, LOG이력현황, 웹팩스상태관리
    // /* 01CM 기준정보  */      , 거래처관리, 회사정보관리, 품목관리, 거래처별단가관리
    // /* 02PS 매입/매출 */      , 계근관리, 입출고관리, 직송관리, 마감관리, 단가정산관리, 매입매출현황, 업체별거래현황
    // /* 06IV 재고      */      , 재고현황, 재고조정
    // /* 70AC 회계      */      , 계정관리, 전표승인, 전표관리, 지불관리, 지출결의서, 세금계산서관리, 어음조회, 일계표, 계정원장, 거래처원장, 합계잔액시산표, 자금일보
    // /* 05EQ 설비      */      , 설비이력관리
    //}
    //public static Dictionary<PROCEDURE_NAME, string> procedureDic = new Dictionary<PROCEDURE_NAME, string>{
    //    /*                */
    //    {PROCEDURE_NAME.로그인,           "usp_Comn_Login"},
    //    {PROCEDURE_NAME.로그저장,         "usp_Comn_SetLog"},
    //    {PROCEDURE_NAME.버전관리조회,     "usp_SYS_VersionCheck"},
    //    {PROCEDURE_NAME.버전업로드,       "usp_SYS_VersionUpload"},
    //    {PROCEDURE_NAME.채번,             "usp_GetSLINO"},
    //    {PROCEDURE_NAME.항목검색,         "usp_SearchInfo"},
    //    /* 00SY 시스템    */
    //    {PROCEDURE_NAME.공통코드관리,     "usp_SY010F00"},
    //    {PROCEDURE_NAME.사용자관리,       "usp_SY020F00"},
    //    {PROCEDURE_NAME.비밀번호변경,     "usp_SY001F00"},
    //    {PROCEDURE_NAME.즐겨찾기관리,     "usp_SY002F00"},
    //    {PROCEDURE_NAME.버전관리,         "usp_SY003F00"},
    //    {PROCEDURE_NAME.LOG이력현황,      "usp_SY101F00"},
    //    {PROCEDURE_NAME.웹팩스상태관리,   "usp_SY090F00"},
    //    /* 01CM 기준정보  */
    //    {PROCEDURE_NAME.거래처관리,       "usp_CM001F00"},
    //    {PROCEDURE_NAME.회사정보관리,     "usp_CM001F01"},
    //    {PROCEDURE_NAME.품목관리,         "usp_CM010F00"},
    //    {PROCEDURE_NAME.거래처별단가관리, "usp_CM020F00"},
    //    /* 02PS 매입/매출 */
    //    {PROCEDURE_NAME.계근관리,         "usp_PS010F00"},
    //    {PROCEDURE_NAME.입출고관리,       "usp_PS020F00"},
    //    {PROCEDURE_NAME.직송관리,         "usp_PS035F00"},   // 수정필요, 현재 PS035F00으로, 단가정산관리와 중복
    //    {PROCEDURE_NAME.마감관리,         "usp_PS030F00"},
    //    {PROCEDURE_NAME.단가정산관리,     "usp_PS035F00"},
    //    {PROCEDURE_NAME.매입매출현황,     "usp_PS101F00"},
    //    {PROCEDURE_NAME.업체별거래현황,   "usp_PS102F00"},
    //    /* 06IV 재고      */
    //    {PROCEDURE_NAME.재고현황,         "usp_IV101F00"},
    //    {PROCEDURE_NAME.재고조정,         "usp_IV010F00"},
    //    /* 70AC 회계      */
    //    {PROCEDURE_NAME.계정관리,         "usp_AC001F00"},
    //    {PROCEDURE_NAME.전표승인,         "usp_AC020F00"},
    //    {PROCEDURE_NAME.전표관리,         "usp_AC010F00"},
    //    {PROCEDURE_NAME.지불관리,         "usp_AC050F00"},  // PU050F00
    //    {PROCEDURE_NAME.지출결의서,       "usp_AC105F00"},  // 수정필요, 현재 PU105F00으로, 문자 통일여부 확인필요
    //    {PROCEDURE_NAME.세금계산서관리,   "usp_AC040F00"},  // SA040F00
    //    {PROCEDURE_NAME.어음조회,         "usp_AC201F00"},
    //    {PROCEDURE_NAME.일계표,           "usp_AC102F00"},
    //    {PROCEDURE_NAME.계정원장,         "usp_AC103F00"},
    //    {PROCEDURE_NAME.거래처원장,       "usp_AC104F00"},
    //    {PROCEDURE_NAME.합계잔액시산표,   "usp_AC105F00"},
    //    {PROCEDURE_NAME.자금일보,         "usp_AC106F00"},
    //    /* 05EQ 설비      */
    //    {PROCEDURE_NAME.설비이력관리,     "usp_EQ010F00"},
    //};        
    #endregion

    #region [로그인]
    public static bool gp_Login(string ID, string PW)
    {
        bool isOK = false;
        parameterDic.Clear();
        parameterDic.Add("USRID", ID);
        parameterDic.Add("USRPW", PW);
        DataTable dt = DBConn.GetDataTable("usp_Comn_Login", parameterDic);
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {
                isOK = true;
                LoginUser.USRID = dt.Rows[0]["USRID"]?.ToString();
                LoginUser.USRNM = dt.Rows[0]["USRNM"]?.ToString();
                LoginUser.USRCD = dt.Rows[0]["USRCD"]?.ToString();
                LoginUser.USLVL = dt.Rows[0]["USLVL"]?.ToString();

                if (dt.Rows[0]["NEWYN"].ToString().Equals("Y"))
                {
                    MN002F00 frm = new MN002F00();
                    gp_PrintMessage("비밀번호를 변경해주세요.", frm.Text, MessageType.알림);
                    frm.ShowDialog();
                }
            }
        }
        return isOK;
    }
    #endregion

    #region[권한 관련]
    // 권한에 따른 메뉴 (대분류) 보이기 설정
    //public static void SetMenuListByAuth(string USERID, AccordionControlElement Cate_SYSTEM, AccordionControlElement Cate_STD, AccordionControlElement Cate_PROD
    //    , AccordionControlElement Cate_SUBALJU, AccordionControlElements Cate_CHULHA, AccordionControlElement Cate_EQUIP
    //    , AccordionControlElement Cate_QC, AccordionControlElement Cate_RPT, AccordionControlElement Cate_STS
    //    , AccordionControlElement[] Cate_Child)
    //{
    //    Dictionary<string, string> dicParams = new Dictionary<string, string>();
    //    dicParams.Add("CMD", "AUTH");
    //    dicParams.Add("PGMID", "");
    //    dicParams.Add("USRID", USERID);

    //    DataTable dt = GetInfo(dicParams, "GET_AUTH_RST");

    //    //대분류에 속한 컨트롤의 권한 체크를 위한 카운트 값, Loop 후 0일 시 해당 대분류는 Visible False;
    //    int SYSTEM = 0;
    //    int STD = 0;
    //    int PROD = 0;
    //    int SUBALJU = 0;
    //    int CHULHA = 0;
    //    int EQUIP = 0;
    //    int QC = 0;
    //    int RPT = 0;
    //    int STS = 0;

    //    for (int i = 0; i < dt.Rows.Count; i++)
    //    {
    //        string sPgmId = dt.Rows[i]["PGMID"]?.ToString();
    //        string sPgGrp = dt.Rows[i]["PGGRP"]?.ToString();
    //        string sUseYn = dt.Rows[i]["USE_Y"]?.ToString();

    //        for (int j = 0; j < Cate_Child.Length; j++)
    //        {
    //            if (sPgmId.Equals(Cate_Child[j].Tag))
    //            {
    //                Cate_Child[j].Visible = true;
    //                if (sPgGrp.Equals("11"))
    //                    SYSTEM++;
    //                else if (sPgGrp.Equals("22"))
    //                    STD++;
    //                else if (sPgGrp.Equals("33"))
    //                    PROD++;
    //                else if (sPgGrp.Equals("44"))
    //                    SUBALJU++;
    //                else if (sPgGrp.Equals("55"))
    //                    CHULHA++;
    //                else if (sPgGrp.Equals("66"))
    //                    EQUIP++;
    //                else if (sPgGrp.Equals("77"))
    //                    QC++;
    //                else if (sPgGrp.Equals("88"))
    //                    RPT++;
    //                else if (sPgGrp.Equals("99"))
    //                    STS++;

    //            }
    //        }

    //    }

    //    //카운트 값 0일시 해당 서브Control 미존재로 간주, 해당 대분류 Control Visible False 설정
    //    if (SYSTEM == 0)
    //    {
    //        Cate_SYSTEM.Visible = false;
    //    }
    //    else
    //    {
    //        Cate_SYSTEM.Visible = true;
    //    }

    //    if (STD == 0)
    //    {
    //        Cate_STD.Visible = false;
    //    }
    //    else
    //    {
    //        Cate_STD.Visible = true;
    //    }

    //    if (PROD == 0)
    //    {
    //        Cate_PROD.Visible = false;
    //    }
    //    else
    //    {
    //        Cate_PROD.Visible = true;
    //    }

    //    if (SUBALJU == 0)
    //    {
    //        Cate_SUBALJU.Visible = false;
    //    }
    //    else
    //    {
    //        Cate_SUBALJU.Visible = true;
    //    }

    //    if (CHULHA == 0)
    //    {
    //        Cate_CHULHA.Visible = false;
    //    }
    //    else
    //    {
    //        Cate_CHULHA.Visible = true;
    //    }

    //    if (EQUIP == 0)
    //    {
    //        Cate_EQUIP.Visible = false;
    //    }
    //    else
    //    {
    //        Cate_EQUIP.Visible = true;
    //    }

    //    if (QC == 0)
    //    {
    //        Cate_QC.Visible = false;
    //    }
    //    else
    //    {
    //        Cate_QC.Visible = true;
    //    }

    //    if (RPT == 0)
    //    {
    //        Cate_RPT.Visible = false;
    //    }
    //    else
    //    {
    //        Cate_RPT.Visible = true;
    //    }

    //    if (STS == 0)
    //    {
    //        Cate_STS.Visible = false;
    //    }
    //    else
    //    {
    //        Cate_STS.Visible = true;
    //    }
    //}

    //// 권한에 따른 메뉴 (소분류) 보이기 설정
    //public static void SetMenuListByAuth(DataTable dt, AccordionControlElement Big_Basic, AccordionControlElement Big_Prod
    //    , AccordionControlElement Big_Mesure, AccordionControlElement Big_Packing, AccordionControlElement Big_System, AccordionControlElement[] SubCategory)
    //{
    //    //대분류에 속한 컨트롤의 권한 체크를 위한 카운트 값, Loop 후 0일 시 해당 대분류는 Visible False;
    //    int Chk_Basic = 0;
    //    int Chk_Prod = 0;
    //    int Chk_Mesure = 0;
    //    int Chk_Packing = 0;
    //    int Chk_System = 0;

    //    for (int i = 0; i < dt.Rows.Count; i++)
    //    {
    //        string sPgTag = dt.Rows[i]["PGTAG"]?.ToString();
    //        string sPgGrp = dt.Rows[i]["PGGRP"]?.ToString();
    //        string sUseYn = dt.Rows[i]["USE_Y"]?.ToString();

    //        if (sUseYn.Equals("Y"))
    //        {
    //            //SubCategory 중 PGGRP 컬럼 값 매칭 후 Visible 변환
    //            for (int j = 0; j < SubCategory.Length; j++)
    //            {
    //                if (SubCategory[j].Name.Equals(sPgTag))
    //                {
    //                    SubCategory[j].Visible = true;

    //                    //공통코드로 부터 코드값 HARD_CODING
    //                    if (sPgGrp.Equals("11"))
    //                    {
    //                        Chk_System++;
    //                    }
    //                    else if (sPgGrp.Equals("22"))
    //                    {
    //                        Chk_Basic++;
    //                    }
    //                    else if (sPgGrp.Equals("33"))
    //                    {
    //                        Chk_Prod++;
    //                    }
    //                    else if (sPgGrp.Equals("44"))
    //                    {
    //                        Chk_Mesure++;
    //                    }
    //                    else if (sPgGrp.Equals("55"))
    //                    {
    //                        Chk_Packing++;
    //                    }
    //                }
    //            }
    //        }
    //    }

    //    //카운트 값 0일시 해당 서브Control 미존재로 간주, 해당 대분류 Control Visible False 설정
    //    if (Chk_System == 0)
    //    {
    //        Big_System.Visible = false;
    //    }
    //    else
    //    {
    //        Big_System.Visible = true;
    //    }

    //    if (Chk_Basic == 0)
    //    {
    //        Big_Basic.Visible = false;
    //    }
    //    else
    //    {
    //        Big_Basic.Visible = true;
    //    }

    //    if (Chk_Prod == 0)
    //    {
    //        Big_Prod.Visible = false;
    //    }
    //    else
    //    {
    //        Big_Prod.Visible = true;
    //    }

    //    if (Chk_Mesure == 0)
    //    {
    //        Big_Mesure.Visible = false;
    //    }
    //    else
    //    {
    //        Big_Mesure.Visible = true;
    //    }

    //    if (Chk_Packing == 0)
    //    {
    //        Big_Packing.Visible = false;
    //    }
    //    else
    //    {
    //        Big_Packing.Visible = true;
    //    }

    //}

    //// 권한 가져오기 (프로시저 사용 권한)
    //public static DataTable gp_GetAuthInfo(string sUsrCd, string sProcedureName)
    //{
    //    string[] sField = new string[2];
    //    string[] sValue = new string[sField.Length];

    //    sField[0] = "CMD";
    //    sValue[0] = "AUTH";

    //    sField[1] = "USRCD";
    //    sValue[1] = sUsrCd;

    //    return DBConn.GetDataTableByProcedure(DBConn.dbCon, sProcedureName, sField, sValue);
    //}

    // 권한 가져오기 (페이지 사용 권한)
    /// <summary>
    /// 현재 사용자의 메뉴 권한 가져오기 (True : 권한있음, False : 권한없음)
    /// </summary>
    /// <param name="sCmd">확인할 권한 (USE, ADD, UPD, PDL, PRT, XLS)</param>
    /// <param name="sPgmId">확인할 메뉴 ID (대부분의 경우 Name)</param>
    public static bool gp_GetAuthInfo(string cmd, string pgmid)
    {
        DBConn._DicParams.Add("CMD", cmd);
        DBConn._DicParams.Add("USRCD", LoginUser.USRCD);
        DBConn._DicParams.Add("PGMID", pgmid);
        DataTable dt = DBConn.GetDataTable("GET_AUTH_RST", DBConn._DicParams);

        try
        {
            if (dt.Rows.Count > 0)
            {
                string sRst = dt.Rows[0]["RST"]?.ToString();
                string sMsg = dt.Rows[0]["MSG"]?.ToString();
                if (sRst.Equals("Y"))
                    return true;
                else
                {
                    gp_PrintMessage(sMsg, "권한", MessageType.오류);
                    return false;
                }
            }
            else
            {
                string sWord = string.Empty;
                if (cmd.Equals("USE")) { sWord = "사용"; }
                else if (cmd.Equals("ADD")) { sWord = "추가"; }
                else if (cmd.Equals("UPD")) { sWord = "수정"; }
                else if (cmd.Equals("DEL")) { sWord = "삭제"; }
                else if (cmd.Equals("PRT")) { sWord = "출력"; }
                else if (cmd.Equals("XLS")) { sWord = "엑셀"; }
                gp_PrintMessage("해당 사용자는 " + sWord + " 권한이 없습니다.", "권한", MessageType.오류);
                return false;
            }
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    // 개별 권한 수정 가능 체크      
    public static bool gp_CheckEditableAuth(string sUsrCd, string sPgmId, string sProcedureName, string sEdit_Kind)
    {
        try
        {
            string sColumnsName = string.Empty;
            if (sEdit_Kind.Equals("A")) //추가
            {
                sColumnsName = "ADD_Y";
            }
            else if (sEdit_Kind.Equals("U"))    //수정
            {
                sColumnsName = "UPD_Y";
            }
            else if (sEdit_Kind.Equals("D"))    //삭제
            {
                sColumnsName = "DEL_Y";
            }

            if (string.IsNullOrEmpty(sColumnsName))
            {
                XtraMessageBox.Show("시스템 ERROR : ComnFunc.CheckEditableAuth 참조, sColumnsName 변수 관련 에러");
                return true;
            }

            string[] sField = new string[3];
            string[] sValue = new string[sField.Length];

            sField[0] = "CMD";
            sValue[0] = "AUTH_CHK";

            sField[1] = "USRCD";
            sValue[1] = sUsrCd;

            sField[2] = "PGMID";
            sValue[2] = sPgmId;

            string sResult = string.Empty;
            DataTable dt = DBConn.GetDataTableByProcedure(DBConn.dbCon, sProcedureName, sField, sValue);
            if (dt.Rows.Count > 0)
            {
                sResult = dt.Rows[0][sColumnsName]?.ToString();
            }
            else
            {
                XtraMessageBox.Show("해당 권한정보가 존재하지 않습니다. \r\n 시스템 ERROR : ComnEtc.CheckEditableAuth 참조, DataTable.Rows.Count == 0");
                return true;
            }

            if (sResult.Equals("Y"))
                return false;
            else
                return true;
        }
        catch (Exception ex)
        {
            XtraMessageBox.Show(ex.Message);
            return true;
        }
    }
    #endregion

    #region [LOG 기록]
    public enum CONNECT_TYPE { 접속, 종료, 조회, 등록, 수정, 저장, 삭제, 엑셀, 출력 }
    // 파일명 + 확장자만 : System.IO.Path.GetFileName(System.Reflection.Assembly.GetEntryAssembly().Location)
    // 프로그램 전체경로 : System.Reflection.Assembly.GetEntryAssembly().Location
    // LOG 정보 저장 (프로그램명 + 버전정보 추가)
    public static void gp_SetLogInfo(string ProgramID, string ProgramName, CONNECT_TYPE CON)
    {
        parameterDic.Clear();
        parameterDic.Add("LOGGU", Enum.GetName(typeof(CONNECT_TYPE), CON)?.ToString());
        parameterDic.Add("USRCD", LoginUser.USRCD);
        parameterDic.Add("PGMID", ProgramID);
        parameterDic.Add("PGMNM", ProgramName);
        parameterDic.Add("COMIP", DBConn.Client_IP);
        parameterDic.Add("EXENM", System.Reflection.Assembly.GetEntryAssembly().Location);
//        parameterDic.Add("VERNO", ComnString.INI_VERSION);
        DBConn.ExecuteNonQuery("usp_Comn_SetLog", parameterDic);
    }
    #endregion

    #region [INI 저장 관련]
    public enum INI_NAME { USER, VERSION, SETTING }
    public static Dictionary<INI_NAME, string> IniNameDic = new Dictionary<INI_NAME, string>
    {
        { INI_NAME.USER, "User.ini" },
        { INI_NAME.VERSION, "Version.ini" },
        { INI_NAME.SETTING, "setting.ini" }
    };

    [DllImport("kernel32")]
    private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
    [DllImport("kernel32")]
    private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

    /// <summary>
    /// ini 파일 내부의 값 문자열 반환 메서드
    /// </summary>
    /// <param name="ini">접근할 ini 파일 할당</param>
    /// <param name="Section">ini 파일 내부의 섹션 ( [ ] 로 묶여있는 대분류 )</param>
    /// <param name="Key">ini 파일 섹션 아래의 키 값</param>
    /// <returns></returns>
    public static string gp_GetIniValue(INI_NAME ini, string Section, string Key)
    {
        string path = string.Format(@"{0}\{1}", Application.StartupPath, IniNameDic[ini]);
        StringBuilder temp = new StringBuilder(255);
        int i = GetPrivateProfileString(Section, Key, "", temp, 255, path);
        return temp.ToString();
    }
    /// <summary>
    /// ini 파일 내부의 값 변경 및 저장 메서드
    /// </summary>
    /// <param name="ini">접근할 ini 파일 할당</param>
    /// <param name="Section">ini 파일 내부의 섹션 ( [ ] 로 묶여있는 대분류 )</param>
    /// <param name="Key">ini 파일 섹션 아래의 키 값</param>
    /// <param name="Value">변경 및 저장할 데이터</param>
    /// <returns></returns>
    public static void gp_SetIniValue(INI_NAME ini, string Section, string Key, string Value)
    {
        string path = string.Format(@"{0}\{1}", Application.StartupPath, IniNameDic[ini]);
        WritePrivateProfileString(Section, Key, Value, path);
    }
    #endregion         

    #region [레이아웃 저장 & 초기화]
    public static void gp_SetFormText(Form form) 
    {
        string sFormText = ComnString.SAVE_LAYOUT_LOADING_NAME;
        form.Text += sFormText;
    }
    public static void gp_SaveLayout(string sFormName, GridView[] arrGrdView, LayoutControl layout)
    {
        //GridView 저장
        for (int i = 0; i < arrGrdView.Length; i++)
        {
            string sFileName = string.Format("{0}{1}", sFormName, (i + 1).ToString());
            gp_SaveGridViewLayout(LoginUser.USRID, sFileName, arrGrdView[i]);
        }
        //Layout 저장
        gp_SaveGridViewLayout(LoginUser.USRID, sFormName, layout);
    }
    public static void gp_SetLayout(string sFormName, GridView[] arrGrdView, LayoutControl layout)
    {
        for (int i = 0; i < arrGrdView.Length; i++)
        {
            string sFileName = string.Format("{0}{1}", sFormName, (i + 1).ToString());
            gp_SetGridViewLayout(LoginUser.USRID, "AccAdm", sFileName, arrGrdView[i]);
        }
    }
    public static void gp_SaveGridViewLayout(string sId, string sClass, DevExpress.XtraGrid.Views.Grid.GridView view)
    {
        string path = Application.StartupPath + @"\xaml\" + sId;
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        string sFile = Application.StartupPath + @"\xaml\" + sId + @"\" + sClass + ".xaml";
        view.SaveLayoutToXml(sFile);
    }
    public static void gp_SaveGridViewLayout(string sId, string sClass, LayoutControl layout)
    {
        string path = Application.StartupPath + @"\xaml\" + sId;
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        string sFile = Application.StartupPath + @"\xaml\" + sId + @"\" + sClass + "_Layout.xaml";
        layout.SaveLayoutToXml(sFile);
    }
    public static void gp_SetGridViewLayout(string sId, string sProject, string sClass, DevExpress.XtraGrid.Views.Grid.GridView view)
    {
        string sFile = Application.StartupPath + @"\xaml\" + sId + @"\" + sClass + ".xaml";
        if (!File.Exists(sFile))
        {
            sFile = Application.StartupPath + @"\xaml\" + sId + @"\" + sClass + ".xaml";
            if (!File.Exists(sFile)) return;
        }
        view.RestoreLayoutFromXml(sFile);
    }
    public static void gp_SetGridViewLayout(string sId, string sProject, string sClass, LayoutControl layout)
    {
        string sFile = Application.StartupPath + @"\xaml\" + sId + @"\" + sClass + "_Layout.xaml";
        if (!File.Exists(sFile))
        {
            sFile = Application.StartupPath + @"\xaml\" + sId + @"\" + sClass + "_Layout.xaml";
            if (!File.Exists(sFile)) return;
        }
        layout.RestoreLayoutFromXml(sFile);
    }
    public static Form gp_GetAssemblyForm(string strFormName)
    {
        Form f = null;
        foreach (Type t in System.Reflection.Assembly.GetExecutingAssembly().GetTypes())
        {
            if (t.Name == strFormName)                     //프로젝트 내 폼 중에서 찾을 이름과 같으면...
            {
                object o = Activator.CreateInstance(t);    //인스턴스 개체 생성 
                f = o as Form;                                  //인스턴스 개체 폼 형식으로 캐스팅
            }
        }
        return f;
    }
    #endregion

    #region [DateEdit 설정 관련 - 날짜 세팅, ShowClear = false + 날짜 체크(빈값)]
    /// <summary>
    /// 조회기간 초기 설정 : 금일 ~ 금일
    /// </summary>
    /// <param name="ymdFrom"></param>
    /// <param name="ymdTo"></param>
    public static void gp_SetDateFromFromValue(DateEdit ymdFrom, DateEdit ymdTo)
    {
        DateTime today = DateTime.Now.Date;
        ymdFrom.EditValue = today;
        ymdTo.EditValue = today;
        ymdFrom.Properties.ShowClear = false;
        ymdTo.Properties.ShowClear = false;
    }
    /// <summary>
    /// 조회기간 초기 설정 : 금월 1일 ~ 금일
    /// </summary>
    /// <param name="ymdFrom"></param>
    /// <param name="ymdTo"></param>
    public static void gp_SetDateFromToValue(DateEdit ymdFrom, DateEdit ymdTo)
    {
        DateTime today = DateTime.Now.Date;
        ymdFrom.EditValue = today.AddDays(1 - today.Day);
        ymdTo.EditValue = today;
        ymdFrom.Properties.ShowClear = false;
        ymdTo.Properties.ShowClear = false;
    }
    /// <summary>
    /// 조회기간 초기 설정 : 금년 1월 1일 ~ 금일
    /// </summary>
    /// <param name="ymdFrom"></param>
    /// <param name="ymdTo"></param>
    public static void gp_SetDateFromToValueThisYear(DateEdit ymdFrom, DateEdit ymdTo)
    {
        int year = DateTime.Now.Year;
        DateTime today = DateTime.Now.Date;
        DateTime firstDay = new DateTime(year, 1, 1);
        ymdFrom.EditValue = firstDay;
        ymdTo.EditValue = today;
        ymdFrom.Properties.ShowClear = false;
        ymdTo.Properties.ShowClear = false;
    }
    /// <summary>
    /// 조회일자 초기 설정 : 금월 1일
    /// </summary>
    /// <param name="ymdFrom"></param>
    public static void gp_SetDateFromValue(DateEdit ymdFrom)
    {
        DateTime today = DateTime.Now.Date;
        ymdFrom.EditValue = today.AddDays(1 - today.Day);
        ymdFrom.Properties.ShowClear = false;
    }
    /// <summary>
    /// 조회일자 초기 설정 : 금일
    /// </summary>
    /// <param name="ymdTo"></param>
    public static void gp_SetDateToValue(DateEdit ymdTo)
    {
        DateTime today = DateTime.Now.Date;
        ymdTo.EditValue = today;
        ymdTo.Properties.ShowClear = false;
    }
    #endregion

    #region [현재 미사용]

    #region [버튼 관련]
    public enum BUTTON_NAME { 조회, 추가, 수정, 저장, 삭제, 출력, 엑셀, 닫기 }
    public static Dictionary<BUTTON_NAME, string> buttonNameDic = new Dictionary<BUTTON_NAME, string>
    {
        { BUTTON_NAME.조회, "조회 (F5)" }
    };
    public static Dictionary<BUTTON_NAME, string> buttonCodeDic = new Dictionary<BUTTON_NAME, string>
    {
        { BUTTON_NAME.조회, "bt_Retr" }
    };

    /// <summary>
    /// 버튼 종류에 따른 기본 세팅? - 활용방안에 의문
    /// </summary>
    /// <param name="simpleButton"></param>
    /// <param name="BTN"></param>
    public static void gp_SetComponent(SimpleButton simpleButton, BUTTON_NAME BTN)
    {
        simpleButton.Name = buttonCodeDic[BTN];
        simpleButton.Size = new System.Drawing.Size(100, 30);
        simpleButton.Text = buttonNameDic[BTN];
    }
    #endregion
    
    #endregion
    
    #region [Lookup 관련]
    /// <summary>
    /// LookupEdit 초기 설정 메서드,
    /// 사용 예 : SetBoundLookUp(LkupHOUSE, "HOUSF", "HOUSE", "HOUNM"); 
    ///           SetBoundLookUp(LkupWLINE, "REFFPF", "WLINE", "");
    /// </summary>
    /// <param name="lkup">대상 LookupEdit 컨트롤</param>
    /// <param name="tableName">참조할 테이블명</param>
    /// <param name="columnName1">참조할 테이블의 코드명</param>
    /// <param name="columnName2">참조할 테이블의 대상 컬럼명 (예, HOUSE 테이블의 창고코드와 창고명을 조회할 때 : columnName1 = HOSCD; columnName2 = HOSNM /// REFFPF의 경우, "" 추천)</param>
    public static void gp_SetBoundLookUp(LookUpEdit lkup, string tableName, string columnName1, string columnName2)
    {
        gp_ExecuteLookupSQLquery(DBConn.dbCon, lkup, tableName, columnName1, columnName2);
    }

    /// <summary>
    /// LookupEdit 초기 설정 메서드 (외부 DB 참조용),
    /// 사용 예 : SetBoundLookUp(LkupHOUSE, "HOUSF", "HOUSE", "HOUNM"); 
    ///           SetBoundLookUp(LkupWLINE, "REFFPF", "WLINE", "");
    /// </summary>
    /// <param name="lkup">대상 LookupEdit 컨트롤</param>
    /// <param name="tableName">참조할 테이블명</param>
    /// <param name="columnName1">참조할 테이블의 코드명</param>
    /// <param name="columnName2">참조할 테이블의 대상 컬럼명 (예, HOUSE 테이블의 창고코드와 창고명을 조회할 때 : columnName1 = HOSCD; columnName2 = HOSNM /// REFFPF의 경우, "" 추천)</param>
    /// <param name="sc">대상 외부 데이터베이스 연결객체</param>
    public static void gp_SetBoundLookUp(LookUpEdit lkup, string tableName, string columnName1, string columnName2, SqlConnection sc)
    {
        gp_ExecuteLookupSQLquery(sc, lkup, tableName, columnName1, columnName2);
    }

    // LookupEdit 초기 설정 메서드 (내부 수행 함수)
    private static void gp_ExecuteLookupSQLquery(SqlConnection sc, LookUpEdit lkup, string tableName, string columnName1, string columnName2)
    {
        StringBuilder strSql = new StringBuilder();
        strSql.Clear();
        strSql.AppendLine(" ");
        strSql.AppendLine(" WITH ITEM_INFO AS ( ");

        strSql.AppendLine(" SELECT '' AS CD               ");
        strSql.AppendLine("      , '' AS NM               ");
        strSql.AppendLine("      , -1 AS SEQ              ");

        strSql.AppendLine(" UNION ALL                     ");

        if (tableName.Equals("REFFPF"))
        {
            if (columnName1.Equals("EQUIPGB"))
            {
                if (columnName2.Equals("LINE"))
                {
                    strSql.AppendLine(" SELECT A.REFNO AS CD                                 ");
                    strSql.AppendLine("      , A.RETNM AS NM                                 ");
                    strSql.AppendLine("      , ROW_NUMBER() OVER(ORDER BY ISNULL(CONVERT(INT, A.BIGO3),0)) AS SEQ ");
                    strSql.AppendLine("   FROM REFFPF A                                  ");
                    strSql.AppendLine("  WHERE A.RCDTP = '" + columnName1 + "'               ");
                    strSql.AppendLine("    AND A.REFNO <> '?'                                ");
                    strSql.AppendLine("    AND A.BIGO3 <> ''                                 ");
                    strSql.AppendLine("    AND A.BIGO3 IS NOT NULL                           ");
                }
                else
                {
                    strSql.AppendLine(" SELECT A.REFNO AS CD                                 ");
                    strSql.AppendLine("      , A.RETNM AS NM                                 ");
                    strSql.AppendLine("      , A.PRTSQ AS SEQ                                ");
                    strSql.AppendLine("   FROM REFFPF A                                  ");
                    strSql.AppendLine("  WHERE A.RCDTP = '" + columnName1 + "'               ");
                    strSql.AppendLine("    AND A.REFNO <> '?'                                ");
                }
            }
            else
            {
                strSql.AppendLine(" SELECT A.REFNO AS CD                                 ");
                strSql.AppendLine("      , A.RETNM AS NM                                 ");
                strSql.AppendLine("      , A.PRTSQ AS SEQ                                ");
                strSql.AppendLine("   FROM REFFPF A                                  ");
                strSql.AppendLine("  WHERE A.RCDTP = '" + columnName1 + "'               ");
                strSql.AppendLine("    AND A.REFNO <> '?'                                ");
            }
        }
        else
        {
            strSql.AppendLine(" SELECT A." + columnName1 + " AS CD                                 ");
            strSql.AppendLine("      , A." + columnName2 + " AS NM                                 ");
            strSql.AppendLine("      , ROW_NUMBER() OVER(ORDER BY A." + columnName1 + ") AS SEQ    ");
            strSql.AppendLine("   FROM " + tableName + " A                                         ");
        }

        strSql.AppendLine(" )                            ");
        strSql.AppendLine(" SELECT CD, NM FROM ITEM_INFO ");
        strSql.AppendLine("  ORDER BY SEQ, CD            ");

        lkup.Properties.DataSource = DBConn.GetDataTable(sc, strSql.ToString());
        lkup.Properties.ValueMember = "CD";
        lkup.Properties.DisplayMember = "NM";
        lkup.Properties.ShowHeader = false;
        lkup.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
        lkup.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
    }

    /// <summary>
    /// 그리드 LookupEdit 초기 설정 메서드,
    /// 사용 예 : SetBoundGridLookUp(RepoHOUSE, "HOUSF", "HOUSE", "HOUNM"); 
    ///           SetBoundGridLookUp(RepoWLINE, "REFFPF", "WLINE", "");
    /// </summary>
    /// <param name="lkup">대상 GridLookupEdit 컨트롤</param>
    /// <param name="tableName">참조할 테이블명</param>
    /// <param name="columnName1">참조할 테이블의 코드명</param>
    /// <param name="columnName2">참조할 테이블의 대상 컬럼명 (예, HOUSE 테이블의 창고코드와 창고명을 조회할 때 : columnName1 = HOSCD; columnName2 = HOSNM /// REFFPF의 경우, "" 추천)</param>
    public static void gp_SetBoundGridLookUp(RepositoryItemGridLookUpEdit repositoryLookUp, string tableName, string columnName1, string columnName2)
    {
        gp_ExecuteGridLookupSQLquery(DBConn.dbCon, repositoryLookUp, tableName, columnName1, columnName2);
    }

    /// <summary>
    /// 그리드 LookupEdit 초기 설정 메서드 (외부 DB 참조용),
    /// 사용 예 : SetBoundGridLookUp(RepoHOUSE, "HOUSF", "HOUSE", "HOUNM"); 
    ///           SetBoundGridLookUp(RepoWLINE, "REFFPF", "WLINE", "");
    /// </summary>
    /// <param name="lkup">대상 GridLookupEdit 컨트롤</param>
    /// <param name="tableName">참조할 테이블명</param>
    /// <param name="columnName1">참조할 테이블의 코드명</param>
    /// <param name="columnName2">참조할 테이블의 대상 컬럼명 (예, HOUSE 테이블의 창고코드와 창고명을 조회할 때 : columnName1 = HOSCD; columnName2 = HOSNM /// REFFPF의 경우, "" 추천)</param>
    /// <param name="sc">대상 외부 데이터베이스 연결객체</param>
    public static void gp_SetBoundGridLookUp(RepositoryItemGridLookUpEdit repositoryLookUp, string tableName, string columnName1, string columnName2, SqlConnection sc)
    {
        gp_ExecuteGridLookupSQLquery(sc, repositoryLookUp, tableName, columnName1, columnName2);
    }

    // GridLookupEdit 초기 설정 메서드 (내부 수행 함수)
    private static void gp_ExecuteGridLookupSQLquery(SqlConnection sc, RepositoryItemGridLookUpEdit repositoryLookUp, string tableName, string columnName1, string columnName2)
    {
        StringBuilder strSql = new StringBuilder();
        strSql.Clear();
        strSql.AppendLine(" ");
        strSql.AppendLine(" WITH ITEM_INFO AS ( ");

        strSql.AppendLine(" SELECT '' AS CD               ");
        strSql.AppendLine("      , '' AS NM               ");
        strSql.AppendLine("      , -1 AS SEQ              ");

        strSql.AppendLine(" UNION ALL                     ");

        if (tableName.Equals("REFFPF"))
        {
            if (columnName1.Equals("EQUIPGB"))
            {
                if (columnName2.Equals("LINE"))
                {
                    strSql.AppendLine(" SELECT A.REFNO AS CD                                 ");
                    strSql.AppendLine("      , A.RETNM AS NM                                 ");
                    strSql.AppendLine("      , ROW_NUMBER() OVER(ORDER BY ISNULL(CONVERT(INT, A.BIGO3),0)) AS SEQ ");
                    strSql.AppendLine("   FROM REFFPF A                                  ");
                    strSql.AppendLine("  WHERE A.RCDTP = '" + columnName1 + "'               ");
                    strSql.AppendLine("    AND A.REFNO <> '?'                                ");
                    strSql.AppendLine("    AND A.BIGO3 <> ''                                 ");
                    strSql.AppendLine("    AND A.BIGO3 IS NOT NULL                           ");
                }
                else
                {
                    strSql.AppendLine(" SELECT A.REFNO AS CD                                 ");
                    strSql.AppendLine("      , A.RETNM AS NM                                 ");
                    strSql.AppendLine("      , A.PRTSQ AS SEQ                                ");
                    strSql.AppendLine("   FROM REFFPF A                                  ");
                    strSql.AppendLine("  WHERE A.RCDTP = '" + columnName1 + "'               ");
                    strSql.AppendLine("    AND A.REFNO <> '?'                                ");
                }
            }
            else
            {
                strSql.AppendLine(" SELECT A.REFNO AS CD                                 ");
                strSql.AppendLine("      , A.RETNM AS NM                                 ");
                strSql.AppendLine("      , A.PRTSQ AS SEQ                                ");
                strSql.AppendLine("   FROM REFFPF A                                  ");
                strSql.AppendLine("  WHERE A.RCDTP = '" + columnName1 + "'               ");
                strSql.AppendLine("    AND A.REFNO <> '?'                                ");
            }
        }
        else
        {
            strSql.AppendLine(" SELECT A." + columnName1 + " AS CD                                 ");
            strSql.AppendLine("      , A." + columnName2 + " AS NM                                 ");
            strSql.AppendLine("      , ROW_NUMBER() OVER(ORDER BY A." + columnName1 + ") AS SEQ    ");
            strSql.AppendLine("   FROM " + tableName + " A                                         ");
        }

        strSql.AppendLine(" )                            ");
        strSql.AppendLine(" SELECT CD, NM FROM ITEM_INFO ");
        strSql.AppendLine("  ORDER BY SEQ                ");

        repositoryLookUp.DataSource = DBConn.GetDataTable(sc, strSql.ToString());
        repositoryLookUp.ValueMember = "CD";
        repositoryLookUp.DisplayMember = "NM";

        repositoryLookUp.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
        repositoryLookUp.PopupFilterMode = PopupFilterMode.Contains;
        repositoryLookUp.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
        repositoryLookUp.PopupView.OptionsFilter.AllowFilterIncrementalSearch = true;
        repositoryLookUp.ImmediatePopup = true;
        repositoryLookUp.View.OptionsView.ShowColumnHeaders = false;
    }
    #endregion

    #region [JSON 관련]
    public static string gp_DataTableToJsonWithNewtonSoft(DataTable table)
    {
        string JSONstring = string.Empty;
        JSONstring = JsonConvert.SerializeObject(table);
        return JSONstring;
    } 
    #endregion

    #region [FTP 업로드-다운로드]
    /// <summary>
    /// FTP 디렉토리에 같은 이름의 파일 존재 유무 검사 메서드
    /// </summary>
    /// <param name="addr">FTP 디렉토리 경로</param>
    /// <param name="FtpId">FTP ID</param>
    /// <param name="FtpPw">FTP PASS</param>
    /// <returns>FTP 파일존재 여부</returns>
    public static bool gp_FtpFileExists(string addr, string FtpId, string FtpPw)
    {
        bool IsExists = true;
        FtpWebRequest reqFTP = null;
        FtpWebResponse respFTP = null;

        try
        {
            reqFTP = (FtpWebRequest)WebRequest.Create(addr);
            reqFTP.Credentials = new NetworkCredential(FtpId, FtpPw);
            reqFTP.Method = WebRequestMethods.Ftp.GetFileSize;
            respFTP = (FtpWebResponse)reqFTP.GetResponse();

            if (respFTP.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable)
            {
                IsExists = false;
            }
        }
        catch (Exception ex)
        {
            IsExists = false;
        }
        finally
        {
            if (reqFTP != null)
            {
                reqFTP = null;
            }
            if (respFTP != null)
            {
                respFTP = null;
            }
        }
        return IsExists;
    }

    public static bool gp_FTP_CheckDir(string dir)
    {
        string str = string.Empty;
        bool _isExist = false;

        try
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(dir);
            request.Credentials = new NetworkCredential(ComnString.FTP_ID, ComnString.FTP_PW);
            request.Method = WebRequestMethods.Ftp.ListDirectory;
            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
            {
                response.Close();
                _isExist = true;
            }
        }
        catch (WebException e)
        {
            FtpWebResponse response = (FtpWebResponse)e.Response;
            if (response.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable)
            {
                str = "폴더 없음";
            }
            else if (e.Status == WebExceptionStatus.ProtocolError)
            {
                str = string.Format("상태코드 : {0}", ((HttpWebResponse)e.Response).StatusCode);
                str += string.Format("\r\n상태설명 : {0}", ((HttpWebResponse)e.Response).StatusDescription);
            }
            else
            {
                str = "Error : " + e.Message;
            }
            return _isExist;
        }
        catch (Exception ex)
        {
            XtraMessageBox.Show(ex.Message);
        }
        return _isExist;
    }

    public static void gp_FTP_MakeDir(string dir)
    {
        try
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(dir);
            request.Credentials = new NetworkCredential(ComnString.FTP_ID, ComnString.FTP_PW);
            request.Method = WebRequestMethods.Ftp.MakeDirectory;
            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
            {
                response.Close();
            }
        }
        catch (WebException ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// 저장 시에 호출하는 FTP 업로드 메서드
    /// </summary>
    /// <param name="paths"></param>
    /// <param name="DIR"></param>
    /// <param name="SLINO"></param>
    public static void gp_FTP_Upload_Files(Dictionary<string, string[]> paths, string DIR, string SLINO)
    {
        try
        {
            string FTP_DIR = DIR + @"/" + SLINO;
            if (!gp_FTP_CheckDir(FTP_DIR))
            {
                gp_FTP_MakeDir(FTP_DIR);
            }
            foreach (string[] path in paths.Values)
            {
                for (int i = 0; i < path.Length; i++)
                {
                    string sFileName = Path.GetFileName(path[i]);
                    //Read the contents of the file into a stream
                    var fileStream = new FileStream(path[i], FileMode.Open);
                    byte[] FILE = new byte[fileStream.Length];
                    fileStream.Read(FILE, 0, FILE.Length);
                    string FtpFilePath = FTP_DIR + @"/" + sFileName;
                    if (gp_FtpFileExists(FtpFilePath, ComnString.FTP_ID, ComnString.FTP_PW))
                    {
                        if (XtraMessageBox.Show("이미 있는 파일입니다. 덮어쓸까요?" + "\r\n파일명 : " + sFileName
                            , "중복 파일", MessageBoxButtons.YesNo) != DialogResult.Yes)
                        {
                            continue;
                        }
                    }
                    gp_FTPUpload(FtpFilePath, ComnString.FTP_ID, ComnString.FTP_PW, FILE);
                }
            }
        }
        catch (Exception ex)
        {
            XtraMessageBox.Show(ex.Message);
            return;
        }
    }

    public static void gp_FTP_Download_Files(string dir, string[] fileArr)
    {
        try
        {
            using (XtraFolderBrowserDialog dialog = new XtraFolderBrowserDialog())
            {
                dialog.Title = "저장할 폴더 선택";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string FTP_dir = string.Empty;
                    string COM_dir = string.Empty;
                    for (int i = 0; i < fileArr.Length; i++)
                    {
                        FTP_dir = dir + @"/" + fileArr[i];
                        COM_dir = dialog.SelectedPath + @"/" + fileArr[i];

                        // WebRequest.Create로 Http,Ftp,File Request 객체를 모두 생성할 수 있다.
                        FtpWebRequest request = (FtpWebRequest)WebRequest.Create(FTP_dir);
                        request.Method = WebRequestMethods.Ftp.DownloadFile;
                        request.Credentials = new NetworkCredential(ComnString.FTP_ID, ComnString.FTP_PW);
                        using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                        {
                            Stream stream = response.GetResponseStream();
                            string data; // 결과값 문자열 (Binary로도 읽을 수 있다)                
                            using (StreamReader reader = new StreamReader(stream))
                            {
                                data = reader.ReadToEnd();
                            }
                            File.WriteAllText(COM_dir, data);
                        }
                    }
                    XtraMessageBox.Show("저장되었습니다.");
                }
            }
        }
        catch (Exception ex)
        {
            XtraMessageBox.Show(ex.Message);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="directoryPath">FTP 경로</param>
    /// <param name="_FTPuserID">ID</param>
    /// <param name="_FTPpassword">PASS</param>
    /// <param name="data">DATA</param>
    public static void gp_FTPUpload(string directoryPath, string _FTPuserID, string _FTPpassword, byte[] data)
    {
        //업로드 위한 설정
        try
        {
            FtpWebRequest req = (FtpWebRequest)WebRequest.Create(directoryPath);
            req.Method = WebRequestMethods.Ftp.UploadFile;
            req.Credentials = new NetworkCredential(_FTPuserID, _FTPpassword);
            req.ContentLength = data.Length;

            // RequestStream에 데이타를 쓴다
            Stream reqStream = req.GetRequestStream();
            reqStream.Write(data, 0, data.Length);
            reqStream.Close();
            FtpWebResponse response = (FtpWebResponse)req.GetResponse();
            response.Close();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// FTP 경로의 디렉토리를 점검하고 없으면 생성
    /// </summary>
    /// <param name="directoryPath">FTP 디렉토리 경로</param>
    /// <param name="_FTPuserID">FTP ID</param>
    /// <param name="_FTPpassword">FTP PASS</param>
    public static void gp_FTPDirectioryCheck(string directoryPath, string _FTPuserID, string _FTPpassword)
    {
        string[] directoryPaths = directoryPath.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
        string[] result = new string[directoryPaths.Length - 1];
        for (int i = 0; i < result.Length; i++)
        {
            if (i == 0)
            {
                result[i] = directoryPaths[i] + "//" + directoryPaths[i + 1];
            }
            else
            {
                result[i] = directoryPaths[i + 1];
            }
        }

        string currentDirectory = string.Empty;
        foreach (string directory in result)
        {
            currentDirectory += string.Format("{0}/", directory);
            if (!gp_IsExistDirectory(currentDirectory, _FTPuserID, _FTPpassword))
            {
                gp_MakeDirectory(currentDirectory, _FTPuserID, _FTPpassword);
            }
        }
    }

    /// <summary>
    /// FTP 디렉토리 존재 여부 확인
    /// </summary>
    /// <param name="Directory">FTP 디렉토리 경로</param>
    /// <param name="_FTPuserID">FTP ID</param>
    /// <param name="_FTPpassword">FTP PASS</param>
    /// <returns>경로 존재 여부</returns>
    public static bool gp_IsExistDirectory(string Directory, string _FTPuserID, string _FTPpassword)
    {
        try
        {
            var request = (FtpWebRequest)WebRequest.Create(Directory);
            request.Method = WebRequestMethods.Ftp.ListDirectory;
            request.Credentials = new NetworkCredential(_FTPuserID, _FTPpassword);

            using (request.GetResponse())
            {
                return true;
            }
        }
        catch (WebException)
        {
            return false;
        }
    }

    /// <summary>
    /// FTP 디렉토리 생성
    /// </summary>
    /// <param name="Directory">FTP 디렉토리 경로</param>
    /// <param name="_FTPuserID">FTP ID</param>
    /// <param name="_FTPpassword">FTP PASS</param>
    /// <returns></returns>
    public static bool gp_MakeDirectory(string Directory, string _FTPuserID, string _FTPpassword)
    {
        string URI = Directory;
        System.Net.FtpWebRequest ftp = WebRequest.Create(new Uri(URI)) as FtpWebRequest;
        ftp.Credentials = new NetworkCredential(_FTPuserID, _FTPpassword);
        ftp.UseBinary = true;
        ftp.UsePassive = true;
        ftp.Timeout = 10000;
        ftp.Method = System.Net.WebRequestMethods.Ftp.MakeDirectory;

        try
        {
            string str = gp_GetStringResponse(ftp);
        }
        catch (Exception ex)
        {
            return false;
        }
        return true;
    }
    public static string gp_GetStringResponse(FtpWebRequest ftp)
    {
        string result = "";
        using (FtpWebResponse response = (FtpWebResponse)ftp.GetResponse())
        {
            long size = response.ContentLength;
            using (Stream datastream = response.GetResponseStream())
            {
                if (datastream != null)
                {
                    using (StreamReader sr = new StreamReader(datastream))
                    {
                        result = sr.ReadToEnd();
                        sr.Close();
                    }
                    datastream.Close();
                }
            }
            response.Close();
        }
        return result;
    }
    #endregion

    public static void gp_FTP_Delete(string directory, string _FTPuserID, string _FTPpassword)
    {
        try
        {
            if (gp_FtpFileExists(directory, _FTPuserID, _FTPpassword))
            {
                FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(directory);
                ftpRequest.Credentials = new NetworkCredential(_FTPuserID, _FTPpassword);
                ftpRequest.EnableSsl = false;
                //ftpRequest.Method = WebRequestMethods.Ftp.RemoveDirectory;
                ftpRequest.Method = WebRequestMethods.Ftp.DeleteFile;

                FtpWebResponse ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
                ftpResponse.Close();
            }
        }
        catch (Exception ex)
        {
            //gp_PrintMessage(ex.Message, "FTP 파일삭제", MessageType.오류);
        }
    }

    #region [ 다른 탭으로 이동하게 하는 메서드 ]
    // 폼을 MDI에 종속하여 열기 (메인 MDI 탭 컨트롤에 붙이기)
    public static void gp_FormCheck(XtraForm frm, XtraForm parent)
    {
        SplashScreenManager.ShowForm(typeof(WaitForm1));
        try
        {
            if (gp_FormIsExist(frm.GetType(), parent))
            {
                frm.Dispose();
                SplashScreenManager.CloseForm();
                return;
            }
            else
            {
                frm.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
                frm.MdiParent = parent;
                frm.KeyPreview = true;
                frm.Show();
                //frm.GotFocus += gp_GotFocus;
                SplashScreenManager.CloseForm();
            }
        }
        catch
        {
            gp_PrintMessage("메뉴 열기에 실패하였습니다. 관리자에게 문의하세요.", "BTN LAB", MessageType.오류);
            SplashScreenManager.CloseForm();
            return;
        }
    }

    // 폼을 MDI에 종속하지 않고 새 창에서 열기 (MDI 탭 컨트롤에 붙지 않음)
    public static void gp_FormCheck_Popup(XtraForm frm, XtraForm parent)
    {
        SplashScreenManager.ShowForm(typeof(WaitForm1));
        try
        {
            if (gp_FormIsExist(frm.GetType(), parent))
            {
                frm.Dispose();
                SplashScreenManager.CloseForm();
                return;
            }
            else
            {
                foreach (Form openform in parent.OwnedForms)        // 현재 폼(메인메뉴)가 소유중인 폼 컬렉션
                {
                    if (openform.Name.Equals(frm.Name))             // 생성하려는 폼(frm)이 메인메뉴가 이미 소유중(폼이 열려있는 상태)인 폼이라면
                    {
                        openform.WindowState = FormWindowState.Normal;      // 최소화 상태일 때의 예외처리
                        openform.BringToFront();                            // 맨 앞으로 포커싱
                        frm.Dispose();                                      // 생성하려는 폼 리소스 할당해제
                                                                            /// 폼 리소스 할당해제가 아닌, 갱신형태로 리로드 되어야함 (추후 추가 예정)
                        //ReloadForm(frm);
                        SplashScreenManager.CloseForm();
                        return;
                    }
                }
                frm.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
                frm.KeyPreview = true;
                frm.Show();
                frm.Owner = parent;                                 // 생성하면서 메인메뉴의 소유폼으로 설정
                frm.GotFocus += gp_GotFocus;
                SplashScreenManager.CloseForm();
            }
        }
        catch (Exception ex)
        {
            gp_PrintMessage("창 열기에 실패하였습니다. 관리자에게 문의하세요.", "BTN LAB", MessageType.오류);
            SplashScreenManager.CloseForm();
            return;
        }
    }

    // MDI 폼 존재여부 확인
    public static bool gp_FormIsExist(Type tp, XtraForm parent)
    {
        foreach (XtraForm ff in parent.MdiChildren)
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

    /// <summary>
    /// MDI 자식 폼 레벨 하위에서 팝업창(등록/수정 또는 검색) 열기 메서드 (MDI 관계여부에 따라 팝업창의 소유자가 달라짐)
    /// </summary>
    /// <param name="target">오픈 대상 팝업창</param>
    /// <param name="main">MDI 부모 폼 (대부분 Main 메뉴이며, Main 화면이 소유할 필요가 없을 경우 null)</param>
    /// <param name="sub">MDI 자식 폼 (팝업창을 불러온 폼 메뉴, 거의 대부분의 경우에서 this)</param>
    public static void gp_OpenPopupForm(XtraForm target, XtraForm main, XtraForm sub)
    {
        if (main != null) { gp_FormCheck_Popup(target, main); }       // MDI 관계일 때, Main 폼이 소유
        else { gp_FormCheck_Popup(target, sub); }                     // MDI 관계가 아닐 때(플로팅 상태), 해당 폼이 소유
    }
    #endregion

    #region [엑셀 관련]
    public static void gp_ExportExcelFile(string sFileName, GridControl grid)
    {
        string FileName = string.Empty;
        FileDialog fileDlg = new SaveFileDialog();

        try
        {
            string sFileNM = sFileName + DateTime.Now.ToLongDateString().Replace(" ", "");
            string sFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            fileDlg.InitialDirectory = sFolderPath;
            fileDlg.FileName = sFileNM;
            fileDlg.Filter = "Excel files (*.xls or .xlsx)|.xls;*.xlsx";

            if (fileDlg.ShowDialog() == DialogResult.OK)
            {
                FileName = fileDlg.FileName;

                grid.ExportToXls(FileName + ".xls");
                Process.Start(FileName + ".xls");
            }
            fileDlg.Dispose();
        }
        catch (Exception ex)
        {
            if (ex.Message.Equals("Error Opening file"))
            {
                //파일이 열려있음 체크
                MessageBox.Show(((ex.InnerException).InnerException).Message);
            }
        }
    }

    // 엑셀 파일 저장 (3개 시트병합, 시스템 로그 포함)
    public static void gp_ExportExcelFile(string sFileName, GridControl grid1, GridControl grid2, GridControl grid3, string sheetname1, string sheetname2, string sheetname3, string name, string text)
    {
        string FileName = string.Empty;
        FileDialog fileDlg = new SaveFileDialog();
        string TempName = string.Empty;

        try
        {
            string sFileNM = sFileName + "_" + DateTime.Now.ToLongDateString().Replace(" ", "");
            string sFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            fileDlg.InitialDirectory = sFolderPath;
            fileDlg.FileName = sFileNM;
            fileDlg.Filter = "Excel files (*.xlsx or *.xls)|*.xlsx;*.xls";

            if (fileDlg.ShowDialog() == DialogResult.OK)
            {
                FileName = fileDlg.FileName;
                string[] FileNameNondat = FileName.Split('.');
                TempName = FileNameNondat[0];

                //XlsxExportOptions xlsxOptions1 = new XlsxExportOptions();
                //xlsxOptions1.ShowGridLines = true;
                //xlsxOptions1.TextExportMode = TextExportMode.Text;
                //xlsxOptions1.ExportHyperlinks = true;
                //xlsxOptions1.SheetName = sheetname1;                        // 시트명 변경
                //xlsxOptions1.ExportMode = XlsxExportMode.DifferentFiles;

                //XlsxExportOptions xlsxOptions2 = new XlsxExportOptions();
                //xlsxOptions2.ShowGridLines = true;
                //xlsxOptions2.TextExportMode = TextExportMode.Text;
                //xlsxOptions2.ExportHyperlinks = true;
                //xlsxOptions2.SheetName = sheetname2;                        // 시트명 변경
                //xlsxOptions2.ExportMode = XlsxExportMode.DifferentFiles;

                //XlsxExportOptions xlsxOptions3 = new XlsxExportOptions();
                //xlsxOptions3.ShowGridLines = true;
                //xlsxOptions3.TextExportMode = TextExportMode.Text;
                //xlsxOptions3.ExportHyperlinks = true;
                //xlsxOptions3.SheetName = sheetname3;                        // 시트명 변경
                //xlsxOptions3.ExportMode = XlsxExportMode.DifferentFiles;

                //// 병합할 엑셀 임시파일 생성
                //grid1.ExportToXlsx(TempName + "_1.xlsx", xlsxOptions1);
                //grid2.ExportToXlsx(TempName + "_2.xlsx", xlsxOptions2);
                //grid3.ExportToXlsx(TempName + "_3.xlsx", xlsxOptions3);

                // 워크북 생성 (엑셀 시트)
                Workbook book1 = new Workbook();
                book1.LoadDocument(TempName + "_1.xlsx", DocumentFormat.Xlsx); // 1번 그리드 정보 로드
                Workbook book2 = new Workbook();
                book2.LoadDocument(TempName + "_2.xlsx", DocumentFormat.Xlsx); // 2번 그리드 정보 로드
                Workbook book3 = new Workbook();
                book3.LoadDocument(TempName + "_3.xlsx", DocumentFormat.Xlsx); // 3번 그리드 정보 로드

                // 시트 병합
                book1.Append(book2);            // 1번 엑셀문서에 2번 시트 붙여넣기
                book1.Append(book3);            // 1번 엑셀문서에 3번 시트 붙여넣기
                book1.SaveDocument(FileName);   // 병합된 1번 엑셀을 최초경로에 저장

                // 엑셀 임시파일 삭제
                File.Delete(TempName + "_1.xlsx");
                File.Delete(TempName + "_2.xlsx");
                File.Delete(TempName + "_3.xlsx");
                Process.Start(FileName);
            }
            fileDlg.Dispose();
            ComnMethod.SetLogInfo(name, text, ComnMethod.CONNECT_TYPE.엑셀);
        }
        catch (Exception ex)
        {
            // 엑셀 임시파일 삭제
            File.Delete(TempName + "_1.xlsx");
            File.Delete(TempName + "_2.xlsx");
            File.Delete(TempName + "_3.xlsx");
            gp_PrintMessage(ex.Message, "엑셀저장 실패", MessageType.오류);
        }
    }

    /// <summary>
    /// 엑셀 내보내기 시, 일부 Row(Cell) 색상 및 폰트 할당
    /// - 그리드의 RowStyle이나 RowCellStyle 같이 코딩 영역에서 발생하는 동적 색상조정은 엑셀에 반영되지 않음
    /// - 코딩 영역이 아닌 디자이너 영역(Appearance)에서 설정한 색상은 엑셀에 반영됨
    /// - 따라서, Appearance에 영향을 주는 AppearanceObject 객체를 생성하여 수동으로 할당
    /// - Row와 Cell이 중복되는 부분의 경우, 이후 적용하는 코드로 덮어짐
    /// </summary>
    /// <param name="gv">대상 그리드 뷰</param>
    /// <param name="color">적용할 색상</param>
    /// <param name="expression">SQL 조건식, "[기준 컬럼의 FieldName] ? 값" 형식으로 작성 (ex. "[TRAMT] >= 10000", "[ITNAM] = '품목A'")</param>
    /// <param name="isRow">적용할 범위 (행 : true(default), 특정 셀 : false)</param>
    /// <param name="fieldName">특정 셀 범위 적용 시(isRow = false) 적용할 셀의 필드명</param>
    /// <param name="font">적용할 폰트 (미사용 시 null(default))</param>
    public static void gp_SetGridRowExportStyle(GridView gv, Color color, string expression, bool isRow = true, string fieldName = "", Font font = null)
    {
        StyleFormatCondition formatCondition = new StyleFormatCondition();
        // 배경색 설정
        formatCondition.Appearance.BackColor = color;
        formatCondition.Appearance.Options.UseBackColor = true;

        // 폰트 설정
        if (font != null)
        {
            formatCondition.Appearance.Font = font;
            formatCondition.Appearance.Options.UseFont = true;
        }

        // 조건방식 설정 (FormatConditionEnum.Expression : SQL 형식의 문자열 수식)
        formatCondition.Condition = FormatConditionEnum.Expression;
        /// 조건 설정
        /// 양식은 "[기준 컬럼의 FieldName] ? 값" 이며, 값이 문자열 형식일 경우 작은 따옴표 붙여서 사용
        /// ex) "[TRAMT] >= 10000", "[ITNAM] = '품목A'"
        formatCondition.Expression = expression;

        // Row에 적용 (디폴트는 false이며, 아래코드 미적용 시, 엑셀에 색상 반영되지 않음)
        if (isRow) { formatCondition.ApplyToRow = true; }
        // 특정 컬럼(셀)에만 적용 (formatCondition.Column = gv.Columns["적용할 컬럼의 FieldName"];)
        else { formatCondition.Column = gv.Columns[fieldName]; }

        // 위의 설정한 AppearanceObject 객체 할당
        gv.FormatConditions.Add(formatCondition);
    }
    #endregion

    #region [이미지 관련]
    /// <summary>
    /// 이미지 -> 바이트 배열
    /// </summary>
    /// <param name="imageIn">대상 이미지</param>
    /// <returns></returns>
    public static byte[] gp_ImageToByteArray(System.Drawing.Image imageIn)
    {
        using (var ms = new MemoryStream())
        {
            imageIn.Save(ms, imageIn.RawFormat);
            return ms.ToArray();
        }
    }

    /// <summary>
    /// 바이트 배열 -> 이미지
    /// </summary>
    /// <param name="byteArrayIn">대상 바이트 배열</param>
    /// <returns></returns>
    public static Image gp_ByteArrayToImage(byte[] byteArrayIn)
    {
        Image returnImage = null;
        try
        {
            System.IO.MemoryStream ms = new MemoryStream(byteArrayIn, 0, byteArrayIn.Length);
            ms.Write(byteArrayIn, 0, byteArrayIn.Length);
            returnImage = Image.FromStream(ms, true);//Exception occurs here
        }
        catch { }
        return returnImage;
    }
    #endregion

    #region [ 버튼에디트 클릭이벤트 및 엔터키 입력시 자동 검색]
    /// <summary>
    /// 버튼에디트에 클릭이벤트 및 엔터키 입력 이벤트 추가 및 검색기능 자동화
    /// Ex) SetButtonEdit(Be_PlnNm, Tx_PlnCd, "사용자")
    /// </summary>
    /// <param name="buttonEdit">버튼에디트 Name</param>
    /// <param name="textEdit">코드값이 들어가는 텍스트에디트 Name</param>
    /// <param name="Gubun">"사용자" OR "거래처" OR "품목"</param>
    /// <param name="SANO">사업자번호(현재 테스트중) 해당 매개변수는 비우면 됨</param>
    public static void gp_SetButtonEdit(ButtonEdit buttonEdit, TextEdit textEdit, string Gubun, TextEdit SANO = null)
    {
        //buttonEdit.ButtonClick += (sender, e) =>
        //{
        //    if (Gubun.Equals("사용자"))
        //    {
        //        UserSelect frm = new UserSelect();
        //        frm.DataRowSendEvent += new UserSelect.SendDataHandler(GetUserInfo);
        //        frm.ShowDialog();
        //    }
        //    else if (Gubun.Equals("거래처"))
        //    {
        //        CvSelect frm = new CvSelect();
        //        frm.DataRowSendEvent += new CvSelect.SendDataHandler(GetCvInfo);
        //        frm.ShowDialog();
        //    }
        //    else if (Gubun.Equals("품목"))
        //    {
        //        ProductSelect frm = new ProductSelect();
        //        frm.DataRowSendEvent += new ProductSelect.SendDataHandler(GetItemInfo);
        //        frm.ShowDialog();
        //    }
        //};

        //buttonEdit.KeyDown += (sender, e) =>
        //{
        //    if (Gubun.Equals("사용자"))
        //    {
        //        ButtonEdit findWord = sender as ButtonEdit;
        //        string FIND_WORD = findWord.EditValue?.ToString();

        //        if (e.KeyCode == Keys.Enter)
        //        {
        //            StringBuilder strSql = new StringBuilder();
        //            strSql.Clear();

        //            strSql.AppendLine(" SELECT * ");
        //            strSql.AppendLine("   FROM ZUSRLST z ");
        //            strSql.AppendLine("  WHERE z.USRNM LIKE '%" + FIND_WORD + "%' ");
        //            strSql.AppendLine("     OR z.USRCD ='" + FIND_WORD + "'");

        //            DataTable dt = DBConn.GetDataTable(DBConn.dbCon, strSql.ToString());

        //            if (dt.Rows.Count == 1)
        //            {
        //                buttonEdit.EditValue = dt.Rows[0]["USRNM"]?.ToString();
        //                textEdit.EditValue = dt.Rows[0]["USRCD"]?.ToString();
        //            }
        //            else
        //            {
        //                UserSelect frm = new UserSelect();
        //                frm.DataRowSendEvent += new UserSelect.SendDataHandler(GetUserInfo);
        //                frm.FindWord = FIND_WORD;
        //                frm.ShowDialog();
        //            }
        //        }
        //    }
        //    else if (Gubun.Equals("거래처"))
        //    {
        //        ButtonEdit findWord = sender as ButtonEdit;
        //        string FIND_WORD = findWord.EditValue?.ToString();

        //        if (e.KeyCode == Keys.Enter)
        //        {
        //            StringBuilder strSql = new StringBuilder();
        //            strSql.Clear();

        //            strSql.AppendLine(" SELECT * ");
        //            strSql.AppendLine("   FROM CVMAST z ");
        //            strSql.AppendLine("  WHERE z.CVCOD LIKE '%" + FIND_WORD + "%' ");
        //            strSql.AppendLine("     OR z.CVNAM LIKE '%" + FIND_WORD + "%' ");

        //            DataTable dt = DBConn.GetDataTable(DBConn.dbCon, strSql.ToString());

        //            if (dt.Rows.Count == 1)
        //            {
        //                buttonEdit.EditValue = dt.Rows[0]["CVNAM"]?.ToString();
        //                textEdit.EditValue = dt.Rows[0]["CVCOD"]?.ToString();
        //                //SANO.EditValue = dt.Rows[0]["SANO"]?.ToString();
        //            }
        //            else
        //            {
        //                CvSelect frm = new CvSelect();
        //                frm.DataRowSendEvent += new CvSelect.SendDataHandler(GetCvInfo);
        //                frm.FindWord = FIND_WORD;
        //                frm.ShowDialog();
        //            }
        //        }
        //    }
        //    else if (Gubun.Equals("품목"))
        //    {
        //        ButtonEdit findWord = sender as ButtonEdit;
        //        string FIND_WORD = findWord.EditValue?.ToString();

        //        if (e.KeyCode == Keys.Enter)
        //        {
        //            StringBuilder strSql = new StringBuilder();
        //            strSql.Clear();

        //            strSql.AppendLine(" SELECT * ");
        //            strSql.AppendLine("   FROM ITEMAS z ");
        //            strSql.AppendLine("  WHERE z.ITCOD LIKE '%" + FIND_WORD + "%' ");
        //            strSql.AppendLine("     OR z.ITNAM LIKE '%" + FIND_WORD + "%' ");
        //            //strSql.AppendLine("     OR z.CVJNO LIKE '%" + FIND_WORD + "%' ");

        //            DataTable dt = DBConn.GetDataTable(DBConn.dbCon, strSql.ToString());

        //            if (dt.Rows.Count == 1)
        //            {
        //                buttonEdit.EditValue = dt.Rows[0]["ITNAM"]?.ToString();
        //                textEdit.EditValue = dt.Rows[0]["ITCOD"]?.ToString();
        //                //SANO.EditValue = dt.Rows[0]["SANO"]?.ToString();
        //            }
        //            else
        //            {
        //                ProductSelect frm = new ProductSelect();
        //                frm.DataRowSendEvent += new ProductSelect.SendDataHandler(GetItemInfo);
        //                frm.FindWord = FIND_WORD;
        //                frm.ShowDialog();
        //            }
        //        }
        //    }
        //};

        buttonEdit.Leave += (sender, e) =>
        {
            ButtonEdit input = sender as ButtonEdit;

            if (string.IsNullOrEmpty(input.EditValue?.ToString()))
            {
                textEdit.EditValue = "";
            }
        };

        void GetUserInfo(DataRow row)
        {
            buttonEdit.EditValue = row["USRNM"];
            textEdit.EditValue = row["USRCD"];
        }

        void GetCvInfo(DataRow row)
        {
            buttonEdit.EditValue = row["CVNAM"];
            textEdit.EditValue = row["CVCOD"];
        }
        void GetItemInfo(DataRow row)
        {
            buttonEdit.EditValue = row["ITNAM"];
            textEdit.EditValue = row["ITCOD"];
        }
    }
    #endregion

    #region [ 엑셀 업로드 ]
    // 22.05.24 추가, 0번 시트 기준
    // 그리드 뷰에 엑셀 데이터 바인딩 메서드
    public static DataTable gp_XlsToTable()
    {
        string _sFileName = string.Empty;
        try
        {
            XtraOpenFileDialog fileDialog = new XtraOpenFileDialog();
            string filePath = Application.StartupPath;
            fileDialog.Filter = "Excel Files (.xlsx; .xls)|*.xlsx;*.xls";
            fileDialog.FilterIndex = 1;
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                // _sFileName : 선택한 파일 경로를 담음
                _sFileName = fileDialog.FileName;
            }

            fileDialog.Dispose();
            if (!string.IsNullOrEmpty(_sFileName))
            {
                ExcelDataSource excelDataSource = gp_GetExcelDataSource(_sFileName);
                ExcelWorksheetSettings workSheetSettings = new ExcelWorksheetSettings();
                // 엑셀 워크시트 네임 받아오기 (x번째 시트, 엑셀 파일명<경로>)
                workSheetSettings.WorksheetName = gp_GetWorkSheetNameByIndex(0, _sFileName);
                excelDataSource.SourceOptions = new ExcelSourceOptions(workSheetSettings)
                {
                    SkipEmptyRows = true,
                    UseFirstRowAsHeader = true
                };
                excelDataSource.Fill();
                _sFileName = string.Empty;
                return gp_ExcelToDataTable(excelDataSource);
            }
            else
            {
                _sFileName = string.Empty;
                return null;
            }
        }
        catch (Exception ex)
        {
            if (ex.Message.Equals("Error Opening file"))
            {
                //MessageBox.Show(((ex.InnerException).InnerException).Message);
            }
            else
            {
                //XtraMessageBox.Show(ex.Message);
            }
            throw ex;
        }
    }

    // 엑셀 데이터 받아오기 메서드
    public static ExcelDataSource gp_GetExcelDataSource(string fileName)
    {
        ExcelDataSource ds = new ExcelDataSource();
        ds.FileName = fileName;
        ExcelSourceOptions excelSourceOptions1 = new ExcelSourceOptions();
        ExcelWorksheetSettings excelWorksheetSettings1 = new ExcelWorksheetSettings();
        excelWorksheetSettings1.WorksheetName = gp_GetWorkSheetNameByIndex(0, fileName);
        excelSourceOptions1.ImportSettings = excelWorksheetSettings1;
        ds.SourceOptions = excelSourceOptions1;
        ds.Fill();
        return ds;
    }

    // 엑셀 워크시트 이름 받아오기 메서드
    public static string gp_GetWorkSheetNameByIndex(int p, string fileName)
    {
        string worksheetName = "";
        //using (ISpreadsheetSource spreadsheetSource = SpreadsheetSourceFactory.CreateSource(fileName))
        //{
        //    IWorksheetCollection worksheetCollection = spreadsheetSource.Worksheets;
        //    worksheetName = worksheetCollection[p].Name;
        //}
        return worksheetName;
    }

    // 엑셀 데이터를 데이터 테이블로 변환 메서드
    public static DataTable gp_ExcelToDataTable(ExcelDataSource excelDataSource)
    {
        IList list = ((IListSource)excelDataSource).GetList();
        DevExpress.DataAccess.Native.Excel.DataView dataView = (DevExpress.DataAccess.Native.Excel.DataView)list;
        List<PropertyDescriptor> props = dataView.Columns.ToList<PropertyDescriptor>();
        DataTable table = new DataTable();
        for (int i = 0; i < props.Count; i++)
        {
            PropertyDescriptor prop = props[i];
            table.Columns.Add(prop.Name, typeof(object));
        }
        object[] values = new object[props.Count];
        foreach (DevExpress.DataAccess.Native.Excel.ViewRow item in list)
        {
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = props[i].GetValue(item);
            }
            table.Rows.Add(values);
        }
        return table;
    }
    #endregion

    #region [기타]
    // 컨트롤 초기화 (SHS: 2020.11.24)
    public static void gp_ResetControl(Control control)
    {
        foreach (Control ctl in control.Controls)
        {
            if (ctl is TextBox) { TextBox tb = (TextBox)ctl; if (string.IsNullOrEmpty(tb.Tag?.ToString())) { tb.Text = string.Empty; } }
            else
            if (ctl is TextEdit) { TextEdit te = (TextEdit)ctl; if (string.IsNullOrEmpty(te.Tag?.ToString())) { te.Text = string.Empty; } }
            else
            if (ctl is MemoEdit) { MemoEdit me = (MemoEdit)ctl; if (string.IsNullOrEmpty(me.Tag?.ToString())) { me.Text = string.Empty; } }
            else
            if (ctl is DateEdit) { DateEdit de = (DateEdit)ctl; if (string.IsNullOrEmpty(de.Tag?.ToString())) { de.Text = string.Empty; } }
            else
            if (ctl is ButtonEdit) { ButtonEdit be = (ButtonEdit)ctl; if (string.IsNullOrEmpty(be.Tag?.ToString())) { be.Text = string.Empty; } }
            else
            if (ctl is LookUpEdit) { LookUpEdit le = (LookUpEdit)ctl; if (string.IsNullOrEmpty(le.Tag?.ToString())) { le.SelectedText = string.Empty; } }
            else
            if (ctl is PictureEdit) { PictureEdit pe = (PictureEdit)ctl; if (string.IsNullOrEmpty(pe.Tag?.ToString())) { pe.Image = null; } }
        }
    }

    // Control에 포커스 가면 색 변경 (SHS: 2020.12.02)
    public static void gp_SetColorFocused(Control sender)
    {
        foreach (Control ctl in sender.Controls)
        {
            if (ctl is TextEdit) { TextEdit tb = (TextEdit)ctl; tb.Properties.AppearanceFocused.BackColor = System.Drawing.Color.LightYellow; }
            else
            if (ctl is MemoEdit) { MemoEdit me = (MemoEdit)ctl; me.Properties.AppearanceFocused.BackColor = System.Drawing.Color.LightYellow; }
            else
            if (ctl is LookUpEdit) { LookUpEdit le = (LookUpEdit)ctl; le.Properties.AppearanceFocused.BackColor = System.Drawing.Color.LightYellow; }
        }
    }
    #endregion

    // 22.01.25 추가
    /// <summary>
    /// 컨트롤 규칙 자동 초기 세팅
    /// </summary>
    /// <param name="control">레이아웃 컨트롤, 대부분의 경우 layoutControl1</param>
    public static void gp_InitControllerRule(Control control)
    {
        foreach (Control ctrl in control.Controls)
        {
            if (ctrl is DateEdit)
            {
                DateEdit dt = (DateEdit)ctrl;
                dt.Properties.ShowClear = false;
                dt.EnterMoveNextControl = true;
                dt.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
                dt.Properties.Mask.UseMaskAsDisplayFormat = true;
                dt.Properties.AppearanceFocused.BackColor = System.Drawing.Color.LightYellow;
            }
            else if (ctrl is ComboBoxEdit)
            {
                ComboBoxEdit cb = (ComboBoxEdit)ctrl;
                cb.EnterMoveNextControl = true;
                cb.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
                cb.Properties.AppearanceFocused.BackColor = System.Drawing.Color.LightYellow;
            }
            else if (ctrl is TextEdit)
            {
                TextEdit tx = (TextEdit)ctrl;
                tx.EnterMoveNextControl = true;
                tx.ImeMode = ImeMode.Hangul;
                tx.Properties.AppearanceFocused.BackColor = System.Drawing.Color.LightYellow;
            }
            else if (ctrl is ButtonEdit)
            {
                ButtonEdit be = (ButtonEdit)ctrl;
                be.EnterMoveNextControl = true;
                be.ImeMode = ImeMode.Hangul;
                be.Properties.AppearanceFocused.BackColor = System.Drawing.Color.LightYellow;
            }
            else if (ctrl is LookUpEdit)
            {
                LookUpEdit lk = (LookUpEdit)ctrl;
                lk.EnterMoveNextControl = true;
                lk.Properties.AppearanceFocused.BackColor = System.Drawing.Color.LightYellow;
            }
            else if (ctrl is RadioGroup)
            {
                RadioGroup rg = (RadioGroup)ctrl;
                rg.EnterMoveNextControl = true;
            }
            //else if (ctrl is MemoEdit)
            //{
            //    MemoEdit me = (MemoEdit)ctrl;
            //    me.ImeMode = ImeMode.Hangul;
            //    me.Properties.AppearanceFocused.BackColor = System.Drawing.Color.LightYellow;
            //    //me.EnterMoveNextControl = true;
            //}
            else if (ctrl is CheckEdit)
            {
                CheckEdit ck = (CheckEdit)ctrl;
                ck.Properties.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
                ck.EnterMoveNextControl = true;
            }
        }
    }

    // 컨트롤 초기화 (SHS: 2020.11.24)
    public static void gp_ResetCompo(Control sender)
    {

        foreach (Control ctl in sender.Controls)
        {
            if (ctl is TextBox) { TextBox tb = (TextBox)ctl; if (string.IsNullOrEmpty(tb.Tag?.ToString())) { tb.Text = string.Empty; } }
            else
            if (ctl is TextEdit) { TextEdit tb = (TextEdit)ctl; if (string.IsNullOrEmpty(tb.Tag?.ToString())) { tb.Text = string.Empty; } }
            else
            if (ctl is MemoEdit) { MemoEdit me = (MemoEdit)ctl; if (string.IsNullOrEmpty(me.Tag?.ToString())) { me.Text = string.Empty; } }
            else
            if (ctl is DateEdit) { DateEdit me = (DateEdit)ctl; if (string.IsNullOrEmpty(me.Tag?.ToString())) { me.Text = string.Empty; } }
            else
            if (ctl is ButtonEdit) { ButtonEdit me = (ButtonEdit)ctl; if (string.IsNullOrEmpty(me.Tag?.ToString())) { me.Text = string.Empty; } }
            else
            if (ctl is LookUpEdit) { LookUpEdit cb = (LookUpEdit)ctl; if (string.IsNullOrEmpty(cb.Tag?.ToString())) { cb.SelectedText = string.Empty; } }
        }
    }

    // 22.05.10 추가
    /// <summary>
    /// 프로젝트 공통 키 이벤트 설정
    /// </summary>
    /// <param name="sbt">버튼 배열</param>
    /// <param name="e">키 이벤트 핸들러</param>
    public static void gp_CommonButtonKeyEvent(SimpleButton[] sbt, KeyEventArgs e)
    {
        foreach (SimpleButton bt in sbt)
        {
            // =============== 대부분의 메뉴에 자주 사용되는 버튼 ===============
            // 조회(F5)
            if ((bt.Name.Contains("Retr")) && e.KeyCode == Keys.F5) { bt.PerformClick(); return; }
            // 추가(F1)
            else if ((bt.Name.Contains("Add")) && e.KeyCode == Keys.F1) { bt.PerformClick(); return; }
            // 초기화(F1)
            else if (bt.Name.Contains("Reset") && e.KeyCode == Keys.F1) { bt.PerformClick(); return; }
            // 연속저장(F2)
            else if (bt.Name.Contains("SaveMul") && e.KeyCode == Keys.F2) { bt.PerformClick(); return; }
            // 저장(F3)
            else if ((bt.Name.Contains("Save")) && e.KeyCode == Keys.F3) { bt.PerformClick(); return; }
            // 삭제(F4)
            else if ((bt.Name.Contains("Delete")) && e.KeyCode == Keys.F4) { bt.PerformClick(); return; }
            // 엑셀(F8)
            else if ((bt.Name.Contains("Xls")) && e.KeyCode == Keys.F8) { bt.PerformClick(); return; }
            // 출력(F12)
            else if (bt.Name.Contains("Print") && e.KeyCode == Keys.F12) { bt.PerformClick(); return; }
            // 닫기(ESC)
            else if ((bt.Name.Contains("Close")) && e.KeyCode == Keys.Escape) { bt.PerformClick(); return; }

            // =============== 특정 메뉴에 한정적으로 사용되는 버튼 ===============
            // 엑셀업로드(F1)           // 
            else if (bt.Name.Contains("UpXls") && e.KeyCode == Keys.F1) { bt.PerformClick(); return; }
            // 엑셀선택(F2)             // 
            else if (bt.Name.Contains("SelXls") && e.KeyCode == Keys.F2) { bt.PerformClick(); return; }
            // 연마감(F10), DeadLine    // 
            else if (bt.Name.Contains("DLine") && e.KeyCode == Keys.F10) { bt.PerformClick(); return; }
        }
    }

    // 22.05.12 추가
    /// <summary>
    /// 프로젝트 공통 마스터 조회 메서드 설정 (프로시저)
    /// </summary>
    /// <param name="_command">CMD, 필수항목</param>
    /// <param name="_dateF">시작일자, 필수항목</param>
    /// <param name="dateF_format">시작일자 형식 ex) yyyy-MM-dd, 필수항목</param>
    /// <param name="_dateT">종료일자, 없으면 null</param>
    /// <param name="dateT_format">종료일자 형식 ex) yyyy-MM-dd, 종료일자 있으면 필수항목, 없으면 null</param>
    /// <param name="_idx">찾을 항목, 없으면 null</param>
    /// <param name="_word">찾을 단어, 없으면 null</param>
    /// <param name="_gridM">마스터 그리드, 필수항목</param>
    /// <param name="_procedure">프로시저명, 필수항목</param>
    public static void gp_CommonRetrMethod(string _command
                                      , DateEdit _dateF, string dateF_format
                                      , DateEdit _dateT, string dateT_format
                                      , ComboBoxEdit _idx, TextEdit _word, GridControl _gridM
                                      , string _procedure)
    {
        string sDateF = string.Empty;
        string sDateT = string.Empty;
        string sIdx = string.Empty;
        string sWord = string.Empty;

        // 시작일자
        if (_dateF != null && dateF_format.Equals("yyyy"))
            sDateF = _dateF.EditValue?.ToString().Substring(0, 4);
        else if (_dateF != null && dateF_format.Equals("yyyy-MM"))
            sDateF = _dateF.EditValue?.ToString().Substring(0, 7);
        else if (_dateF != null && dateF_format.Equals("yyyy-MM-dd"))
            sDateF = _dateF.EditValue?.ToString().Substring(0, 10);

        // 종료일자
        if (_dateT != null && dateT_format.Equals("yyyy"))
            sDateT = _dateT.EditValue?.ToString().Substring(0, 4);
        else if (_dateT != null && dateT_format.Equals("yyyy-MM"))
            sDateT = _dateT.EditValue?.ToString().Substring(0, 7);
        else if (_dateT != null && dateT_format.Equals("yyyy-MM-dd"))
            sDateT = _dateT.EditValue?.ToString().Substring(0, 10);

        parameterList.Clear();
        parameterList.Add(new Parameter("CMD", _command));
        parameterList.Add(new Parameter("DATE_F", sDateF));
        if (_dateT != null)
            parameterList.Add(new Parameter("DATE_T", sDateT));
        // 찾을 항목
        if (_idx != null)
        {
            sIdx = _idx.SelectedIndex.ToString();
            parameterList.Add(new Parameter("FIND_IDX", sIdx));
        }
        // 찾을 단어
        if (_word != null)
        {
            sWord = _word.EditValue?.ToString();
            parameterList.Add(new Parameter("FIND_WORD", sWord));
        }

        DataTable dt = DBConn.GetDataTable(DBConn.dbCon, _procedure, parameterList);
        if (dt != null)
        {
            _gridM.DataSource = dt;
            if (dt.Rows.Count > 0)
                _gridM.Focus();
            else
            {
                if (_word != null)
                    _word.Focus();
                else
                    _dateF.Focus();
            }
        }
    }

    // 22.06.09 추가 (현재는 LabelControl만)
    /// <summary>
    /// 컨트롤 크기에 맞춰 폰트크기 자동 조절 메서드
    /// (사용 예시, label.Font = AutoFontSize(label, label.Text)
    /// </summary>
    /// <param name="label">컨트롤 (현재는 LabelControl만)</param>
    /// <param name="text"> 컨트롤 텍스트 (ex. label.Text)</param>
    /// <returns>크기 조절한 Font 반환</returns>
    public static Font gp_AutoFontSize(LabelControl label, string text)
    {
        Font ft;
        Graphics gp;
        SizeF sz;
        float Faktor, FaktorX, FaktorY;

        gp = label.CreateGraphics();
        sz = gp.MeasureString(text, label.Font);
        gp.Dispose();

        FaktorX = (label.Width) / sz.Width;
        FaktorY = (label.Height) / sz.Height;

        if (FaktorX > FaktorY)
            Faktor = FaktorY;
        else
            Faktor = FaktorX;
        ft = label.Font;

        return new Font(ft.Name, ft.SizeInPoints * (Faktor) - 1);
    }

    // 22.11.10 추가
    /// <summary>
    /// 그리드 컨트롤 서브 팝업메뉴 생성 및 폼 로드 메서드
    /// </summary>
    /// <param name="_view">대상 그리드 뷰</param>
    /// <param name="e">PopupMenuShowing 이벤트 파라미터</param>
    /// <param name="_caption">생성할 팝업메뉴 텍스트</param>
    /// <param name="_frm">불러올 폼 코드 (ex. BY001F01)</param>
    public static void gp_CreatePopupSubMenu(GridView _view, PopupMenuShowingEventArgs e, string _caption, string _frm)
    {
        formName = _frm;
        if (e.MenuType == GridMenuType.Row)
        {
            int rowHandle = e.HitInfo.RowHandle;
            e.Menu.Items.Clear();
            e.Menu.Items.Add(gp_CreateSubMenuInfo(_view, rowHandle, _caption));
        }
    }
    // 그리드 뷰 Row 정보 클래스
    public class RowInfo
    {
        // 기본
        public GridView View;
        public int RowHandle;
        public RowInfo(GridView view, int rowHandle)
        {
            this.RowHandle = rowHandle;
            this.View = view;
        }

        // SA015F00
        public string SliNo;
        public string ChkYn;
        public int AWeit;
        public string IpChlNo;
        public RowInfo(GridView view, int rowHandle, string slino, string chkyn, int aweit = 0)
        {
            this.RowHandle = rowHandle;
            this.View = view;
            this.SliNo = slino;
            this.ChkYn = chkyn;
            this.AWeit = aweit;
        }
    }
    // 서브 팝업메뉴 생성 메서드
    private static DXMenuItem gp_CreateSubMenuInfo(GridView _view, int _rowHandle, string _caption)
    {
        DXMenuItem subMenu = new DXMenuItem(_caption);
        subMenu.Tag = new RowInfo(_view, _rowHandle);
        subMenu.Enabled = _view.IsDataRow(_rowHandle);
        subMenu.Click += new EventHandler(gp_SubMenuClick);
        return subMenu;
    }
    // 팝업 서브메뉴 클릭 시 폼 로드 이벤트
    private static void gp_SubMenuClick(object sender, EventArgs e)
    {
        DXMenuItem menuItem = sender as DXMenuItem;
        RowInfo ri = menuItem.Tag as RowInfo;
        if (ri != null)
        {
            //// 계근관리
            //if (formName.Equals("SA015F00"))
            //{
            //    SA015F02 frm = new SA015F02();
            //    frm.OpenFormEvent += new SA015F02.OpenFormHandler(gp_Open_Form);
            //    frm.Show();
            //}
        }
    }

    private static void gp_Open_Form(XtraForm targetForm)
    {
        //if (targetForm.Name.Equals("SA015F02")) { SA015F00 frm = new SA015F00(); gp_FormCheck(frm, _main); }
    }

    /// <summary>
    /// 엑셀 파일 저장 (시스템 로그 포함)
    /// </summary>
    /// <param name="sFileName">엑셀파일 저장 대화상자 디폴트 파일명</param>
    /// <param name="grid">대상 그리드 컨트롤</param>
    /// <param name="name">시스템 로그에 남길 프로그램코드 (대부분 Name)</param>
    /// <param name="text">시스템 로그에 남길 프로그램명 (대부분 Text)</param>
    public static void gp_ExportExcelFile(string sFileName, GridControl grid, string name, string text)
    {
        string FileName = string.Empty;
        FileDialog fileDlg = new SaveFileDialog();

        try
        {
            //string sFileNM = sFileName + "_" + DateTime.Now.ToLongDateString().Replace(" ", "") ;
            string sFileNM = sFileName + " (" + DateTime.Now.ToString("yyMMdd_dddd") + ")";
            string sFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            fileDlg.InitialDirectory = sFolderPath;
            fileDlg.FileName = sFileNM;
            fileDlg.Filter = "Excel files (*.xls or .xlsx)|.xls;*.xlsx";

            if (fileDlg.ShowDialog() == DialogResult.OK)
            {
                FileName = fileDlg.FileName;

                grid.ExportToXls(FileName + ".xls");
                Process.Start(FileName + ".xls");
            }
            fileDlg.Dispose();
            gp_SetLogInfo(name, text, CONNECT_TYPE.엑셀);
        }
        catch (Exception ex)
        {
            if (ex.Message.Equals("Error Opening file"))
            {
                //파일이 열려있음 체크
                MessageBox.Show(((ex.InnerException).InnerException).Message);
            }
        }
    }

    /// <summary>
    /// 오늘 날짜와 자동 번호매김을 조합하여 전표번호 생성 메서드
    /// </summary>
    /// <param name="code">전표 구분자 ex) S, T, W , ...</param>
    /// <param name="date">오늘날짜</param>
    /// <param name="fieldname">해당 테이블의 전표번호 필드명 ex) SLINO, PONO, ...</param>
    /// <param name="tablename">전표번호 체크 대상 테이블명</param>
    /// <returns>자동채번된 전표번호 문자열</returns>
    public static string gp_NextSlipNo(string code, string date, string fieldname, string tablename)
    {
        StringBuilder strSql = new StringBuilder();
        strSql.Clear();
        if (tablename.Equals("ITEMAS"))
        {
            strSql.AppendLine("SELECT ( CASE WHEN ISNULL(MAX(RIGHT(ITCOD,5)),'')= ''");
            strSql.AppendLine("              THEN 'IT00001'                         ");
            strSql.AppendLine("              ELSE CONCAT('IT', RIGHT('00000' + CAST(CONVERT(INT, RIGHT(MAX(ITCOD), 5)) + 1 AS VARCHAR) , 5)) END ) AUTOSLINO ");
            strSql.AppendLine("  FROM ITEMAS ");
        }
        else if (tablename.Equals("ITEMPF"))
        {
            strSql.AppendLine("SELECT ( CASE WHEN ISNULL(MAX(RIGHT(ITCOD,3)),'')= ''");
            strSql.AppendLine("              THEN 'IT001'                         ");
            strSql.AppendLine("              ELSE CONCAT('IT', RIGHT('000' + CAST(CONVERT(INT, RIGHT(MAX(ITCOD), 3)) + 1 AS VARCHAR) , 3)) END ) AUTOSLINO ");
            strSql.AppendLine("  FROM ITEMPF ");
        }
        else if (tablename.Equals("zUSRLST"))
        {
            strSql.AppendLine("SELECT ( CASE WHEN ISNULL(MAX(RIGHT(USRCD,5)),'')= ''");
            strSql.AppendLine("              THEN '00000'                         ");
            strSql.AppendLine("              ELSE RIGHT('00000' + CAST(CONVERT(INT, RIGHT(MAX(USRCD), 5)) + 1 AS VARCHAR) , 5) END ) AUTOSLINO ");
            strSql.AppendLine("  FROM zUSRLST ");
        }
        else
        {
            strSql.AppendLine(" DECLARE @NO VARCHAR(15)='" + code + date + "';   ");
            strSql.AppendLine(" SELECT (CASE WHEN ISNULL(MAX(" + fieldname + "), '')='' THEN REPLACE(@NO, '%', '') + '001'    ");
            strSql.AppendLine(" ELSE SUBSTRING(MAX(" + fieldname + "), 1, 7) + RIGHT('000' + CAST(CONVERT(INT, SUBSTRING(MAX(" + fieldname + "), 8, 3)) + 1 AS VARCHAR), 3) END) AUTOSLINO  ");
            strSql.AppendLine(" FROM " + tablename + " ");
            strSql.AppendLine(" WHERE " + fieldname + " LIKE '%' + @NO + '%' ");
        }
        DataTable dt = DBConn.GetDataTable(DBConn.dbCon, strSql.ToString());
        return dt.Rows[0]["AUTOSLINO"].ToString();
    }

    #region [ 메세지박스 생성 ]
    public enum MessageType { 알림, 경고, 오류, 질문 }      // 메세지박스 출력 시 사용하는 변수 (public)
    // 22.12.08 추가
    /// <summary>
    /// 확인형 메세지박스 자동 생성
    /// </summary>
    /// <param name="_message">출력할 메세지</param>
    /// <param name="_title">메세지박스 제목</param>
    /// <param name="_mt">메세지박스 타입 (알림, 경고, 오류)</param>
    public static void gp_PrintMessage(string _message, string _title, MessageType _mt)
    {
        if (_mt.Equals(MessageType.알림))
        { XtraMessageBox.Show(_message, _title, MessageBoxButtons.OK, MessageBoxIcon.Information); }
        else if (_mt.Equals(MessageType.경고))
        { XtraMessageBox.Show(_message, _title, MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        else if (_mt.Equals(MessageType.오류))
        { XtraMessageBox.Show(_message, _title, MessageBoxButtons.OK, MessageBoxIcon.Error); }
    }

    // 22.12.08 추가
    /// <summary>
    /// 질문형 메세지박스 자동 생성 (예 : True, 아니오 : False)
    /// </summary>
    /// <param name="_message">출력할 메세지</param>
    /// <param name="_title">메세지박스 제목</param>
    /// <param name="_mt">메세지박스 타입 (경고, 질문)</param>
    public static bool gp_PrintQuestion(string _message, string _title, MessageType _mt)
    {
        if (_mt.Equals(MessageType.경고))
        {
            if (XtraMessageBox.Show(_message, _title, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                return true;
            else
                return false;
        }
        else if (_mt.Equals(MessageType.질문))
        {
            if (XtraMessageBox.Show(_message, _title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                return true;
            else
                return false;
        }
        return false;
    }
    #endregion

    #region [ 스마트 1번가 접속 LOG API ]
    // 22.09.15 추가, 스마트 1번가 접속 로그 기록 API
    public static void gp_LogAPI(string sGuBun, string sComip)
    {
        JObject data = new JObject();

        data.Add("crtfcKey", "$5$API$1DVIx96FG95ndK25ElIOHDmvPwKGmu5ZYj0iICdh5fA");
        data.Add("logDt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
        data.Add("useSe", sGuBun);
        data.Add("sysUser", LoginUser.USRNM);
        data.Add("conectIp", DBConn.Client_IP);
        data.Add("dataUsgqty", "0");
        try
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://log.smart-factory.kr/apisvc/sendLogDataJSON.do?logData=" + data.ToString());
            httpWebRequest.Method = "GET";

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                JObject res = JObject.Parse(result);
            }
        }
        catch (Exception ex)
        {
            gp_PrintMessage(ex.Message, "스마트공장 1번가", MessageType.오류);
        }
    }

    // 23.11.08 추가, 스마트 1번가 접속 로그 기록 API (바이트 포함)
    public static void gp_LogAPI(string sGuBun, string sComip, GridControl gc)
    {
        long size = 0;
        try
        {
            Dictionary<string, string> dicParams = new Dictionary<string, string>();
            dicParams.Clear();
            dicParams.Add("CMD", "Log_Retr");
            dicParams.Add("DATE_F", DateTime.Now.ToString("yyyy-MM-dd"));
            dicParams.Add("DATE_T", DateTime.Now.ToString("yyyy-MM-dd"));
            dicParams.Add("LOG_IDX", string.Empty);
            dicParams.Add("FIND_IDX", string.Empty);
            dicParams.Add("FIND_WORD", string.Empty);
            DataTable dt = DBConn.GetDataTable(DBConn.dbCon, "usp_SY004F00", dicParams);
            if (dt != null)
            {
                gc.DataSource = dt;

                // 임시파일 생성
                string temp = Directory.GetCurrentDirectory().ToString() + "\\접속LOG.xlsx";
                gc.ExportToXlsx(temp);

                // 임시파일 정보(바이트) 가져오기
                FileInfo fi = new FileInfo(temp);
                size = fi.Length;

                // 정보(바이트) 가져온 후 임시파일 삭제
                fi.Delete();
            }
        }
        catch (Exception ex)
        {
            gp_PrintMessage(ex.Message, "스마트공장 1번가", MessageType.오류);
            return;
        }

        JObject data = new JObject();

        data.Add("crtfcKey", "$5$API$1DVIx96FG95ndK25ElIOHDmvPwKGmu5ZYj0iICdh5fA");
        data.Add("logDt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
        data.Add("useSe", sGuBun);
        data.Add("sysUser", LoginUser.USRNM);
        data.Add("conectIp", DBConn.Client_IP);
        data.Add("dataUsgqty", size.ToString());
        try
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://log.smart-factory.kr/apisvc/sendLogDataJSON.do?logData=" + data.ToString());
            httpWebRequest.Method = "GET";

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                JObject res = JObject.Parse(result);
            }
        }
        catch (Exception ex)
        {
            gp_PrintMessage(ex.Message, "스마트공장 1번가", MessageType.오류);
        }
    }
    #endregion

    #region [ 비밀번호 정규식 ]
    // 21.08.26 추가, 비밀번호 정규화 검사
    public static bool gp_CheckPassword(string pw)
    {
        /// 영 대/소문자 1개 이상, 숫자 1개 이상,  특수문자 1개 이상, 비밀번호 길이 8자 이상 체크
        //Regex rxPassword = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9]).{8,}$");
        // ^ : 라인의 처음
        // ?= : 전방 탐색
        // .* : 새 라인을 제외한 하나 이상 문자
        // [a-z] : a~z까지 영소문자,   [0-9] : 0~9까지 숫자,   [\W] : 특수문자열(!@#$...)
        // .{8,} : 8자리 이상
        // $ : 라인의 마지막
        Regex rxPassword = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[\W]).{8,}$");

        // 입력한 Pw가 형식에 맞으면 true, 아니면 False 반환
        return rxPassword.IsMatch(pw);

        /// 특수문자 자체 사용방법
        // \^ : ^
        // \. : .
        // \[ : [
        // \$ : $
        // \( : (
        // \) : )
        // \| : |
        // \* : *
        // \+ : +
        // \? : ?
        // \{ : {
        // \\ : \
        // \n : 줄넘김 문자
        // \r : 리턴 문자
        // \w : 알파벳과 _ (언더바)
        // \W : 알파벳과 _ 가 아닌 것
        // \s : 빈 공간(space)
        // \S : 빈 공간이 아닌 것
        // \d : 숫자
        // \D : 숫자가 아닌 것
        // \b : 단어와 단어 사이의 경계
        // \B : 단어 사이의 경계가 아닌 것
        // \t : Tab 문자
        // \xnn : 16진수 nn에 해당하는 문자
    }
    #endregion

    // MDI 자식폼 포커스 변경 시 이벤트 (메인화면 버튼 Visible 관련) 
    public static void gp_GotFocus(object sender, EventArgs e)
    {
        /*
        XtraForm frm = (XtraForm)sender;
        XtraForm temp = (XtraForm)frm.MdiParent;
        Main_New main = (Main_New)temp;
        ComnString._FOCUSED_MENU = frm.Name;
        LayoutControlItem[] lci = new LayoutControlItem[8]
        { main.Lci_Retr, main.Lci_Add, main.Lci_Reset, main.Lci_Save, main.Lci_Delete, main.Lci_Xls, main.Lci_Print, main.Lci_Close };
        foreach (LayoutControlItem item in lci)
            item.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

        if (frm.Name.Equals("SY010F00")) { gp_SetButtonVisible(main, true, true, false, false, true, true, false, true); }
        else if (frm.Name.Equals("CM002F00")) { gp_SetButtonVisible(main, true, true, false, true, true, false, false, true); }
        */
    }

    /// <summary>
    /// 메인화면 버튼 Visible 옵션 설정 메서드
    /// </summary>
    /// <param name="main">메인화면</param>
    /// <param name="Retr">조회 버튼 활성화</param>
    /// <param name="Add">추가 버튼 활성화</param>
    /// <param name="Save">저장 버튼 활성화</param>
    /// <param name="Delete">삭제 버튼 활성화</param>
    /// <param name="Xls">엑셀 버튼 활성화</param>
    /// <param name="Print">출력 버튼 활성화</param>
    /// <param name="Close">닫기 버튼 활성화</param>
    public static void gp_SetMainButtonVisible(MN002F00 main, bool Retr, bool Add, bool Save, bool Delete, bool Xls, bool Print, bool Close)
    {
        main.Lci_Retr.Visibility = Retr ? DevExpress.XtraLayout.Utils.LayoutVisibility.Always : DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        main.Lci_Add.Visibility = Add ? DevExpress.XtraLayout.Utils.LayoutVisibility.Always : DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        main.Lci_Save.Visibility = Save ? DevExpress.XtraLayout.Utils.LayoutVisibility.Always : DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        main.Lci_Delete.Visibility = Delete ? DevExpress.XtraLayout.Utils.LayoutVisibility.Always : DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        main.Lci_Xls.Visibility = Xls ? DevExpress.XtraLayout.Utils.LayoutVisibility.Always : DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        main.Lci_Print.Visibility = Print ? DevExpress.XtraLayout.Utils.LayoutVisibility.Always : DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        main.Lci_Close.Visibility = Close ? DevExpress.XtraLayout.Utils.LayoutVisibility.Always : DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
    }

    // AES256 암호화 
    public static string Encrypt(string plainText, string key)
    {
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.KeySize = 256;  // 256비트 키 사용
            aesAlg.Key = Encoding.UTF8.GetBytes(key);  // 키를 바이트 배열로 변환
            aesAlg.IV = Encoding.UTF8.GetBytes(key.Substring(0, 16)); // IV (Initialization Vector)는 16바이트 크기, 기본값 0으로 설정
            aesAlg.Mode = CipherMode.CBC;
            aesAlg.Padding = System.Security.Cryptography.PaddingMode.PKCS7;

            // 암호화된 데이터를 저장할 메모리 스트림
            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, aesAlg.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(plainText);
                    }
                }

                // 암호화된 데이터를 바이트 배열로 반환
                return Convert.ToBase64String(msEncrypt.ToArray());
            }
        }
    }

    // AES256 복호화
    public static string Decrypt(string cipherText, string key)
    {
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.KeySize = 256;  // 256비트 키 사용
            aesAlg.Key = Encoding.UTF8.GetBytes(key);  // 키를 바이트 배열로 변환
            aesAlg.IV = new byte[16];  // IV (Initialization Vector)는 16바이트 크기, 기본값 0으로 설정

            // 암호화된 데이터를 복호화할 메모리 스트림
            using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText)))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, aesAlg.CreateDecryptor(), CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {
                        return srDecrypt.ReadToEnd();
                    }
                }
            }
        }
    }

    /// <summary>
    /// 커스텀 세로 스크롤바 사용 시, 그리드 뷰와 동기화하는 메서드
    /// </summary>
    /// <param name="xr">수행폼 (this) 고정</param>
    /// <param name="view">대상 그리드뷰</param>
    /// <param name="vsbar">대상 세로 스크롤바 컨트롤</param>
    private void SetCustomScrollBar(XtraForm xr, GridView view, DevExpress.XtraEditors.VScrollBar vsbar)
    {
        // BeginInvoke 미사용 시, 첫 설정에 GetVisibleRowCount() 함수가 동기화되지 않음
        xr.BeginInvoke(new Action(() =>
        {
            // 전체 Row 개수 - 화면에 표시 가능한 Row 수 만큼 스크롤 범위 설정
            int totalRows = view.RowCount + 1;   // +1 이 없으면, 맨 마지막 행이 잘림
            int visibleRows = GetVisibleRowCount(view);
            if (totalRows == 1) { vsbar.Visible = false; } // 0개 Row면 숨김처리
            else
            {
                if (totalRows > visibleRows)
                {
                    vsbar.Visible = true;
                    vsbar.Minimum = 0;
                    vsbar.Maximum = totalRows - visibleRows;
                    vsbar.SmallChange = 1;
                    vsbar.LargeChange = 1;
                }
                else { vsbar.Visible = false; }
            }
        }));
    }

    // 그리드 뷰에 보이는 행의 수 계산 메서드
    private int GetVisibleRowCount(GridView view)
    {
        if (view.RowCount == 0)
            return 0;

        // 그리드뷰 영역 높이 가져오기
        int viewHeight = view.ViewRect.Height;

        // 한 행의 높이 가져오기
        int rowHeight = view.RowHeight;

        // 전체 높이 / 행 높이 = 보이는 행 개수
        return rowHeight > 0 ? viewHeight / rowHeight : 0;
    }
}

class GridFunc
{
    public static DataTable gp_DeleteAllGridViewRows(GridControl grid)
    {
        DataTable dt = (DataTable)grid.DataSource;
        dt.Rows.Clear();

        return dt;
    }

    #region [GridView 기본세팅 한번에 하기]

    public static void gp_GridStyleBasicSetting(GridView view)
    {
        view.CustomDrawRowIndicator += (sender, e) =>
        {
            if (view.IsGroupRow(e.RowHandle))
            {
                e.Info.ImageIndex = -1;
            }
            else
            {
                if (e.RowHandle < 0)
                    e.Info.DisplayText = view.RowCount.ToString();
                else
                    e.Info.DisplayText = (e.RowHandle + 1).ToString();

                e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                int indiWidth = TextRenderer.MeasureText(view.RowCount.ToString(), null).Width;
            }
        };

        view.RowStyle += (sender, e) =>
        {
            if (e.RowHandle % 2 == 0)
            {
                e.Appearance.BackColor = Color.FromArgb(239, 240, 242);
            }
        };

        view.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
        view.OptionsNavigation.EnterMoveNextColumn = true;
        view.OptionsView.ColumnAutoWidth = false;
        view.OptionsView.ShowGroupPanel = false;
        view.IndicatorWidth = 50;
        view.ColumnPanelRowHeight = 30;
    }

    public static void gp_GridStyleBasicSetting(DevExpress.XtraGrid.Views.BandedGrid.BandedGridView view)
    {
        view.CustomDrawRowIndicator += (sender, e) =>
        {
            if (view.IsGroupRow(e.RowHandle))
            {
                e.Info.ImageIndex = -1;
            }
            else
            {
                if (e.RowHandle < 0)
                    e.Info.DisplayText = view.RowCount.ToString();
                else
                    e.Info.DisplayText = (e.RowHandle + 1).ToString();

                e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                int indiWidth = TextRenderer.MeasureText(view.RowCount.ToString(), null).Width;
            }
        };

        view.RowStyle += (sender, e) =>
        {
            if (e.RowHandle % 2 == 0)
            {
                e.Appearance.BackColor = Color.FromArgb(239, 240, 242);
            }
        };

        view.CustomDrawFooter += (sender, e) =>
        {
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Near;
            stringFormat.LineAlignment = StringAlignment.Center;
            var rect = e.Bounds;
            rect.X += 10;
            e.DefaultDraw();
            Font f = new Font(e.Appearance.GetFont(), FontStyle.Bold);
            e.Cache.DrawString("합계 : ", f, e.Appearance.GetForeBrush(e.Cache), rect, stringFormat);
            e.Handled = true;
        };

        for (int i = 0; i < view.Bands.Count; i++)
        {
            view.Bands[i].AppearanceHeader.Options.UseTextOptions = true;
            view.Bands[i].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
        }

        view.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
        view.OptionsNavigation.EnterMoveNextColumn = true;
        view.OptionsView.ColumnAutoWidth = false;
        view.OptionsView.ShowGroupPanel = false;
        view.IndicatorWidth = 50;
        view.ColumnPanelRowHeight = 30;
    }

    /// <summary>
    /// Group 형 GridView에 적용할 Style
    /// </summary>
    /// <param name="view"></param>
    public static void gp_GridStyleBasicSettingForGroup(GridView view)
    {
        Dictionary<int, Color> groupRowColorsCollection = null;
        Random random = null;
        int rowCnt = 0;
        view.CustomDrawRowIndicator += (sender, e) =>
        {
            if (view.IsGroupRow(e.RowHandle))
            {
                e.Info.ImageIndex = -1;
            }
            else
            {
                if (e.RowHandle < 0)
                    e.Info.DisplayText = view.RowCount.ToString();
                else
                {
                    e.Info.DisplayText = (e.RowHandle + 1).ToString();
                    rowCnt = (e.RowHandle + 1);
                }
                e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                int indiWidth = TextRenderer.MeasureText(view.RowCount.ToString(), null).Width;
            }

            // Indicator 헤더 색상 설정 및 실제 Row 수 계산
            if (e.Info.Kind == DevExpress.Utils.Drawing.IndicatorKind.Header)
            {
                e.Painter.DrawObject(e.Info);
                e.Handled = true;
                SolidBrush brush = new SolidBrush(Color.FromArgb(246, 246, 246));
                Rectangle rect = e.Bounds;
                rect.Inflate(-3, -3);
                e.Graphics.FillRectangle(brush, rect);
                Size size = ImageCollection.GetImageListSize(e.Info.ImageCollection);
                Rectangle r = e.Bounds;
                ImageCollection.DrawImageListImage(e.Cache, e.Info.ImageCollection, e.Info.ImageIndex,
                        new Rectangle(r.X + (r.Width - size.Width) / 2, r.Y + (r.Height - size.Height) / 2, size.Width, size.Height));
                brush.Dispose();

                //e.Appearance.DrawBackground(e.Cache, e.Bounds);
                e.Appearance.BackColor = Color.FromArgb(239, 240, 242);
                e.Appearance.DrawString(e.Cache, rowCnt.ToString(), e.Bounds);
                e.Handled = true;
            }
        };

        view.RowStyle += (sender, e) =>
        {
            if (e.RowHandle < 0) return;
            if (groupRowColorsCollection == null) return;

            int groupRowHandle = view.GetParentRowHandle(e.RowHandle);
            if (groupRowColorsCollection.Keys.Contains(groupRowHandle))
            {
                if (e.RowHandle % 2 == 0)
                {
                    e.Appearance.BackColor = Color.FromArgb(239, 240, 242);
                }
            }
        };

        view.CustomDrawGroupRow += (sender, e) =>
        {
            if (groupRowColorsCollection == null)
            {
                groupRowColorsCollection = new Dictionary<int, Color>();
                random = new Random();
            }

            if (!groupRowColorsCollection.Keys.Contains(e.RowHandle))
            {
                Color color = Color.FromArgb(random.Next(255), random.Next(255), random.Next(255));
                groupRowColorsCollection.Add(e.RowHandle, color);
            }
            // 주석 제거 시, 그룹 row 색상 무작위
            //e.Appearance.BackColor = groupRowColorsCollection[e.RowHandle];
        };

        view.GroupLevelStyle += (sender, e) =>
        {
            int i = view.GroupCount;

            if (i == 1)
            {
                if (e.Level == 0)
                {
                    e.LevelAppearance.BackColor = Color.LightCyan;
                    e.LevelAppearance.ForeColor = Color.Black;
                }
            }
            else if (i == 2)
            {
                if (e.Level == 0)
                {
                    e.LevelAppearance.BackColor = Color.LightSkyBlue;
                    e.LevelAppearance.ForeColor = Color.Black;
                }
                else if (e.Level == 1)
                {
                    e.LevelAppearance.BackColor = Color.LightCyan;
                    e.LevelAppearance.ForeColor = Color.Black;
                }
            }
            else if (i == 3)
            {
                if (e.Level == 0)
                {
                    e.LevelAppearance.BackColor = Color.FromArgb(95, 180, 255);
                    e.LevelAppearance.ForeColor = Color.Black;
                }
                else if (e.Level == 1)
                {
                    e.LevelAppearance.BackColor = Color.LightSkyBlue;
                    e.LevelAppearance.ForeColor = Color.Black;
                }
                else if (e.Level == 2)
                {
                    e.LevelAppearance.BackColor = Color.LightCyan;
                    e.LevelAppearance.ForeColor = Color.Black;
                }
            }
        };

        view.CustomDrawFooter += (sender, e) =>
        {
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Near;
            stringFormat.LineAlignment = StringAlignment.Center;
            var rect = e.Bounds;
            rect.X += 10;
            e.DefaultDraw();
            Font f = new Font(e.Appearance.GetFont(), FontStyle.Bold);
            e.Cache.DrawString("합계 : ", f, e.Appearance.GetForeBrush(e.Cache), rect, stringFormat);
            e.Handled = true;
        };

        view.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
        view.Appearance.SelectedRow.BackColor = Color.LightYellow;
        view.Appearance.SelectedRow.ForeColor = Color.Black;
        //view.Appearance.FocusedRow.BackColor = Color.LightPink;
        //view.Appearance.FocusedRow.ForeColor = Color.Black;
        view.OptionsNavigation.EnterMoveNextColumn = true;
        view.OptionsView.ColumnAutoWidth = false;
        view.OptionsView.ShowGroupPanel = false;
        view.IndicatorWidth = 50;
        view.ColumnPanelRowHeight = 30;
    }

    public static void gp_GridStyleForSelect(GridView view)
    {
        view.CustomDrawRowIndicator += (sender, e) =>
        {
            if (view.IsGroupRow(e.RowHandle))
            {
                e.Info.ImageIndex = -1;
            }
            else
            {
                if (e.RowHandle < 0)
                    e.Info.DisplayText = view.RowCount.ToString();
                else
                    e.Info.DisplayText = (e.RowHandle + 1).ToString();

                int indiWidth = TextRenderer.MeasureText(view.RowCount.ToString(), null).Width;
            }
        };

        view.RowStyle += (sender, e) =>
        {
            if (e.RowHandle % 2 == 0)
            {
                e.Appearance.BackColor = Color.FromArgb(239, 240, 242);
            }
        };

        view.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
        view.OptionsView.ColumnAutoWidth = false;
        view.OptionsView.ShowGroupPanel = false;
        view.IndicatorWidth = 50;
        view.ColumnPanelRowHeight = 30;
        view.OptionsBehavior.Editable = false;
    }
    #endregion

    #region [GridView 새 줄 추가, 삭제]
    /// <summary>
    /// GridView에 새 줄을 추가하는 메서드.
    /// </summary>
    /// <param name="gridView1">대상 GridView</param>
    /// <param name="focusColumn">개행 후 포커스가 가야하는 Column</param>
    public static void gp_GridAddLine(GridView gridView1, GridColumn focusColumn)
    {
        try
        {
            //if (gridView1.FocusedRowHandle == gridView1.RowCount - 1
            //    || gridView1.RowCount == 0)
            //{
            gridView1.AddNewRow();
            gridView1.UpdateCurrentRow();
            gridView1.FocusedColumn = focusColumn;
            //}
            //else { SendKeys.Send("{TAB}"); }
        }
        catch (Exception ex)
        {
            XtraMessageBox.Show(ex.Message);
        }
    }

    /// <summary>
    /// GridView에 새 줄을 추가하는 메서드.
    /// 마지막 줄이 아니면 TAB키를 누른 효과를 준다.
    /// </summary>
    /// <param name="gridView1">대상 GridView</param>
    /// <param name="seqColumn">순번 Column</param>
    /// <param name="focusColumn">개행 후 포커스가 가야하는 Column</param>
    public static void gp_GridAddLine(GridView gridView1, GridColumn seqColumn, GridColumn focusColumn)
    {
        try
        {
            //if (gridView1.FocusedRowHandle == gridView1.RowCount - 1
            //    || gridView1.RowCount == 0)
            //{
                gridView1.FocusedColumn = focusColumn;
                gridView1.AddNewRow();
                gridView1.UpdateCurrentRow();
                for (int i = 0; i < gridView1.RowCount; i++) { gridView1.SetRowCellValue(i, seqColumn, i + 1); }
                gridView1.SetFocusedRowCellValue(seqColumn, gridView1.FocusedRowHandle + 1);
                gridView1.FocusedColumn = focusColumn;

            //}
            //else { SendKeys.Send("{TAB}"); }
        }
        catch (Exception ex)
        {
            XtraMessageBox.Show(ex.Message);
        }
    }

    /// <summary>
    /// GridView에 새 줄을 추가하는 메서드.
    /// 마지막 줄이 아니면 TAB키를 누른 효과를 준다.
    /// 개행 후 주어져야 하는 기본 값을 Dictionary로 받음.
    /// </summary>
    /// <param name="gridView1">대상 GridView</param>
    /// <param name="seqColumn">순번 Column</param>
    /// <param name="focusColumn">개행 후 포커스가 가야하는 Column</param>
    /// <param name="addTextDic">개행 후 기본으로 주어저야 하는 값과 그 컬럼 Dictionary </param>
    public static void gp_GridAddLine(GridView gridView1, GridColumn seqColumn, GridColumn focusColumn, Dictionary<GridColumn, string> addTextDic)
    {
        try
        {
            if (gridView1.FocusedRowHandle == gridView1.RowCount - 1
                || gridView1.RowCount == 0)
            {
                gridView1.AddNewRow();
                gridView1.UpdateCurrentRow();
                for (int i = 0; i < addTextDic.Count; i++)
                {
                    gridView1.SetFocusedRowCellValue(addTextDic.Keys.ElementAt(i), addTextDic.Values.ElementAt(i));
                }
                for (int i = 0; i < gridView1.RowCount; i++) { gridView1.SetRowCellValue(i, seqColumn, i + 1); }
                gridView1.FocusedColumn = focusColumn;
            }
            else { SendKeys.Send("{TAB}"); }
        }
        catch (Exception ex)
        {
            XtraMessageBox.Show(ex.Message);
        }
    }


    /// <summary>
    /// 포커싱된 GridView의 줄을 삭제하는 메서드.
    /// GridControl의 DataSource를 활용한다. (개선가능하다면 개선 필요) 
    /// </summary>
    /// <param name="_gc">대상 GridControl</param>
    /// <param name="_gv">대상 GridView</param>
    /// <param name="_col">Focus되어야 하는 Column</param>
    public static void gp_GridDeleteLine(GridControl _gc, GridView _gv, GridColumn _col)
    {
        try
        {
            int iFocusedRow = 0;
            DataTable dt = (DataTable)_gc.DataSource;
            if (dt.Rows.Count != 0)
            {
                iFocusedRow = _gv.FocusedRowHandle;
                dt.Rows.RemoveAt(iFocusedRow);
            }
            _gc.DataSource = dt;

            if (dt.Rows.Count != 0)
            {
                _gv.FocusedRowHandle = iFocusedRow;
                _gv.Focus();
                _gv.FocusedColumn = _col;
            }
        }
        catch (Exception ex)
        {
            XtraMessageBox.Show(ex.Message);
        }
    }

    /// <summary>
    /// 포커싱된 GridView의 줄을 삭제하는 메서드.
    /// GridControl의 DataSource를 활용한다. (개선가능하다면 개선 필요) 
    /// </summary>
    /// <param name="gridView1">대상 GridView</param>
    /// <param name="gridControl1">대상 GridControl</param>
    /// <param name="focusColumn">Focus되어야 하는 Column</param>
    /// <param name="seqFieldName">순번이 입력되는 FieldName</param>
    public static void gp_GridDeleteLine(GridControl gridControl1, GridView gridView1, GridColumn focusColumn, string seqFieldName)
    {
        try
        {
            DataTable dt = gridControl1.DataSource as DataTable;
            if (dt == null) { XtraMessageBox.Show("삭제할 행이 없습니다."); return; }
            if (dt.Rows.Count == 0) { XtraMessageBox.Show("삭제할 행이 없습니다."); return; }

            if (dt.Rows.Count == 1)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    Type t = dt.Columns[i].DataType;
                    if (t.Equals(typeof(System.Decimal))) { dt.Rows[0][i] = 0; }
                    else { dt.Rows[0][i] = string.Empty; }
                    dt.Rows[0][seqFieldName] = 1;
                }
                return;
            }
            dt.Rows.RemoveAt(gridView1.FocusedRowHandle);
            for (int i = 0; i < dt.Rows.Count; i++) { dt.Rows[i][seqFieldName] = i + 1; }
            gridControl1.DataSource = dt;
            gridView1.UpdateCurrentRow();
            gridView1.FocusedColumn = focusColumn;
        }
        catch (Exception ex)
        {
            XtraMessageBox.Show(ex.Message);
        }
    }

    /// <summary>
    /// 포커싱된 GridView의 줄을 삭제하는 메서드.
    /// GridControl의 DataSource를 활용한다. (개선가능하다면 개선 필요) 
    /// </summary>
    /// <param name="gridView1">대상 GridView</param>
    /// <param name="gridControl1">대상 GridControl</param>
    /// <param name="focusColumn">Focus되어야 하는 Column</param>
    /// <param name="seqFieldName">순번이 입력되는 FieldName</param>
    public static void gp_GridDelLine(GridControl gridControl1, GridView gridView1, GridColumn focusColumn, string seqFieldName)
    {
        try
        {
            DataTable dt = gridControl1.DataSource as DataTable;
            if (dt == null) { XtraMessageBox.Show("삭제할 행이 없습니다."); return; }
            if (dt.Rows.Count == 0 || dt.Rows.Count == 1) { return; }
            dt.Rows.RemoveAt(gridView1.FocusedRowHandle);
            for (int i = 0; i < dt.Rows.Count; i++) { dt.Rows[i][seqFieldName] = i + 1; }
            gridControl1.DataSource = dt;
            gridView1.UpdateCurrentRow();
            gridView1.FocusedColumn = focusColumn;
        }
        catch (Exception ex)
        {
            XtraMessageBox.Show(ex.Message);
        }
    }

    #endregion

    #region [GridView 저장 전 빈 값 체크]
    /// <summary>
    /// GridView의 특정 컬럼에 빈 값이 있는지 체크하는 메서드
    /// 빈 값이 있다면, 해당 값의 행번-컬럼Caption명에 대한 입력요청 메시지를 리턴한다.
    /// 빈 값이 없다면, string.Empty를 리턴.
    /// </summary>
    /// <param name="gridView">체크하고자 하는 GridView</param>
    /// <param name="gridColumns">체크하고자 하는 컬럼 배열</param>
    /// <returns></returns>
    public static string gp_GridViewEmptyValueCheck(GridView gridView, GridColumn[] gridColumns)
    {
        // strMessage가 Empty라면, 빈 값이 없습니다.
        string strMessage = string.Empty;
        Dictionary<int, GridColumn> dicValue = gp_ValidateRows(gridView, gridColumns);
        if (dicValue.Count > 0)
        {
            int key = 0;
            GridColumn value = null;
            foreach (var item in dicValue)
            {
                key = item.Key;
                value = item.Value;
            }
            strMessage = ((key + 1) + " 번째 줄 " + value.Caption.ToString() + "의 값을 입력해 주세요.");
        }
        return strMessage;
    }
    /// <summary>
    /// GridViewEmptyValueCheck를 위한 메서드 
    /// </summary>
    /// <param name="view">체크하고자 하는 GridView</param>
    /// <param name="gridColumns">체크하고자 하는 컬럼 배열</param>
    /// <returns></returns>
    public static Dictionary<int, GridColumn> gp_ValidateRows(GridView view, GridColumn[] gridColumns)
    {
        Dictionary<int, GridColumn> dicValue = new Dictionary<int, GridColumn>();

        if (view.RowCount > 0)
        {
            string[] strArr = new string[gridColumns.Length];
            for (int i = 0; i < view.RowCount; i++)
            {
                for (int j = 0; j < gridColumns.Length; j++)
                {
                    strArr[j] = view.GetRowCellValue(i, gridColumns[j])?.ToString();
                    if (string.IsNullOrEmpty(strArr[j]))
                    {
                        dicValue.Add(i, gridColumns[j]);
                        break;
                    }
                }
            }
        }
        return dicValue;
    }

    /// <summary>
    /// Grid의 0값 체크를 위한 메서드
    /// </summary>
    /// <param name="gridView">체크할 gridview</param>
    /// <param name="gridColumns">체크할 gridColumns</param>
    /// <returns></returns>
    public static string gp_GridViewZeroValueCheck(GridView gridView, GridColumn[] gridColumns)
    {
        // strMessage가 Empty라면, 빈 값이 없습니다.
        string strMessage = string.Empty;
        Dictionary<int, GridColumn> dicValue = gp_ValidateRows_Zero(gridView, gridColumns);
        if (dicValue.Count > 0)
        {
            int key = 0;
            GridColumn value = null;
            foreach (var item in dicValue)
            {
                key = item.Key;
                value = item.Value;
            }
            strMessage = ((key + 1) + " 번째 줄 " + value.Caption.ToString() + "의 값을 입력해 주세요.");
        }
        return strMessage;
    }
    public static Dictionary<int, GridColumn> gp_ValidateRows_Zero(GridView view, GridColumn[] gridColumns)
    {
        Dictionary<int, GridColumn> dicValue = new Dictionary<int, GridColumn>();
        double dCheck = 0;
        if (view.RowCount > 0)
        {
            string[] strArr = new string[gridColumns.Length];
            for (int i = 0; i < view.RowCount; i++)
            {
                for (int j = 0; j < gridColumns.Length; j++)
                {
                    strArr[j] = view.GetRowCellValue(i, gridColumns[j])?.ToString();
                    dCheck = double.TryParse(strArr[j], out dCheck) ? Convert.ToDouble(strArr[j]) : 0;
                    if (dCheck == 0)
                    {
                        dicValue.Add(i, gridColumns[j]);
                        break;
                    }
                }
            }
        }
        return dicValue;
    }
    #endregion

    #region [GridView 한글 입력 모드 지정(특정 컬럼)]
    /// <summary>
    /// 선택한 컬럼에 진입할 때 한글입력 모드로 전환하는 메서드.
    /// GridView.ShownEditor() 이벤트에 연결 시켜야 한다.
    /// </summary>
    /// <param name="view">대상이 될 GridView 객체</param>
    /// <param name="gridColumns">한글 지정할 컬림의 배열</param>
    public static void gp_GridViewInputHangul(GridView view, GridColumn[] gridColumns)
    {
        for (int i = 0; i < gridColumns.Length; i++)
        {
            if (view.FocusedColumn == gridColumns[i])
            {
                view.ActiveEditor.ImeMode = ImeMode.Hangul;
            }
        }
    }

    /// <summary>
    /// 선택한 컬럼에 진입할 때 한글입력 모드로 전환하는 메서드.
    /// 파라미터로 주어진 GridView에 ShownEditor 이벤트를 더한다.
    /// </summary>
    /// <param name="view">대상이 될 GridView 객체</param>
    /// <param name="gridColumns">한글 지정할 컬림의 배열</param>
    public static void gp_GridViewInputHangul_ShownEditor(GridView view, GridColumn[] gridColumns)
    {
        view.ShownEditor += (sender, e) =>
        {
            for (int i = 0; i < gridColumns.Length; i++)
            {
                if (view.FocusedColumn == gridColumns[i])
                {
                    view.ActiveEditor.ImeMode = ImeMode.Hangul;
                }
            }
        };
    }



    /// <summary>
    /// 선택한 컬럼에 진입할 때 입력모드를 지정하는 메서드.
    /// 파라미터로 주어진 GridView에 ShownEditor 이벤트를 더한다.
    /// </summary>
    /// <param name="view">대상이 될 GridView 객체</param>
    /// <param name="gridColumns">컬럼 - ImeMode Dicitonary 객체</param>
    public static void gp_GridViewInputSet_ShownEditor(GridView view, Dictionary<GridColumn, ImeMode> columnsIme)
    {
        view.ShownEditor += (sender, e) =>
        {
            foreach (var item in columnsIme)
            {
                if (view.FocusedColumn == item.Key)
                {
                    view.ActiveEditor.ImeMode = item.Value;
                }
            }
        };
    }

    #endregion

    #region [Grid에서 FTP 업로드 - 다운로드]
    /// <summary>
    /// Grid에서 FTP 업로드할 파일을 엽니다. 
    /// 1. 변수를 (Key : gridView 이름 - Value : 행번호)구성의 지정된 ComnFunc의 Dictionary 객체로 저장합니다.
    /// 2. 지정한 GridColumn에 파일목록을 표시합니다.
    /// </summary>
    /// <param name="view">대상이 되는 GridView</param>
    /// <param name="column_FileName">파일 목록 정보가 담길 GridColumn</param>
    public static void gp_OpenFile_Grid(GridView view, GridColumn column_FileName)
    {
        using (XtraOpenFileDialog openFileDialog = new XtraOpenFileDialog())
        {
            openFileDialog.Title = "저장할 파일을 선택해주세요.";
            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;
            openFileDialog.Multiselect = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                //Get the path of specified file
                string[] filePath = null;
                filePath = new string[openFileDialog.FileNames.Length];
                filePath = openFileDialog.FileNames; // 경로를 포함한 파일이름

                string sFileNames = string.Empty;
                for (int i = 0; i < filePath.Length; i++)
                {
                    sFileNames += Path.GetFileName(filePath[i]); // 경로를 배제한 파일이름들
                    if (i == filePath.Length - 1)
                        continue;
                    sFileNames += ",";
                }
                string sKey = view.Name + view.FocusedRowHandle.ToString();
                Dictionary<string, string[]> pathDic = new Dictionary<string, string[]>();
                pathDic.Remove(sKey);
                pathDic.Add(sKey, filePath);
                view.SetFocusedRowCellValue(column_FileName, sFileNames);
            }
        }
    }

    /// <summary>
    /// Grid에서 지정한 파일을 FTP로 부터 다운 받습니다.
    /// </summary>
    /// <param name="view">대상이 되는 GridView</param>
    /// <param name="column_FileName">파일목록이 있는 GridColumn (쉼표로 구분) </param>
    /// <param name="dir">다운받을 FTP 경로</param>
    public static void gp_DownFile_Grid(GridView view, GridColumn column_FileName, string dir)
    {
        try
        {
            string files = view.GetFocusedRowCellValue(column_FileName)?.ToString();
            if (string.IsNullOrEmpty(files))
            {
                XtraMessageBox.Show("대상이 될 파일이 없습니다.");
                return;
            }
            string[] fileArr = files.Split(',');
            ComnFunc.gp_FTP_Download_Files(dir, fileArr);
        }
        catch (Exception ex)
        {
            XtraMessageBox.Show(ex.Message);
            return;
        }
    }

    #endregion

    #region [ 그리드에 Footer 생성 및 설정]
    /// <summary>
    /// 그리드에 Footer 생성 및 기본적인 설정 (Footer 좌측 합계 : 문자열 출력)
    /// </summary>
    /// <param name="gridView">GridView</param>
    /// <param name="gridColumns">GridColumn 배열 (디폴트 null값)</param>
    public static void gp_GridViewFooterSetting(GridView gridView, GridColumn[] gridColumns = null)
    {
        gridView.OptionsView.ShowFooter = true;

        gridView.CustomDrawFooter += (sender, e) =>
        {
            StringFormat @string = new StringFormat();
            @string.Alignment = StringAlignment.Near;
            @string.LineAlignment = StringAlignment.Center;

            var rect = e.Bounds;
            rect.X += 10;

            e.DefaultDraw();
            e.Cache.DrawString("합계 : ", e.Appearance.GetFont(), e.Appearance.GetForeBrush(e.Cache)
                , rect, @string);
            e.Handled = true;
        };

        foreach (GridColumn Column in gridColumns)
        {
            if (Column.Name.Contains("QTY"))
            {
                Column.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                Column.SummaryItem.DisplayFormat = "{0:n0}";
            }
            else if (Column.Name.Contains("WGT") || Column.Name.Contains("LOSSW"))
            {
                Column.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                Column.SummaryItem.DisplayFormat = "{0:n2}";
            }
            else if (Column.Name.Contains("AMT") || Column.Name.Contains("VAT") || Column.Name.Contains("COST") || Column.Name.Contains("TOTAL"))
            {
                Column.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                Column.SummaryItem.DisplayFormat = "{0:c0}";
            }
        }
    }
    #endregion
    public static void gp_MoveGridViewColumn(GridView view, GridControl gCotrl, int pCol)
    {
        // GridControl의 GridView의 pCol에 커서 이동
        view.FocusedColumn = view.VisibleColumns[pCol];
        gCotrl.Focus();
    }

    #region [GridView Row 위, 아래 이동]
    // 22.12.12 추가
    /// <summary>
    /// GridView의 Row 위치를 위로 이동시키는 메서드
    /// </summary>
    /// <param name="_gc">대상 GridControl</param>
    /// <param name="_gv">대상 GridView</param>
    public static void gp_GridRowUpLocate(GridControl _gc, GridView _gv)
    {
        try
        {
            if (_gv.RowCount == 0)
                return;

            DataTable dt = _gc.DataSource as DataTable;
            int idx = _gv.FocusedRowHandle;

            if (idx == 0) return; // Top일 경우 종료

            DataRow curRow = dt.Rows[idx];
            DataRow newRow = dt.NewRow();

            newRow.ItemArray = curRow.ItemArray; // 새 row에 현재 row 내용 복사.

            dt.Rows.RemoveAt(idx);
            dt.Rows.InsertAt(newRow, idx - 1);

            if (idx == dt.Rows.Count - 1)
                _gv.FocusedRowHandle = _gv.FocusedRowHandle - 1;
            else
                _gv.FocusedRowHandle = _gv.FocusedRowHandle - 2;
            _gv.SelectRow(_gv.FocusedRowHandle);
        }
        catch (Exception ex)
        {
            ComnFunc.gp_PrintMessage(ex.Message, "", ComnFunc.MessageType.오류);
        }
    }

    // 22.12.12 추가
    /// <summary>
    /// GridView의 Row 위치를 아래 이동시키는 메서드
    /// </summary>
    /// <param name="_gc">대상 GridControl</param>
    /// <param name="_gv">대상 GridView</param>
    public static void gp_GridRowDownLocate(GridControl _gc, GridView _gv)
    {
        try
        {
            if (_gv.RowCount == 0)
            {
                return;
            }
            DataTable dt = _gc.DataSource as DataTable;

            int idx = _gv.FocusedRowHandle;
            if (idx == dt.Rows.Count - 1) return; // Bottom일 경우 종료

            DataRow curRow = dt.Rows[idx];
            DataRow newRow = dt.NewRow();

            newRow.ItemArray = curRow.ItemArray; // 내용 복사. 

            dt.Rows.RemoveAt(idx);
            dt.Rows.InsertAt(newRow, idx + 1);

            _gv.FocusedRowHandle++;
            _gv.SelectRow(_gv.FocusedRowHandle);
        }
        catch (Exception ex)
        {
            ComnFunc.gp_PrintMessage(ex.Message, "", ComnFunc.MessageType.오류);
        }
    }
    #endregion
}