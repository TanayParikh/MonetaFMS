/***************************************************************************************************************
 * 
    Author:         Tanay Parikh
    Project Name:   Moneta
    Description:    Expense module tracking business expenses. Also stores receipt images. Further has the 
                    ability to time work, and add distances travelled.
 * 
***************************************************************************************************************/

using System;
using System.Data;
using System.Windows.Forms;
using System.Diagnostics;

//Libraries used for sql database access and file IO
using MySql.Data.MySqlClient;
using System.IO;

namespace Moneta
{
    class ExpenseModule
    {
        //Class shared data and form variables used to access main form data
        SharedData data;
        frmMain frm;
        
        //Stores the dgv's previous value's and position
        private string oldCellValue = "";
        private int prevRow = 0;
        private int prevCol = 0;

        //Tracks the amount of time passed for the timing sub-module
        private int ticks = 0;
        private int numMinutes = 0;
        private int numHours = 0;

        //Constants to track time intervals
        private const double TIME_MULTIPLE = 60;
        private const double SECONDS_IN_HOUR = 3600;

        //Class constructor with the form and shared data parameters
        public ExpenseModule(frmMain frm, SharedData data)
        {
            //Locally stores the form and shared data
            this.frm = frm;
            this.data = data;
        }

        //Pre: None
        //Post: Intializes the images column in the data grid view. 
        //Description: Sets up the image column in the expenses dgv. Sets the expense category column width to 250.
        public void Initialize()
        {
            //Sets expense category column width to 250
            frm.dgvExpenses.Columns[5].Width = 250;

            //Sets up the images column. Makes it read only. This column helps view/add receipt images.
            DataGridViewTextBoxColumn imageColumn = new DataGridViewTextBoxColumn();
            imageColumn.HeaderText = "Original Document";
            imageColumn.Name = "Original";
            imageColumn.ReadOnly = true;
            frm.dgvExpenses.Columns.Add(imageColumn);
        }

        //Pre: The DGV Control showing event arg. Provides access to the current textbox in the dgv expenses.
        //Post: Fills expense categorization textbox with autocomplete values.
        //Description: If the current textbox is in the fifth (exp. categorization) column, fills it up with autcomplete values.
        public void DisplayExpenseCategorizationInfo(DataGridViewEditingControlShowingEventArgs e)
        {
            //Creates a tool tip to allow the user to see all category options.
            ToolTip expenseCategoriesTip = new ToolTip();

            //Casts the event args as a textbox.
            TextBox autoText = e.Control as TextBox;

            //Removes all existing tool tips.
            expenseCategoriesTip.RemoveAll();

            //Ensures the box isn't null
            if (autoText != null)
            {
                //Runs if the current cell is in column 5, that of the expense categorization
                if (frm.dgvExpenses.CurrentCell.ColumnIndex == 5)
                {
                    //Sets the tooltip for the text box, as being the expense categories, seperated by new lines
                    expenseCategoriesTip.SetToolTip(autoText, "Advertising\nInsurance\nInterest/bank charges\nOffice expenses\nOffice maintenance\nLegal fees and related expenses\nAccounting and other professional fees\nManagement and admin fees\nMaintenance and repair\nSalaries\nWages\nBenefits\nProperty taxes\nTravel\nUtilities\nCost of goods sold\nMotor vehicle expenses\nLodging\nParking fees\nOther misc. supplies\nUnion professional and other similar dues");

                    //If it isn't, adds autocomplete for the text box.
                    autoText.AutoCompleteMode = AutoCompleteMode.Suggest;
                    autoText.AutoCompleteSource = AutoCompleteSource.CustomSource;

                    //Creates an autocomplete collection, and associates it with the textbox.
                    AutoCompleteStringCollection DataCollection = new AutoCompleteStringCollection();
                    AutoCompleteItems(DataCollection, true);
                    autoText.AutoCompleteCustomSource = DataCollection;
                }
                //Runs if the current textbox isn't part of the expense categorization column
                else
                {
                    //Removes any tool tips
                    expenseCategoriesTip.RemoveAll();

                    //Creates a blank autocomplete collection
                    autoText.AutoCompleteMode = AutoCompleteMode.Suggest;
                    autoText.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    AutoCompleteStringCollection DataCollection = new AutoCompleteStringCollection();

                    //Associates it with the textbox to remove any autocorrect fields
                    AutoCompleteItems(DataCollection, false);
                    autoText.AutoCompleteCustomSource = DataCollection;

                }
            }
        }

        //Pre: The autcomplete collection to be modified, and whether the collection is to be added to or removed from.
        //Post: The fields are added/removed from the collection
        //Description: Based on whether the autocomplete is added to or removed from, modifies the collection with the expense categorizations.
        public void AutoCompleteItems(AutoCompleteStringCollection col, bool add)
        {
            //Executes if the fields are to be added to the collection.
            if (add)
            {
                //Adds the expense categories
                col.Add("Advertising");
                col.Add("Insurance");
                col.Add("Interest/bank charges");
                col.Add("Office expenses");
                col.Add("Office maintenance");
                col.Add("Legal fees and related expenses");
                col.Add("Accounting and other professional fees");
                col.Add("Management and admin fees");
                col.Add("Maintenance and repair");
                col.Add("Salaries");
                col.Add("Wages");
                col.Add("Benefits");
                col.Add("Property taxes");
                col.Add("Travel");
                col.Add("Utilities");
                col.Add("Cost of goods sold");
                col.Add("Motor vehicle expenses");
                col.Add("Lodging");
                col.Add("Parking fees");
                col.Add("Other misc. supplies");
                col.Add("Union professional and other similar dues");
            }
            else
            {
                //Removes the expense categories
                col.Remove("Advertising");
                col.Remove("Insurance");
                col.Remove("Interest/bank charges");
                col.Remove("Office expenses");
                col.Remove("Office maintenance");
                col.Remove("Legal fees and related expenses");
                col.Remove("Accounting and other professional fees");
                col.Remove("Management and admin fees");
                col.Remove("Maintenance and repair");
                col.Remove("Salaries");
                col.Remove("Wages");
                col.Remove("Benefits");
                col.Remove("Property taxes");
                col.Remove("Travel");
                col.Remove("Utilities");
                col.Remove("Cost of goods sold");
                col.Remove("Motor vehicle expenses");
                col.Remove("Lodging");
                col.Remove("Parking fees");
                col.Remove("Other misc. supplies");
                col.Remove("Union professional and other similar dues");
            }
        }        

        //Calls for the display of expense dgv data error (Event handler)
        public void dgvDataError(object sender, DataGridViewDataErrorEventArgs error)
        {
            data.DisplayDGVError(sender, error);
        }

        //Called upon ever tick of the timer (Event handler)
        public void TimerTick()
        {
            //Increments the number of ticks
            ++ticks;

            //Updates the form time label with the new time if need be
            frm.lblTimerTime.Text = GetTimeFormated();
        }

        //Pre: None
        //Post: The timeer is reset
        //Description: Resets the ticks, minutes and hours, stops the timer and resets button text
        public void TimerReset()
        {
            //Stops timer
            frm.tmrWorkTime.Stop();

            //Resets time elapsed
            ticks = 0;
            numMinutes = 0;
            numHours = 0;

            //Updates time labels and buttons
            frm.lblTimerTime.Text = GetTimeFormated();
            frm.btnTimerStartStop.Text = "Start";
        }

        //Pre: None
        //Post: Starts/stops the timer
        //Description: Based on the current state of the button, starts/stops the timer
        public void TimerStartStop()
        {
            //Executesf the button says start
            if (frm.btnTimerStartStop.Text == "Start" || frm.btnTimerStartStop.Text == "Start Timer")
            {
                //Starts the timer and sets the button to stop
                frm.tmrWorkTime.Start();
                frm.btnTimerStartStop.Text = "Stop";
            }
            else
            {
                //Otherwise stops the timer and sets the button to start
                frm.tmrWorkTime.Stop();
                frm.btnTimerStartStop.Text = "Start";
            }
        }

        //Pre: None
        //Post: Formats the time elapsed in minutes, seconds and hours
        //Description: Upon reaching 60 ticks, indicates a minute has passed, after 60 minutes an hour has passed. Formats this in natural language.
        private string GetTimeFormated()
        {
            //After 60 ticks indicates minute has passed.
            if (ticks == TIME_MULTIPLE)
            {
                //Increments minutes and resets ticks
                ++numMinutes;
                ticks = 0;
            }

            //After 60 minutes an hour has passed
            if (numMinutes == TIME_MULTIPLE)
            {
                //Increments hours and resets minutes
                ++numHours;
                numMinutes = 0;
            }

            //Returns the time formatted in a natural language string
            return numHours + "H. " + numMinutes + "m. " + ticks + "s.";
        }

        //Pre: None
        //Post: Submits the time elapsed specified by the user into the dgv expenses and expenses table, as a wage.
        //Description: Gets the invoice ID and time elapsed, and adds as an expense to the database and dgv expenses.
        public void SubmitTime()
        {
            //Gets today's date
            DateTime dateTime = DateTime.UtcNow.Date;

            //Gets the total time elapsed in seconds
            double timeElapsed = ticks + numMinutes * TIME_MULTIPLE + numHours * TIME_MULTIPLE * TIME_MULTIPLE;

            //Gets the houly wage, and wage by second
            double hourlyWage = Convert.ToDouble(frm.numHourlyWage.Value);
            double secondWage = hourlyWage / SECONDS_IN_HOUR;

            //Calculates the total expense
            double expense = timeElapsed * secondWage;

            //Checks if the time elapsed and hourly wage is greater than 0
            if (timeElapsed > 0d && hourlyWage > 0d)
            {
                //Sets the sql query headings and values
                string sql = "INSERT INTO expenses (Date, Description, ";
                string values = " VALUES (str_to_date('"
                    + dateTime.ToString("MM/dd/yyyy")
                    + "', '%m/%d/%Y'),";

                //Checks to see if the invoice ID is greater than 0, and if so, if it exists in the invoices table.
                if (frm.numExpenseTimeInvoiceID.Value > 0 && data.ExecuteSQLQuery("SELECT COUNT(1) FROM invoices WHERE InvoiceID = " + frm.numExpenseTimeInvoiceID.Value, "If you'd like to associate an invoice. Please enter in a valid invoice ID"))
                {
                    //If not puts a default description of working on project
                    values += "'Work on Project', ";

                    //If so adds the invoice header and value into the sql statement
                    values += Convert.ToInt32(frm.numExpenseTimeInvoiceID.Value).ToString() + ", ";
                    sql += "Invoices_InvoiceID,";
                }
                else
                {
                    //If not puts a default description of working on the company
                    values += "'Work on Company', ";
                }

                //Adds the ending of the sql statement with the remaining fields. 
                sql += "ExpenseCategory, TotalAmount, TaxAmount)";
                values += "'Wages', " + Math.Round(expense, 2).ToString() + "," + 0 + ") ";

                //Executes sql command with sql heading and values
                data.ExecuteSQLQuery(sql + values, "Invalid Invoice ID. Please enter a valid invoice ID.");

                //Updates database and dgv expenses. Also updates images column
                frm.expensesTableAdapter.Fill(frm.expensesDataSet.expenses);
                UpdateImagesColumn();
            }
            else
            {
                //Informs user of invalid distance
                MessageBox.Show("Please ensure time has elapsed and that you have set an hourly wage.");
            }
        }

        //Pre: None
        //Post: Submits the distance specified by the user into the dgv expenses and expenses table
        //Description: Gets the value listed in the expenses num box, and transfers it over to the tables/dgv expenses.
        public void SubmitDistance()
        {
            //Gets today's date
            DateTime dateTime = DateTime.UtcNow.Date;

            //Gets the distance travelled, rounded to two decimal places
            double distanceTravelled = Math.Round(Convert.ToDouble(frm.numDistance.Value), 2);

            //Checks if the distance travelled is greater than 0
            if (distanceTravelled > 0d)
            {
                //Sets the sql query headings and values
                string sql = "INSERT INTO expenses (Date, Description, ";
                string values = " VALUES (str_to_date('"
                    + dateTime.ToString("MM/dd/yyyy")
                    + "', '%m/%d/%Y'),";

                //Checks if there is a description for the distance
                if (!String.IsNullOrEmpty(frm.txtDistanceDescription.Text))
                {
                    //If so adds the description to the values
                    values += "'" + frm.txtDistanceDescription.Text + "',";
                }
                else
                {
                    //If not puts a default description
                    values += "'Travel Fuel Expense', ";
                }

                //Checks to see if the invoice ID is greater than 0, and if so, if it exists in the invoices table.
                if (frm.numDistanceInvoiceID.Value > 0 && data.ExecuteSQLQuery("SELECT COUNT(1) FROM invoices WHERE InvoiceID = " + frm.numDistanceInvoiceID.Value, "If you'd like to associate an invoice. Please enter in a valid invoice ID"))
                {
                    //If so adds the invoice header and value into the sql statement
                    values += Convert.ToInt32(frm.numDistanceInvoiceID.Value).ToString() + ", ";
                    sql += "Invoices_InvoiceID,";
                }

                //Adds the ending of the sql statement with the remaining fields. Calculates cost using cost/km specified in settings
                sql += "ExpenseCategory, TotalAmount, TaxAmount)";
                values += "'Motor Vehicle Expenses', " + Math.Round((distanceTravelled * Convert.ToDouble(data.generalSettings[SharedData.COST_PER_KM])), 2).ToString() + "," + 0 + ") ";

                //Executes sql command with sql heading and values
                data.ExecuteSQLQuery(sql + values, "Invalid Invoice ID. Please enter a valid invoice ID.");

                //Updates database and dgv expenses. Also updates images column
                frm.expensesTableAdapter.Fill(frm.expensesDataSet.expenses);
                UpdateImagesColumn();
            }
            else
            {
                //Informs user of invalid distance
                MessageBox.Show("Please enter a valid distance travelled.");
            }
        }

        //Pre: None
        //Post: Updates the images column of the dgv, with either "View" image or "Add" image.
        //Description: Based on the state of the image reference column determines what to display in each cell.
        public void UpdateImagesColumn()
        {
            //Runs for all displayed rows
            for (int i = 0; i < frm.dgvExpenses.RowCount; ++i)
            {
                //Ensures that the cell and value of the cell in the image reference isn't null. 
                if (frm.dgvExpenses.Rows[i].Cells[3].Value != null && !string.IsNullOrEmpty(frm.dgvExpenses.Rows[i].Cells[3].Value.ToString()))
                {
                    //If not then indicates image reference present, and allows the user to view image.
                    frm.dgvExpenses.Rows[i].Cells[8].Value = "View";
                }
                else
                {
                    //If so indicates that no image reference present, so allows the user to associate an image.
                    frm.dgvExpenses.Rows[i].Cells[8].Value = "Add";
                }
            }
        }

        //Called upon click of dgv cell. (Event handler)
        public void dgvCellClick(DataGridViewCellEventArgs e)
        {
            //Calls to check if the image column has been selected. If so opens the file dialog/stored image file.
            AddRemoveImage(e);
        }

        //Pre: None
        //Post: Opens the stored image file, or file dialog to associate a file, based on the current state of the image source.
        //Description: Checks if the cell click occured in the 8th (image view) column. If so based on whether an image is already associated, gets an image/opens up the existing image.
        private void AddRemoveImage(DataGridViewCellEventArgs e)
        {
            //Ensures it is the 8th image view column, and the cell isn't null
            if (e.ColumnIndex == 8 && frm.dgvExpenses.CurrentCell.Value != null)
            {
                //Checks to see if the field is currently set to add (Means no image refrence has been associated with the expense)
                if (frm.dgvExpenses.CurrentCell.Value.ToString() == "Add")
                {
                    //Opens the file dialog and waits for the user to select ok
                    if (frm.ofdExpenses.ShowDialog() == DialogResult.OK)
                    {
                        //Gets the file path selected by the user
                        string sourcePath = frm.ofdExpenses.FileName;

                        //Checks if the expense directory exists, if not creates it
                        if (!System.IO.Directory.Exists(data.databasePath + "\\Expenses"))
                        {
                            System.IO.Directory.CreateDirectory(data.databasePath + "\\Expenses");
                        }

                        //Attempts to copy over the file into the directory
                        try
                        {
                            //Copies the file over to the directory and associates the selected image with the expense by passing a refrence link
                            System.IO.File.Copy(sourcePath, data.databasePath + "\\Expenses\\" + Path.GetFileName(sourcePath));
                            frm.dgvExpenses.Rows[e.RowIndex].Cells[3].Value = Path.GetFileName(sourcePath);
                        }
                        catch (IOException)
                        {
                            //Generates a random number to append to the file name to ensure duplication prevention
                            int random = data.generator.Next(1, 99999);

                            //Copies the file over to the directory and associates the selected image with the expense by passing a refrence link
                            System.IO.File.Copy(sourcePath, data.databasePath + "\\Expenses\\" + random + Path.GetFileName(sourcePath));
                            frm.dgvExpenses.Rows[e.RowIndex].Cells[3].Value = random + Path.GetFileName(sourcePath);
                        }

                        //Attempts to build and execute the sql query to update the image refrence
                        try
                        {
                            //Creates new sql query to update image reference
                            string sql = "UPDATE expenses SET expenses.ImageReference = '"
                                        + frm.dgvExpenses.Rows[e.RowIndex].Cells[3].Value
                                        + "' WHERE expenses.ExpenseID = "
                                        + frm.dgvExpenses.Rows[e.RowIndex].Cells[0].Value + ";";

                            //Executes the query and supplies an error message
                            data.ExecuteSQLQuery(sql, " File could not be copied. Please move to another location and try again.");

                            //Updates the displayed table with the new data and indicates temp error by pass to ensure smoother experience
                            frm.expensesTableAdapter.Fill(frm.expensesDataSet.expenses);
                            UpdateImagesColumn();
                            data.tempByPass = true;

                        }
                        //Catches any exceptions and informs the user
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }

                        //Sets the value of the current cell to view
                        frm.dgvExpenses.CurrentCell.Value = "View";
                    }
                }
                //Checks if the current cell is set to view. If so, opens up the file
                else if (frm.dgvExpenses.CurrentCell.Value.ToString() == "View")
                {
                    //Attempts to open up associated file for the expense
                    try
                    {
                        System.Diagnostics.Process.Start(data.databasePath + "\\Expenses\\" + frm.dgvExpenses.Rows[e.RowIndex].Cells[3].Value.ToString());
                    }
                    catch
                    {
                        //If opening fails, indicates invalid/corrupt file, and informs the user to re-add the file. 
                        frm.dgvExpenses.CurrentCell.Value = "Add";
                        MessageBox.Show("File not found");

                        //Attempts to build and execute the sql query to remove the image refrence
                        try
                        {
                            //Creates new sql query to remove image reference
                            string sql = "UPDATE expenses SET expenses.ImageReference = NULL WHERE expenses.ExpenseID = "
                                        + frm.dgvExpenses.Rows[e.RowIndex].Cells[0].Value + ";";

                            //Executes the query and supplies an empty error message
                            data.ExecuteSQLQuery(sql, "");

                            //Updates the displayed table with the new data and indicates temp error by pass to ensure smoother experience
                            frm.expensesTableAdapter.Fill(frm.expensesDataSet.expenses);
                            UpdateImagesColumn();
                            data.tempByPass = true;

                        }
                        //Catches any exceptions and informs the user
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
            }
        }

        //Pre: None
        //Post: An expense is added into the dgv and expenses table.
        //Description: Validates form control input data, and if valid adds data into expenses table and expenses dgv.
        public void AddExpense()
        {
            //Ensures that the data collected is in the valid format/length
            bool isDataValid = false;

            //Creates variables to assist in query generation
            string newClientSql;
            string newClientValues;
            string sqlQuery;

            //Retrieves user input for client data form controls
            string date = frm.dtpExpenses.Value.Date.ToString("d", data.dateCulture);
            string description = frm.txtExpenseDescription.Text;
            string expenseCategory = frm.cmbExpenseCategory.Text;
            int invoiceID = Convert.ToInt32(frm.numExpenseInvoiceID.Value);
            double totalTax = Convert.ToDouble(frm.numExpenseTaxAmount.Value);
            double totalAmount = Convert.ToDouble(frm.numExpenseTotalAmount.Value);

            if (description.Length > 0
                && expenseCategory.Length > 0
                && totalAmount > 0
                && VerifyExpenseCategorization(expenseCategory))
            {
                //Checks if no invoice ID is entered (0) or if one is entered, it is valid (Through an sql query of the invoices table.
                if (invoiceID == 0 || data.ExecuteSQLQuery("SELECT COUNT(1) FROM invoices WHERE InvoiceID = " + invoiceID, "If you'd like to associate an invoice. Please enter in a valid invoice ID"))
                {
                    //If so, validates date
                    isDataValid = true;
                }
                else
                {
                    //If not, indicates invalid data
                    isDataValid = false;
                }
                
            }
            else
            {
                isDataValid = false;
                MessageBox.Show("Please ensure that you have filled in the mandatory fields. (Date, description, expense category, total amount, tax amount.)");
            }


            //Executes datatable update if data is valid
            if (isDataValid)
            {
                //Checks if connection is closed. If so opens it up
                if (data.connection.State == ConnectionState.Closed)
                {
                    data.connection.Open();
                }

                //Creates two sql strings to help in query creation
                //Fills first with names of values being modified, and second with values themselves
                newClientSql = "INSERT INTO expenses (Date, Description, ExpenseCategory, TaxAmount, TotalAmount";
                newClientValues = "VALUES ('" + date + "', '" + description + "', '" + expenseCategory + "', " + totalTax + "," + totalAmount;

                //Checks if the invoice ID entered is valid
                if (invoiceID > 0)
                {
                    //If not indicates the company is being added, and then adds the company name
                    newClientSql += ", Invoices_InvoiceID";
                    newClientValues += ", " + invoiceID;
                }

                //Checks if the address textbox is null or empty
                if (frm.btnExpenseAddImage.Text != "Add Image")
                {
                    //If not indicates the address is being added, and then adds the address value
                    newClientSql += ", ImageReference";
                    newClientValues += ", '" + frm.btnExpenseAddImage.Text + "'";
                }

                //Combines the name query with the value query, and includes closing brackets
                sqlQuery = newClientSql + ") " + newClientValues + ");";

                //Executes the command with the query and connection
                MySqlCommand newExpense = new MySqlCommand(sqlQuery, data.connection);
                newExpense.ExecuteNonQuery();

                //Re-fills the expense table and images column with the new data and informs user of successful entry
                frm.expensesTableAdapter.Fill(frm.expensesDataSet.expenses);
                UpdateImagesColumn();
                MessageBox.Show("Expense successfully added to the database.");
            }
        }

        //Pre: The string value of the image currently (for backup purposes)
        //Post: The image is copied over to the application folder, and the path is saved
        //Description: Opens the file dialog to get the image. Saves it in the expenses directory, with the file path. 
        public string FetchImage(string value)
        {
            //Executes if user selects an image
            if (frm.ofdExpenses.ShowDialog() == DialogResult.OK)
            {
                //Sets the source path of the image to the one that the user selected
                string sourcePath = frm.ofdExpenses.FileName;

                //Checks if expenses directory exists, if not creates it
                if (!System.IO.Directory.Exists(data.databasePath + "\\Expenses"))
                {
                    System.IO.Directory.CreateDirectory(data.databasePath + "\\Expenses");
                }

                //Attempts to copy over the image into the Expenses directory and return the image identifier
                try
                {
                    System.IO.File.Copy(sourcePath, data.databasePath + "\\Expenses\\" + Path.GetFileName(sourcePath));
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

        //Pre: None
        //Post: Updates the dgv and expenses table based on selection change.
        //Description: Determines if the expense category value has been changed, if so verifies it. Also updates and syncs the dgv and database table data.
        public void dgvSelectionChanged()
        {
            try
            {
                //Checks if the cell being worked on previously was part of the expense categorization column
                if (prevCol == 5 && frm.dgvExpenses.Rows[prevRow].Cells[prevCol].Value != null)
                {
                    //If so verifies new entry is a valid category
                    if (!VerifyExpenseCategorization(frm.dgvExpenses.Rows[prevRow].Cells[prevCol].Value.ToString()))
                    {
                        //If not returns the cell to the old value
                        frm.dgvExpenses.CurrentCell.Value = oldCellValue;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            //Attempts to update the table data based on dgv input
            try
            {
                //Ends the binding and updates the table
                data.binding.EndEdit();
                frm.expensesTableAdapter.Update(frm.expensesDataSet.expenses);

                try
                {
                    //Backs up the "previous" cell position. Previous cell now being the current cell, as that's when this event will be called again.
                    prevRow = frm.dgvExpenses.CurrentCell.RowIndex;
                    prevCol = frm.dgvExpenses.CurrentCell.ColumnIndex;
                }
                catch
                {
                    //Indicates backup failed as no previous selection. Sets default values
                    prevCol = 0;
                    prevRow = 0;
                }
            }
            catch
            {
                //Informs the user of the error if the invoice ID entered was invalid.
                MessageBox.Show("Please enter in a valid Invoice ID");
            }
        }

        //Pre: The text value being examined to see if it is a valid expense category.
        //Post: Answers whether or not it is a valid expense category in bool form.
        //Description: Runs through all the expense categories. If the input is equal to any one, returns valid categorization.
        private bool VerifyExpenseCategorization(string newValue)
        {
            //Establishes default output, invalid categorization
            bool valueVerified = false;

            //Runs for all the categories
            for (int i = 0; i < data.expenseCategories.Length; ++i)
            {
                //Checks to see if the value is equal to that expense category
                if (newValue == data.expenseCategories[i])
                {
                    //If so, indicates valid category and stops searching other entries by exiting loop
                    valueVerified = true;
                    break;
                }
            }

            //Returns whether the value has been verified as an expense category.
            return valueVerified;
        }

        //Indicates a cell has been entered in expenses dgv. (Event handler)
        public void dgvCellEnter(DataGridViewCellEventArgs e)
        {
            //Checks to see if it is the 5th column (Expense categorization)
            if (frm.dgvExpenses.CurrentCell.Value != DBNull.Value && e.ColumnIndex == 5)
            {
                //If so backs up the original value, in case the user enters an invalid expense category.
                oldCellValue = (string)frm.dgvExpenses.CurrentCell.Value;
            }
        }        
    }
}
