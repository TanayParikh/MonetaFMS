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
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//Libraries used for file and data connections
using System.IO;
using MySql.Data.MySqlClient;
using ICSharpCode.SharpZipLib.Zip;

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

        //Stores the drive name and database path of the program executes from
        public string driveName;
        public string databasePath;

        //Blank class constructor (Needed for structural purposes)
        public SharedData()
        {
            // Sets the database path for XAMPP server and other files
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Moneta"))
            {
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Moneta");
            }

            databasePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Moneta";
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

        // Pre: None
        // Post: The zampplite server is set up.
        // Description: Unzips the server if need be and starts it up. 
        public void SetupNewProgram()
        {
            driveName = Application.StartupPath.Substring(0, Application.StartupPath.IndexOf('\\')) + "\\";
            
            //Checks if the database directory exists, if not creates it
            if (!System.IO.Directory.Exists(driveName + "\\MonetaDatabase"))
            {
                // Informs user of setup
                MessageBox.Show("Setting up the program for initial use. Please press ok to continue.");

                // Assigns path and creates directory for xampplite server
                string zipPath = Application.StartupPath + "\\Resources\\xampplite.zip";
                Directory.CreateDirectory(driveName + "\\MonetaDatabase");

                // Initializes the unziper
                var targetDir = driveName + "\\MonetaDatabase";
                FastZip fastZip = new FastZip();
                string fileFilter = null;

                // Will always overwrite if target filenames already exist
                fastZip.ExtractZip(zipPath, targetDir, fileFilter);

                // Informs user of completion and need for program restart
                MessageBox.Show("Moneta Financial Management Suite has successfully been set up. Please restart the program.");
                Application.Exit();
            }

            // Starts up local xampp server
            string[] MyArguments = { "y", "n", "y", "x" };
            StartProcess(driveName + "\\MonetaDatabase\\xampplite\\setup_xampp.bat", String.Join(" ", MyArguments));
            StartProcess(driveName + "\\MonetaDatabase\\xampplite\\mysql_start.bat");
        }

        // Pre: processName and commandLineArgs of process.
        // Post: Executes process in background.
        // Description: Runs command line process. 
        public int StartProcess(string processName, string commandLineArgs = null)
        {
            Process process = new Process();
            process.StartInfo.FileName = processName;
            process.StartInfo.Arguments = commandLineArgs;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.ErrorDialog = false;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.Start();
            return process.Id;
        }
    }
}
