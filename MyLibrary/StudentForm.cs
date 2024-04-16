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
    public partial class StudentForm : Form
    {
        public StudentForm()
        {
            InitializeComponent();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=DESKTOP-GI251AE\SQLEXPRESS;Initial Catalog=MyLibrary;Integrated Security=True;Encrypt=False");

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainForm main = new MainForm();
            main.Show();
        }

        public void populate()
        {
            Con.Open();
            string query = "SELECT * FROM StudentTbl";
            SqlDataAdapter da = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            StudentDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void StudentForm_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (StdId.Text == "" || StdName.Text == "" || StdDept.Text == "" || cbSem.Text == "")
            {
                MessageBox.Show("Missing Infomation");
            }
            else
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO StudentTbl VALUES ("+ StdId.Text +",'"+ StdName.Text +"','"+ StdDept.Text +"',"+ cbSem.SelectedItem.ToString() +", '"+ StdPhone.Text +"')", Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Student Added Successfully");
                Con.Close();
                populate();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (StdId.Text == "")
            {
                MessageBox.Show("Enter The StudentId");
            }
            else
            {
                Con.Open();
                string query = "DELETE FROM StudentTbl WHERE StdId = " + StdId.Text + ";";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Student Successfully Deleted");
                Con.Close();
                populate();
            }
        }




        // Bam' Nham` :)))
        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void StudentDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < StudentDGV.Rows.Count)
            {
                DataGridViewRow row = StudentDGV.Rows[e.RowIndex];

                StdId.Text = row.Cells[0].Value?.ToString();
                StdName.Text = row.Cells[1].Value?.ToString();
                StdDept.Text = row.Cells[2].Value?.ToString();
                cbSem.SelectedItem = row.Cells[3].Value?.ToString();
                StdPhone.Text = row.Cells[4].Value?.ToString();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (StdId.Text == "" || StdName.Text == "" || StdDept.Text == "" || cbSem.Text == "")
            {
                MessageBox.Show("Missing Information!");
            }
            else
            {
                Con.Open();
                string query = "UPDATE StudentTbl SET StdName='" + StdName.Text + "',StdDep='" + StdDept.Text + "',StdSem='"+ cbSem.SelectedItem.ToString() +"',StdPhone='" + StdPhone.Text + "' WHERE StdId =" + StdId.Text + ";";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Student Successfully Updated");
                Con.Close();
                populate();
            }
        }
    }
}
