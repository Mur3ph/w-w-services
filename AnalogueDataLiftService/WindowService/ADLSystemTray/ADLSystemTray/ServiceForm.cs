using ADLSystemTray.registry;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ADLSystemTray
{
    public partial class frmSysTray : Form
    {
        private LocalRegisryService _service = new LocalRegisryService();

        //Regular Expressions
        private const string WEB_SERVICE_URL_REGULAR_EXPRESSIONS = @"^(http://www\.)([\w]+)\.([\w]+)$";
        private const string LOCAL_CONNECTION_STRING_REGULAR_EXPRESSIONS = @"^(?=.*\b(?i)data source\b)(?=.*\b(?i)initial catalog\b)(?=.*\b(?i)id\b)(?=.*\b(?i)password\b).+$";
        private const string POLL_TIME_INTERVAL_REGULAR_EXPRESSIONS = "^[0-9]";

        //Text Box Hints
        private const string WEB_SERVICE_URL_HINT = "e.g. http://www.example.com";
        private const string REMOTE_DATABASE_NAME_HINT = "e.g. Name of database on RMSLite installation";
        private const string LOCAL_CONNECTION_STRING_HINT = "e.g. data source; initial catalog; User ID; Password;";
        private const string POLL_TIME_INTERVAL_HINT = "e.g. 0-9";

        public frmSysTray()
        {
            InitializeComponent();
        }

        private void frmSysTray_Load(object sender, EventArgs e)
        {
            AnalogDataLift.Icon = Properties.Resources.database1;
            this.ControlBox = false;
            this.addDataToTextBoxes();
        }

        //When you right-click the System Tray image this button appears as "Edit Settings"
        private void MenuItemShowForm_Click(object sender, EventArgs e)
        {
            this.Show();
            this.addDataToTextBoxes();
        }

        //The "Hide" button on the form to send the form back to the System Tray
        private void btnHideForm_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.removeHintsToTextBoxes();
            this.removeImagesFromPictureBox();
            this.setLabelsBlack();
        }

        //This is the "Save" button on the form
        private void btnRunNow_Click(object sender, EventArgs e)
        {
            //if the text area is empty or is still showing the hint, then DON'T save/send data to the Registry
            if (this.txtWebServiceURL.Text != "" && this.txtWebServiceURL.Text != WEB_SERVICE_URL_HINT)
            {
                //Only if the user inputs the correct data format, will we save/send data to the Registry
                if (RegExp(WEB_SERVICE_URL_REGULAR_EXPRESSIONS, this.txtWebServiceURL, this.lblWebServiceURL, "Web Service URL", this.picWebService))
                {
                    //Save user input to the Registry
                    this._service.setRegistryValueWebServiceURL(this.txtWebServiceURL.Text);
                }
            }

            if (this.txtRemoteDbName.Text != "" && this.txtRemoteDbName.Text != REMOTE_DATABASE_NAME_HINT)
            {
                this._service.setRegistryValueRemoteDatabaseName(this.txtRemoteDbName.Text);
                this.picRemoteDB.Image = Properties.Resources.good;//good => image from Resources                 
                this.lblRemoteDbName.ForeColor = Color.Green;//Set label green
            }

            if (txtLocalConnectionString.Text != "" && txtLocalConnectionString.Text != LOCAL_CONNECTION_STRING_HINT)
            {
                if (RegExp(LOCAL_CONNECTION_STRING_REGULAR_EXPRESSIONS, this.txtLocalConnectionString, this.lblLocalConnectionString, "Local Connection String", this.picLocalConnString))
                {
                    this._service.setRegistryValueLocalConnectionString(this.txtLocalConnectionString.Text);
                }
            }

            if (txtPollInterval.Text != "" && txtPollInterval.Text != POLL_TIME_INTERVAL_HINT)
            {
                if (RegExp(POLL_TIME_INTERVAL_REGULAR_EXPRESSIONS, this.txtPollInterval, this.lblPollInterval, "Poll Time Interval (Milliseconds)", this.picPollInterval))
                {
                    this._service.setRegistryValueTimePollInterval(this.txtPollInterval.Text);
                }
            }
        }

        //USER INPUT VALIDATION..

        //Event: Only if the value of the datae picker changes should we send the value to the registry
        private void txtLastRunDate_ValueChanged(object sender, EventArgs e)
        {
            if (this.txtLastRunDate.Text != "")
            {
                this._service.setRegistryValueLastRunDateToNow(this.txtLastRunDate.Value);
                this.piclastRunDate.Image = Properties.Resources.good;//good => image from Resources                 
                this.lbltxtLastRunDate.ForeColor = Color.Green;//Set label green
            }
        }

        //Generates an image good or bad depending on, if user input is correct. It also changes the label green or red
        private bool RegExp(string expression, TextBox txtBox, Label lbl, string nameOfLabel, PictureBox pb)         
         {             
             Regex regex = new Regex(expression);             
             if (regex.IsMatch(txtBox.Text))             
             {
                 pb.Image = Properties.Resources.good;//good => image from Resources                 
                 lbl.ForeColor = Color.Green;            //Set label green     
                 return true;
             }             
             else           
             {                
                 pb.Image = Properties.Resources.bad;//bad => image from Resources                
                 lbl.ForeColor = Color.Red;              //Set label green   
                 return false;
             }         
         }

        //EVENT WHEN A TEXT BOX IS CLICK ON, IT CLEARS THE HINT FOR THE USER TO WRITE
        private void txtWebServiceURL_Click(object sender, EventArgs e)
        {
            this.txtWebServiceURL.Clear();
            this.txtWebServiceURL.ForeColor = Color.Black;
        }

        private void txtRemoteDbName_Click(object sender, EventArgs e)
        {
            this.txtRemoteDbName.Clear();
            this.txtRemoteDbName.ForeColor = Color.Black;
        }

        private void txtLocalConnectionString_Click(object sender, EventArgs e)
        {
            this.txtLocalConnectionString.Clear();
            this.txtLocalConnectionString.ForeColor = Color.Black;
        }

        private void txtPollInterval_Click(object sender, EventArgs e)
        {
            this.txtPollInterval.Clear();
            this.txtPollInterval.ForeColor = Color.Black;
        }

        //Add hints for user to text box area 
        private void addDataToTextBoxes()
        {
            setWebServiceURLTextbox();
            setDatabaseNameTextbox();
            setLocalConnectionStringTextbox();
            setTimePollIntervalTextbox();
        }

        //Remove hints for the text box area
        private void removeHintsToTextBoxes()
        {
            this.txtWebServiceURL.Clear();
            this.txtRemoteDbName.Clear();
            this.txtLocalConnectionString.Clear();
            this.txtPollInterval.Clear();
        }

        //Remove image from picture box area of the form
        private void removeImagesFromPictureBox()
        {
            this.picWebService.Image = null;
            this.picRemoteDB.Image = null;
            this.picLocalConnString.Image = null;
            this.picPollInterval.Image = null;
            this.piclastRunDate.Image = null;
        }

        //Set labels back to their original colour black
        private void setLabelsBlack()
        {
            this.lblWebServiceURL.ForeColor = Color.Black;
            this.lblRemoteDbName.ForeColor = Color.Black;
            this.lblLocalConnectionString.ForeColor = Color.Black;
            this.lblPollInterval.ForeColor = Color.Black;
            this.lbltxtLastRunDate.ForeColor = Color.Black;
        }

        private void btnSetRunNowKeyToTrue_Click(object sender, EventArgs e)
        {
            _service.setRegistryValueRunNowBoolean("true");
        }

        //Setting text boxes with data from the registry or hints for user
        private void setDatabaseNameTextbox()
        {
            if (this._service.getRemoteDatabaseNameFromRegistry() != null || this._service.getRemoteDatabaseNameFromRegistry() != "")
            {
                this.txtRemoteDbName.Text = this._service.getRemoteDatabaseNameFromRegistry().ToString();
                this.txtRemoteDbName.ForeColor = Color.Gray;
                this.txtRemoteDbName.Select(this.txtRemoteDbName.TextLength, 0);
            }
            else
            {
                this.txtRemoteDbName.Text = REMOTE_DATABASE_NAME_HINT;
                this.txtRemoteDbName.ForeColor = Color.Gray;
                this.txtRemoteDbName.Select(this.txtRemoteDbName.TextLength, 0);
            }
        }

        private void setWebServiceURLTextbox()
        {
            if (this._service.getWebServiceURLFromRegistry() != null || this._service.getWebServiceURLFromRegistry() != "")
            {
                this.txtWebServiceURL.Text = this._service.getWebServiceURLFromRegistry().ToString();
                this.txtWebServiceURL.ForeColor = Color.Gray;
                this.txtWebServiceURL.Select(this.txtWebServiceURL.TextLength, 0);
            }
            else
            {
                this.txtWebServiceURL.Text = WEB_SERVICE_URL_HINT;
                this.txtWebServiceURL.ForeColor = Color.Gray;
                this.txtWebServiceURL.Select(this.txtWebServiceURL.TextLength, 0);
            }
        }

        private void setLocalConnectionStringTextbox()
        {
            if (this._service.getLocalConnectionStringFromRegistry() != null || this._service.getLocalConnectionStringFromRegistry() != "")
            {
                this.txtLocalConnectionString.Text = this._service.getLocalConnectionStringFromRegistry().ToString();
                this.txtLocalConnectionString.ForeColor = Color.Gray;
                this.txtLocalConnectionString.Select(this.txtLocalConnectionString.TextLength, 0);
            }
            else
            {
                this.txtLocalConnectionString.Text = LOCAL_CONNECTION_STRING_HINT;
                this.txtLocalConnectionString.ForeColor = Color.Gray;
                this.txtLocalConnectionString.Select(this.txtLocalConnectionString.TextLength, 0);
            }
        }

        private void setTimePollIntervalTextbox()
        {
            if (this._service.getTimePollIntervalFromRegistry() != null || this._service.getTimePollIntervalFromRegistry() != "")
            {
                this.txtPollInterval.Text = this._service.getTimePollIntervalFromRegistry().ToString();
                this.txtPollInterval.ForeColor = Color.Gray;
                this.txtPollInterval.Select(this.txtPollInterval.TextLength, 0);
            }
            else
            {
                this.txtPollInterval.Text = POLL_TIME_INTERVAL_HINT;
                this.txtPollInterval.ForeColor = Color.Gray;
                this.txtPollInterval.Select(this.txtPollInterval.TextLength, 0);
            }
        }

        //TODO: Set the date picker on the form with the last run date in the registry, if it's there..
        private void setDateTextbox()
        {
            if (this._service.getLastRunDateFromRegistry() != null || this._service.getLastRunDateFromRegistry() != "")
            {
                string[] datesString = this._service.getTimePollIntervalFromRegistry().Split('-');
                int[] datesIntger = Array.ConvertAll(datesString, int.Parse);

                //Format of date: dd-MM-yyyy..
                //this.txtLastRunDate.Value = new DateTime(datesIntger[2], datesIntger[1], datesIntger[0]);
                DateTime lastRunDate = DateTime.ParseExact(this._service.getTimePollIntervalFromRegistry(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                this.txtLastRunDate.Value = lastRunDate;
            }
        }
}

    }