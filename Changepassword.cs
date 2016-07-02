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
    public partial class Changepassword : Form
    {
        private OleDbConnection connection = new OleDbConnection();
        public Changepassword()
        {
            InitializeComponent();
            connection.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=E:\Database1.accdb;
           Persist Security Info=False;";
        }


        //change password
        private void Change_Password_Click(object sender, EventArgs e)
        {
            try
            {
                int RowsAffected = 0;
                if ((textusername.Text.Trim().Length == 0))
                {
                    MessageBox.Show("Please enter user name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textusername.Focus();
                    return;
                }
                if ((textoldpassword.Text.Trim().Length == 0))
                {
                    MessageBox.Show("Please enter old password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textoldpassword.Focus();
                    return;
                }
                if ((textnewpassword.Text.Trim().Length == 0))
                {
                    MessageBox.Show("Please enter new password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textnewpassword.Focus();
                    return;
                }
                if ((textconfirmpassword.Text.Trim().Length == 0))
                {
                    MessageBox.Show("Please confirm new password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textconfirmpassword.Focus();
                    return;
                }
                if ((textnewpassword.TextLength < 5))
                {
                    MessageBox.Show("The New Password Should be of Atleast 5 Characters", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textnewpassword.Text = "";
                    textconfirmpassword.Text = "";
                    textnewpassword.Focus();
                    return;
                }
                else if ((textnewpassword.Text != textconfirmpassword.Text))
                {
                    MessageBox.Show("Password do not match", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textnewpassword.Text = "";
                    textoldpassword.Text = "";
                    textconfirmpassword.Text = "";
                    textoldpassword.Focus();
                    return;
                }
                else if ((textoldpassword.Text == textnewpassword.Text))
                {
                    MessageBox.Show("Password is same..Re-enter new password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textnewpassword.Text = "";
                    textconfirmpassword.Text = "";
                    textnewpassword.Focus();
                    return;
                }



                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                string co = "UPDATE Admin SET [Pword] = '" + textnewpassword.Text + "'WHERE [User_Id]='" + textusername.Text + "' AND [Pword] = '" + textoldpassword.Text + "'";
                command = new OleDbCommand(co);
                command.Connection = connection;
                RowsAffected = command.ExecuteNonQuery();


                if ((RowsAffected > 0))
                {
                    MessageBox.Show("Successfully changed", "Password", MessageBoxButtons.OK, MessageBoxIcon.Information);
                   // this.Hide();
                    textusername.Text = "";
                    textnewpassword.Text = "";
                    textoldpassword.Text = "";
                    textconfirmpassword.Text = "";
                   // Main_Menu LoginForm1 = new Main_Menu();
                    //LoginForm1.Show();

                }
                else
                {
                    MessageBox.Show("invalid username or password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textusername.Text = "";
                    textnewpassword.Text = "";
                    textoldpassword.Text = "";
                    textconfirmpassword.Text = "";
                    textusername.Focus();
                }
                if ((connection.State == ConnectionState.Open))
                {
                    connection.Close();
                }
                connection.Close();
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
    }
}
