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
    public partial class AdminDash : Form
    {
        public AdminDash()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            using(Cars newCar = new Cars())
            {
                newCar.ShowDialog();
            }
            this.Show();
        }

        
        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            using (register newUser = new register("admin"))
            {
                newUser.ShowDialog();
            }
            this.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            using (Reservations newReservation = new Reservations("admin", ""))
            {
                newReservation.ShowDialog();
            }
            this.Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
