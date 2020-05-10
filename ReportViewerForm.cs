using System;
using System.Data;
using System.Data.SqlClient;
using CrystalDecisions.CrystalReports.Engine;
using System.Windows.Forms;

namespace AirPlane
{
    public partial class ReportViewerForm : Form
    {
        SqlConnection myConnection = new SqlConnection(@"Data Source=HACKERMAN;Initial Catalog=MyDataBase;Integrated Security=True");
        DataSet dbSet;
        DataTable dbdatatableset;
        ReportDocument document = new ReportDocument();
        public ReportViewerForm()
        {
            InitializeComponent();
        }

        private void ReportViewerForm_Load(object sender, EventArgs e)
        {
            myConnection.Open();
            SqlCommand cmdDataBase = new SqlCommand("SELECT DISTINCT TransId, UserLogin, Address, [client Name], BookingDate, Charges, PaidAmount, Total, Type, Remark FROM TrancReport", myConnection);
            try
            {
                SqlDataAdapter sd = new SqlDataAdapter();
                sd.SelectCommand = cmdDataBase;
                dbdatatableset = new DataTable();
                sd.Fill(dbdatatableset);
                BindingSource bSource = new BindingSource();

                bSource.DataSource = dbdatatableset;
                dataGridView1.DataSource = bSource;
                sd.Update(dbdatatableset);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            myConnection.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {

                SqlCommand cmdDataBase = new SqlCommand("SELECT DISTINCT * FROM Invoices WHERE [Client Name] = '" + textBox1.Text + "';", myConnection);

                try
                {
                    document.Load(@"C:\Users\mufalmeva\Documents\Visual Studio 2015\Эварист4K\AirPlane\IncoiceRPT.rpt");
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = cmdDataBase;
                    DataSet dbdataset = new DataSet();
                    adapter.Fill(dbdataset, "RPTinv");
                    document.SetDataSource(dbdataset);
                    crystalReportViewer1.ReportSource = document;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (!string.IsNullOrEmpty(textBox2.Text))
            {

                SqlCommand cmdDataBase = new SqlCommand("SELECT DISTINCT * FROM Invoices WHERE OSN = '" + textBox2.Text + "';", myConnection);

                try
                {
                    document.Load(@"C:\Users\mufalmeva\Documents\Visual Studio 2015\Эварист4K\AirPlane\IncoiceRPT.rpt");
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = cmdDataBase;
                    DataSet dbdataset = new DataSet();
                    adapter.Fill(dbdataset, "RPTinv");
                    document.SetDataSource(dbdataset);
                    crystalReportViewer1.ReportSource = document;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button4.Visible = true;
            SqlCommand cmdDataBase = new SqlCommand("SELECT DISTINCT * FROM TrancReport WHERE TransId = '"+dataGridView1.SelectedRows[0].Cells[0].Value.ToString()+"';", myConnection);

            try
            {
                document.Load(@"C:\Users\mufalmeva\Documents\Visual Studio 2015\Эварист4K\AirPlane\CrystalReport2.rpt");
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmdDataBase;
                DataSet dbdataset = new DataSet();
                adapter.Fill(dbdataset, "RPTtranc");
                document.SetDataSource(dbdataset);
                crystalReportViewer1.ReportSource = document;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            panel1.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {
            try
            {
                DataView dbview = new DataView(dbdatatableset);
                dbview.RowFilter = string.Format("[Client Name] LIKE '%{0}%'", toolStripTextBox1.Text);
                dataGridView1.DataSource = dbview;
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                DataView dbview = new DataView(dbdatatableset);
                dbview.RowFilter = string.Format("CONVERT(BookingDate, System.String) LIKE '%{0}%'", dateTimePicker1.Text);
                dataGridView1.DataSource = dbview;
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel1.Show();
            button4.Visible = false;
        }
    }
}
