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
    public partial class ReturnForm : Form
    {
        // May ham duoi bam lin tin nen ko dung den

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        // ------------------------------------------------------------------

        public ReturnForm()
        {
            InitializeComponent();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=DESKTOP-GI251AE\SQLEXPRESS;Initial Catalog=MyLibrary;Integrated Security=True;Encrypt=False");

        public void populate()
        {
            Con.Open();
            string query = "SELECT * FROM IssueTbl";
            SqlDataAdapter da = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            BookIssuedDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        public void populateReturn()
        {
            Con.Open();
            string query = "SELECT * FROM ReturnTbl";
            SqlDataAdapter da = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            ReturnedBookDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void FillBook()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("select BookName from BookTbl where Qty>" + 0 + "", Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("BookName", typeof(string));
            dt.Load(rdr);
            cbBook.ValueMember = "BookName";
            cbBook.DataSource = dt;
            Con.Close();
        }
        private void ReturnForm_Load(object sender, EventArgs e)
        {
            populate();
            populateReturn();
            FillBook();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainForm main = new MainForm();
            main.Show();
        }

        private void BookIssuedDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < BookIssuedDGV.Rows.Count)
            {
                DataGridViewRow row = BookIssuedDGV.Rows[e.RowIndex];

                //txbIssueNum.Text = row.Cells[0].Value.ToString();
                StdCb.SelectedItem = row.Cells[1].Value.ToString();
                txbStdName.Text = row.Cells[2].Value.ToString();
                txbStdDept.Text = row.Cells[3].Value.ToString();
                txbStdPhone.Text = row.Cells[4].Value.ToString();
                cbBook.Text = row.Cells[5].Value.ToString();
            }
        }

        private void ReturnedBookDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void UpdateBook()
        {
            int Qty, newQty;
            Con.Open();
            string query = "select * from BookTbl where BookName='" + cbBook.SelectedValue.ToString() + "'";
            SqlCommand cmd = new SqlCommand(query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                Qty = Convert.ToInt32(dr["Qty"].ToString());
                newQty = Qty + 1;
                string query1 = "update BookTbl set Qty=" + newQty + " where BookName='" + cbBook.SelectedValue.ToString() + "';";
                SqlCommand cmd1 = new SqlCommand(query1, Con);
                cmd1.ExecuteNonQuery();
            };
            Con.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            // Click Return
            if (txbReturnNum.Text == "" || txbStdName.Text == "")
            {
                MessageBox.Show("Missing Information!");
            }
            else
            {
                string issuedate = IssueDate.Value.ToString("yyyy-MM-dd");
                string returndate = ReturnDate.Value.ToString("yyyy-MM-dd");
                //string issuedate = IssueDate.Value.Day.ToString() + "/" + IssueDate.Value.Month.ToString() + "/" + IssueDate.Value.Year.ToString();
                Con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO ReturnTbl VALUES(" + txbReturnNum.Text + "," + StdCb.SelectedItem.ToString() + ",'" + txbStdName.Text + "','" + txbStdDept.Text + "','" + txbStdPhone.Text + "','" + cbBook.SelectedValue.ToString() + "', '" + issuedate + "', '" + returndate + "')", Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Return Successfully");
                Con.Close();
                UpdateBook(); // ham nay giup ta tinh khi ma lay lai 1 quyen sach thi ham nay se tinh lai so luong sach 
                populate();
                populateReturn();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
