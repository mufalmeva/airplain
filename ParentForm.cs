using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using CrystalDecisions.CrystalReports.Engine;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AirPlane
{
    public partial class ParentForm : Form
    {
        public ParentForm()
        {
            InitializeComponent();
        }

        LoginForm log ;

        private void создатьОкноToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HomeForm ft = new HomeForm();
            ft.MdiParent = this;
            ft.MdiParent.StartPosition = FormStartPosition.Manual;
            ft.ShowDialog();
        }

        private void CompaniesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CompanySearchForm comp = new CompanySearchForm();
            comp.MdiParent = this;
            comp.Show();
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormClosingEventArgs ex;
            DialogResult Result = MessageBox.Show("Вы собираетесь выйти из приложения...!", "Выход", MessageBoxButtons.YesNo);
            if (Result == DialogResult.Yes)
            {
                log = new LoginForm();
                this.Dispose();
                log.Show();
            }
            else if (Result == DialogResult.No) { ex = new FormClosingEventArgs(CloseReason.FormOwnerClosing, cancel: true); }
        }

        private void ParentForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult Result = MessageBox.Show("Вы собираетесь выйти из приложения...!", "Выход", MessageBoxButtons.YesNo);
            if (Result == DialogResult.Yes)
            {
                log = new LoginForm();
                this.Dispose();
                log.Show();
            }
            else if (Result == DialogResult.No) { e.Cancel = true; }
        }

        private void flightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FlightsRegard_Update_Form fly = new FlightsRegard_Update_Form();
            fly.StartPosition = FormStartPosition.CenterScreen;
            fly.ShowDialog();
        }
        

        private void employeeInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EmployeeInfoForm employeeinfo = new EmployeeInfoForm();
            employeeinfo.MdiParent = this;
            employeeinfo.Show();
        }

        private void agentInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetClientForm client = new SetClientForm();
            client.MdiParent = this;
            client.Show();
        }

        private void agentFormToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            OrderForm orderAgent = new OrderForm();
            orderAgent.MdiParent = this;
            orderAgent.Show();
        }

        private void generalFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OrderForm generalForm = new OrderForm();
            generalForm.Text = "Booking Card for General";
            generalForm.ShowDialog();
        }
        

        private void OrdersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OrderForm brony = new OrderForm();
            brony.MdiParent = this;
            brony.Show();
        }
        
        private void TransactionReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReportViewerForm report = new ReportViewerForm();
            report.panel1.Visible = true;
            report.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            report.dataGridView1.Dock = DockStyle.Fill;
            report.ShowDialog();
        }

        private void salesReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReportViewerForm report = new ReportViewerForm();
            report.ShowDialog();
        }

        private void ticketIssuedReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReportViewerForm report = new ReportViewerForm();
            report.ShowDialog();
        }

        private void settlementReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReportViewerForm report = new ReportViewerForm();
            report.ShowDialog();
        }

        private void incompleteIncoiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReportViewerForm report = new ReportViewerForm();
            report.ShowDialog();

        }

        private void setFlightsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetFlightForm setF = new SetFlightForm();
            setF.MdiParent = this;
            setF.Show();
        }

        private void homeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HomeForm home = new HomeForm();
            home.MdiParent = this;
            home.Show();
        }
        SqlConnection myConnection = new SqlConnection(@"Data Source=HACKERMAN;Initial Catalog=MyDataBase;Integrated Security=True");
        //ReportDocument document = new ReportDocument();

        private void SearchInvoiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReportViewerForm report = new ReportViewerForm();
            SqlCommand cmdDataBase = new SqlCommand("SELECT DISTINCT * FROM Invoices ;", myConnection);
            report.label1.Visible = true;
            report.label2.Visible = true;
            report.textBox1.Visible = true;
            report.textBox2.Visible = true;
            report.button1.Visible = true;
            try
            {
                //document.Load(@"C:\Users\mufalmeva\Documents\Visual Studio 2015\Эварист4K\AirPlane\IncoiceRPT.rpt");
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmdDataBase;
                DataSet dbdataset = new DataSet();
                adapter.Fill(dbdataset, "RPTinv");
                //document.SetDataSource(dbdataset);
                //report.crystalReportViewer1.ReportSource = document;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            report.ShowDialog();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.ShowDialog();
        }
    }
}
