using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRS
{
    public partial class LoginPage : Form
    {
        private string userType;

        public LoginPage(string userType)
        {
            InitializeComponent();
            this.userType = userType;
            if(userType=="admin")
            {
                button2.Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;

            if (AuthenticateUser(username, password))
            {
                MessageBox.Show("Login successful!");
                this.Hide();
                if(userType=="customer")
                {
                    using (UserDash customer = new UserDash(username))
                    {
                        customer.ShowDialog();
                    }
                        
                }
                else
                {
                    using (AdminDash admin = new AdminDash())
                    {
                        admin.ShowDialog();
                    }
                  
                }
                
                // Proceed with further actions or display another form, etc.
            }
            else
            {
                MessageBox.Show("Invalid username or password!");
            }

        }

        private bool AuthenticateUser(string username, string password)
        {
            
            string query = $"SELECT * FROM auth_user WHERE username = '{username}' AND password = '{password}' and user_type = '{userType}'";

            using (SqlConnection connection = Globals.createSQLConnection())
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader data = command.ExecuteReader();
                return data.HasRows;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            using (register newUser = new register(userType))
            {
                newUser.ShowDialog();
            }
            this.Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Hide();
            WelcomeForm wF = new WelcomeForm();
            wF.ShowDialog();
        }

        private void LoginPage_Load(object sender, EventArgs e)
        {

        }
    }
}
