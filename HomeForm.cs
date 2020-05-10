using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows.Forms;

namespace AirPlane
{
    public partial class HomeForm : Form
    {
        public SqlConnection myConnection = new SqlConnection(@"Data Source=HACKERMAN;Initial Catalog=MyDataBase;Integrated Security=True");
        public HomeForm()
        {
            InitializeComponent();            
            ShowFlights();
        }

        DataTable dbdatatableset;

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
                    string comboString = myReader.GetString(0);
                    comboTemp.Items.Add(comboString);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            myConnection.Close();
        }

        public void Delete(string Query)
        {
            SqlCommand cmdDataBase = new SqlCommand(Query, myConnection);
            SqlDataReader myReader;
            try
            {
                myConnection.Open();
                myReader = cmdDataBase.ExecuteReader();
                MessageBox.Show("Элемент успешно удален из базы данных");
                while (myReader.Read())
                { }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            myConnection.Close();
        }

        private void ShowFlights()
        {
            myConnection.Open();
            SqlCommand cmdDataBase = new SqlCommand("SELECT DISTINCT dbo.Flights.FlightId, dbo.Flights.FSN, dbo.Code.CityName, dbo.Flights.DepartureDate, dbo.Flights.AircraftId, dbo.Flights.FlightStatus FROM dbo.Route INNER JOIN dbo.Flights ON dbo.Route.FlightId = dbo.Flights.FlightId INNER JOIN dbo.Code ON dbo.Code.CityId = dbo.Route.ArrCityId;", myConnection);
            try
            {
                SqlDataAdapter sd = new SqlDataAdapter();
                sd.SelectCommand = cmdDataBase;
                dbdatatableset = new DataTable();
                sd.Fill(dbdatatableset);
                BindingSource bSource = new BindingSource();

                bSource.DataSource = dbdatatableset;
                homedataGridView1.DataSource = bSource;
                sd.Update(dbdatatableset);
            }
            catch (Exception ex) { MessageBox.Show("\n The is no any set flight ... !"); }
            myConnection.Close();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (Char.IsDigit(ch)) { e.Handled = true; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ShowFlights();
        }

        DataView dbview;
        public bool dateChecked
        { get; set; }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                dbview = new DataView(dbdatatableset);
                dbview.RowFilter = string.Format("CONVERT(DepartureDate, System.String) LIKE '%{0}%'", dateTimePicker1.Text);
                homedataGridView1.DataSource = dbview;
                dateChecked = true;
                
            }catch(Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataView dbview = new DataView(dbdatatableset);
                dbview.RowFilter = string.Format("CityName LIKE '%{0}%'", textBox2.Text);
                homedataGridView1.DataSource = dbview;
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
        }
        
        OrderForm ord = new OrderForm();

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
