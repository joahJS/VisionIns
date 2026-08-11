using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace VisionIns
{
    /*
     * 클래스설명 : 품목검색창 (차종정보관리(CM001F00) 데이터 기반)
     */
    public partial class ProductSelect : DevExpress.XtraEditors.XtraForm
    {
        // CM001F00(차종정보관리)와 동일한 프로시저를 사용하여 별도의 DB 작업 없이 조회 가능
        private string PROCEDURE_ID = "DP_CM001F00";

        public ProductSelect()
        {
            InitializeComponent();
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
        }

        #region [변수]
        public delegate void SendDataHandler(DataRow row);
        public event SendDataHandler DataRowSendEvent;
        public string FindWord = string.Empty;
        #endregion

        #region [화면로드]
        private void ProductSelect_Load(object sender, EventArgs e)
        {
            ComnGridFunc.GridStyleBasicSetting(GridViewProduct);

            if (!string.IsNullOrEmpty(FindWord))
                TxtSearch.EditValue = FindWord;

            BtnRetr.PerformClick();
        }
        #endregion

        #region [조회]
        private void BtnRetr_Click(object sender, EventArgs e)
        {
            GetProductInfo();
        }

        private void GetProductInfo()
        {
            FindWord = TxtSearch.EditValue?.ToString();

            ComnMethod.parameterDic.Clear();
            ComnMethod.parameterDic.Add("CMD", "LIST");
            ComnMethod.parameterDic.Add("FIND_IDX", CboSearch.SelectedIndex.ToString());
            ComnMethod.parameterDic.Add("FIND_WORD", FindWord);
            GridProduct.DataSource = DBConn.GetDataTable(PROCEDURE_ID, ComnMethod.parameterDic);

            if (GridViewProduct.RowCount > 0)
                GridViewProduct.Focus();
            else
                TxtSearch.Focus();
        }

        private void TxtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                BtnRetr.PerformClick();
        }
        #endregion

        #region [품목선택]
        private void BtnChoice_Click(object sender, EventArgs e)
        {
            if (GridViewProduct.GetFocusedDataRow() == null)
            {
                ComnEtcFunc.gp_PrintMessage("선택된 품목이 없습니다.", Text, ComnEtcFunc.MessageType.알림);
                return;
            }

            DataRowSendEvent?.Invoke(GridViewProduct.GetFocusedDataRow());
            DialogResult = DialogResult.OK;
            Close();
        }

        private void GridViewProduct_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Clicks == 2)
                BtnChoice.PerformClick();
        }

        private void GridViewProduct_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                BtnChoice.PerformClick();
        }
        #endregion

        #region [단축키 & 닫기]
        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ProductSelect_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
                BtnRetr.PerformClick();
            else if (e.KeyCode == Keys.F3)
                BtnChoice.PerformClick();
            else if (e.KeyCode == Keys.Escape)
                Close();
        }
        #endregion
    }
}
