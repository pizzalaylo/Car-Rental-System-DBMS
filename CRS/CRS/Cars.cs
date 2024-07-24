using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRS
{
    public partial class Cars : Form
    {
        private BindingSource bindingSource1 = new BindingSource();
        public Cars()
        {
            InitializeComponent();

            //setting data source for grid view
            setBindindSource("SELECT * from car");

        }

        

        private void setBindindSource(string queryString)
        {
            string query = queryString;
            using (SqlConnection connection = Globals.createSQLConnection())
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;

                DataTable table = new DataTable();
                table.Locale = System.Globalization.CultureInfo.InvariantCulture;
                adapter.Fill(table);
                bindingSource1.DataSource = table;
                dataGridView1.DataSource = bindingSource1;
            }
        }

        private void row_select(object sender, DataGridViewCellMouseEventArgs e)
        {

            if(dataGridView1[0, e.RowIndex].Value.ToString() != "")
            {
                textBox1.Text = dataGridView1[0, e.RowIndex].Value.ToString();
                textBox3.Text = (string)dataGridView1[1, e.RowIndex].Value;
                textBox4.Text = (string)dataGridView1[2, e.RowIndex].Value;
                comboBox2.SelectedItem = (string)dataGridView1[3, e.RowIndex].Value;
                textBox2.Text = (string)dataGridView1[4, e.RowIndex].Value;
                comboBox3.SelectedItem = (string)dataGridView1[5, e.RowIndex].Value;
                textBox8.Text = (string)dataGridView1[6, e.RowIndex].Value;
                textBox5.Text = (string)dataGridView1[7, e.RowIndex].Value.ToString();
            }
            

        }

        
        private void clearAllValues()
        {
            textBox1.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            comboBox2.SelectedItem = null;
            textBox2.Text = "";
            comboBox3.SelectedItem = null;
            textBox8.Text = "";
            textBox5.Text = "";
        }


        
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }




        private void button1_Click_1(object sender, EventArgs e)
        {
            if (textBox7.Text != "")
            {
                setBindindSource($"SELECT * from car where model like '%{textBox7.Text.ToLower()}%'");
            }
            else
            {
                MessageBox.Show("Empty Text Field!");
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            string query = $"delete from car where car_id={textBox1.Text}";
            using (SqlConnection connection = Globals.createSQLConnection())
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    command.ExecuteNonQuery();
                    MessageBox.Show("Car Deleted successfully!");
                    setBindindSource("SELECT * from car");
                    clearAllValues();

                }
                catch (SqlException err)
                {
                    Console.WriteLine(err.Message);
                }
            }
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            clearAllValues();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            string query = $"update car set make='{textBox3.Text}', model='{textBox4.Text}', model_type='{comboBox2.SelectedItem}', model_year='{textBox2.Text}', condition='{comboBox3.SelectedItem}', color='{textBox8.Text}', price_per_day={textBox5.Text} where car_id={textBox1.Text}";
            using (SqlConnection connection = Globals.createSQLConnection())
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    command.ExecuteNonQuery();
                    MessageBox.Show("Car Edited successfully!");
                    setBindindSource("SELECT * from car");
                    clearAllValues();

                }
                catch (SqlException err)
                {
                    Console.WriteLine(err.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                string query = $"insert into car values('{textBox3.Text}', '{textBox4.Text}','{comboBox2.SelectedItem}', '{textBox2.Text}', '{comboBox3.SelectedItem}', '{textBox8.Text}', {textBox5.Text})";
                using (SqlConnection connection = Globals.createSQLConnection())
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);

                    try
                    {
                        command.ExecuteNonQuery();
                        MessageBox.Show("Car Added successfully!");
                        setBindindSource("SELECT * from car");

                    }
                    catch (SqlException err)
                    {
                        Console.WriteLine(err.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Car Already Exists!");
                clearAllValues();
            }
        }
    }
}
