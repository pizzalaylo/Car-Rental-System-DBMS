using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CRS
{
    public partial class RentCars : Form
    {
        private string username;
        private BindingSource bindingSource1 = new BindingSource();
        private int price;
        public RentCars(string username)
        {
            InitializeComponent();
            this.username = username;
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
            if (dataGridView1[0, e.RowIndex].Value.ToString() != "")
            {
                textBox1.Text = dataGridView1[0, e.RowIndex].Value.ToString();
                textBox3.Text = (string)dataGridView1[1, e.RowIndex].Value;
                textBox4.Text = (string)dataGridView1[2, e.RowIndex].Value;
                dateTimePicker1.Value.ToString("yyyy-MM-dd");
                this.price = (int) dataGridView1[7, e.RowIndex].Value;
            }
        }


        

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox7.Text != "")
            {
                if (comboBox1.SelectedItem?.ToString() == "Price - High to Low")
                {
                    setBindindSource($"SELECT * from car where model like '%{textBox7.Text.ToLower()}%' order by price_per_day desc");
                }
                else
                {
                    setBindindSource($"SELECT * from car where model like '%{textBox7.Text.ToLower()}%' order by price_per_day");
                }

            }
            else
            {
                if (comboBox1.SelectedItem?.ToString() == "Price - High to Low")
                {
                    setBindindSource("SELECT * from car order by price_per_day desc");
                }
                else
                {
                    setBindindSource("SELECT * from car order by price_per_day");
                }

            }
        }

        private void dateTimePicker1_ValueChanged_1(object sender, EventArgs e)
        {
            int total_price = (int)dateTimePicker2.Value.Subtract(dateTimePicker1.Value).TotalDays;
            total_price *= price;
            textBox5.Text = total_price.ToString();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            int total_price = (int)dateTimePicker2.Value.Subtract(dateTimePicker1.Value).TotalDays;
            total_price *= price;
            string query = $"insert into reservation values({textBox1.Text}, '{textBox2.Text}','{textBox3.Text}', '{textBox4.Text}', '{dateTimePicker1.Value.ToString("yyyy-MM-dd")}', '{dateTimePicker2.Value.ToString("yyyy-MM-dd")}', {total_price}, '{username}')";
            using (SqlConnection connection = Globals.createSQLConnection())
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                string query1 = $"SELECT booking_id from reservation where car_id={textBox1.Text} and customer_name='{textBox2.Text}' and make='{textBox3.Text}' and model='{textBox4.Text}' and pick_up_date='{dateTimePicker1.Value.ToString("yyyy-MM-dd")}' and drop_date='{dateTimePicker2.Value.ToString("yyyy-MM-dd")}' and total_price={total_price} and username='{username}'";
                SqlCommand getBookCommand = new SqlCommand(query1, connection);

                try
                {
                    command.ExecuteNonQuery();
                    SqlDataReader data = getBookCommand.ExecuteReader();
                    data.Read();
                    MessageBox.Show($"Reservation has been made, Your Booking ID is {data["booking_id"]}");


                }
                catch (SqlException err)
                {
                    Console.WriteLine(err.Message);
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox2.Text = "";
            textBox5.Text = "";
        }
    }
}
