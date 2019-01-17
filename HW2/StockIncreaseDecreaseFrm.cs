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
    public partial class StockIncreaseDecreaseFrm : Form
    {
        OleDbConnection oledbConn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Directory.GetCurrentDirectory().ToString() + @"\Book.mdb");
        int bookID;
        public StockIncreaseDecreaseFrm(int ID) //for updating stock user also has to select one line
        {                                       //and this line has an bookID
            InitializeComponent();
            bookID = ID;
        }

        private void StockIncreaseDecreaseFrm_Load(object sender, EventArgs e)
        {
            fillPreviousStock();  //for more user friendly interface show the previous stock.
        }

        private void fillPreviousStock() // for showing the previous stock which user  wants to change
        {
            string command = @"SELECT Stock.bookID, Stock.count, Stock.lastUpdate FROM Stock
                               WHERE (((Stock.bookID)=[?]))";
            OleDbCommand cmd = new OleDbCommand(command, oledbConn);

            cmd.Parameters.Add(":bookId", OleDbType.BigInt).Value = bookID;

            try
            {
                oledbConn.Open();
                OleDbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (Convert.ToInt32(reader["count"].ToString()) != -1)  //if the count exists...
                    {
                        lblPreviousStockDynamic.Text = reader["count"].ToString();  //show it to the user in Label.
                    }
                }
                reader.Close();
                oledbConn.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("A problem occured in previous stock. Please try again", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void updateStock()  // updating stock with the value that user writes in textbox. 
        {
            string command = @"UPDATE [Stock] SET [count]=?,[lastUpdate]=?
                               WHERE (((Stock.bookID)=[?]))";
            OleDbCommand cmd = new OleDbCommand(command, oledbConn);

            cmd.Parameters.Add(":count", OleDbType.BigInt).Value = Convert.ToInt32(tbCurrentStock.Text);
            cmd.Parameters.Add(":lastUpDate", OleDbType.Date).Value = DateTime.Now;
            cmd.Parameters.Add(":bookId", OleDbType.BigInt).Value = bookID;

            try
            {
                oledbConn.Open();
                cmd.ExecuteNonQuery();
                oledbConn.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("A problem occured while saving current stock. Please try again", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BookAddUpdateFrm bridge = new BookAddUpdateFrm();  // I define number control function on BookAddUpdateFrm. So I have to create an object to call this function.
            if (tbCurrentStock.Text != "" && bridge.number_control(tbCurrentStock.Text) == true)  //if the only space is not empty and if it is a number... 
            {
                if(tbCurrentStock.Text != lblPreviousStockDynamic.Text)  // if new stock is different from previous one...
                {
                    updateStock(); //update the stock
                    MessageBox.Show("Stock is updated successfully", " :) ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK; //conditions are completed.
                    this.Close(); //close the StockIncreaseDecreaseFrm
                }
                else
                {
                    MessageBox.Show("You should change the stock for update", "Not Valid", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }
            else
            {
                MessageBox.Show("Please check the stock information.", "Not Valid", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }
        
    }
}
