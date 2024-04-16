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
    public partial class IssueBookForm : Form
    {
        public IssueBookForm()
        {
            InitializeComponent();
        }
        
        SqlConnection Con = new SqlConnection(@"Data Source=DESKTOP-GI251AE\SQLEXPRESS;Initial Catalog=MyLibrary;Integrated Security=True;Encrypt=False");
        private void FillStudent()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("select StdId from StudentTbl",Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("StdId", typeof(int));
            dt.Load(rdr);
            StdCb.ValueMember = "StdId";
            StdCb.DataSource = dt;
            Con.Close();
        }

        public void populate()
        {
            Con.Open();
            string query = "SELECT * FROM IssueTbl";
            SqlDataAdapter da = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            IssueDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void fetchStudentData()
        {
            Con.Open();
            string query = "select * from StudentTbl where StdId=" + StdCb.SelectedValue.ToString() + "";
            SqlCommand cmd = new SqlCommand(query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                txbStdName.Text = dr["StdName"].ToString();
                txbStdDept.Text = dr["StdDep"].ToString();
                txbStdPhone.Text = dr["StdPhone"].ToString();
            }
            Con.Close() ;
        }

        // Ham nay thay doi so luong khi da cho muon 1 cuon sach
        private void UpdateBook()
        {
            int Qty, newQty;
            Con.Open() ;
            string query = "select * from BookTbl where BookName='" + cbBook.SelectedValue.ToString() + "'";
            SqlCommand cmd = new SqlCommand(query, Con) ;
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter (cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                Qty = Convert.ToInt32(dr["Qty"].ToString());
                newQty = Qty - 1;
                string query1 = "update BookTbl set Qty=" + newQty + " where BookName='" + cbBook.SelectedValue.ToString() + "';";
                SqlCommand cmd1 = new SqlCommand(query1, Con);
                cmd1.ExecuteNonQuery();
            };
            Con.Close();
        }


        // Dung cho delete btn 
        private void UpdateBookCancellation()
        {
            int Qty, newQty;
            Con.Open();
            string query = "select * from BookTbl where BookName='" + cbBook.SelectedItem.ToString() + "'";
            SqlCommand cmd = new SqlCommand(query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                Qty = Convert.ToInt32(dr["Qty"].ToString());
                newQty = Qty + 1;
                string query1 = "update BookTbl set Qty=" + newQty + " where BookName='" + cbBook.SelectedItem.ToString() + "';";
                SqlCommand cmd1 = new SqlCommand(query1, Con);
                cmd1.ExecuteNonQuery();
            };
            Con.Close();
        }


        private void FillBook()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("select BookName from BookTbl where Qty>"+0+"", Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("BookName", typeof(string));
            dt.Load(rdr);
            cbBook.ValueMember = "BookName";
            cbBook.DataSource = dt;
            Con.Close();
        }
        private void IssueBookForm_Load(object sender, EventArgs e)
        {
            FillStudent();
            FillBook();
            populate();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainForm main = new MainForm();
            main.Show();
        }



        // Dong duoi lam nham`
        private void StdCb_SelectedValueChanged(object sender, EventArgs e)
        {
                
        }

        private void StdCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            fetchStudentData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Issue Btn
            if (txbIssueNum.Text == "" || txbStdName.Text == "")
            {
                MessageBox.Show("Missing Information!");
            }
            else
            {
                string issuedate = IssueDate.Value.ToString("yyyy-MM-dd");
                //string issuedate = IssueDate.Value.Day.ToString() + "/" + IssueDate.Value.Month.ToString() + "/" + IssueDate.Value.Year.ToString();
                Con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO IssueTbl VALUES(" + txbIssueNum.Text + "," + StdCb.SelectedValue.ToString() + ",'" + txbStdName.Text + "','" + txbStdDept.Text + "','" + txbStdPhone.Text + "','" + cbBook.SelectedValue.ToString() + "', '" + issuedate + "')", Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Added Successfully");
                Con.Close();
                UpdateBook(); // ham nay giup ta tinh khi ma cho muon di 1 quyen sach thi ham nay se tinh lai so luong sach con lai
                populate();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Delete btn
            if (txbIssueNum.Text == "")
            {
                MessageBox.Show("Enter the Issue Number");
            }
            else
            {
                Con.Open();
                string query = "DELETE FROM IssueTbl WHERE IssueNum = " + txbIssueNum.Text + ";";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Issue Successfully Deleted");
                Con.Close();
                UpdateBookCancellation();
                populate();
            }

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /*private void LibrarianDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }*/

        private void IssueDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < IssueDGV.Rows.Count)
            {
                DataGridViewRow row = IssueDGV.Rows[e.RowIndex];

                txbIssueNum.Text = row.Cells[0].Value.ToString();
                StdCb.SelectedItem = row.Cells[1].Value.ToString();
                txbStdName.Text = row.Cells[2].Value.ToString();
                txbStdDept.Text = row.Cells[3].Value.ToString();
                txbStdPhone.Text = row.Cells[4].Value.ToString();
                cbBook.Text = row.Cells[5].Value.ToString();
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (txbIssueNum.Text == "" || txbStdName.Text == "" || txbStdDept.Text == "" || txbStdPhone.Text == "")
            {
                MessageBox.Show("Missing Information!");
            }
            else
            {
                string issuedate = IssueDate.Value.ToString("yyyy-MM-dd");
                Con.Open();
                string query = "UPDATE IssueTbl SET StdId='" + StdCb.SelectedValue.ToString() + "',StdName='" + txbStdName.Text + "',StdDept='" + txbStdDept.Text + "', StdPhone='" + txbStdPhone + "',Bookissued='" + cbBook.SelectedValue.ToString() + "',IssueDate='" + issuedate +"'  WHERE IssueNum =" + txbIssueNum.Text + ";";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show(" Successfully Updated");
                Con.Close();
                populate();
            }
        }
    }
}
