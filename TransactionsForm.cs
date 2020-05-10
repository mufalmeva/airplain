using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using CrystalDecisions;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AirPlane
{
    public partial class TransactionsForm : Form
    {
        public SqlConnection myConnection = new SqlConnection(@"Data Source=HACKERMAN;Initial Catalog=MyDataBase;Integrated Security=True");

        OrderForm order;
        DataTable dbdatatableset;
        public double money
        {
            get;
            set;            
        }
        public TransactionsForm()
        {
            InitializeComponent();
            order = new OrderForm();
            textBox4.Text = order.textBox1.Text;
            textBox7.Text = order.textBox8.Text;
            _Operate("SELECT MAX (TransId) FROM Transactions;", textBox5);
            _fillDataGridView();
        }

        void _Operate(string _str, TextBox textB)
        {
            SqlCommand cmdDataBase = new SqlCommand(_str, myConnection);
            SqlDataReader myReader;
            try
            {
                myConnection.Open();
                myReader = cmdDataBase.ExecuteReader();
                while (myReader.Read())
                {
                    int route = myReader.GetInt32(0);
                    textB.Text = route.ToString();
                }
                cmdDataBase.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                textB.Text = "1";
            }
            myConnection.Close();
        }

        void Operate(string _str, TextBox textB)
        {
            SqlCommand cmdDataBase = new SqlCommand(_str, myConnection);
            SqlDataReader myReader;
            try
            {
                myConnection.Open();
                myReader = cmdDataBase.ExecuteReader();
                while (myReader.Read())
                {
                    string route = myReader.GetString(0);
                    textB.Text = route;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            myConnection.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                order = new OrderForm();

                if (!string.IsNullOrEmpty(comboBox4.Text))
                {
                    string Query = "INSERT INTO Transactions VALUES ('" + dateTimePicker1.Text + "', '" + textBox8.Text + "', '" + textBox6.Text + "', '" + textBox10.Text + "', '" + textBox11.Text + "', '" + comboBox4.Text + "', '" + richTextBox1.Text + "')";
                    _sSave(Query);
                    _fillDataGridView();
                }
                else MessageBox.Show("Please, Choose the type value!");

                order.textBox7.Text = textBox6.Text;
                order.textBox6.Text = textBox8.Text;
                order.textBox5.Text = textBox11.Text;
                order.money = textBox11.Text;
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void _fillDataGridView()
        {
            string Query = "SELECT Transactions.TransId AS [Trans. No], Transactions.BookingDate AS [Trans. Date], Transactions.PaidAmount AS [Amount], Transactions.Type AS Type FROM Transactions;";
            myConnection.Open();
            SqlCommand cmdDataBase = new SqlCommand(Query, myConnection);
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
        private void _sSave(string Query)
        {
            try
            {
                SqlCommand cmdDataBase = new SqlCommand(Query, myConnection);
                myConnection.Open();
                cmdDataBase.ExecuteNonQuery();
                myConnection.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            myConnection.Close();
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(textBox8.Text))
                {
                    textBox9.Text = (double.Parse(textBox11.Text) - double.Parse(textBox6.Text)).ToString();
                }
                else if (string.IsNullOrEmpty(textBox8.Text))
                {
                   textBox9.Text = "0";
                   textBox9.Text = (double.Parse(textBox11.Text) - double.Parse(textBox6.Text)).ToString();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox11.Text = (double.Parse(textBox8.Text) + money).ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                Operate("SELECT distinct Clients.CLFName +' '+ Clients.CLLName + ' '+ Clients.CLSName FROM MyDataBase.dbo.Clients WHERE ClientId = (select ClientId from MyDataBase.dbo.Orders where MyDataBase.dbo.Orders.ClientId = MyDataBase.dbo.Clients.ClientId)", textBox7);
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                textBox5.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                dateTimePicker1.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                textBox6.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                comboBox4.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
