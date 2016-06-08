namespace ADLSystemTray
{
    partial class frmSysTray
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSysTray));
            this.btnRunNow = new System.Windows.Forms.Button();
            this.lblWebServiceURL = new System.Windows.Forms.Label();
            this.txtWebServiceURL = new System.Windows.Forms.TextBox();
            this.txtRemoteDbName = new System.Windows.Forms.TextBox();
            this.txtLocalConnectionString = new System.Windows.Forms.TextBox();
            this.lblRemoteDbName = new System.Windows.Forms.Label();
            this.lblLocalConnectionString = new System.Windows.Forms.Label();
            this.btnHideForm = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MenuItemShowForm = new System.Windows.Forms.ToolStripMenuItem();
            this.AnalogDataLift = new System.Windows.Forms.NotifyIcon(this.components);
            this.lbltxtLastRunDate = new System.Windows.Forms.Label();
            this.txtLastRunDate = new System.Windows.Forms.DateTimePicker();
            this.lblPollInterval = new System.Windows.Forms.Label();
            this.txtPollInterval = new System.Windows.Forms.TextBox();
            this.picWebService = new System.Windows.Forms.PictureBox();
            this.picRemoteDB = new System.Windows.Forms.PictureBox();
            this.picLocalConnString = new System.Windows.Forms.PictureBox();
            this.picPollInterval = new System.Windows.Forms.PictureBox();
            this.piclastRunDate = new System.Windows.Forms.PictureBox();
            this.btnSetRunNowKeyToTrue = new System.Windows.Forms.Button();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picWebService)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRemoteDB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLocalConnString)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPollInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.piclastRunDate)).BeginInit();
            this.SuspendLayout();
            // 
            // btnRunNow
            // 
            this.btnRunNow.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRunNow.Location = new System.Drawing.Point(15, 219);
            this.btnRunNow.Name = "btnRunNow";
            this.btnRunNow.Size = new System.Drawing.Size(94, 38);
            this.btnRunNow.TabIndex = 1;
            this.btnRunNow.Text = "Save";
            this.btnRunNow.UseVisualStyleBackColor = true;
            this.btnRunNow.Click += new System.EventHandler(this.btnRunNow_Click);
            // 
            // lblWebServiceURL
            // 
            this.lblWebServiceURL.AutoSize = true;
            this.lblWebServiceURL.Location = new System.Drawing.Point(12, 16);
            this.lblWebServiceURL.Name = "lblWebServiceURL";
            this.lblWebServiceURL.Size = new System.Drawing.Size(97, 13);
            this.lblWebServiceURL.TabIndex = 2;
            this.lblWebServiceURL.Text = "Web Service URL:";
            // 
            // txtWebServiceURL
            // 
            this.txtWebServiceURL.Location = new System.Drawing.Point(186, 9);
            this.txtWebServiceURL.Name = "txtWebServiceURL";
            this.txtWebServiceURL.Size = new System.Drawing.Size(262, 20);
            this.txtWebServiceURL.TabIndex = 4;
            this.txtWebServiceURL.Click += new System.EventHandler(this.txtWebServiceURL_Click);
            // 
            // txtRemoteDbName
            // 
            this.txtRemoteDbName.Location = new System.Drawing.Point(186, 49);
            this.txtRemoteDbName.Name = "txtRemoteDbName";
            this.txtRemoteDbName.Size = new System.Drawing.Size(262, 20);
            this.txtRemoteDbName.TabIndex = 5;
            this.txtRemoteDbName.Click += new System.EventHandler(this.txtRemoteDbName_Click);
            // 
            // txtLocalConnectionString
            // 
            this.txtLocalConnectionString.Location = new System.Drawing.Point(186, 91);
            this.txtLocalConnectionString.Name = "txtLocalConnectionString";
            this.txtLocalConnectionString.Size = new System.Drawing.Size(262, 20);
            this.txtLocalConnectionString.TabIndex = 6;
            this.txtLocalConnectionString.Click += new System.EventHandler(this.txtLocalConnectionString_Click);
            // 
            // lblRemoteDbName
            // 
            this.lblRemoteDbName.AutoSize = true;
            this.lblRemoteDbName.Location = new System.Drawing.Point(12, 56);
            this.lblRemoteDbName.Name = "lblRemoteDbName";
            this.lblRemoteDbName.Size = new System.Drawing.Size(127, 13);
            this.lblRemoteDbName.TabIndex = 7;
            this.lblRemoteDbName.Text = "Remote Database Name:";
            // 
            // lblLocalConnectionString
            // 
            this.lblLocalConnectionString.AutoSize = true;
            this.lblLocalConnectionString.Location = new System.Drawing.Point(12, 98);
            this.lblLocalConnectionString.Name = "lblLocalConnectionString";
            this.lblLocalConnectionString.Size = new System.Drawing.Size(123, 13);
            this.lblLocalConnectionString.TabIndex = 8;
            this.lblLocalConnectionString.Text = "Local Connection String:";
            // 
            // btnHideForm
            // 
            this.btnHideForm.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHideForm.Location = new System.Drawing.Point(115, 219);
            this.btnHideForm.Name = "btnHideForm";
            this.btnHideForm.Size = new System.Drawing.Size(94, 38);
            this.btnHideForm.TabIndex = 10;
            this.btnHideForm.Text = "Close";
            this.btnHideForm.UseVisualStyleBackColor = true;
            this.btnHideForm.Click += new System.EventHandler(this.btnHideForm_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemShowForm});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(140, 26);
            // 
            // MenuItemShowForm
            // 
            this.MenuItemShowForm.Name = "MenuItemShowForm";
            this.MenuItemShowForm.Size = new System.Drawing.Size(139, 22);
            this.MenuItemShowForm.Text = "Edit Settings";
            this.MenuItemShowForm.Click += new System.EventHandler(this.MenuItemShowForm_Click);
            // 
            // AnalogDataLift
            // 
            this.AnalogDataLift.ContextMenuStrip = this.contextMenuStrip1;
            this.AnalogDataLift.Icon = ((System.Drawing.Icon)(resources.GetObject("AnalogDataLift.Icon")));
            this.AnalogDataLift.Text = "AnalogDataLift";
            this.AnalogDataLift.Visible = true;
            // 
            // lbltxtLastRunDate
            // 
            this.lbltxtLastRunDate.AutoSize = true;
            this.lbltxtLastRunDate.Location = new System.Drawing.Point(12, 190);
            this.lbltxtLastRunDate.Name = "lbltxtLastRunDate";
            this.lbltxtLastRunDate.Size = new System.Drawing.Size(82, 13);
            this.lbltxtLastRunDate.TabIndex = 11;
            this.lbltxtLastRunDate.Text = "Last Run Date: ";
            // 
            // txtLastRunDate
            // 
            this.txtLastRunDate.Location = new System.Drawing.Point(186, 183);
            this.txtLastRunDate.Name = "txtLastRunDate";
            this.txtLastRunDate.Size = new System.Drawing.Size(262, 20);
            this.txtLastRunDate.TabIndex = 13;
            this.txtLastRunDate.Value = new System.DateTime(2016, 6, 3, 0, 0, 0, 0);
            this.txtLastRunDate.ValueChanged += new System.EventHandler(this.txtLastRunDate_ValueChanged);
            // 
            // lblPollInterval
            // 
            this.lblPollInterval.AutoSize = true;
            this.lblPollInterval.Location = new System.Drawing.Point(12, 143);
            this.lblPollInterval.Name = "lblPollInterval";
            this.lblPollInterval.Size = new System.Drawing.Size(142, 13);
            this.lblPollInterval.TabIndex = 14;
            this.lblPollInterval.Text = "Poll Time Interval (Seconds):";
            // 
            // txtPollInterval
            // 
            this.txtPollInterval.Location = new System.Drawing.Point(186, 136);
            this.txtPollInterval.Name = "txtPollInterval";
            this.txtPollInterval.Size = new System.Drawing.Size(262, 20);
            this.txtPollInterval.TabIndex = 15;
            this.txtPollInterval.Click += new System.EventHandler(this.txtPollInterval_Click);
            // 
            // picWebService
            // 
            this.picWebService.Location = new System.Drawing.Point(454, 9);
            this.picWebService.Name = "picWebService";
            this.picWebService.Size = new System.Drawing.Size(25, 20);
            this.picWebService.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picWebService.TabIndex = 16;
            this.picWebService.TabStop = false;
            // 
            // picRemoteDB
            // 
            this.picRemoteDB.Location = new System.Drawing.Point(454, 49);
            this.picRemoteDB.Name = "picRemoteDB";
            this.picRemoteDB.Size = new System.Drawing.Size(25, 20);
            this.picRemoteDB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picRemoteDB.TabIndex = 17;
            this.picRemoteDB.TabStop = false;
            // 
            // picLocalConnString
            // 
            this.picLocalConnString.Location = new System.Drawing.Point(454, 91);
            this.picLocalConnString.Name = "picLocalConnString";
            this.picLocalConnString.Size = new System.Drawing.Size(25, 20);
            this.picLocalConnString.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picLocalConnString.TabIndex = 18;
            this.picLocalConnString.TabStop = false;
            // 
            // picPollInterval
            // 
            this.picPollInterval.Location = new System.Drawing.Point(454, 136);
            this.picPollInterval.Name = "picPollInterval";
            this.picPollInterval.Size = new System.Drawing.Size(25, 20);
            this.picPollInterval.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picPollInterval.TabIndex = 19;
            this.picPollInterval.TabStop = false;
            // 
            // piclastRunDate
            // 
            this.piclastRunDate.Location = new System.Drawing.Point(454, 183);
            this.piclastRunDate.Name = "piclastRunDate";
            this.piclastRunDate.Size = new System.Drawing.Size(25, 20);
            this.piclastRunDate.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.piclastRunDate.TabIndex = 20;
            this.piclastRunDate.TabStop = false;
            // 
            // btnSetRunNowKeyToTrue
            // 
            this.btnSetRunNowKeyToTrue.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSetRunNowKeyToTrue.Location = new System.Drawing.Point(215, 219);
            this.btnSetRunNowKeyToTrue.Name = "btnSetRunNowKeyToTrue";
            this.btnSetRunNowKeyToTrue.Size = new System.Drawing.Size(94, 38);
            this.btnSetRunNowKeyToTrue.TabIndex = 21;
            this.btnSetRunNowKeyToTrue.Text = "Run Now";
            this.btnSetRunNowKeyToTrue.UseVisualStyleBackColor = true;
            this.btnSetRunNowKeyToTrue.Click += new System.EventHandler(this.btnSetRunNowKeyToTrue_Click);
            // 
            // frmSysTray
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(493, 264);
            this.Controls.Add(this.btnSetRunNowKeyToTrue);
            this.Controls.Add(this.piclastRunDate);
            this.Controls.Add(this.picPollInterval);
            this.Controls.Add(this.picLocalConnString);
            this.Controls.Add(this.picRemoteDB);
            this.Controls.Add(this.picWebService);
            this.Controls.Add(this.txtPollInterval);
            this.Controls.Add(this.lblPollInterval);
            this.Controls.Add(this.txtLastRunDate);
            this.Controls.Add(this.lbltxtLastRunDate);
            this.Controls.Add(this.btnHideForm);
            this.Controls.Add(this.lblLocalConnectionString);
            this.Controls.Add(this.lblRemoteDbName);
            this.Controls.Add(this.txtLocalConnectionString);
            this.Controls.Add(this.txtRemoteDbName);
            this.Controls.Add(this.txtWebServiceURL);
            this.Controls.Add(this.lblWebServiceURL);
            this.Controls.Add(this.btnRunNow);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmSysTray";
            this.Text = "Analogue Data Lift";
            this.Load += new System.EventHandler(this.frmSysTray_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picWebService)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRemoteDB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLocalConnString)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPollInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.piclastRunDate)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRunNow;
        private System.Windows.Forms.Label lblWebServiceURL;
        private System.Windows.Forms.TextBox txtWebServiceURL;
        private System.Windows.Forms.TextBox txtRemoteDbName;
        private System.Windows.Forms.TextBox txtLocalConnectionString;
        private System.Windows.Forms.Label lblRemoteDbName;
        private System.Windows.Forms.Label lblLocalConnectionString;
        private System.Windows.Forms.Button btnHideForm;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.NotifyIcon AnalogDataLift;
        private System.Windows.Forms.ToolStripMenuItem MenuItemShowForm;
        private System.Windows.Forms.Label lbltxtLastRunDate;
        private System.Windows.Forms.DateTimePicker txtLastRunDate;
        private System.Windows.Forms.Label lblPollInterval;
        private System.Windows.Forms.TextBox txtPollInterval;
        private System.Windows.Forms.PictureBox picWebService;
        private System.Windows.Forms.PictureBox picRemoteDB;
        private System.Windows.Forms.PictureBox picLocalConnString;
        private System.Windows.Forms.PictureBox picPollInterval;
        private System.Windows.Forms.PictureBox piclastRunDate;
        private System.Windows.Forms.Button btnSetRunNowKeyToTrue;
    }
}

