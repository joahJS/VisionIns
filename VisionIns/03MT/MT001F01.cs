using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using static ComnFunc;
using static GridFunc;
using System.IO;


namespace VisionIns
{
    public partial class MT001F01 : XtraForm
    {
        public delegate void SendDataHandler(string sVal);
        public event SendDataHandler DataRowSendEvent;

        private string _inspNo = "";
        private DataRow _row;

        public MT001F01()
        {
            InitializeComponent();
        }

        public MT001F01(string inspNo)
        {
            InitializeComponent();

            _inspNo = inspNo;
        }

        public MT001F01(DataRow row)
        {
            InitializeComponent();

            _row = row;
        }

        private void LoadInspectionDetail(string inspNo)
        {
            // 여기서 검사번호 기준으로 DB 조회

        }



        private void MT001F01_Load(object sender, EventArgs e)
        {
            // 전달받은 검사번호로 상세정보 조회
            //if (!string.IsNullOrEmpty(_inspNo))
            //{
            //    LoadInspectionDetail(_inspNo);
            //}

            if (_row != null)
            {
                SetDetailData(_row);
            }
        }

        //
        private void SetDetailData(DataRow row)
        {
            BasicInfoItem1Text.Text = row["INSPCD"].ToString();
            BasicInfoItem2Text.Text = row["WDATE"].ToString();
            BasicInfoItem3Text.Text = row["WTIME"].ToString();
            BasicInfoItem4Text.Text = row["ITCOD"].ToString();
            BasicInfoItem5Text.Text = row["WORKNM"].ToString();
            BasicInfoItem6Text.Text = row["INSPRSLT"].ToString();

            // 판정 색상
            if (row["INSPRSLT"].ToString() == "OK")
            {
                BasicInfoItem6Text.ForeColor = ColorTranslator.FromHtml("#5ED845");
                lblSumSignal.BackColor = ColorTranslator.FromHtml("#5ED845");
            }
            else
            {
                BasicInfoItem6Text.ForeColor = ColorTranslator.FromHtml("#FF4D45");
                lblSumSignal.BackColor = ColorTranslator.FromHtml("#FF4D45");
            }

            lblSumSignal.Text = row["INSPRSLT"].ToString();

            //검사항목 구분(INSP1~INSP5 OK/NG 개수 계산)
            int okCount = 0;
            int ngCount = 0;

            string[] inspColumns = { "INSP1", "INSP2", "INSP3", "INSP4", "INSP5" };

            foreach (string col in inspColumns)
            {
                string value = row[col]?.ToString().Trim().ToUpper();

                if (value == "OK")
                {
                    okCount++;
                }
                else if (value == "NG")
                {
                    ngCount++;
                }
            }

            // 화면 표시
            lblSumValue1.Text = ngCount.ToString();
            lblSumValue2.Text = okCount.ToString();

            // 검사항목 표시 예시
            lblResultText1.Text = row["INSP1"].ToString();
            lblResultText2.Text = row["INSP2"].ToString();
            lblResultText3.Text = row["INSP3"].ToString();
            lblResultText4.Text = row["INSP4"].ToString();
            lblResultText5.Text = row["INSP5"].ToString();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
