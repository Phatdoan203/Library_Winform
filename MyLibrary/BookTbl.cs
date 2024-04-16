using System;
using System.Collections;
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
    public partial class BookTbl : Form
    {
        public BookTbl()
        {
            InitializeComponent();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=DESKTOP-GI251AE\SQLEXPRESS;Initial Catalog=MyLibrary;Integrated Security=True;Encrypt=False");

        public void populate()
        {
            Con.Open();
            string query = "SELECT * FROM BookTbl";
            SqlDataAdapter da = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            BookDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //Add
            if (txbBookName.Text == "" || txbAuthor.Text == "" || txbPublisher.Text == "" || txbPrice.Text == "" || txbQuantity.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO BookTbl VALUES ('" + txbBookName.Text + "','" + txbAuthor.Text + "','" + txbPublisher.Text + "'," + txbPrice.Text + ", " + txbQuantity.Text + ")", Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Book Added Successfully");
                Con.Close();
                populate();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Edit btn
            if (txbBookName.Text == "" || txbAuthor.Text == "" || txbPublisher.Text == "" || txbPrice.Text == "" || txbQuantity.Text == "")
            {
                MessageBox.Show("Missing Information!");
            }
            else
            {
                Con.Open();
                string query = "UPDATE BookTbl SET Author='" + txbAuthor.Text + "',Publisher='" + txbPublisher.Text + "',Price=" + txbPrice.Text + ",Qty=" + txbQuantity.Text + " WHERE BookName ='" + txbBookName.Text + "';";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Student Successfully Updated");
                Con.Close();
                populate();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Delete btn
            if (txbBookName.Text == "")
            {
                MessageBox.Show("Enter The StudentId");
            }
            else
            {
                Con.Open();
                string query = "DELETE FROM BookTbl WHERE BookName = '" + txbBookName.Text + "';";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Book Successfully Deleted");
                Con.Close();
                populate();
            }
        }

        private void BookTbl_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainForm main = new MainForm();
            main.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BookDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < BookDGV.Rows.Count)
            {
                DataGridViewRow row = BookDGV.Rows[e.RowIndex];

                txbBookName.Text = row.Cells[0].Value.ToString();
                txbAuthor.Text = row.Cells[1].Value.ToString();
                txbPublisher.Text = row.Cells[2].Value.ToString();
                txbPrice.Text = row.Cells[3].Value.ToString();
                txbQuantity.Text = row.Cells[4].Value.ToString();
                
            }
        }
    }
}
