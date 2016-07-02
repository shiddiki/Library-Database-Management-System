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
using System.Diagnostics;
namespace Library_Database_version2
{
    public partial class Main_Menu : Form
    {
        private OleDbConnection connection = new OleDbConnection();
        public Main_Menu()
        {
            InitializeComponent();
            connection.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=E:\Database1.accdb;
           Persist Security Info=False;";

        }

        private void changeAdminPasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Changepassword chngpass = new Changepassword();
            chngpass.Show();
        }

        private void addAnotherAdminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Add_admin addadmin  = new Add_admin();
            addadmin.Show();
        }

        private void updateStudentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Student upstd = new Student();
            upstd.Show();
        }

        private void updateBookToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Book upbk = new Book();
            upbk.Show();
        }

        private void newStudentEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Student instd = new Student();
            instd.Show();
        }

        private void newBookEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Book inbk = new Book();
            inbk.Show();
        }

        private void issueBookToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Book_issue bkissue = new Book_issue();
            bkissue.Show();
        }

        private void studentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Student st = new Student();
            st.Show();
        }

        private void bookToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Book bk = new Book();
            bk.Show();
        }

        private void search_student_Click(object sender, EventArgs e)
        {
            this.Hide();
            Student st = new Student();
            st.Show();
        }

        private void search_book_Click(object sender, EventArgs e)
        {
            this.Hide();
            Book bk = new Book();
            bk.Show();
        }

        private void search_book_by_student_Click(object sender, EventArgs e)
        {
            this.Hide();
            Search_book srcbk = new Search_book();
            srcbk.Show();         
                        
        }

        //Send Email

        private void sendEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            connection.Open();
            OleDbCommand command1 = new OleDbCommand();
            command1.Connection = connection;
            string cq = "Select Email From Student where Student_Id in(select Student_Id FROM Studentbook where DateDiff('d',[Issue_Date], NOW())=31) ";
            command1.CommandText = cq;
            OleDbDataAdapter da1 = new OleDbDataAdapter(command1);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);

            
            int notsent = 0;
            foreach (DataRow row in dt1.Rows)
            {
                string email = row["Email"].ToString();

                if (String.IsNullOrEmpty(email))
                    continue;
                
               MessageBox.Show("Trying to send email ");
                using (MailMessage mm = new MailMessage("****@gmail.com", email))
                {
                    try
                    {
                        mm.Subject = "Attention please,renew your book";
                        mm.Body = string.Format("1 month over,you should renew or return the book");

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
                    }
                    catch 
                    {
                        notsent++;
                    }

                   
                }
            }

            if(notsent>0)
                MessageBox.Show("Email send Failure!\nPlease check connection and email address.");

            else
                MessageBox.Show("All Email sent successfully!");

            connection.Close();
        }


       //Logout
        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            connection.Close();
            this.Hide();
            Login lgout = new Login();
            lgout.Show();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            about ab = new about();
            ab.Show();
        }

        private void Main_Menu_Load(object sender, EventArgs e)
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

       


        //mouse hover colour change
        private void search_book_by_student_MouseHover(object sender, EventArgs e)
        {
            //search_book_by_student.BackColor = Color.Cyan;
        }

        private void search_book_by_student_MouseEnter(object sender, EventArgs e)
        {
            search_book_by_student.ForeColor = Color.SlateGray;
        }

        private void search_book_by_student_MouseLeave(object sender, EventArgs e)
        {
            search_book_by_student.ForeColor = Color.Black;
        }

        private void search_student_MouseEnter(object sender, EventArgs e)
        {
            search_student.ForeColor = Color.SlateGray;
        }

        private void search_student_MouseLeave(object sender, EventArgs e)
        {
            search_student.ForeColor = Color.Black;
        }

        private void search_book_MouseEnter(object sender, EventArgs e)
        {
            search_book.ForeColor = Color.SlateGray;
        }

        private void search_book_MouseLeave(object sender, EventArgs e)
        {
            search_book.ForeColor = Color.Black;
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            help hp = new help();
            hp.Show();
        }
        
    }
}
