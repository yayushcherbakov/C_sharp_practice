namespace Orders.Forms.Seller
{
    partial class ClientDetailsForm
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
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.labelPaidAmount = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.labelAddress = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.labelPhone = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.labelEmail = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labelUser = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.orderListView = new System.Windows.Forms.ListView();
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.button1);
            this.splitContainer2.Panel1.Controls.Add(this.labelPaidAmount);
            this.splitContainer2.Panel1.Controls.Add(this.label9);
            this.splitContainer2.Panel1.Controls.Add(this.labelAddress);
            this.splitContainer2.Panel1.Controls.Add(this.label7);
            this.splitContainer2.Panel1.Controls.Add(this.labelPhone);
            this.splitContainer2.Panel1.Controls.Add(this.label5);
            this.splitContainer2.Panel1.Controls.Add(this.labelEmail);
            this.splitContainer2.Panel1.Controls.Add(this.label3);
            this.splitContainer2.Panel1.Controls.Add(this.labelUser);
            this.splitContainer2.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.orderListView);
            this.splitContainer2.Size = new System.Drawing.Size(582, 375);
            this.splitContainer2.SplitterDistance = 89;
            this.splitContainer2.TabIndex = 5;
            // 
            // labelPaidAmount
            // 
            this.labelPaidAmount.AutoSize = true;
            this.labelPaidAmount.Location = new System.Drawing.Point(287, 72);
            this.labelPaidAmount.Name = "labelPaidAmount";
            this.labelPaidAmount.Size = new System.Drawing.Size(41, 13);
            this.labelPaidAmount.TabIndex = 9;
            this.labelPaidAmount.Text = "label10";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(287, 55);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(126, 13);
            this.label9.TabIndex = 8;
            this.label9.Text = "Paid amount for all orders";
            // 
            // labelAddress
            // 
            this.labelAddress.AutoSize = true;
            this.labelAddress.Location = new System.Drawing.Point(13, 72);
            this.labelAddress.Name = "labelAddress";
            this.labelAddress.Size = new System.Drawing.Size(35, 13);
            this.labelAddress.TabIndex = 7;
            this.labelAddress.Text = "label8";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 55);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(45, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Address";
            // 
            // labelPhone
            // 
            this.labelPhone.AutoSize = true;
            this.labelPhone.Location = new System.Drawing.Point(446, 30);
            this.labelPhone.Name = "labelPhone";
            this.labelPhone.Size = new System.Drawing.Size(35, 13);
            this.labelPhone.TabIndex = 5;
            this.labelPhone.Text = "label6";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(446, 13);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Phone";
            // 
            // labelEmail
            // 
            this.labelEmail.AutoSize = true;
            this.labelEmail.Location = new System.Drawing.Point(287, 30);
            this.labelEmail.Name = "labelEmail";
            this.labelEmail.Size = new System.Drawing.Size(35, 13);
            this.labelEmail.TabIndex = 3;
            this.labelEmail.Text = "label4";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(287, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Email";
            // 
            // labelUser
            // 
            this.labelUser.AutoSize = true;
            this.labelUser.Location = new System.Drawing.Point(13, 30);
            this.labelUser.Name = "labelUser";
            this.labelUser.Size = new System.Drawing.Size(58, 13);
            this.labelUser.TabIndex = 1;
            this.labelUser.Text = "User name";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "User";
            // 
            // orderListView
            // 
            this.orderListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader9,
            this.columnHeader8});
            this.orderListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.orderListView.FullRowSelect = true;
            this.orderListView.HideSelection = false;
            this.orderListView.Location = new System.Drawing.Point(0, 0);
            this.orderListView.Name = "orderListView";
            this.orderListView.Size = new System.Drawing.Size(582, 282);
            this.orderListView.TabIndex = 1;
            this.orderListView.UseCompatibleStateImageBehavior = false;
            this.orderListView.View = System.Windows.Forms.View.Details;
            this.orderListView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.OrderListViewMouseClick);
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Order number";
            this.columnHeader5.Width = 82;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Creation date";
            this.columnHeader6.Width = 114;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Status";
            this.columnHeader7.Width = 186;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "Total price";
            this.columnHeader9.Width = 80;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Products";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(480, 55);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 10;
            this.button1.Text = "Refresh";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.RefreshOrderViewButtonClick);
            // 
            // ClientDetailsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(582, 375);
            this.Controls.Add(this.splitContainer2);
            this.Name = "ClientDetailsForm";
            this.Text = "ClientDetailsForm";
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Label labelPaidAmount;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label labelAddress;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label labelPhone;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label labelEmail;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelUser;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView orderListView;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.Button button1;
    }
}