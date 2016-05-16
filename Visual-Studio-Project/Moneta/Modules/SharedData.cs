/***************************************************************************************************************
 * 
    Author:         Tanay Parikh
    Project Name:   Moneta
    Description:    Stores the solution wide shared data. Main class stored in form. All modules passed in a
                    reference of an object of this class.
 * 
***************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//Libraries used for file and data connections
using System.IO;
using MySql.Data.MySqlClient;

namespace Moneta
{
    class SharedData
    {
        //Database connection variables. Used to connect to database tables
        public MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
        public MySqlConnection connection = new MySqlConnection();
        public MySqlDataAdapter adapter = new MySqlDataAdapter();
        public BindingSource binding = new BindingSource();

        //Items dataset for the items specific table
        public DataSet itemsDataset = new DataSet();
        
        //Random number generator
        public Random generator = new Random();

        //An array storing the natural language values of all the expense categories
        public string[] expenseCategories = new string[21] { "Advertising", "Insurance", "Interest/bank charges", "Office expenses", "Office maintenance", "Legal fees and related expenses", "Accounting and other professional fees", "Management and admin fees", "Maintenance and repair", "Salaries", "Wages", "Benefits", "Property taxes", "Travel", "Utilities", "Cost of goods sold", "Motor vehicle expenses", "Lodging", "Parking fees", "Other misc. supplies", "Union professional and other similar dues" };
        
        //Creates variables to store program theme colors
        public Color textColorWhite = System.Drawing.ColorTranslator.FromHtml("#dfe0e6");
        public Color greenText = System.Drawing.ColorTranslator.FromHtml("#01ef65");

        //Variables to store culture information. Used to format string with dates and as currency.
        public System.Globalization.CultureInfo dateCulture = new System.Globalization.CultureInfo("ja-JP");
        public System.Globalization.DateTimeFormatInfo monthNameCulture = new System.Globalization.DateTimeFormatInfo();

        //Settings array storing the user's unique settings
        public string[] generalSettings = new string[5];
        public string[] emailSettings = new string[5];
        public string[] databaseSettings = new string[4];

        //General settings array constant index accessors
        public const int COMPANY_NAME = 0;
        public const int COMPANY_ADDRESS = 1;
        public const int COMPANY_PHONE_NUMBER = 2;
        public const int COMPANY_LOGO_PATH = 3;
        public const int COST_PER_KM = 4;

        //Email settings array constant index accessors
        public const int SMTP_ADDRESS = 0;
        public const int SMTP_PORT = 1;
        public const int SMTP_USER_ID = 2;
        public const int SMTP_PASSWORD = 3;
        public const int SMTP_REQUIRESSL = 4;

        //Database settings array constant index accessors
        public const int DATABASE_SERVER = 0;
        public const int DATABASE_USER_ID = 1;
        public const int DATABASE_PASSWORD = 2;
        public const int DATABASE_NAME = 3;

        //Error checking verification variable
        public bool tempByPass;

        //Indicates which invoice template to use
        public int invoiceTemplate = 1;

        //Blank class constructor (Needed for structural purposes)
        public SharedData()
        {
            
        }

        //Pre: The error type and the sender object.
        //Post: Informs the user of the error
        //Description. Determines what the error was in the dgv, and based on that informs the user.
        public void DisplayDGVError(object sender, DataGridViewDataErrorEventArgs error)
        {
            //Checks if it was a commit error. (User data not saved)
            if (error.Context == DataGridViewDataErrorContexts.Commit)
            {
                //Checks if the error is currently set to be ignored (For smoother user experience)
                if (!tempByPass)
                {
                    //If not informs the user
                    MessageBox.Show("Commit error");
                }

                //Resets the bypass variable
                tempByPass = false;
            }
            //Checks if it is a constraint exception
            else if ((error.Exception) is ConstraintException)
            {
                //Casts the sender as a datagridview item
                DataGridView view = (DataGridView)sender;

                //Finds the index row and column and sets the error text
                view.Rows[error.RowIndex].ErrorText = "An error has occured";
                view.Rows[error.RowIndex].Cells[error.ColumnIndex].ErrorText = "An error has occured";

                //Prevents the throwing of the exception from the stack
                error.ThrowException = false;
            }
        }

        //Pre: The sql query and the error message if necessary.
        //Post: Executes query. Returns boolean success result.
        //Description: Executes the sql query with the data connection. Returns whether it was successful.
        public bool ExecuteSQLQuery(string query, string errorMessage)
        {
            //Attempts to execute query
            try
            {
                //Checks if connection is closed. If so opens it up
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                //Sets up the command with the query and connection
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //Executes the sql command
                cmd.ExecuteNonQuery();

                //Indicates the command has been successfully performed.
                return true;
            }
            catch (Exception e)
            {
                //Informs the user of the error, with the custom + system generated message
                MessageBox.Show(errorMessage + " " + e.Message);

                //Indicates that the query has failed to execute.
                return false;
            }
        }
    }
}
