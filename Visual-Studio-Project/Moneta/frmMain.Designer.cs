namespace Moneta
{
    partial class FrmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.formsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.frmclientsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.frminvoicesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtCFirstName = new System.Windows.Forms.TextBox();
            this.txtCLastName = new System.Windows.Forms.TextBox();
            this.txtCCompany = new System.Windows.Forms.TextBox();
            this.txtCNumber = new System.Windows.Forms.TextBox();
            this.btnCreateClient = new System.Windows.Forms.Button();
            this.txtCEmail = new System.Windows.Forms.TextBox();
            this.txtCAddress = new System.Windows.Forms.TextBox();
            this.txtCNotes = new System.Windows.Forms.TextBox();
            this.lblCFirstName = new System.Windows.Forms.Label();
            this.lblCLastName = new System.Windows.Forms.Label();
            this.lblCCompany = new System.Windows.Forms.Label();
            this.lblCPhoneNumber = new System.Windows.Forms.Label();
            this.lblCEmail = new System.Windows.Forms.Label();
            this.lblCAddress = new System.Windows.Forms.Label();
            this.lblCNotes = new System.Windows.Forms.Label();
            this.dgvClients = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FirstName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LastName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.companyDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.phoneDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.emailDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.addressDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.noteDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clientsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.mydbDataSet = new Moneta.mydbDataSet();
            this.clientsTableAdapter = new Moneta.mydbDataSetTableAdapters.clientsTableAdapter();
            this.pnlClients = new System.Windows.Forms.Panel();
            this.lblClientTitle = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlMenu = new System.Windows.Forms.Panel();
            this.lblNavSettings = new System.Windows.Forms.Label();
            this.lblNavBusinessStats = new System.Windows.Forms.Label();
            this.lblNavExpenses = new System.Windows.Forms.Label();
            this.lblNavInvoices = new System.Windows.Forms.Label();
            this.lblNavClients = new System.Windows.Forms.Label();
            this.lblNavHome = new System.Windows.Forms.Label();
            this.lblViewSelect = new System.Windows.Forms.Label();
            this.pnlInvoices = new System.Windows.Forms.Panel();
            this.pnlCreateEditInvoice = new System.Windows.Forms.Panel();
            this.btnCreateEditAddItems = new System.Windows.Forms.Button();
            this.btnCreateEditDone = new System.Windows.Forms.Button();
            this.dgvItems = new System.Windows.Forms.DataGridView();
            this.txtInvoiceClientName = new System.Windows.Forms.TextBox();
            this.lblCreateEditInvoiceItems = new System.Windows.Forms.Label();
            this.btnEmailInvoice = new System.Windows.Forms.Button();
            this.lblInvoiceIDTitle = new System.Windows.Forms.Label();
            this.btnPrintInvoice = new System.Windows.Forms.Button();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.numClientID = new System.Windows.Forms.NumericUpDown();
            this.lblClientID = new System.Windows.Forms.Label();
            this.lblInvoiceIDNum = new System.Windows.Forms.Label();
            this.cbxPaid = new System.Windows.Forms.CheckBox();
            this.cbxQuote = new System.Windows.Forms.CheckBox();
            this.btnCreateInvoice = new System.Windows.Forms.Button();
            this.btnInvoiceClearResults = new System.Windows.Forms.Button();
            this.btnInvoiceSearch = new System.Windows.Forms.Button();
            this.txtInvoiceSearch = new System.Windows.Forms.TextBox();
            this.lblnvoiceSearch = new System.Windows.Forms.Label();
            this.dgvInvoices = new System.Windows.Forms.DataGridView();
            this.invoiceIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clientsClientIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.typeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.paidDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.invoicesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.invoicesDataSet = new Moneta.invoicesDataSet();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.invoicesTableAdapter = new Moneta.invoicesDataSetTableAdapters.invoicesTableAdapter();
            this.pnlExpenses = new System.Windows.Forms.Panel();
            this.lblExpenseHourlyWage = new System.Windows.Forms.Label();
            this.numHourlyWage = new System.Windows.Forms.NumericUpDown();
            this.btnTimeAddExpense = new System.Windows.Forms.Button();
            this.lblExpenseTimeInvoiceID = new System.Windows.Forms.Label();
            this.numExpenseTimeInvoiceID = new System.Windows.Forms.NumericUpDown();
            this.btnExpenseAddImage = new System.Windows.Forms.Button();
            this.lblTaxAmount = new System.Windows.Forms.Label();
            this.lblTotalAmount = new System.Windows.Forms.Label();
            this.numExpenseTotalAmount = new System.Windows.Forms.NumericUpDown();
            this.numExpenseTaxAmount = new System.Windows.Forms.NumericUpDown();
            this.lblInvoiceID = new System.Windows.Forms.Label();
            this.cmbExpenseCategory = new System.Windows.Forms.ComboBox();
            this.lblExpenseCategory = new System.Windows.Forms.Label();
            this.numExpenseInvoiceID = new System.Windows.Forms.NumericUpDown();
            this.lblExpenseDescription = new System.Windows.Forms.Label();
            this.lblAddAnExpense = new System.Windows.Forms.Label();
            this.lblExpensesTitle = new System.Windows.Forms.Label();
            this.lblExpenseDate = new System.Windows.Forms.Label();
            this.btnAddExpense = new System.Windows.Forms.Button();
            this.txtExpenseDescription = new System.Windows.Forms.TextBox();
            this.dtpExpenses = new System.Windows.Forms.DateTimePicker();
            this.numDistanceInvoiceID = new System.Windows.Forms.NumericUpDown();
            this.txtDistanceDescription = new System.Windows.Forms.TextBox();
            this.lblDistanceInvoiceID = new System.Windows.Forms.Label();
            this.dgvExpenses = new System.Windows.Forms.DataGridView();
            this.expenseIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descriptionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.imageReferenceDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.invoicesInvoiceIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.expenseCategoryDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.taxAmountDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.totalAmountDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.expensesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.expensesDataSet = new Moneta.expensesDataSet();
            this.lblDistanceDescription = new System.Windows.Forms.Label();
            this.lblDistanceTravelledLabel = new System.Windows.Forms.Label();
            this.numDistance = new System.Windows.Forms.NumericUpDown();
            this.lblMileageTitle = new System.Windows.Forms.Label();
            this.btnSubmitDistance = new System.Windows.Forms.Button();
            this.lblTimerTitle = new System.Windows.Forms.Label();
            this.btnTimerReset = new System.Windows.Forms.Button();
            this.btnTimerStartStop = new System.Windows.Forms.Button();
            this.lblTimerTime = new System.Windows.Forms.Label();
            this.tmrWorkTime = new System.Windows.Forms.Timer(this.components);
            this.expensesTableAdapter = new Moneta.expensesDataSetTableAdapters.expensesTableAdapter();
            this.ofdExpenses = new System.Windows.Forms.OpenFileDialog();
            this.pnlBusinessStats = new System.Windows.Forms.Panel();
            this.lblClientStatsSubHeading = new System.Windows.Forms.Label();
            this.lblBusinessStatsSubHeading = new System.Windows.Forms.Label();
            this.chrtPDFExport = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btnGenerateProfitLossStatement = new System.Windows.Forms.Button();
            this.btnUpdateStats = new System.Windows.Forms.Button();
            this.dgvClientStats = new System.Windows.Forms.DataGridView();
            this.dtpStatsEnd = new System.Windows.Forms.DateTimePicker();
            this.dtpStatsStart = new System.Windows.Forms.DateTimePicker();
            this.lblEndDate = new System.Windows.Forms.Label();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.dgvBusinessStats = new System.Windows.Forms.DataGridView();
            this.lblBusinessStatsTitle = new System.Windows.Forms.Label();
            this.pnlSettings = new System.Windows.Forms.Panel();
            this.btnSaveSettings = new System.Windows.Forms.Button();
            this.dgvSettings = new System.Windows.Forms.DataGridView();
            this.lblSettingsTitle = new System.Windows.Forms.Label();
            this.pnlHome = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblClicktoNavigate = new System.Windows.Forms.Label();
            this.lblHomeTitle = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.btnMinimize = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClients)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.clientsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mydbDataSet)).BeginInit();
            this.pnlClients.SuspendLayout();
            this.pnlMenu.SuspendLayout();
            this.pnlInvoices.SuspendLayout();
            this.pnlCreateEditInvoice.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numClientID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInvoices)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.invoicesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.invoicesDataSet)).BeginInit();
            this.pnlExpenses.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numHourlyWage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numExpenseTimeInvoiceID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numExpenseTotalAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numExpenseTaxAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numExpenseInvoiceID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDistanceInvoiceID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExpenses)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.expensesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.expensesDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDistance)).BeginInit();
            this.pnlBusinessStats.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chrtPDFExport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClientStats)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBusinessStats)).BeginInit();
            this.pnlSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSettings)).BeginInit();
            this.pnlHome.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // formsToolStripMenuItem
            // 
            this.formsToolStripMenuItem.Name = "formsToolStripMenuItem";
            this.formsToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // frmclientsToolStripMenuItem
            // 
            this.frmclientsToolStripMenuItem.Name = "frmclientsToolStripMenuItem";
            this.frmclientsToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // frminvoicesToolStripMenuItem
            // 
            this.frminvoicesToolStripMenuItem.Name = "frminvoicesToolStripMenuItem";
            this.frminvoicesToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // txtCFirstName
            // 
            this.txtCFirstName.Location = new System.Drawing.Point(333, 85);
            this.txtCFirstName.Name = "txtCFirstName";
            this.txtCFirstName.Size = new System.Drawing.Size(184, 20);
            this.txtCFirstName.TabIndex = 0;
            // 
            // txtCLastName
            // 
            this.txtCLastName.Location = new System.Drawing.Point(640, 85);
            this.txtCLastName.Name = "txtCLastName";
            this.txtCLastName.Size = new System.Drawing.Size(184, 20);
            this.txtCLastName.TabIndex = 1;
            // 
            // txtCCompany
            // 
            this.txtCCompany.Location = new System.Drawing.Point(333, 137);
            this.txtCCompany.Name = "txtCCompany";
            this.txtCCompany.Size = new System.Drawing.Size(184, 20);
            this.txtCCompany.TabIndex = 4;
            // 
            // txtCNumber
            // 
            this.txtCNumber.Location = new System.Drawing.Point(333, 111);
            this.txtCNumber.Name = "txtCNumber";
            this.txtCNumber.Size = new System.Drawing.Size(184, 20);
            this.txtCNumber.TabIndex = 2;
            // 
            // btnCreateClient
            // 
            this.btnCreateClient.Location = new System.Drawing.Point(724, 210);
            this.btnCreateClient.Name = "btnCreateClient";
            this.btnCreateClient.Size = new System.Drawing.Size(100, 23);
            this.btnCreateClient.TabIndex = 4;
            this.btnCreateClient.Text = "Create Client";
            this.btnCreateClient.UseVisualStyleBackColor = true;
            this.btnCreateClient.Click += new System.EventHandler(this.btnCreateClient_Click);
            // 
            // txtCEmail
            // 
            this.txtCEmail.Location = new System.Drawing.Point(640, 111);
            this.txtCEmail.Name = "txtCEmail";
            this.txtCEmail.Size = new System.Drawing.Size(184, 20);
            this.txtCEmail.TabIndex = 3;
            // 
            // txtCAddress
            // 
            this.txtCAddress.Location = new System.Drawing.Point(640, 137);
            this.txtCAddress.Name = "txtCAddress";
            this.txtCAddress.Size = new System.Drawing.Size(184, 20);
            this.txtCAddress.TabIndex = 5;
            // 
            // txtCNotes
            // 
            this.txtCNotes.Location = new System.Drawing.Point(333, 163);
            this.txtCNotes.Multiline = true;
            this.txtCNotes.Name = "txtCNotes";
            this.txtCNotes.Size = new System.Drawing.Size(491, 40);
            this.txtCNotes.TabIndex = 6;
            // 
            // lblCFirstName
            // 
            this.lblCFirstName.AutoSize = true;
            this.lblCFirstName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(224)))), ((int)(((byte)(230)))));
            this.lblCFirstName.Location = new System.Drawing.Point(265, 88);
            this.lblCFirstName.Name = "lblCFirstName";
            this.lblCFirstName.Size = new System.Drawing.Size(61, 13);
            this.lblCFirstName.TabIndex = 9;
            this.lblCFirstName.Text = "First Name*";
            // 
            // lblCLastName
            // 
            this.lblCLastName.AutoSize = true;
            this.lblCLastName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(224)))), ((int)(((byte)(230)))));
            this.lblCLastName.Location = new System.Drawing.Point(572, 88);
            this.lblCLastName.Name = "lblCLastName";
            this.lblCLastName.Size = new System.Drawing.Size(62, 13);
            this.lblCLastName.TabIndex = 10;
            this.lblCLastName.Text = "Last Name*";
            // 
            // lblCCompany
            // 
            this.lblCCompany.AutoSize = true;
            this.lblCCompany.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(224)))), ((int)(((byte)(230)))));
            this.lblCCompany.Location = new System.Drawing.Point(277, 140);
            this.lblCCompany.Name = "lblCCompany";
            this.lblCCompany.Size = new System.Drawing.Size(51, 13);
            this.lblCCompany.TabIndex = 11;
            this.lblCCompany.Text = "Company";
            // 
            // lblCPhoneNumber
            // 
            this.lblCPhoneNumber.AutoSize = true;
            this.lblCPhoneNumber.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(224)))), ((int)(((byte)(230)))));
            this.lblCPhoneNumber.Location = new System.Drawing.Point(244, 114);
            this.lblCPhoneNumber.Name = "lblCPhoneNumber";
            this.lblCPhoneNumber.Size = new System.Drawing.Size(82, 13);
            this.lblCPhoneNumber.TabIndex = 12;
            this.lblCPhoneNumber.Text = "Phone Number*";
            // 
            // lblCEmail
            // 
            this.lblCEmail.AutoSize = true;
            this.lblCEmail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(224)))), ((int)(((byte)(230)))));
            this.lblCEmail.Location = new System.Drawing.Point(597, 114);
            this.lblCEmail.Name = "lblCEmail";
            this.lblCEmail.Size = new System.Drawing.Size(36, 13);
            this.lblCEmail.TabIndex = 13;
            this.lblCEmail.Text = "Email*";
            // 
            // lblCAddress
            // 
            this.lblCAddress.AutoSize = true;
            this.lblCAddress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(224)))), ((int)(((byte)(230)))));
            this.lblCAddress.Location = new System.Drawing.Point(560, 140);
            this.lblCAddress.Name = "lblCAddress";
            this.lblCAddress.Size = new System.Drawing.Size(74, 13);
            this.lblCAddress.TabIndex = 14;
            this.lblCAddress.Text = "Client Address";
            // 
            // lblCNotes
            // 
            this.lblCNotes.AutoSize = true;
            this.lblCNotes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(224)))), ((int)(((byte)(230)))));
            this.lblCNotes.Location = new System.Drawing.Point(292, 166);
            this.lblCNotes.Name = "lblCNotes";
            this.lblCNotes.Size = new System.Drawing.Size(35, 13);
            this.lblCNotes.TabIndex = 15;
            this.lblCNotes.Text = "Notes";
            // 
            // dgvClients
            // 
            this.dgvClients.AllowUserToAddRows = false;
            this.dgvClients.AllowUserToDeleteRows = false;
            this.dgvClients.AutoGenerateColumns = false;
            this.dgvClients.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(35)))), ((int)(((byte)(38)))));
            this.dgvClients.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvClients.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvClients.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.FirstName,
            this.LastName,
            this.companyDataGridViewTextBoxColumn,
            this.phoneDataGridViewTextBoxColumn,
            this.emailDataGridViewTextBoxColumn,
            this.addressDataGridViewTextBoxColumn,
            this.noteDataGridViewTextBoxColumn});
            this.dgvClients.DataSource = this.clientsBindingSource;
            this.dgvClients.Location = new System.Drawing.Point(0, 285);
            this.dgvClients.Name = "dgvClients";
            this.dgvClients.Size = new System.Drawing.Size(977, 204);
            this.dgvClients.TabIndex = 17;
            this.dgvClients.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvClients_DataError);
            this.dgvClients.SelectionChanged += new System.EventHandler(this.dgvClients_SelectionChanged);
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "ClientID";
            this.Column1.HeaderText = "Client ID";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 65;
            // 
            // FirstName
            // 
            this.FirstName.DataPropertyName = "FirstName";
            this.FirstName.HeaderText = "FirstName";
            this.FirstName.Name = "FirstName";
            // 
            // LastName
            // 
            this.LastName.DataPropertyName = "LastName";
            this.LastName.HeaderText = "LastName";
            this.LastName.Name = "LastName";
            // 
            // companyDataGridViewTextBoxColumn
            // 
            this.companyDataGridViewTextBoxColumn.DataPropertyName = "Company";
            this.companyDataGridViewTextBoxColumn.HeaderText = "Company";
            this.companyDataGridViewTextBoxColumn.Name = "companyDataGridViewTextBoxColumn";
            // 
            // phoneDataGridViewTextBoxColumn
            // 
            this.phoneDataGridViewTextBoxColumn.DataPropertyName = "Phone";
            this.phoneDataGridViewTextBoxColumn.HeaderText = "Phone";
            this.phoneDataGridViewTextBoxColumn.Name = "phoneDataGridViewTextBoxColumn";
            this.phoneDataGridViewTextBoxColumn.Width = 120;
            // 
            // emailDataGridViewTextBoxColumn
            // 
            this.emailDataGridViewTextBoxColumn.DataPropertyName = "Email";
            this.emailDataGridViewTextBoxColumn.HeaderText = "Email";
            this.emailDataGridViewTextBoxColumn.Name = "emailDataGridViewTextBoxColumn";
            this.emailDataGridViewTextBoxColumn.Width = 120;
            // 
            // addressDataGridViewTextBoxColumn
            // 
            this.addressDataGridViewTextBoxColumn.DataPropertyName = "Address";
            this.addressDataGridViewTextBoxColumn.HeaderText = "Address";
            this.addressDataGridViewTextBoxColumn.Name = "addressDataGridViewTextBoxColumn";
            this.addressDataGridViewTextBoxColumn.Width = 150;
            // 
            // noteDataGridViewTextBoxColumn
            // 
            this.noteDataGridViewTextBoxColumn.DataPropertyName = "Note";
            this.noteDataGridViewTextBoxColumn.HeaderText = "Note";
            this.noteDataGridViewTextBoxColumn.Name = "noteDataGridViewTextBoxColumn";
            this.noteDataGridViewTextBoxColumn.Width = 150;
            // 
            // clientsBindingSource
            // 
            this.clientsBindingSource.DataMember = "clients";
            this.clientsBindingSource.DataSource = this.mydbDataSet;
            // 
            // mydbDataSet
            // 
            this.mydbDataSet.DataSetName = "mydbDataSet";
            this.mydbDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // clientsTableAdapter
            // 
            this.clientsTableAdapter.ClearBeforeFill = true;
            // 
            // pnlClients
            // 
            this.pnlClients.Controls.Add(this.lblClientTitle);
            this.pnlClients.Controls.Add(this.dgvClients);
            this.pnlClients.Controls.Add(this.txtCFirstName);
            this.pnlClients.Controls.Add(this.lblCNotes);
            this.pnlClients.Controls.Add(this.txtCLastName);
            this.pnlClients.Controls.Add(this.lblCAddress);
            this.pnlClients.Controls.Add(this.txtCCompany);
            this.pnlClients.Controls.Add(this.lblCEmail);
            this.pnlClients.Controls.Add(this.txtCNumber);
            this.pnlClients.Controls.Add(this.lblCPhoneNumber);
            this.pnlClients.Controls.Add(this.btnCreateClient);
            this.pnlClients.Controls.Add(this.lblCCompany);
            this.pnlClients.Controls.Add(this.lblCLastName);
            this.pnlClients.Controls.Add(this.txtCEmail);
            this.pnlClients.Controls.Add(this.lblCFirstName);
            this.pnlClients.Controls.Add(this.txtCAddress);
            this.pnlClients.Controls.Add(this.txtCNotes);
            this.pnlClients.Location = new System.Drawing.Point(4, 68);
            this.pnlClients.Name = "pnlClients";
            this.pnlClients.Size = new System.Drawing.Size(977, 491);
            this.pnlClients.TabIndex = 18;
            // 
            // lblClientTitle
            // 
            this.lblClientTitle.AutoSize = true;
            this.lblClientTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClientTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(224)))), ((int)(((byte)(230)))));
            this.lblClientTitle.Location = new System.Drawing.Point(424, 5);
            this.lblClientTitle.Name = "lblClientTitle";
            this.lblClientTitle.Size = new System.Drawing.Size(129, 17);
            this.lblClientTitle.TabIndex = 30;
            this.lblClientTitle.Text = "Client Management";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(224)))), ((int)(((byte)(230)))));
            this.lblTitle.Location = new System.Drawing.Point(375, 7);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(251, 17);
            this.lblTitle.TabIndex = 22;
            this.lblTitle.Text = "Moneta Financial Management System";
            // 
            // pnlMenu
            // 
            this.pnlMenu.Controls.Add(this.lblNavSettings);
            this.pnlMenu.Controls.Add(this.lblNavBusinessStats);
            this.pnlMenu.Controls.Add(this.lblNavExpenses);
            this.pnlMenu.Controls.Add(this.lblNavInvoices);
            this.pnlMenu.Controls.Add(this.lblNavClients);
            this.pnlMenu.Controls.Add(this.lblNavHome);
            this.pnlMenu.Location = new System.Drawing.Point(4, 63);
            this.pnlMenu.Name = "pnlMenu";
            this.pnlMenu.Size = new System.Drawing.Size(138, 205);
            this.pnlMenu.TabIndex = 23;
            // 
            // lblNavSettings
            // 
            this.lblNavSettings.AutoSize = true;
            this.lblNavSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNavSettings.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(224)))), ((int)(((byte)(230)))));
            this.lblNavSettings.Location = new System.Drawing.Point(20, 135);
            this.lblNavSettings.Name = "lblNavSettings";
            this.lblNavSettings.Size = new System.Drawing.Size(59, 17);
            this.lblNavSettings.TabIndex = 7;
            this.lblNavSettings.Text = "Settings";
            this.lblNavSettings.Click += new System.EventHandler(this.lblNavSettings_Click);
            // 
            // lblNavBusinessStats
            // 
            this.lblNavBusinessStats.AutoSize = true;
            this.lblNavBusinessStats.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNavBusinessStats.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(224)))), ((int)(((byte)(230)))));
            this.lblNavBusinessStats.Location = new System.Drawing.Point(20, 114);
            this.lblNavBusinessStats.Name = "lblNavBusinessStats";
            this.lblNavBusinessStats.Size = new System.Drawing.Size(101, 17);
            this.lblNavBusinessStats.TabIndex = 4;
            this.lblNavBusinessStats.Text = "Business Stats";
            this.lblNavBusinessStats.Click += new System.EventHandler(this.lblNavBusinessStats_Click);
            // 
            // lblNavExpenses
            // 
            this.lblNavExpenses.AutoSize = true;
            this.lblNavExpenses.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNavExpenses.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(224)))), ((int)(((byte)(230)))));
            this.lblNavExpenses.Location = new System.Drawing.Point(20, 92);
            this.lblNavExpenses.Name = "lblNavExpenses";
            this.lblNavExpenses.Size = new System.Drawing.Size(69, 17);
            this.lblNavExpenses.TabIndex = 3;
            this.lblNavExpenses.Text = "Expenses";
            this.lblNavExpenses.Click += new System.EventHandler(this.lblNavExpenses_Click);
            // 
            // lblNavInvoices
            // 
            this.lblNavInvoices.AutoSize = true;
            this.lblNavInvoices.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNavInvoices.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(224)))), ((int)(((byte)(230)))));
            this.lblNavInvoices.Location = new System.Drawing.Point(20, 70);
            this.lblNavInvoices.Name = "lblNavInvoices";
            this.lblNavInvoices.Size = new System.Drawing.Size(59, 17);
            this.lblNavInvoices.TabIndex = 2;
            this.lblNavInvoices.Text = "Invoices";
            this.lblNavInvoices.Click += new System.EventHandler(this.lblNavInvoices_Click);
            // 
            // lblNavClients
            // 
            this.lblNavClients.AutoSize = true;
            this.lblNavClients.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNavClients.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(224)))), ((int)(((byte)(230)))));
            this.lblNavClients.Location = new System.Drawing.Point(20, 47);
            this.lblNavClients.Name = "lblNavClients";
            this.lblNavClients.Size = new System.Drawing.Size(50, 17);
            this.lblNavClients.TabIndex = 1;
            this.lblNavClients.Text = "Clients";
            this.lblNavClients.Click += new System.EventHandler(this.lblClients_Click);
            // 
            // lblNavHome
            // 
            this.lblNavHome.AutoSize = true;
            this.lblNavHome.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNavHome.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(224)))), ((int)(((byte)(230)))));
            this.lblNavHome.Location = new System.Drawing.Point(20, 25);
            this.lblNavHome.Name = "lblNavHome";
            this.lblNavHome.Size = new System.Drawing.Size(45, 17);
            this.lblNavHome.TabIndex = 0;
            this.lblNavHome.Text = "Home";
            this.lblNavHome.Click += new System.EventHandler(this.lblNavHome_Click);
            // 
            // lblViewSelect
            // 
            this.lblViewSelect.AutoSize = true;
            this.lblViewSelect.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblViewSelect.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(224)))), ((int)(((byte)(230)))));
            this.lblViewSelect.Location = new System.Drawing.Point(194, 19);
            this.lblViewSelect.Name = "lblViewSelect";
            this.lblViewSelect.Size = new System.Drawing.Size(274, 17);
            this.lblViewSelect.TabIndex = 25;
            this.lblViewSelect.Text = "Please select an Invoice ID to View Invoice";
            // 
            // pnlInvoices
            // 
            this.pnlInvoices.Controls.Add(this.pnlCreateEditInvoice);
            this.pnlInvoices.Controls.Add(this.btnCreateInvoice);
            this.pnlInvoices.Controls.Add(this.btnInvoiceClearResults);
            this.pnlInvoices.Controls.Add(this.btnInvoiceSearch);
            this.pnlInvoices.Controls.Add(this.txtInvoiceSearch);
            this.pnlInvoices.Controls.Add(this.lblnvoiceSearch);
            this.pnlInvoices.Controls.Add(this.dgvInvoices);
            this.pnlInvoices.Controls.Add(this.lblViewSelect);
            this.pnlInvoices.Location = new System.Drawing.Point(4, 69);
            this.pnlInvoices.Name = "pnlInvoices";
            this.pnlInvoices.Size = new System.Drawing.Size(977, 490);
            this.pnlInvoices.TabIndex = 25;
            // 
            // pnlCreateEditInvoice
            // 
            this.pnlCreateEditInvoice.Controls.Add(this.btnCreateEditAddItems);
            this.pnlCreateEditInvoice.Controls.Add(this.btnCreateEditDone);
            this.pnlCreateEditInvoice.Controls.Add(this.dgvItems);
            this.pnlCreateEditInvoice.Controls.Add(this.txtInvoiceClientName);
            this.pnlCreateEditInvoice.Controls.Add(this.lblCreateEditInvoiceItems);
            this.pnlCreateEditInvoice.Controls.Add(this.btnEmailInvoice);
            this.pnlCreateEditInvoice.Controls.Add(this.lblInvoiceIDTitle);
            this.pnlCreateEditInvoice.Controls.Add(this.btnPrintInvoice);
            this.pnlCreateEditInvoice.Controls.Add(this.dtpDate);
            this.pnlCreateEditInvoice.Controls.Add(this.numClientID);
            this.pnlCreateEditInvoice.Controls.Add(this.lblClientID);
            this.pnlCreateEditInvoice.Controls.Add(this.lblInvoiceIDNum);
            this.pnlCreateEditInvoice.Controls.Add(this.cbxPaid);
            this.pnlCreateEditInvoice.Controls.Add(this.cbxQuote);
            this.pnlCreateEditInvoice.Location = new System.Drawing.Point(156, 19);
            this.pnlCreateEditInvoice.Name = "pnlCreateEditInvoice";
            this.pnlCreateEditInvoice.Size = new System.Drawing.Size(804, 448);
            this.pnlCreateEditInvoice.TabIndex = 44;
            this.pnlCreateEditInvoice.Visible = false;
            // 
            // btnCreateEditAddItems
            // 
            this.btnCreateEditAddItems.Location = new System.Drawing.Point(197, 145);
            this.btnCreateEditAddItems.Name = "btnCreateEditAddItems";
            this.btnCreateEditAddItems.Size = new System.Drawing.Size(102, 23);
            this.btnCreateEditAddItems.TabIndex = 45;
            this.btnCreateEditAddItems.Text = "Add Items";
            this.btnCreateEditAddItems.UseVisualStyleBackColor = true;
            this.btnCreateEditAddItems.Click += new System.EventHandler(this.btnCreateEditAddItems_Click);
            // 
            // btnCreateEditDone
            // 
            this.btnCreateEditDone.Location = new System.Drawing.Point(23, 14);
            this.btnCreateEditDone.Name = "btnCreateEditDone";
            this.btnCreateEditDone.Size = new System.Drawing.Size(75, 23);
            this.btnCreateEditDone.TabIndex = 44;
            this.btnCreateEditDone.Text = "Done";
            this.btnCreateEditDone.UseVisualStyleBackColor = true;
            this.btnCreateEditDone.Click += new System.EventHandler(this.btnCreateEditDone_Click);
            // 
            // dgvItems
            // 
            this.dgvItems.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(35)))), ((int)(((byte)(38)))));
            this.dgvItems.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItems.Location = new System.Drawing.Point(5, 208);
            this.dgvItems.Name = "dgvItems";
            this.dgvItems.Size = new System.Drawing.Size(792, 225);
            this.dgvItems.TabIndex = 27;
            this.dgvItems.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvItems_CellValueChanged);
            this.dgvItems.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvItems_DataError);
            // 
            // txtInvoiceClientName
            // 
            this.txtInvoiceClientName.Location = new System.Drawing.Point(197, 84);
            this.txtInvoiceClientName.Name = "txtInvoiceClientName";
            this.txtInvoiceClientName.Size = new System.Drawing.Size(217, 20);
            this.txtInvoiceClientName.TabIndex = 43;
            this.txtInvoiceClientName.TextChanged += new System.EventHandler(this.txtInvoiceClientName_TextChanged);
            // 
            // lblCreateEditInvoiceItems
            // 
            this.lblCreateEditInvoiceItems.AutoSize = true;
            this.lblCreateEditInvoiceItems.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreateEditInvoiceItems.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(224)))), ((int)(((byte)(230)))));
            this.lblCreateEditInvoiceItems.Location = new System.Drawing.Point(43, 189);
            this.lblCreateEditInvoiceItems.Name = "lblCreateEditInvoiceItems";
            this.lblCreateEditInvoiceItems.Size = new System.Drawing.Size(428, 17);
            this.lblCreateEditInvoiceItems.TabIndex = 26;
            this.lblCreateEditInvoiceItems.Text = "Please select an item to edit it or fill out the last row to add an item.";
            this.lblCreateEditInvoiceItems.Visible = false;
            // 
            // btnEmailInvoice
            // 
            this.btnEmailInvoice.Location = new System.Drawing.Point(498, 95);
            this.btnEmailInvoice.Name = "btnEmailInvoice";
            this.btnEmailInvoice.Size = new System.Drawing.Size(103, 23);
            this.btnEmailInvoice.TabIndex = 42;
            this.btnEmailInvoice.Text = "Email Invoice";
            this.btnEmailInvoice.UseVisualStyleBackColor = true;
            this.btnEmailInvoice.Click += new System.EventHandler(this.btnEmailInvoice_Click);
            // 
            // lblInvoiceIDTitle
            // 
            this.lblInvoiceIDTitle.AutoSize = true;
            this.lblInvoiceIDTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(224)))), ((int)(((byte)(230)))));
            this.lblInvoiceIDTitle.Location = new System.Drawing.Point(198, 27);
            this.lblInvoiceIDTitle.Name = "lblInvoiceIDTitle";
            this.lblInvoiceIDTitle.Size = new System.Drawing.Size(56, 13);
            this.lblInvoiceIDTitle.TabIndex = 37;
            this.lblInvoiceIDTitle.Text = "Invoice ID";
            // 
            // btnPrintInvoice
            // 
            this.btnPrintInvoice.Location = new System.Drawing.Point(498, 60);
            this.btnPrintInvoice.Name = "btnPrintInvoice";
            this.btnPrintInvoice.Size = new System.Drawing.Size(102, 23);
            this.btnPrintInvoice.TabIndex = 41;
            this.btnPrintInvoice.Text = "Print Invoice";
            this.btnPrintInvoice.UseVisualStyleBackColor = true;
            this.btnPrintInvoice.Click += new System.EventHandler(this.btnPrintInvoice_Click);
            // 
            // dtpDate
            // 
            this.dtpDate.Location = new System.Drawing.Point(198, 114);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(216, 20);
            this.dtpDate.TabIndex = 29;
            // 
            // numClientID
            // 
            this.numClientID.Location = new System.Drawing.Point(272, 54);
            this.numClientID.Name = "numClientID";
            this.numClientID.Size = new System.Drawing.Size(68, 20);
            this.numClientID.TabIndex = 39;
            // 
            // lblClientID
            // 
            this.lblClientID.AutoSize = true;
            this.lblClientID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(224)))), ((int)(((byte)(230)))));
            this.lblClientID.Location = new System.Drawing.Point(198, 56);
            this.lblClientID.Name = "lblClientID";
            this.lblClientID.Size = new System.Drawing.Size(47, 13);
            this.lblClientID.TabIndex = 30;
            this.lblClientID.Text = "Client ID";
            // 
            // lblInvoiceIDNum
            // 
            this.lblInvoiceIDNum.AutoSize = true;
            this.lblInvoiceIDNum.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(224)))), ((int)(((byte)(230)))));
            this.lblInvoiceIDNum.Location = new System.Drawing.Point(269, 27);
            this.lblInvoiceIDNum.Name = "lblInvoiceIDNum";
            this.lblInvoiceIDNum.Size = new System.Drawing.Size(67, 13);
            this.lblInvoiceIDNum.TabIndex = 38;
            this.lblInvoiceIDNum.Text = "New Invoice";
            // 
            // cbxPaid
            // 
            this.cbxPaid.AutoSize = true;
            this.cbxPaid.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(224)))), ((int)(((byte)(230)))));
            this.cbxPaid.Location = new System.Drawing.Point(367, 26);
            this.cbxPaid.Name = "cbxPaid";
            this.cbxPaid.Size = new System.Drawing.Size(47, 17);
            this.cbxPaid.TabIndex = 33;
            this.cbxPaid.Text = "Paid";
            this.cbxPaid.UseVisualStyleBackColor = true;
            // 
            // cbxQuote
            // 
            this.cbxQuote.AutoSize = true;
            this.cbxQuote.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(224)))), ((int)(((byte)(230)))));
            this.cbxQuote.Location = new System.Drawing.Point(367, 54);
            this.cbxQuote.Name = "cbxQuote";
            this.cbxQuote.Size = new System.Drawing.Size(55, 17);
            this.cbxQuote.TabIndex = 35;
            this.cbxQuote.Text = "Quote";
            this.cbxQuote.UseVisualStyleBackColor = true;
            // 
            // btnCreateInvoice
            // 
            this.btnCreateInvoice.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(199)))), ((int)(((byte)(83)))));
            this.btnCreateInvoice.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCreateInvoice.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreateInvoice.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnCreateInvoice.Location = new System.Drawing.Point(744, 38);
            this.btnCreateInvoice.Name = "btnCreateInvoice";
            this.btnCreateInvoice.Size = new System.Drawing.Size(216, 52);
            this.btnCreateInvoice.TabIndex = 48;
            this.btnCreateInvoice.Text = "Create Invoice";
            this.btnCreateInvoice.UseVisualStyleBackColor = false;
            this.btnCreateInvoice.Click += new System.EventHandler(this.btnCreateInvoice_Click);
            // 
            // btnInvoiceClearResults
            // 
            this.btnInvoiceClearResults.Location = new System.Drawing.Point(857, 176);
            this.btnInvoiceClearResults.Name = "btnInvoiceClearResults";
            this.btnInvoiceClearResults.Size = new System.Drawing.Size(103, 23);
            this.btnInvoiceClearResults.TabIndex = 46;
            this.btnInvoiceClearResults.Text = "Clear Results";
            this.btnInvoiceClearResults.UseVisualStyleBackColor = true;
            this.btnInvoiceClearResults.Visible = false;
            // 
            // btnInvoiceSearch
            // 
            this.btnInvoiceSearch.Location = new System.Drawing.Point(743, 176);
            this.btnInvoiceSearch.Name = "btnInvoiceSearch";
            this.btnInvoiceSearch.Size = new System.Drawing.Size(103, 23);
            this.btnInvoiceSearch.TabIndex = 47;
            this.btnInvoiceSearch.Text = "Search";
            this.btnInvoiceSearch.UseVisualStyleBackColor = true;
            this.btnInvoiceSearch.Visible = false;
            // 
            // txtInvoiceSearch
            // 
            this.txtInvoiceSearch.Location = new System.Drawing.Point(743, 149);
            this.txtInvoiceSearch.Name = "txtInvoiceSearch";
            this.txtInvoiceSearch.Size = new System.Drawing.Size(217, 20);
            this.txtInvoiceSearch.TabIndex = 45;
            this.txtInvoiceSearch.Visible = false;
            // 
            // lblnvoiceSearch
            // 
            this.lblnvoiceSearch.AutoSize = true;
            this.lblnvoiceSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblnvoiceSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(224)))), ((int)(((byte)(230)))));
            this.lblnvoiceSearch.Location = new System.Drawing.Point(743, 126);
            this.lblnvoiceSearch.Name = "lblnvoiceSearch";
            this.lblnvoiceSearch.Size = new System.Drawing.Size(96, 15);
            this.lblnvoiceSearch.TabIndex = 34;
            this.lblnvoiceSearch.Text = "Search Invoices:";
            this.lblnvoiceSearch.Visible = false;
            // 
            // dgvInvoices
            // 
            this.dgvInvoices.AllowUserToAddRows = false;
            this.dgvInvoices.AllowUserToDeleteRows = false;
            this.dgvInvoices.AutoGenerateColumns = false;
            this.dgvInvoices.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(35)))), ((int)(((byte)(38)))));
            this.dgvInvoices.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvInvoices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInvoices.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.invoiceIDDataGridViewTextBoxColumn,
            this.dateDataGridViewTextBoxColumn,
            this.clientsClientIDDataGridViewTextBoxColumn,
            this.typeDataGridViewTextBoxColumn,
            this.paidDataGridViewTextBoxColumn});
            this.dgvInvoices.DataSource = this.invoicesBindingSource;
            this.dgvInvoices.Location = new System.Drawing.Point(156, 41);
            this.dgvInvoices.Name = "dgvInvoices";
            this.dgvInvoices.Size = new System.Drawing.Size(581, 424);
            this.dgvInvoices.TabIndex = 28;
            this.dgvInvoices.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvInvoices_CellClick_1);
            this.dgvInvoices.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvInvoices_ColumnHeaderMouseClick);
            this.dgvInvoices.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvInvoices_DataError);
            this.dgvInvoices.SelectionChanged += new System.EventHandler(this.dgvInvoices_SelectionChanged);
            this.dgvInvoices.Sorted += new System.EventHandler(this.dgvInvoices_Sorted);
            // 
            // invoiceIDDataGridViewTextBoxColumn
            // 
            this.invoiceIDDataGridViewTextBoxColumn.DataPropertyName = "InvoiceID";
            this.invoiceIDDataGridViewTextBoxColumn.HeaderText = "Invoice ID";
            this.invoiceIDDataGridViewTextBoxColumn.Name = "invoiceIDDataGridViewTextBoxColumn";
            this.invoiceIDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // dateDataGridViewTextBoxColumn
            // 
            this.dateDataGridViewTextBoxColumn.DataPropertyName = "Date";
            this.dateDataGridViewTextBoxColumn.HeaderText = "Date";
            this.dateDataGridViewTextBoxColumn.Name = "dateDataGridViewTextBoxColumn";
            // 
            // clientsClientIDDataGridViewTextBoxColumn
            // 
            this.clientsClientIDDataGridViewTextBoxColumn.DataPropertyName = "Clients_ClientID";
            this.clientsClientIDDataGridViewTextBoxColumn.HeaderText = "Client ID";
            this.clientsClientIDDataGridViewTextBoxColumn.Name = "clientsClientIDDataGridViewTextBoxColumn";
            this.clientsClientIDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // typeDataGridViewTextBoxColumn
            // 
            this.typeDataGridViewTextBoxColumn.DataPropertyName = "Type";
            this.typeDataGridViewTextBoxColumn.HeaderText = "Type";
            this.typeDataGridViewTextBoxColumn.Name = "typeDataGridViewTextBoxColumn";
            // 
            // paidDataGridViewTextBoxColumn
            // 
            this.paidDataGridViewTextBoxColumn.DataPropertyName = "Paid";
            this.paidDataGridViewTextBoxColumn.HeaderText = "Paid";
            this.paidDataGridViewTextBoxColumn.Name = "paidDataGridViewTextBoxColumn";
            // 
            // invoicesBindingSource
            // 
            this.invoicesBindingSource.DataMember = "invoices";
            this.invoicesBindingSource.DataSource = this.invoicesDataSet;
            // 
            // invoicesDataSet
            // 
            this.invoicesDataSet.DataSetName = "invoicesDataSet";
            this.invoicesDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "ClientID";
            this.dataGridViewTextBoxColumn1.HeaderText = "Client ID";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 65;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "FirstName";
            this.dataGridViewTextBoxColumn2.HeaderText = "FirstName";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "LastName";
            this.dataGridViewTextBoxColumn3.HeaderText = "LastName";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "Company";
            this.dataGridViewTextBoxColumn4.HeaderText = "Company";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "Phone";
            this.dataGridViewTextBoxColumn5.HeaderText = "Phone";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.Width = 120;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "Email";
            this.dataGridViewTextBoxColumn6.HeaderText = "Email";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.Width = 120;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "Address";
            this.dataGridViewTextBoxColumn7.HeaderText = "Address";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.Width = 150;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.DataPropertyName = "Note";
            this.dataGridViewTextBoxColumn8.HeaderText = "Note";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.Width = 150;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.DataPropertyName = "InvoiceID";
            this.dataGridViewTextBoxColumn9.HeaderText = "Invoice ID";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.DataPropertyName = "Date";
            this.dataGridViewTextBoxColumn10.HeaderText = "Date";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.DataPropertyName = "Client ID";
            this.dataGridViewTextBoxColumn11.HeaderText = "Client ID";
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            this.dataGridViewTextBoxColumn11.ReadOnly = true;
            // 
            // invoicesTableAdapter
            // 
            this.invoicesTableAdapter.ClearBeforeFill = true;
            // 
            // pnlExpenses
            // 
            this.pnlExpenses.Controls.Add(this.lblExpenseHourlyWage);
            this.pnlExpenses.Controls.Add(this.numHourlyWage);
            this.pnlExpenses.Controls.Add(this.btnTimeAddExpense);
            this.pnlExpenses.Controls.Add(this.lblExpenseTimeInvoiceID);
            this.pnlExpenses.Controls.Add(this.numExpenseTimeInvoiceID);
            this.pnlExpenses.Controls.Add(this.btnExpenseAddImage);
            this.pnlExpenses.Controls.Add(this.lblTaxAmount);
            this.pnlExpenses.Controls.Add(this.lblTotalAmount);
            this.pnlExpenses.Controls.Add(this.numExpenseTotalAmount);
            this.pnlExpenses.Controls.Add(this.numExpenseTaxAmount);
            this.pnlExpenses.Controls.Add(this.lblInvoiceID);
            this.pnlExpenses.Controls.Add(this.cmbExpenseCategory);
            this.pnlExpenses.Controls.Add(this.lblExpenseCategory);
            this.pnlExpenses.Controls.Add(this.numExpenseInvoiceID);
            this.pnlExpenses.Controls.Add(this.lblExpenseDescription);
            this.pnlExpenses.Controls.Add(this.lblAddAnExpense);
            this.pnlExpenses.Controls.Add(this.lblExpensesTitle);
            this.pnlExpenses.Controls.Add(this.lblExpenseDate);
            this.pnlExpenses.Controls.Add(this.btnAddExpense);
            this.pnlExpenses.Controls.Add(this.txtExpenseDescription);
            this.pnlExpenses.Controls.Add(this.dtpExpenses);
            this.pnlExpenses.Controls.Add(this.numDistanceInvoiceID);
            this.pnlExpenses.Controls.Add(this.txtDistanceDescription);
            this.pnlExpenses.Controls.Add(this.lblDistanceInvoiceID);
            this.pnlExpenses.Controls.Add(this.dgvExpenses);
            this.pnlExpenses.Controls.Add(this.lblDistanceDescription);
            this.pnlExpenses.Controls.Add(this.lblDistanceTravelledLabel);
            this.pnlExpenses.Controls.Add(this.numDistance);
            this.pnlExpenses.Controls.Add(this.lblMileageTitle);
            this.pnlExpenses.Controls.Add(this.btnSubmitDistance);
            this.pnlExpenses.Controls.Add(this.lblTimerTitle);
            this.pnlExpenses.Controls.Add(this.btnTimerReset);
            this.pnlExpenses.Controls.Add(this.btnTimerStartStop);
            this.pnlExpenses.Controls.Add(this.lblTimerTime);
            this.pnlExpenses.Location = new System.Drawing.Point(4, 69);
            this.pnlExpenses.Name = "pnlExpenses";
            this.pnlExpenses.Size = new System.Drawing.Size(977, 490);
            this.pnlExpenses.TabIndex = 26;
            // 
            // lblExpenseHourlyWage
            // 
            this.lblExpenseHourlyWage.AutoSize = true;
            this.lblExpenseHourlyWage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExpenseHourlyWage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(224)))), ((int)(((byte)(230)))));
            this.lblExpenseHourlyWage.Location = new System.Drawing.Point(634, 99);
            this.lblExpenseHourlyWage.Name = "lblExpenseHourlyWage";
            this.lblExpenseHourlyWage.Size = new System.Drawing.Size(82, 15);
            this.lblExpenseHourlyWage.TabIndex = 57;
            this.lblExpenseHourlyWage.Text = "Hourly Wage*";
            // 
            // numHourlyWage
            // 
            this.numHourlyWage.Location = new System.Drawing.Point(724, 96);
            this.numHourlyWage.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.numHourlyWage.Name = "numHourlyWage";
            this.numHourlyWage.Size = new System.Drawing.Size(122, 20);
            this.numHourlyWage.TabIndex = 56;
            // 
            // btnTimeAddExpense
            // 
            this.btnTimeAddExpense.Location = new System.Drawing.Point(861, 101);
            this.btnTimeAddExpense.Name = "btnTimeAddExpense";
            this.btnTimeAddExpense.Size = new System.Drawing.Size(99, 42);
            this.btnTimeAddExpense.TabIndex = 55;
            this.btnTimeAddExpense.Text = "Add Time Expense";
            this.btnTimeAddExpense.UseVisualStyleBackColor = true;
            this.btnTimeAddExpense.Click += new System.EventHandler(this.btnTimeAddExpense_Click);
            // 
            // lblExpenseTimeInvoiceID
            // 
            this.lblExpenseTimeInvoiceID.AutoSize = true;
            this.lblExpenseTimeInvoiceID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExpenseTimeInvoiceID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(224)))), ((int)(((byte)(230)))));
            this.lblExpenseTimeInvoiceID.Location = new System.Drawing.Point(656, 122);
            this.lblExpenseTimeInvoiceID.Name = "lblExpenseTimeInvoiceID";
            this.lblExpenseTimeInvoiceID.Size = new System.Drawing.Size(60, 15);
            this.lblExpenseTimeInvoiceID.TabIndex = 54;
            this.lblExpenseTimeInvoiceID.Text = "Invoice ID";
            // 
            // numExpenseTimeInvoiceID
            // 
            this.numExpenseTimeInvoiceID.Location = new System.Drawing.Point(724, 122);
            this.numExpenseTimeInvoiceID.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.numExpenseTimeInvoiceID.Name = "numExpenseTimeInvoiceID";
            this.numExpenseTimeInvoiceID.Size = new System.Drawing.Size(122, 20);
            this.numExpenseTimeInvoiceID.TabIndex = 53;
            // 
            // btnExpenseAddImage
            // 
            this.btnExpenseAddImage.Location = new System.Drawing.Point(440, 157);
            this.btnExpenseAddImage.Name = "btnExpenseAddImage";
            this.btnExpenseAddImage.Size = new System.Drawing.Size(85, 24);
            this.btnExpenseAddImage.TabIndex = 52;
            this.btnExpenseAddImage.Text = "Add Image";
            this.btnExpenseAddImage.UseVisualStyleBackColor = true;
            this.btnExpenseAddImage.Click += new System.EventHandler(this.btnAddImage_Click);
            // 
            // lblTaxAmount
            // 
            this.lblTaxAmount.AutoSize = true;
            this.lblTaxAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTaxAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(224)))), ((int)(((byte)(230)))));
            this.lblTaxAmount.Location = new System.Drawing.Point(210, 187);
            this.lblTaxAmount.Name = "lblTaxAmount";
            this.lblTaxAmount.Size = new System.Drawing.Size(77, 15);
            this.lblTaxAmount.TabIndex = 51;
            this.lblTaxAmount.Text = "Tax Amount*";
            // 
            // lblTotalAmount
            // 
            this.lblTotalAmount.AutoSize = true;
            this.lblTotalAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(224)))), ((int)(((byte)(230)))));
            this.lblTotalAmount.Location = new System.Drawing.Point(203, 215);
            this.lblTotalAmount.Name = "lblTotalAmount";
            this.lblTotalAmount.Size = new System.Drawing.Size(84, 15);
            this.lblTotalAmount.TabIndex = 50;
            this.lblTotalAmount.Text = "Total Amount*";
            // 
            // numExpenseTotalAmount
            // 
            this.numExpenseTotalAmount.Location = new System.Drawing.Point(295, 215);
            this.numExpenseTotalAmount.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numExpenseTotalAmount.Name = "numExpenseTotalAmount";
            this.numExpenseTotalAmount.Size = new System.Drawing.Size(135, 20);
            this.numExpenseTotalAmount.TabIndex = 49;
            // 
            // numExpenseTaxAmount
            // 
            this.numExpenseTaxAmount.Location = new System.Drawing.Point(295, 187);
            this.numExpenseTaxAmount.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numExpenseTaxAmount.Name = "numExpenseTaxAmount";
            this.numExpenseTaxAmount.Size = new System.Drawing.Size(135, 20);
            this.numExpenseTaxAmount.TabIndex = 48;
            // 
            // lblInvoiceID
            // 
            this.lblInvoiceID.AutoSize = true;
            this.lblInvoiceID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInvoiceID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(224)))), ((int)(((byte)(230)))));
            this.lblInvoiceID.Location = new System.Drawing.Point(227, 157);
            this.lblInvoiceID.Name = "lblInvoiceID";
            this.lblInvoiceID.Size = new System.Drawing.Size(60, 15);
            this.lblInvoiceID.TabIndex = 47;
            this.lblInvoiceID.Text = "Invoice ID";
            // 
            // cmbExpenseCategory
            // 
            this.cmbExpenseCategory.AutoCompleteCustomSource.AddRange(new string[] {
            "Advertising",
            "Insurance",
            "Interest/bank charges",
            "Office expenses",
            "Office maintenance",
            "Legal fees and related expenses",
            "Accounting and other professional fees",
            "Management and admin fees",
            "Maintenance and repair",
            "Salaries",
            "Wages",
            "Benefits",
            "Property taxes",
            "Travel",
            "Utilities",
            "Cost of goods sold",
            "Motor vehicle expenses",
            "Lodging",
            "Parking fees",
            "Other misc. supplies",
            "Union professional and other similar dues"});
            this.cmbExpenseCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbExpenseCategory.FormattingEnabled = true;
            this.cmbExpenseCategory.Items.AddRange(new object[] {
            "Advertising",
            "Insurance",
            "Interest/bank charges",
            "Office expenses",
            "Office maintenance",
            "Legal fees and related expenses",
            "Accounting and other professional fees",
            "Management and admin fees",
            "Maintenance and repair",
            "Salaries",
            "Wages",
            "Benefits",
            "Property taxes",
            "Travel",
            "Utilities",
            "Cost of goods sold",
            "Motor vehicle expenses",
            "Lodging",
            "Parking fees",
            "Other misc. supplies",
            "Union professional and other similar dues"});
            this.cmbExpenseCategory.Location = new System.Drawing.Point(295, 127);
            this.cmbExpenseCategory.Name = "cmbExpenseCategory";
            this.cmbExpenseCategory.Size = new System.Drawing.Size(230, 21);
            this.cmbExpenseCategory.TabIndex = 46;
            // 
            // lblExpenseCategory
            // 
            this.lblExpenseCategory.AutoSize = true;
            this.lblExpenseCategory.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExpenseCategory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(224)))), ((int)(((byte)(230)))));
            this.lblExpenseCategory.Location = new System.Drawing.Point(176, 128);
            this.lblExpenseCategory.Name = "lblExpenseCategory";
            this.lblExpenseCategory.Size = new System.Drawing.Size(111, 15);
            this.lblExpenseCategory.TabIndex = 45;
            this.lblExpenseCategory.Text = "Expense Category*";
            // 
            // numExpenseInvoiceID
            // 
            this.numExpenseInvoiceID.Location = new System.Drawing.Point(295, 157);
            this.numExpenseInvoiceID.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.numExpenseInvoiceID.Name = "numExpenseInvoiceID";
            this.numExpenseInvoiceID.Size = new System.Drawing.Size(135, 20);
            this.numExpenseInvoiceID.TabIndex = 44;
            // 
            // lblExpenseDescription
            // 
            this.lblExpenseDescription.AutoSize = true;
            this.lblExpenseDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExpenseDescription.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(224)))), ((int)(((byte)(230)))));
            this.lblExpenseDescription.Location = new System.Drawing.Point(213, 101);
            this.lblExpenseDescription.Name = "lblExpenseDescription";
            this.lblExpenseDescription.Size = new System.Drawing.Size(74, 15);
            this.lblExpenseDescription.TabIndex = 43;
            this.lblExpenseDescription.Text = "Description*";
            // 
            // lblAddAnExpense
            // 
            this.lblAddAnExpense.AutoSize = true;
            this.lblAddAnExpense.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAddAnExpense.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(224)))), ((int)(((byte)(230)))));
            this.lblAddAnExpense.Location = new System.Drawing.Point(346, 43);
            this.lblAddAnExpense.Name = "lblAddAnExpense";
            this.lblAddAnExpense.Size = new System.Drawing.Size(111, 17);
            this.lblAddAnExpense.TabIndex = 42;
            this.lblAddAnExpense.Text = "Add an Expense";
            // 
            // lblExpensesTitle
            // 
            this.lblExpensesTitle.AutoSize = true;
            this.lblExpensesTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExpensesTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(224)))), ((int)(((byte)(230)))));
            this.lblExpensesTitle.Location = new System.Drawing.Point(409, 4);
            this.lblExpensesTitle.Name = "lblExpensesTitle";
            this.lblExpensesTitle.Size = new System.Drawing.Size(148, 17);
            this.lblExpensesTitle.TabIndex = 41;
            this.lblExpensesTitle.Text = "Expense Management";
            // 
            // lblExpenseDate
            // 
            this.lblExpenseDate.AutoSize = true;
            this.lblExpenseDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExpenseDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(224)))), ((int)(((byte)(230)))));
            this.lblExpenseDate.Location = new System.Drawing.Point(249, 72);
            this.lblExpenseDate.Name = "lblExpenseDate";
            this.lblExpenseDate.Size = new System.Drawing.Size(38, 15);
            this.lblExpenseDate.TabIndex = 40;
            this.lblExpenseDate.Text = "Date*";
            // 
            // btnAddExpense
            // 
            this.btnAddExpense.Location = new System.Drawing.Point(440, 187);
            this.btnAddExpense.Name = "btnAddExpense";
            this.btnAddExpense.Size = new System.Drawing.Size(85, 48);
            this.btnAddExpense.TabIndex = 39;
            this.btnAddExpense.Text = "Add Expense";
            this.btnAddExpense.UseVisualStyleBackColor = true;
            this.btnAddExpense.Click += new System.EventHandler(this.btnAddExpense_Click);
            // 
            // txtExpenseDescription
            // 
            this.txtExpenseDescription.Location = new System.Drawing.Point(295, 99);
            this.txtExpenseDescription.Name = "txtExpenseDescription";
            this.txtExpenseDescription.Size = new System.Drawing.Size(230, 20);
            this.txtExpenseDescription.TabIndex = 38;
            // 
            // dtpExpenses
            // 
            this.dtpExpenses.Location = new System.Drawing.Point(295, 70);
            this.dtpExpenses.Name = "dtpExpenses";
            this.dtpExpenses.Size = new System.Drawing.Size(230, 20);
            this.dtpExpenses.TabIndex = 37;
            // 
            // numDistanceInvoiceID
            // 
            this.numDistanceInvoiceID.Location = new System.Drawing.Point(724, 240);
            this.numDistanceInvoiceID.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.numDistanceInvoiceID.Name = "numDistanceInvoiceID";
            this.numDistanceInvoiceID.Size = new System.Drawing.Size(120, 20);
            this.numDistanceInvoiceID.TabIndex = 36;
            // 
            // txtDistanceDescription
            // 
            this.txtDistanceDescription.Location = new System.Drawing.Point(724, 213);
            this.txtDistanceDescription.Name = "txtDistanceDescription";
            this.txtDistanceDescription.Size = new System.Drawing.Size(120, 20);
            this.txtDistanceDescription.TabIndex = 35;
            // 
            // lblDistanceInvoiceID
            // 
            this.lblDistanceInvoiceID.AutoSize = true;
            this.lblDistanceInvoiceID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDistanceInvoiceID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(224)))), ((int)(((byte)(230)))));
            this.lblDistanceInvoiceID.Location = new System.Drawing.Point(656, 243);
            this.lblDistanceInvoiceID.Name = "lblDistanceInvoiceID";
            this.lblDistanceInvoiceID.Size = new System.Drawing.Size(60, 15);
            this.lblDistanceInvoiceID.TabIndex = 34;
            this.lblDistanceInvoiceID.Text = "Invoice ID";
            // 
            // dgvExpenses
            // 
            this.dgvExpenses.AllowUserToAddRows = false;
            this.dgvExpenses.AllowUserToDeleteRows = false;
            this.dgvExpenses.AutoGenerateColumns = false;
            this.dgvExpenses.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(35)))), ((int)(((byte)(38)))));
            this.dgvExpenses.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvExpenses.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvExpenses.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.expenseIDDataGridViewTextBoxColumn,
            this.dateDataGridViewTextBoxColumn1,
            this.descriptionDataGridViewTextBoxColumn,
            this.imageReferenceDataGridViewTextBoxColumn,
            this.invoicesInvoiceIDDataGridViewTextBoxColumn,
            this.expenseCategoryDataGridViewTextBoxColumn,
            this.taxAmountDataGridViewTextBoxColumn,
            this.totalAmountDataGridViewTextBoxColumn});
            this.dgvExpenses.DataSource = this.expensesBindingSource;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvExpenses.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvExpenses.GridColor = System.Drawing.SystemColors.AppWorkspace;
            this.dgvExpenses.Location = new System.Drawing.Point(10, 277);
            this.dgvExpenses.Name = "dgvExpenses";
            this.dgvExpenses.Size = new System.Drawing.Size(950, 203);
            this.dgvExpenses.TabIndex = 33;
            this.dgvExpenses.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvExpenses_CellClick);
            this.dgvExpenses.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvExpenses_CellEnter);
            this.dgvExpenses.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvExpenses_DataError);
            this.dgvExpenses.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvExpenses_EditingControlShowing);
            this.dgvExpenses.SelectionChanged += new System.EventHandler(this.dgvExpenses_SelectionChanged);
            this.dgvExpenses.Sorted += new System.EventHandler(this.dgvExpenses_Sorted);
            // 
            // expenseIDDataGridViewTextBoxColumn
            // 
            this.expenseIDDataGridViewTextBoxColumn.DataPropertyName = "ExpenseID";
            this.expenseIDDataGridViewTextBoxColumn.HeaderText = "Expense ID";
            this.expenseIDDataGridViewTextBoxColumn.Name = "expenseIDDataGridViewTextBoxColumn";
            this.expenseIDDataGridViewTextBoxColumn.ReadOnly = true;
            this.expenseIDDataGridViewTextBoxColumn.Visible = false;
            // 
            // dateDataGridViewTextBoxColumn1
            // 
            this.dateDataGridViewTextBoxColumn1.DataPropertyName = "Date";
            this.dateDataGridViewTextBoxColumn1.HeaderText = "Date";
            this.dateDataGridViewTextBoxColumn1.Name = "dateDataGridViewTextBoxColumn1";
            // 
            // descriptionDataGridViewTextBoxColumn
            // 
            this.descriptionDataGridViewTextBoxColumn.DataPropertyName = "Description";
            this.descriptionDataGridViewTextBoxColumn.HeaderText = "Description";
            this.descriptionDataGridViewTextBoxColumn.Name = "descriptionDataGridViewTextBoxColumn";
            this.descriptionDataGridViewTextBoxColumn.Width = 150;
            // 
            // imageReferenceDataGridViewTextBoxColumn
            // 
            this.imageReferenceDataGridViewTextBoxColumn.DataPropertyName = "ImageReference";
            this.imageReferenceDataGridViewTextBoxColumn.HeaderText = "ImageReference";
            this.imageReferenceDataGridViewTextBoxColumn.Name = "imageReferenceDataGridViewTextBoxColumn";
            this.imageReferenceDataGridViewTextBoxColumn.Visible = false;
            // 
            // invoicesInvoiceIDDataGridViewTextBoxColumn
            // 
            this.invoicesInvoiceIDDataGridViewTextBoxColumn.DataPropertyName = "Invoices_InvoiceID";
            this.invoicesInvoiceIDDataGridViewTextBoxColumn.HeaderText = "Invoice ID";
            this.invoicesInvoiceIDDataGridViewTextBoxColumn.Name = "invoicesInvoiceIDDataGridViewTextBoxColumn";
            this.invoicesInvoiceIDDataGridViewTextBoxColumn.Width = 85;
            // 
            // expenseCategoryDataGridViewTextBoxColumn
            // 
            this.expenseCategoryDataGridViewTextBoxColumn.DataPropertyName = "ExpenseCategory";
            this.expenseCategoryDataGridViewTextBoxColumn.HeaderText = "Expense Category";
            this.expenseCategoryDataGridViewTextBoxColumn.Name = "expenseCategoryDataGridViewTextBoxColumn";
            this.expenseCategoryDataGridViewTextBoxColumn.Width = 120;
            // 
            // taxAmountDataGridViewTextBoxColumn
            // 
            this.taxAmountDataGridViewTextBoxColumn.DataPropertyName = "TaxAmount";
            this.taxAmountDataGridViewTextBoxColumn.HeaderText = "Tax Amount";
            this.taxAmountDataGridViewTextBoxColumn.Name = "taxAmountDataGridViewTextBoxColumn";
            // 
            // totalAmountDataGridViewTextBoxColumn
            // 
            this.totalAmountDataGridViewTextBoxColumn.DataPropertyName = "TotalAmount";
            this.totalAmountDataGridViewTextBoxColumn.HeaderText = "Total Amount";
            this.totalAmountDataGridViewTextBoxColumn.Name = "totalAmountDataGridViewTextBoxColumn";
            // 
            // expensesBindingSource
            // 
            this.expensesBindingSource.DataMember = "expenses";
            this.expensesBindingSource.DataSource = this.expensesDataSet;
            // 
            // expensesDataSet
            // 
            this.expensesDataSet.DataSetName = "expensesDataSet";
            this.expensesDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // lblDistanceDescription
            // 
            this.lblDistanceDescription.AutoSize = true;
            this.lblDistanceDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDistanceDescription.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(224)))), ((int)(((byte)(230)))));
            this.lblDistanceDescription.Location = new System.Drawing.Point(582, 214);
            this.lblDistanceDescription.Name = "lblDistanceDescription";
            this.lblDistanceDescription.Size = new System.Drawing.Size(134, 15);
            this.lblDistanceDescription.TabIndex = 32;
            this.lblDistanceDescription.Text = "Description/Destination";
            // 
            // lblDistanceTravelledLabel
            // 
            this.lblDistanceTravelledLabel.AutoSize = true;
            this.lblDistanceTravelledLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDistanceTravelledLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(224)))), ((int)(((byte)(230)))));
            this.lblDistanceTravelledLabel.Location = new System.Drawing.Point(603, 188);
            this.lblDistanceTravelledLabel.Name = "lblDistanceTravelledLabel";
            this.lblDistanceTravelledLabel.Size = new System.Drawing.Size(113, 15);
            this.lblDistanceTravelledLabel.TabIndex = 31;
            this.lblDistanceTravelledLabel.Text = "Distance Travelled*";
            // 
            // numDistance
            // 
            this.numDistance.Location = new System.Drawing.Point(724, 186);
            this.numDistance.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.numDistance.Name = "numDistance";
            this.numDistance.Size = new System.Drawing.Size(120, 20);
            this.numDistance.TabIndex = 30;
            // 
            // lblMileageTitle
            // 
            this.lblMileageTitle.AutoSize = true;
            this.lblMileageTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMileageTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(224)))), ((int)(((byte)(230)))));
            this.lblMileageTitle.Location = new System.Drawing.Point(657, 164);
            this.lblMileageTitle.Name = "lblMileageTitle";
            this.lblMileageTitle.Size = new System.Drawing.Size(167, 17);
            this.lblMileageTitle.TabIndex = 29;
            this.lblMileageTitle.Text = "Add a Travelled Distance";
            // 
            // btnSubmitDistance
            // 
            this.btnSubmitDistance.Location = new System.Drawing.Point(861, 215);
            this.btnSubmitDistance.Name = "btnSubmitDistance";
            this.btnSubmitDistance.Size = new System.Drawing.Size(99, 45);
            this.btnSubmitDistance.TabIndex = 28;
            this.btnSubmitDistance.Text = "Add Distance Expense";
            this.btnSubmitDistance.UseVisualStyleBackColor = true;
            this.btnSubmitDistance.Click += new System.EventHandler(this.btnSubmitDistance_Click);
            // 
            // lblTimerTitle
            // 
            this.lblTimerTitle.AutoSize = true;
            this.lblTimerTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTimerTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(224)))), ((int)(((byte)(230)))));
            this.lblTimerTitle.Location = new System.Drawing.Point(743, 43);
            this.lblTimerTitle.Name = "lblTimerTitle";
            this.lblTimerTitle.Size = new System.Drawing.Size(151, 17);
            this.lblTimerTitle.TabIndex = 27;
            this.lblTimerTitle.Text = "Time the Work You Do";
            // 
            // btnTimerReset
            // 
            this.btnTimerReset.Location = new System.Drawing.Point(881, 67);
            this.btnTimerReset.Name = "btnTimerReset";
            this.btnTimerReset.Size = new System.Drawing.Size(75, 23);
            this.btnTimerReset.TabIndex = 3;
            this.btnTimerReset.Text = "Reset Timer";
            this.btnTimerReset.UseVisualStyleBackColor = true;
            this.btnTimerReset.Click += new System.EventHandler(this.btnTimerReset_Click);
            // 
            // btnTimerStartStop
            // 
            this.btnTimerStartStop.Location = new System.Drawing.Point(674, 67);
            this.btnTimerStartStop.Name = "btnTimerStartStop";
            this.btnTimerStartStop.Size = new System.Drawing.Size(75, 23);
            this.btnTimerStartStop.TabIndex = 2;
            this.btnTimerStartStop.Text = "Start Timer";
            this.btnTimerStartStop.UseVisualStyleBackColor = true;
            this.btnTimerStartStop.Click += new System.EventHandler(this.btnTimerStartStop_Click);
            // 
            // lblTimerTime
            // 
            this.lblTimerTime.AutoSize = true;
            this.lblTimerTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTimerTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(224)))), ((int)(((byte)(230)))));
            this.lblTimerTime.Location = new System.Drawing.Point(776, 70);
            this.lblTimerTime.Name = "lblTimerTime";
            this.lblTimerTime.Size = new System.Drawing.Size(83, 15);
            this.lblTimerTime.TabIndex = 1;
            this.lblTimerTime.Text = "Time Elapsed";
            // 
            // tmrWorkTime
            // 
            this.tmrWorkTime.Interval = 1000;
            this.tmrWorkTime.Tick += new System.EventHandler(this.tmrWorkTime_Tick);
            // 
            // expensesTableAdapter
            // 
            this.expensesTableAdapter.ClearBeforeFill = true;
            // 
            // pnlBusinessStats
            // 
            this.pnlBusinessStats.Controls.Add(this.lblClientStatsSubHeading);
            this.pnlBusinessStats.Controls.Add(this.lblBusinessStatsSubHeading);
            this.pnlBusinessStats.Controls.Add(this.chrtPDFExport);
            this.pnlBusinessStats.Controls.Add(this.btnGenerateProfitLossStatement);
            this.pnlBusinessStats.Controls.Add(this.btnUpdateStats);
            this.pnlBusinessStats.Controls.Add(this.dgvClientStats);
            this.pnlBusinessStats.Controls.Add(this.dtpStatsEnd);
            this.pnlBusinessStats.Controls.Add(this.dtpStatsStart);
            this.pnlBusinessStats.Controls.Add(this.lblEndDate);
            this.pnlBusinessStats.Controls.Add(this.lblStartDate);
            this.pnlBusinessStats.Controls.Add(this.dgvBusinessStats);
            this.pnlBusinessStats.Controls.Add(this.lblBusinessStatsTitle);
            this.pnlBusinessStats.Location = new System.Drawing.Point(4, 68);
            this.pnlBusinessStats.Name = "pnlBusinessStats";
            this.pnlBusinessStats.Size = new System.Drawing.Size(977, 491);
            this.pnlBusinessStats.TabIndex = 27;
            this.pnlBusinessStats.Visible = false;
            // 
            // lblClientStatsSubHeading
            // 
            this.lblClientStatsSubHeading.AutoSize = true;
            this.lblClientStatsSubHeading.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClientStatsSubHeading.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(224)))), ((int)(((byte)(230)))));
            this.lblClientStatsSubHeading.Location = new System.Drawing.Point(699, 153);
            this.lblClientStatsSubHeading.Name = "lblClientStatsSubHeading";
            this.lblClientStatsSubHeading.Size = new System.Drawing.Size(103, 17);
            this.lblClientStatsSubHeading.TabIndex = 34;
            this.lblClientStatsSubHeading.Text = "Client Statistics";
            // 
            // lblBusinessStatsSubHeading
            // 
            this.lblBusinessStatsSubHeading.AutoSize = true;
            this.lblBusinessStatsSubHeading.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBusinessStatsSubHeading.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(224)))), ((int)(((byte)(230)))));
            this.lblBusinessStatsSubHeading.Location = new System.Drawing.Point(292, 153);
            this.lblBusinessStatsSubHeading.Name = "lblBusinessStatsSubHeading";
            this.lblBusinessStatsSubHeading.Size = new System.Drawing.Size(125, 17);
            this.lblBusinessStatsSubHeading.TabIndex = 33;
            this.lblBusinessStatsSubHeading.Text = "Business Statistics";
            // 
            // chrtPDFExport
            // 
            chartArea1.Name = "ChartArea1";
            this.chrtPDFExport.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chrtPDFExport.Legends.Add(legend1);
            this.chrtPDFExport.Location = new System.Drawing.Point(295, 446);
            this.chrtPDFExport.Name = "chrtPDFExport";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chrtPDFExport.Series.Add(series1);
            this.chrtPDFExport.Size = new System.Drawing.Size(483, 319);
            this.chrtPDFExport.TabIndex = 32;
            this.chrtPDFExport.Text = "chart1";
            this.chrtPDFExport.Visible = false;
            // 
            // btnGenerateProfitLossStatement
            // 
            this.btnGenerateProfitLossStatement.Location = new System.Drawing.Point(691, 85);
            this.btnGenerateProfitLossStatement.Name = "btnGenerateProfitLossStatement";
            this.btnGenerateProfitLossStatement.Size = new System.Drawing.Size(180, 23);
            this.btnGenerateProfitLossStatement.TabIndex = 31;
            this.btnGenerateProfitLossStatement.Text = "Generate Profit/Loss Statement";
            this.btnGenerateProfitLossStatement.UseVisualStyleBackColor = true;
            this.btnGenerateProfitLossStatement.Click += new System.EventHandler(this.btnGenerateProfitLossStatement_Click);
            // 
            // btnUpdateStats
            // 
            this.btnUpdateStats.Location = new System.Drawing.Point(568, 85);
            this.btnUpdateStats.Name = "btnUpdateStats";
            this.btnUpdateStats.Size = new System.Drawing.Size(75, 23);
            this.btnUpdateStats.TabIndex = 30;
            this.btnUpdateStats.Text = "Update Stats";
            this.btnUpdateStats.UseVisualStyleBackColor = true;
            this.btnUpdateStats.Click += new System.EventHandler(this.btnUpdateStats_Click);
            // 
            // dgvClientStats
            // 
            this.dgvClientStats.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(35)))), ((int)(((byte)(38)))));
            this.dgvClientStats.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvClientStats.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvClientStats.Location = new System.Drawing.Point(563, 181);
            this.dgvClientStats.Name = "dgvClientStats";
            this.dgvClientStats.Size = new System.Drawing.Size(365, 285);
            this.dgvClientStats.TabIndex = 29;
            // 
            // dtpStatsEnd
            // 
            this.dtpStatsEnd.Location = new System.Drawing.Point(313, 109);
            this.dtpStatsEnd.Name = "dtpStatsEnd";
            this.dtpStatsEnd.Size = new System.Drawing.Size(203, 20);
            this.dtpStatsEnd.TabIndex = 28;
            // 
            // dtpStatsStart
            // 
            this.dtpStatsStart.Location = new System.Drawing.Point(311, 64);
            this.dtpStatsStart.Name = "dtpStatsStart";
            this.dtpStatsStart.Size = new System.Drawing.Size(203, 20);
            this.dtpStatsStart.TabIndex = 27;
            // 
            // lblEndDate
            // 
            this.lblEndDate.AutoSize = true;
            this.lblEndDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEndDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(224)))), ((int)(((byte)(230)))));
            this.lblEndDate.Location = new System.Drawing.Point(229, 113);
            this.lblEndDate.Name = "lblEndDate";
            this.lblEndDate.Size = new System.Drawing.Size(71, 17);
            this.lblEndDate.TabIndex = 26;
            this.lblEndDate.Text = "End Date:";
            // 
            // lblStartDate
            // 
            this.lblStartDate.AutoSize = true;
            this.lblStartDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStartDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(224)))), ((int)(((byte)(230)))));
            this.lblStartDate.Location = new System.Drawing.Point(229, 68);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(76, 17);
            this.lblStartDate.TabIndex = 25;
            this.lblStartDate.Text = "Start Date:";
            // 
            // dgvBusinessStats
            // 
            this.dgvBusinessStats.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(35)))), ((int)(((byte)(38)))));
            this.dgvBusinessStats.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvBusinessStats.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBusinessStats.Location = new System.Drawing.Point(171, 182);
            this.dgvBusinessStats.Name = "dgvBusinessStats";
            this.dgvBusinessStats.Size = new System.Drawing.Size(365, 285);
            this.dgvBusinessStats.TabIndex = 24;
            // 
            // lblBusinessStatsTitle
            // 
            this.lblBusinessStatsTitle.AutoSize = true;
            this.lblBusinessStatsTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBusinessStatsTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(224)))), ((int)(((byte)(230)))));
            this.lblBusinessStatsTitle.Location = new System.Drawing.Point(428, 20);
            this.lblBusinessStatsTitle.Name = "lblBusinessStatsTitle";
            this.lblBusinessStatsTitle.Size = new System.Drawing.Size(125, 17);
            this.lblBusinessStatsTitle.TabIndex = 23;
            this.lblBusinessStatsTitle.Text = "Business Statistics";
            // 
            // pnlSettings
            // 
            this.pnlSettings.Controls.Add(this.btnSaveSettings);
            this.pnlSettings.Controls.Add(this.dgvSettings);
            this.pnlSettings.Controls.Add(this.lblSettingsTitle);
            this.pnlSettings.Location = new System.Drawing.Point(4, 69);
            this.pnlSettings.Name = "pnlSettings";
            this.pnlSettings.Size = new System.Drawing.Size(977, 491);
            this.pnlSettings.TabIndex = 28;
            this.pnlSettings.Visible = false;
            // 
            // btnSaveSettings
            // 
            this.btnSaveSettings.Location = new System.Drawing.Point(781, 405);
            this.btnSaveSettings.Name = "btnSaveSettings";
            this.btnSaveSettings.Size = new System.Drawing.Size(167, 23);
            this.btnSaveSettings.TabIndex = 25;
            this.btnSaveSettings.Text = "Save Settings";
            this.btnSaveSettings.UseVisualStyleBackColor = true;
            this.btnSaveSettings.Click += new System.EventHandler(this.btnSaveSettings_Click);
            // 
            // dgvSettings
            // 
            this.dgvSettings.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(35)))), ((int)(((byte)(38)))));
            this.dgvSettings.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvSettings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSettings.Location = new System.Drawing.Point(171, 53);
            this.dgvSettings.Name = "dgvSettings";
            this.dgvSettings.Size = new System.Drawing.Size(785, 319);
            this.dgvSettings.TabIndex = 24;
            this.dgvSettings.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvSettings_DataError);
            this.dgvSettings.SelectionChanged += new System.EventHandler(this.dgvSettings_SelectionChanged);
            // 
            // lblSettingsTitle
            // 
            this.lblSettingsTitle.AutoSize = true;
            this.lblSettingsTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSettingsTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(224)))), ((int)(((byte)(230)))));
            this.lblSettingsTitle.Location = new System.Drawing.Point(457, 21);
            this.lblSettingsTitle.Name = "lblSettingsTitle";
            this.lblSettingsTitle.Size = new System.Drawing.Size(59, 17);
            this.lblSettingsTitle.TabIndex = 23;
            this.lblSettingsTitle.Text = "Settings";
            // 
            // pnlHome
            // 
            this.pnlHome.Controls.Add(this.pictureBox1);
            this.pnlHome.Controls.Add(this.lblClicktoNavigate);
            this.pnlHome.Controls.Add(this.lblHomeTitle);
            this.pnlHome.Location = new System.Drawing.Point(4, 69);
            this.pnlHome.Name = "pnlHome";
            this.pnlHome.Size = new System.Drawing.Size(977, 491);
            this.pnlHome.TabIndex = 29;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.InitialImage")));
            this.pictureBox1.Location = new System.Drawing.Point(353, 119);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(316, 109);
            this.pictureBox1.TabIndex = 25;
            this.pictureBox1.TabStop = false;
            // 
            // lblClicktoNavigate
            // 
            this.lblClicktoNavigate.AutoSize = true;
            this.lblClicktoNavigate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClicktoNavigate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(224)))), ((int)(((byte)(230)))));
            this.lblClicktoNavigate.Location = new System.Drawing.Point(350, 242);
            this.lblClicktoNavigate.Name = "lblClicktoNavigate";
            this.lblClicktoNavigate.Size = new System.Drawing.Size(264, 17);
            this.lblClicktoNavigate.TabIndex = 24;
            this.lblClicktoNavigate.Text = "Click a Section to the Left to Navigate to:";
            // 
            // lblHomeTitle
            // 
            this.lblHomeTitle.AutoSize = true;
            this.lblHomeTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHomeTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(224)))), ((int)(((byte)(230)))));
            this.lblHomeTitle.Location = new System.Drawing.Point(346, 93);
            this.lblHomeTitle.Name = "lblHomeTitle";
            this.lblHomeTitle.Size = new System.Drawing.Size(86, 17);
            this.lblHomeTitle.TabIndex = 23;
            this.lblHomeTitle.Text = "Welcome to:";
            // 
            // button1
            // 
            this.button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(5, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(22, 22);
            this.button1.TabIndex = 21;
            this.button1.UseVisualStyleBackColor = true;
            // 
            // btnMinimize
            // 
            this.btnMinimize.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnMinimize.BackgroundImage")));
            this.btnMinimize.FlatAppearance.BorderSize = 0;
            this.btnMinimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinimize.Location = new System.Drawing.Point(905, 3);
            this.btnMinimize.Name = "btnMinimize";
            this.btnMinimize.Size = new System.Drawing.Size(27, 14);
            this.btnMinimize.TabIndex = 20;
            this.btnMinimize.UseVisualStyleBackColor = true;
            this.btnMinimize.Click += new System.EventHandler(this.btnMinimize_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClose.BackgroundImage")));
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Location = new System.Drawing.Point(938, -1);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(43, 18);
            this.btnClose.TabIndex = 19;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(35)))), ((int)(((byte)(38)))));
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.pnlMenu);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnMinimize);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.pnlInvoices);
            this.Controls.Add(this.pnlClients);
            this.Controls.Add(this.pnlHome);
            this.Controls.Add(this.pnlSettings);
            this.Controls.Add(this.pnlBusinessStats);
            this.Controls.Add(this.pnlExpenses);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmMain";
            this.Text = "Moneta FMS";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.frmMain_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmMain_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.frmMain_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.dgvClients)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.clientsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mydbDataSet)).EndInit();
            this.pnlClients.ResumeLayout(false);
            this.pnlClients.PerformLayout();
            this.pnlMenu.ResumeLayout(false);
            this.pnlMenu.PerformLayout();
            this.pnlInvoices.ResumeLayout(false);
            this.pnlInvoices.PerformLayout();
            this.pnlCreateEditInvoice.ResumeLayout(false);
            this.pnlCreateEditInvoice.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numClientID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInvoices)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.invoicesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.invoicesDataSet)).EndInit();
            this.pnlExpenses.ResumeLayout(false);
            this.pnlExpenses.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numHourlyWage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numExpenseTimeInvoiceID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numExpenseTotalAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numExpenseTaxAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numExpenseInvoiceID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDistanceInvoiceID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExpenses)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.expensesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.expensesDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDistance)).EndInit();
            this.pnlBusinessStats.ResumeLayout(false);
            this.pnlBusinessStats.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chrtPDFExport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClientStats)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBusinessStats)).EndInit();
            this.pnlSettings.ResumeLayout(false);
            this.pnlSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSettings)).EndInit();
            this.pnlHome.ResumeLayout(false);
            this.pnlHome.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ToolStripMenuItem formsToolStripMenuItem;
    public System.Windows.Forms.ToolStripMenuItem frmclientsToolStripMenuItem;
    public System.Windows.Forms.ToolStripMenuItem frminvoicesToolStripMenuItem;
    public System.Windows.Forms.TextBox txtCFirstName;
    public System.Windows.Forms.TextBox txtCLastName;
    public System.Windows.Forms.TextBox txtCCompany;
    public System.Windows.Forms.TextBox txtCNumber;
    public System.Windows.Forms.Button btnCreateClient;
    public System.Windows.Forms.TextBox txtCEmail;
    public System.Windows.Forms.TextBox txtCAddress;
    public System.Windows.Forms.TextBox txtCNotes;
    public System.Windows.Forms.Label lblCFirstName;
    public System.Windows.Forms.Label lblCLastName;
    public System.Windows.Forms.Label lblCCompany;
    public System.Windows.Forms.Label lblCPhoneNumber;
    public System.Windows.Forms.Label lblCEmail;
    public System.Windows.Forms.Label lblCAddress;
    public System.Windows.Forms.Label lblCNotes;
    public System.Windows.Forms.DataGridView dgvClients;
    public mydbDataSet mydbDataSet;
    public System.Windows.Forms.BindingSource clientsBindingSource;
    public mydbDataSetTableAdapters.clientsTableAdapter clientsTableAdapter;
    public System.Windows.Forms.DataGridViewTextBoxColumn clientIDDataGridViewTextBoxColumn;
    public System.Windows.Forms.DataGridViewTextBoxColumn firstNameDataGridViewTextBoxColumn;
    public System.Windows.Forms.DataGridViewTextBoxColumn lastNameDataGridViewTextBoxColumn;
    public System.Windows.Forms.DataGridViewTextBoxColumn Column1;
    public System.Windows.Forms.DataGridViewTextBoxColumn FirstName;
    public System.Windows.Forms.DataGridViewTextBoxColumn LastName;
    public System.Windows.Forms.DataGridViewTextBoxColumn companyDataGridViewTextBoxColumn;
    public System.Windows.Forms.DataGridViewTextBoxColumn phoneDataGridViewTextBoxColumn;
    public System.Windows.Forms.DataGridViewTextBoxColumn emailDataGridViewTextBoxColumn;
    public System.Windows.Forms.DataGridViewTextBoxColumn addressDataGridViewTextBoxColumn;
    public System.Windows.Forms.DataGridViewTextBoxColumn noteDataGridViewTextBoxColumn;
    public System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
    public System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
    public System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
    public System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
    public System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
    public System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
    public System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
    public System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
    public System.Windows.Forms.Panel pnlClients;
    public System.Windows.Forms.Button btnClose;
    public System.Windows.Forms.Button btnMinimize;
    public System.Windows.Forms.Button button1;
    public System.Windows.Forms.Label lblTitle;
    public System.Windows.Forms.Panel pnlMenu;
    public System.Windows.Forms.Label lblNavHome;
    public System.Windows.Forms.Label lblNavBusinessStats;
    public System.Windows.Forms.Label lblNavExpenses;
    public System.Windows.Forms.Label lblNavInvoices;
    public System.Windows.Forms.Label lblNavClients;
    public System.Windows.Forms.Label lblNavSettings;
    public System.Windows.Forms.Label lblViewSelect;
    public System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
    public System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
    public System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
    public System.Windows.Forms.Panel pnlInvoices;
    public System.Windows.Forms.Label lblCreateEditInvoiceItems;
    public System.Windows.Forms.DataGridView dgvItems;
    public System.Windows.Forms.DataGridView dgvInvoices;
    public invoicesDataSet invoicesDataSet;
    public System.Windows.Forms.BindingSource invoicesBindingSource;
    public invoicesDataSetTableAdapters.invoicesTableAdapter invoicesTableAdapter;
    public System.Windows.Forms.Label lblInvoiceIDNum;
    public System.Windows.Forms.Label lblInvoiceIDTitle;
    public System.Windows.Forms.CheckBox cbxQuote;
    public System.Windows.Forms.Label lblnvoiceSearch;
    public System.Windows.Forms.CheckBox cbxPaid;
    public System.Windows.Forms.Label lblClientID;
    public System.Windows.Forms.DateTimePicker dtpDate;
    public System.Windows.Forms.DataGridViewTextBoxColumn invoiceIDDataGridViewTextBoxColumn;
    public System.Windows.Forms.DataGridViewTextBoxColumn dateDataGridViewTextBoxColumn;
    public System.Windows.Forms.DataGridViewTextBoxColumn clientsClientIDDataGridViewTextBoxColumn;
    public System.Windows.Forms.DataGridViewTextBoxColumn typeDataGridViewTextBoxColumn;
    public System.Windows.Forms.DataGridViewTextBoxColumn paidDataGridViewTextBoxColumn;
    public System.Windows.Forms.NumericUpDown numClientID;
    public System.Windows.Forms.Panel pnlExpenses;
    public System.Windows.Forms.Timer tmrWorkTime;
    public System.Windows.Forms.Button btnTimerStartStop;
    public System.Windows.Forms.Label lblTimerTime;
    public System.Windows.Forms.Button btnTimerReset;
    public System.Windows.Forms.Label lblTimerTitle;
    public System.Windows.Forms.Label lblDistanceDescription;
    public System.Windows.Forms.Label lblDistanceTravelledLabel;
    public System.Windows.Forms.NumericUpDown numDistance;
    public System.Windows.Forms.Label lblMileageTitle;
    public System.Windows.Forms.Button btnSubmitDistance;
    public System.Windows.Forms.DataGridView dgvExpenses;
    public expensesDataSet expensesDataSet;
    public System.Windows.Forms.BindingSource expensesBindingSource;
    public expensesDataSetTableAdapters.expensesTableAdapter expensesTableAdapter;
    public System.Windows.Forms.OpenFileDialog ofdExpenses;
    public System.Windows.Forms.Button btnPrintInvoice;
    public System.Windows.Forms.Button btnEmailInvoice;
    public System.Windows.Forms.NumericUpDown numDistanceInvoiceID;
    public System.Windows.Forms.TextBox txtDistanceDescription;
    public System.Windows.Forms.Label lblDistanceInvoiceID;
    public System.Windows.Forms.Panel pnlBusinessStats;
    public System.Windows.Forms.DataGridView dgvBusinessStats;
    public System.Windows.Forms.Label lblBusinessStatsTitle;
    public System.Windows.Forms.DateTimePicker dtpStatsEnd;
    public System.Windows.Forms.DateTimePicker dtpStatsStart;
    public System.Windows.Forms.Label lblEndDate;
    public System.Windows.Forms.Label lblStartDate;
    public System.Windows.Forms.Panel pnlSettings;
    public System.Windows.Forms.DataGridView dgvSettings;
    public System.Windows.Forms.Label lblSettingsTitle;
    public System.Windows.Forms.Button btnSaveSettings;
    public System.Windows.Forms.Panel pnlHome;
    public System.Windows.Forms.Label lblHomeTitle;
    public System.Windows.Forms.Label lblClicktoNavigate;
    public System.Windows.Forms.DataGridView dgvClientStats;
    public System.Windows.Forms.Button btnUpdateStats;
    public System.Windows.Forms.Button btnGenerateProfitLossStatement;
    public System.Windows.Forms.Label lblClientTitle;
    public System.Windows.Forms.DataGridViewTextBoxColumn expenseIDDataGridViewTextBoxColumn;
    public System.Windows.Forms.DataGridViewTextBoxColumn dateDataGridViewTextBoxColumn1;
    public System.Windows.Forms.DataGridViewTextBoxColumn descriptionDataGridViewTextBoxColumn;
    public System.Windows.Forms.DataGridViewTextBoxColumn imageReferenceDataGridViewTextBoxColumn;
    public System.Windows.Forms.DataGridViewTextBoxColumn invoicesInvoiceIDDataGridViewTextBoxColumn;
    public System.Windows.Forms.DataGridViewTextBoxColumn expenseCategoryDataGridViewTextBoxColumn;
    public System.Windows.Forms.DataGridViewTextBoxColumn taxAmountDataGridViewTextBoxColumn;
    public System.Windows.Forms.DataGridViewTextBoxColumn totalAmountDataGridViewTextBoxColumn;
    public System.Windows.Forms.DataVisualization.Charting.Chart chrtPDFExport;
    public System.Windows.Forms.Label lblClientStatsSubHeading;
    public System.Windows.Forms.Label lblBusinessStatsSubHeading;
    public System.Windows.Forms.Button btnAddExpense;
    public System.Windows.Forms.TextBox txtExpenseDescription;
    public System.Windows.Forms.DateTimePicker dtpExpenses;
    public System.Windows.Forms.NumericUpDown numExpenseTotalAmount;
    public System.Windows.Forms.NumericUpDown numExpenseTaxAmount;
    public System.Windows.Forms.Label lblInvoiceID;
    public System.Windows.Forms.ComboBox cmbExpenseCategory;
    public System.Windows.Forms.Label lblExpenseCategory;
    public System.Windows.Forms.NumericUpDown numExpenseInvoiceID;
    public System.Windows.Forms.Label lblExpenseDescription;
    public System.Windows.Forms.Label lblAddAnExpense;
    public System.Windows.Forms.Label lblExpensesTitle;
    public System.Windows.Forms.Label lblExpenseDate;
    public System.Windows.Forms.Label lblTaxAmount;
    public System.Windows.Forms.Label lblTotalAmount;
    public System.Windows.Forms.Button btnExpenseAddImage;
    private System.Windows.Forms.PictureBox pictureBox1;
    public System.Windows.Forms.Label lblExpenseHourlyWage;
    public System.Windows.Forms.NumericUpDown numHourlyWage;
    public System.Windows.Forms.Button btnTimeAddExpense;
    public System.Windows.Forms.Label lblExpenseTimeInvoiceID;
    public System.Windows.Forms.NumericUpDown numExpenseTimeInvoiceID;
    public System.Windows.Forms.TextBox txtInvoiceClientName;
    public System.Windows.Forms.TextBox txtInvoiceSearch;
    public System.Windows.Forms.Panel pnlCreateEditInvoice;
    public System.Windows.Forms.Button btnInvoiceClearResults;
    public System.Windows.Forms.Button btnInvoiceSearch;
    private System.Windows.Forms.Button btnCreateInvoice;
    private System.Windows.Forms.Button btnCreateEditDone;
    public System.Windows.Forms.Button btnCreateEditAddItems;

    }
}
