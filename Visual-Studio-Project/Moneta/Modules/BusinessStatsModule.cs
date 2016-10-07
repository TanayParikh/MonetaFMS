/***************************************************************************************************************
 * 
    Author:         Tanay Parikh
    Project Name:   Moneta
    Description:    Calculates business statistics, using data sourced from external databases. Also generates
 *                  a profit/loss financial statement. 
 * 
***************************************************************************************************************/

//System libraries
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//Library for database access
using MySql.Data.MySqlClient;

//Libraries used for PDF export
using System.IO;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.xmp;
using iTextSharp.text.pdf;
using System.Collections;
using System.Windows.Forms.DataVisualization.Charting;

namespace Moneta
{
    internal class BusinessStatsModule
    {
        //Stores module specific refrence to shared data and form
        private SharedData data;
        private FrmMain frm;

        //Constants to identify stats within the stats arrays
        private const int REVENUE = 0;
        private const int EXPENSES = 1;
        private const int PROFIT = 2;
        private const int COGS = 3;
        private const int ACCOUNTS_RECEIVABLE = 4;
        private const int GROSS_MARGIN = 5;
        private const int TAXES_PAID = 6;
        private const int TAXES_COLLECTED = 7;

        //Stores the number of months, for which stats are being calculated
        private int numMonths;

        //Stores the COGS computed. Used for multiple calculations.
        private double sumCogs;

        //Arrays to store the stat names and values
        private string[] statHeadings;
        private double[] stats = new double[8];

        //Class constructor with the form and shared data as parameters
        public BusinessStatsModule(FrmMain frm, SharedData data)
        {
            //Locally stores shared data and the form
            this.frm = frm;
            this.data = data;

            //Sets the headings of the stats.
            //Each entry is seperated by the comma delimiter. 
            statHeadings = "Revenue,Expenses,Profit,Cost of Goods Sold,Accounts Receivable,Gross Margin,Taxes Paid,Taxes Collected".Split(',');
        }

        //Pre: None
        //Post: Initializes the module's dgv columns
        //Description: Sets up the columns for the business stats and client stats dgvs
        public void initialize()
        {
            //Intializes the headings column, with width set to 175px, and with read only capability. Adds to dgv business stats
            DataGridViewTextBoxColumn statHeadings = new DataGridViewTextBoxColumn();
            statHeadings.HeaderText = "Statistic";
            statHeadings.Name = "Statistic";
            statHeadings.Width = 175;
            statHeadings.SortMode = DataGridViewColumnSortMode.Automatic;
            statHeadings.ReadOnly = true;
            frm.dgvBusinessStats.Columns.Add(statHeadings);

            //Intializes the values column, with width set to 125px, and with read only capability. Adds to dgv business stats
            DataGridViewTextBoxColumn statValues = new DataGridViewTextBoxColumn();
            statValues.HeaderText = "Performance";
            statValues.Name = "Performance";
            statValues.Width = 125;
            statValues.SortMode = DataGridViewColumnSortMode.Automatic;
            statValues.ReadOnly = true;
            frm.dgvBusinessStats.Columns.Add(statValues);

            //Disables the ability to add/remove rows from business stats dgv
            frm.dgvBusinessStats.AllowUserToAddRows = false;
            frm.dgvBusinessStats.AllowUserToDeleteRows = false; ;

            //Intializes the client stats dgv headings column, with width set to 150px, and with read only capability.
            DataGridViewTextBoxColumn clientID = new DataGridViewTextBoxColumn
            {
                HeaderText = "Client ID",
                Name = "Client ID",
                Width = 150,
                SortMode = DataGridViewColumnSortMode.Automatic,
                ReadOnly = true
            };
            frm.dgvClientStats.Columns.Add(clientID);

            //Intializes the client stats dgv values column, with width set to 150px, and with read only capability.
            DataGridViewTextBoxColumn clientTotal = new DataGridViewTextBoxColumn
            {
                HeaderText = "Amount Billed",
                Name = "clientTotal",
                Width = 150,
                SortMode = DataGridViewColumnSortMode.Automatic,
                ReadOnly = true
            };
            frm.dgvClientStats.Columns.Add(clientTotal);

            //Disables the ability to add/remove rows from dgv client stats
            frm.dgvClientStats.AllowUserToAddRows = false;
            frm.dgvClientStats.AllowUserToDeleteRows = false;
        }

        //Pre: None
        //Post: Calls for the calculation of the business and client stats
        //Description: Clears the current display, fetches the dates, and calculates the stats
        public void calculateStats()
        {
            //Determines the month timespan between the starting and ending dates. Will be used later when printing. 
            numMonths = frm.dtpStatsEnd.Value.Month - frm.dtpStatsStart.Value.Month + (frm.dtpStatsEnd.Value.Year - frm.dtpStatsStart.Value.Year) * 12;

            //Checks if a valid date range (Positive number of months) has been selected
            if (numMonths >= 0)
            {
                //Clears the currently displayed stats
                frm.dgvBusinessStats.Rows.Clear();
                frm.dgvClientStats.Rows.Clear();

                //Fetches the start and end date for stat calculations from the date time picker, and formats them appropriately. 
                string startDate = frm.dtpStatsStart.Value.Date.ToString("d", data.dateCulture);
                string endDate = frm.dtpStatsEnd.Value.Date.ToString("d", data.dateCulture);

                //Calls for the calculation of the business and client stats
                compileBusinessStats(startDate, endDate);

                //Accounts for no clients
                if (frm.dgvClients.RowCount > 0)
                {
                    compileClientStats(startDate, endDate);
                }
            }
            else
            {
                //If not informs user
                MessageBox.Show("Please enter a valid date range.");
            }
        }

        //Pre: The start and end dates of the calculation.
        //Post: The business stats are calculated and displayed in the business stats dgv.
        //Description: Calls for the calculation of each business stat, and then displays each one with heading in the dgv. 
        private void compileBusinessStats(string startDate, string endDate)
        {
            computeStats(startDate, endDate);

            //Runs for all the stats
            for (int i = 0; i < statHeadings.Count(); ++i)
            {
                //Adds a row to the business stats dgv, and sets the stat heading
                frm.dgvBusinessStats.Rows.Add();
                frm.dgvBusinessStats.Rows[i].Cells[0].Value = statHeadings[i];

                //Checks to see it isn't the 5th row (Gross margin) as that is percentage based
                if (i != 5)
                {
                    //Formats as currency
                    frm.dgvBusinessStats.Rows[i].Cells[1].Value = String.Format("{0:C}", stats[i]);
                }
                else
                {
                    //Formats gross margin as %
                    frm.dgvBusinessStats.Rows[i].Cells[1].Value = stats[i] + "%";
                }
            }
        }

        private void computeStats(string startDate, string endDate)
        {
            stats[EXPENSES] = calculateExpenses(startDate, endDate);
            stats[REVENUE] = calculateRevenue(startDate, endDate);
            stats[PROFIT] = calculateProfit();
            stats[ACCOUNTS_RECEIVABLE] = calculateAccountsReceivable(startDate, endDate);
            stats[GROSS_MARGIN] = calculateGrossMargin(startDate, endDate);
            stats[COGS] = sumCogs;
            stats[TAXES_COLLECTED] = calculateTaxesCollected(startDate, endDate);
            stats[TAXES_PAID] = calculateTaxesPaid(startDate, endDate);
        }

        //Pre: The start and end dates of the calculation.
        //Post: The client stats are calculated and displayed in the client stats dgv.
        //Description: Calculates the client stats based on dates, and then displays the amount each client has been billed in that timespan. 
        private void compileClientStats(string startDate, string endDate)
        {
            string query = "SELECT concat(FirstName, ' ', LastName) AS Name, Company, ClientTotal FROM clients"
                + " JOIN"
                + " (SELECT ClientID, SUM(Total) AS ClientTotal FROM"
                + " (SELECT SUM(Price) AS Total, Invoices_InvoiceID AS InvoiceID, MAX(ClientID) AS ClientID FROM mydb.items AS items"
                + " JOIN (SELECT Clients_ClientID AS ClientID, InvoiceID FROM invoices WHERE invoices.Date >= @startDate AND invoices.Date <= @endDate) AS invoices"
                + " ON items.Invoices_InvoiceID = invoices.InvoiceID"
                + " GROUP BY Invoices_InvoiceID) AS totalInvoiceClient"
                + " GROUP BY ClientID ORDER BY ClientTotal DESC) AS clientIDTotal" 
                + " ON clients.ClientID = clientIDTotal.ClientID";

            //Checks if the sql connection is open, if not opens it up. 
            if (data.connection.State == ConnectionState.Closed)
            {
                data.connection.Open();
            }

            using (MySqlCommand command = new MySqlCommand(query, data.connection))
            {
                command.Parameters.AddWithValue("@startDate", startDate);
                command.Parameters.AddWithValue("@endDate", endDate);

                //Executes the command
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    //Runs for all results
                    while (reader.Read())
                    {
                        // Adds row with client name and total
                        int index = frm.dgvClientStats.Rows.Add();
                        frm.dgvClientStats.Rows[index].Cells[0].Value = (string.IsNullOrEmpty(reader["Company"].ToString())) ? reader["Name"].ToString() : reader["Company"].ToString();
                        frm.dgvClientStats.Rows[index].Cells[1].Value = String.Format("${0:C}", reader["ClientTotal"].ToString());
                    }
                }

                data.connection.Close();
            }

            // Sets DGV Column header
            frm.dgvClientStats.Columns[0].HeaderText = "Client";
        }

        //Pre: None
        //Post: The profit is returned
        //Description: Calculates the profit -> (Revenue-expenses)
        private double calculateProfit()
        {
            //Returns the calculated profit
            return stats[REVENUE] - stats[EXPENSES];
        }

        //Pre: The start and end dates of the calculation.
        //Post: The business revenue is returned
        //Description: Calculates the revenue, by finding the invoices in the date range, and then 
        //             finding the sum of the item values for each of the invoices.
        private double calculateRevenue(string startDate, string endDate)
        {
            //Stores the revenue calculated so far and 
            double sumRevenue = 0;
            List<string> invoices = new List<string>();

            //Finds the invoice id in the invoices table, in the given date range
            string sql = "SELECT invoices.InvoiceID FROM invoices WHERE invoices.Date >= '"
                + startDate
                + "' AND invoices.Date <= '"
                + endDate
                + "' AND invoices.Type = 'Invoice' AND invoices.Paid = 'Yes';";

            //Checks if the sql connection is open, if not opens it up
            if (data.connection.State == ConnectionState.Closed)
            {
                data.connection.Open();
            }

            //Uses the sql command generated above and the connection
            using (MySqlCommand command = new MySqlCommand(sql, data.connection))
            {
                //Executes the command
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    //Stores each invoice id in the invoices array
                    while (reader.Read())
                    {
                        invoices.Add(reader["InvoiceID"].ToString());
                    }
                }
            }

            //Closes the sql connection
            data.connection.Close();

            //Runs for each invoice
            for (int i = 0; i < invoices.Count; ++i)
            {
                //Opens the sql connection
                data.connection.Open();

                //Finds the sum of the invoices within and stores as an object
                MySqlCommand command = new MySqlCommand("SELECT SUM(Price) FROM items WHERE Invoices_invoiceID = " + invoices[i], data.connection);
                object sumRevenueO = command.ExecuteScalar();

                //Verifies validity of object
                if (!string.IsNullOrEmpty(sumRevenueO.ToString()))
                {
                    //If valid, converts to double and adds to total revenue
                    sumRevenue += Convert.ToDouble(sumRevenueO.ToString());
                }

                //Closes the sql connection
                data.connection.Close();
            }

            //Returns the sum revenue found
            return sumRevenue;
        }

        //Pre: The start and end dates of the calculation.
        //Post: The business expenses are returned
        //Description: Calculates the total expenses, by using the c# compute operrand.
        private double calculateExpenses(string startDate, string endDate)
        {
            //Attempts the calculation. If data is valid, returns it. Otherwise returns 0
            try
            {
                //Calls for the computation of the sum of the TotalAmount column in the expenses table within the given date range. Converts to double
                object expenses = frm.expensesDataSet.Tables["expenses"].Compute("SUM(TotalAmount)", "Date >= '" + startDate + "' AND Date <= '" + endDate + "'");
                return Convert.ToDouble(expenses);
            }
            catch
            {
                //Returns 0
                return 0;
            }
        }

        //Pre: The start and end dates of the calculation.
        //Post: The money owed to the business is returned
        //Description: Calculates the accounts payaable, by finding the invoices in the date range, which are unpaid, 
        //             and then finding the sum of the item values for each of those invoices.
        private double calculateAccountsReceivable(string startDate, string endDate)
        {
            //Variables storing the sum of the amount receivable and the appropriate invoice ID
            double sumReceivable = 0;
            List<string> invoices = new List<string>();

            //Finds the invoice id of all unpaid invoices
            string sql = "SELECT invoices.InvoiceID FROM invoices WHERE Paid = 'No';";

            //Checks if connection is open, if not opens it up
            if (data.connection.State == ConnectionState.Closed)
            {
                data.connection.Open();
            }

            //Executes using the sql command from above and the sql connection
            using (MySqlCommand command = new MySqlCommand(sql, data.connection))
            {
                //Executes command
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    //Reads for each result row
                    while (reader.Read())
                    {
                        //Stores the invoice ID in a  list
                        invoices.Add(reader["InvoiceID"].ToString());
                    }
                }
            }

            //Closes the connection
            data.connection.Close();

            //Runs for each invoice
            for (int i = 0; i < invoices.Count; ++i)
            {
                //Opens the data connection
                data.connection.Open();

                //Funds the sum of the items within the invoice and stores in an object
                MySqlCommand command = new MySqlCommand("SELECT SUM(Price) FROM items WHERE Invoices_invoiceID = " + invoices[i], data.connection);
                object sumReceivableO = command.ExecuteScalar();

                //Ensures that the object isn't null or empty
                if (!string.IsNullOrEmpty(sumReceivableO.ToString()))
                {
                    //If not adds to the receivable sum
                    sumReceivable += Convert.ToDouble(sumReceivableO.ToString());
                }

                //Closes the sql connection
                data.connection.Close();
            }

            //Returns the receivable sum
            return sumReceivable;
        }

        //Pre: The start and end dates of the calculation.
        //Post: The gross margin is calculated
        //Description: Finds all the expenses, which are associated with an invoice, and finds the cost of goods sold, and
        //             subsequently the gross margin. (Revenue - Cost of goods sold) / Revenue
        private double calculateGrossMargin(string startDate, string endDate)
        {
            //Local variables to help calculate the gross margin and store found invoices
            double sumRev = 0;
            double sumGrossMargin = 0;
            List<string> invoices = new List<string>();

            //Sets the sql command to find the invoice ID and total expense amount from the expenses table where the invoice id isn't null
            string sql = "SELECT Invoices_InvoiceID, TotalAmount FROM expenses WHERE Invoices_InvoiceID IS NOT NULL";

            //Checks if the sql connection is open, if not opens it up
            if (data.connection.State == ConnectionState.Closed)
            {
                data.connection.Open();
            }

            //Uses the sql command and the data connection
            using (MySqlCommand command = new MySqlCommand(sql, data.connection))
            {
                //Executes the command
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    //Reads all the resultant rows
                    while (reader.Read())
                    {
                        //Adds the invoice ID to the invoices array and adds the expense total to the COGS
                        invoices.Add(reader["Invoices_InvoiceID"].ToString());
                        sumCogs += Convert.ToDouble(reader["TotalAmount"]);
                    }
                }
            }

            //Closes the sql connection
            data.connection.Close();

            //Runs for all the found invoices to find revenue for the invoices with expenses associated with them
            for (int i = 0; i < invoices.Count; ++i)
            {
                //Opens the sql connection and queries for the sum of the item prices with the appropriate invoice id
                data.connection.Open();
                MySqlCommand command = new MySqlCommand("SELECT SUM(Price) FROM items WHERE Invoices_invoiceID = " + invoices[i], data.connection);
                object sumRevO = command.ExecuteScalar();

                //Sees if the object found isn't null or empty
                if (!string.IsNullOrEmpty(sumRevO.ToString()))
                {
                    //If not converts to double and adds to revenue
                    sumRev += Convert.ToDouble(sumRevO.ToString());
                }

                //Closes the sql connection
                data.connection.Close();
            }

            //Uses the gross margin formula to calculate - (Rev - COGS)/Rev
            sumGrossMargin = (sumRev - sumCogs) / sumRev;

            //Returns the gross margin rounded to two decimal places
            return Math.Round(sumGrossMargin * 100, 2);
        }

        //Pre: The start and end dates of the calculation.
        //Post: The total taxes paid on expenses are calculated
        //Description: Finds the sum of the paid taxes, which are associated with an expense.
        private double calculateTaxesPaid(string startDate, string endDate)
        {
            //Performs the calculation. Attempts to convert to double. If not possible returns 0 (No taxes paid).
            try
            {
                //Computes the sum of the expenses table's taxes column within the given date range. Converts to double.
                object taxPaid = frm.expensesDataSet.Tables["expenses"].Compute("SUM(TaxAmount)", "Date >= '" + startDate + "' AND Date <= '" + endDate + "'");
                return Convert.ToDouble(taxPaid);
            }
            catch
            {
                //Indicates no taxes paid
                return 0;
            }
        }

        //Pre: The start and end dates of the calculation.
        //Post: The total taxes collected, from the invoices, within the given date range are calculated
        //Description: Finds the product of the items sold and tax percentage, for items associated 
        //             to invoices within the time frame.
        private double calculateTaxesCollected(string startDate, string endDate)
        {
            //Local variables to store taxes collected and invoice refrences
            double sumTaxCollected = 0;
            List<string> invoices = new List<string>();

            //Finds all invoice ids for invoices within the date range
            string sql = "SELECT invoices.InvoiceID FROM invoices WHERE invoices.Date >= '"
                + startDate
                + "' AND invoices.Date <= '"
                + endDate
                + "' AND invoices.Type = 'Invoice' AND invoices.Paid = 'Yes';";

            //Checks if the connection is open, if not opens it up
            if (data.connection.State == ConnectionState.Closed)
            {
                data.connection.Open();
            }

            //Uses the sql command and the sql connection
            using (MySqlCommand command = new MySqlCommand(sql, data.connection))
            {
                //Executes the command
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    //Reads all the rows returned
                    while (reader.Read())
                    {
                        //Adds the invoice ID to the invoices list
                        invoices.Add(reader["InvoiceID"].ToString());
                    }
                }
            }

            //Closes the data connection
            data.connection.Close();

            //Runs for all the invoices found
            for (int i = 0; i < invoices.Count; ++i)
            {
                //Opens the sql connection
                data.connection.Open();

                //Executes the sql command to find the sum of the items multiplied by the tax percentage in the given invoice range
                MySqlCommand command = new MySqlCommand("SELECT SUM(items.Price * (items.TaxPercentage / 100)) FROM items WHERE Invoices_invoiceID = " + invoices[i], data.connection);
                object sumRevenueO = command.ExecuteScalar();

                //If the object found wasn't null or empty converts to double
                if (!string.IsNullOrEmpty(sumRevenueO.ToString()))
                {
                    sumTaxCollected += Convert.ToDouble(sumRevenueO.ToString());
                }

                //Closes the data connection
                data.connection.Close();
            }

            //Returns the sum of the taxes collected
            return sumTaxCollected;
        }

        //Pre: The number of months for which to calculate the monthly expenses.
        //Post: The monthly expense breakdown in a array of doubles
        //Description: Finds date ranges for the months indicated and finds the expenses associated with the ranges.
        private double[] calculatePastMonthsExpenses(int numMonths)
        {
            //Creates a local double array with the size being the numMonths being calculated
            double[] pastMonthlyExpenses = new double[numMonths];
            
            //Runs for all the months plus 1 (Accounts for non-zero based index for months)
            for (int i = 1; i < numMonths + 1; ++i)
            {
                //Finds date ranges one month apart (i) months away from the current month
                string endDate = DateTime.Today.AddMonths(-(i - 1)).Date.ToString("d", data.dateCulture);
                string startDate = DateTime.Today.AddMonths(-i).Date.ToString("d", data.dateCulture);

                //Finds the sum of the expenses within the date range
                object monthlyExp = frm.expensesDataSet.Tables["expenses"].Compute("SUM(TotalAmount)", "Date >= '" + startDate + "' AND Date <= '" + endDate + "'");

                //If the value isn't null, adds to the appropriate index of the past monthly expenses
                if (monthlyExp != DBNull.Value)
                {
                    pastMonthlyExpenses[i - 1] = Convert.ToDouble(monthlyExp);
                }
                //If not, adds 0 to the month's expenses
                else
                {
                    pastMonthlyExpenses[i - 1] = 0;
                }
            }

            //Returns the past monthly expenses array
            return pastMonthlyExpenses;
        }

        //Pre: The number of months for which to calculate the monthly expenses.
        //Post: The monthly revenue breakdown in a array of doubles
        //Description: Finds date ranges for the months indicated and finds the revenues associated with the ranges.
        private double[] calculatePastMonthsRevenues(int numMonths)
        {
            //Creates a double array with the size being the number of months for which the rev is being calculated
            double[] pastMonthlyRevenues = new double[numMonths];

            //Runs for the number of months
            for (int i = 1; i < numMonths + 1; ++i)
            {
                //Finds date ranges one month apart (i) months away from the current month
                string endDate = DateTime.Today.AddMonths(-(i - 1)).Date.ToString("d", data.dateCulture);
                string startDate = DateTime.Today.AddMonths(-i).Date.ToString("d", data.dateCulture);

                //Calls for the calculation of the revenue within the given range, and adds to the monthly revenues array at the appropriate index.
                double monthlyRev = calculateRevenue(startDate, endDate);
                pastMonthlyRevenues[i - 1] = monthlyRev;
            }

            //Returns the monthly revenues
            return pastMonthlyRevenues;
        }

        //Pre: None
        //Post: Generates a profit loss statement, in PDF form and opens it up.
        //Description: Compiles the data, and adds onto a PDF using the iTextSharp dll
        public void generateProfitLoss()
        {
            //Creates variables to store the document path
            string docPath;

            //Fetches the start and end date for stat calculations from the date time picker, and formats them appropriately. 
            string startDate = frm.dtpStatsStart.Value.Date.ToString("d", data.dateCulture);
            string endDate = frm.dtpStatsEnd.Value.Date.ToString("d", data.dateCulture);

            //Fetches today's date
            string dateExtension = DateTime.Today.Date.ToString("d MMMM", System.Globalization.CultureInfo.CreateSpecificCulture("en-US"));

            //Creates a new PDF doc and writer for a PDF of letter size
            Document doc = new Document(iTextSharp.text.PageSize.LETTER);
            PdfWriter writer;

            //Checks if the export directory exists, if not creates it.
            if (!Directory.Exists(data.databasePath + "\\ProfitLossStatements"))
            {
                Directory.CreateDirectory(data.databasePath + "\\ProfitLossStatements");
            }
            
            //Attempts to create a document in the startup path folder, in the ProfitLossStatements sub-directory.
            try
            {
                //If possible names the file the company name followed by today's date
                docPath = data.databasePath + "\\ProfitLossStatements\\" + data.generalSettings[SharedData.COMPANY_NAME] + "_" + dateExtension + ".pdf";
                writer = PdfWriter.GetInstance(doc, new FileStream(docPath, FileMode.Create));
            }
            catch
            {
                //Otherwise if an error occurs, such as the file already exists, creates a new file, with a random number extension added on.
                docPath = data.databasePath + "\\ProfitLossStatements\\" + data.generalSettings[SharedData.COMPANY_NAME] + "_" + dateExtension + "_" + data.generator.Next(1, 9999).ToString() + ".pdf";
                writer = PdfWriter.GetInstance(doc, new FileStream(docPath, FileMode.Create));
            }

            //Opens up the doc to make it writable
            doc.Open();

            //Attempts to source and scale the company logo
            try
            {
                //Opens the logo and scales to 100px in height
                iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(data.databasePath + "//Images//" + data.generalSettings[SharedData.COMPANY_LOGO_PATH]);
                float logoHeightMultiple = logo.Height / 100f;
                logo.ScaleAbsolute(logo.Width / logoHeightMultiple, logo.Height / logoHeightMultiple);

                //Adds to the document
                doc.Add(logo);
            }
            catch
            {
                //Indicates logo couldn't be opened
                MessageBox.Show("Logo not found at the specified location. Please check the logo location, in the settings tab. Document will print without a logo.");
            }

            //Adds in the title (Profit and Loss Statement for (Company Name)). Includes date of compilation and time span under question
            doc.Add(new Paragraph("  "));
            doc.Add(new Paragraph("Profit and Loss Statement for " + data.generalSettings[SharedData.COMPANY_NAME]));
            doc.Add(new Paragraph("Compiled on " + dateExtension + " for the time span " + startDate + " to " + endDate + "."));
            doc.Add(new Paragraph("  "));

            //Adds the company address and phone number to the doc. 
            doc.Add(new Paragraph(data.generalSettings[SharedData.COMPANY_ADDRESS]));
            doc.Add(new Paragraph("Phone: " + data.generalSettings[SharedData.COMPANY_PHONE_NUMBER]));

            //Creates a new cell for the PDF tables, and defines its attributes
            PdfPCell cell = new PdfPCell(new Phrase(""));
            cell.Colspan = 3;
            cell.HorizontalAlignment = 1;


            /*************************************
                        Statistics Table
            *************************************/
            //Creats a PDF table with 2 columns
            PdfPTable table = new PdfPTable(2);
            table.TotalWidth = 400f;
            table.LockedWidth = true;

            //Sets the widths of the table
            float[] widths = new float[] { 10f, 3f};
            table.SetWidths(widths);
            table.HorizontalAlignment = 0;

            //Sets up table spacing
            table.SpacingBefore = 5f;
            table.SpacingAfter = 15f;
            table.PaddingTop = 15f;

            //Adds the cell attributes to the table
            table.AddCell(cell);

            //Adds table headings
            table.AddCell(new Paragraph("Statistic:"));
            table.AddCell(new Paragraph("Performance:"));

            //Runs through a loop for each stat
            for (int i = 0; i < statHeadings.Count(); ++i)
            {
                //Formats as currency, except for the 5th index row as that is a percentage (Gross margin)
                if (i != 5)
                {
                    //Displays the stat, alongside the performance formatted as currency
                    table.AddCell(new Paragraph(statHeadings[i]));
                    table.AddCell(new Paragraph(String.Format("{0:C}", stats[i])));
                }
                else
                {
                    //Displays the stat, alongside the performance formatted as a percentage
                    table.AddCell(new Paragraph(statHeadings[i]));
                    table.AddCell(new Paragraph(stats[i] + "%"));
                }
            }

            //Adds the table to the document
            doc.Add(table);


            /*************************************
                        Expenses Table
            *************************************/
            //Creates pdf table with 2 columns
            PdfPTable expenseCategoryTable = new PdfPTable(2);
            
            //Sets up table
            expenseCategoryTable.TotalWidth = 400f;
            expenseCategoryTable.LockedWidth = true;
            expenseCategoryTable.SetWidths(widths);
            expenseCategoryTable.HorizontalAlignment = 0;

            //Sets up table spacing
            expenseCategoryTable.SpacingBefore = 10f;
            expenseCategoryTable.SpacingAfter = 0f;
            expenseCategoryTable.PaddingTop = 10f;

            //Adds the cell attributes to the table
            expenseCategoryTable.AddCell(cell);

            //Adds table headings
            expenseCategoryTable.AddCell(new Paragraph("Expense Category:"));
            expenseCategoryTable.AddCell(new Paragraph("Expense:"));

            //Runs through all the expense categories
            for (int i = 0; i < data.expenseCategories.Count(); ++i)
            {
                //Attempts to add a row to the table with the category name and value
                try
                {
                    //Fills a cell with the name of the category
                    expenseCategoryTable.AddCell(new Paragraph(data.expenseCategories[i]));

                    //Calculates the expenses for that category by finding the sum of the expenses TotalAmount column with respect to that category
                    double expenseCategorySum = Convert.ToDouble(frm.expensesDataSet.Tables["expenses"].Compute("SUM(TotalAmount)", "ExpenseCategory = '" + data.expenseCategories[i] + "' AND Date >= '" + startDate + "' AND Date <= '" + endDate + "'"));

                    //Fills the expenses value column with the expense found, formatted as currency
                    expenseCategoryTable.AddCell(new Paragraph(string.Format("{0:C}", expenseCategorySum)));
                }
                catch
                {
                    //Indicates there were no expenses for the category, and adds it into the table, formatted as currency
                    double expenseCategorySum = 0;
                    expenseCategoryTable.AddCell(new Paragraph(string.Format("{0:C}", expenseCategorySum)));
                }
            }

            //Adds the expense table to the document
            doc.Add(expenseCategoryTable);

            //If the number of months under study is under 12, generates graphs
            if (numMonths < 12)
            {
                //Generates an array to store month names, as well as a current month index
                string[] months = new string[numMonths];
                int month = DateTime.Today.Month;

                //Runss for the number of months being investigated
                for (int i = 0; i < numMonths; ++i)
                {
                    //If the month hits 0 - resets it to 12
                    if (month < 1)
                    {
                        month = 12;
                    }

                    //Determines the name of the month, in natural language from the index, and then reducecs the month by one
                    months[i] = data.monthNameCulture.GetMonthName(month).ToString();
                    --month;
                }

                //Clears the chart
                frm.chrtPDFExport.Series.Clear();

                //Creates the expenses series and adds in the values
                frm.chrtPDFExport.Series.Add("Expenses");
                frm.chrtPDFExport.Series[0].Points.DataBindXY(months, calculatePastMonthsExpenses(numMonths));
                frm.chrtPDFExport.Series[0].ChartType = SeriesChartType.Bar;

                //Creates the revenues series and adds in the valeus
                frm.chrtPDFExport.Series.Add("Revenues");
                frm.chrtPDFExport.Series[1].Points.DataBindXY(months, calculatePastMonthsRevenues(numMonths));
                frm.chrtPDFExport.Series[1].ChartType = SeriesChartType.Bar;

                //Creates a variable to store the chart image  in memory. Then converts to iTextSharp readable format
                MemoryStream chartImage = new MemoryStream();
                frm.chrtPDFExport.SaveImage(chartImage, ChartImageFormat.Png);
                iTextSharp.text.Image chartExport = iTextSharp.text.Image.GetInstance(chartImage.GetBuffer());

                //Sets the chart title, aligned center, and then adds onto pdf with the bar chart itself.
                Paragraph chartTitle = new Paragraph("Monthly Expense/Revenue Stream");
                chartTitle.Alignment = Element.ALIGN_CENTER;
                doc.Add(chartTitle);
                doc.Add(chartExport);

                //Clears the entire chart of both series from above. Adds in a new profit/expense series, and fills it up with values.
                frm.chrtPDFExport.Series.Clear();
                frm.chrtPDFExport.Series.Add("Profit/Expense");
                frm.chrtPDFExport.Series[0].Points.DataBindXY(new string[] { "Profit", "Expenses" }, new double[] { stats[PROFIT], stats[EXPENSES] });
                frm.chrtPDFExport.Series[0].ChartType = SeriesChartType.Pie;

                //Calculates the profit/expenses percentage and adds as labels to the valeus
                double profitPercent = stats[PROFIT] / (stats[EXPENSES] + stats[PROFIT]) * 100d;
                frm.chrtPDFExport.Series["Profit/Expense"].Points[0].AxisLabel = "Profit \n" + Math.Round(profitPercent, 2) + "%";
                frm.chrtPDFExport.Series["Profit/Expense"].Points[1].AxisLabel = "Expense \n" + Math.Round(100 - profitPercent, 2) + "%";

                //Creates a variable to store the chart image  in memory. Then converts to iTextSharp readable format
                MemoryStream pieImage = new MemoryStream();
                frm.chrtPDFExport.SaveImage(pieImage, ChartImageFormat.Png);
                iTextSharp.text.Image pieExport = iTextSharp.text.Image.GetInstance(pieImage.GetBuffer());

                //Sets the chart title, aligned center, and then adds onto pdf with the pie chart itself.
                chartTitle = new Paragraph("Profit Expense Breakdown");
                chartTitle.Alignment = Element.ALIGN_CENTER;
                doc.Add(chartTitle);
                doc.Add(pieExport);
            }

            //Closes the document creation
            doc.Close();

            //Attempts to open up the document in the native system application.
            try
            {
                System.Diagnostics.Process.Start(docPath);
            }
            catch
            {
                //If it couldn't be opened, informs the user
                MessageBox.Show("Document couldn't be opened");
            }
        }
    }
}