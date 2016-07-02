using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Library_Database_version2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            progressBar1.Height = 5;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
            progressBar1.Visible = true;

            this.progressBar1.Value = this.progressBar1.Value + 5;
            if (this.progressBar1.Value == 10)
            {
                label1.Text = "Reading modules..";
            }
            else if (this.progressBar1.Value == 20)
            {                
                label1.Text = "Turning on modules.";
            }
            else if (this.progressBar1.Value == 40)
            {
                label1.Text = "Starting modules..";
            }
            else if (this.progressBar1.Value == 60)
            {
                label1.Text = "Loading Database..";
            }
            else if (this.progressBar1.Value == 80)
            {                
                label1.Text = "Connecting to Database..";
            }
            else if (this.progressBar1.Value == 100)
            {

                timer1.Enabled = false;

                new Login().Show();
                this.Hide();


            }
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }
    }
}
