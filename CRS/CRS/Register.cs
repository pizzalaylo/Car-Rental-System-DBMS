using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Drawing.Text;

namespace CRS
{
    public partial class register : Form
    {
        private string userType;
        public register(string userType)
        {
            this.userType = userType;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (username.Text != "")
            {
                if (first_name.Text != "")
                {
                    if (last_name.Text != "")
                    {
                        if (email.Text != "")
                        {
                            if (password.Text != "")
                            {
                                if (confirmPassword.Text != password.Text)
                                {
                                    MessageBox.Show("Please Enter same password");
                                    password.Text = "";
                                    confirmPassword.Text = "";

                                }
                                else
                                {
                                   registerUser();
                                }
                            }
                            else
                            {
                            }
                        }
                    }
                }
            }
        }
        
    

        private void registerUser()
        {
            string query = $"insert into auth_user values('{username.Text}', '{first_name.Text}', '{last_name.Text}', '{email.Text}', '{password.Text}', '{userType}')";
            using (SqlConnection connection = Globals.createSQLConnection())
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                try
                {
                    command.ExecuteNonQuery();
                    MessageBox.Show("User Registered successfully!");
                    this.Hide();

                }
                catch (SqlException err)
                {
                    Console.WriteLine(err.Message);
                }

            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginPage login = new LoginPage("customer"); 
            login.ShowDialog();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        
    }
}
