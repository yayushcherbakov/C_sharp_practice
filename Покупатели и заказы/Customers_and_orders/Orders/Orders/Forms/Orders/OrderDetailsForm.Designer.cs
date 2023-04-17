namespace Orders.Forms.Orders
{
    partial class OrderDetailsForm
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
            this.labelTotalPrice = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.labelStatus = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.labelCreationDate = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.labelOrderNumber = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labelUser = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cartListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
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
            this.splitContainer2.Panel1.Controls.Add(this.labelTotalPrice);
            this.splitContainer2.Panel1.Controls.Add(this.label9);
            this.splitContainer2.Panel1.Controls.Add(this.labelStatus);
            this.splitContainer2.Panel1.Controls.Add(this.label7);
            this.splitContainer2.Panel1.Controls.Add(this.labelCreationDate);
            this.splitContainer2.Panel1.Controls.Add(this.label5);
            this.splitContainer2.Panel1.Controls.Add(this.labelOrderNumber);
            this.splitContainer2.Panel1.Controls.Add(this.label3);
            this.splitContainer2.Panel1.Controls.Add(this.labelUser);
            this.splitContainer2.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.cartListView);
            this.splitContainer2.Size = new System.Drawing.Size(507, 389);
            this.splitContainer2.SplitterDistance = 93;
            this.splitContainer2.TabIndex = 4;
            // 
            // labelTotalPrice
            // 
            this.labelTotalPrice.AutoSize = true;
            this.labelTotalPrice.Location = new System.Drawing.Point(212, 72);
            this.labelTotalPrice.Name = "labelTotalPrice";
            this.labelTotalPrice.Size = new System.Drawing.Size(41, 13);
            this.labelTotalPrice.TabIndex = 9;
            this.labelTotalPrice.Text = "label10";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(212, 55);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(57, 13);
            this.label9.TabIndex = 8;
            this.label9.Text = "Total price";
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Location = new System.Drawing.Point(13, 72);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(35, 13);
            this.labelStatus.TabIndex = 7;
            this.labelStatus.Text = "label8";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 55);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(37, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Status";
            // 
            // labelCreationDate
            // 
            this.labelCreationDate.AutoSize = true;
            this.labelCreationDate.Location = new System.Drawing.Point(321, 30);
            this.labelCreationDate.Name = "labelCreationDate";
            this.labelCreationDate.Size = new System.Drawing.Size(35, 13);
            this.labelCreationDate.TabIndex = 5;
            this.labelCreationDate.Text = "label6";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(321, 13);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Creation date";
            // 
            // labelOrderNumber
            // 
            this.labelOrderNumber.AutoSize = true;
            this.labelOrderNumber.Location = new System.Drawing.Point(212, 30);
            this.labelOrderNumber.Name = "labelOrderNumber";
            this.labelOrderNumber.Size = new System.Drawing.Size(35, 13);
            this.labelOrderNumber.TabIndex = 3;
            this.labelOrderNumber.Text = "label4";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(212, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Order number";
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
            // cartListView
            // 
            this.cartListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.cartListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cartListView.FullRowSelect = true;
            this.cartListView.HideSelection = false;
            this.cartListView.Location = new System.Drawing.Point(0, 0);
            this.cartListView.Name = "cartListView";
            this.cartListView.Size = new System.Drawing.Size(507, 292);
            this.cartListView.TabIndex = 0;
            this.cartListView.UseCompatibleStateImageBehavior = false;
            this.cartListView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 263;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Article";
            this.columnHeader2.Width = 114;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Price";
            this.columnHeader3.Width = 66;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Count";
            // 
            // OrderDetailsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(507, 389);
            this.Controls.Add(this.splitContainer2);
            this.Name = "OrderDetailsForm";
            this.Text = "OrderDetailsForm";
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Label labelCreationDate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label labelOrderNumber;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelUser;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView cartListView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Label labelTotalPrice;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.Label label7;
    }
}