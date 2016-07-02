using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
namespace Library_Database_version2
{
    public partial class Login : Form
    {
        public static string admin;
        private OleDbConnection connection = new OleDbConnection();
        public Login()
        {
            InitializeComponent();
            connection.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=E:\Database1.accdb;
           Persist Security Info=False;";

        }


        //Login
        private void button_OK_Click(object sender, EventArgs e)
        {
            if (textusername.Text.Trim().Length == 0)
            {
                MessageBox.Show("Please Enter User Name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textusername.Focus();
                return;
            }
            if (textpassword.Text.Trim().Length == 0)
            {
                MessageBox.Show("Please Enter Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textpassword.Focus();
                return;
            }
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                string query = "SELECT User_Id,Pword FROM Admin WHERE User_Id = @system AND Pword = @admin";
                command.CommandText = query;



                OleDbParameter uName = new OleDbParameter("@system", OleDbType.VarChar);
                OleDbParameter uPassword = new OleDbParameter("@admin", OleDbType.VarChar);
                uName.Value = textusername.Text;
                admin = textusername.Text;
                uPassword.Value = textpassword.Text;
                command.Parameters.Add(uName);
                command.Parameters.Add(uPassword);
                OleDbDataReader myReader = command.ExecuteReader(CommandBehavior.CloseConnection);

                if (myReader.Read() == true)
                {
                    this.Hide();
                    Main_Menu menu = new Main_Menu();
                    menu.Show();


                }


                else
                {
                    MessageBox.Show("Login is Failed...Try again !", "Login Denied", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    textusername.Clear();
                    textpassword.Clear();
                    textusername.Focus();

                }
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }


        }

        //cancle login

        private void button_cancle_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            
        }


        //kills process
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x10) // WM_CLOSE
            {
                // Process the form closing. Call the base method if required,
                // and return from the function if not.
                // For example:
                var ret = MessageBox.Show("Exit??", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (ret == System.Windows.Forms.DialogResult.No)
                    return;
                else
                {
                    //this.Close();
                    Environment.Exit(1);
                    Application.Exit();


                }
            }
            base.WndProc(ref m);
        }

     /*   private void Recovery_password_Click(object sender, EventArgs e)
        {
            
            this.Hide();
            Recovery_Password rpass = new Recovery_Password();
            rpass.Show();
            
        }
        */
        private void Recovery_password_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Recovery_Password rpass = new Recovery_Password();
            rpass.Show();
        }

    }
}