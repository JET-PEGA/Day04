using Stimulsoft.Report;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReportViewerTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            string template = string.Format(@"{0}\{1}\", System.Environment.CurrentDirectory, "templates") + "DEMO.mrt";

            StiReport report = new StiReport();
            report.Load(template);
            report.Design();
            report.Save(template.Replace(".mrt", "_new.mrt"));
        }
    }
}
