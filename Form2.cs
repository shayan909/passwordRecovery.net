using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;

namespace EmailPassword
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();

        }
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-692HJVG;Initial Catalog=1712172;Integrated Security=True");

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            String recipient = textBox1.Text + "@" + comboBox1.Text;
            string recovery = textBox2.Text;
            string pass = textBox3.Text;
            int port;
            string server;


            string Password = "";
            int Char = 0;
            int Char2 = 0;
            char C = ' ';
          
            int X = 0;
            Random R = new Random();
            Random R2 = new Random();

            for (int i = 0; i < 8; i++)
            {
                Char = R.Next(65, 90);
                Char2 = R2.Next(107, 122);
                C = (char)Char;
               
                X = Char2;
           
                Password += C;
                Password += X.ToString();

            }

            MailMessage msg = new MailMessage(recovery, recipient, "Password Reset Notification", "Your New Password is: "+Password+"");
            msg.IsBodyHtml = true;
            if (comboBox1.Text == "live.com")
            {
                server = "smtp.live.com";
                port = 587;
            }
            else
            {
                server = "smtp.gmail.com";
                port = 465;
                
            }
            SmtpClient cv = new SmtpClient(server, port);

            cv.UseDefaultCredentials = false;
            cv.EnableSsl = true;
            cv.Credentials = new NetworkCredential(recovery,pass);
            cv.SendMailAsync(msg);
                
            
            
            
            
          con.Open();
            string query = "update info set password = '"+Password+"'where email ='"+recipient+"'";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            sda.SelectCommand.ExecuteNonQuery();
            con.Close();

            MessageBox.Show(Password);
       

    
                }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            textBox3.UseSystemPasswordChar = true;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
