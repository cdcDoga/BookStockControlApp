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
    public partial class BookAddUpdateFrm : Form
    {
        OleDbConnection oledbConn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Directory.GetCurrentDirectory().ToString() + @"\Book.mdb");

        int bookID = -1;    //these integers are for seperate add and update processes. they'll take value just in updating.
        int writerID = -1;
        ArrayList ALwriter = new ArrayList();
        ArrayList ALselectedWriter = new ArrayList();

        public BookAddUpdateFrm()  // first constructor with no input.
        {
            InitializeComponent();
            this.Text = "New Book";  // just for userfriendy form name
            tbBookName.Clear();
            tbPage.Clear();  // garantee that form is empty for adding a new book
        }
        public BookAddUpdateFrm(int indexB, int indexW)  // other constructor with 2 inputs.
        {
            InitializeComponent();
            this.Text = "Update the Book";  // just for user friendly form name
            bookID = indexB;  
            writerID = indexW;  // define the inputs' value
            fillBook_and_Writer();  // for uptading, it is more practicle show to user previous informations.  
        }

        private void addUpdateFrm_Load(object sender, EventArgs e)
        {
            cbWriter.Items.Clear();  // for avoiding repetition of same 3 writer
            fillWriter();
        }
        private void fillWriter()  //for filling combobox's items
        {
            string command = @"SELECT Writer.*FROM Writer";
            OleDbCommand cmd = new OleDbCommand(command, oledbConn);  
            try
            {
                oledbConn.Open();
                cbWriter.Items.Clear();
                ALwriter.Clear();
                OleDbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    cbWriter.Items.Add(reader["writerName"].ToString() + " " + reader["writerSurname"].ToString());
                    ALwriter.Add(reader["writerID"].ToString());
                }
                reader.Close();
                oledbConn.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("A problem occured in filling writers. Please try again", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
        
        private void btnSubmit_Click(object sender, EventArgs e)  // updating and adding processes are both starsing with submit button.
        {
            if (tbBookName.Text != "" && cbWriter.Text != "" && tbPage.Text != ""  && number_control(tbPage.Text) && dtpPublishDate.Value <= DateTime.Now)  // if there is no empty spaces and page is number and date is not after today...
            {
                if (bookID == -1)  //for inserting
                {
                    if (control_addExistBook() == true)  // if user is not trying to add a book which already exists.
                    {
                        insertNewBook();  
                        insert_initialStock_zero();
                        MessageBox.Show("Add successfully!", ":)", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;  //conditions are completed.
                        this.Close();  //close the BookAddUpdateFrm
                    }
                    else
                    {
                        MessageBox.Show("You are trying to add a book which already exists", "Not Valid", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                }
                else //for updating
                {
                    updateBook();
                    MessageBox.Show("Update successfully!", ":)", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;  //conditions are completed.
                    this.Close();  //close the BookAddUpdateFrm
                }
            }
            else
            {
                MessageBox.Show("Please check your information", "Not Valid", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }
        public bool number_control(string x)  // controllig string is number or not
        {
            bool b = x.All(char.IsDigit);
            return b;
        }
        private string findName(string x)  // find name from whole name
        {
            int index = x.LastIndexOf(' ');
            if (index != -1)
            {
                x = x.Remove(index);
            }
            return x;
        }
        private string findSurname(string x) // find the surname from whole name
        {
            int index = x.LastIndexOf(' ');
            if (index != -1)
            {
                x = x.Substring(index + 1);
            }
            return x;
        }
        private bool control_addExistBook()  //control if user is not trying to add a book which already exists.
        {
            bool b = true;
            string command = @"SELECT Book.*, Writer.*
                               FROM Writer INNER JOIN Book ON Writer.writerID = Book.writerID";

            OleDbCommand cmd = new OleDbCommand(command, oledbConn);
            try
            {
                oledbConn.Open();
                OleDbDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    if (tbBookName.Text == reader["BookName"].ToString())
                    { 
                        b = false;
                        break;   
                    }
                }
                reader.Close();
                oledbConn.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("A problem occured in process. Please try again", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return b;
        }

// ---------------------------------------------------------insert functions begins-----------------------------------------------------------------------    

        int newID = -1; //preperation for @@Identitiy query
        private void insertNewBook()  //for inserting book andfind the new ID of inserting book
        {
            string command = @"INSERT INTO [Book]([bookName],[pageNumber],[publishDate],[writerID])  VALUES (?,?,?,?)";
            string command_2 = "SELECT @@Identity"; // Returns the biggest member of bookID (autoNumber, primary key)

            OleDbCommand cmd = new OleDbCommand(command, oledbConn);

            cmd.Parameters.Add(":bookname", OleDbType.Char).Value = tbBookName.Text.ToString();
            cmd.Parameters.Add(":page", OleDbType.BigInt).Value = Convert.ToInt32(tbPage.Text.ToString());
            cmd.Parameters.Add(":publish", OleDbType.Date).Value = DateTime.Parse(dtpPublishDate.Text);
            cmd.Parameters.Add(":wId", OleDbType.BigInt).Value = findNEWWriterId(cbWriter.Text);
            
            try
            {
                oledbConn.Open();
                if (cmd.ExecuteNonQuery() == 1)
                {
                    cmd.CommandText = command_2;
                    newID = Convert.ToInt32(cmd.ExecuteScalar());
                }
                oledbConn.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("A problem occured while adding a new book. Please try again" + e, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        
        private void insert_initialStock_zero()  // I didn't put 'initial stock' in BookAddUpdateFrm design. So I arrenge the system as:  
        {                                        // When user add a new book, automaticly stock will be zero. Then he/she can change it with StockUpdate button.
            string command = @"INSERT INTO [Stock]([count],[bookID])  VALUES (?,?)";
            OleDbCommand cmd = new OleDbCommand(command, oledbConn);

            cmd.Parameters.Add(":Count", OleDbType.BigInt).Value = 0; //stock will be zero.
            cmd.Parameters.Add(":bookid", OleDbType.BigInt).Value = newID; // this is the ID which we found with @@Identitiy

            try
            {
                oledbConn.Open();
                cmd.ExecuteNonQuery();  //for transferring to database.
                oledbConn.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("A problem occured while adding initial stock. Please try again" + e, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

// ------------------------insert functions ends------------------------------------update functions begins--------------------------------------------------------------------------

        private void fillBook_and_Writer()  // fill the previous informations before updating
        {
            string command = @"SELECT Book.bookID, Book.bookName, Book.pageNumber, Book.publishDate, Writer.writerID, Writer.writerName, Writer.writerSurname
                               FROM Writer INNER JOIN Book ON Writer.writerID = Book.writerID
                               WHERE (((Book.bookID)=?) AND ((Writer.writerID)=?))";
            OleDbCommand cmd = new OleDbCommand(command, oledbConn);

            cmd.Parameters.Add(":bookId", OleDbType.BigInt).Value = bookID;
            cmd.Parameters.Add(":writerId", OleDbType.BigInt).Value = writerID;

            try
            {
                oledbConn.Open();
                OleDbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    tbBookName.Text = reader["bookName"].ToString();
                    tbPage.Text = reader["pageNumber"].ToString();
                    dtpPublishDate.Value = DateTime.Parse(reader["publishDate"].ToString());
                    cbWriter.Text = reader["writerName"].ToString() + " " + reader["writerSurname"].ToString(); //* >> actually the last parameter line is for filling the previous writer 
                }                                                                                               //but although previous item is in the dropDownList I can't make it written in cbWriter.text
                reader.Close();
                oledbConn.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("A problem occured while filling the previous information. Please try again", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
        private void updateBook()  //for Updating the book with the help of input of constructor 'bookID'.
        {
            string command = @"UPDATE [Book] SET [bookName]=?,[pageNumber]=?,[publishDate]=?, [writerID]=?
                               WHERE [bookID]=? ";
            OleDbCommand cmd = new OleDbCommand(command, oledbConn);

            cmd.Parameters.Add(":bookname", OleDbType.Char).Value = tbBookName.Text;
            cmd.Parameters.Add(":page", OleDbType.BigInt).Value = Convert.ToInt32(tbPage.Text);
            cmd.Parameters.Add(":publish", OleDbType.Date).Value = DateTime.Parse(dtpPublishDate.Text);
            cmd.Parameters.Add(":writerId", OleDbType.BigInt).Value = findNEWWriterId(cbWriter.Text);
            cmd.Parameters.Add(":bookId", OleDbType.BigInt).Value = bookID;

            try
            {
                oledbConn.Open();
                cmd.ExecuteNonQuery();
                oledbConn.Close();
            }
            catch(Exception)
            {
                MessageBox.Show("A problem occured while updating book information. Please try again", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        

        private int findNEWWriterId(string x)  // The most important function in the code. For finding the selected item's writerID !
        {
            int wIDnew = -1;
            string command = @"SELECT Writer.writerID, Writer.writerName, Writer.writerSurname FROM Writer
                               WHERE [writerName]=? AND [writerSurname]=?";  //query for searching ID which has specific name AND specific surname.
            OleDbCommand cmd = new OleDbCommand(command, oledbConn);

            cmd.Parameters.Add(":writername", OleDbType.Char).Value = findName(x);
            cmd.Parameters.Add(":writersurname", OleDbType.Char).Value = findSurname(x);

            try
            {
                oledbConn.Open();
                OleDbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    wIDnew = Convert.ToInt32(reader["writerID"].ToString());
                }
                reader.Close();
                oledbConn.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("A problem occured while selecting new writer. Please try again" + e, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return wIDnew;  // return the writer ID value.
        }
    }
}
