using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AirPlane
{
    public partial class CompanySearchForm : Form
    {
        public SqlConnection myConnection = new SqlConnection(@"Data Source=HACKERMAN;Initial Catalog=MyDataBase;Integrated Security=True");
        DataTable dbdatatableset;
        SqlDataAdapter sda;

        public CompanySearchForm()
        {
            InitializeComponent();
            fillData();
        }

        private void fillData()
        {
            myConnection.Open();
            SqlCommand cmdDataBase = new SqlCommand("select * from Companies;", myConnection);
            try
            {
                sda = new SqlDataAdapter();
                sda.SelectCommand = cmdDataBase;
                dbdatatableset = new DataTable();
                sda.Fill(dbdatatableset);
                BindingSource bSource = new BindingSource();

                bSource.DataSource = dbdatatableset;
                dataGridView1.DataSource = bSource;
                sda.Update(dbdatatableset);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            myConnection.Close();
        }

        void FillCombo_(string Query, ComboBox comboTemp)
        {
            SqlCommand connect = new SqlCommand(Query, myConnection);
            SqlDataReader myReader;
            try
            {
                myConnection.Open();
                myReader = connect.ExecuteReader();
                while (myReader.Read())
                {
                    string comboString = myReader.GetString(0);
                    comboTemp.Items.Add(comboString);
                }

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }            
        }

        private void SortTextBox_TextChanged(object sender, EventArgs e)
        {
            DataView dbview = new DataView(dbdatatableset);
            dbview.RowFilter = string.Format("CompanyName LIKE '%{0}%'", SortTextBox.Text);
            dataGridView1.DataSource = dbview;
        }

        private void SortTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (Char.IsDigit(ch)) { e.Handled = true; }
        }
        

        private void CompComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            groupBox1.Enabled = true;
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            AddCompanyTextBox.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            codeTextBox.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            label2.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();

        }

        private void Addbutton_Click(object sender, EventArgs e)
        {
            string Query = "insert into Companies(CompanyName, AirlineCode) values('" + AddCompanyTextBox.Text + "', '" + codeTextBox.Text + "')";
            SqlCommand cmdDataBase = new SqlCommand(Query, myConnection);
            try
            {
                myConnection.Open();
                cmdDataBase.ExecuteNonQuery();
                MessageBox.Show("item has been saved successfuly ... ", "", MessageBoxButtons.OK);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            myConnection.Close();
            fillData();
        }

        private void Deletebutton_Click(object sender, EventArgs e)
        {
            string Query = "delete from Companies where CompanyName = '" + AddCompanyTextBox.Text + "';";
            SqlCommand cmdDataBase = new SqlCommand(Query, myConnection);
            SqlDataReader myReader;
            try
            {
                myConnection.Open();
                myReader = cmdDataBase.ExecuteReader();
                while (myReader.Read())
                { }
                MessageBox.Show("Item has been deleted successfuly ... ", "", MessageBoxButtons.OK);

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            myConnection.Close();

            fillData();
        }

        private void Editbutton_Click(object sender, EventArgs e)
        {

        }

        private void Updatebutton_Click(object sender, EventArgs e)
        {
            SqlCommand cmdDataBase = new SqlCommand("UPDATE Companies SET CompanyName = '" + AddCompanyTextBox.Text + "', AirlineCode = '" + codeTextBox.Text + "' WHERE CompanyId = '"+label2.Text+"' ", myConnection);
            myConnection.Open();
            cmdDataBase.ExecuteNonQuery();
            myConnection.Close();
            MessageBox.Show("Company has been successfuly updated");

            fillData();
        }
    }
}
