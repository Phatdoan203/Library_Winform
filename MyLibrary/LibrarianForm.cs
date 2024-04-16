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
using System.Collections;

namespace MyLibrary
{
    public partial class LibrarianForm : Form
    {
        SqlConnection Con = new SqlConnection(@"Data Source=DESKTOP-GI251AE\SQLEXPRESS;Initial Catalog=MyLibrary;Integrated Security=True;Encrypt=False");
        public LibrarianForm()
        {
            InitializeComponent();
        }

        private void LibrarianForm_Load(object sender, EventArgs e)
        {
            populate();
        }

        public void populate()
        {
            Con.Open();
            string query = "SELECT * FROM LibrarianTbl";
            SqlDataAdapter da = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            LibrarianDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        

        private void button1_Click(object sender, EventArgs e)
        {
            if(LibId.Text == "" || LibName.Text == "" || LibPass.Text == "" || LibPhone.Text == "")
            {
                MessageBox.Show("Missing Information!");
            }
            else
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO LibrarianTbl VALUES("+LibId.Text+",'"+LibName.Text+"','"+LibPass.Text+"','"+LibPhone.Text+"')", Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Librarian Added Successfully");
                Con.Close();
                populate();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (LibId.Text == "")
            {
                MessageBox.Show("Enter the Librarian Id");
            }
            else
            {
                Con.Open();
                string query = "DELETE FROM LibrarianTbl WHERE LibId = " + LibId.Text + ";";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Librarian Successfully Deleted");
                Con.Close();
                populate();
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            if (LibId.Text == "" || LibName.Text == "" || LibPass.Text == "" || LibPhone.Text == "")
            {
                MessageBox.Show("Missing Information!");
            }
            else
            {
                Con.Open();
                string query = "UPDATE LibrarianTbl SET LibName='"+LibName.Text+"',LibPassword='"+LibPass.Text+"',LibPhone='"+LibPhone.Text+"' WHERE LibId ="+LibId.Text+";";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Librarian Successfully Updated");
                Con.Close();
                populate();
            }
        }




        //Nothing
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void LibrarianDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < LibrarianDGV.Rows.Count)
            {
                DataGridViewRow row = LibrarianDGV.Rows[e.RowIndex];

                LibId.Text = row.Cells[0].Value.ToString();
                LibName.Text = row.Cells[1].Value.ToString();
                LibPass.Text = row.Cells[2].Value.ToString();
                LibPhone.Text = row.Cells[3].Value.ToString();
            }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainForm main = new MainForm();
            main.Show();
        }
    }
}
