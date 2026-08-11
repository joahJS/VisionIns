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
using static ComnMethod;

namespace VisionIns
{
    public partial class SY000F00 : DevExpress.XtraEditors.XtraForm
    {
        public SY000F00()
        {
            InitializeComponent();
        }

        private void SY000F00_Load(object sender, EventArgs e)
        {
            ComnEtcFunc.gp_SetColorFocused(layoutControl1);
            //ComnMethod.SetDateFromToValueThisYear(de_Fr, de_To);
            ComnGridFunc.GridStyleBasicSetting(gridView1);
            bt_Retr.PerformClick();
        }

        private void Bt_Retr_Click(object sender, EventArgs e)
        {
            //string DATE_F = string.IsNullOrEmpty(de_Fr.EditValue?.ToString()) ? string.Empty : de_Fr.EditValue.ToString().Substring(0, 10);
            //string DATE_T = string.IsNullOrEmpty(de_To.EditValue?.ToString()) ? string.Empty : de_To.EditValue.ToString().Substring(0, 10);

            parameterDic.Clear();
            parameterDic.Add("CMD", "LIST_VERSION");
            //parameterDic.Add("DATE_F", DATE_F);
            //parameterDic.Add("DATE_T", DATE_T);
            gridControl1.DataSource = DBConn.GetDataTable("DP_SY000F00", parameterDic);
        }

        private void Bt_Add_Click(object sender, EventArgs e)
        {
            SY000F01 frm = new SY000F01();
            frm.Owner = this;
            frm.DataSendEvent += new SY000F01.SendDataHandler(GetSLINO);
            frm.Show();
        }

        private void GetSLINO(string SLINO)
        {
            bt_Retr.PerformClick();
            gridView1.FocusedRowHandle = gridView1.LocateByDisplayText(0, gridColumn_VERSION_ID, SLINO);
        }

        private void Bt_Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SY000F00_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5) { bt_Retr.PerformClick(); }
            if (e.KeyCode == Keys.F1) { bt_Add.PerformClick(); }
            if (e.KeyCode == Keys.Escape) { bt_Close.PerformClick(); }
        }
    }
}