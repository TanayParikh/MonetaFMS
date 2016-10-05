/***************************************************************************************************************
 * 
    Author:         Tanay Parikh
    Project Name:   Moneta
    Description:    Client module managing the client datatable and datagridview. Enables the user to track
                    clients and contact information.
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

//Used for SQL database access
using MySql.Data.MySqlClient;

namespace Moneta
{
    internal class ClientModule
    {
        //Local shared data and form refrences used to access main form data
        private SharedData data;
        private frmMain frm;

        //Class constructor with the form and shared data as parameters
        public ClientModule(frmMain frm, SharedData data)
        {
            //Locally stores the refrences to the parameters
            this.frm = frm;
            this.data = data;
        }

        //Pre: None
        //Post: Creates the client in the database clients datatable.
        //Description. Given the specified form control information, creates a client through an sql command.
        public void CreateClient()
        {
            //Ensures that the data collected is in the valid format/length
            bool isDataValid = false;

            //Creates variables to assist in query generation
            string newClientSql;
            string newClientValues;
            string sqlQuery;

            //Retrieves user input for client data form controls
            string firstName = frm.txtCFirstName.Text;
            string lastName = frm.txtCLastName.Text;
            string company = frm.txtCCompany.Text;
            string phoneNumber = frm.txtCNumber.Text;
            string email = frm.txtCEmail.Text;
            string address = frm.txtCAddress.Text;
            string note = frm.txtCNotes.Text;

            //Checks if the first name, last name, phone number and email are all greater in length than 0
            if (firstName.Length > 0
                && lastName.Length > 0
                && phoneNumber.Length > 0
                && email.Length > 0)
            {
                //Attempts to parse the email address, to check validity in format
                try
                {
                    //If the email address is parsed, indicates valid data
                    System.Net.Mail.MailAddress validAddress = new System.Net.Mail.MailAddress(email);
                    isDataValid = true;
                }
                catch
                {
                    //If email is unparsable, indicates invalid email. Informs user.
                    MessageBox.Show("Please ensure that you have entered a valid email address.");
                    isDataValid = false;
                }
            }
            //Executes if data length isn't sufficient
            else
            {
                //Indicates invalid data, informs user.
                isDataValid = false;
                MessageBox.Show("Please ensure that you have filled in the mandatory fields. (First name, last name, phone number and email.)");
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
                newClientSql = "INSERT INTO clients (FirstName, LastName, Phone, Email";
                newClientValues = "VALUES ('" + firstName + "', '" + lastName + "', '" + phoneNumber + "', '" + email + "'";

                //Checks if the company textbox is null or empty
                if (!string.IsNullOrEmpty(company))
                {
                    //If not indicates the company is being added, and then adds the company name
                    newClientSql += ", Company";
                    newClientValues += ", '" + company + "'";
                }

                //Checks if the address textbox is null or empty
                if (!string.IsNullOrEmpty(address))
                {
                    //If not indicates the address is being added, and then adds the address value
                    newClientSql += ", Address";
                    newClientValues += ", '" + address + "'";
                }

                //Checks if the note textbox is null or empty
                if (!string.IsNullOrEmpty(note))
                {
                    //If not indicates the note is being added, and then adds the note value
                    newClientSql += ", Note";
                    newClientValues += ", '" + note + "'";
                }

                //Combines the name query with the value query, and includes closing brackets
                sqlQuery = newClientSql + ") " + newClientValues + ");";

                //Executes the command with the query and connection
                MySqlCommand newClient = new MySqlCommand(sqlQuery, data.connection);
                newClient.ExecuteNonQuery();

                //Re-fills the client table with the new data and informs user of successful entry
                frm.SetUpClientAutofill();
                frm.clientsTableAdapter.Fill(frm.mydbDataSet.clients);
                MessageBox.Show(firstName + " " + lastName + " was successfully added to the database.");
            }
        }

        //Indicates dgv selection has changed
        public void dgvSelectionChanged()
        {
            //Forces the re-binding of the dgv, the data is updated in the database table
            data.binding.EndEdit();
            frm.clientsTableAdapter.Update(frm.mydbDataSet.clients);
        }

        //Indicates dgv error has occured.
        public void dgvDataError(object sender, DataGridViewDataErrorEventArgs error)
        {
            //Calls for the handling of the error.
            data.DisplayDGVError(sender, error);
        }
    }
}