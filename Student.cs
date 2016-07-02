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
    public partial class Student : Form
    {
        private OleDbConnection connection = new OleDbConnection();
        public Student()
        {
            InitializeComponent();
            connection.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=E:\Database1.accdb;
           Persist Security Info=False;";
        }
        
        //update student
        private void update_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                string cb = "UPDATE Student SET [Student_Name]= '" + textstudentname.Text + "',[Department_Name]= '" + textdepartmentname.Text + "',[Email]='" +textemail.Text+ "'where [Student_Id]= '"+textstudentid.Text+"'";
                command = new OleDbCommand(cb);
                command.Connection = connection;
                command.ExecuteReader();
                MessageBox.Show("Successfully updated", "Student Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                connection.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            refresh1();
        }

       //insert new student

        private void insert_Click(object sender, EventArgs e)
        {
            if (textstudentid.Text == "")
            {
                MessageBox.Show("Please enter Student_ID", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textstudentid.Focus();
                return;
            }

            if (textstudentname.Text == "")
            {
                MessageBox.Show("Please enter Student_Name", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textstudentname.Focus();
                return;
            }
            
         
            if (textdepartmentname.Text == "")
            {
                MessageBox.Show("Please enter Departmentname", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textdepartmentname.Focus();
                return;
            }

            if (textemail.Text == "")
            {
                MessageBox.Show("Please select EmailS", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textemail.Focus();
                return;
            }




            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;

                string ct = "SELECT Student_Id FROM Student WHERE Student_Id=@find";

                command = new OleDbCommand(ct);
                command.Connection = connection;
                command.Parameters.Add(new OleDbParameter("@find", System.Data.OleDb.OleDbType.VarChar, 20, "Student_Id"));
                command.Parameters["@find"].Value = textstudentid.Text;
                OleDbDataReader rdr = command.ExecuteReader();

                if (rdr.Read())
                {
                    MessageBox.Show("Student ID Already Exists", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    if ((rdr != null))
                    {
                        rdr.Close();
                    }
                    connection.Close();

                }
                else
                {
                    //connection.Open();

                    string cb = "INSERT INTO Student(Student_Id,Student_Name,Department_Name,Email) VALUES (@d1,@d2,@d3,@d4)";

                    command = new OleDbCommand(cb);

                    command.Connection = connection;

                    command.Parameters.Add(new OleDbParameter("@d1", System.Data.OleDb.OleDbType.VarChar, 20, "Student_Id"));
                    command.Parameters.Add(new OleDbParameter("@d2", System.Data.OleDb.OleDbType.VarChar, 30, "Student_Name"));
                    command.Parameters.Add(new OleDbParameter("@d3", System.Data.OleDb.OleDbType.VarChar, 30, "Department_Name"));
                    command.Parameters.Add(new OleDbParameter("@d4", System.Data.OleDb.OleDbType.VarChar, 30, "Email"));
                    
                    command.Parameters["@d1"].Value = textstudentid.Text;
                    command.Parameters["@d2"].Value = textstudentname.Text;
                    command.Parameters["@d3"].Value = textdepartmentname.Text;
                    command.Parameters["@d4"].Value = textemail.Text;
                    


                    command.ExecuteReader();
                    MessageBox.Show("Successfully saved", "Student Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    insert.Enabled = true;

                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }


                    connection.Close();
                    connection.Open();
                    string cb1 = "INSERT INTO Admincontrol(User_Id,Student_Id) VALUES (@d1,@d2)";
                    command = new OleDbCommand(cb1);

                    command.Connection = connection;
                    command.Parameters.Add(new OleDbParameter("@d1", System.Data.OleDb.OleDbType.VarChar, 20, "Admin"));
                    command.Parameters.Add(new OleDbParameter("@d2", System.Data.OleDb.OleDbType.VarChar, 30, "Student_Id"));

                    command.Parameters["@d1"].Value = Login.admin;
                    command.Parameters["@d2"].Value = textstudentid.Text;

                    command.ExecuteReader();
                    connection.Close();
                }



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            refresh1();     //to refresh gridbox
        }


        //show all students
        private void all_student_Click(object sender, EventArgs e)
        {
            connection.Close();

            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                string query = " select (Student_Id)as[Student_Id],(Student_Name)as[Student_Name],(Department_Name)as[Department_name],(Email)as[Email] from Student order by Student_Id";

                command.CommandText = query;

                OleDbDataAdapter da1 = new OleDbDataAdapter(command);
                DataTable dt1 = new DataTable();

                da1.Fill(dt1);
                dataGridView1.DataSource = dt1;


                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //search student icchamoto
        private void search_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Student_Id")
            {

                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                string cp = "SELECT (Student_Id)as [Student_Id],(Student_Name) as [Student_name],(Department_Name) as [Department_Name],(Email) as [Email] from Student where Student_Id like '" + textsearch.Text + "' order by Student_Id";


                command.CommandText = cp;


                OleDbDataAdapter da1 = new OleDbDataAdapter(command);
                DataTable dt1 = new DataTable();

                da1.Fill(dt1);
                dataGridView1.DataSource = dt1;


                connection.Close();

            }
            if (comboBox1.Text == "Student_Name")
            {

                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
               
                string cp = "SELECT (Student_Id)as [Student_Id],(Student_Name) as [Student_name],(Department_Name) as [Department_Name],(Email) as [Email] from Student where Student_Name like '%" + textsearch.Text + "%' order by Student_Id";

                command.CommandText = cp;


                OleDbDataAdapter da1 = new OleDbDataAdapter(command);
                DataTable dt1 = new DataTable();

                da1.Fill(dt1);
                dataGridView1.DataSource = dt1;


                connection.Close();

            }

            if (comboBox1.Text == "Department_Name")
            {

                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
               
                string cp = "SELECT (Student_Id)as [Student_Id],(Student_Name) as [Student_name],(Department_Name) as [Department_Name],(Email) as [Email] from Student where Department_Name like '" + textsearch.Text + "' order by Student_Id";

                command.CommandText = cp;


                OleDbDataAdapter da1 = new OleDbDataAdapter(command);
                DataTable dt1 = new DataTable();

                da1.Fill(dt1);
                dataGridView1.DataSource = dt1;


                connection.Close();

            }
        }

        //delete student record
        private void delete_Click(object sender, EventArgs e)
        {
            try
            {


                if (MessageBox.Show("Do you really want to delete the record?", "Student Record", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                {
                    delete_records();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            refresh1();
        }

        //delete function
        private void delete_records()
        {

            try
            {

                int RowsAffected = 0;
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                string cq = "delete from Student where Student_Id=@DELETE1;";
                command = new OleDbCommand(cq);
                command.Connection = connection;
                command.Parameters.Add(new OleDbParameter("@DELETE1", System.Data.OleDb.OleDbType.VarChar, 20, "Student_Id"));
                command.Parameters["@DELETE1"].Value = textstudentid.Text;
                RowsAffected = command.ExecuteNonQuery();

                if (RowsAffected > 0)
                {
                    MessageBox.Show("Successfully deleted", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                }
                else
                {
                    MessageBox.Show("No record found", "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                 
                }
                connection.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
           /* DataGridViewCell cel = null;
            foreach (DataGridViewCell selectedCell in dataGridView1.SelectedCells)
            {
                cel = selectedCell;
                break;

            }
            if (cel != null) //find out row
            {

                DataGridViewRow row = cel.OwningRow;

                textstudentid.Text = row.Cells[0].Value.ToString();
                textstudentname.Text = row.Cells[1].Value.ToString();
                textdepartmentname.Text = row.Cells[2].Value.ToString();
                textemail.Text = row.Cells[3].Value.ToString();
               

            }
            * */
        }


        //go back main_menu
        private void main_menu_Click(object sender, EventArgs e)
        {
            this.Hide();
            Main_Menu mn = new Main_Menu();
            mn.Show();
        }


        /// <summary>
        /// loads all student info initially
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
      
        private void Student_Load(object sender, EventArgs e)
        {
            connection.Close();

            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                string query = " select (Student_Id)as[Student_Id],(Student_Name)as[Student_Name],(Department_Name)as[Department_name],(Email)as[Email] from Student order by Student_Id";

                command.CommandText = query;

                OleDbDataAdapter da1 = new OleDbDataAdapter(command);
                DataTable dt1 = new DataTable();

                da1.Fill(dt1);
                dataGridView1.DataSource = dt1;


                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        public void refresh1()
    {
    
        connection.Close();

            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                string query = " select (Student_Id)as[Student_Id],(Student_Name)as[Student_Name],(Department_Name)as[Department_name],(Email)as[Email] from Student order by Student_Id";

                command.CommandText = query;

                OleDbDataAdapter da1 = new OleDbDataAdapter(command);
                DataTable dt1 = new DataTable();

                da1.Fill(dt1);
                dataGridView1.DataSource = dt1;


                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

    }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCell cel = null;
            foreach (DataGridViewCell selectedCell in dataGridView1.SelectedCells)
            {
                cel = selectedCell;
                break;

            }
            if (cel != null) //find out row
            {

                DataGridViewRow row = cel.OwningRow;

                textstudentid.Text = row.Cells[0].Value.ToString();
                textstudentname.Text = row.Cells[1].Value.ToString();
                textdepartmentname.Text = row.Cells[2].Value.ToString();
                textemail.Text = row.Cells[3].Value.ToString();


            }
        }


    }
}
