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
using System.Drawing;

using DevExpress.XtraEditors.Controls;

namespace VisionIns
{
    public partial class MT002F00 : DevExpress.XtraEditors.XtraForm
    {
        private string PROCEDURE_ID = "DP_MT002F00";
        private int _RefreshTimer = 0;

        public MT002F00()
        {
            InitializeComponent();

            Lb_CDATE.Text = DateTime.Now.ToString("yyyy-MM-dd");
            Lb_CTIME.Text = DateTime.Now.ToString("HH:mm:ss");

            SetQrImageBox();
        }

        private void SetTempQrImages()
        {
            QRItem1Img.Image = Properties.Resources.QR_Sample;
            QRItem2Img.Image = Properties.Resources.QR_Sample;
            QRItem3Img.Image = Properties.Resources.QR_Sample;
            QRItem4Img.Image = Properties.Resources.QR_Sample;
            QRItem5Img.Image = Properties.Resources.QR_Sample;
            QRItem6Img.Image = Properties.Resources.QR_Sample;
            QRItem7Img.Image = Properties.Resources.QR_Sample;
            QRItem8Img.Image = Properties.Resources.QR_Sample;
            QRItem9Img.Image = Properties.Resources.QR_Sample;
            QRItem10Img.Image = Properties.Resources.QR_Sample;

            QRITEM1CODE.Text = "A1234";
            QRITEM2CODE.Text = "A1235";
            QRITEM3CODE.Text = "A1236";
            QRITEM4CODE.Text = "A1237";
            QRITEM5CODE.Text = "A1238";
            QRITEM6CODE.Text = "A1239";
            QRITEM7CODE.Text = "A1240";
            QRITEM8CODE.Text = "A1241";
            QRITEM9CODE.Text = "A1242";
            QRITEM10CODE.Text = "A1243";

            //QRItem1Img.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            //QRItem2Img.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            //QRItem3Img.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            //QRItem4Img.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            //QRItem5Img.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            //QRItem6Img.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            //QRItem7Img.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            //QRItem8Img.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            //QRItem9Img.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            //QRItem10Img.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
        }

        private void SetQrImageBox()
        {


            PictureEdit[] qrImgs =
            {
                QRItem1Img,
                QRItem2Img,
                QRItem3Img,
                QRItem4Img,
                QRItem5Img,
                QRItem6Img,
                QRItem7Img,
                QRItem8Img,
                QRItem9Img,
                QRItem10Img
            };

            foreach (PictureEdit img in qrImgs)
            {
                img.Dock = DockStyle.None;
                img.Anchor = AnchorStyles.None;

                img.Size = new Size(150, 150);
                img.MinimumSize = new Size(150, 150);
                img.MaximumSize = new Size(150, 150);

                img.Properties.SizeMode = PictureSizeMode.Zoom;
                img.BorderStyle = BorderStyles.NoBorder;
            }
        }

        private void MT002F00_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            Lb_CDATE.Text = DateTime.Now.ToString("yyyy-MM-dd");
            Lb_CTIME.Text = DateTime.Now.ToString("HH:mm:ss");

            _RefreshTimer++;
            if (_RefreshTimer > 10)
            {
                Retr();
                _RefreshTimer = 0;
            }
        }

        private void Retr()
        {

        }

        //전체화면 esc로 닫기
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.Close();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
