using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyLibrary
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StudentForm student = new StudentForm();
            student.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            BookTbl bookTbl = new BookTbl();
            bookTbl.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            LibrarianForm lib = new LibrarianForm();
            lib.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            IssueBookForm issueBook = new IssueBookForm();
            issueBook.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ReturnForm returnForm = new ReturnForm();
            returnForm.Show();
            this.Hide();
        }

        
    }
}
