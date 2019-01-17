using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Collections;
using System.IO;

namespace HW2
{
    public partial class mainFrm : Form
    {
        ArrayList ALbookID = new ArrayList();    // for keeping bookIDs.     >> I'm going to use them in updating
        ArrayList ALwriterID = new ArrayList();  // for keeping writerIDs.         book and stock as input.
        OleDbConnection oledbConn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Directory.GetCurrentDirectory().ToString() + @"\Book.mdb");  // Ole Db Connection itself. 
        public mainFrm()
        {
            InitializeComponent();
        }

        private void mainFrm_Load(object sender, EventArgs e)
        {
            loginControl();   // before main form; login form will open.
            fill_listView();  // for filling ListView with book, writer and stock information from database.      
        }
        private void loginControl()
        {
            loginFrm myLogin = new loginFrm();  // for connecting login form.
            if (myLogin.ShowDialog(this) == DialogResult.OK)  // if the conditions that I define are satisfied...
            {
                lblGreeting.Text = myLogin.getName();  //...user's name will be on the rigth top corner of main form.
            }
            else   // if the conditions that I define are not satisfied...
            {
                this.Close();  // main form won't be opened.
            }
        }
        private void fill_listView()  // for filling listView with book, writer and stock information from database.
        {
            string command = @"SELECT Stock.count, Book.bookName, Book.pageNumber, Book.publishDate, Writer.writerName, Writer.writerSurname, Book.bookID, Book.writerID
                               FROM Writer INNER JOIN (Book INNER JOIN Stock ON Book.bookID = Stock.bookID) ON Writer.writerID = Book.writerID";
            OleDbCommand cmd = new OleDbCommand(command, oledbConn);
            try
            {               
                oledbConn.Open();
                OleDbDataReader reader = cmd.ExecuteReader();
                lvBooks.Items.Clear();  //   first clear the arrays and listView itself.
                ALbookID.Clear();       //   if we don't clear all the information will be occur in listView(or arrays) again and again.
                ALwriterID.Clear();
                while (reader.Read())
                {
                    DateTime pDate = DateTime.Parse(reader["publishDate"].ToString());  // for better visualize in listView.              
                    string[] subitems = { reader["bookName"].ToString(), reader["writerName"].ToString() + " " + reader["writerSurname"].ToString(), reader["pageNumber"].ToString(), pDate.ToString("yyyy"), reader["count"].ToString() };
                    ListViewItem items = new ListViewItem(subitems);
                    lvBooks.Items.Add(items);
                    ALbookID.Add(reader["bookID"].ToString());  // for keeping IDs in an arraylist.
                    ALwriterID.Add(reader["writerID"].ToString());
                }
                reader.Close();
                oledbConn.Close();
            }
            catch (Exception)  // in case there will be any problem in connection with database.
            { 
                MessageBox.Show("A problem occured in filling list. Please try again", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);                 
            }
        }

        private void btnAddBook_Click(object sender, EventArgs e)
        {
            BookAddUpdateFrm addBook = new BookAddUpdateFrm();  // for connecting first constructor of BookAddUpdate form.
            if (addBook.ShowDialog(this) == DialogResult.OK)
            {
                fill_listView();  // after the changes, clear and fill the listView again.
            }
        }

        private void button1_Click(object sender, EventArgs e)  //this is updateStock button. 
        {
            if (lvBooks.SelectedItems.Count == 1)  // if user select 'a line of listView' not more not less. 
            {
                int indiceBookID = Convert.ToInt32(ALbookID[lvBooks.SelectedIndices[0]]);  // for finding selected bookID from the array that we create. 
                StockIncreaseDecreaseFrm updateStock = new StockIncreaseDecreaseFrm(indiceBookID);  // for connecting StockIncreaseDecrease form.
                if (updateStock.ShowDialog(this) == DialogResult.OK)
                {
                    fill_listView();  // after the changes, clear and fill the listView again.
                }
            }            
        }

        private void lvBooks_DoubleClick(object sender, EventArgs e)
        {
            if (lvBooks.SelectedItems.Count == 1)  // if user select 'a line of listView' not more not less. 
            {
                int indiceBookID = Convert.ToInt32(ALbookID[lvBooks.SelectedIndices[0]]);   // for finding selected bookID from the array that we create.
                int indiceWriterID = Convert.ToInt32(ALwriterID[lvBooks.SelectedIndices[0]]);  //for finding selected writerID from the array that we create.
                BookAddUpdateFrm updateBook = new BookAddUpdateFrm(indiceBookID, indiceWriterID);  // for connecting other constructor of BookAddUpdate form.
                if (updateBook.ShowDialog(this) == DialogResult.OK)
                {
                    fill_listView();  // after the changes, clear and fill the listView again.
                }
            }
        }       
    }
}
