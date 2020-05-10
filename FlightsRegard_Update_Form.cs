using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AirPlane
{
    public partial class FlightsRegard_Update_Form : Form
    {
        SqlConnection myConnection = new SqlConnection(@"Data Source=HACKERMAN;Initial Catalog=MyDataBase;Integrated Security=True");
        DataTable dbTableSet;
        SqlDataAdapter sda;

        public FlightsRegard_Update_Form()
        {
            InitializeComponent();
            _loadTable_Flight();
        }

        private void Closebutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                dataGridView2.Columns.Clear();
                textBox9.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                textBox1.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                dateTimePicker1.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                comboBox3.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                comboBox1.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();

                _loadTable_Route();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void dataGridView2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                textBox10.Text = dataGridView2.SelectedRows[0].Cells[0].Value.ToString();
                textBox7.Text = dataGridView2.SelectedRows[0].Cells[1].Value.ToString();
                textBox6.Text = dataGridView2.SelectedRows[0].Cells[2].Value.ToString();
                textBox2.Text = dataGridView2.SelectedRows[0].Cells[3].Value.ToString();
                dateTimePicker5.Text = dataGridView2.SelectedRows[0].Cells[4].Value.ToString();
                dateTimePicker3.Text = dataGridView2.SelectedRows[0].Cells[5].Value.ToString();
                dateTimePicker2.Text = dataGridView2.SelectedRows[0].Cells[6].Value.ToString();
                dateTimePicker4.Text = dataGridView2.SelectedRows[0].Cells[7].Value.ToString();
                textBox9.Text = dataGridView2.SelectedRows[0].Cells[8].Value.ToString();
                _loadTable_Class();
            }
            catch (Exception ex)
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

        private void _loadTable_Flight()
        {
            myConnection.Open();
            SqlCommand cmdDataBase = new SqlCommand("SELECT * FROM Flights", myConnection);
            try
            {
                sda = new SqlDataAdapter();
                sda.SelectCommand = cmdDataBase;
                dbTableSet = new DataTable();
                sda.Fill(dbTableSet);
                BindingSource bSource = new BindingSource();

                bSource.DataSource = dbTableSet;
                dataGridView1.DataSource = bSource;
                sda.Update(dbTableSet);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            myConnection.Close();
        }
        private void _loadTable_Route()
        {
            myConnection.Open();
            SqlCommand cmdDataBase = new SqlCommand("SELECT * FROM Route WHERE FlightId = '" + textBox9.Text + "';", myConnection);
            try
            {
                sda = new SqlDataAdapter();
                sda.SelectCommand = cmdDataBase;
                dbTableSet = new DataTable();
                sda.Fill(dbTableSet);
                BindingSource bSource = new BindingSource();

                bSource.DataSource = dbTableSet;
                dataGridView2.DataSource = bSource;
                sda.Update(dbTableSet);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            myConnection.Close();
        }

        private void _loadTable_Class()
        {
            myConnection.Open();
            SqlCommand cmdDataBase = new SqlCommand("SELECT * FROM Class WHERE RtId = '" + textBox10.Text + "'", myConnection);
            try
            {
                sda = new SqlDataAdapter();
                sda.SelectCommand = cmdDataBase;
                dbTableSet = new DataTable();
                sda.Fill(dbTableSet);
                BindingSource bSource = new BindingSource();

                bSource.DataSource = dbTableSet;
                dataGridView3.DataSource = bSource;
                sda.Update(dbTableSet);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            myConnection.Close();
        }


        private void buttonFDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string del = "DELETE FROM Flights WHERE FlightId = '" + dataGridView1.SelectedRows[0].Cells[0].Value + "'";
                _sSave(del);
                _loadTable_Flight();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " \n\tThe table is empty... !", "Query Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void Resetbutton1_Click(object sender, EventArgs e)
        {
            string str_1 = "UPDATE Flights SET FSN = '" + textBox1.Text + "', DepartureDate= '" + dateTimePicker1.Text + "', AircraftId = '" + comboBox3.Text + "', FlightStatus = '" + comboBox1.Text + "' WHERE FlightId = '" + textBox9.Text + "'";
            _sSave(str_1);
            _loadTable_Flight();
        }

        private void dataGridView3_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                textBox11.Text = dataGridView3.SelectedRows[0].Cells[0].Value.ToString();
                textBox10.Text = dataGridView3.SelectedRows[0].Cells[1].Value.ToString();
                textBox8.Text = dataGridView3.Rows[0].Cells[2].Value.ToString();
                textBox4.Text = dataGridView3.SelectedRows[0].Cells[3].Value.ToString();
                textBox5.Text = dataGridView3.SelectedRows[0].Cells[4].Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string _strClass = "UPDATE Class SET FC = '" + textBox8.Text + "', BC = '" + textBox4.Text + "', EC = '" + textBox5.Text + "' WHERE ClassId = '" + textBox11.Text + "'";
            _sSave(_strClass);
            _loadTable_Class();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string _str_ = "INSERT INTO Class(RtId, FC,BC, EC, AircraftId) VALUES ('" + textBox10.Text + "','" + textBox8.Text + "','" + textBox4.Text + "','" + textBox5.Text + "', '" + dataGridView1.SelectedRows[0].Cells[3].Value.ToString() + "')";
            _sSave(_str_);
            _loadTable_Class();
        }

        private void AddRbutton7_Click(object sender, EventArgs e)
        {
            string Query = "INSERT INTO Route(RouteCode, DepCityId, ArrCityId,DepDate, DepTime, ArrDate, ArrTime, FlightId) VALUES('" + textBox7.Text + "', '" + textBox6.Text + "', '" + textBox2.Text + "','"+ dateTimePicker5.Text+"', '" + dateTimePicker3.Text + "', '" + dateTimePicker2.Text + "', '" + dateTimePicker4.Text + "', '" + textBox9.Text + "')";
            _sSave(Query);
            _loadTable_Route();
        }

        private void UpdateRbutton8_Click(object sender, EventArgs e)
        {
            string _str_1 = "UPDATE Route SET RouteCode = '" + textBox7.Text + "', DepCityId = '" + textBox6.Text + "', ArrCityId = '" + textBox2.Text + "', DepDate = '"+dateTimePicker5.Text+"', DepTime = '" + dateTimePicker3.Text + "', ArrDate = '" + dateTimePicker2.Text + "', ArrTime = '" + dateTimePicker4.Text + "',  FlightId ='" + textBox9.Text + "' WHERE RtId = '" + textBox10.Text + "'";
            _sSave(_str_1);
            _loadTable_Route();
        }

        private void DeleltRbutton2_Click(object sender, EventArgs e)
        {
            string del = "DELETE FROM Route WHERE RtId = '" + textBox10.Text + "'";
            _sSave(del);
            _loadTable_Route();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message + " You should make a right selection ... ! "); }
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView3_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // MessageBox.Show(dataGridView3.SelectedCells[0].OwningColumn.Name.ToString());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string del = "DELETE FROM Class WHERE ClassId = '" + dataGridView3.SelectedRows[0].Cells[0].Value + "'";
            _sSave(del);
            _loadTable_Class();

        }

        ArrayList list;
        
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView2.DataSource = null;
                dataGridView2.Columns.Clear();
                dataGridView2.Update();
                dataGridView2.Columns.Add("RtId", "RtId");
                dataGridView2.Columns.Add(" RouteCode", "RouteCode");
                dataGridView2.Columns.Add("DepCityId", "DepCityId");
                dataGridView2.Columns.Add("ArrCityId", "ArrCityId");
                dataGridView2.Columns.Add("DepDate", "DepDate");
                dataGridView2.Columns.Add("DepTime", "DepTime");
                dataGridView2.Columns.Add("ArrDate", "ArrDate");
                dataGridView2.Columns.Add("ArrTime", "ArrTime");
                dataGridView2.Columns.Add("FlightId", "FlightId");
                list = new ArrayList();
                SetRouts rout = new SetRouts();
                rout.ShowDialog();
                label22.Text = rout.textBox2.Text;

                if (rout.radioButton1.Checked)
                {
                    label23.Text = "FC";
                }
                else if (rout.radioButton2.Checked)
                {
                    label23.Text = "BC";
                }
                else if (rout.radioButton3.Checked)
                {
                    label23.Text = "EC";
                }
                int count = 0;
                foreach (string str in rout.listBox1.Items)
                {
                    list.Add(str);
                    count++;
                }
                for (int i = 0; i < count - 1; i++)
                {
                    string insert = "SELECT DISTINCT * FROM MyDataBase.dbo.Route WHERE EXISTS(SELECT DepCities.CityCode FROM MyDataBase.dbo.Code AS DepCities WHERE DepCities.CityCode = '" + list[i].ToString() + "' AND DepCities.CityId = MyDataBase.dbo.Route.DepCityId); ";
                    SqlCommand cmdDataBase = new SqlCommand(insert, myConnection);
                    SqlDataReader myReader;
                    try
                    {
                        myConnection.Open();
                        myReader = cmdDataBase.ExecuteReader();
                        while (myReader.Read())
                        {
                            dataGridView2.Rows.Add(myReader["RtId"].ToString(), myReader["RouteCode"].ToString(), myReader["DepCityId"].ToString(), myReader["ArrCityId"].ToString(), myReader["DepDate"].ToString(), myReader["DepTime"].ToString(), myReader["ArrDate"].ToString(), myReader["ArrTime"].ToString(), myReader["FlightId"].ToString());
                        }
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                    myConnection.Close();
                }
            }
            catch(Exception ex) { MessageBox.Show(ex.Message);}           
        }
    }
}
