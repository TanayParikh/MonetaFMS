﻿/***************************************************************************************************************
 * 
    Author:         Tanay Parikh
    Project Name:   Moneta
    Description:    Expense module tracking business expenses. Also stores receipt images. Further has the 
                    ability to time work, and add distances travelled.
 * 
***************************************************************************************************************/

using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Moneta
{
    internal class ExpenseModule
    {
        //Class shared data and form variables used to access main form data
        private SharedData data;
        private FrmMain frm;
        
        //Stores the dgv's previous value's and position
        private string oldCellValue = "";
        private int prevRow;
        private int prevCol;

        //Tracks the amount of time passed for the timing sub-module
        private int ticks;
        private int numMinutes;
        private int numHours;

        //Constants to track time intervals
        private const double TIME_MULTIPLE = 60;
        private const double SECONDS_IN_HOUR = 3600;

        private readonly string[] expenseCategories = { "Advertising", "Insurance", "Interest/bank charges", 
                                                         "Office expenses", "Office maintenance", "Legal fees and related expenses", 
                                                         "Accounting and other professional fees", "Management and admin fees", 
                                                         "Maintenance and repair", "Salaries", "Wages", "Benefits", "Property taxes", 
                                                         "Travel", "Utilities", "Cost of goods sold", "Motor vehicle expenses", 
                                                         "Lodging", "Parking fees", "Other misc. supplies", 
                                                         "Union professional and other similar dues", "Telephone", 
                                                         "Internet and Communication" };

        //Class constructor with the form and shared data parameters
        public ExpenseModule(FrmMain frm, SharedData data)
        {
            //Locally stores the form and shared data
            this.frm = frm;
            this.data = data;
        }

        //Pre: None
        //Post: Intializes the images column in the data grid view. 
        //Description: Sets up the image column in the expenses dgv. Sets the expense category column width to 250.
        public void initialize()
        {
            //Sets expense category column width to 250
            frm.dgvExpenses.Columns[5].Width = 250;

            //Sets up the images column. Makes it read only. This column helps view/add receipt images.
            DataGridViewTextBoxColumn imageColumn = new DataGridViewTextBoxColumn
            {
                HeaderText = "Original Document",
                Name = "Original",
                ReadOnly = true
            };
            frm.dgvExpenses.Columns.Add(imageColumn);
        }

        //Pre: The DGV Control showing event arg. Provides access to the current textbox in the dgv expenses.
        //Post: Fills expense categorization textbox with autocomplete values.
        //Description: If the current textbox is in the fifth (exp. categorization) column, fills it up with autcomplete values.
        public void displayExpenseCategorizationInfo(DataGridViewEditingControlShowingEventArgs e)
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
                associateToolTips(expenseCategoriesTip, autoText);
            }
        }

        private void associateToolTips(ToolTip expenseCategoriesTip, TextBox autoText)
        {
            //Runs if the current cell is in column 5, that of the expense categorization
            if (frm.dgvExpenses.CurrentCell.ColumnIndex == 5)
            {
                setExpenseCategoryToolTip(expenseCategoriesTip, autoText);
            }
            //Runs if the current textbox isn't part of the expense categorization column
            else
            {
                //Removes any tool tips
                removeExpenseCategoryToolTip(expenseCategoriesTip, autoText);
            }
        }

        private void removeExpenseCategoryToolTip(ToolTip expenseCategoriesTip, TextBox autoText)
        {
            expenseCategoriesTip.RemoveAll();

            //Creates a blank autocomplete collection
            autoText.AutoCompleteMode = AutoCompleteMode.Suggest;
            autoText.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection dataCollection = new AutoCompleteStringCollection();

            //Associates it with the textbox to remove any autocorrect fields
            autoCompleteItems(dataCollection, false);
            autoText.AutoCompleteCustomSource = dataCollection;
        }

        private void setExpenseCategoryToolTip(ToolTip expenseCategoriesTip, TextBox autoText)
        {
            //Sets the tooltip for the text box, as being the expense categories, seperated by new lines
            expenseCategoriesTip.SetToolTip(autoText, string.Join(Environment.NewLine, expenseCategories));

            //If it isn't, adds autocomplete for the text box.
            autoText.AutoCompleteMode = AutoCompleteMode.Suggest;
            autoText.AutoCompleteSource = AutoCompleteSource.CustomSource;

            //Creates an autocomplete collection, and associates it with the textbox.
            AutoCompleteStringCollection dataCollection = new AutoCompleteStringCollection();
            autoCompleteItems(dataCollection, true);
            autoText.AutoCompleteCustomSource = dataCollection;
        }

        //Pre: The autcomplete collection to be modified, and whether the collection is to be added to or removed from.
        //Post: The fields are added/removed from the collection
        //Description: Based on whether the autocomplete is added to or removed from, modifies the collection with the expense categorizations.
        public void autoCompleteItems(AutoCompleteStringCollection col, bool add)
        {
            //Executes if the fields are to be added to the collection.
            if (add)
            {
                foreach (string category in expenseCategories)
                {
                    col.Add(category);
                }
            }
            else
            {
                foreach (string category in expenseCategories)
                {
                    col.Remove(category);
                }
            }
        }        

        //Calls for the display of expense dgv data error (Event handler)
        public void dgvDataError(object sender, DataGridViewDataErrorEventArgs error)
        {
            data.displayDGVError(sender, error);
        }

        //Called upon ever tick of the timer (Event handler)
        public void timerTick()
        {
            //Increments the number of ticks
            ++ticks;

            //Updates the form time label with the new time if need be
            frm.lblTimerTime.Text = getTimeFormated();
        }

        //Pre: None
        //Post: The timeer is reset
        //Description: Resets the ticks, minutes and hours, stops the timer and resets button text
        public void timerReset()
        {
            //Stops timer
            frm.tmrWorkTime.Stop();

            //Resets time elapsed
            ticks = 0;
            numMinutes = 0;
            numHours = 0;

            //Updates time labels and buttons
            frm.lblTimerTime.Text = getTimeFormated();
            frm.btnTimerStartStop.Text = "Start";
        }

        //Pre: None
        //Post: Starts/stops the timer
        //Description: Based on the current state of the button, starts/stops the timer
        public void timerStartStop()
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
        private string getTimeFormated()
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
        public void submitTime()
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
                addTimeExpense(dateTime, expense);

                //Updates database and dgv expenses. Also updates images column
                frm.expensesTableAdapter.Fill(frm.expensesDataSet.expenses);
                updateImagesColumn();
            }
            else
            {
                //Informs user of invalid distance
                MessageBox.Show("Please ensure time has elapsed and that you have set an hourly wage.");
            }
        }

        private void addTimeExpense(DateTime dateTime, double expense)
        {
            //Sets the sql query headings and values
            string sql = "INSERT INTO expenses (Date, Description, ";
            string values = " VALUES (str_to_date('"
                            + dateTime.ToString("MM/dd/yyyy")
                            + "', '%m/%d/%Y'),";

            //Checks to see if the invoice ID is greater than 0, and if so, if it exists in the invoices table.
            if (frm.numExpenseTimeInvoiceID.Value > 0 &&
                data.executeSQLQuery("SELECT COUNT(1) FROM invoices WHERE InvoiceID = " +
                                     frm.numExpenseTimeInvoiceID.Value,
                    "If you'd like to associate an invoice. Please enter in a valid invoice ID"))
            {
                //If not puts a default description of working on project
                values += "'Work on Project', ";

                //If so adds the invoice header and value into the sql statement
                values += Convert.ToInt32(frm.numExpenseTimeInvoiceID.Value) + ", ";
                sql += "Invoices_InvoiceID,";
            }
            else
            {
                //If not puts a default description of working on the company
                values += "'Work on Company', ";
            }

            //Adds the ending of the sql statement with the remaining fields. 
            sql += "ExpenseCategory, TotalAmount, TaxAmount)";
            values += "'Wages', " + Math.Round(expense, 2) + "," + 0 + ") ";

            //Executes sql command with sql heading and values
            data.executeSQLQuery(sql + values, "Invalid Invoice ID. Please enter a valid invoice ID.");
        }

        //Pre: None
        //Post: Submits the distance specified by the user into the dgv expenses and expenses table
        //Description: Gets the value listed in the expenses num box, and transfers it over to the tables/dgv expenses.
        public void submitDistance()
        {
            //Gets the distance travelled, rounded to two decimal places
            double distanceTravelled = Math.Round(Convert.ToDouble(frm.numDistance.Value), 2);

            //Checks if the distance travelled is greater than 0
            if (distanceTravelled > 0d)
            {
                addDistanceExpense(distanceTravelled);

                //Updates database and dgv expenses. Also updates images column
                frm.expensesTableAdapter.Fill(frm.expensesDataSet.expenses);
                updateImagesColumn();
            }
            else
            {
                //Informs user of invalid distance
                MessageBox.Show("Please enter a valid distance travelled.");
            }
        }

        private void addDistanceExpense(double distanceTravelled)
        {
            //Sets the sql query headings and values
            string sql = "INSERT INTO expenses (Date, Description, ";
            string values = " VALUES (str_to_date('"
                            + DateTime.UtcNow.Date.ToString("MM/dd/yyyy")
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
            if (frm.numDistanceInvoiceID.Value > 0 &&
                data.executeSQLQuery("SELECT COUNT(1) FROM invoices WHERE InvoiceID = " + frm.numDistanceInvoiceID.Value,
                    "If you'd like to associate an invoice. Please enter in a valid invoice ID"))
            {
                //If so adds the invoice header and value into the sql statement
                values += Convert.ToInt32(frm.numDistanceInvoiceID.Value) + ", ";
                sql += "Invoices_InvoiceID,";
            }

            //Adds the ending of the sql statement with the remaining fields. Calculates cost using cost/km specified in settings
            sql += "ExpenseCategory, TotalAmount, TaxAmount)";
            values += "'Motor Vehicle Expenses', " +
                      Math.Round((distanceTravelled*Convert.ToDouble(data.generalSettings[SharedData.COST_PER_KM])), 2) + "," +
                      0 + ") ";

            //Executes sql command with sql heading and values
            data.executeSQLQuery(sql + values, "Invalid Invoice ID. Please enter a valid invoice ID.");
        }

        //Pre: None
        //Post: Updates the images column of the dgv, with either "View" image or "Add" image.
        //Description: Based on the state of the image reference column determines what to display in each cell.
        public void updateImagesColumn()
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
            addRemoveImage(e);
        }

        //Pre: None
        //Post: Opens the stored image file, or file dialog to associate a file, based on the current state of the image source.
        //Description: Checks if the cell click occured in the 8th (image view) column. If so based on whether an image is already associated, gets an image/opens up the existing image.
        private void addRemoveImage(DataGridViewCellEventArgs cell)
        {
            if (cell.ColumnIndex != 8 || frm.dgvExpenses.CurrentCell.Value == null) return;

            //Checks to see if the field is currently set to add (Means no image refrence has been associated with the expense)
            if (frm.dgvExpenses.CurrentCell.Value.ToString() == "Add")
            {
                addExpenseImage(cell);
            }
            else if (frm.dgvExpenses.CurrentCell.Value.ToString() == "View")
            {
                viewExpenseImage(cell);
            }
        }

        private void viewExpenseImage(DataGridViewCellEventArgs cell)
        {
            //Attempts to open up associated file for the expense
            try
            {
                Process.Start(data.databasePath + "\\Expenses\\" + frm.dgvExpenses.Rows[cell.RowIndex].Cells[3].Value);
            }
            catch
            {
                //If opening fails, indicates invalid/corrupt file, and informs the user to re-add the file. 
                frm.dgvExpenses.CurrentCell.Value = "Add";
                MessageBox.Show("File not found");

                //Attempts to build and execute the sql query to remove the image refrence
                unreferenceImageFromExpense(cell);
            }
        }

        private void unreferenceImageFromExpense(DataGridViewCellEventArgs cell)
        {
            try
            {
                //Creates new sql query to remove image reference
                string sql = "UPDATE expenses SET expenses.ImageReference = NULL WHERE expenses.ExpenseID = "
                             + frm.dgvExpenses.Rows[cell.RowIndex].Cells[0].Value + ";";

                //Executes the query and supplies an empty error message
                data.executeSQLQuery(sql, "");

                //Updates the displayed table with the new data and indicates temp error by pass to ensure smoother experience
                frm.expensesTableAdapter.Fill(frm.expensesDataSet.expenses);
                updateImagesColumn();
                data.tempByPass = true;
            }
                //Catches any exceptions and informs the user
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void addExpenseImage(DataGridViewCellEventArgs cell)
        {
            //Opens the file dialog and waits for the user to select ok
            if (frm.ofdExpenses.ShowDialog() == DialogResult.OK)
            {
                //Gets the file path selected by the user
                string sourcePath = frm.ofdExpenses.FileName;

                createExpenseDirectoryInDataWarehouse();

                //Attempts to copy over the file into the directory
                copyExpenseFileToDataWarehouse(cell, sourcePath);

                //Attempts to build and execute the sql query to update the image refrence
                string imageReference = (string)frm.dgvExpenses.Rows[cell.RowIndex].Cells[3].Value;
                string expenseId = (string)frm.dgvExpenses.Rows[cell.RowIndex].Cells[0].Value;

                updateExpenseAddImage(imageReference, expenseId);

                //Sets the value of the current cell to view
                frm.dgvExpenses.CurrentCell.Value = "View";
            }
        }

        private void createExpenseDirectoryInDataWarehouse()
        {
            if (!Directory.Exists(data.databasePath + "\\Expenses"))
            {
                Directory.CreateDirectory(data.databasePath + "\\Expenses");
            }
        }

        private void updateExpenseAddImage(string imageReference, string expenseId)
        {
            try
            {
                //Creates new sql query to update image reference
                string sql = "UPDATE expenses SET expenses.ImageReference = '"
                             + imageReference
                             + "' WHERE expenses.ExpenseID = "
                             + expenseId + ";";

                //Executes the query and supplies an error message
                data.executeSQLQuery(sql, " File could not be copied. Please move to another location and try again.");

                //Updates the displayed table with the new data and indicates temp error by pass to ensure smoother experience
                frm.expensesTableAdapter.Fill(frm.expensesDataSet.expenses);
                updateImagesColumn();
                data.tempByPass = true;
            }
                //Catches any exceptions and informs the user
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void copyExpenseFileToDataWarehouse(DataGridViewCellEventArgs cell, string sourcePath)
        {
            try
            {
                //Copies the file over to the directory and associates the selected image with the expense by passing a refrence link
                File.Copy(sourcePath, data.databasePath + "\\Expenses\\" + Path.GetFileName(sourcePath));
                frm.dgvExpenses.Rows[cell.RowIndex].Cells[3].Value = Path.GetFileName(sourcePath);
            }
            catch (IOException)
            {
                //Generates a random number to append to the file name to ensure duplication prevention
                int random = data.generator.Next(1, 99999);

                //Copies the file over to the directory and associates the selected image with the expense by passing a refrence link
                File.Copy(sourcePath, data.databasePath + "\\Expenses\\" + random + Path.GetFileName(sourcePath));
                frm.dgvExpenses.Rows[cell.RowIndex].Cells[3].Value = random + Path.GetFileName(sourcePath);
            }
        }

        //Pre: None
        //Post: An expense is added into the dgv and expenses table.
        //Description: Validates form control input data, and if valid adds data into expenses table and expenses dgv.
        public void addExpense()
        {
            //Ensures that the data collected is in the valid format/length
            bool isDataValid = false;

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
                && verifyExpenseCategorization(expenseCategory))
            {
                //Checks if no invoice ID is entered (0) or if one is entered, it is valid (Through an sql query of the invoices table.
                if (invoiceID == 0 || 
                    data.executeSQLQuery("SELECT COUNT(1) FROM invoices WHERE InvoiceID = " 
                    + invoiceID, "If you'd like to associate an invoice. Please enter in a valid invoice ID"))
                {
                    //If so, validates date
                    isDataValid = true;
                }
                
            }
            else
            {
                MessageBox.Show("Please ensure that you have filled in the mandatory fields. (Date, description, expense category, total amount, tax amount.)");
            }


            //Executes datatable update if data is valid
            if (!isDataValid) return;

            //Checks if connection is closed. If so opens it up
            if (data.connection.State == ConnectionState.Closed)
            {
                data.connection.Open();
            }

            //Creates two sql strings to help in query creation
            insertExpenseToDatabase(date, description, expenseCategory, totalTax, totalAmount, invoiceID);

            //Re-fills the expense table and images column with the new data and informs user of successful entry
            frm.expensesTableAdapter.Fill(frm.expensesDataSet.expenses);
            updateImagesColumn();
            MessageBox.Show("Expense successfully added to the database.");
        }

        private void insertExpenseToDatabase(string date, string description, string expenseCategory, double totalTax,
            double totalAmount, int invoiceID)
        {
            string newClientSql;
            string newClientValues;
            string sqlQuery;
            
            //Fills first with names of values being modified, and second with values themselves
            newClientSql = "INSERT INTO expenses (Date, Description, ExpenseCategory, TaxAmount, TotalAmount";
            newClientValues = "VALUES ('" + date + "', '" + description + "', '" + expenseCategory + "', " + totalTax + "," +
                              totalAmount;

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
        }

        //Description: Opens the file dialog to get the image. Saves it in the expenses directory, with the file path. 
        public string fetchImage(string value)
        {
            if (frm.ofdExpenses.ShowDialog() != DialogResult.OK) return value;

            //Sets the source path of the image to the one that the user selected
            string sourcePath = frm.ofdExpenses.FileName;

            //Checks if expenses directory exists, if not creates it
            if (!Directory.Exists(data.databasePath + "\\Expenses"))
            {
                Directory.CreateDirectory(data.databasePath + "\\Expenses");
            }

            //Attempts to copy over the image into the Expenses directory and return the image identifier
            try
            {
                File.Copy(sourcePath, data.databasePath + "\\Expenses\\" + Path.GetFileName(sourcePath));
                return Path.GetFileName(sourcePath);
            }
                //Otherwise if the file can't be copied, tries copying again but with a random number attached to the end
            catch (IOException)
            {
                //Creates random num
                int random = data.generator.Next(1, 9999);

                //Saves the file path and returns to user
                File.Copy(sourcePath, data.databasePath + "\\Expenses\\" + random + Path.GetFileName(sourcePath));
                return random + Path.GetFileName(sourcePath);
            }
            //If extraordinary exception occurs (File too large in size) lets the user know.
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " Please try another image.");
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
                    if (!verifyExpenseCategorization(frm.dgvExpenses.Rows[prevRow].Cells[prevCol].Value.ToString()))
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

            updateDataTable();
        }

        private void updateDataTable()
        {
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
        private bool verifyExpenseCategorization(string newValue)
        {
            //Returns whether the value has been verified as an expense category.
            return data.expenseCategories.Any(t => newValue == t);
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
