using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace cg
{
    public partial class Form5 : Form
    {
        int eye = 0;
        int sec = 0;
        OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\clg documents\pro1\cg_database.mdb");
        String id = "aaa", pass = "aaa";
        static String MyOtp = "";

        String pname, email, mob, password;
        private static Random r = new Random();

        public Form5()
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

            con.Open();
            String query = "select * from PersonalDetailed where email = '"
                                + textBox1.Text + "'";
            OleDbCommand cmd = new OleDbCommand(query, con);
            OleDbDataReader dr = cmd.ExecuteReader();
            if (dr.Read()) // id found
            {


                pass = dr["password"].ToString();
                label3.Text = pass;

            }
            else
            {
                textBox1.Text = "";
                textBox2.Text = "";

                label3.Text = "No Record Found !";
            }

            con.Close();       


            label3.Visible = true;

            label4.Visible = false;
            label6.Visible = false;
            label5.Visible = false;

            pictureBox2.Visible = false;

            textBox5.Visible = false;
            textBox6.Visible = false;

            button5.Visible = false;
        }
        public int GetPasswordStrength(string password)
        {
            int Marks = 0;
            // here we will check password strength
            if (password.Length < 8)
            {
                // bad
                return 1;
            }
            else
            {
                Marks = 1;
            }
            if (Regex.IsMatch(password, "[a-z]"))
            {
                // 2    fair
                Marks++;
            }
            if (Regex.IsMatch(password, "[A-Z]"))
            {
                // 3    medium
                Marks++;
            }
            if (Regex.IsMatch(password, "[0-9]"))
            {
                //4     strong
                Marks++;
            }
            if (Regex.IsMatch(password, "[<,>,@,!,#,$,%,^,&,*,(,),_,+,\\[,\\],{,},?,:,;,|,',\\,.,/,~,`,-,=]"))
            {
                //5     very strong
                Marks++;
            }
            return Marks;
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            label3.Visible = false;

            label4.Visible = true;
            label6.Visible = true;
            label5.Visible = true;

            pictureBox2.Visible = true;

            textBox5.Visible = true;
            textBox6.Visible = true;

            button5.Visible = true;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (eye == 0)
            {
                eye = 1;
                pictureBox2.ImageLocation = "C:\\clg documents\\pro1\\img\\hide.png";
                textBox5.PasswordChar = '\0';
            }
            else
            {
                eye = 0;
                pictureBox2.ImageLocation = "C:\\clg documents\\pro1\\img\\open.png";
                textBox5.PasswordChar = '●';
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            int m = GetPasswordStrength(textBox5.Text);
            switch (m)
            {
                case 0:
                    label5.Text = "BAD";
                    break;

                case 1:
                    label5.Text = "BAD";
                    break;

                case 2:
                    label5.Text = "FAIR";
                    break;

                case 3:
                    label5.Text = "MEDIUM";
                    break;

                case 4:
                    label5.Text = "STRONG";
                    break;

                case 5:
                    label5.Text = "VERY STRONG";
                    break;


                default:
                    break;
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button3.Enabled = false;
            
            if (textBox1.Text == "")
            {
                MessageBox.Show("Please Enter Email ID ");
            }
            else
            {//otp code
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
                    button2.Visible = true;
                    timer1.Enabled = false;
                    
                }
                else
                {
                    MessageBox.Show(" Not verified");
                    timer1.Enabled = false;
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
        //    con.Open();
        //    OleDbCommand cmd = con.CreateCommand();
        //    cmd.CommandType = CommandType.Text;
        //    cmd.CommandText = "update PersonalDetailed set password = '"
        //                        + textBox5.Text + "' where email = '"
        //                        + textBox1.Text + "'";
        //    cmd.ExecuteNonQuery();
        //    con.Close();



            ////// R

            con.Open();
            String query = "select * from PersonalDetailed where email = '"
                                + textBox1.Text + "'";
            OleDbCommand cmd = new OleDbCommand(query, con);
            OleDbDataReader dr = cmd.ExecuteReader();
            if (dr.Read()) // id found
            {


                pname = dr["pname"].ToString();
                email = dr["email"].ToString();
                mob = dr["mob"].ToString();
                password = dr["password"].ToString();
                
             //   label3.Text = pass;

            }
            else
            {
                //textBox1.Text = "";
                //textBox2.Text = "";

                label3.Text = "No Record Found !";
            }

            con.Close();       

            ////// D

            con.Open();
            OleDbCommand cmd1 = con.CreateCommand();
            cmd1.CommandType = CommandType.Text;
            cmd1.CommandText = "delete from PersonalDetailed where email = '"
                                + textBox1.Text + "'";
            cmd1.ExecuteNonQuery();
            con.Close();


            ////// I

            con.Open();
            OleDbCommand cmd2 = con.CreateCommand();
            //SqlCommand
            cmd2.CommandType = CommandType.Text;
            // query
            cmd2.CommandText = "Insert into PersonalDetailed values('"
                                + pname + "','"
                                + email + "','"
                                + mob + "','"
                                + textBox5.Text + "')";
            cmd2.ExecuteNonQuery();
            con.Close();
            
            MessageBox.Show("Password updated");
           // display();
           // textBox1.Text = "";
            //textBox2.Text = "";
          //  textBox3.Text = "";

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            sec--;
            label8.Text = sec + " seconds remaining";
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
