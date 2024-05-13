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
    public partial class Form4 : Form
    {
        int eye = 0;
        int sec = 0;
        OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\clg documents\pro1\cg_database.mdb");
        static String MyOtp = "";
        private static Random r = new Random();

        public Form4()
        {
            InitializeComponent();
        }
        public static string RandomString(int length) //code
        {
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[r.Next(s.Length)]).ToArray());
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (eye == 0)
            {
                eye = 1;
                pictureBox2.ImageLocation = "C:\\clg documents\\pro1\\img\\hide.png";
                textBox5.PasswordChar='\0';
            }
            else
            {
                eye = 0;
                pictureBox2.ImageLocation = "C:\\clg documents\\pro1\\img\\open.png";
                textBox5.PasswordChar = '●';
            }
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


        private void label7_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.Show();
            this.Hide();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            if(textBox5.Text == textBox6.Text)//confirm password
            {
             //   MessageBox.Show("Password Match");
                con.Open();
                OleDbCommand cmd = con.CreateCommand();
                //SqlCommand
                cmd.CommandType = CommandType.Text;
                // query
                cmd.CommandText = "Insert into PersonalDetailed values('"
                                    + textBox1.Text + "','"
                                    + textBox2.Text + "','"
                                    + textBox4.Text + "','"
                                    + textBox5.Text + "')";

                cmd.ExecuteNonQuery();
                con.Close();

                textBox1.Text = "";
                textBox2.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";

                MessageBox.Show("Account has been Created");

                Form2 f2 = new Form2();
                f2.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Password Not Match");
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;
            
            // otp code

            if (textBox2.Text == "")
            {
                MessageBox.Show("Please Enter Email ID ");
            }
            else
            {
                MyOtp = RandomString(4);
                MailMessage mail = new MailMessage();
                mail.To.Add(textBox2.Text);
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

        private void button3_Click(object sender, EventArgs e)
        {// show password or hide password
            if (textBox2.Text == "")
            {
                MessageBox.Show("Please Enter OTP");
            }
            else
            {
                if (textBox3.Text == MyOtp)
                {
                    MessageBox.Show("Verified");
                    button1.Visible = true;
                    timer1.Enabled = false;
                }
                else
                {
                    MessageBox.Show(" Not verified");
                    //button1.Visible = true;
                    timer1.Enabled = false;
                }
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            int m = GetPasswordStrength(textBox5.Text);
            switch (m)
            {
                case 0:
                    label8.Text = "BAD";
                    break;

                case 1:
                    label8.Text = "BAD";
                    break;

                case 2:
                    label8.Text = "FAIR";
                    break;

                case 3:
                    label8.Text = "MEDIUM";
                    break;

                case 4:
                    label8.Text = "STRONG";
                    break;

                case 5:
                    label8.Text = "VERY STRONG";
                    break;


                default:
                    break;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            sec--;
            label9.Text = sec + " seconds remaining";
            if (sec==0)
            {
                timer1.Enabled = false;
                button2.Enabled = true;
                
                MyOtp = "0000";
                MessageBox.Show("OTP time up!!! ");
                
            }
        }
    }
}
