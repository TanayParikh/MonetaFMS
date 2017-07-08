/***************************************************************************************************************
 * 
    Author:         Tanay Parikh
    Project Name:   Moneta
    Description:    Manages the invoices and items dgv. Also enables users to add items/invoices as well as
                    print and email created invoices. 
 * 
***************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using System.Diagnostics;

//Libraries for file/database input/ouput
using System.IO;
using MySql.Data.MySqlClient;

//Libraries used for PDF creation
using iTextSharp.text;
using iTextSharp.text.pdf;

//Library used for SMTP mailing
using System.Net.Mail;


namespace Moneta
{
    internal class InvoiceModule
    {
        //Form and shared data variables to store refrences from the main class
        private FrmMain frm;
        public SharedData data;

        //Valid Client IDs
        private List<int> clientIDs = new List<int>();

        //Stores the id of the invoice currently being worked on
        private int curInvoiceID;

        //Class constructor with the form and shared data passed in as refrences
        public InvoiceModule(FrmMain frmMain, SharedData data)
        {
            //Locally stores the parameters
            this.frm = frmMain;
            this.data = data;
        }

        //Pre: None
        //Post: Initializes specialty columns on the invoices dgv.
        //Description: Creates checkbox columns for the quote and paid categories. Makes the text columns for those values invisible.
        public void initialize()
        {
            //Creates a new checkbox column for the quotes. A checkbox indicates a quote, an unchecked entry indicates an invoice.
            //Creates flatstyle checkbox, sortable in nature and adds to the data grid view
            DataGridViewCheckBoxColumn quoteColumn = new DataGridViewCheckBoxColumn
            {
                HeaderText = "Quote",
                Name = "QuoteCheckbox",
                FlatStyle = FlatStyle.Standard,
                ThreeState = false
            };
            quoteColumn.CellTemplate.Style.BackColor = System.Drawing.Color.LightBlue;
            quoteColumn.SortMode = DataGridViewColumnSortMode.Automatic;
            frm.dgvInvoices.Columns.Add(quoteColumn);

            //Creates a new checkbox column indicating whether the invoice has been paid or not.
            //Creates flatstyle checkbox, sortable in nature and adds to the data grid view
            DataGridViewCheckBoxColumn paidColumn = new DataGridViewCheckBoxColumn
            {
                HeaderText = "Paid",
                Name = "PaidCheckbox",
                FlatStyle = FlatStyle.Standard,
                ThreeState = false
            };
            paidColumn.CellTemplate.Style.BackColor = System.Drawing.Color.LightBlue;
            paidColumn.SortMode = DataGridViewColumnSortMode.Automatic;
            frm.dgvInvoices.Columns.Add(paidColumn);

            //Makes the natural language quote and paid columns invisible. Access will be through the checkboxes
            frm.dgvInvoices.Columns[3].Visible = false;
            frm.dgvInvoices.Columns[4].Visible = false;

            //Disables the ability to add or remove rows from the invoices dgv
            frm.dgvInvoices.AllowUserToDeleteRows = false;
        }

        //Indicates that a cell has been clicked in the invoices dgv
        public void dgvCellClick()
        {
            //Calls for the display of the appropriate list of items in the items dgv
            displayExistingInvoice();
        }

        // Pre: None
        // Post: Invoices view is displayed.
        // Description: Makes all CreateEditInvoice elements invisible.
        public void returnToInvoices()
        {
            frm.pnlCreateEditInvoice.Visible = false;
        }

        //Pre: None
        //Post: Calls for the display of all invoice data of the invoice selected.
        //Description: Makes UI elements visible and populates data. 
        private void displayExistingInvoice()
        {
            //If the cell selected's column index is equal to 0 (invoice id column), executes
            if (frm.dgvInvoices.CurrentCell.ColumnIndex.Equals(0))
            {
                //Executes when the current cell, and it's value aren't null, and the invoice ID is present
                if (frm.dgvInvoices.CurrentCell != null && frm.dgvInvoices.CurrentCell.Value != null && Convert.ToInt32(frm.dgvInvoices.CurrentCell.Value) > -1)
                {
                    frm.pnlCreateEditInvoice.Visible = true;

                    // Resets the client autofill box
                    frm.txtInvoiceClientName.Text = string.Empty;

                    displayItems();
                }
            }
        }

        //Pre: None
        //Post: Calls for the display of items, and sets up the invoice to be edited
        //Description: Arranges for the form controls to show invoice data, and for the items of the invoice to be displayed. 
        private void displayItems()
        {
            //Makes the create edit invoice label visible and sets the invoice id and client id label/num box
            frm.lblCreateEditInvoiceItems.Visible = true;
            frm.lblInvoiceIDNum.Text = frm.dgvInvoices.CurrentCell.Value.ToString();
            frm.numClientID.Value = Convert.ToDecimal(frm.dgvInvoices.Rows[frm.dgvInvoices.CurrentCell.RowIndex].Cells[2].Value);

            //Enables the updating of the invoice
            // frm.btnUpdateExistingInvoice.Visible = true;

            //Using data connection, executes command
            //MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM items WHERE Invoices_InvoiceID = " + curInvoiceID, data.connection);
            //int numEntriesDatabase = Convert.ToInt32(cmd.ExecuteScalar());

            //Gets the state of the fifth columns check box (quote), if pressed, indicates quote
            if (frm.dgvInvoices.Rows[frm.dgvInvoices.CurrentCell.RowIndex].Cells[5].Value != null && frm.dgvInvoices.Rows[frm.dgvInvoices.CurrentCell.RowIndex].Cells[5].Value.Equals(true))
            {
                frm.cbxQuote.Checked = true;
            }
            //If not indicates invoice
            else
            {
                frm.cbxQuote.Checked = false;
            }

            //Gets the state of the sixth column checkbox (paid), if pressed, indicates invoice paid
            if (frm.dgvInvoices.Rows[frm.dgvInvoices.CurrentCell.RowIndex].Cells[6].Value != null && frm.dgvInvoices.Rows[frm.dgvInvoices.CurrentCell.RowIndex].Cells[6].Value.Equals(true))
            {
                frm.cbxPaid.Checked = true;
            }
            //If not indicates unpaid
            else
            {
                frm.cbxPaid.Checked = false;
            }

            //Sets the date to be equal to that of the dgv invoice date value. Stores as array splitting at the '/' delimiter
            string[] date = frm.dgvInvoices.Rows[frm.dgvInvoices.CurrentCell.RowIndex].Cells[1].Value.ToString().Split('/', ' ');

            //Stores date in a date time picker
            frm.dtpDate.Value = new DateTime(Convert.ToInt32(date[2]), Convert.ToInt32(date[0]), Convert.ToInt32(date[1]));

            //Fills the invoice dgv with the appropriate invoice items
            fillItemsData(Convert.ToInt32(frm.dgvInvoices.CurrentCell.Value));
        }

        //Pre: None
        //Post: Displays the appropriate list of items in the dgv, based on the invoice selected.
        //Description: Fetches the list of items from items datatable in the database. Displays items with prices and taxes
        private void fillItemsData(int invoiceID)
        {
            //Uses the data connection
            using (data.connection)
            {
                //Clears the current dataset being worked with
                data.itemsDataset.Clear();

                //Uses the data adapter and sets the sql command to find all the values in the items table where the invoice id equals the one specified.
                using (data.adapter.SelectCommand = new MySqlCommand("SELECT items.ItemID, items.Item, items.Price, items.TaxPercentage FROM items WHERE items.Invoices_InvoiceID =" + invoiceID.ToString() + ";", data.connection))
                {
                    //Attempts to fill the data adapter with the given values
                    try
                    {
                        //Associates the item dataset with the column found
                        data.adapter.Fill(data.itemsDataset, "ItemID");
                        data.adapter.Fill(data.itemsDataset, "Item");
                        data.adapter.Fill(data.itemsDataset, "Price");
                        data.adapter.Fill(data.itemsDataset, "TaxPercentage");
                    }
                    //If an error occurs, informs the user of its nature.
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                    //Associates the dgv items datasource to that of the items data set
                    frm.dgvItems.DataSource = data.itemsDataset;

                    //Sets the columns of the items dgv
                    frm.dgvItems.DataMember = "ItemID";
                    frm.dgvItems.DataMember = "Item";
                    frm.dgvItems.DataMember = "Price";
                    frm.dgvItems.DataMember = "TaxPercentage";

                    //Sets the widths of the dgv items columns, as well as the header texts. Makes item ID invisible
                    frm.dgvItems.Columns[0].Visible = false;
                    frm.dgvItems.Columns[3].HeaderText = "Tax %";
                    frm.dgvItems.Columns[1].Width = 450;
                    frm.dgvItems.Columns[2].Width = 125;
                    frm.dgvItems.Columns[3].Width = 125;
                }
            }

            //Sets the current invoice ID equal to that of the one fed in as a paramter. Used for calculation purposes later on.
            curInvoiceID = invoiceID;
        }

        //Pre: None
        //Post: The items datatable values are updated in accordance with dgv items
        //Description: Connects to the datatable and updates values with the same invoice ID
        public void dgvItemsSelectionChanged()
        {
            //Local sql string to store the command
            string sql = "";

            //Ends all open processes with the binding
            data.binding.EndEdit();

            //Checks if connection is closed, if so opens it
            if (data.connection.State == ConnectionState.Closed)
            {
                data.connection.Open();
            }

            //Ensures that the current cell isn't null before entering
            if (frm.dgvItems.CurrentCell != null)
            {
                //Attempts to update/add to the data table
                try
                {
                    //Using data connection, executes command
                    MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM items WHERE Invoices_InvoiceID = " + curInvoiceID, data.connection);
                    int numEntriesDatabase = Convert.ToInt32(cmd.ExecuteScalar());
                    int numEntriesDGV = 0;

                    //Runs for all the items in items dgv
                    for (int i = 0; i < frm.dgvItems.RowCount; ++i)
                    {
                        //Attempts to count all the valid items
                        try
                        {
                            //Ensures item is not null and not empty. If so, adds to total num entries.
                            if (!string.IsNullOrEmpty(frm.dgvItems.Rows[i].Cells[1].Value.ToString()))
                            {
                                numEntriesDGV++;
                            }
                        }
                        //Breaks when an invalid item has been reached
                        catch
                        {
                            break;
                        }
                    }

                    //Checks if the database and dgv both have the same number of entries
                    if (numEntriesDatabase < numEntriesDGV)
                    {
                        //Ensures that the fields are all not empty
                        if (frm.dgvItems.Rows[frm.dgvItems.CurrentCell.RowIndex].Cells[1].Value.ToString().Length > 0
                            && frm.dgvItems.Rows[frm.dgvItems.CurrentCell.RowIndex].Cells[2].Value.ToString().Length > 0
                            && frm.dgvItems.Rows[frm.dgvItems.CurrentCell.RowIndex].Cells[3].Value.ToString().Length > 0)
                        {
                            //Creates an sql command with the dgv items field values
                            sql = "INSERT INTO items (Item, Price, TaxPercentage, Invoices_InvoiceID) VALUES ('"
                                + frm.dgvItems.Rows[frm.dgvItems.CurrentCell.RowIndex].Cells[1].Value.ToString()
                                + "', "
                                + frm.dgvItems.Rows[frm.dgvItems.CurrentCell.RowIndex].Cells[2].Value.ToString()
                                + ", "
                                + frm.dgvItems.Rows[frm.dgvItems.CurrentCell.RowIndex].Cells[3].Value.ToString()
                                + ", "
                                + curInvoiceID
                                + ");";

                            //Runs the sql command with the data connection
                            MySqlCommand insertCmd = new MySqlCommand(sql, data.connection);
                            insertCmd.ExecuteNonQuery();

                            //Runs the sql command with the data connection
                            MySqlCommand getNewItemID = new MySqlCommand("SELECT ItemID FROM items ORDER BY ItemID DESC LIMIT 1;", data.connection);
                            frm.dgvItems.Rows[frm.dgvItems.CurrentCell.RowIndex].Cells[0].Value = Convert.ToInt32(getNewItemID.ExecuteScalar());
                            
                        }
                    }
                    //Indicates item needs to be updated. Updates item. 
                    else if (numEntriesDatabase == numEntriesDGV)
                    {
                        //Ensures that the fields are all not empty
                        if (frm.dgvItems.Rows[frm.dgvItems.CurrentCell.RowIndex].Cells[1].Value.ToString().Length > 0
                            && frm.dgvItems.Rows[frm.dgvItems.CurrentCell.RowIndex].Cells[2].Value.ToString().Length > 0
                            && frm.dgvItems.Rows[frm.dgvItems.CurrentCell.RowIndex].Cells[3].Value.ToString().Length > 0)
                        {
                            //Reads in values from dgv and creates sql command
                            sql = "UPDATE items SET items.Item = '"
                                    + frm.dgvItems.Rows[frm.dgvItems.CurrentCell.RowIndex].Cells[1].Value.ToString()
                                    + "', items.Price = "
                                    + frm.dgvItems.Rows[frm.dgvItems.CurrentCell.RowIndex].Cells[2].Value.ToString()
                                    + ", items.TaxPercentage = "
                                    + frm.dgvItems.Rows[frm.dgvItems.CurrentCell.RowIndex].Cells[3].Value.ToString()
                                    + " WHERE items.ItemID ="
                                    + frm.dgvItems.Rows[frm.dgvItems.CurrentCell.RowIndex].Cells[0].Value.ToString()
                                    + ";";

                            //Using data connection, executes command
                            MySqlCommand updateCmd = new MySqlCommand(sql, data.connection);
                            updateCmd.ExecuteNonQuery();
                        }
                        else
                        {
                            //Informs user that not all columns are filled
                            MessageBox.Show("Please fill all fields of the item. To remove an item select the row and press the delete key.");
                        }
                    }
                }
                //Lets the user know of the exception
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        //Pre: The event args and sender for the action.
        //Post: Updates the existing invoice data in dgv invoices based on form controls.
        //Description: Reads in form controls and updates the dgv invoices.
        public void updateExistingInvoice(object sender, EventArgs e)
        {
            //Based on whether or not the checkbox for the quote is selected in the form control, updates accordingly in dgv.
            frm.dgvInvoices.Rows[frm.dgvInvoices.CurrentCell.RowIndex].Cells[5].Value = frm.cbxQuote.Checked;

            //Based on whether or not the checkbox for the paid field is selected in the form control, updates accordingly in dgv.
            frm.dgvInvoices.Rows[frm.dgvInvoices.CurrentCell.RowIndex].Cells[6].Value = frm.cbxPaid.Checked;

            //Reads the checkboxes, and updates the natural language equivalent boxes
            readInvoiceCheckboxes();

            //Updates the date in the dgv to that specified in the date time picker
            frm.dgvInvoices.Rows[frm.dgvInvoices.CurrentCell.RowIndex].Cells[1].Value = frm.dtpDate.Value.Month + "/" + frm.dtpDate.Value.Day + "/" + frm.dtpDate.Value.Year + " 12:00:00 AM";

            //Rebinds the table (Updates dgv data to dataset in database)
            data.binding.EndEdit();
            frm.invoicesTableAdapter.Update(frm.invoicesDataSet.invoices);
        }

        public void setupNewInvoice()
        {
            // Disables print and email buttons till invoice is created
            frm.btnPrintInvoice.Enabled = false;
            frm.btnEmailInvoice.Enabled = false;

            // Hides the items dgv as well
            frm.lblCreateEditInvoiceItems.Visible = false;
            frm.dgvItems.Visible = false;

            // Sets the invoice id to a new num
            frm.lblInvoiceIDNum.Text = "New Invoice";

            // Makes add items button visible
            frm.btnCreateEditAddItems.Visible = true;

            // Sets the dtp date to today
            frm.dtpDate.Value = DateTime.Now;

            // Resets the client autofill box
            frm.txtInvoiceClientName.Text = string.Empty;

            // Makes the create edit invoice panel visible
            frm.pnlCreateEditInvoice.Visible = true;

            // Makes the add items button visible
            frm.btnCreateEditAddItems.Visible = true;
        }

        //Pre: None
        //Post: Adds an invoice, with the specifications selected to the invoices data table
        //Description: Fetches data from form controls, and creates an sql query to add the invoiceinto the database.
        public void createInvoice()
        {
            //Creates local variables to store control values
            string quote;
            string paid;
            string date;
            string clientID;

            //Creates a string to store the sql command
            string sql = "";

            //Determines the press state of the checkbox, and sets the string quote variable accordingly. 
            quote = frm.cbxQuote.Checked ? "Quote" : "Invoice";

            //Determines the press state of the checkbox, and sets the string paid variable accordingly. 
            paid = frm.cbxPaid.Checked ? "Yes" : "No";

            //Gets the date from the date time picker, formatted as needed
            date = frm.dtpDate.Value.Month + "/" + frm.dtpDate.Value.Day + "/" + frm.dtpDate.Value.Year;

            //Stores the client id from the client id num box.
            clientID = Convert.ToString(Convert.ToInt32(frm.numClientID.Value));

            //Creates the sql statement with the needed fields
            sql = "INSERT INTO invoices (Date, Clients_ClientID, Type, Paid) VALUES (str_to_date('"
                        + date
                        + "', '%m/%d/%Y'), "
                        + clientID
                        + ", '"
                        + quote
                        + "', '"
                        + paid
                        + "');";

            //Calls for the execution of the query, with a error message passed in
            if (data.executeSQLQuery(sql, "Invalid Client ID, please enter a valid client id."))
            {
                //If the query was successfully performed, re-enables the update client button.
                // frm.btnUpdateExistingInvoice.Visible = true;

                if (frm.dgvInvoices.RowCount > 2)
                {
                    //Sets the current cell to that of the last row, and displays the new invoice's items
                    frm.dgvInvoices.CurrentCell = this.frm.dgvInvoices[0, frm.dgvInvoices.RowCount - 2];
                }

                // Enables print and email buttons
                frm.btnPrintInvoice.Enabled = true;
                frm.btnEmailInvoice.Enabled = true;

                // Makes the items dgv visible
                frm.lblCreateEditInvoiceItems.Visible = true;
                frm.dgvItems.Visible = true;

                // Hides add items button
                frm.btnCreateEditAddItems.Visible = true;

                //Checks if connection is closed, if so opens it up
                if (data.connection.State == ConnectionState.Closed)
                {
                    data.connection.Open();
                }

                // Updates the invoice label to show the new invoice number
                MySqlCommand cmd = new MySqlCommand("SELECT MAX(InvoiceID) FROM Invoices;", data.connection);
                int invoiceID = Convert.ToInt32(cmd.ExecuteScalar());
                frm.lblInvoiceIDNum.Text = invoiceID.ToString();

                // Fills items dgv and makes add items button invisible
                fillItemsData(invoiceID);
                frm.btnCreateEditAddItems.Visible = false;

                //Informs the user that the invoice has been created.
                MessageBox.Show("Invoice Created! Add items to \nthe invoice with the table below.");
            }

            //Fills the invoice checkboxes after refreshing the table with the new values.
            frm.invoicesTableAdapter.Fill(frm.invoicesDataSet.invoices);
            updateInvoiceCheckboxes();

            //Deselects the first cell, and selects the row just added in, so to display the appropriate items table
            frm.dgvInvoices.Rows[0].Selected = false;
            frm.dgvInvoices.Rows[frm.dgvInvoices.RowCount - 1].Selected = true;
        }

        //Pre: The event args storing the column index of the column the user attempted to sort
        //Post: The column is sorted
        public void sortInvoices(DataGridViewCellMouseEventArgs e)
        {
            //If the column was #5 (Quotes/Invoices)
            if (e.ColumnIndex == 5)
            {
                //Depended on the current sort order, sorts the text version of that column (source of checkbox data) inversely
                if (frm.dgvInvoices.SortOrder == SortOrder.Descending || frm.dgvInvoices.SortOrder == SortOrder.None)
                {
                    frm.dgvInvoices.Sort(frm.dgvInvoices.Columns[3], ListSortDirection.Ascending);
                }
                else
                {
                    frm.dgvInvoices.Sort(frm.dgvInvoices.Columns[3], ListSortDirection.Descending);
                }
            }
            //If the column was #6 (Paid/Unpaid)
            else if (e.ColumnIndex == 6)
            {
                //Depended on the current sort order, sorts the text version of that column (source of checkbox data) inversely
                if (frm.dgvInvoices.SortOrder == SortOrder.Descending || frm.dgvInvoices.SortOrder == SortOrder.None)
                {
                    frm.dgvInvoices.Sort(frm.dgvInvoices.Columns[4], ListSortDirection.Ascending);
                }
                else
                {
                    frm.dgvInvoices.Sort(frm.dgvInvoices.Columns[4], ListSortDirection.Descending);
                }
            }

            //Updates the checkboxes based on newly sorted arrangement
            updateInvoiceCheckboxes();
        }

        //Calls for the generation of the invoice with emailing (Button Press Event handler)
        public void printInvoice()
        {
            generateDoc(false);
        }

        //Calls for the generation of the invoice with emailing (Button Press Event handler)
        public void sendInvoice()
        {
            generateDoc(true);
        }

        //Pre: A boolean representing whether or not to email the generated doc to the client.
        //Post: Generates the PDF, and based on parameters emails/opens it up
        //Description: Using the iTextSharp dll, creates a PDF of the doc, sourcing data from the invoices, items and clients database
        private void generateDoc(bool toEmail)
        {
            updateExistingInvoice(null, null);

            //Ensures an invoice has been selected
            if (frm.lblInvoiceIDNum.Text != "Select an Invoice")
            {
                //Difference between source PSD page pixel resolution and itextsharp page dimensions
                const float PAGE_SCALE_FACTOR = (612f/1275f);

                //Creates a new PDF doc of letter size
                Document doc = new Document(iTextSharp.text.PageSize.LETTER, 117 * PAGE_SCALE_FACTOR, 117 * PAGE_SCALE_FACTOR, 0, 0);

                //Sets document font
                string fontpath = Application.StartupPath + "\\Resources\\Lato-Regular.ttf";
                BaseFont lato = BaseFont.CreateFont(fontpath, BaseFont.CP1252, BaseFont.EMBEDDED);
                iTextSharp.text.Font lato7 = new iTextSharp.text.Font(lato, 7);
                iTextSharp.text.Font lato8 = new iTextSharp.text.Font(lato, 8);
                iTextSharp.text.Font lato9 = new iTextSharp.text.Font(lato, 9, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.GRAY);
                iTextSharp.text.Font lato12 = new iTextSharp.text.Font(lato, 12);
                iTextSharp.text.Font lato22 = new iTextSharp.text.Font(lato, 22);
                iTextSharp.text.Font lato30 = new iTextSharp.text.Font(lato, 30);

                //Variables storing information to pass onto emailing sub-program
                string invoicePath;
                string clientEmail = "";

                //Variables to store the invoice price and tax totals
                double totalPrice = 0;
                double totalTax = 0;

                //Checks if invoice export directory is present, if not creates it.
                if (!System.IO.Directory.Exists(data.databasePath + "\\InvoiceExport"))
                {
                    System.IO.Directory.CreateDirectory(data.databasePath + "\\InvoiceExport");
                }

                PdfWriter writer;

                //Tries to create the invoice path and a filestream with the pdf writer with that path.
                try
                {
                    invoicePath = data.databasePath + "\\InvoiceExport\\" + data.generalSettings[SharedData.COMPANY_NAME] + "_" + frm.lblInvoiceIDNum.Text + ".pdf";
                    writer = PdfWriter.GetInstance(doc, new FileStream(invoicePath, FileMode.Create));
                }
                //If path is in-accessible, updates the path with a random number appended, and creates the pdf writer
                catch
                {
                    invoicePath = data.databasePath + "\\InvoiceExport\\" + data.generalSettings[SharedData.COMPANY_NAME] + "_" + data.generator.Next(1, 9999).ToString() + "_" + frm.lblInvoiceIDNum.Text + ".pdf";
                    writer = PdfWriter.GetInstance(doc, new FileStream(invoicePath, FileMode.Create));
                }

                writer.CompressionLevel = PdfStream.BEST_COMPRESSION;

                //Opens up the document
                doc.Open();

                //Attempts to source and scale the company logo
                try
                {
                    //Opens the logo and scales to 100px in height
                    iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(data.databasePath + "//Images//" + data.generalSettings[SharedData.COMPANY_LOGO_PATH]);
                    float logoHeightMultiple = logo.Height / 80f;
                    logo.ScaleAbsolute(logo.Width / logoHeightMultiple, logo.Height / logoHeightMultiple);
                    logo.SetAbsolutePosition(830 * PAGE_SCALE_FACTOR, PageSize.LETTER.Height - 137 * PAGE_SCALE_FACTOR - logo.Height / logoHeightMultiple);

                    //Adds to the document
                    doc.Add(logo);
                }
                catch
                {
                    //Indicates logo couldn't be opened
                    MessageBox.Show("Logo not found at the specified location. Please check the logo location, in the settings tab. Document will print without a logo.");
                }

                //Checks if connection is closed, if so opens it up
                if (data.connection.State == ConnectionState.Closed)
                {
                    data.connection.Open();
                }

                //Stores invoice identification information
                string invoiceType = string.Empty;
                string invoiceID = string.Empty;
                string date = string.Empty;

                //Uses an sql command to select all columns from the invoices table where the invoice ID is a match
                using (MySqlCommand command = new MySqlCommand("SELECT * FROM invoices WHERE invoices.InvoiceID = " + frm.lblInvoiceIDNum.Text, data.connection))
                {
                    //Executes the command
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        //Reads all the lines
                        while (reader.Read())
                        {
                            //Adds the type to the invoice (Quote/Invoice), as well as invoice ID
                            invoiceType = reader["Type"].ToString();
                            invoiceID = reader["InvoiceID"].ToString();

                            //Stores the date, and formats it, such that the final time part of the datetime is cut off
                            date = reader["date"].ToString();
                            date = date.Substring(0, date.IndexOf(' '));
                        }
                    }
                }

                // Prints Document
                try
                {
                    // Adds header image to document based on active theme
                    iTextSharp.text.Image header = iTextSharp.text.Image.GetInstance(Application.StartupPath + "\\Resources\\Invoices\\T" + data.invoiceTemplate + "\\T" + data.invoiceTemplate + "_Header.JPG");
                    header.ScaleAbsolute(header.Width * PAGE_SCALE_FACTOR, header.Height * PAGE_SCALE_FACTOR);
                    header.SetAbsolutePosition(0, PageSize.LETTER.Height - header.Height * PAGE_SCALE_FACTOR);
                    doc.Add(header);


                    // Adds invoice id to document
                    doc.Add(new Paragraph("  "));
                    doc.Add(new Paragraph("  "));
                    doc.Add(new Paragraph("  "));
                    doc.Add(new Paragraph("  "));
                    doc.Add(new Paragraph("  "));

                    Chunk invoiceTitle;

                    if (invoiceType == "Invoice")
                    {
                        invoiceTitle = new Chunk("Invoice ", lato22);
                    }
                    else
                    {
                        invoiceTitle = new Chunk("Quote ", lato22);
                    }

                    Chunk invoiceNumber = new Chunk(invoiceID.PadLeft(4, '0'), lato30);
                    Phrase invoiceIDElement = new Phrase();
                    invoiceIDElement.Add(invoiceTitle);
                    invoiceIDElement.Add(invoiceNumber);
                    doc.Add(invoiceIDElement);


                    // Adds formatted date to doc
                    doc.Add(new Paragraph("  "));
                    doc.Add(new Paragraph(Convert.ToDateTime(date).ToLongDateString(), lato9));


                    // Adds contact info image to document based on active theme
                    iTextSharp.text.Image contactInfo = iTextSharp.text.Image.GetInstance(Application.StartupPath + "\\Resources\\Invoices\\T" + data.invoiceTemplate + "\\T" + data.invoiceTemplate + "_Contact.JPG");
                    contactInfo.ScaleAbsolute(contactInfo.Width * PAGE_SCALE_FACTOR, contactInfo.Height * PAGE_SCALE_FACTOR);
                    contactInfo.SetAbsolutePosition(0, PageSize.LETTER.Height - (contactInfo.Height + 360) * PAGE_SCALE_FACTOR);
                    doc.Add(contactInfo);


                    // Adds sending company contact information
                    PdfContentByte cb = writer.DirectContent;
                    ColumnText fromContactInfo = new ColumnText(cb);
                    fromContactInfo.SetSimpleColumn(new Phrase(new Chunk(data.generalSettings[SharedData.COMPANY_NAME] + "\n" + data.generalSettings[SharedData.COMPANY_PHONE_NUMBER] + "\n" + data.generalSettings[SharedData.COMPANY_ADDRESS], lato7)),
                                       180 * PAGE_SCALE_FACTOR,
                                       PageSize.LETTER.Height - 570 * PAGE_SCALE_FACTOR,
                                       620 * PAGE_SCALE_FACTOR,
                                       PageSize.LETTER.Height - 418 * PAGE_SCALE_FACTOR,
                                       14, Element.ALIGN_LEFT | Element.ALIGN_TOP);
                    fromContactInfo.Go();


                    // Gets client contact information
                    string toContactInfoStr = string.Empty;
                    #region GetClientInformation
                    //Gets all the columns from the clients table where the client ID is a match
                    using (MySqlCommand command = new MySqlCommand("SELECT * FROM clients WHERE clients.ClientID = " + frm.numClientID.Value, data.connection))
                    {
                        //Executes the command
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            //Reads all the rows
                            while (reader.Read())
                            {
                                //Adds client id and client name to the doc
                                //If the client's company isn't blank, adds to the doc
                                if (!string.IsNullOrEmpty(reader["Company"].ToString()))
                                {
                                    toContactInfoStr = reader["Company"].ToString() + "\n";
                                }

                                toContactInfoStr += reader["FirstName"].ToString() + " " + reader["LastName"].ToString() + "\n";

                                //Adds the client's email and phone number to the doc
                                clientEmail = reader["Email"].ToString();
                                toContactInfoStr += String.Format("{0:(###) ###-####}", Convert.ToInt64(reader["Phone"].ToString().Replace(" ", ""))) + "\n";

                                //If the address has been entered into the db, adds into the doc
                                if (!string.IsNullOrEmpty(reader["Address"].ToString()))
                                {
                                    string address = reader["Address"].ToString();

                                    if (address.Split(',').Length >= 2)
                                    {
                                        // Creates string address
                                        string[] addressLines = address.Split(',');

                                        // Displays the first line seperately
                                        toContactInfoStr += addressLines[0] + ",\n";

                                        // Runs through the rest of the address
                                        for (int i = 1; i < addressLines.Length; ++i)
                                        {
                                            // Spaces out commas appropriately
                                            if (i != addressLines.Length - 1)
                                            {
                                                toContactInfoStr += addressLines[i] + ", ";
                                            }
                                            else
                                            {
                                                toContactInfoStr += addressLines[i];
                                            }
                                        }
                                    }
                                    else
                                    {
                                        // Otherwise displays regular address without breaking up. 
                                        toContactInfoStr += address;
                                    }
                                }
                            }
                        }
                    }
                    #endregion GetClientInformation


                    // Adds client contact info to doc
                    ColumnText toContactInfo = new ColumnText(cb);
                    toContactInfo.SetSimpleColumn(new Phrase(new Chunk(toContactInfoStr, lato7)),
                                       700 * PAGE_SCALE_FACTOR,
                                       PageSize.LETTER.Height - 570 * PAGE_SCALE_FACTOR,
                                       1145 * PAGE_SCALE_FACTOR,
                                       PageSize.LETTER.Height - 418 * PAGE_SCALE_FACTOR,
                                       14, Element.ALIGN_LEFT | Element.ALIGN_TOP);
                    toContactInfo.Go();


                    //Adds additional spacing before item data
                    doc.Add(new Paragraph("  "));
                    doc.Add(new Paragraph("  "));
                    doc.Add(new Paragraph("  "));
                    doc.Add(new Paragraph("  "));
                    doc.Add(new Paragraph("  "));
                    doc.Add(new Paragraph("  "));
                    doc.Add(new Paragraph("  "));
                    doc.Add(new Paragraph("  "));
                    doc.Add(new Paragraph("  "));
                    doc.Add(new Paragraph("ITEMS", lato12));
                    doc.Add(new Paragraph("  "));
                    doc.Add(new Paragraph("  "));


                    // Adds items header image to document based on active theme
                    iTextSharp.text.Image itemsHeader = iTextSharp.text.Image.GetInstance(Application.StartupPath + "\\Resources\\Invoices\\T" + data.invoiceTemplate + "\\T" + data.invoiceTemplate + "_ItemsHeader.JPG");
                    itemsHeader.ScaleAbsolute(itemsHeader.Width * PAGE_SCALE_FACTOR, itemsHeader.Height * PAGE_SCALE_FACTOR);
                    itemsHeader.SetAbsolutePosition(0, PageSize.LETTER.Height - (itemsHeader.Height + 702) * PAGE_SCALE_FACTOR);
                    doc.Add(itemsHeader);


                    int numItems = 0;
                    string description = string.Empty;
                    string price = string.Empty;


                    //Using sql, finds all the columns in the items table where the invoice ID is a match
                    using (MySqlCommand command = new MySqlCommand("SELECT * FROM items WHERE items.Invoices_InvoiceID = " + frm.lblInvoiceIDNum.Text, data.connection))
                    {
                        //Executes the command
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            //Reads each row
                            while (reader.Read())
                            {
                                //Adds the item description, as well as price and tax percentage, formated as text, currency and percent, repectably
                                description += reader["Item"].ToString() + "\n";
                                price += String.Format("{0:C}", Convert.ToDouble(reader["Price"].ToString())) + "\n";

                                //Adds the item price and tax total to the invoice totals
                                totalPrice += Convert.ToDouble(reader["Price"]);
                                totalTax += (Convert.ToDouble(reader["TaxPercentage"].ToString()) / 100d) * Convert.ToDouble(reader["Price"]);

                                // Adds items row image to document based on active theme
                                iTextSharp.text.Image itemRow = iTextSharp.text.Image.GetInstance(Application.StartupPath + "\\Resources\\Invoices\\T" + data.invoiceTemplate + "\\T" + data.invoiceTemplate + "_ItemsRow.JPG");
                                itemRow.ScaleAbsolute(itemRow.Width * PAGE_SCALE_FACTOR, itemRow.Height * PAGE_SCALE_FACTOR);
                                itemRow.SetAbsolutePosition(0, PageSize.LETTER.Height - (itemRow.Height + 755 + numItems * itemRow.Height) * PAGE_SCALE_FACTOR);
                                doc.Add(itemRow);

                                ++numItems;
                            }
                        }
                    }


                    // Adds item footer image to document based on active theme
                    iTextSharp.text.Image itemFooter = iTextSharp.text.Image.GetInstance(Application.StartupPath + "\\Resources\\Invoices\\T" + data.invoiceTemplate + "\\T" + data.invoiceTemplate + "_ItemsFooter.JPG");
                    itemFooter.ScaleAbsolute(itemFooter.Width * PAGE_SCALE_FACTOR, itemFooter.Height * PAGE_SCALE_FACTOR);
                    itemFooter.SetAbsolutePosition(0, PageSize.LETTER.Height - (itemFooter.Height + 755 + (numItems) * 69) * PAGE_SCALE_FACTOR);
                    doc.Add(itemFooter);


                    float endOfItemsPos = (itemFooter.Height + 755 + (numItems) * 69);


                    // Adds description info to doc
                    ColumnText descriptionCol = new ColumnText(cb);
                    descriptionCol.SetSimpleColumn(new Phrase(new Chunk(description, lato9)),
                                       195 * PAGE_SCALE_FACTOR,
                                       PageSize.LETTER.Height - (780 + numItems * 69) * PAGE_SCALE_FACTOR,
                                       975 * PAGE_SCALE_FACTOR,
                                       PageSize.LETTER.Height - (729) * PAGE_SCALE_FACTOR,
                                       33, Element.ALIGN_LEFT | Element.ALIGN_TOP);
                    descriptionCol.Go();


                    // Adds price info to doc
                    ColumnText priceCol = new ColumnText(cb);
                    priceCol.SetSimpleColumn(new Phrase(new Chunk(price, lato9)),
                                       1030 * PAGE_SCALE_FACTOR,
                                       PageSize.LETTER.Height - (780 + numItems * 69) * PAGE_SCALE_FACTOR,
                                       1150 * PAGE_SCALE_FACTOR,
                                       PageSize.LETTER.Height - (729) * PAGE_SCALE_FACTOR,
                                       33, Element.ALIGN_LEFT | Element.ALIGN_TOP);
                    priceCol.Go();

                    // Adds item footer image to document based on active theme
                    iTextSharp.text.Image footer = iTextSharp.text.Image.GetInstance(Application.StartupPath + "\\Resources\\Invoices\\T" + data.invoiceTemplate + "\\T" + data.invoiceTemplate + "_Footer.JPG");
                    footer.ScaleAbsolute(footer.Width * PAGE_SCALE_FACTOR, footer.Height * PAGE_SCALE_FACTOR);
                    footer.SetAbsolutePosition(0, 0);
                    doc.Add(footer);

                    // Adds total and comments image to document based on active theme
                    iTextSharp.text.Image totals = iTextSharp.text.Image.GetInstance(Application.StartupPath + "\\Resources\\Invoices\\T" + data.invoiceTemplate + "\\T" + data.invoiceTemplate + "_Totals.JPG");
                    totals.ScaleAbsolute(totals.Width * PAGE_SCALE_FACTOR, totals.Height * PAGE_SCALE_FACTOR);
                    totals.SetAbsolutePosition(0, PageSize.LETTER.Height - (totals.Height + endOfItemsPos + 15) * PAGE_SCALE_FACTOR);
                    doc.Add(totals);


                    string totalsStr = String.Format("{0:C}", totalPrice) + "\n" + 
                        String.Format("{0:C}", totalTax) + "\n" + 
                        String.Format("{0:C}", totalPrice + totalTax);

                    // Adds totals info to doc
                    Chunk invoiceSubtotals = new Chunk(String.Format("{0:C}", totalPrice) + "\n" + String.Format("{0:C}", totalTax) + "\n", lato8);
                    Chunk invoiceGrandTotal = new Chunk(String.Format("{0:C}", totalPrice + totalTax), lato12);
                    Phrase invoiceTotals = new Phrase();
                    invoiceTotals.Add(invoiceSubtotals);
                    invoiceTotals.Add(invoiceGrandTotal);

                    ColumnText totalsCol = new ColumnText(cb);
                    totalsCol.SetSimpleColumn(invoiceTotals,
                                       1025 * PAGE_SCALE_FACTOR,
                                       PageSize.LETTER.Height - (endOfItemsPos + 205 + 15) * PAGE_SCALE_FACTOR,
                                       1150 * PAGE_SCALE_FACTOR,
                                       PageSize.LETTER.Height - (endOfItemsPos + 45 + 15) * PAGE_SCALE_FACTOR,
                                       25, Element.ALIGN_LEFT | Element.ALIGN_TOP);
                    totalsCol.Go();



                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }

                //Closes the file
                doc.Close();

                //Checks if the doc is to be emailed
                if (toEmail)
                {
                    //Calls for the emailing of the doc with client email and the path of the generated invoice. Then opens the invoice.
                    sendEmail(invoicePath, clientEmail);
                    Process.Start(invoicePath);
                }
                else
                {
                    //If not opens up the invoice
                    Process.Start(invoicePath);
                }
            }
            else
            {
                //If no invoice has been selected informs user
                MessageBox.Show("Please select an invoice before printing or emailing.");
            }
        }
        
        //Pre: The string path to the invoice generated, and the client's email address.
        //Post: Emails the invoice to the client.
        //Description: Sends an email, using SMTP to the client, with the invoice created attached as a PDF.
        private void sendEmail(string invoicePath, string clientEmail)
        {
            //Attempts to send the email with SMTP information provided
            try
            {
                //Sets up an SMTP client with the address and port specified by the user.
                SmtpClient client = new SmtpClient(data.emailSettings[SharedData.SMTP_ADDRESS], Convert.ToInt32(data.emailSettings[SharedData.SMTP_PORT]));

                //Determines if SSL is enabled based on settings, and sets the property appropriately
                if (data.emailSettings[SharedData.SMTP_REQUIRESSL].ToLower() == "true")
                {
                    client.EnableSsl = true;
                }
                else
                {
                    client.EnableSsl = false;
                }

                //Sets email client information, based on user settings
                client.Timeout = 30000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential(data.emailSettings[SharedData.SMTP_USER_ID], data.emailSettings[SharedData.SMTP_PASSWORD]);

                //Creates a new mail message, and adds client email as destination
                MailMessage msg = new MailMessage();
                msg.To.Add(clientEmail);

                //Sets the send from property of the email
                msg.From = new MailAddress(data.emailSettings[SharedData.SMTP_USER_ID]);

                //Sets the default subject and body message of the email.
                msg.Subject = "Your Invoice From " + data.generalSettings[SharedData.COMPANY_NAME];
                msg.Body = "Please see attached \n\n\n\n Kind Regards, \n\n " + data.generalSettings[SharedData.COMPANY_NAME];

                //Attaches the invoice to the email
                Attachment attachment = new Attachment(invoicePath);
                msg.Attachments.Add(attachment);

                //Calls for the sending of the email
                client.Send(msg);

                //Informs user the message has been sent
                MessageBox.Show("Message Sent!");
            }
            catch
            {
                //Indicates invalid SMTP info. Informs user
                MessageBox.Show("Invalid SMTP information. Please go to settings and verify SMTP information under the E-Mail Section. Message not sent.");
            }
        }

        //Pre: None
        //Post: Indicates the dgv selection has changed and reads in the data from the dgv.
        //Description: Reads in check box data and fills the binding source and table adapter.
        public void dgvInvoicesSelectionChanged()
        {
            //Reads checkbox data
            readInvoiceCheckboxes();

            //Rebinds the table (Updates dgv data to dataset in database)
            data.binding.EndEdit();
            frm.invoicesTableAdapter.Update(frm.invoicesDataSet.invoices);
        }

        // Indicates invoice editing has been completed.
        public void btnCreateDone(object sender, EventArgs e)
        {
            // Only updates invoice if it was created or already exists. 
            if (frm.lblInvoiceIDNum.Text != "New Invoice")
            {
                // Updates invoice data.
                updateExistingInvoice(sender, e);

                //Indicates existing invoice is to be updated, alongside dgv invoices
                dgvInvoicesSelectionChanged();
            }

            // Returns to invoices view
            returnToInvoices();
        }

        //Pre: None
        //Post: The checkboxes of the quote and paid columns are updated
        //Description: Reads in the text values from the hidden columns and interprets the proper check state of the boxes.
        public void updateInvoiceCheckboxes()
        {
            //Runs for all the rows of checkboxes
            for (int i = 0; i < frm.dgvInvoices.RowCount; ++i)
            {
                //If the third column indicates that it is a quote, 'checks' the invoices column checkbox
                if (frm.dgvInvoices.Rows[i].Cells[3].Value.ToString() == "Quote")
                {
                    frm.dgvInvoices.Rows[i].Cells[5].Value = true;
                }
                //Otherwise, unchecks the box
                else
                {
                    frm.dgvInvoices.Rows[i].Cells[5].Value = false;
                }

                //If the fourth column indicates that it is a paid invoice, 'checks' the paid column checkbox
                if (frm.dgvInvoices.Rows[i].Cells[4].Value.ToString() == "Yes")
                {
                    frm.dgvInvoices.Rows[i].Cells[6].Value = true;
                }
                //Otherwise unchecks the box
                else
                {
                    frm.dgvInvoices.Rows[i].Cells[6].Value = false;
                }
            }
        }

        //Pre: None
        //Post: Reads in the checkbox values, and translates to natural language equivalent in datatable columns
        //Description: For all the invoices, reads in checkboxes, and sets the text columns to the appropriate true/false state.
        private void readInvoiceCheckboxes()
        {
            //Runs for all the rows in dgv invoices
            for (int i = 0; i < frm.dgvInvoices.RowCount - 1; ++i)
            {
                //Attempts to find the text value equivalent to the press state
                try
                {
                    //If the quote column checkbox (5) is checked, sets the text column (3) equal to the natural language equivalent
                    if (frm.dgvInvoices.Rows[i].Cells[5].Value.Equals(true))
                    {
                        frm.dgvInvoices.Rows[i].Cells[3].Value = "Quote";
                    }
                    //Otherwise interprets it as an invoice
                    else
                    {
                        frm.dgvInvoices.Rows[i].Cells[3].Value = "Invoice";
                    }

                    //If the paid column checkbox (6) is checked, sets the text column (4) equal to the natural language equivalent
                    if (frm.dgvInvoices.Rows[i].Cells[6].Value.Equals(true))
                    {
                        frm.dgvInvoices.Rows[i].Cells[4].Value = "Yes";
                    }
                    //Otherwise interprets it as unpaid
                    else
                    {
                        frm.dgvInvoices.Rows[i].Cells[4].Value = "No";
                    }
                }
                catch
                {
                    //Indicates null value. Not established what was selected. 
                }
            }
        }

        //Pre: None
        //Post: 
        public void setUpAutofillClientName()
        {
            //DataView dv = new DataView(clientsDataSet);
            frm.txtInvoiceClientName.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            frm.txtInvoiceClientName.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection collection = new AutoCompleteStringCollection();

            //Checks if connection is closed. If so opens it up
            if (data.connection.State == ConnectionState.Closed)
            {
                data.connection.Open();
            }
            
            //Using sql, finds all the columns in the items table where the invoice ID is a match
            using (MySqlCommand command = new MySqlCommand("SELECT ClientID, FirstName, LastName, Company, Phone FROM clients", data.connection))
            {
                //Executes the command
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    //Reads each row
                    while (reader.Read())
                    {
                        collection.Add(reader["FirstName"].ToString() + ", " + reader["LastName"].ToString() + "- CID: " + reader["ClientID"].ToString());
                        collection.Add(reader["LastName"].ToString() + ", " + reader["FirstName"].ToString() + "- CID: " + reader["ClientID"].ToString());
                        collection.Add(reader["Phone"].ToString() + "- CID: " + reader["ClientID"].ToString());

                        if (reader["Company"] != null)
                        {
                            collection.Add(reader["Company"].ToString() + "- CID: " + reader["ClientID"].ToString());
                        }

                        clientIDs.Add(Convert.ToInt32(reader["ClientID"].ToString()));
                    }
                }
            }

            frm.txtInvoiceClientName.AutoCompleteCustomSource = collection;
        }

        // Pre: None
        // Post: The client ID num is set
        // Description: Scans the Client name textbox to find client id
        public void scanForClientID()
        {
            // Gets the last index of the InvoiceClientName textbox
            int lastIndex = frm.txtInvoiceClientName.Text.LastIndexOf("- CID: ");

            // Ensures a valid index is in place
            if (lastIndex > -1)
            {
                // Gets the client id under question
                int numStartPosition = lastIndex + "- CID: ".Length;
                int clientID = Convert.ToInt32(frm.txtInvoiceClientName.Text.Substring(numStartPosition));

                // Validates client id and populates the client id num box
                if (clientIDs.BinarySearch(clientID) >= 0)
                {
                    frm.numClientID.Value = clientID;
                }
            }
        }
    }
}