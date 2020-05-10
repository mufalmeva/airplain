using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Collections;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AirPlane
{
    public partial class SetRouts : Form
    {
        SqlConnection myConnection = new SqlConnection(@"Data Source=HACKERMAN;Initial Catalog=MyDataBase;Integrated Security=True");
        DataTable dbTableSet;
        SqlDataAdapter sda;
        public SetRouts()
        {
            InitializeComponent();
            fillTreeview_();
        }
        
        private ArrayList customerArray = new ArrayList();


        void fillTreeview_()
        {
            string search = "SELECT DISTINCT RouteCode FROM Route";
            SqlCommand cmdDataBase = new SqlCommand(search, myConnection);
            SqlDataReader myReader;
            try
            {
                myConnection.Open();
                myReader = cmdDataBase.ExecuteReader();
                while (myReader.Read())
                {
                    string strin = myReader.GetString(0);
                    customerArray.Add(strin);
                    treeView1.Nodes.Add(new TreeNode(strin));
                }
                myConnection.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message);
                myConnection.Close(); }
            
            try
            {
                foreach (TreeNode str in treeView1.Nodes)
                {
                    string find = "SELECT CityCode from Code;";
                    SqlCommand cmd = new SqlCommand(find, myConnection);
                    SqlDataReader reader;
                    try
                    {
                        myConnection.Open();
                        reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            string dep = reader.GetString(0);
                            str.Nodes.Add(new TreeNode(dep));
                        }
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                    myConnection.Close();
                }
            }catch(Exception ex) { MessageBox.Show(ex.Message); }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            List<TreeNode> node = new List<TreeNode>();

            TreeNodeCollection collect;

            foreach(TreeNode str in treeView1.Nodes)
            {
                if(str.Checked)
                {
                    collect = str.Nodes;
                    str.ExpandAll();
                    label3.Text = str.Text;

                    foreach (TreeNode nos in collect)
                    {
                        if(nos.Checked)
                        {
                            listBox1.Items.Add(nos.Text);
                        }
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            listBox1.Items.Remove(listBox1.SelectedItem);
        }

        public int temp
        {
            get; set;
        }

        public string strC
        {
            get; set;
        }

        FlightsRegard_Update_Form fly = new FlightsRegard_Update_Form();
        

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                ArrayList list = new ArrayList();
                int count = 0;
                foreach (string str in listBox1.Items)
                {
                    list.Add(str);
                    count++;
                }
                string path = "";
                path = path + list[0];
                for (int i = 1; i < count; i++)
                {
                    path = path + " - " + list[i];
                }
                textBox1.Text = path;

                ArrayList array = new ArrayList();

                int count_ = 0;
                foreach (string str in listBox1.Items)
                {
                    array.Add(str);
                    count_++;
                }

                int money = 0;
                for (int i = 0; i + 1 < count_; i++)
                {
                    string insert = "SELECT MyDataBase.dbo.Route.RtId FROM MyDataBase.dbo.Route WHERE EXISTS(SELECT DepCities.CityCode FROM MyDataBase.dbo.Code AS DepCities WHERE DepCities.CityId = MyDataBase.dbo.Route.DepCityId AND DepCities.CityCode = '" + array[i].ToString() + "' AND EXISTS(SELECT ArrCities.CityCode FROM MyDataBase.dbo.Code AS ArrCities WHERE ArrCities.CityId = MyDataBase.dbo.Route.ArrCityId AND ArrCities.CityCode = '" + array[i + 1].ToString() + "')); ";
                    SqlCommand cmdDataBase = new SqlCommand(insert, myConnection);
                    SqlDataReader myReader;
                    try
                    {
                        myConnection.Open();
                        myReader = cmdDataBase.ExecuteReader();
                        while (myReader.Read())
                        {
                            temp = myReader.GetInt32(0);
                        }
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                    myConnection.Close();

                    if (radioButton1.Checked == true)
                    {
                        string temp_ = "SELECT FC FROM FCT WHERE FCT.RtId = '" + temp + "'; ";
                        string _insert = temp_;
                        SqlCommand cmd_ = new SqlCommand(_insert, myConnection);
                        SqlDataReader _reader;
                        try
                        {
                            myConnection.Open();
                            _reader = cmd_.ExecuteReader();
                            while (_reader.Read())
                            {
                                money = money + _reader.GetInt32(0);
                                textBox2.Text = money.ToString();
                            }
                        }
                        catch (Exception ex) { MessageBox.Show(ex.Message); }
                        myConnection.Close();
                    }

                    else if (radioButton2.Checked == true)
                    {
                        string temp_ = "SELECT BC FROM BCT WHERE BCT.RtId = '" + temp + "'; ";
                        string _insert = temp_;
                        SqlCommand cmd_ = new SqlCommand(_insert, myConnection);
                        SqlDataReader _reader;
                        try
                        {
                            myConnection.Open();
                            _reader = cmd_.ExecuteReader();
                            while (_reader.Read())
                            {
                                money = money + _reader.GetInt32(0);
                                textBox2.Text = money.ToString();
                            }
                        }
                        catch (Exception ex) { MessageBox.Show(ex.Message); }
                        myConnection.Close();
                    }

                    else if (radioButton3.Checked == true)
                    {
                        string temp_ = "SELECT EC FROM ECT WHERE ECT.RtId = '" + temp + "'; ";
                        string _insert = temp_;
                        SqlCommand cmd_ = new SqlCommand(_insert, myConnection);
                        SqlDataReader _reader;
                        try
                        {
                            myConnection.Open();
                            _reader = cmd_.ExecuteReader();
                            while (_reader.Read())
                            {
                                money = money + _reader.GetInt32(0);
                                textBox2.Text = money.ToString();
                            }
                        }
                        catch (Exception ex) { MessageBox.Show(ex.Message); }
                        myConnection.Close();
                    }

                    else
                    {
                        MessageBox.Show("You didn't choose a Class!\n Please! Make sure you've choosen a Class", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Make sure you've choosen a opion!","", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            string insert = "INSERT INTO FlightPath (Path, RouteCode) VALUES('"+textBox1.Text+"', '"+label3.Text+"');";
            SqlCommand cmdDataBase = new SqlCommand(insert, myConnection);
            SqlDataReader myReader;
            try
            {
                myConnection.Open();
                myReader = cmdDataBase.ExecuteReader();
                while (myReader.Read())
                { }
            }
            catch (Exception ex) {  MessageBox.Show(ex.Message); }
            myConnection.Close();
            this.Close();
        }
    }
}
