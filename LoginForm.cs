using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AirPlane
{
    public partial class LoginForm : Form
    {
        ParentForm parent = new ParentForm();
        public SqlConnection myConnection = new SqlConnection(@"Data Source=HACKERMAN;Initial Catalog=MyDataBase;Integrated Security=True");
        public LoginForm()
        {
            InitializeComponent();
            Username_text.Focus();
        }
        string user_;
        public string user
        {
            
            get
            {
                return user_;
            }
            set
            {
                user_ = value;
            }
        }

        private void Login_button_Click(object sender, EventArgs e)
        {
            EmployeeInfoForm log = new EmployeeInfoForm();
            OrderForm log_ = new OrderForm();
            try
            {
                SqlCommand cmdDatabase = new SqlCommand("select * from Users where UserLogin = '"+ Username_text.Text + "'and Password = '" + Password_text.Text + "';", myConnection);
                SqlDataReader myReader;
                myConnection.Open();
                myReader = cmdDatabase.ExecuteReader();
                int count = 0;
                while (myReader.Read())
                {
                    count = count + 1;
                }
                myConnection.Close();
                if (count == 1)
                {
                    user = Username_text.Text;
                    MessageBox.Show("UserName and Password are correct");
                    parent.toolStripStatusLabel1.Text = user;
                    parent.Show();
                    this.Hide();
                }
                else if (count > 1)
                {
                    MessageBox.Show("Duplicate Username and password ... Access denied!", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    Username_text.Clear();
                    Password_text.Clear();
                    Username_text.Focus();
                }
                else
                {
                    MessageBox.Show("Username and password is not correst .. Please repeat!", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    Username_text.Clear();
                    Password_text.Clear();
                    Username_text.Focus();
                }
                myConnection.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void button1_Click(object sender, EventArgs e)
        {
           Application.Exit();
        }
        
        private void Username_text_KeyPress(object sender, KeyPressEventArgs e)
        {
            EmployeeInfoForm log = new EmployeeInfoForm();
            OrderForm log_ = new OrderForm();
            if (e.KeyChar == (char)(Keys.Enter))
            {
                if(!string.IsNullOrEmpty(Username_text.Text))
                {
                    try
                    {
                        SqlCommand cmdDatabase = new SqlCommand("select * from Users where UserLogin = '" + Username_text.Text + "'and Password = '" + Password_text.Text + "';", myConnection);
                        SqlDataReader myReader;
                        myConnection.Open();
                        myReader = cmdDatabase.ExecuteReader();
                        int count = 0;
                        while (myReader.Read())
                        {
                            count = count + 1;
                        }
                        myConnection.Close();
                        if (count == 1)
                        {
                            user = Username_text.Text;
                            MessageBox.Show("UserName and Password are correct");
                            parent.toolStripStatusLabel1.Text = user;
                            parent.Show();
                            this.Hide();
                        }
                        else if (count > 1)
                        {
                            MessageBox.Show("Duplicate Username and password ... Access denied", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            Username_text.Clear();
                            Password_text.Clear();
                            Username_text.Focus();
                        }
                        else
                        {
                            MessageBox.Show("Username and password is not correst .. Please repeat", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            Username_text.Clear();
                            Password_text.Clear();
                            Username_text.Focus();
                        }
                        myConnection.Close();
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                }
                else
                {
                    MessageBox.Show("Please, enter your username !", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
                
        }
    }
            
}
