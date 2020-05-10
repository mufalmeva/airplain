using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AirPlane
{
    public partial class SetClientForm : Form
    {
        public SqlConnection myConnection = new SqlConnection(@"Data Source=HACKERMAN;Initial Catalog=MyDataBase;Integrated Security=True");
        DataTable dbdatatableset;

        public SetClientForm()
        {
            InitializeComponent();
            fillData();
        }

        void sSave_(string Query)
        {
            SqlCommand connect = new SqlCommand(Query, myConnection);
            SqlDataReader myReader;
            try
            {
                myConnection.Open();
                myReader = connect.ExecuteReader();
                while (myReader.Read())
                { }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            myConnection.Close();
        }

        private void fillData()
        {
            myConnection.Open();
            SqlCommand cmdDataBase = new SqlCommand("select * from Clients;", myConnection);
            try
            {
                SqlDataAdapter sda = new SqlDataAdapter();
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

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string str = "DELETE FROM Clients WHERE ClientId = '" + textBox2.Text + "'";
            sSave_(str);
            MessageBox.Show("Client has been successfuly deleted");
            fillData();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string Query = "INSERT INTO Clients(CLFName, CLLName, CLSName, Sex, Birthday, Passport, CreditCard, Tel, EMail) VALUES ('" + textBoxFN.Text +
                    "', '" + textBoxSN.Text + "','" + textBoxLN.Text + "','" + comboBox2.Text +
                    "','" + dateTimePicker2.Text + "','" + textBoxPassport.Text +
                    "','" + textBoxCreditcard.Text + "','" + textBoxTel.Text + "')";
                SqlCommand cmdDataBase = new SqlCommand(Query, myConnection);
                myConnection.Open();
                cmdDataBase.ExecuteNonQuery();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            myConnection.Close();
            fillData();
        }
        private void textBox1_TextChanged(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (Char.IsDigit(ch)) { e.Handled = true; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBoxFN.Clear();
            textBoxLN.Clear();
            textBoxPassport.Clear();
            textBoxSN.Clear();
            textBoxTel.Clear();
            textBoxEmail.Clear();
            textBoxCreditcard.Clear();
            comboBox2.Text = "";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            DataView dbview = new DataView(dbdatatableset);
            dbview.RowFilter = string.Format("CLFName LIKE '%{0}%'", textBox1.Text);
            dataGridView1.DataSource = dbview;
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textBoxFN.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            textBoxLN.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            textBoxSN.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            comboBox2.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            dateTimePicker2.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
            textBoxPassport.Text = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();
            textBoxCreditcard.Text = dataGridView1.SelectedRows[0].Cells[7].Value.ToString();
            textBoxTel.Text = dataGridView1.SelectedRows[0].Cells[8].Value.ToString();
            textBoxEmail.Text = dataGridView1.SelectedRows[0].Cells[9].Value.ToString();
        }

        private void Updatebutton_Click(object sender, EventArgs e)
        {
            try
            {
                string sQuery = "UPDATE Clients SET CLFName = '" + textBoxFN.Text +
                "', CLLName = '" + textBoxLN.Text +
                "', CLSName = '" + textBoxSN.Text +
                "', Sex = '" + comboBox2.Text +
                "', Birthday = '" + dateTimePicker2.Text +
                "', Passport = '" + textBoxPassport.Text +
                "', CreditCard = '" + textBoxCreditcard.Text +
                "', Tel = '" + textBoxTel.Text +
                "', EMail = '" + textBoxEmail.Text +
                "' WHERE ClientId = '" + textBox2.Text +
                "' ";
                SqlCommand cmdDataBase = new SqlCommand(sQuery, myConnection);
                myConnection.Open();
                cmdDataBase.ExecuteNonQuery();
                MessageBox.Show("Client has been successfuly updated");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            myConnection.Close();
            fillData();
        }
    }
}
