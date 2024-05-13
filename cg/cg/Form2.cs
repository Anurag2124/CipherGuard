using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace cg
{
    public partial class Form2 : Form
    {
        OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\clg documents\pro1\cg_database.mdb");
        String id = "aaa", pass = "aaa";
        public static string uname = "", uemail="";
        int eye = 0;
        public Form2()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Form5 f5 = new Form5();
            f5.Show();
            this.Hide();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            f3.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Form4 f4 = new Form4();
            f4.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // id pass check
            // uemail = textBox6.Text;
            // check captcha
            // if (textBox3.Text == textBox4.Text)
            // {
            // verif id pass

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
                    uname = dr["pname"].ToString();
                    uemail = dr["email"].ToString();
                    id = dr["email"].ToString();
                    pass = dr["password"].ToString();

                    // MessageBox.Show("Record Found !");
                    // pass check
                    if (pass == textBox2.Text)  // password check
                    {
                        //move
                        //Homepg h1 = new Homepg();
                        //h1.Show();
                        //this.Hide();
                        MessageBox.Show("Welcome !");

                        Form6 f = new Form6();
                        f.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("incorrect pass");
                    }
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (eye == 0)
            {
                eye = 1;
                pictureBox1.ImageLocation = "C:\\clg documents\\pro1\\img\\hide.png";
                textBox2.PasswordChar = '\0';
            }
            else
            {
                eye = 0;
                pictureBox1.ImageLocation = "C:\\clg documents\\pro1\\img\\open.png";
                textBox2.PasswordChar = '●';
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

    }
}
