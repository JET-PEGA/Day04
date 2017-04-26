using Stimulsoft.Base;
using Stimulsoft.Report;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Day04
{
    public partial class frmMain : Form
    {
        /// <summary>
        /// 範本存放路徑
        /// </summary>
        private string _templatePath = "";

        /// <summary>
        /// 建構子
        /// </summary>
        public frmMain()
        {
            InitializeComponent();

            _templatePath = string.Format(@"{0}\{1}\", System.Environment.CurrentDirectory, "templates"); // 取得範本路徑

        }

        /// <summary>
        /// DataSet 來源 輸出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            string template = _templatePath + "DEMO_01.mrt";
            // 建立綁定資料
            string ret = string.Empty;
            DataSet ds = new DataSet("DEMO");
            DataTable dt = new DataTable("Data");
            dt.Columns.Add("ID");
            dt.Columns.Add("NAME");
            dt.Rows.Add("LA01", "Jet");
            dt.Rows.Add("LA02", "Eric");
            dt.Rows.Add("LA03", "David");
            ds.Tables.Add(dt);

            // 讀取範本輸出成PDF
            StiReport report = new StiReport();
            report.Load(template);
            report.RegData(ds);
            report.Render();
            report.ExportDocument(StiExportFormat.Pdf, template.Replace(".mrt", ".pdf"));
        }

        /// <summary>
        /// JSON File 來源 輸出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            string template = _templatePath + "DEMO_02.mrt";
            // 建立綁定資料
            string ret = string.Empty;
            DataSet ds = StiJsonToDataSetConverter.GetDataSetFromFile(_templatePath + "DEMO_02_2.json");
            StiReport report = new StiReport();
            report.Load(template);
            report.Dictionary.Databases.Clear();
            report.RegData(ds);
            report.Render();
            report.ExportDocument(StiExportFormat.Pdf, template.Replace(".mrt", "_1.pdf"));
            MessageBox.Show("OK");
        }

        /// <summary>
        /// JSON String 來源 輸出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            string template = _templatePath + "DEMO_02.mrt";
            // 建立綁定資料
            string ret = string.Empty;
            string jsonText = "";
            using (StreamReader sr = new StreamReader(_templatePath + "DEMO_02_2.json"))
            {
                jsonText = sr.ReadToEnd();
                sr.Close();
            }
            DataSet ds = StiJsonToDataSetConverter.GetDataSet(jsonText); // where jsonReport is string containing the JSON
            StiReport report = new StiReport();
            report.Load(template);
            report.Dictionary.Databases.Clear();
            report.RegData(ds);
            report.Render();
            report.ExportDocument(StiExportFormat.Pdf, template.Replace(".mrt", "_2.pdf"));
            MessageBox.Show("OK");
        }

        /// <summary>
        /// 將報表輸出成 mdc
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            string template = _templatePath + "DEMO_02.mrt";
            // 建立綁定資料
            string ret = string.Empty;
            DataSet ds = StiJsonToDataSetConverter.GetDataSetFromFile(_templatePath + "DEMO_02_2.json");
            StiReport report = new StiReport();
            report.Load(template);
            report.Dictionary.Databases.Clear();
            report.RegData(ds);
            report.Render();
            string json = report.SaveDocumentJsonToString();

            using (StreamWriter sw = new StreamWriter(template.Replace(".mrt", ".mdc"), false))
            {
                sw.Write(json);
                sw.Flush();
                sw.Close();
            }

            MessageBox.Show("OK");

        }


        private void button11_Click(object sender, EventArgs e)
        {
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

        }
    }
}
