using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AirPlane
{
    public partial class EmployeeInfoForm : Form
    {
        public SqlConnection myConnection = new SqlConnection(@"Data Source=HACKERMAN;Initial Catalog=MyDataBase;Integrated Security=True");
        public EmployeeInfoForm()
        {
            InitializeComponent();
            button2.Enabled = true;
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

        public string login
        {
            get; set;
        }

        private void EmployeeInfoForm_Load(object sender, EventArgs e)
        {
            SqlCommand cmdDataBase = new SqlCommand("SELECT * FROM Users WHERE UserLogin = 'admin';", myConnection);
            SqlDataReader myReader;
            try
            { 
                myConnection.Open();
                myReader = cmdDataBase.ExecuteReader();
                while (myReader.Read())
                {
                    textBox2.Text = myReader["FName"].ToString();
                    textBox8.Text = myReader["LName"].ToString();
                    textBox9.Text = myReader["SName"].ToString();
                    textBox1.Text = myReader["UserLogin"].ToString();
                    textBox3.Text = myReader["Address"].ToString();
                    textBox4.Text = myReader["Tel"].ToString();
                    textBox5.Text = myReader["Email"].ToString();
                    textBox6.Text = myReader["UserId"].ToString();
                    comboBox1.Text = myReader["Sex"].ToString();
                    comboBox2.Text = myReader["MaritalStatus"].ToString();
                    comboBox3.Text = myReader["Role"].ToString();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            myConnection.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();// remember to write the method for Saving updates
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Enabled = true;
            button1.Enabled = true;
            button6.Enabled = true;
            textBox9.Enabled = true;
            textBox8.Enabled = true;
            textBox3.Enabled = true;
            textBox4.Enabled = true;
            comboBox2.Enabled = true;
        }
        // Air Travel Organisers' Licensing
        private void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                string str = "INSERT INTO Users(UserLogin, Password, Email, Tel, Sex, Role, FName, LName, SName, MaritalStatus, Address) VALUES('" +
                textBox1.Text + "', '" + textBox7.Text + "', '" + textBox5.Text + "', '" + textBox4.Text + "', '" + comboBox1.Text + "', '" +
                comboBox3.Text + "', '" + textBox2.Text + "', '" + textBox8.Text + "', '" + textBox9.Text + "', '" + comboBox2.Text
                + "', '" + textBox3.Text + "');";
                SqlCommand cmdDataBase = new SqlCommand(str, myConnection);
                myConnection.Open();
                cmdDataBase.ExecuteNonQuery();
                MessageBox.Show("Employee saved");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
                myConnection.Close();
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string str = "DELETE FROM Users WHERE UserId = '"+textBox6.Text+"'";
            sSave_(str);
            MessageBox.Show("Employee has been successfuly deleted");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            textBox7.Enabled = true;
            textBox1.Enabled = true;
            textBox2.Enabled = true;
            textBox3.Enabled = true;
            textBox4.Enabled = true;
            textBox5.Enabled = true;
            textBox7.Enabled = true;
            textBox8.Enabled = true;
            textBox9.Enabled = true;
            comboBox1.Enabled = true;
            comboBox2.Enabled = true;
            comboBox3.Enabled = true;
            button6.Enabled = true;

            textBox7.Clear();
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox7.Clear();
            textBox8.Clear();
            textBox9.Clear();
            comboBox1.ResetText();
            comboBox2.ResetText();
            comboBox3.ResetText();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string str = "SELECT * FROM Users WHERE UserLogin = '"+textBox1.Text+"'";
            SqlCommand cmdDataBase = new SqlCommand(str, myConnection);
            SqlDataReader myReader;
            try
            {
                myConnection.Open();
                myReader = cmdDataBase.ExecuteReader();
                while (myReader.Read())
                {
                    textBox2.Text = myReader["FName"].ToString();
                    textBox8.Text = myReader["LName"].ToString();
                    textBox9.Text = myReader["SName"].ToString();
                    textBox1.Text = myReader["UserLogin"].ToString();
                    textBox3.Text = myReader["Address"].ToString();
                    textBox4.Text = myReader["Tel"].ToString();
                    textBox5.Text = myReader["Email"].ToString();
                    textBox6.Text = myReader["UserId"].ToString();
                    comboBox1.Text = myReader["Sex"].ToString();
                    comboBox2.Text = myReader["MaritalStatus"].ToString();
                    comboBox3.Text = myReader["Role"].ToString();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            myConnection.Close();
        }

        private void Updatebutton_Click(object sender, EventArgs e)
        {
            SqlCommand cmdDataBase = new SqlCommand("UPDATE Users SET UserLogin = '" + textBox1.Text +
                "', Password = '" +textBox7.Text+ 
                "', Email = '"+textBox5.Text+
                "', Tel = '"+textBox4.Text+
                "', Sex = '"+comboBox1.Text+
                "', Role = '"+comboBox3.Text+
                "', FName = '"+textBox2.Text+
                "', LName = '"+textBox8.Text+
                "', SName = '"+textBox9.Text+
                "', MaritalStatus = '"+comboBox2.Text+
                "', Address = '"+textBox3.Text+
                "' WHERE UserId = '" + textBox6.Text + 
                "' ", myConnection);

            myConnection.Open();
            cmdDataBase.ExecuteNonQuery();
            myConnection.Close();
            MessageBox.Show("Employee has been successfuly updated");
        }
    }
}
