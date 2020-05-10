using System;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using System.Windows.Forms;

namespace AirPlane
{
    public partial class OrderForm : Form
    {
        public SqlConnection myConnection = new SqlConnection(@"Data Source=HACKERMAN;Initial Catalog=MyDataBase;Integrated Security=True");
        ParentForm parent = new AirPlane.ParentForm();
        public OrderForm()
        {
            InitializeComponent();
            textBox1_TextChanged();
            _controlButton(true);
            NewOrder();
        }

        private void _controlButton(bool btn)
        {
            button2.Enabled = btn;
            button1.Enabled = button4.Enabled = button3.Enabled = !btn;
        }

        public void Insert(string Query)
        {
            SqlCommand cmdDataBase = new SqlCommand(Query, myConnection);
            SqlDataReader myReader;
            try
            {
                myConnection.Open();
                myReader = cmdDataBase.ExecuteReader();
                while (myReader.Read())
                { }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            myConnection.Close();
        }

        public void fillCombo_(string Query, TextBox comboTemp)
        {
            SqlCommand cmdDataBase = new SqlCommand(Query, myConnection);
            SqlDataReader myReader;
            try
            {
                myConnection.Open();
                myReader = cmdDataBase.ExecuteReader();
                while (myReader.Read())
                {
                    int comboString = myReader.GetInt32(0);
                    comboTemp.Text = comboString.ToString();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            myConnection.Close();
        }
        

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        private void ClientOrder_Load(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = myConnection;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            FlightsRegard_Update_Form fly = new FlightsRegard_Update_Form();
            SetRouts route = new SetRouts();

            fly.Size = new System.Drawing.Size(1087, 582);
            fly.dataGridView3.Hide();
            fly.label13.Hide();
            fly.label20.Hide();
            fly.textBox11.Hide();
            fly.groupBox5.Hide();
            fly.button1.Hide();
            fly.button4.Hide();
            fly.button2.Hide();
            fly.groupBox2.Size = new System.Drawing.Size(436, 198);
            fly.panel1.Size = new System.Drawing.Size(1044, 470);
            fly.label21.Location = new System.Drawing.Point(72, 500);
            fly.label21.Visible = true;
            fly.label22.Location = new System.Drawing.Point(202, 500);
            fly.label22.Visible = true;
            fly.label23.Location = new System.Drawing.Point(485, 500);
            fly.label23.Visible = true;
            fly.label24.Location = new System.Drawing.Point(392, 500);
            fly.label24.Visible = true;
            fly.button3.Location = new System.Drawing.Point(815, 500);
            fly.Closebutton.Location = new System.Drawing.Point(937, 500);
            fly.dataGridView3.SelectionMode = DataGridViewSelectionMode.CellSelect;
            fly.button3.Enabled = true;
            fly.dataGridView1.ColumnHeadersVisible = true;
            fly.dataGridView2.Columns.Add("RtId", "RtId");
            fly.dataGridView2.Columns.Add(" RouteCode", "RouteCode");
            fly.dataGridView2.Columns.Add("DepCityId", "DepCityId");
            fly.dataGridView2.Columns.Add("ArrCityId", "ArrCityId");
            fly.dataGridView2.Columns.Add("DepTime", "DepTime");
            fly.dataGridView2.Columns.Add("ArrDate","DepTime");
            fly.dataGridView2.Columns.Add("ArrTime", "ArrTime");
            fly.dataGridView2.Columns.Add("FlightId", "FlightId");
            fly.ShowDialog();

            try
            {
                dataGridView2.Rows.Clear();
                textBox5.Text = fly.label22.Text;
                textBox9.Text = fly.label23.Text;
                textBox3.Text = fly.dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                textBox10.Text = fly.dataGridView2.SelectedRows[0].Cells[1].Value.ToString();
                MessageBox.Show(fly.dataGridView2.Rows.Count.ToString());
                for(int i = 0; i < fly.dataGridView2.Rows.Count - 1; i++)
                {
                    dataGridView2.Rows.Add( fly.dataGridView2.Rows[i].Cells[2].Value.ToString(),
                                            fly.dataGridView2.Rows[i].Cells[3].Value.ToString(),
                                            fly.dataGridView1.SelectedRows[0].Cells[2].Value.ToString(),
                                            fly.label22.Text.ToString(),
                                            fly.dataGridView1.SelectedRows[0].Cells[0].Value.ToString(),
                                            fly.dataGridView2.Rows[i].Cells[4].Value.ToString(),
                                            fly.dataGridView2.Rows[i].Cells[6].Value.ToString(),
                                            fly.dataGridView2.Rows[i].Cells[5].Value.ToString(),
                                            fly.dataGridView2.Rows[i].Cells[0].Value.ToString());
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message + " You should make a right selection...! ");
            }
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
                    textB.Text = route.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            myConnection.Close();
        }
        private void button8_Click(object sender, EventArgs e)
        {
            TransactionsForm transactions = new TransactionsForm();
            try
            {
                if (!string.IsNullOrEmpty(textBox5.Text) && !string.IsNullOrEmpty(textBox6.Text) && !string.IsNullOrEmpty(textBox7.Text) && !string.IsNullOrEmpty(textBox8.Text))
                {
                    Operate("SELECT Clients.CLFName +' '+ Clients.CLLName + ' '+ Clients.CLSName FROM Clients WHERE ClientId = '" + textBox8.Text + "'", transactions.textBox7);
                    transactions.textBox4.Text = textBox1.Text;
                    transactions.textBox11.Text = textBox5.Text;
                    transactions.money = double.Parse(textBox5.Text);
                    transactions.textBox8.Text = textBox6.Text;
                    transactions.textBox6.Text = textBox7.Text;
                    transactions.ShowDialog();
                }
                else { MessageBox.Show("Please, check all your entries", "Error ... !", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void Closebutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        

        private void Editbutton_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView2.Rows.Remove(dataGridView2.SelectedRows[0]);
                try
                {
                    FlightsRegard_Update_Form fly = new FlightsRegard_Update_Form();
                    fly.dataGridView3.SelectionMode = DataGridViewSelectionMode.CellSelect;
                    fly.button3.Enabled = true;
                    fly.ShowDialog();

                    textBox5.Text = fly.dataGridView3.SelectedCells[0].Value.ToString();
                    textBox9.Text = fly.dataGridView3.SelectedCells[0].OwningColumn.Name.ToString();
                    textBox3.Text = fly.textBox9.Text;
                    textBox10.Text = fly.textBox10.Text;

                    dataGridView2.Rows.Add(fly.dataGridView2.SelectedRows[0].Cells[2].Value.ToString(),
                        fly.dataGridView2.SelectedRows[0].Cells[3].Value.ToString(),
                        fly.dataGridView1.SelectedRows[0].Cells[2].Value.ToString(),
                        fly.dataGridView3.SelectedCells[0].OwningColumn.Name.ToString(),
                        fly.dataGridView1.SelectedRows[0].Cells[0].Value.ToString(),
                        fly.dataGridView2.SelectedRows[0].Cells[4].Value.ToString(),
                        fly.dataGridView2.SelectedRows[0].Cells[6].Value.ToString(),
                        fly.dataGridView2.SelectedRows[0].Cells[5].Value.ToString(),
                        fly.dataGridView2.SelectedRows[0].Cells[0].Value.ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n\tYou should make a right selection...! ");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("The Row is emply...!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void Savebutton_Click(object sender, EventArgs e)
        {
            bool btn = false;
            for (int i = 0; i < dataGridView2.Rows.Count - 1; i++)
            {
                TextBox text = new TextBox();
                TextBox text_ = new TextBox();
                fillCombo_("SELECT MAX(TransId) FROM Transactions", text);
                fillCombo_("SELECT MAX(PathId) FROM FlightPath", text_);
                try
                {
                    SqlCommand cmdDataBase = new SqlCommand("INSERT INTO Orders VALUES('" + dataGridView1.SelectedRows[0].Cells[1].Value.ToString() + "','" + textBox11.Text +
                        "','" +4 +
                        "', '" + text.Text + "', '" + dataGridView2.Rows[i].Cells[8].Value.ToString() + "','"+text_.Text+"','" + textBox8.Text + "','" + textBox9.Text + "','" + dataGridView1.SelectedRows[0].Cells[5].Value.ToString() +
                        "', '" + textBox4.Text + "')", myConnection);
                    SqlDataReader myReader;
                    try
                    {
                        myConnection.Open();
                        myReader = cmdDataBase.ExecuteReader();
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                    myConnection.Close();
                }
                catch (Exception ex) { MessageBox.Show("There is no entry or no item selected", "Error...!", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            _controlButton(!btn);
            //this.Close();
        }

        private void InvoiceButton_Click(object sender, EventArgs e)
        {
            ReportViewerForm report = new ReportViewerForm();
            report.ShowDialog();
        }

        private void textBox1_TextChanged()
        {
            string Query = "SELECT MAX (OrderId) FROM Orders;";
            SqlCommand cmdDataBase = new SqlCommand(Query, myConnection);
            SqlDataReader myReader;
            try
            {
                myConnection.Open();
                myReader = cmdDataBase.ExecuteReader();
                while (myReader.Read())
                {
                    int route = myReader.GetInt32(0) + 1;
                    textBox1.Text = route.ToString();
                }
            }
            catch (Exception ex)
            {
                textBox1.Text = "1";
            }
            myConnection.Close();
        }

        void NewOrder()
        {
            SqlDataAdapter cmdAdapter = new SqlDataAdapter("NewOSN", myConnection);
            cmdAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable tab = new DataTable();
            cmdAdapter.Fill(tab);
            textBox2.Text = tab.Rows[0][0].ToString();
        }

        string sTemp;
        public string money;
        private void button2_Click(object sender, EventArgs e)
        {
            bool btn = false;
            string str = "SELECT CLFName, CLLName , CLSName FROM Clients WHERE ClientId = '"+textBox8.Text+"'";
            SqlCommand cmdDataBase = new SqlCommand(str, myConnection);
            SqlDataReader myReader;
            try
            {
                myConnection.Open();
                myReader = cmdDataBase.ExecuteReader();
                while (myReader.Read())
                {
                    string route = myReader["CLFName"].ToString() +" "+ myReader["CLSName"].ToString()+" "+ myReader["CLLName"].ToString();
                    sTemp = route;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            myConnection.Close();
            try
            {
                if (!string.IsNullOrEmpty(comboBox1.Text) && !string.IsNullOrEmpty(textBox2.Text) && !string.IsNullOrEmpty(textBox8.Text))
                {
                    if(!string.IsNullOrEmpty(textBox5.Text))
                    {
                        dataGridView1.Rows.Add(sTemp, textBox2.Text, textBox5.Text, textBox7.Text, textBox6.Text, comboBox1.Text);
                        _controlButton(btn);
                    }
                    else { MessageBox.Show("Choose first of all the Flight ... !", "Booking Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk); }
                }
                else if(string.IsNullOrEmpty(comboBox1.Text))
                {
                    _controlButton(!btn);
                    MessageBox.Show("Please enter the Order Status ...!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else if(string.IsNullOrEmpty(textBox2.Text))
                {
                    _controlButton(!btn);
                    MessageBox.Show("Please enter the Order Serial Number ...!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else if(string.IsNullOrEmpty(textBox8.Text))
                {
                    _controlButton(!btn);
                    MessageBox.Show("Please enter the Client Id...!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            SetClientForm client = new SetClientForm();
            client.Text = " Client booking card";
            client.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool btn = false;
            try
            {
                dataGridView1.Rows.Remove(dataGridView1.SelectedRows[0]);
                _controlButton(!btn);
            }
            catch(Exception ex)
            {
                MessageBox.Show("The Row is emply...!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}