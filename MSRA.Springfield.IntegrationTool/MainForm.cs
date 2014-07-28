using System;
using System.Data;
using System.Windows.Forms;
using System.ComponentModel;
using System.Workflow.Runtime;
using System.Collections.Generic;
using System.Threading;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;

namespace MSRA.Springfield.IntegrationTool
{
    public partial class MainForm : Form
    {
        private DataSet dsExportedData = new DataSet();
        private BackgroundWorker bw_Import = new BackgroundWorker();
        private Dictionary<string, object> Context = new Dictionary<string, object>();
        private string tempLogFile = string.Empty;

        /// <summary>
        /// create a container for workflow
        /// </summary>
        private WorkflowRuntime workflowContainer = null;

        public MainForm()
        {
            InitializeComponent();

            bw_Import.DoWork += new DoWorkEventHandler(bw_Import_DoWork);
            bw_Import.ProgressChanged += new ProgressChangedEventHandler(bw_Import_ProgressChanged);
            bw_Import.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_Import_RunWorkerCompleted);

            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "log/integration_log.txt"))
            {
                tempLogFile = AppDomain.CurrentDomain.BaseDirectory +
                    "log/integration_log_" + DateTime.Now.ToFileTime().ToString() + ".txt";

                File.Copy(AppDomain.CurrentDomain.BaseDirectory +
                    "log/integration_log.txt", tempLogFile);
            }
        }

        void bw_Import_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            tssInfo.Text = "Import Finished...";
            this.dataGridView1.Enabled = true;
            this.clearToolStripMenuItem.Enabled = this.toolStripButton3.Enabled = true;

            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "log/integration_log.txt"))
            {
                tempLogFile = AppDomain.CurrentDomain.BaseDirectory + 
                    "log/integration_log_" + DateTime.Now.ToFileTime().ToString() + ".txt";

                File.Copy(AppDomain.CurrentDomain.BaseDirectory + 
                    "log/integration_log.txt", tempLogFile);
            }
        }

        void bw_Import_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            
        }

        void bw_Import_DoWork(object sender, DoWorkEventArgs e)
        {
            DoImport();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadData()
        {
            dsExportedData = new DataSet();
            using (SqlConnection conn = new SqlConnection(
                ConfigurationManager.ConnectionStrings["PERSONHuman"].ConnectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter("select * from [PERSONHuman]", conn);
                da.Fill(dsExportedData, "ExportedData");
            }

            ddlFields.Items.Clear();
            for (int i = 0; i < dsExportedData.Tables[0].Columns.Count; i++)
            {
                ddlFields.Items.Add(dsExportedData.Tables[0].Columns[i].ColumnName);
            }

            this.dataGridView1.DataSource = dsExportedData.Tables[0];
            this.tssInfo.Text = dsExportedData.Tables[0].Rows.Count.ToString() + " records have been loaded...";
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            try
            {
                this.dataGridView1.Rows.Clear();
                LoadData();
                this.tabControl1.SelectedTab = this.tabPage1;
            }
            catch (Exception ex)
            { 
                MessageBox.Show(ex.Message);
            }
        }
                
        private void importToolStripMenuItem_Click(object sender1, EventArgs e1)
        {
            this.dataGridView1.Enabled = false;
            this.clearToolStripMenuItem.Enabled = this.toolStripButton3.Enabled = false;
            bw_Import.RunWorkerAsync();
        }

        private void DoImport()
        {
            if (Context.ContainsKey("ExportedData") == false)
            {
                Context.Add("ExportedData", dsExportedData.Tables[0]);
            }

            if (Context.ContainsKey("BatchNo") == false)
            {
                Context.Add("BatchNo", new Random().Next(1000, 9999));
            }

            using (WorkflowRuntime workflowContainer = new WorkflowRuntime())
            {
                AutoResetEvent waitHandle = new AutoResetEvent(false);

                workflowContainer.WorkflowCompleted +=
                    delegate(object sender, WorkflowCompletedEventArgs e)
                    {
                        waitHandle.Set();
                        //this.tssInfo.Text = "Finished";
                    };

                WorkflowInstance instance = workflowContainer.CreateWorkflow
                    (typeof(IntegrationWorkflow.IntegrationWorkflow), Context);

                instance.Start();
                waitHandle.WaitOne();
            }
        }

        private void openImoprtLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.rtbLog.Text = File.ReadAllText(tempLogFile);

            this.tabControl1.SelectedTab = this.tabPage2;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            this.dataGridView1.Rows.Clear();
            this.dsExportedData = new DataSet();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            importToolStripMenuItem_Click(sender, e);
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            openImoprtLogToolStripMenuItem_Click(sender, e);
        }

        private void openImoprtLogToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            openImoprtLogToolStripMenuItem_Click(sender, e);
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Directory.Delete(AppDomain.CurrentDomain.BaseDirectory + "log/", true);
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            this.dataGridView1.Enabled = false;
            this.clearToolStripMenuItem.Enabled = this.toolStripButton3.Enabled = false;

            string fieldName = this.ddlFields.SelectedItem.ToString();
            string fieldValue = this.txtValue.Text;

            DataView dv = dsExportedData.Tables[0].DefaultView;
            dv.RowFilter = string.Format(" {0} = '{1}'", fieldName, fieldValue);

            if (Context.ContainsKey("ExportedData")) Context.Remove("ExportedData");

            Context.Add("ExportedData", dv.ToTable());
            this.dataGridView1.DataSource = dv;
            //bw_Import.RunWorkerAsync();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
