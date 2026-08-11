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
using System.IO;

namespace VisionIns
{
    public partial class SY000F01 : DevExpress.XtraEditors.XtraForm
    {
        public SY000F01()
        {
            InitializeComponent();
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            de_Upload.EditValue = DateTime.Now;
            tx_Version.EditValue = Application.ProductVersion;
        }

        public delegate void SendDataHandler(string sValue);
        public event SendDataHandler DataSendEvent;
        public DataRow _row_FILE = null;
        byte[] _FILE = null;

        private void SY000F01_Load(object sender, EventArgs e)
        {
            ComnEtcFunc.gp_SetColorFocused(layoutControl1);
            if (_row_FILE != null)
            {
                tx_Version.EditValue = _row_FILE["VERSION_ID"];
                de_Upload.EditValue = _row_FILE["UPLOAD_DT"];
                tx_FileName.EditValue = _row_FILE["FILE_NAME"];
                tx_FileSize.EditValue = _row_FILE["FILE_BYTE"];
                mm_Remark.EditValue = _row_FILE["VERSION_RMK"];
                bt_Upload.Enabled = false;
            }
            else
            {
                bt_Download.Enabled = false;
            }
        }

        private void Bt_Download_Click(object sender, EventArgs e)
        {

        }

        private void Bt_Upload_Click(object sender, EventArgs e)
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;

            using (XtraOpenFileDialog openFileDialog = new XtraOpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "Exe Files (.exe)|*.exe|All Files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;

                    //Read the contents of the file into a stream
                    var fileStream = openFileDialog.OpenFile();
                    _FILE = new byte[fileStream.Length];
                    fileStream.Read(_FILE, 0, _FILE.Length);

                    tx_FileSize.EditValue = _FILE.Length;

                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        fileContent = reader.ReadToEnd();
                    }
                }
            }
            XtraMessageBox.Show("저장을 눌러주세요.");
        }

        private void Bt_Save_Click(object sender, EventArgs e)
        {
            string version = tx_Version.EditValue?.ToString();
            string uploadDate = de_Upload.EditValue?.ToString().Substring(0, 10);
            string fileName = tx_FileName.EditValue?.ToString();
            int fileSize = string.IsNullOrEmpty(tx_FileSize.EditValue?.ToString()) ?
                0 : Convert.ToInt32(tx_FileSize.EditValue?.ToString());
            string remark = mm_Remark.EditValue?.ToString();

            if (_FILE == null && _row_FILE == null)
            {
                XtraMessageBox.Show("파일을 업로드하세요");
                return;
            }
            else if (string.IsNullOrEmpty(version))
            {
                XtraMessageBox.Show("VersionID를 입력하세요(예 : 1.1.10");
                tx_Version.Focus();
                return;
            }
            else if (string.IsNullOrEmpty(uploadDate))
            {
                XtraMessageBox.Show("업로드 일자를 입력하세요");
                de_Upload.Focus();
                return;
            }

            parameterList.Clear();
            parameterList.Add(new Parameter("VERSION_ID", version, SqlDbType.NVarChar));
            parameterList.Add(new Parameter("UPLOAD_DT", uploadDate, SqlDbType.NVarChar));
            parameterList.Add(new Parameter("FILE_NAME", fileName, SqlDbType.NVarChar));
            parameterList.Add(new Parameter("FILE_BYTE", fileSize, SqlDbType.Int));
            parameterList.Add(new Parameter("VERSION_RMK", remark, SqlDbType.NVarChar));
            parameterList.Add(new Parameter("USRID", LoginUser.USRID, SqlDbType.NVarChar));
            parameterList.Add(new Parameter("FILE_NO", _FILE, SqlDbType.VarBinary));
            DataTable dt = DBConn.GetDataTable("DP_SY000F01", parameterList);
            string msg = ComnString.TXT_SAVE_FAIL;
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    msg = dt.Rows[0]["MSG"]?.ToString();
                }
            }
            XtraMessageBox.Show(msg);
            DataSendEvent(version);
            Dispose();
        }

        private void Bt_Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SY000F01_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F3) { bt_Save.PerformClick(); }
            else if (e.KeyCode == Keys.Escape) { bt_Close.PerformClick(); }
        }
    }
}