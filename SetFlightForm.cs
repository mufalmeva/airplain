using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AirPlane
{
    public partial class SetFlightForm : Form
    {
        SqlConnection myConnection = new SqlConnection(@"Data Source=HACKERMAN;Initial Catalog=MyDataBase;Integrated Security=True");

        public string _text
        { get; set; }

        public SetFlightForm()
        {
            InitializeComponent();
            bool btn = true;
            _ButtonControl(btn);
            NewFlight();
        }

        void fillCombo(string Query, ComboBox comboTemp)
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
                    comboTemp.Items.Add(comboString);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            myConnection.Close();
        }

        private void AddRbutton7_Click(object sender, EventArgs e)
        {
            
            try
            {
                dataGridView2.Rows.Add(textBox7.Text, textBox6.Text, textBox2.Text, dateTimePicker3.Text, dateTimePicker2.Text, dateTimePicker4.Text, textBox9.Text, textBox8.Text, textBox4.Text, textBox5.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            textBox7.ResetText();
            textBox6.ResetText();
            textBox2.ResetText();
            dateTimePicker3.ResetText();
            dateTimePicker4.ResetText();
            dateTimePicker2.ResetText();
            textBox8.ResetText();
            textBox4.ResetText();
            textBox5.ResetText();
        }

        private void buttonAddFlight_Click(object sender, EventArgs e)
        {
            
            bool btn = false;
            try
            {
                dataGridView1.Rows.Add(textBox1.Text, dateTimePicker1.Text, textBox3.Text, comboBox3.Text, comboBox1.Text);
                _ButtonControl(btn);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            textBox1.ResetText();
            dateTimePicker1.ResetText();
            textBox3.Clear();
            comboBox3.ResetText();
            comboBox1.ResetText();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            HomeForm fly = new HomeForm();
            fly.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            CompanySearchForm comp = new CompanySearchForm();
            comp.Show();
        }

        private void Closebutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _ButtonControl(bool btn)
        {
            buttonAddFlight.Enabled = btn;
            Resetbutton1.Enabled = !btn;
            buttonFDelete.Enabled = AddRbutton7.Enabled = button1.Enabled = UpdateRbutton8.Enabled = DeleltRbutton2.Enabled = button4.Enabled = button2.Enabled = button10.Enabled = button7.Enabled = button9.Enabled = button8.Enabled = !btn;
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                bool btn = false;
                _ButtonControl(btn);

                textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                dateTimePicker1.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                textBox3.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                comboBox3.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                comboBox1.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Resetbutton1_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show("Index = " + index);
                dataGridView1.Rows[index].Cells[0].Value = textBox1.Text;
                dataGridView1.Rows[index].Cells[1].Value = dateTimePicker1.Text;
                dataGridView1.Rows[index].Cells[2].Value = textBox3.Text;
                dataGridView1.Rows[index].Cells[3].Value = comboBox3.Text;
                dataGridView1.Rows[index].Cells[4].Value = comboBox1.Text;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            textBox1.ResetText();
            dateTimePicker1.ResetText();
            textBox3.Clear();
            comboBox3.ResetText();
            comboBox1.ResetText();
        }

        private void buttonFDelete_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Remove(dataGridView1.SelectedRows[0]);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool btn = true;
            _ButtonControl(btn);
        }

        private void DeleltRbutton2_Click(object sender, EventArgs e)
        {
            dataGridView2.Rows.Remove(dataGridView2.SelectedRows[0]);
        }

        public int index
        { get; set; }
        public int _index
        { get; set; }
        public int _index_
        { get; set; }
        private void UpdateRbutton8_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show("Index = " + _index);
                dataGridView2.Rows[_index].Cells[0].Value = textBox7.Text;
                dataGridView2.Rows[_index].Cells[1].Value = textBox6.Text;
                dataGridView2.Rows[_index].Cells[2].Value = textBox2.Text;
                dataGridView2.Rows[_index].Cells[3].Value = dateTimePicker3.Text;
                dataGridView2.Rows[_index].Cells[4].Value = dateTimePicker2.Text;
                dataGridView2.Rows[_index].Cells[5].Value = dateTimePicker4.Text;
                dataGridView2.Rows[_index].Cells[6].Value = textBox9.Text;
                textBox9.Enabled = true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
            textBox7.ResetText();
            textBox6.ResetText();
            textBox2.ResetText();
            dateTimePicker3.ResetText();
            dateTimePicker4.ResetText();
            dateTimePicker2.ResetText();
            textBox8.ResetText();
            textBox5.ResetText();
            textBox4.ResetText();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            index = e.RowIndex;
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            _index = e.RowIndex;
            textBox9.Enabled = false;
        }

        private void dataGridView2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                textBox7.Text = dataGridView2.SelectedRows[0].Cells[0].Value.ToString();
                textBox6.Text = dataGridView2.SelectedRows[0].Cells[1].Value.ToString();
                textBox2.Text = dataGridView2.SelectedRows[0].Cells[2].Value.ToString();
                dateTimePicker3.Text = dataGridView2.SelectedRows[0].Cells[3].Value.ToString();
                dateTimePicker2.Text = dataGridView2.SelectedRows[0].Cells[4].Value.ToString();
                dateTimePicker4.Text = dataGridView2.SelectedRows[0].Cells[5].Value.ToString();
                textBox9.Text = dataGridView2.SelectedRows[0].Cells[6].Value.ToString();
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void dataGridView3_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                textBox10.Text = dataGridView3.SelectedRows[0].Cells[0].Value.ToString();
                textBox8.Text = dataGridView3.SelectedRows[0].Cells[1].Value.ToString();
                textBox4.Text = dataGridView3.SelectedRows[0].Cells[2].Value.ToString();
                textBox5.Text = dataGridView3.SelectedRows[0].Cells[3].Value.ToString();
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView3.Rows.Add(textBox10.Text, textBox8.Text, textBox4.Text, textBox5.Text, dataGridView1.Rows[0].Cells[3].Value.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
            textBox8.ResetText();
            textBox4.ResetText();
            textBox5.ResetText();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            bool btn = false;
            string _text = "SELECT MAX (FlightId) FROM Flights";
            string str = "INSERT INTO Flights (FSN, DepartureDate, CompanyId, AircraftId, FlightStatus) VALUES ('"+ dataGridView1.SelectedRows[0].Cells[0].Value.ToString()+ "', '"+ dataGridView1.SelectedRows[0].Cells[1].Value.ToString() +
                "', '"+ dataGridView1.SelectedRows[0].Cells[2].Value.ToString() + "', '"+ dataGridView1.SelectedRows[0].Cells[3].Value.ToString() +
                "', '"+ dataGridView1.SelectedRows[0].Cells[4].Value.ToString() + "');";
            try
            {
                _sSave(str);
                _ButtonControl(btn);
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            try
            {
                SqlCommand cmdDataBase = new SqlCommand(_text, myConnection);
                SqlDataReader myReader;
                myConnection.Open();
                myReader = cmdDataBase.ExecuteReader();
                while (myReader.Read())
                {
                    int comboString = myReader.GetInt32(0);
                    textBox9.Text = comboString.ToString();
                }
            }catch(Exception ex) { MessageBox.Show(ex.Message); }
                myConnection.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string _text = "SELECT MAX (RtId) FROM Route";
            string _str = "INSERT INTO Route(RouteCode, DepCityId, ArrCityId, DepTime, ArrDate, ArrTime, FlightId) VALUES('"+ dataGridView2.SelectedRows[0].Cells[0].Value.ToString() + 
                "', '"+ dataGridView2.SelectedRows[0].Cells[1].Value.ToString() + "', '"+ dataGridView2.SelectedRows[0].Cells[2].Value.ToString() + 
                "', '"+ dataGridView2.SelectedRows[0].Cells[3].Value.ToString() + "', '"+ dataGridView2.SelectedRows[0].Cells[4].Value.ToString() + 
                "', '"+ dataGridView2.SelectedRows[0].Cells[5].Value.ToString() + "', '"+ dataGridView2.SelectedRows[0].Cells[6].Value.ToString() + "')";
            try
            {
                _sSave(_str);
                SqlCommand cmdDataBase = new SqlCommand(_text, myConnection);
                SqlDataReader myReader;
                myConnection.Open();
                myReader = cmdDataBase.ExecuteReader();
                while (myReader.Read())
                {
                    int comboString = myReader.GetInt32(0);
                    textBox10.Text = comboString.ToString();
                }
                myConnection.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView3.Rows[_index_].Cells[0].Value = textBox10.Text;
                dataGridView3.Rows[_index_].Cells[1].Value = textBox8.Text;
                dataGridView3.Rows[_index_].Cells[2].Value = textBox4.Text;
                dataGridView3.Rows[_index_].Cells[3].Value = textBox5.Text;
                dataGridView3.Rows[_index_].Cells[4].Value = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            textBox10.ResetText();
            textBox8.ResetText();
            textBox5.ResetText();
            textBox4.ResetText();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string _strClass = "INSERT INTO Class (RtId, FC, BC, EC, AircraftId) VALUES('"+ dataGridView3.SelectedRows[0].Cells[0].Value.ToString() +
                "', '" + dataGridView3.SelectedRows[0].Cells[1].Value.ToString() + "','" + dataGridView3.SelectedRows[0].Cells[2].Value.ToString() + 
                "','" + dataGridView3.SelectedRows[0].Cells[3].Value.ToString() + "', '"+dataGridView3.SelectedRows[0].Cells[4].Value.ToString()+"')";
            try
            {
                _sSave(_strClass);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FlightsRegard_Update_Form fly = new FlightsRegard_Update_Form();
            fly.ShowDialog();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            FlightsRegard_Update_Form fly = new FlightsRegard_Update_Form();
            fly.ShowDialog();
        }

        private void textBox3_TextChanged_1(object sender, EventArgs e)
        {
            string fillStr = "SELECT AircraftId FROM Aircrafts WHERE CompanyId = '" + textBox3.Text + "'";
            if (textBox3.Text != "")
            {
                fillCombo(fillStr, comboBox3);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void tex1()
        {
            try
            {
                SqlCommand cmdDataBase = new SqlCommand("SELECT FSN FROM Flights WHERE FlightId = '"+_text+"'", myConnection);
                SqlDataReader myReader;
                myConnection.Open();
                myReader = cmdDataBase.ExecuteReader();
                while (myReader.Read())
                {
                    string comboString = myReader.GetString(0);
                    textBox1.Text = comboString.ToString();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            myConnection.Close();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView3.Rows.Remove(dataGridView3.SelectedRows[0]);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void NewFlight()
        {
            SqlDataAdapter cmdAdapter = new SqlDataAdapter("[dbo].[NewFSN]", myConnection);
            cmdAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable tab = new DataTable();
            cmdAdapter.Fill(tab);
            textBox1.Text = tab.Rows[0][0].ToString();
        }
    }
}
