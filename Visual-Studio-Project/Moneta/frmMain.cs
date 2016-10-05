/***************************************************************************************************************
 * 
    Author:         Tanay Parikh
    Project Name:   Moneta
    Description:    Main view controller. Manages form actions and calls the appropriate 
                    command in the appropriate module. Includes refrences to the Business Stats, Invoice,
                    Settings, Client and Expense modules. 
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
using MySql.Data.MySqlClient;
using System.Diagnostics;
using System.IO;


namespace Moneta
{
    public partial class FrmMain : Form
    {
        //Stores the main project wide shared data variable
        private SharedData data;
        
        //Variables storing whether the window is currently being dragged and the last point on the screen of the window.
        private bool dragging = false;
        private Point startPoint = new Point(0, 0);
        
        //Variables storing the individual modules
        private InvoiceModule invoices;
        private SettingsModule settings;
        private BusinessStatsModule businessStats;
        private ClientModule clients;
        private ExpenseModule expenses;

        //Loads the item dataset
        public DataSet itemsDataS = new DataSet("items");

        //Main form class constructor
        public FrmMain()
        {
            //Instanstiates the program's shared data
            data = new SharedData();

            //Instanstiates the program's modules, with the form, and the shared data variable as it's parameters. 
            invoices = new InvoiceModule(this, data);
            settings = new SettingsModule(this, data);
            businessStats = new BusinessStatsModule(this, data);
            clients = new ClientModule(this, data);
            expenses = new ExpenseModule(this, data);

            //Initializes the form
            InitializeComponent();

            //Initializes the modules
            invoices.initialize();
            expenses.initialize();
            businessStats.initialize();
            settings.initialize();
        }

        //Loads form elements
        private void frmMain_Load(object sender, EventArgs e)
        {
            data.setupNewProgram();

            //Attempts to load the databases
            try
            {
                //Loads the expenses dataset
                this.expensesTableAdapter.Fill(this.expensesDataSet.expenses);
                
                //Loads the invoices dataset
                this.invoicesTableAdapter.Fill(this.invoicesDataSet.invoices);
                
                //Loads the client dataset
                this.clientsTableAdapter.Fill(this.mydbDataSet.clients);

                //Performs the final instialization of the individual modules.
                //Fills in checkboxes and other special columns in datagridview
                expenses.updateImagesColumn();
                invoices.updateInvoiceCheckboxes();

                //Fills value for autofill of client information
                setUpClientAutofill();

                //Accounts for first initialization
                if (dgvInvoices.Rows.Count > 0)
                {
                    //Ensures no invoice is initially selected
                    dgvInvoices.Rows[0].Cells[0].Selected = false;
                }
            }
            //Catches any exceptions resulting from the inability to access the database
            catch (MySqlException sqle)
            {
                //Informs the user of the error and troubleshooting
                MessageBox.Show("Please restart the program." + sqle.Message);
            }
        }
        

        /****************************************************************************************************************************
         * 
         *                                               General Form Event Handlers
         *                                                
        ****************************************************************************************************************************/
        private void btnClose_Click(object sender, EventArgs e)
        {
            //Stops the mysql server
            string driveName = Application.StartupPath.Substring(0, Application.StartupPath.IndexOf('\\')) + "\\";
            data.startProcess(driveName + "\\MonetaDatabase\\xampplite\\mysql_stop.bat");

            //Closes the application upon selection of the 'x' button
            Application.Exit();
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            //Minimizes the application upon selection of the '-' button
            this.WindowState = FormWindowState.Minimized;
        }

        private void frmMain_MouseDown(object sender, MouseEventArgs e)
        {
            //Indicates the mouse is currently down on the form, dragging begins/continues
            //The current update's window coordinate is stored. 
            dragging = true; 
            startPoint = new Point(e.X, e.Y);
        }

        private void frmMain_MouseUp(object sender, MouseEventArgs e)
        {
            //Indicates that the dragging has stopped
            dragging = false;
        }

        private void frmMain_MouseMove(object sender, MouseEventArgs e)
        {
            //Checks if the window is currently being dragged
            if (dragging)
            {
                //Gets the current cursor position
                Point cursorLocation = PointToScreen(e.Location);

                //Updates the location of the window to the amount moved by the cursor in the x and y directions
                Location = new Point(cursorLocation.X - this.startPoint.X, cursorLocation.Y - this.startPoint.Y);
            }
        }

        private void lblClients_Click(object sender, EventArgs e)
        {
            //Indicates the selection of the clients module.
            //Resets visibilities of other modules, and makes the module nav text green.
            resetVisibilities();
            lblNavClients.ForeColor = data.greenText;
            pnlClients.Visible = true;
        }

        private void lblNavInvoices_Click(object sender, EventArgs e)
        {
            //Indicates the selection of the invoices module.
            //Resets visibilities of other modules, and makes the module nav text green.
            resetVisibilities();
            lblNavInvoices.ForeColor = data.greenText;
            pnlInvoices.Visible = true;
        }

        private void lblNavExpenses_Click(object sender, EventArgs e)
        {
            //Indicates the selection of the expenses module.
            //Resets visibilities of other modules, and makes the module nav text green.
            resetVisibilities();
            lblNavExpenses.ForeColor = data.greenText;
            pnlExpenses.Visible = true;
        }

        private void lblNavBusinessStats_Click(object sender, EventArgs e)
        {
            //Indicates the selection of the business stats module.
            //Resets visibilities of other modules, and makes the module nav text green.
            resetVisibilities();
            lblNavBusinessStats.ForeColor = data.greenText;
            pnlBusinessStats.Visible = true;

            //Sets the default starting and ending dates, and calls for the calculation of the stats
            dtpStatsStart.Value = DateTime.Today.AddDays(-120).Date;
            dtpStatsEnd.Value = DateTime.Today;
            businessStats.calculateStats();
        }

        private void lblNavSettings_Click(object sender, EventArgs e)
        {
            //Indicates the selection of the settings module.
            //Resets visibilities of other modules, and makes the module nav text green.
            resetVisibilities();
            lblNavSettings.ForeColor = data.greenText;
            pnlSettings.Visible = true;
        }

        private void lblNavHome_Click(object sender, EventArgs e)
        {
            //Indicates the selection of the home page.
            //Resets visibilities of other modules, and makes the page nav text green.
            resetVisibilities();
            lblNavHome.ForeColor = data.greenText;
            pnlHome.Visible = true;
        }

        //Pre: None
        //Post: Resets the visibilities and text colours of the nav text.
        //Description: Makes all panels invisible and sets text colors to white for the navigation. 
        private void resetVisibilities()
        {
            //Makes all panels, aside from navigation, invisible
            pnlClients.Visible = false;
            pnlInvoices.Visible = false;
            pnlExpenses.Visible = false;
            pnlBusinessStats.Visible = false;
            pnlSettings.Visible = false;
            pnlHome.Visible = false;

            //Resets all nav link fore colours to white
            lblNavBusinessStats.ForeColor = data.textColorWhite;
            lblNavClients.ForeColor = data.textColorWhite;
            lblNavExpenses.ForeColor = data.textColorWhite;
            lblNavHome.ForeColor = data.textColorWhite;
            lblNavInvoices.ForeColor = data.textColorWhite;
            lblNavSettings.ForeColor = data.textColorWhite;
        }



        /****************************************************************************************************************************
         * 
         *                                                Clients Event Handlers
         *                                                
        ****************************************************************************************************************************/
        public void btnCreateClient_Click(object sender, EventArgs e)
        {
            //Calls for the creation of the client
            clients.createClient();
        }

        public void dgvClients_SelectionChanged(object sender, EventArgs e)
        {
            //Indicates selection has changed in client dgv
            clients.dgvSelectionChanged();
        }

        public void dgvClients_DataError(object sender, DataGridViewDataErrorEventArgs error)
        {
            //Indicates error in client dgv
            clients.dgvDataError(sender, error);
        }



        /****************************************************************************************************************************
         * 
         *                                                Expenses Event Handlers
         *                                                
        ****************************************************************************************************************************/
        public void tmrWorkTime_Tick(object sender, EventArgs e)
        {
            //Instantiates the timer
            expenses.timerTick();
        }

        public void btnTimerReset_Click(object sender, EventArgs e)
        {
            //Resets the timer and values
            expenses.timerReset();
        }

        public void btnTimerStartStop_Click(object sender, EventArgs e)
        {
            //Starts/stops the timer
            expenses.timerStartStop();
        }

        public void btnSubmitDistance_Click(object sender, EventArgs e)
        {
            //Submits the distance into the dgv, and updates dgv entries
            expenses.submitDistance();
            dgvExpenses_SelectionChanged(sender, e);
        }

        private void btnTimeAddExpense_Click(object sender, EventArgs e)
        {
            //Submits the time into the dgv, and updates dgv entries
            expenses.submitTime();
            dgvExpenses_SelectionChanged(sender, e);
        }

        public void dgvExpenses_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Indicates cell click in expenses dgv
            expenses.dgvCellClick(e);
        }

        public void dgvExpenses_SelectionChanged(object sender, EventArgs e)
        {
            //Indicates selection changed in expenses dgv
            expenses.dgvSelectionChanged();
        }

        public void dgvExpenses_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            //Indicates cell has been entered in expenses dgv
            expenses.dgvCellEnter(e);
        }
        
        public void dgvExpenses_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            //Calls for the instantiation of the autocomplete fields for the expense categories
            expenses.displayExpenseCategorizationInfo(e);
        }

        public void dgvExpenses_DataError(object sender, DataGridViewDataErrorEventArgs error)
        {
            //Indicates an error has been encountered with dgv expenses
            expenses.dgvDataError(sender, error);
        }

        private void dgvExpenses_Sorted(object sender, EventArgs e)
        {
            //Redisplays the image column based on the newly sorted dgv
            expenses.updateImagesColumn();
        }

        private void btnAddExpense_Click(object sender, EventArgs e)
        {
            //Indicates the add expense button has been pressed
            expenses.addExpense();
        }

        private void btnAddImage_Click(object sender, EventArgs e)
        {
            //Indicates the add image button has been pressed. Calls for the fetching of the image, if it fails, returns back the existing label.
            btnExpenseAddImage.Text = expenses.fetchImage("Add Image");
        }



        /****************************************************************************************************************************
         * 
         *                                                Invoices Event Handlers
         *                                                
        ****************************************************************************************************************************/
        public void dgvInvoices_Sorted(object sender, EventArgs e)
        {
            //Calls for the updating of dgv invoice checkboxes based on newly sorted arrangement
            invoices.updateInvoiceCheckboxes();
        }

        public void dgvInvoices_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //Calls for the sorting of the invoices
            invoices.sortInvoices(e);
        }

        public void btnPrintInvoice_Click(object sender, EventArgs e)
        {
            //Calls for the printing of the invoice
            invoices.printInvoice();
        }

        public void btnEmailInvoice_Click(object sender, EventArgs e)
        {
            //Calls for the emailing of the invoice
            invoices.sendInvoice();
        }

        public void dgvItems_DataError(object sender, DataGridViewDataErrorEventArgs error)
        {
            //Indicates error in items dgv
            data.displayDGVError(sender, error);
        }

        private void dgvItems_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //Indicates selection change in items dgv
            invoices.dgvItemsSelectionChanged();
        }

        public void dgvInvoices_DataError(object sender, DataGridViewDataErrorEventArgs error)
        {
            //Indicates error in invoices dgv
            data.displayDGVError(sender, error);
        }

        public void dgvInvoices_SelectionChanged(object sender, EventArgs e)
        {
            //Indicates selection change in invoices dgv
            invoices.dgvInvoicesSelectionChanged();
        }

        private void txtInvoiceClientName_TextChanged(object sender, EventArgs e)
        {
            invoices.scanForClientID();
        }

        public void setUpClientAutofill()
        {
            invoices.setUpAutofillClientName();
        }

        public void dgvInvoices_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            //Indicates cell in dgv invoices has been clicked
            invoices.dgvCellClick();
        }

        private void btnCreateInvoice_Click(object sender, EventArgs e)
        {
            //Calls for the creation of the invoice and the updating of the dgv invoices
            invoices.setupNewInvoice();
            dgvInvoices_SelectionChanged(sender, e);
        }

        private void btnCreateEditDone_Click(object sender, EventArgs e)
        {
            //Calls to save modified invoice data if any. 
            invoices.btnCreateDone(sender, e);
        }

        private void btnCreateEditAddItems_Click(object sender, EventArgs e)
        {
            //Confirms creation of invoice with data provided.
            invoices.createInvoice();

        }



        /****************************************************************************************************************************
         * 
         *                                             Business Stats Event Handlers
         *                                                
        ****************************************************************************************************************************/
        private void btnUpdateStats_Click(object sender, EventArgs e)
        {
            //Calls for the recalculation of the stats based on time range selected
            businessStats.calculateStats();
        }

        private void btnGenerateProfitLossStatement_Click(object sender, EventArgs e)
        {
            //Calls for the generation of the profit loss statement
            businessStats.generateProfitLoss();
        }



        /****************************************************************************************************************************
         * 
         *                                                Settings Event Handlers
         *                                                
        ****************************************************************************************************************************/
        private void btnSaveSettings_Click(object sender, EventArgs e)
        {
            //Saves the settings entered
            settings.saveSettings();
        }

        private void dgvSettings_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //Displays the settings dgv error
            data.displayDGVError(sender, e);
        }

        private void dgvSettings_SelectionChanged(object sender, EventArgs e)
        {
            //Indicates selection changed in settings dgv
            settings.dgvSelectionChanged();
        }
    }
}
