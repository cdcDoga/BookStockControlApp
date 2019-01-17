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
    public partial class loginFrm : Form
    {
        OleDbConnection oledbConn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Directory.GetCurrentDirectory().ToString() + @"\Book.mdb");
        //we need this connection in each form that we want to connect database.
        public loginFrm()
        {
            InitializeComponent();
        }

        private void loginFrm_Load(object sender, EventArgs e)
        {

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string command = @"SELECT User.* FROM[User]";
            OleDbCommand cmd = new OleDbCommand(command, oledbConn);
            int cnt = 0;  //for controlling user number(there are 2 users in our database)
            try
            {
                oledbConn.Open(); 
                OleDbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    cnt++;
                    if (tbLogName.Text == reader["UserName"].ToString() && tbLogSurname.Text == reader["UserSurname"].ToString() && tbPassword.Text == reader["Password"].ToString()) 
                    {              //if user's name, surname and password are true at the same time...
                        this.DialogResult = DialogResult.OK;   //conditions are completed
                        break;
                    } 
                    else 
                    {
                        if (cnt == 2) //in the last user if informations are still wrong...
                        {
                            MessageBox.Show("Please check your information", "Not Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning);                        
                        }
                    }
                }
                reader.Close();
                oledbConn.Close();
            
            
            }
            catch(Exception)
            {
                MessageBox.Show("A problem occured in connection. Please try again","Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        public string getName()  //public because I'm going to call this function in main form 
        {
            return tbLogName.Text + " " + tbLogSurname.Text;  //for greeting label, returns user's name and surname
        }
    }
}
