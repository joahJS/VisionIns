using DevExpress.DataAccess.Excel;
//using DevExpress.SpreadsheetSource;
using DevExpress.Utils.CodedUISupport;
using DevExpress.Utils.Menu;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraSplashScreen;
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
using VisionIns;
using DevExpress.Spreadsheet;
using DevExpress.XtraPrinting;

class ComnEtcFunc
{
    public static StringBuilder StrSQL = new StringBuilder();
    public static DataRow row;
    public static SimpleButton sb;
    public static string formName;

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

    // 권한 가져오기 (프로시저 사용 권한)
    public static DataTable GetAuthInfo(string sUsrCd, string sProcedureName)
    {
        string[] sField = new string[2];
        string[] sValue = new string[sField.Length];

        sField[0] = "CMD";
        sValue[0] = "AUTH";

        sField[1] = "USRCD";
        sValue[1] = sUsrCd;

        return DBConn.GetDataTableByProcedure(DBConn.dbCon, sProcedureName, sField, sValue);
    }

    // 권한 가져오기 (페이지 사용 권한)
    public static bool GetAuthInfo(string sCmd, string sUsrId, string sPgmId)
    {
        Dictionary<string, string> dicParams = new Dictionary<string, string>();

        dicParams.Add("CMD", sCmd);
        dicParams.Add("USRID", sUsrId);
        dicParams.Add("PGMID", sPgmId);

        DataTable dt = GetInfo(dicParams, "GET_AUTH_RST");
        if (dt.Rows.Count > 0)
        {
            string sRst = dt.Rows[0]["RST"]?.ToString();
            string sMsg = dt.Rows[0]["MSG"]?.ToString();
            if (sRst.Equals("Y"))
                return false;
            else
            {
                XtraMessageBox.Show(sMsg);
                return true;
            }
        }
        else
        {
            string sWord = string.Empty;
            if (sCmd.Equals("USE"))
                sWord = "사용";
            else if (sCmd.Equals("ADD"))
                sWord = "추가";
            else if (sCmd.Equals("UPD"))
                sWord = "수정";
            else if (sCmd.Equals("DEL"))
                sWord = "삭제";
            else if (sCmd.Equals("PRT"))
                sWord = "출력";
            else if (sCmd.Equals("XLS"))
                sWord = "엑셀";
            XtraMessageBox.Show(string.Format("해당 사용자는 {0} 권한이 없습니다.", sWord));
            return true;
        }
    }

    // 개별 권한 수정 가능 체크      
    public static bool CheckEditableAuth(string sUsrCd, string sPgmId, string sProcedureName, string sEdit_Kind)
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
                XtraMessageBox.Show("시스템 ERROR : ComnEtcFunc.CheckEditableAuth 참조, sColumnsName 변수 관련 에러");
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

    #region [LookUpEdit 설정 관련]

    

    public static void SetLookUpEdit(LookUpEdit lkup, DataTable dt, string sValue, string sDisplay, string sSetIdx)
    {
        lkup.Properties.DataSource = dt;
        lkup.Properties.ValueMember = sValue;
        lkup.Properties.DisplayMember = sDisplay;

        if (sSetIdx.Equals("Y")) lkup.ItemIndex = 0;
    }

    public static void SetGridLookUpEdit(RepositoryItemGridLookUpEdit repositoryLookUp, DataTable dataTable, GridControl gridControl, GridColumn gridColumn, string sValue, string sDisplay, string sNullText)
    {
        GridView view = new GridView();

        repositoryLookUp.DataSource = dataTable;
        repositoryLookUp.ValueMember = sValue;
        repositoryLookUp.DisplayMember = sDisplay;

        gridControl.RepositoryItems.Add(repositoryLookUp);
        gridColumn.ColumnEdit = repositoryLookUp;
        repositoryLookUp.NullText = sNullText;

        repositoryLookUp.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
        repositoryLookUp.PopupFilterMode = PopupFilterMode.Contains;
        repositoryLookUp.PopupView.OptionsFilter.AllowFilterIncrementalSearch = true;
        repositoryLookUp.ImmediatePopup = true;
        repositoryLookUp.View.OptionsView.ShowColumnHeaders = false;

    }

    // 전체 LoopUpEdit Setting Method (공통)
    // 사용  예 : ComnTestFunc.SetBoundLookUp(LkupHOUSE, "HOUSF", "HOUSE", "HOUNM"); 
    //            ComnTestFunc.SetBoundLookUp(LkupWLINE, "REFFPF", "WLINE", "");
    // 파라미터 : 바인딩할 lookupedit명, 값이 있는 테이블 명, 코드값 컬럼명, 보여줄 컬럼명
    //            공통코드 테이블(REFFPF)의 경우 마지막 파라미터는 고려되지 않음. "" 추천.
    public static void SetBoundLookUp(LookUpEdit lkup, string tableName, string columnName1, string columnName2)
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
                    strSql.AppendLine("      , A.REFNM AS NM                                 ");
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
                    strSql.AppendLine("      , A.REFNM AS NM                                 ");
                    strSql.AppendLine("      , A.PRTSQ AS SEQ                                ");
                    strSql.AppendLine("   FROM REFFPF A                                  ");
                    strSql.AppendLine("  WHERE A.RCDTP = '" + columnName1 + "'               ");
                    strSql.AppendLine("    AND A.REFNO <> '?'                                ");
                }
            }
            else if (columnName1.Equals("HOUSE"))
            {
                strSql.AppendLine(" SELECT A.REFNO AS CD                                 ");
                strSql.AppendLine("      , A.REFNM AS NM                                 ");
                strSql.AppendLine("      , A.PRTSQ AS SEQ                                ");
                strSql.AppendLine("   FROM REFFPF A                                  ");
                strSql.AppendLine("  WHERE A.RCDTP = '" + columnName1 + "'               ");
                strSql.AppendLine("    AND A.REFNO <> '?'                                ");
                strSql.AppendLine("    AND A.USEYN <> 'N'                                ");
            }
            else
            {
                strSql.AppendLine(" SELECT A.REFNO AS CD                                 ");
                strSql.AppendLine("      , A.REFNM AS NM                                 ");
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

        lkup.Properties.DataSource = DBConn.GetDataTable(DBConn.dbCon, strSql.ToString());
        lkup.Properties.ValueMember = "CD";
        lkup.Properties.DisplayMember = "NM";
        lkup.Properties.ShowHeader = false;
        lkup.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
        lkup.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;

    }
    
    // 외부 DB 참조용
    public static void SetBoundLookUp(LookUpEdit lkup, string tableName, string columnName1, string columnName2, SqlConnection sc)
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


    public static DataRow gp_CallNameCvmast(string pCD, string pWHERE)
    {
        // 거래처 1건 조회, 그외는 Form Load (SHS: 2020.12.09)
        string sSQL =
            "\r\n" + " SELECT * FROM CVMAST " +
            "\r\n" + " WHERE  CVGU <>'0' AND (CVCOD='" + pCD + "' OR CVNAM LIKE '%" + pCD + "%') " + pWHERE;

        DataTable dt = DBConn.GetDataTable(DBConn.dbCon, sSQL.ToString());
        if (dt.Rows.Count == 1) { return dt.Rows[0]; }
        else { return null; }
    }

    public static void SetDateFromToValue(DateEdit ymdFrom, DateEdit ymdTo)
    {
        DateTime today = DateTime.Now.Date;
        ymdFrom.EditValue = today.AddDays(1 - today.Day);
        ymdTo.EditValue = today;
    }
    public static DataRow gp_CallNamezUsrlst(string pCD, string pWHERE)
    {
        // 유저 1건 조회, 그외는 Form Load (SHS: 2020.12.09)
        string sSQL =
            "\r\n" + " SELECT * FROM zUSRLST " +
            "\r\n" + " WHERE  (USRID='" + pCD + "' OR USRNM LIKE '%" + pCD + "%') " + pWHERE;

        DataTable dt = DBConn.GetDataTable(DBConn.dbCon, sSQL.ToString());
        if (dt.Rows.Count == 1) { return dt.Rows[0]; }
        else { return null; }
    }
    public static DataRow gp_CallNameItemas(string pCD, string pWHERE)
    {
        // 품목 1건 조회, 그외는 Form Load (SHS: 2020.12.09)
        string sSQL =
            "\r\n" + " SELECT * FROM ITEMAS " +
            "\r\n" + " WHERE  (ITCOD='" + pCD + "' OR ITNAM LIKE '%" + pCD + "%') " + pWHERE;

        DataTable dt = DBConn.GetDataTable(DBConn.dbCon, sSQL.ToString());
        if (dt.Rows.Count == 1) { return dt.Rows[0]; }
        else { return null; }
    }

    // 제품 LookUpEdit Setting Method (임시 - 항목 많은 LookUp의 예)
    public static void SetJepumLookUp(LookUpEdit lkup)
    {
        StringBuilder sb = new StringBuilder();
        sb.Clear();
        sb.AppendLine(" WITH ITEM_INFO AS                       ");
        sb.AppendLine("     (                                   ");
        sb.AppendLine("       SELECT '' AS 제품코드             ");
        sb.AppendLine("            , '' AS 차종                 ");
        sb.AppendLine("            , '' AS 규격                 ");
        sb.AppendLine("            , '' AS 품명                 ");
        sb.AppendLine("            , '' AS 재질                 ");
        sb.AppendLine("      UNION ALL                          ");
        sb.AppendLine("       SELECT A.ITCOD AS 제품코드        ");
        sb.AppendLine("            , A.ITYPE AS 차종            ");
        sb.AppendLine("            , A.SPEC  AS 규격            ");
        sb.AppendLine("            , A.ITNAM AS 품명            ");
        sb.AppendLine("            , A.ITPMS AS 재질            ");
        sb.AppendLine("         FROM MES_JEPUMPF A              ");
        sb.AppendLine("        WHERE A.JGUBN = '1'              ");
        sb.AppendLine("      )                                  ");
        sb.AppendLine(" SELECT 제품코드, 차종, 규격, 품명, 재질 ");
        sb.AppendLine("   FROM ITEM_INFO                        ");
        sb.AppendLine("  ORDER BY 제품코드, 차종                ");

        lkup.Properties.DataSource = DBConn.GetDataTable(DBConn.dbCon, sb.ToString());
        lkup.Properties.ValueMember = "제품코드";
        lkup.Properties.DisplayMember = "규격";
        lkup.Properties.ShowHeader = true;
        lkup.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
        lkup.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
        lkup.Properties.PopupFilterMode = PopupFilterMode.Contains;
    }

    // 전체 GridLoopUpEdit Setting Method (공통)
    // 사용  예 : ComnTestFunc.SetBoundGridLookUp(RepoGLkupWLINE, "REFFPF", "WLINE", "");
    //            ComnTestFunc.SetBoundGridLookUp(RepoGLkupMACOD, "EQUIPF", "MACOD", "MANAM");
    // 파라미터 : 바인딩할 lookupedit명, 값이 있는 테이블 명, 코드값 컬럼명, 보여줄 컬럼명
    //            공통코드 테이블(REFFPF)의 경우 마지막 파라미터는 고려되지 않음. "" 추천.
    public static void SetBoundGridLookUp(RepositoryItemGridLookUpEdit repositoryLookUp, string tableName, string columnName1, string columnName2)
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
                    strSql.AppendLine("      , A.REFNM AS NM                                 ");
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
                    strSql.AppendLine("      , A.REFNM AS NM                                 ");
                    strSql.AppendLine("      , A.PRTSQ AS SEQ                                ");
                    strSql.AppendLine("   FROM REFFPF A                                  ");
                    strSql.AppendLine("  WHERE A.RCDTP = '" + columnName1 + "'               ");
                    strSql.AppendLine("    AND A.REFNO <> '?'                                ");
                }
            }
            else if (columnName1.Equals("HOUSE"))
            {
                strSql.AppendLine(" SELECT A.REFNO AS CD                                 ");
                strSql.AppendLine("      , A.REFNM AS NM                                 ");
                strSql.AppendLine("      , A.PRTSQ AS SEQ                                ");
                strSql.AppendLine("   FROM REFFPF A                                  ");
                strSql.AppendLine("  WHERE A.RCDTP = '" + columnName1 + "'               ");
                strSql.AppendLine("    AND A.REFNO <> '?'                                ");
                strSql.AppendLine("    AND A.USEYN <> 'N'                                ");
            }
            else
            {
                strSql.AppendLine(" SELECT A.REFNO AS CD                                 ");
                strSql.AppendLine("      , A.REFNM AS NM                                 ");
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


        repositoryLookUp.DataSource = DBConn.GetDataTable(DBConn.dbCon, strSql.ToString());
        repositoryLookUp.ValueMember = "CD";
        repositoryLookUp.DisplayMember = "NM";


        repositoryLookUp.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
        repositoryLookUp.PopupFilterMode = PopupFilterMode.Contains;
        repositoryLookUp.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
        repositoryLookUp.PopupView.OptionsFilter.AllowFilterIncrementalSearch = true;
        repositoryLookUp.ImmediatePopup = true;
        repositoryLookUp.View.OptionsView.ShowColumnHeaders = false;

    }

    // 외부 DB 참조용
    public static void SetBoundGridLookUp(RepositoryItemGridLookUpEdit repositoryLookUp, string tableName, string columnName1, string columnName2, SqlConnection sc)
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

    #region [엑셀 관련]
    public static void ExportExcelFile(string sFileName, GridControl grid)
    {
        string FileName = string.Empty;
        FileDialog fileDlg = new SaveFileDialog();

        try
        {
            string sFileNM = sFileName + DateTime.Now.ToLongDateString().Replace(" ", "");
            string sFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            fileDlg.InitialDirectory = sFolderPath;
            fileDlg.FileName = sFileNM;
            fileDlg.Filter = "Excel files (*.xlsx or *.xls)|*.xlsx;*.xls";

            if (fileDlg.ShowDialog() == DialogResult.OK)
            {
                FileName = fileDlg.FileName;

                grid.ExportToXlsx(FileName);
                Process.Start(FileName);
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
    #endregion

    #region [Grid 관련]

    // 행 선택 메서드
    public static void SelectRowByParam2(GridControl gv, string[] arrColName, string[] arrColVal)
    {
        if (arrColName[0] == null || arrColName[1] == null)
            return;
        if (arrColVal[0] == null || arrColVal[1] == null)
            return;

        ColumnView view = gv.MainView as ColumnView;
        GridColumn col1 = view.Columns[arrColName[0]];
        GridColumn col2 = view.Columns[arrColName[1]];
        if (col1 == null || col2 == null) return;

        view.OptionsSelection.MultiSelect = true;
        view.ClearSelection();
        int rowHandle = -1;

        while (rowHandle != GridControl.InvalidRowHandle)
        {
            rowHandle = view.LocateByDisplayText(rowHandle + 1, col1, arrColVal[0]);
            break;
        }

        for (int i = rowHandle; i < view.RowCount; i++)
        {
            string sVal = view.GetRowCellValue(i, arrColName[1])?.ToString();
            if (sVal.Equals(arrColVal[1]))
            {
                view.SelectRow(i);
                view.FocusedRowHandle = i;
                break;
            }
        }
    }

    #endregion

    #region [값 존재 체크]
    public static bool ChkStringValueExist(string TableName, string ColumnName, string CheckValue)
    {
        StringBuilder sb = new StringBuilder();
        sb.Clear();
        sb.AppendLine(" SELECT " + ColumnName);
        sb.AppendLine("   FROM " + TableName);
        sb.AppendLine("  WHERE " + ColumnName + " = '" + CheckValue + "' ");
        DataTable dt = DBConn.GetDataTable(DBConn.dbCon, sb.ToString());
        if (dt.Rows.Count > 0)
            return true;
        else
            return false;
    }

    public static bool ChkDoubleValueExist(string TableName, string ColumnName, double CheckValue)
    {
        StringBuilder sb = new StringBuilder();
        sb.Clear();
        sb.AppendLine(" SELECT " + ColumnName);
        sb.AppendLine("   FROM " + TableName);
        sb.AppendLine("  WHERE " + ColumnName + " = " + CheckValue);
        DataTable dt = DBConn.GetDataTable(DBConn.dbCon, sb.ToString());
        if (dt.Rows.Count > 0)
            return true;
        else
            return false;
    }

    public static DataTable BringTableSturuct(string TableName)
    {
        StringBuilder sb = new StringBuilder();
        sb.Clear();
        sb.AppendLine(" SELECT * ");
        sb.AppendLine("   FROM " + TableName);
        sb.AppendLine("  WHERE 1=2 ");
        return DBConn.GetDataTable(DBConn.dbCon, sb.ToString());
    }

    #endregion

    #region[채번 관련]

    public enum TARGET_TABLE
    {
        MES_JEPUMPF, MES_PROD_M, MES_PROD_E, MES_MAKE_M, MES_CLAIM
    }

    public static string NextSLINO(string sKey)
    {
        string sDateForSlipNo = DateTime.Now.ToString("yyMMdd");
        string sTableName = string.Empty;
        string sColumnName = string.Empty;
        int iNum = 0;

        if (sKey.Equals("ITEM")) { sColumnName = "ITCOD"; sTableName = "ITEMAS"; iNum = 4; } // 원관
        else if (sKey.Equals("M")) { sColumnName = "ITCOD"; sTableName = "PROD_M"; iNum = 4; }

        StringBuilder sb1 = new StringBuilder();
        sb1.Clear();
        sb1.AppendLine(" SELECT CONVERT(INT, ISNULL(MAX(RIGHT(" + sColumnName + ", " + iNum + ")),0 ))  ");
        sb1.AppendLine("   FROM " + sTableName);
        sb1.AppendLine("  WHERE CAST(CDATE AS DATE) = CAST(GETDATE() AS DATE)          "); // TDATE 로 적용. 변수를 하나 더 받아야.

        SqlCommand sc = new SqlCommand(sb1.ToString(), DBConn.dbCon);
        SqlDataReader sdr = null;
        int iCode = 0;

        try
        {
            sdr = sc.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    iCode = sdr.GetInt32(0);
                }
            }
        }
        catch (Exception ex)
        {
            XtraMessageBox.Show(ex.Message);
        }
        finally
        {
            if (sdr != null)
            {
                sdr.Close();
            }
        }

        iCode++;

        string newCodeNum = string.Empty;

        if (iNum == 3)
            newCodeNum = String.Format("{0:000}", iCode);
        else if (iNum == 4)
            newCodeNum = String.Format("{0:0000}", iCode);

        return sKey + sDateForSlipNo + newCodeNum;

    }

    // TDATE 기준 채번 (테이블에 있다고 가정)
    public static string NextSLINO(string Key, string TDate)
    {
        string sDateForSlipNo = Convert.ToDateTime(TDate).ToString("yyMMdd");
        string sTableName = string.Empty;
        string sColumnName = string.Empty;
        int Number = 0;

        if (Key.Equals("D")) { sColumnName = "SLINO"; sTableName = "MAKE_M"; Number = 4; } // 지시
        else if (Key.Equals("M")) { sColumnName = "SLINO"; sTableName = "PROD_M"; Number = 4; } // 생산            
        else if (Key.Equals("Q")) { sColumnName = "SLINO"; sTableName = "EQUIP_INSP_M"; Number = 3; } // 점검
        else if (Key.Equals("I")) { sColumnName = "SLINO"; sTableName = "IPGO_M"; Number = 3; } // 입고
        else if (Key.Equals("S")) { sColumnName = "SLINO"; sTableName = "SALE_M"; Number = 3; } // 매출
        else if (Key.Equals("H")) { sColumnName = "SLINO"; sTableName = "MOVE_M"; Number = 3; } // 창고

        StringBuilder sb1 = new StringBuilder();
        sb1.Clear();
        sb1.AppendLine(" SELECT CONVERT(INT, ISNULL(MAX(RIGHT(" + sColumnName + ", " + Number + ")),0 ))  ");
        sb1.AppendLine("   FROM " + sTableName);
        sb1.AppendLine("  WHERE CAST(TDATE AS DATE) = CAST( '" + TDate + "' AS DATE)                      ");

        SqlCommand sc = new SqlCommand(sb1.ToString(), DBConn.dbCon);
        SqlDataReader sdr = null;
        int iCode = 0;

        try
        {
            sdr = sc.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    iCode = sdr.GetInt32(0);
                }
            }
        }
        catch (Exception ex)
        {
            XtraMessageBox.Show(ex.Message);
        }
        finally
        {
            if (sdr != null)
            {
                sdr.Close();
            }
        }

        iCode++;

        string newCodeNum = string.Empty;

        if (Number == 3)
            newCodeNum = String.Format("{0:000}", iCode);
        else if (Number == 4)
            newCodeNum = String.Format("{0:0000}", iCode);

        return Key + sDateForSlipNo + newCodeNum;
    }
        
    // TDATE 기준 (Adapter 사용 - try문 안에서)
    public static string NextSlinoAdapter(string _DATE, TARGET_TABLE _TABLE)
    {
        DataTable dt = new DataTable();
        StringBuilder sb = new StringBuilder();
        string _DateForSlipNo = Convert.ToDateTime(_DATE).ToString("yyMMdd");
        string _CHAR = string.Empty;
        string _TableName = string.Empty;
        string _ColumnName = string.Empty;
        int _Number = 0;
        int iCode = 0;

        if (_TABLE == TARGET_TABLE.MES_JEPUMPF) // 제품테이블. (원관기준)
        {
            _ColumnName = "ITCOD"; _TableName = "MES_JEPUMPF";
            sb.Clear();
            sb.AppendLine(" SELECT CONVERT(INT, ISNULL(MAX(" + _ColumnName + ")),0 )) ");
            sb.AppendLine("   FROM " + _TableName);
            sb.AppendLine("  WHERE JGUBN = '2'  ");
        }
        else if (_TABLE == TARGET_TABLE.MES_PROD_M) // 생산 
        {
            _ColumnName = "PRODNO"; _TableName = "MES_PROD_M"; _CHAR = "P"; _Number = 3;
            sb.Clear();
            sb.AppendLine(" SELECT CONVERT(INT, ISNULL(MAX(RIGHT(" + _ColumnName + ", " + _Number + ")),0 )) ");
            sb.AppendLine("   FROM " + _TableName);
            sb.AppendLine("  WHERE LEFT(" + _ColumnName + ",7) = '" + _CHAR + _DateForSlipNo + "'             ");
        }
        else if (_TABLE == TARGET_TABLE.MES_PROD_E) // 불량 
        {
            _ColumnName = "SLINO"; _TableName = "MES_PROD_E"; _CHAR = "E"; _Number = 3;
            sb.Clear();
            sb.AppendLine(" SELECT CONVERT(INT, ISNULL(MAX(RIGHT(" + _ColumnName + ", " + _Number + ")),0 )) ");
            sb.AppendLine("   FROM " + _TableName);
            sb.AppendLine("  WHERE LEFT(" + _ColumnName + ",7) = '" + _CHAR + _DateForSlipNo + "'             ");
        }
        else if (_TABLE == TARGET_TABLE.MES_MAKE_M) // 지시 
        {
            _ColumnName = "SLIPNO"; _TableName = "MES_MAKE_M"; _CHAR = "M"; _Number = 3;
            sb.Clear();
            sb.AppendLine(" SELECT CONVERT(INT, ISNULL(MAX(RIGHT(" + _ColumnName + ", " + _Number + ")),0 )) ");
            sb.AppendLine("   FROM " + _TableName);
            sb.AppendLine("  WHERE LEFT(" + _ColumnName + ",7) = '" + _CHAR + _DateForSlipNo + "'             ");
        }
        else if (_TABLE == TARGET_TABLE.MES_CLAIM) // 품질 - 고객 불만 
        {
            _ColumnName = "SLINO"; _TableName = "MES_CLAIM"; _CHAR = "C"; _Number = 3;
            sb.Clear();
            sb.AppendLine(" SELECT CONVERT(INT, ISNULL(MAX(RIGHT(" + _ColumnName + ", " + _Number + ")),0 )) ");
            sb.AppendLine("   FROM " + _TableName);
            sb.AppendLine("  WHERE LEFT(" + _ColumnName + ",7) = '" + _CHAR + _DateForSlipNo + "'             ");
        }

        try
        {
            dt = DBConn.GetDataTable(DBConn.dbCon, sb.ToString());
            iCode = string.IsNullOrEmpty(dt.Rows[0][0]?.ToString()) ? 0 : Convert.ToInt32(dt.Rows[0][0]?.ToString());
        }
        catch (Exception ex)
        {
            throw ex;
        }

        iCode++;
        string SLINO = string.Empty;
        if (_TABLE == TARGET_TABLE.MES_JEPUMPF)
        {
            SLINO = String.Format("{0:00000}", iCode);
        }
        else if (_TABLE == TARGET_TABLE.MES_PROD_M || _TABLE == TARGET_TABLE.MES_PROD_E || _TABLE == TARGET_TABLE.MES_MAKE_M || _TABLE == TARGET_TABLE.MES_CLAIM)
        {
            SLINO = _CHAR + _DateForSlipNo + String.Format("{0:000}", iCode);
        }
        return SLINO;
    }

    #endregion

    #region [이미지 관련]
    public static byte[] ImageToByteArray(System.Drawing.Image imageIn)
    {
        using (var ms = new MemoryStream())
        {
            imageIn.Save(ms, imageIn.RawFormat);
            return ms.ToArray();
        }
    }
    #endregion

    #region [재고_생산]        
    public static void gf_UptItemblProd(string pPRODNO, int pPlus)
    {
        try
        {
            StrSQL.Clear();
            StrSQL.AppendLine(" SELECT A1.PDATE, A2.*                         ");
            StrSQL.AppendLine("   FROM MES_PROD_M A1                          ");
            StrSQL.AppendLine("   LEFT JOIN MES_PROD_D A2                     ");
            StrSQL.AppendLine("          ON A1.PRODNO = A2.PRODNO             ");
            StrSQL.AppendLine("  WHERE A1.PRODNO = '" + pPRODNO + "'          ");

            DataTable dt = DBConn.GetDataTable(DBConn.dbCon, StrSQL.ToString());

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    string PDATE = row["PDATE"]?.ToString();
                    string MITEM = row["MITEMNO"]?.ToString();
                    string WLINE = row["WLINE"]?.ToString();
                    string CVCOD = row["OCVCOD"]?.ToString();
                    double TRQTY = string.IsNullOrEmpty(row["MQTY"]?.ToString()) ? 0 : Convert.ToDouble(row["MQTY"]?.ToString());

                    gf_ItemblUpdate(PDATE.Substring(0, 4),
                                    PDATE.Substring(5, 2),
                                    MITEM, WLINE, CVCOD,
                                    TRQTY * pPlus, 0, 0
                        );
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static bool gf_ItemblUpdate(string pYear, string pMonth, string pITCOD, string pWLINE, string pCVCOD
                                , double pQty1, double pQty2, double pQty3)
    {
        string sSQL = string.Empty;

        if (string.IsNullOrEmpty(pYear) ||
            string.IsNullOrEmpty(pMonth) ||
            string.IsNullOrEmpty(pITCOD) ||
            string.IsNullOrEmpty(pWLINE) ||
            string.IsNullOrEmpty(pCVCOD))
            return false;

        try
        {
            if (!gf_ItemblAdd(pYear, pITCOD, pWLINE, pCVCOD))
                return false;

            if (pQty1 != 0)
                sSQL = "I1QT" + pMonth + " = I1QT" + pMonth + " + " + pQty1 + "- ( " + pQty3 + ") ";
            else
                sSQL = "O1QT" + pMonth + " = O1QT" + pMonth + " + " + pQty2 + "- ( " + pQty3 + ") ";

            sSQL += " , MDATE = CONVERT(VARCHAR(20), GETDATE(), 120) ";

            StrSQL.Clear();
            StrSQL.AppendLine(" UPDATE MES_YYITEMBL SET         ");
            StrSQL.AppendLine(sSQL);
            StrSQL.AppendLine("  WHERE JYEAR = '" + pYear + "'  ");
            StrSQL.AppendLine("    AND ITCOD = '" + pITCOD + "' ");
            StrSQL.AppendLine("    AND OPSEQ = '" + pWLINE + "' ");
            StrSQL.AppendLine("    AND CVCOD = '" + pCVCOD + "' ");

            SqlCommand cmd = DBConn.dbCon.CreateCommand();
            cmd.Transaction = DBConn.dbTran;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = StrSQL.ToString();
            cmd.ExecuteNonQuery();
            return true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static bool gf_ItemblAdd(string pYear, string pITCOD, string pWLINE, string pCVCOD)
    {
        string sUSRID = LoginUser.USRID;

        if (pITCOD.Equals(""))
            return false;

        StrSQL.Clear();
        StrSQL.AppendLine(" SELECT * FROM MES_YYITEMBL       ");
        StrSQL.AppendLine("  WHERE JYEAR = '" + pYear + "'   ");
        StrSQL.AppendLine("    AND ITCOD = '" + pITCOD + "'  ");
        StrSQL.AppendLine("    AND OPSEQ = '" + pWLINE + "'  ");
        StrSQL.AppendLine("    AND CVCOD = '" + pCVCOD + "'  ");

        DataTable dt = DBConn.GetDataTable(DBConn.dbCon, StrSQL.ToString());

        if (dt != null)
        {
            if (dt.Rows.Count == 0)
            {
                StrSQL.Clear();
                StrSQL.AppendLine(" INSERT INTO MES_YYITEMBL                      ");
                StrSQL.AppendLine("        ( USRID, CDATE                         ");
                StrSQL.AppendLine("        , JYEAR, ITCOD, OPSEQ, CVCOD )         ");
                StrSQL.AppendLine(" VALUES (  '" + sUSRID + "'                    ");
                StrSQL.AppendLine("        , CONVERT(VARCHAR(20), GETDATE(), 120) ");
                StrSQL.AppendLine("        , '" + pYear + "'                      ");
                StrSQL.AppendLine("        , '" + pITCOD + "'                     ");
                StrSQL.AppendLine("        , '" + pWLINE + "'                     ");
                StrSQL.AppendLine("        , '" + pCVCOD + "'           )         ");

                try
                {
                    SqlCommand cmd = DBConn.dbCon.CreateCommand();
                    cmd.Transaction = DBConn.dbTran;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = StrSQL.ToString();
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        return true;
    }
    #endregion

    #region [재고_수주->출하지시]        
    public static void gf_UptItemblSale(string pSUNO, int pPlus)
    {
        try
        {
            StrSQL.Clear();
            StrSQL.AppendLine(" SELECT A1.SUDATE,A1.OTCUST,A2.*    ");
            StrSQL.AppendLine("   FROM MES_SALE_M A1               ");
            StrSQL.AppendLine("   LEFT JOIN MES_SALE_D A2          ");
            StrSQL.AppendLine("           ON A1.SUNO = A2.SUNO     ");
            StrSQL.AppendLine("  WHERE A1.SUNO = '" + pSUNO + "'   ");

            DataTable dt = DBConn.GetDataTable(DBConn.dbCon, StrSQL.ToString());

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    string PDATE = row["SUDATE"]?.ToString();
                    string MITEM = row["MITEMNO"]?.ToString();
                    string WLINE = row["WLINE"]?.ToString();
                    string CVCOD = row["OTCUST"]?.ToString();
                    double TRQTY = string.IsNullOrEmpty(row["MQTY"]?.ToString()) ? 0 : Convert.ToDouble(row["MQTY"]?.ToString());

                    gf_ItemblUpdateSale(PDATE.Substring(0, 4),
                                    PDATE.Substring(5, 2),
                                    MITEM, WLINE, CVCOD,
                                    0, TRQTY * pPlus, 0
                        );
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static bool gf_ItemblUpdateSale(string pYear, string pMonth, string pITCOD, string pWLINE, string pCVCOD
                                , double pQty1, double pQty2, double pQty3)
    {
        string sSQL = string.Empty;

        if (string.IsNullOrEmpty(pYear) ||
            string.IsNullOrEmpty(pMonth) ||
            string.IsNullOrEmpty(pITCOD) ||
            string.IsNullOrEmpty(pWLINE) ||
            string.IsNullOrEmpty(pCVCOD))
            return false;

        try
        {
            if (!gf_ItemblAddSale(pYear, pITCOD, pWLINE, pCVCOD))
                return false;

            if (pQty1 != 0)
                sSQL = "I1QT" + pMonth + " = I1QT" + pMonth + " + " + pQty1 + "- ( " + pQty3 + ") ";
            else
                sSQL = "O1QT" + pMonth + " = O1QT" + pMonth + " + " + pQty2 + "- ( " + pQty3 + ") ";

            sSQL += " , MDATE = CONVERT(VARCHAR(20), GETDATE(), 120) ";

            StrSQL.Clear();
            StrSQL.AppendLine(" UPDATE MES_YYITEMBL SET         ");
            StrSQL.AppendLine(sSQL);
            StrSQL.AppendLine("  WHERE JYEAR = '" + pYear + "'  ");
            StrSQL.AppendLine("    AND ITCOD = '" + pITCOD + "' ");
            StrSQL.AppendLine("    AND OPSEQ = '" + pWLINE + "' ");
            StrSQL.AppendLine("    AND CVCOD = '" + pCVCOD + "' ");

            SqlCommand cmd = DBConn.dbCon.CreateCommand();
            cmd.Transaction = DBConn.dbTran;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = StrSQL.ToString();
            cmd.ExecuteNonQuery();
            return true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static bool gf_ItemblAddSale(string pYear, string pITCOD, string pWLINE, string pCVCOD)
    {
        string sUSRID = LoginUser.USRID;

        if (pITCOD.Equals(""))
            return false;

        StrSQL.Clear();
        StrSQL.AppendLine(" SELECT * FROM MES_YYITEMBL       ");
        StrSQL.AppendLine("  WHERE JYEAR = '" + pYear + "'   ");
        StrSQL.AppendLine("    AND ITCOD = '" + pITCOD + "'  ");
        StrSQL.AppendLine("    AND OPSEQ = '" + pWLINE + "'  ");
        StrSQL.AppendLine("    AND CVCOD = '" + pCVCOD + "'  ");

        DataTable dt = DBConn.GetDataTable(DBConn.dbCon, StrSQL.ToString());

        if (dt != null)
        {
            if (dt.Rows.Count == 0)
            {
                StrSQL.Clear();
                StrSQL.AppendLine(" INSERT INTO MES_YYITEMBL                      ");
                StrSQL.AppendLine("        ( USRID, CDATE                         ");
                StrSQL.AppendLine("        , JYEAR, ITCOD, OPSEQ, CVCOD )         ");
                StrSQL.AppendLine(" VALUES (  '" + sUSRID + "'                    ");
                StrSQL.AppendLine("        , CONVERT(VARCHAR(20), GETDATE(), 120) ");
                StrSQL.AppendLine("        , '" + pYear + "'                      ");
                StrSQL.AppendLine("        , '" + pITCOD + "'                     ");
                StrSQL.AppendLine("        , '" + pWLINE + "'                     ");
                StrSQL.AppendLine("        , '" + pCVCOD + "'           )         ");

                try
                {
                    SqlCommand cmd = DBConn.dbCon.CreateCommand();
                    cmd.Transaction = DBConn.dbTran;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = StrSQL.ToString();
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        return true;
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
    public static void SetButtonEdit(ButtonEdit buttonEdit, TextEdit textEdit, string Gubun, TextEdit SANO = null)
    {
        buttonEdit.ButtonClick += (sender, e) =>
        {
            //if (Gubun.Equals("사용자"))
            //{
            //    UserSelect frm = new UserSelect();
            //    frm.DataRowSendEvent += new UserSelect.SendDataHandler(GetUserInfo);
            //    frm.ShowDialog();
            //}
            //else if (Gubun.Equals("거래처"))
            //{
            //    CvSelect frm = new CvSelect();
            //    frm.DataRowSendEvent += new CvSelect.SendDataHandler(GetCvInfo);
            //    frm.ShowDialog();
            //}
            //else if (Gubun.Equals("품목"))
            //{
            //    ProductSelect frm = new ProductSelect();
            //    frm.DataRowSendEvent += new ProductSelect.SendDataHandler(GetItemInfo);
            //    frm.ShowDialog();
            //}
        };

        buttonEdit.KeyDown += (sender, e) =>
        {
            if (Gubun.Equals("사용자"))
            {
                ButtonEdit findWord = sender as ButtonEdit;
                string FIND_WORD = findWord.EditValue?.ToString();

                if (e.KeyCode == Keys.Enter)
                {
                    StringBuilder strSql = new StringBuilder();
                    strSql.Clear();

                    strSql.AppendLine(" SELECT * ");
                    strSql.AppendLine("   FROM ZUSRLST z ");
                    strSql.AppendLine("  WHERE z.USRNM LIKE '%" + FIND_WORD + "%' ");
                    strSql.AppendLine("     OR z.USRCD ='" + FIND_WORD + "'");

                    DataTable dt = DBConn.GetDataTable(DBConn.dbCon, strSql.ToString());

                    if (dt.Rows.Count == 1)
                    {
                        buttonEdit.EditValue = dt.Rows[0]["USRNM"]?.ToString();
                        textEdit.EditValue = dt.Rows[0]["USRCD"]?.ToString();
                    }
                    else
                    {
                        //UserSelect frm = new UserSelect();
                        //frm.DataRowSendEvent += new UserSelect.SendDataHandler(GetUserInfo);
                        //frm.FindWord = FIND_WORD;
                        //frm.ShowDialog();
                    }
                }
            }
            else if (Gubun.Equals("거래처"))
            {
                ButtonEdit findWord = sender as ButtonEdit;
                string FIND_WORD = findWord.EditValue?.ToString();

                if (e.KeyCode == Keys.Enter)
                {
                    StringBuilder strSql = new StringBuilder();
                    strSql.Clear();

                    strSql.AppendLine(" SELECT * ");
                    strSql.AppendLine("   FROM CVMAST z ");
                    strSql.AppendLine("  WHERE z.CVCOD LIKE '%" + FIND_WORD + "%' ");
                    strSql.AppendLine("     OR z.CVNAM LIKE '%" + FIND_WORD + "%' ");

                    DataTable dt = DBConn.GetDataTable(DBConn.dbCon, strSql.ToString());

                    if (dt.Rows.Count == 1)
                    {
                        buttonEdit.EditValue = dt.Rows[0]["CVNAM"]?.ToString();
                        textEdit.EditValue = dt.Rows[0]["CVCOD"]?.ToString();
                        //SANO.EditValue = dt.Rows[0]["SANO"]?.ToString();
                    }
                    else
                    {
                        //CvSelect frm = new CvSelect();
                        //frm.DataRowSendEvent += new CvSelect.SendDataHandler(GetCvInfo);
                        //frm.FindWord = FIND_WORD;
                        //frm.ShowDialog();
                    }
                }
            }
            else if (Gubun.Equals("품목"))
            {
                ButtonEdit findWord = sender as ButtonEdit;
                string FIND_WORD = findWord.EditValue?.ToString();

                if (e.KeyCode == Keys.Enter)
                {
                    StringBuilder strSql = new StringBuilder();
                    strSql.Clear();

                    strSql.AppendLine(" SELECT * ");
                    strSql.AppendLine("   FROM ITEMAS z ");
                    strSql.AppendLine("  WHERE z.ITCOD LIKE '%" + FIND_WORD + "%' ");
                    strSql.AppendLine("     OR z.ITNAM LIKE '%" + FIND_WORD + "%' ");
                    strSql.AppendLine("     OR z.CVJNO LIKE '%" + FIND_WORD + "%' ");

                    DataTable dt = DBConn.GetDataTable(DBConn.dbCon, strSql.ToString());

                    if (dt.Rows.Count == 1)
                    {
                        buttonEdit.EditValue = dt.Rows[0]["ITNAM"]?.ToString();
                        textEdit.EditValue = dt.Rows[0]["ITCOD"]?.ToString();
                        //SANO.EditValue = dt.Rows[0]["SANO"]?.ToString();
                    }
                    else
                    {
                        //ProductSelect frm = new ProductSelect();
                        //frm.DataRowSendEvent += new ProductSelect.SendDataHandler(GetItemInfo);
                        //frm.FindWord = FIND_WORD;
                        //frm.ShowDialog();
                    }
                }
            }
        };

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
    public static DataTable XlsToTable()
    {
        string _sFileName = string.Empty;
        try
        {
            //XtraOpenFileDialog fileDialog = new XtraOpenFileDialog();
            //string filePath = Application.StartupPath;
            //fileDialog.Filter = "Excel Files (.xlsx; .xls)|*.xlsx;*.xls";
            //fileDialog.FilterIndex = 1;
            //if (fileDialog.ShowDialog() == DialogResult.OK)
            //{
            //    // _sFileName : 선택한 파일 경로를 담음
            //    _sFileName = fileDialog.FileName;
            //}

            //fileDialog.Dispose();
            //if (!string.IsNullOrEmpty(_sFileName))
            //{
            //    ExcelDataSource excelDataSource = GetExcelDataSource(_sFileName);
            //    ExcelWorksheetSettings workSheetSettings = new ExcelWorksheetSettings();
            //    // 엑셀 워크시트 네임 받아오기 (x번째 시트, 엑셀 파일명<경로>)
            //    workSheetSettings.WorksheetName = GetWorkSheetNameByIndex(0, _sFileName);
            //    excelDataSource.SourceOptions = new ExcelSourceOptions(workSheetSettings)
            //    {
            //        SkipEmptyRows = true,
            //        UseFirstRowAsHeader = true
            //    };
            //    excelDataSource.Fill();
            //    _sFileName = string.Empty;
            //    return ExcelToDataTable(excelDataSource);
            //}
            //else
            //{
            //    _sFileName = string.Empty;
            return null;
            //}
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
    public static ExcelDataSource GetExcelDataSource(string fileName)
    {
        ExcelDataSource ds = new ExcelDataSource();
        ds.FileName = fileName;
        ExcelSourceOptions excelSourceOptions1 = new ExcelSourceOptions();
        ExcelWorksheetSettings excelWorksheetSettings1 = new ExcelWorksheetSettings();
        excelWorksheetSettings1.WorksheetName = GetWorkSheetNameByIndex(0, fileName);
        excelSourceOptions1.ImportSettings = excelWorksheetSettings1;
        ds.SourceOptions = excelSourceOptions1;
        ds.Fill();
        return ds;
    }

    // 엑셀 워크시트 이름 받아오기 메서드
    public static string GetWorkSheetNameByIndex(int p, string fileName)
    {
        //string worksheetName = "";
        //using (ISpreadsheetSource spreadsheetSource = SpreadsheetSourceFactory.CreateSource(fileName))
        //{
        //    IWorksheetCollection worksheetCollection = spreadsheetSource.Worksheets;
        //    worksheetName = worksheetCollection[p].Name;
        //}
        //return worksheetName;
        return null;
    }

    // 엑셀 데이터를 데이터 테이블로 변환 메서드
    public static DataTable ExcelToDataTable(ExcelDataSource excelDataSource)
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

    // 2021-06-14 Log 추가
    public static bool GetLog(string sCmd, string sUsrId, string sPgmId, string sComip)
    {
        Dictionary<string, string> dicParams = new Dictionary<string, string>();

        dicParams.Add("CMD", sCmd);
        dicParams.Add("USRCD", sUsrId);
        dicParams.Add("PGMID", sPgmId);
        dicParams.Add("COMIP", sComip);
        dicParams.Add("EXENM", string.Format(@"{0}\{1}", Application.StartupPath, "SHDELV.exe"));

        DataTable dt = DBConn.GetDataTable_1(DBConn.dbCon, "DP_LOGHIST", dicParams);
        //if (dt.Rows.Count > 0)
        //{
        //    string sRst = dt.Rows[0]["RST"]?.ToString();
        //    string sMsg = dt.Rows[0]["MSG"]?.ToString();
        //    if (sRst.Equals("Y"))
        //        return false;
        //    else
        //    {
        //        XtraMessageBox.Show(sMsg);
        //        return true;
        //    }
        //}
        //else
        //{
        return true;
        //}
    }
    // 2021-06-14 IP 수집
    public static string Client_IP
    {
        get
        {
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            string ClientIP = string.Empty;
            for (int i = 0; i < host.AddressList.Length; i++)
            {
                if (host.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
                {
                    ClientIP = host.AddressList[i].ToString();
                }
            }
            return ClientIP;
        }
    }


    // Datatable 가져오기 
    // 매개변수 : 사용될 파라미터정보(Dictionary<string, string>), 프로시저ID(string)
    public static DataTable GetInfo(Dictionary<string, string> dicParams, string sProcedureId)
    {
        return DBConn.GetDataTable(DBConn.dbCon, sProcedureId, dicParams);
    }

    public static DataTable GetInfo(Dictionary<string, object> dicParams, string sProcedureId)
    {
        return DBConn.GetDataTable(DBConn.dbCon, sProcedureId, dicParams);
    }

    // Dictionary 만들기 설정 : KEY(strArr), VALUE(strArr)  
    public static Dictionary<string, string> MakeDictionary(string[] sParamName, string[] sValue)
    {
        Dictionary<string, string> param = new Dictionary<string, string>();

        for (int i = 0; i < sParamName.Length; i++)
        {
            param.Add(sParamName[i], sValue[i]);
        }

        return param;
    }

    // 모든 입력폼 리셋 (현재는 TextEdit, LookUpEdit, PictureEdit)
    public static void ClearAllForm(Control control)
    {
        foreach (Control ctrl in control.Controls)
        {
            TextEdit te = ctrl as TextEdit;
            if (te != null)
            {
                te.ResetText();
            }

            if (ctrl is LookUpEdit)
            {
                LookUpEdit lue = (LookUpEdit)ctrl;
                if (lue != null)
                {
                    lue.EditValue = string.Empty;
                }
            }

            if (ctrl is PictureEdit)
            {
                PictureEdit pe = (PictureEdit)ctrl;
                if (pe != null)
                {
                    pe.Image = null;
                    pe.Tag = null;
                }
            }

            ClearAllForm(ctrl);
        }
    }

    // 룩업 일부(CD) 컬럼 배제 (현재는 모든 룩업의 코드 컬럼에 적용)
    public static void LkupCdClear(Control control)
    {
        foreach (Control ctrl in control.Controls)
        {
            LookUpEdit lue = ctrl as LookUpEdit;

            if (lue != null)
            {
                lue.Properties.ForceInitialize();
                lue.Properties.PopulateColumns();
                lue.Properties.Columns[0].Visible = false;
            }
            LkupCdClear(ctrl);
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

    // 컨트롤 초기화 (SHS: 2020.11.24)
    public static void gp_ResetCompo1(Control sender)
    {

        foreach (Control ctl in sender.Controls)
        {
            if (ctl is TextBox) { TextBox tb = (TextBox)ctl; if (!string.IsNullOrEmpty(tb.Text?.ToString())) { tb.Text = string.Empty; } }
            else
            if (ctl is TextEdit) { TextEdit tb = (TextEdit)ctl; if (!string.IsNullOrEmpty(tb.Text?.ToString())) { tb.Text = string.Empty; } }
            else
            if (ctl is MemoEdit) { MemoEdit me = (MemoEdit)ctl; if (!string.IsNullOrEmpty(me.Text?.ToString())) { me.Text = string.Empty; } }
            else
            if (ctl is DateEdit) { DateEdit me = (DateEdit)ctl; if (!string.IsNullOrEmpty(me.Text?.ToString())) { me.EditValue = ""; } }
            else
            if (ctl is ButtonEdit) { ButtonEdit me = (ButtonEdit)ctl; if (!string.IsNullOrEmpty(me.Tag?.ToString())) { me.Text = string.Empty; } }
            else
            if (ctl is LookUpEdit) { LookUpEdit cb = (LookUpEdit)ctl; if (!string.IsNullOrEmpty(cb.Text?.ToString())) { cb.SelectedText = string.Empty; } }
        }
    }

    // Control에 포커스 가면 색 변경 (SHS: 2020.12.02)
    public static void gp_SetColorFocused(Control sender)
    {

        foreach (Control ctl in sender.Controls)
        {
            if (ctl is TextEdit) { TextEdit tb = (TextEdit)ctl; tb.Properties.AppearanceFocused.BackColor = System.Drawing.Color.LightYellow; }
            else
            if (ctl is MemoEdit) { MemoEdit cb = (MemoEdit)ctl; cb.Properties.AppearanceFocused.BackColor = System.Drawing.Color.LightYellow; }
            else
            if (ctl is LookUpEdit) { LookUpEdit cb = (LookUpEdit)ctl; cb.Properties.AppearanceFocused.BackColor = System.Drawing.Color.LightYellow; }
        }
    }

    public static DataRow gp_CallEquipf(string pCD, string pWHERE)
    {
        // 설비코드 1건 조회, 그외는 Form Load (SHS: 2020.12.09)
        string sSQL =
            "\r\n" + " SELECT * FROM MES_EQUIPF " +
            "\r\n" + " WHERE  (EQUIP_CD LIKE '%" + pCD + "%') " + pWHERE;

        DataTable dt = DBConn.GetDataTable(DBConn.dbCon, sSQL.ToString());
        if (dt.Rows.Count == 1) { return dt.Rows[0]; }
        else { return null; }
    }

    public static DataRow RowSearch { get; set; }
    public static void SearchInfo(BaseControl baseControl, XtraForm form)
    {
        string sVal = baseControl.Text;


    }

    public static string DataTableToJsonObj(DataTable dt)
    {
        DataSet ds = new DataSet();
        ds.Merge(dt);
        StringBuilder JsonString = new StringBuilder();
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            JsonString.Append("[");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                JsonString.Append("{");
                for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                {
                    if (j < ds.Tables[0].Columns.Count - 1)
                    {
                        JsonString.Append("\"" + ds.Tables[0].Columns[j].ColumnName.ToString() + "\":" + "\"" + ds.Tables[0].Rows[i][j].ToString() + "\",");
                    }
                    else if (j == ds.Tables[0].Columns.Count - 1)
                    {
                        JsonString.Append("\"" + ds.Tables[0].Columns[j].ColumnName.ToString() + "\":" + "\"" + ds.Tables[0].Rows[i][j].ToString() + "\"");
                    }
                }
                if (i == ds.Tables[0].Rows.Count - 1)
                {
                    JsonString.Append("}");
                }
                else
                {
                    JsonString.Append("},");
                }
            }
            JsonString.Append("]");
            return JsonString.ToString();
        }
        else
        {
            return null;
        }
    }

    #endregion

    #region [ 다른 탭으로 이동하게 하는 메서드 ]
    public static void FormCheck(XtraForm frm, XtraForm child)
    {
        SplashScreenManager.ShowForm(typeof(WaitForm1));
        XtraForm parent = (XtraForm)child.MdiParent;
        //if (child.Name.Equals("MN001F00")) { parent = (XtraForm)child; }
        //else { parent = (XtraForm)child.MdiParent; }

        if (FormIsExist(frm.GetType(), parent))
        {
            Form[] temp = parent.MdiChildren;
            foreach (XtraForm ff in temp)
            {
                if (frm.Name.Equals(ff.Name))
                {
                    ff.Dispose();
                }
            }
            frm.MdiParent = parent;
            frm.Show();
            SplashScreenManager.CloseForm();
            return;
        }
        else
        {
            frm.MdiParent = parent;
            frm.Show();
            SplashScreenManager.CloseForm();
        }
    }

    public static bool FormIsExist(Type tp, XtraForm parent)
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
    #endregion

    /// <summary>
    /// 바이트 배열 -> 이미지
    /// </summary>
    /// <param name="byteArrayIn"></param>
    /// <returns></returns>
    public static Image byteArrayToImage(byte[] byteArrayIn)
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

    // 22.01.25 추가
    /// <summary>
    /// 컨트롤 규칙 자동 초기 세팅
    /// </summary>
    /// <param name="control">레이아웃 컨트롤, 대부분의 경우 layoutControl1</param>
    public static void InitControllerRule(Control control)
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

    // 22.05.10 추가
    /// <summary>
    /// 프로젝트 공통 키 이벤트 설정
    /// </summary>
    /// <param name="sbt">버튼 배열</param>
    /// <param name="e">키 이벤트 핸들러</param>
    public static void CommonButtonKeyEvent(SimpleButton[] sbt, KeyEventArgs e)
    {
        foreach (SimpleButton bt in sbt)
        {
            // =============== 대부분의 메뉴에 자주 사용되는 버튼 ===============
            // 조회(F5)
            if ((bt.Name.Equals("Bt_Retr") || bt.Name.Equals("c_RETR")) && e.KeyCode == Keys.F5) { bt.PerformClick(); return; }
            // 추가(F1)
            else if ((bt.Name.Equals("Bt_Add") || bt.Name.Equals("c_ADD")) && e.KeyCode == Keys.F1) { bt.PerformClick(); return; }
            // 초기화(F1)
            else if (bt.Name.Equals("Bt_Reset") && e.KeyCode == Keys.F1) { bt.PerformClick(); return; }
            // 연속저장(F2)
            else if (bt.Name.Equals("Bt_SaveMul") && e.KeyCode == Keys.F2) { bt.PerformClick(); return; }
            // 저장(F3)
            else if ((bt.Name.Equals("Bt_Save") || bt.Name.Equals("Bt_Transmit")) && e.KeyCode == Keys.F3) { bt.PerformClick(); return; }
            // 삭제(F4)
            else if ((bt.Name.Equals("Bt_Delete") || bt.Name.Equals("Bt_Cancel") || bt.Name.Equals("c_DELETE")) && e.KeyCode == Keys.F4) { bt.PerformClick(); return; }
            // 엑셀(F8)
            else if ((bt.Name.Equals("Bt_Xls") || bt.Name.Equals("c_EXCEL")) && e.KeyCode == Keys.F8) { bt.PerformClick(); return; }
            // 출력(F12)
            else if (bt.Name.Equals("Bt_Print") && e.KeyCode == Keys.F12) { bt.PerformClick(); return; }
            // 닫기(ESC)
            else if ((bt.Name.Equals("Bt_Close") || bt.Name.Equals("c_CLOSE")) && e.KeyCode == Keys.Escape) { bt.PerformClick(); return; }

            // =============== 특정 메뉴에 한정적으로 사용되는 버튼 ===============
            // 엑셀업로드(F1)           // IV002F00, CL001F00
            else if (bt.Name.Equals("Bt_UpXls") && e.KeyCode == Keys.F1) { bt.PerformClick(); return; }
            // 엑셀선택(F2)             // IV002F01, CL001F01, CM001F02
            else if (bt.Name.Equals("Bt_SelXls") && e.KeyCode == Keys.F2) { bt.PerformClick(); return; }
            // 연마감(F10), DeadLine    // CL001F00
            else if (bt.Name.Equals("Bt_DLine") && e.KeyCode == Keys.F10) { bt.PerformClick(); return; }
            // 조회(F5), 닫기(ESC)      // PD999F00
            else if (bt.Name.Equals("Bt_Retr2") && e.KeyCode == Keys.F5) { bt.PerformClick(); return; }
            else if (bt.Name.Equals("Bt_Close2") && e.KeyCode == Keys.Escape) { bt.PerformClick(); return; }
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
    public static void CommonRetrMethod(string _command
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

        ComnMethod.parameterList.Clear();
        ComnMethod.parameterList.Add(new Parameter("CMD", _command));
        ComnMethod.parameterList.Add(new Parameter("DATE_F", sDateF));
        if (_dateT != null)
            ComnMethod.parameterList.Add(new Parameter("DATE_T", sDateT));
        // 찾을 항목
        if (_idx != null)
        {
            sIdx = _idx.SelectedIndex.ToString();
            ComnMethod.parameterList.Add(new Parameter("FIND_IDX", sIdx));
        }
        // 찾을 단어
        if (_word != null)
        {
            sWord = _word.EditValue?.ToString();
            ComnMethod.parameterList.Add(new Parameter("FIND_WORD", sWord));
        }

        DataTable dt = DBConn.GetDataTable(DBConn.dbCon, _procedure, ComnMethod.parameterList);
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

    // 22.05.26 추가
    /// <summary>
    /// 그리드 뷰 단일 Row 삭제 메서드
    /// </summary>
    /// <param name="_gc">그리드 컨트롤 (ex. GridRetr)</param>
    /// <param name="_gv">그리드 뷰 (ex. GridViewRetr)</param>
    /// <param name="_col">삭제 후 포커스 할 컬럼</param>
    public static void GridDeleteLine(GridControl _gc, GridView _gv, GridColumn _col)
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

    // 22.06.09 추가 (현재는 LabelControl만)
    /// <summary>
    /// 컨트롤 크기에 맞춰 폰트크기 자동 조절 메서드
    /// (사용 예시, label.Font = AutoFontSize(label, label.Text)
    /// </summary>
    /// <param name="label">컨트롤 (현재는 LabelControl만)</param>
    /// <param name="text"> 컨트롤 텍스트 (ex. label.Text)</param>
    /// <returns>크기 조절한 Font 반환</returns>
    public static Font AutoFontSize(LabelControl label, string text)
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
    public static void CreatePopupSubMenu(GridView _view, PopupMenuShowingEventArgs e, string _caption, string _frm)
    {
        formName = _frm;
        if (e.MenuType == GridMenuType.Row)
        {
            int rowHandle = e.HitInfo.RowHandle;
            e.Menu.Items.Clear();
            e.Menu.Items.Add(CreateSubMenuInfo(_view, rowHandle, _caption));
        }
    }
    // 그리드 뷰 Row 정보 클래스
    class RowInfo
    {
        public GridView View;
        public int RowHandle;
        public RowInfo(GridView view, int rowHandle)
        {
            this.RowHandle = rowHandle;
            this.View = view;
        }
    }
    // 서브 팝업메뉴 생성 메서드
    private static DXMenuItem CreateSubMenuInfo(GridView _view, int _rowHandle, string _caption)
    {
        DXMenuItem subMenu = new DXMenuItem(_caption);
        subMenu.Tag = new RowInfo(_view, _rowHandle);
        subMenu.Enabled = _view.IsDataRow(_rowHandle);
        subMenu.Click += new EventHandler(SubMenuClick);
        return subMenu;
    }
    // 팝업 서브메뉴 클릭 시 폼 로드 이벤트
    private static void SubMenuClick(object sender, EventArgs e)
    {
        DXMenuItem menuItem = sender as DXMenuItem;
        RowInfo ri = menuItem.Tag as RowInfo;
        if (ri != null)
        {
            if (formName.Equals("BY003F01"))
            {
                //BY003F00 frm = new BY003F00();
                //frm._Suju_Transfer_Check = true;
                //frm._FocusedSujuDataRow = GridViewM.GetFocusedDataRow();
                //frm.DataRowSendEvent += new SA001F01.SendDataHandler(Open_Form);
                //frm.Show();
            }
        }
    }
    private void Open_Form(XtraForm targetForm)
    {
       // if (targetForm.Equals("BY003F00")) { BY003F00 frm = new BY003F00(); FormCheck(frm, mainForm); }
    }

    // 엑셀 파일 저장 (시스템 로그 포함)
    public static void ExportExcelFile(string sFileName, GridControl grid, string name, string text)
    {
        string FileName = string.Empty;
        FileDialog fileDlg = new SaveFileDialog();

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

                grid.ExportToXlsx(FileName);
                Process.Start(FileName);
            }
            fileDlg.Dispose();
            //ComnMethod.SetLogInfo(name, text, ComnMethod.CONNECT_TYPE.엑셀);
        }
        catch (Exception ex)
        {
            gp_PrintMessage(ex.Message, "엑셀저장 실패", MessageType.오류);
        }
    }

    // 엑셀 파일 저장 (2개 시트병합, 시스템 로그 포함)
    public static void ExportExcelFile(string sFileName, GridControl grid1, GridControl grid2, string sheetname1, string sheetname2, string name, string text)
    {
        //string FileName = string.Empty;
        //FileDialog fileDlg = new SaveFileDialog();
        //string TempName = string.Empty;

        //try
        //{
        //    string sFileNM = sFileName + "_" + DateTime.Now.ToLongDateString().Replace(" ", "");
        //    string sFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        //    fileDlg.InitialDirectory = sFolderPath;
        //    fileDlg.FileName = sFileNM;
        //    fileDlg.Filter = "Excel files (*.xlsx or *.xls)|*.xlsx;*.xls";

        //    if (fileDlg.ShowDialog() == DialogResult.OK)
        //    {
        //        FileName = fileDlg.FileName;
        //        string[] FileNameNondat = FileName.Split('.');
        //        TempName = FileNameNondat[0];

        //        XlsxExportOptions xlsxOptions1 = new XlsxExportOptions();
        //        xlsxOptions1.ShowGridLines = true;
        //        xlsxOptions1.TextExportMode = TextExportMode.Value;
        //        xlsxOptions1.ExportHyperlinks = true;
        //        xlsxOptions1.SheetName = sheetname1;                        // 시트명 변경
        //        xlsxOptions1.ExportMode = XlsxExportMode.DifferentFiles;

        //        XlsxExportOptions xlsxOptions2 = new XlsxExportOptions();
        //        xlsxOptions2.ShowGridLines = true;
        //        xlsxOptions2.TextExportMode = TextExportMode.Value;
        //        xlsxOptions2.ExportHyperlinks = true;
        //        xlsxOptions2.SheetName = sheetname2;                        // 시트명 변경
        //        xlsxOptions2.ExportMode = XlsxExportMode.DifferentFiles;

        //        // 병합할 엑셀 임시파일 생성
        //        grid1.ExportToXlsx(TempName + "_1.xlsx", xlsxOptions1);
        //        grid2.ExportToXlsx(TempName + "_2.xlsx", xlsxOptions2);

        //        // 워크북 생성 (엑셀 시트)
        //        Workbook book1 = new Workbook();
        //        book1.LoadDocument(TempName + "_1.xlsx", DocumentFormat.Xlsx); // 1번 그리드 정보 로드
        //        Workbook book2 = new Workbook();
        //        book2.LoadDocument(TempName + "_2.xlsx", DocumentFormat.Xlsx); // 2번 그리드 정보 로드

        //        // 시트 병합
        //        book1.Append(book2);            // 1번 엑셀문서에 2번 시트 붙여넣기
        //        book1.SaveDocument(FileName);   // 병합된 1번 엑셀을 최초경로에 저장

        //        // 엑셀 임시파일 삭제
        //        File.Delete(TempName + "_1.xlsx");
        //        File.Delete(TempName + "_2.xlsx");
        //        Process.Start(FileName);
        //    }
        //    fileDlg.Dispose();
        //    ComnMethod.SetLogInfo(name, text, ComnMethod.CONNECT_TYPE.엑셀);
        //}
        //catch (Exception ex)
        //{
        //    // 엑셀 임시파일 삭제
        //    File.Delete(TempName + "_1.xlsx");
        //    File.Delete(TempName + "_2.xlsx");
        //    gp_PrintMessage(ex.Message, "엑셀저장 실패", MessageType.오류);
        //}
    }

    // 엑셀 파일 저장 (3개 시트병합, 시스템 로그 포함)
    public static void ExportExcelFile(string sFileName, GridControl grid1, GridControl grid2, GridControl grid3, string sheetname1, string sheetname2, string sheetname3, string name, string text)
    {
        string FileName = string.Empty;
        FileDialog fileDlg = new SaveFileDialog();
        string TempName = string.Empty;

        try
        {
            //string sFileNM = sFileName + "_" + DateTime.Now.ToLongDateString().Replace(" ", "");
            //string sFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            //fileDlg.InitialDirectory = sFolderPath;
            //fileDlg.FileName = sFileNM;
            //fileDlg.Filter = "Excel files (*.xlsx or *.xls)|*.xlsx;*.xls";

            //if (fileDlg.ShowDialog() == DialogResult.OK)
            //{
            //    FileName = fileDlg.FileName;
            //    string[] FileNameNondat = FileName.Split('.');
            //    TempName = FileNameNondat[0];

            //    XlsxExportOptions xlsxOptions1 = new XlsxExportOptions();
            //    xlsxOptions1.ShowGridLines = true;
            //    xlsxOptions1.TextExportMode = TextExportMode.Text;
            //    xlsxOptions1.ExportHyperlinks = true;
            //    xlsxOptions1.SheetName = sheetname1;                        // 시트명 변경
            //    xlsxOptions1.ExportMode = XlsxExportMode.DifferentFiles;

            //    XlsxExportOptions xlsxOptions2 = new XlsxExportOptions();
            //    xlsxOptions2.ShowGridLines = true;
            //    xlsxOptions2.TextExportMode = TextExportMode.Text;
            //    xlsxOptions2.ExportHyperlinks = true;
            //    xlsxOptions2.SheetName = sheetname2;                        // 시트명 변경
            //    xlsxOptions2.ExportMode = XlsxExportMode.DifferentFiles;

            //    XlsxExportOptions xlsxOptions3 = new XlsxExportOptions();
            //    xlsxOptions3.ShowGridLines = true;
            //    xlsxOptions3.TextExportMode = TextExportMode.Text;
            //    xlsxOptions3.ExportHyperlinks = true;
            //    xlsxOptions3.SheetName = sheetname3;                        // 시트명 변경
            //    xlsxOptions3.ExportMode = XlsxExportMode.DifferentFiles;

            //    // 병합할 엑셀 임시파일 생성
            //    grid1.ExportToXlsx(TempName + "_1.xlsx", xlsxOptions1);
            //    grid2.ExportToXlsx(TempName + "_2.xlsx", xlsxOptions2);
            //    grid3.ExportToXlsx(TempName + "_3.xlsx", xlsxOptions3);

            //    // 워크북 생성 (엑셀 시트)
            //    Workbook book1 = new Workbook();
            //    book1.LoadDocument(TempName + "_1.xlsx", DocumentFormat.Xlsx); // 1번 그리드 정보 로드
            //    Workbook book2 = new Workbook();
            //    book2.LoadDocument(TempName + "_2.xlsx", DocumentFormat.Xlsx); // 2번 그리드 정보 로드
            //    Workbook book3 = new Workbook();
            //    book3.LoadDocument(TempName + "_3.xlsx", DocumentFormat.Xlsx); // 3번 그리드 정보 로드

            //    // 시트 병합
            //    book1.Append(book2);            // 1번 엑셀문서에 2번 시트 붙여넣기
            //    book1.Append(book3);            // 1번 엑셀문서에 3번 시트 붙여넣기
            //    book1.SaveDocument(FileName);   // 병합된 1번 엑셀을 최초경로에 저장

            //    // 엑셀 임시파일 삭제
            //    File.Delete(TempName + "_1.xlsx");
            //    File.Delete(TempName + "_2.xlsx");
            //    File.Delete(TempName + "_3.xlsx");
            //    Process.Start(FileName);
            //}
            //fileDlg.Dispose();
            //ComnMethod.SetLogInfo(name, text, ComnMethod.CONNECT_TYPE.엑셀);
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
    public static void SetGridRowExportStyle(GridView gv, Color color, string expression, bool isRow = true, string fieldName = "", Font font = null)
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

    /// <summary>
    /// 오늘 날짜와 자동 번호매김을 조합하여 전표번호 생성 메서드
    /// </summary>
    /// <param name="code">전표 구분자 ex) S, T, W , ...</param>
    /// <param name="date">오늘날짜</param>
    /// <param name="fieldname">해당 테이블의 전표번호 필드명 ex) SLINO, PONO, ...</param>
    /// <param name="tablename">전표번호 체크 대상 테이블명</param>
    /// <returns>자동채번된 전표번호 문자열</returns>
    public static string NextSlipNo(string code, string date, string fieldname, string tablename)
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
        else if (tablename.Equals("zUSRLST"))
        {
            strSql.AppendLine("SELECT ( CASE WHEN ISNULL(MAX(RIGHT(USRCD,5)),'')= ''");
            strSql.AppendLine("              THEN '00000'                         ");
            strSql.AppendLine("              ELSE RIGHT('00000' + CAST(CONVERT(INT, RIGHT(MAX(USRCD), 5)) + 1 AS VARCHAR) , 5) END ) AUTOSLINO ");
            strSql.AppendLine("  FROM zUSRLST ");
        }
        else if (tablename.Equals("REGION"))
        {
            strSql.AppendLine("SELECT ( CASE WHEN ISNULL(MAX(RIGHT(REGNO,5)),'')= ''");
            strSql.AppendLine("              THEN 'RE00001'                         ");
            strSql.AppendLine("              ELSE CONCAT('RE', RIGHT('00000' + CAST(CONVERT(INT, RIGHT(MAX(REGNO), 5)) + 1 AS VARCHAR) , 5)) END ) AUTOSLINO ");
            strSql.AppendLine("  FROM REGION_DAY ");
        }          
        else
        {
            strSql.AppendLine(" DECLARE @NO VARCHAR(15)='" + code + date + "';   ");
            strSql.AppendLine(" SELECT (CASE WHEN ISNULL(MAX(" + fieldname + "), '')='' THEN REPLACE(@NO, '%', '') + '001'    ");
            strSql.AppendLine(" ELSE SUBSTRING(MAX(" + fieldname + "), 1, 9) + RIGHT('000' + CAST(CONVERT(INT, SUBSTRING(MAX(" + fieldname + "), 10, 3)) + 1 AS VARCHAR), 3) END) AUTOSLINO  ");
            strSql.AppendLine(" FROM " + tablename + " ");
            strSql.AppendLine(" WHERE " + fieldname + " LIKE '%' + @NO + '%' ");
        }            
        DataTable dt = DBConn.GetDataTable(DBConn.dbCon, strSql.ToString());
        return dt.Rows[0]["AUTOSLINO"].ToString();
    }

    public static string NextSlipNoLong(string date, string fieldname, string tablename)
    {
        StringBuilder strSql = new StringBuilder();
        strSql.Clear();
        strSql.AppendLine(" DECLARE @NO VARCHAR(15)='" + date + "';   ");
        strSql.AppendLine(" SELECT (CASE WHEN ISNULL(MAX(" + fieldname + "), '')='' THEN REPLACE(@NO, '%', '') + '00001'    ");
        strSql.AppendLine(" ELSE SUBSTRING(MAX(" + fieldname + "), 1, 6) + RIGHT('00000' + CAST(CONVERT(INT, SUBSTRING(MAX(" + fieldname + "), 7, 5)) + 1 AS VARCHAR), 5) END) AUTOSLINO  ");
        strSql.AppendLine(" FROM " + tablename + " ");
        strSql.AppendLine(" WHERE " + fieldname + " LIKE '%' + @NO + '%' ");
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

    #region [GridView Row 위, 아래 이동]
    // 22.12.12 추가
    /// <summary>
    /// GridView의 Row 위치를 위로 이동시키는 메서드
    /// </summary>
    /// <param name="_gc">대상 GridControl</param>
    /// <param name="_gv">대상 GridView</param>
    public static void GridRowUpLocate(GridControl _gc, GridView _gv)
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
            gp_PrintMessage(ex.Message, "", MessageType.오류);
        }
    }

    // 22.12.12 추가
    /// <summary>
    /// GridView의 Row 위치를 아래 이동시키는 메서드
    /// </summary>
    /// <param name="_gc">대상 GridControl</param>
    /// <param name="_gv">대상 GridView</param>
    public static void GridRowDownLocate(GridControl _gc, GridView _gv)
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
            gp_PrintMessage(ex.Message, "", MessageType.오류);
        }
    }
    #endregion

    #region [ 스마트 1번가 접속 LOG API ]
    // 22.09.15 추가, 스마트 1번가 접속 로그 기록 API
    public static void LogAPI(string sGuBun, string sComip)
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
    public static void LogAPI(string sGuBun, string sComip, GridControl gc)
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
            DataTable dt = DBConn.GetDataTable(DBConn.dbCon, "DP_SY004F00", dicParams);
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
    public static bool CheckPassword(string pw)
    {
        /// 영 대/소문자 1개 이상, 숫자 1개 이상,  특수문자 1개 이상, 비밀번호 길이 8자 이상 체크
        //Regex rxPassword = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9]).{8,}$");
        // ^ : 라인의 처음
        // ?= : 전방 탐색
        // .* : 새 라인을 제외한 하나 이상 문자
        // [a-z] : a~z까지 영소문자,   [0-9] : 0~9까지 숫자,   [\W] : 특수문자열(!@#$...)
        // .{8,} : 8자리 이상
        // $ : 라인의 마지막
        /// (?=.*[a-z])(?=.*[A-Z]) : 영 대/소문자 각각 1개 이상 포함
        //Regex rxPassword = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[\W]).{8,}$");
        /// (?=.*[a-zA-Z]) : 영 대/소문자 중 1개 이상 포함
        Regex rxPassword = new Regex(@"^(?=.*[a-zA-Z])(?=.*[0-9])(?=.*[\W]).{8,}$");

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

    // 이벤트 핸들러용 버튼 클릭 메서드
    public static void gp_RefreshRetr(string msg)
    {
        sb.PerformClick();
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
