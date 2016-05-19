/***************************************************************************************************************
 * 
    Author:         Tanay Parikh
    Project Name:   Moneta
    Description:    Settings module, allowing the user to dictate application wide general values, as well as
                    modify database and email settings.
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

//Libraries for sql access and file input output
using System.IO;
using MySql.Data.MySqlClient;


namespace Moneta
{
    class SettingsModule
    {
        //Class shared data and form variables used to access main form data
        SharedData data;
        frmMain frm;

        //Stores the path to the settings file
        private string filePath;

        //Stores the headings of the different settings categories in arrays
        private string[] generalSettingsHeadings = "Company Name,Company Address,Company Phone Number,Logo Location,Cost Per KM Travelled".Split(',');
        private string[] emailSettingsHeadings = "SMTP Address,Port,Email User ID,Email Password,Require SSL (True/False)".Split(',');
        private string[] databaseSettingsHeadings = "Server,Database User ID,DatabasePassword,Database Name".Split(',');

        //Constructs the settings module with the form and shared data as reference
        public SettingsModule(frmMain frm, SharedData data)
        {
            //Locally stores the form and shared data references
            this.frm = frm;
            this.data = data;

            //Sets the file path to the application's start up path combined with the settings.moneta extension
            filePath = data.databasePath + "\\SETTINGS.MONETA";
        }

        //Pre: None
        //Post: Initializes the class's dgv columns
        //Description: Sets up the columns and adds to the dgv settings
        public void Initialize()
        {
            //Creates the settings headings column storing setting names. Adds onto settings dgv
            DataGridViewTextBoxColumn settingsColumn = new DataGridViewTextBoxColumn
            {
                HeaderText = "Setting",
                Name = "Statistic",
                Width = 300,
                SortMode = DataGridViewColumnSortMode.Automatic,
                ReadOnly = true
            };

            frm.dgvSettings.Columns.Add(settingsColumn);

            //Creates the settings values column storing setting values. Adds onto settings dgv
            DataGridViewTextBoxColumn valuesColumn = new DataGridViewTextBoxColumn
            {
                HeaderText = "",
                Name = "Value",
                Width = 350,
                SortMode = DataGridViewColumnSortMode.Automatic
            };

            frm.dgvSettings.Columns.Add(valuesColumn);

            //Disables ability to add/remove rows to the dgv
            frm.dgvSettings.AllowUserToAddRows = false;
            frm.dgvSettings.AllowUserToDeleteRows = false;

            //Reads the settings data from the file
            ReadSettingsData();
        }

        //Pre: None
        //Post: Settings data is read from the external settings file
        //Description: Checks if file exists, and if so reads settings. If not calls for the creation of default settings
        private void ReadSettingsData()
        {   
            //Checks file existance
            if (File.Exists(filePath))
            {
                //Attempts to read the file
                try
                {
                    //Instanstiates the streamreader with the specified file path
                    StreamReader inFile = new StreamReader(filePath);

                    //Reads in each line, settings seperated by the " !@BREAK@!~ " delimiter
                    data.generalSettings = inFile.ReadLine().Split(new string[] { " !@BREAK@!~ " }, StringSplitOptions.None);
                    data.emailSettings = inFile.ReadLine().Split(new string[] { " !@BREAK@!~ " }, StringSplitOptions.None);
                    data.databaseSettings = inFile.ReadLine().Split(new string[] { " !@BREAK@!~ " }, StringSplitOptions.None);

                    //Sets the database build settings, which were read in from the file.
                    data.builder.Server = data.databaseSettings[SharedData.DATABASE_SERVER];
                    data.builder.UserID = data.databaseSettings[SharedData.DATABASE_USER_ID];
                    data.builder.Password = data.databaseSettings[SharedData.DATABASE_PASSWORD];
                    data.builder.Database = data.databaseSettings[SharedData.DATABASE_NAME];

                    //Builds database connection
                    data.connection = new MySqlConnection(data.builder.ToString());

                    //Closes the file and displays the settings in the dgv
                    inFile.Close();
                    DisplaySettings();
                }
                catch
                {
                    //Calls for the creation of a new settings file and the re-calling of this subprogram
                    CreateSettingsFile();
                    ReadSettingsData();
                }
            }
            else
            {
                //Calls for the creation of a new settings file and the re-calling of this subprogram
                CreateSettingsFile();
                ReadSettingsData();
            }
        }

        //Pre: None
        //Post: A new settings file is created.
        //Description: Creates a new settings file with default data
        private void CreateSettingsFile()
        {
            //Attempts to create the settings file
            try
            {
                //Instantiates a streamwriter variable with the required file path
                StreamWriter outFile = new StreamWriter(filePath);

                //Writes the default settings lines onto the file
                outFile.WriteLine("Moneta FMS !@BREAK@!~ 123 Sydney Court, Toronto, Ontario !@BREAK@!~ (647) 123-9999 !@BREAK@!~ /Logo.png !@BREAK@!~ 0.53");
                outFile.WriteLine("smtp.gmail.com !@BREAK@!~ 587 !@BREAK@!~ example@gmail.com !@BREAK@!~ password !@BREAK@!~ true");
                outFile.WriteLine("localhost !@BREAK@!~ root !@BREAK@!~ password !@BREAK@!~ mydb");

                //Closes the file
                outFile.Close();
            }
            //Executes if file creation fails
            catch
            {
                //Informs user that the file couldn't be created, and asks the user to restart the program. 
                MessageBox.Show("File could not be created. Please close all .txt files which may be open and restart the program.");
                Application.Exit();
            }
        }

        //Pre: None
        //Post: The settings are displayed with headings on the settings dgv
        //Description: Displays the settings, with headings, from the settings arrays to the settings dgv
        private void DisplaySettings()
        {
            //Stores local variables for the number of each type of setting thus far published
            int numGen = 0;
            int numEmail = 0;
            int numDat = 0;
            
            //Runs for the number of rows to be present in the settings dgv
            for (int j = 0; j < 20; ++j)
            {
                //Checks if all the general settings have been published
                if (numGen < generalSettingsHeadings.Length)
                {
                    //If not, checks if the first one has been published
                    if (numGen == 0)
                    {
                        //If not, adds a row and fills with the heading of general settings
                        frm.dgvSettings.Rows.Add();
                        frm.dgvSettings.Rows[j].Cells[0].Value = "General Settings";
                        ++j;
                    }

                    //Adds a row, and fills with the general setting name and value
                    frm.dgvSettings.Rows.Add();
                    frm.dgvSettings.Rows[j].Cells[0].Value = generalSettingsHeadings[numGen];
                    frm.dgvSettings.Rows[j].Cells[1].Value = data.generalSettings[numGen];

                    //Increments the number of general settings displayed by one
                    ++numGen;
                }
                //Checks if all the email settings have been published
                else if (numEmail < emailSettingsHeadings.Length)
                {
                    //If not, checks if the first one has been published
                    if (numEmail == 0)
                    {
                        //If not, adds a filler row, and then another row and fills with the heading of general settings.
                        frm.dgvSettings.Rows.Add();
                        frm.dgvSettings.Rows.Add();

                        //Increments by j before and after to account for blank spacing row
                        ++j;
                        frm.dgvSettings.Rows[j].Cells[0].Value = "Email Settings";
                        ++j;
                    }

                    //Adds a row, and fills with the email setting name and value
                    frm.dgvSettings.Rows.Add();
                    frm.dgvSettings.Rows[j].Cells[0].Value = emailSettingsHeadings[numEmail];
                    frm.dgvSettings.Rows[j].Cells[1].Value = data.emailSettings[numEmail];

                    //Increments the number of email settings displayed by one
                    ++numEmail;
                }
                //Checks if all the database settings have been published
                else if (numDat < databaseSettingsHeadings.Length)
                {
                    //If not, checks if the first one has been published
                    if (numDat == 0)
                    {
                        //If not, adds a filler row, and then another row and fills with the heading of general settings
                        frm.dgvSettings.Rows.Add();
                        frm.dgvSettings.Rows.Add();

                        //Increments by j before and after to account for blank spacing row
                        ++j;
                        frm.dgvSettings.Rows[j].Cells[0].Value = "Database Settings";
                        ++j;
                    }

                    //Adds a row, and fills with the database setting name and value
                    frm.dgvSettings.Rows.Add();
                    frm.dgvSettings.Rows[j].Cells[0].Value = databaseSettingsHeadings[numDat];
                    frm.dgvSettings.Rows[j].Cells[1].Value = data.databaseSettings[numDat];

                    //Increments the number of database settings displayed by one
                    numDat++;
                }
            }
        }

        //Pre: None
        //Post: The settings are saved onto an external file.
        //Description: Each row of the settings dgv is read, and the modified settings are stored
        public void SaveSettings()
        {
            //Stores the value of the setting being considered
            string value = "";

            //Sets up a stream writer file with the desired file path
            StreamWriter outFile = new StreamWriter(filePath);

            //Sets up the output lines for the settings data
            string generalData = "";
            string emailData = "";
            string databaseData = "";

            //Checks to see if the database connection settings have been changed
            bool databaseSettingsChanged = false;

            //Runs for all the rows
            for (int i = 0; i < frm.dgvSettings.RowCount; ++i)
            {
                //Ensures that the 2nd column's cell value isn't null for the row
                if (frm.dgvSettings.Rows[i].Cells[1].Value != null && !string.IsNullOrEmpty(frm.dgvSettings.Rows[i].Cells[1].Value.ToString()))
                {
                    //If so, stores the value of the cell
                    value = frm.dgvSettings.Rows[i].Cells[1].Value.ToString();

                    //Runs a switch statement with 'i' representing the row number in the settings dgv
                    //Stores the value entered in the specified settings array's specified index 
                    switch (i)
                    {
                        case 1:
                            data.generalSettings[SharedData.COMPANY_NAME] = value;
                            break;

                        case 2:
                            data.generalSettings[SharedData.COMPANY_ADDRESS] = value;
                            break;

                        case 3:
                            data.generalSettings[SharedData.COMPANY_PHONE_NUMBER] = value;
                            break;

                        case 4:
                            data.generalSettings[SharedData.COMPANY_LOGO_PATH] = value;
                            break;

                        case 5:
                            //Confirms numerical entry by trying to parse it
                            double isCostValid;
                            double.TryParse(value, out isCostValid);

                            //If parsing succeeds, updates the setting with the new value
                            if (isCostValid > 0)
                            {
                                data.generalSettings[SharedData.COST_PER_KM] = value;
                            }
                            else
                            {
                                //Otherwise, informs user.
                                MessageBox.Show("Please enter a valid cost per km.");
                            }

                            break;

                        case 8:
                            data.emailSettings[SharedData.SMTP_ADDRESS] = value;
                            break;

                        case 9:
                            //Confirms numerical entry by trying to parse it
                            int isPortValid;
                            int.TryParse(value, out isPortValid);

                            //If parsing succeeds, updates the setting with the new value
                            if (isPortValid > 0)
                            {
                                data.emailSettings[SharedData.SMTP_PORT] = value;
                            }
                            else
                            {
                                //Otherwise informs user
                                MessageBox.Show("Please enter a valid SMTP port.");
                            }

                            break;

                        case 10:
                            //Attempts to parse email address
                            try
                            {
                                //Tries formating as emailaddress - (a form of parse)
                                System.Net.Mail.MailAddress addr = new System.Net.Mail.MailAddress(value);

                                //If successful, stores the new email user ID
                                data.emailSettings[SharedData.SMTP_USER_ID] = value;
                            }
                            catch
                            {
                                //If parsing fails informs user, resets the dgv value to that of before
                                MessageBox.Show("Please enter a valid email address.");
                                value = data.emailSettings[SharedData.SMTP_USER_ID];
                            }
                            
                            break;

                        case 11:
                            data.emailSettings[SharedData.SMTP_PASSWORD] = value;
                            break;

                        case 12:
                            //Checks if the value entered is either true or false for requiring ssl
                            if (value.ToLower() == "true" || value.ToLower() == "false")
                            {
                                data.emailSettings[SharedData.SMTP_REQUIRESSL] = value;
                            }
                            else
                            {
                                //Otherwise resets value and informs user, as value is invalid
                                value = data.emailSettings[SharedData.SMTP_REQUIRESSL];
                                MessageBox.Show("Require SSL can either be set to true or false. Please update value.");
                            }
                            
                            break;

                        case 15:
                            if (data.databaseSettings[SharedData.DATABASE_SERVER] != value)
                            {
                                data.databaseSettings[SharedData.DATABASE_SERVER] = value;
                                databaseSettingsChanged = true;
                            }

                            break;

                        case 16:
                            if (data.databaseSettings[SharedData.DATABASE_USER_ID] != value)
                            {
                                data.databaseSettings[SharedData.DATABASE_USER_ID] = value;
                                databaseSettingsChanged = true;
                            }
                            break;

                        case 17:
                            if (data.databaseSettings[SharedData.DATABASE_PASSWORD] != value)
                            {
                                data.databaseSettings[SharedData.DATABASE_PASSWORD] = value;
                                databaseSettingsChanged = true;
                            }

                            break;

                        case 18:
                            if (data.databaseSettings[SharedData.DATABASE_NAME] != value)
                            {
                                data.databaseSettings[SharedData.DATABASE_NAME] = value;
                                databaseSettingsChanged = true;
                            }

                            break;
                    }
                }
            }

            //Creates the general settings output line, with the " !@BREAK@!~ " delimiter
            generalData = data.generalSettings[SharedData.COMPANY_NAME] + " !@BREAK@!~ "
                + data.generalSettings[SharedData.COMPANY_ADDRESS] + " !@BREAK@!~ "
                + data.generalSettings[SharedData.COMPANY_PHONE_NUMBER] + " !@BREAK@!~ "
                + data.generalSettings[SharedData.COMPANY_LOGO_PATH] + " !@BREAK@!~ "
                + data.generalSettings[SharedData.COST_PER_KM];

            //Creates the email settings output line, with the " !@BREAK@!~ " delimiter
            emailData = data.emailSettings[SharedData.SMTP_ADDRESS] + " !@BREAK@!~ "
                + data.emailSettings[SharedData.SMTP_PORT] + " !@BREAK@!~ "
                + data.emailSettings[SharedData.SMTP_USER_ID] + " !@BREAK@!~ "
                + data.emailSettings[SharedData.SMTP_PASSWORD] + " !@BREAK@!~ "
                + data.emailSettings[SharedData.SMTP_REQUIRESSL];

            //Creates the database settings output line, with the " !@BREAK@!~ " delimiter
            databaseData = data.databaseSettings[SharedData.DATABASE_SERVER] + " !@BREAK@!~ "
                + data.databaseSettings[SharedData.DATABASE_USER_ID] + " !@BREAK@!~ "
                + data.databaseSettings[SharedData.DATABASE_PASSWORD] + " !@BREAK@!~ "
                + data.databaseSettings[SharedData.DATABASE_NAME];

            //Writes the three lines from above into the file
            outFile.WriteLine(generalData);
            outFile.WriteLine(emailData);
            outFile.WriteLine(databaseData);

            //Closes the file
            outFile.Close();

            //Checks to see if database settings have been changed. If so, restarts the program.
            if (databaseSettingsChanged)
            {
                MessageBox.Show("Database settings have been saved. The program will now restart to configure these changes.");
                Application.Restart();
            }
            else
            {
                MessageBox.Show("Settings have been saved.");
            }
        }

        //Indicates settings dgv selection change
        public void dgvSelectionChanged()
        {
            //Checks if it's the second column of fourth row (Add logo image button)
            if (frm.dgvSettings.CurrentCell.RowIndex == 4 && frm.dgvSettings.CurrentCell.ColumnIndex == 1)
            {
                //Calls for the fetching of the image
                frm.dgvSettings.CurrentCell.Value = FetchImage(frm.dgvSettings.CurrentCell.Value.ToString());
            }
        }

        //Pre: The string value of the image currently (for backup purposes)
        //Post: The image is copied over to the application folder, and the path is saved
        //Description: Opens the file dialog to get the image. Saves it in the images directory, with the file path. 
        private string FetchImage(string value)
        {
            //Executes if user selects an image
            if (frm.ofdExpenses.ShowDialog() == DialogResult.OK)
            {
                //Sets the source path of the image to the one that the user selected
                string sourcePath = frm.ofdExpenses.FileName;

                //Checks if images directory exists, if not creates it
                if (!System.IO.Directory.Exists(data.databasePath + "\\Images"))
                {
                    System.IO.Directory.CreateDirectory(data.databasePath + "\\Images");
                }

                //Attempts to copy over the image into the images directory and return the image identifier
                try
                {
                    System.IO.File.Copy(sourcePath, data.databasePath + "\\Images\\" + Path.GetFileName(sourcePath));
                    return Path.GetFileName(sourcePath);
                }
                //Otherwise if the file can't be copied, tries copying again but with a random number attached to the end
                catch (IOException)
                {
                    //Creates random num
                    int random = data.generator.Next(1, 9999);

                    //Saves the file path and returns to user
                    System.IO.File.Copy(sourcePath, data.databasePath + "\\Expenses\\" + random + Path.GetFileName(sourcePath));
                    return random + Path.GetFileName(sourcePath);
                }
                //If extraordinary exception occurs (File too large in size) lets the user know.
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + " Please try another image.");
                }
            }

            //Returns the backup value if new image path couldn't be found
            return value;
        }
    }
}