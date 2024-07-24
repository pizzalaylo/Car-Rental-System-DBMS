using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace CRS
{
    
    public partial class WelcomeForm : Form
    {
        public WelcomeForm()
        {
            InitializeComponent();
            
    }

        public void customer_Click(object sender, EventArgs e)
        {
            OpenLoginForm("customer");
            //loginPage.Show();
            try
            {
                this.Show();
            }
            catch (ObjectDisposedException err)
            {

            }

        }

        public void admin_Click(object sender, EventArgs e)
        {
            OpenLoginForm("admin");
            //loginPage.Show();
            try
            {
                this.Show();
            }
            catch(ObjectDisposedException err)
            {

            }
            

        }

        private void OpenLoginForm(string userType)
        {
            this.Hide();
            using (LoginPage lp = new LoginPage(userType))
            {
                lp.ShowDialog();
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            OpenLoginForm("admin");
            //loginPage.Show();
            try
            {
                this.Show();
            }
            catch (ObjectDisposedException err)
            {

            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            OpenLoginForm("customer");
            //loginPage.Show();
            try
            {
                this.Show();
            }
            catch(ObjectDisposedException err)
            {

            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }

    

}
