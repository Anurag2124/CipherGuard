using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Mail;
using System.Data.OleDb;

namespace cg
{
    public partial class Form3 : Form
    {
        int sec = 0;
        OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\clg documents\pro1\cg_database.mdb");
        String id = "aaa", pass = "aaa";
        public static string uname = "", uemail = "";
        static String MyOtp = "";
        private static Random r = new Random();
        public Form3()
        {
            InitializeComponent();
        }
        public static string RandomString(int length) //code
        {
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[r.Next(s.Length)]).ToArray());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("Blank values not allowed");
            }
            else
            {
                con.Open();
                String query = "select * from PersonalDetailed where email = '"
                                    + textBox1.Text + "'";
                OleDbCommand cmd = new OleDbCommand(query, con);
                OleDbDataReader dr = cmd.ExecuteReader();
                if (dr.Read()) // id found
                {
                   Form2.uname = dr["pname"].ToString();
                   Form2.uemail = dr["email"].ToString();
                    //id = dr["email"].ToString();
                    //pass = dr["password"].ToString();

                    // MessageBox.Show("Record Found !");
                    // pass check
                    //if (pass == textBox2.Text)  // password check
                    //{
                    //    //move
                    //    //Homepg h1 = new Homepg();
                    //    //h1.Show();
                    //    //this.Hide();
                    //    MessageBox.Show("Welcome !");

                   Form6 f = new Form6();
                   f.Show();
                   this.Hide();
                    //}
                    //else
                    //{
                    //    MessageBox.Show("incorrect pass");
                    //}
                }
                else
                {
                    textBox1.Text = "";
                    textBox2.Text = "";

                    MessageBox.Show("No Record Found !");
                }
                con.Close();
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            button3.Enabled = false;
            if (textBox1.Text == "")
            {
                MessageBox.Show("Please Enter Email ID ");
            }
            else
            {
                MyOtp = RandomString(4);
                MailMessage mail = new MailMessage();
                mail.To.Add(textBox1.Text);
                mail.From = new MailAddress("jarvisai330@gmail.com");
                mail.Subject = "Cipher Guard!";
                string Body = " Your OTP is : " + MyOtp;
                mail.Body = Body;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential("jarvisai330@gmail.com", "vnbfscwlcljtmdxb");
                smtp.EnableSsl = true;
                smtp.Send(mail);
                MessageBox.Show("OTP Successfully Send");
                sec = 30;
                timer1.Enabled = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                MessageBox.Show("Please Enter OTP");
            }
            else
            {
                if (textBox2.Text == MyOtp)
                {
                    MessageBox.Show("Verified");
                    button1.Visible = true;
                }
                else
                {
                    MessageBox.Show(" Not verified");
                }
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Form5 f5 = new Form5();
            f5.Show();
            this.Hide();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            sec--;
            label4.Text = sec + " seconds remaining";
            if (sec == 0)
            {
                timer1.Enabled = false;
                button3.Enabled = true;

                MyOtp = "0000";
                MessageBox.Show("OTP time up!!! ");

            }
        }
    }
}
