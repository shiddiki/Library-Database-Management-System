using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;
using System.Data.OleDb;

namespace Library_Database_version2
{
    public partial class Recovery_Password : Form
    {
        public static string userid;
        private OleDbConnection connection = new OleDbConnection();
        public Recovery_Password()
        {
            InitializeComponent();
            connection.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=E:\Database1.accdb;
           Persist Security Info=False;";
        }

        private void submit_Click(object sender, EventArgs e)
        {
            try
            {
                label2.Text = "Status- Sending.....";
                Refresh();
                userid = textuserid.Text;
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                string cp = "SELECT Email from Admin where User_Id='" + textuserid.Text + "'";
                command.CommandText = cp;
                string email = Convert.ToString(command.ExecuteScalar());
                //MessageBox.Show(email);
                OleDbCommand command1 = new OleDbCommand();
                command1.Connection = connection;
                string ct = "SELECT Pword from Admin where User_Id='" + textuserid.Text + "'";
                command1.CommandText = ct;
                string password = Convert.ToString(command1.ExecuteScalar());
                connection.Close();



               // MessageBox.Show("Trying to send email ");
                
                MailMessage mm = new MailMessage("****@gmail.com", email);
                mm.Subject = "Recovery Password";
                mm.Body = password;

                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                System.Net.NetworkCredential credentials = new System.Net.NetworkCredential();
                credentials.UserName = "****@gmail.com";
                credentials.Password = "****";
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = credentials;
                smtp.Port = 587;
                smtp.Send(mm);

                label2.Text = "Status-";
                Refresh();
                MessageBox.Show("Email sent successfully");

            }
            catch
            {
                MessageBox.Show("Email send Failure!\nPlease check connection and email address.");
            }
            this.Hide();
            Login lg = new Login();
            lg.Show();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        /*private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login lg = new Login();
            lg.Show();
        }*/
    }
}
