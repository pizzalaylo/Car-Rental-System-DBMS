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
    public partial class Reservations : Form
    {
        private string userType;
        private string username;
        private BindingSource bindingSource1 = new BindingSource();
        private int booking = 0;
        public Reservations(string userType, string username)
        {
            InitializeComponent();
            this.username = username;
            this.userType = userType;
            if(userType=="admin")
            {
                setBindindSource("SELECT * FROM reservation");
                button4.Enabled = false;
            }
            else
            {
                //customer will only be shown reservations made from their account
                setBindindSource($"SELECT * FROM reservation WHERE username='{username}'");
            }
            
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

       

       

        
        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridView1[0, e.RowIndex].Value.ToString() != "")
            {
                this.booking = (int)dataGridView1[0, e.RowIndex].Value;
                
            }
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            if (textBox7.Text != "")
            {
                if (comboBox1.SelectedItem?.ToString() == "Booking ID")
                {
                    setBindindSource($"SELECT * from reservation where booking_id={textBox7.Text}");
                }
                else if (comboBox1.SelectedItem?.ToString() == "Customer Name")
                {
                    setBindindSource($"SELECT * from reservation where customer_name='{textBox7.Text}'");
                }
                else
                {
                    MessageBox.Show("Please select search criteria from the dropdown!");
                }

            }
            else
            {
                MessageBox.Show("Please enter something in the search Textbox!");

            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.booking != 0)
            {
                string query = $"delete from reservation where booking_id={this.booking}";
                using (SqlConnection connection = Globals.createSQLConnection())
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);

                    try
                    {
                        command.ExecuteNonQuery();
                        MessageBox.Show("Reservation Deleted successfully!");
                        if (this.userType == "admin")
                        {
                            setBindindSource("SELECT * from reservation");
                        }
                        else
                        {
                            setBindindSource($"SELECT * from reservation where username='{this.username}'");
                        }


                    }
                    catch (SqlException err)
                    {
                        Console.WriteLine(err.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a row to delete first!");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            using (RentCars newRent = new RentCars(username))
            {
                newRent.ShowDialog();
            }
            this.Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
