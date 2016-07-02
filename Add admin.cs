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
    public partial class Add_admin : Form
    {
        private OleDbConnection connection = new OleDbConnection();
        public Add_admin()
        {
            InitializeComponent();
            connection.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=E:\Database1.accdb;
           Persist Security Info=False;";
        }


        //Add new admin
        private void Add_Click(object sender, EventArgs e)
        {
            if (textusername.Text == "")
            {
                MessageBox.Show("Please enter Username", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textusername.Focus();
                return;
            }

            if (textpassword.Text == "")
            {
                MessageBox.Show("Please enter Password", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textpassword.Focus();
                return;
            }
            if (textemail.Text == "")
            {
                MessageBox.Show("Please enter Email", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textpassword.Focus();
                return;
            }

            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;

                string ct = "SELECT User_Id FROM Admin WHERE User_Id=@find";

                command = new OleDbCommand(ct);
                command.Connection = connection;
                command.Parameters.Add(new OleDbParameter("@find", System.Data.OleDb.OleDbType.VarChar, 20, "User_Id"));
                command.Parameters["@find"].Value = textusername.Text;
                OleDbDataReader rdr = command.ExecuteReader();

                if (rdr.Read())
                {
                    MessageBox.Show("User_Id Already Exists", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    if ((rdr != null))
                    {
                        rdr.Close();
                    }


                }


                else
                {
                    string cb = "INSERT INTO Admin(User_Id,Pword,Email) VALUES (@d1,@d2,@d3)";

                    command = new OleDbCommand(cb);

                    command.Connection = connection;

                    command.Parameters.Add(new OleDbParameter("@d1", System.Data.OleDb.OleDbType.VarChar, 20, "User_Id"));
                    command.Parameters.Add(new OleDbParameter("@d2", System.Data.OleDb.OleDbType.VarChar, 30, "Pword"));
                    command.Parameters.Add(new OleDbParameter("@d3", System.Data.OleDb.OleDbType.VarChar, 30, "Email"));
                   

                    command.Parameters["@d1"].Value = textusername.Text;
                    command.Parameters["@d2"].Value = textpassword.Text;
                    command.Parameters["@d3"].Value = textemail.Text;



                    command.ExecuteNonQuery();
                    MessageBox.Show("Successfully saved", "Student Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Add.Enabled = true;
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }


                    connection.Close();
                    textusername.Text = String.Empty;
                    textpassword.Text=String.Empty;
                    textemail.Text = String.Empty;

                    
                }


            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



        }


        //go back main_menu
        private void Main_Menu_Click(object sender, EventArgs e)
        {
            this.Hide();
            Main_Menu mn = new Main_Menu();
            mn.Show();
        }

        private void Add_admin_Load(object sender, EventArgs e)
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
    }
}