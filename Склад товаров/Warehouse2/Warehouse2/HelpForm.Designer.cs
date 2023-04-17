namespace Warehouse2
{
    partial class HelpForm
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
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(0, 0);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(538, 279);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "Hello, dear user.\n\n" +
            "This is some tutorial.\n\n" +
            "In the \"File\" menu item you can find basic operations with the warehouse (open, create, save).\n" +
            "Next to it you can find an operation for generating a report, sorting classifiers.\n\n" +
            "Working with classifiers is carried out using the context menu.You will find operations for adding, editing, deleting a classifier.This menu also contains the operation of adding a product to the classifier.\n\n" +
            "Editing a product is available by double - clicking on a cell with product data.\n" +
            "To remove a product you need to select a row with product and press “Delete” on keyboard.\n\n" +
            "Random generation of classifiers and products is also available in the classifier context menu\n\n" +
            "Good luck, have fun!";
            // 
            // HelpForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(538, 279);
            this.Controls.Add(this.richTextBox1);
            this.Name = "HelpForm";
            this.Text = "HelpForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
    }
}