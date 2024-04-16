using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyLibrary
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=DESKTOP-GI251AE\SQLEXPRESS;Initial Catalog=MyLibrary;Integrated Security=True;Encrypt=False");
        private void button1_Click(object sender, EventArgs e)
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM LibrarianTbl WHERE LibName = @UserName AND LibPassword = @Password", Con);
            cmd.Parameters.AddWithValue("@UserName", txbUserName.Text);
            cmd.Parameters.AddWithValue("@Password", txbPassWord.Text);
            int count = Convert.ToInt32(cmd.ExecuteScalar());
            if (count == 1)
            {
                this.Hide();
                MainForm main = new MainForm();
                main.Show();
            }
            else
            {
                MessageBox.Show("Wrong User Name Or PassWord");
            }
            Con.Close();
            /*Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select count(*) from LibrarianTbl where LibName = '" + txbUserName.Text + "' and LibPassword = '" + txbPassWord + "' ", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows[0][0].ToString()=="1")
            {
                this.Hide();
                MainForm main = new MainForm();
                main.Show();
            }
            else
            {
                MessageBox.Show("Wrong User Name Or PassWord");
            }
            Con.Close();*/
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
