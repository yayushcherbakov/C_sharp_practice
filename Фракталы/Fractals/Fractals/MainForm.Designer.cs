
namespace Fractals
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.applyKochCurveButton = new System.Windows.Forms.Button();
            this.applyPythagorasTreeButton = new System.Windows.Forms.Button();
            this.serpinskiTriangleButton = new System.Windows.Forms.Button();
            this.serpinskiTriangle = new System.Windows.Forms.Button();
            this.cantorSetButton = new System.Windows.Forms.Button();
            this.trackBarKochCurve = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labelKochCurve = new System.Windows.Forms.Label();
            this.labelCantorSet = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.trackBarCantorSet = new System.Windows.Forms.TrackBar();
            this.trackBarSerpinskiCarpet = new System.Windows.Forms.TrackBar();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.labelSerpinskiCarpet = new System.Windows.Forms.Label();
            this.labelSerpinskiTriangle = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.trackBarSerpinskiTriangle = new System.Windows.Forms.TrackBar();
            this.trackBarPythagorasTree5 = new System.Windows.Forms.TrackBar();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.labelPythagorasTree = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarKochCurve)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarCantorSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSerpinskiCarpet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSerpinskiTriangle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarPythagorasTree5)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox
            // 
            this.pictureBox.BackColor = System.Drawing.Color.White;
            this.pictureBox.Location = new System.Drawing.Point(0, 0);
            this.pictureBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(600, 600);
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            // 
            // applyKochCurveButton
            // 
            this.applyKochCurveButton.Location = new System.Drawing.Point(607, 131);
            this.applyKochCurveButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.applyKochCurveButton.Name = "applyKochCurveButton";
            this.applyKochCurveButton.Size = new System.Drawing.Size(174, 22);
            this.applyKochCurveButton.TabIndex = 1;
            this.applyKochCurveButton.Text = "Кривая Коха";
            this.applyKochCurveButton.UseVisualStyleBackColor = true;
            this.applyKochCurveButton.Click += new System.EventHandler(this.ApplyKochCurveButtonClick);
            // 
            // applyPythagorasTreeButton
            // 
            this.applyPythagorasTreeButton.Location = new System.Drawing.Point(607, 244);
            this.applyPythagorasTreeButton.Name = "applyPythagorasTreeButton";
            this.applyPythagorasTreeButton.Size = new System.Drawing.Size(174, 23);
            this.applyPythagorasTreeButton.TabIndex = 3;
            this.applyPythagorasTreeButton.Text = "Пифагорово дерево";
            this.applyPythagorasTreeButton.UseVisualStyleBackColor = true;
            this.applyPythagorasTreeButton.Click += new System.EventHandler(this.ApplyPythagorasTreeButtonClick);
            // 
            // serpinskiTriangleButton
            // 
            this.serpinskiTriangleButton.Location = new System.Drawing.Point(606, 350);
            this.serpinskiTriangleButton.Name = "serpinskiTriangleButton";
            this.serpinskiTriangleButton.Size = new System.Drawing.Size(174, 23);
            this.serpinskiTriangleButton.TabIndex = 4;
            this.serpinskiTriangleButton.Text = "Треугольник Серпинского";
            this.serpinskiTriangleButton.UseVisualStyleBackColor = true;
            this.serpinskiTriangleButton.Click += new System.EventHandler(this.SerpinskiTriangleButtonClick);
            // 
            // serpinskiTriangle
            // 
            this.serpinskiTriangle.Location = new System.Drawing.Point(606, 458);
            this.serpinskiTriangle.Name = "serpinskiTriangle";
            this.serpinskiTriangle.Size = new System.Drawing.Size(174, 23);
            this.serpinskiTriangle.TabIndex = 5;
            this.serpinskiTriangle.Text = "Ковер Серпинского";
            this.serpinskiTriangle.UseVisualStyleBackColor = true;
            this.serpinskiTriangle.Click += new System.EventHandler(this.SerpinskiCarpetButtonClick);
            // 
            // cantorSetButton
            // 
            this.cantorSetButton.Location = new System.Drawing.Point(606, 566);
            this.cantorSetButton.Name = "cantorSetButton";
            this.cantorSetButton.Size = new System.Drawing.Size(173, 23);
            this.cantorSetButton.TabIndex = 6;
            this.cantorSetButton.Text = " Множество Кантора";
            this.cantorSetButton.UseVisualStyleBackColor = true;
            this.cantorSetButton.Click += new System.EventHandler(this.CantorSetButtonClick);
            // 
            // trackBar1
            // 
            this.trackBarKochCurve.LargeChange = 3;
            this.trackBarKochCurve.Location = new System.Drawing.Point(634, 81);
            this.trackBarKochCurve.Maximum = 7;
            this.trackBarKochCurve.Name = "trackBar1";
            this.trackBarKochCurve.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.trackBarKochCurve.Size = new System.Drawing.Size(104, 45);
            this.trackBarKochCurve.TabIndex = 8;
            this.trackBarKochCurve.ValueChanged += new System.EventHandler(this.TrackBarKochCurveValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(615, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(13, 15);
            this.label1.TabIndex = 9;
            this.label1.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(744, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(13, 15);
            this.label2.TabIndex = 9;
            this.label2.Text = "7";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(646, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 15);
            this.label3.TabIndex = 10;
            this.label3.Text = "Итерации:";
            // 
            // kochIterLabel
            // 
            this.labelKochCurve.AccessibleName = "";
            this.labelKochCurve.AutoSize = true;
            this.labelKochCurve.Location = new System.Drawing.Point(712, 64);
            this.labelKochCurve.Name = "kochIterLabel";
            this.labelKochCurve.Size = new System.Drawing.Size(13, 15);
            this.labelKochCurve.TabIndex = 11;
            this.labelKochCurve.Text = "0";
            // 
            // label4
            // 
            this.labelCantorSet.AccessibleName = "";
            this.labelCantorSet.AutoSize = true;
            this.labelCantorSet.Location = new System.Drawing.Point(712, 498);
            this.labelCantorSet.Name = "label4";
            this.labelCantorSet.Size = new System.Drawing.Size(13, 15);
            this.labelCantorSet.TabIndex = 11;
            this.labelCantorSet.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(646, 496);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 15);
            this.label5.TabIndex = 10;
            this.label5.Text = "Итерации:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(744, 515);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(13, 15);
            this.label6.TabIndex = 9;
            this.label6.Text = "6";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(615, 515);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(13, 15);
            this.label7.TabIndex = 9;
            this.label7.Text = "0";
            // 
            // trackBar2
            // 
            this.trackBarCantorSet.LargeChange = 3;
            this.trackBarCantorSet.Location = new System.Drawing.Point(634, 515);
            this.trackBarCantorSet.Maximum = 6;
            this.trackBarCantorSet.Name = "trackBar2";
            this.trackBarCantorSet.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.trackBarCantorSet.Size = new System.Drawing.Size(104, 45);
            this.trackBarCantorSet.TabIndex = 8;
            this.trackBarCantorSet.ValueChanged += new System.EventHandler(this.TrackBarCantorSetValueChanged);
            // 
            // trackBar3
            // 
            this.trackBarSerpinskiCarpet.LargeChange = 3;
            this.trackBarSerpinskiCarpet.Location = new System.Drawing.Point(634, 407);
            this.trackBarSerpinskiCarpet.Maximum = 5;
            this.trackBarSerpinskiCarpet.Name = "trackBar3";
            this.trackBarSerpinskiCarpet.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.trackBarSerpinskiCarpet.Size = new System.Drawing.Size(104, 45);
            this.trackBarSerpinskiCarpet.TabIndex = 8;
            this.trackBarSerpinskiCarpet.ValueChanged += new System.EventHandler(this.TrackBarSerpinskiCarpetValueChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(615, 407);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(13, 15);
            this.label8.TabIndex = 9;
            this.label8.Text = "0";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(744, 407);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(13, 15);
            this.label9.TabIndex = 9;
            this.label9.Text = "5";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(646, 388);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(64, 15);
            this.label10.TabIndex = 10;
            this.label10.Text = "Итерации:";
            // 
            // label11
            // 
            this.labelSerpinskiCarpet.AccessibleName = "";
            this.labelSerpinskiCarpet.AutoSize = true;
            this.labelSerpinskiCarpet.Location = new System.Drawing.Point(712, 390);
            this.labelSerpinskiCarpet.Name = "label11";
            this.labelSerpinskiCarpet.Size = new System.Drawing.Size(13, 15);
            this.labelSerpinskiCarpet.TabIndex = 11;
            this.labelSerpinskiCarpet.Text = "0";
            // 
            // label12
            // 
            this.labelSerpinskiTriangle.AccessibleName = "";
            this.labelSerpinskiTriangle.AutoSize = true;
            this.labelSerpinskiTriangle.Location = new System.Drawing.Point(712, 282);
            this.labelSerpinskiTriangle.Name = "label12";
            this.labelSerpinskiTriangle.Size = new System.Drawing.Size(13, 15);
            this.labelSerpinskiTriangle.TabIndex = 11;
            this.labelSerpinskiTriangle.Text = "0";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(646, 280);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(64, 15);
            this.label13.TabIndex = 10;
            this.label13.Text = "Итерации:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(744, 299);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(13, 15);
            this.label14.TabIndex = 9;
            this.label14.Text = "5";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(615, 299);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(13, 15);
            this.label15.TabIndex = 9;
            this.label15.Text = "0";
            // 
            // trackBar4
            // 
            this.trackBarSerpinskiTriangle.LargeChange = 3;
            this.trackBarSerpinskiTriangle.Location = new System.Drawing.Point(634, 299);
            this.trackBarSerpinskiTriangle.Maximum = 5;
            this.trackBarSerpinskiTriangle.Name = "trackBar4";
            this.trackBarSerpinskiTriangle.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.trackBarSerpinskiTriangle.Size = new System.Drawing.Size(104, 45);
            this.trackBarSerpinskiTriangle.TabIndex = 8;
            this.trackBarSerpinskiTriangle.ValueChanged += new System.EventHandler(this.TrackBarSerpinskiTriangleValueChanged);
            // 
            // trackBar5
            // 
            this.trackBarPythagorasTree5.LargeChange = 3;
            this.trackBarPythagorasTree5.Location = new System.Drawing.Point(634, 187);
            this.trackBarPythagorasTree5.Maximum = 12;
            this.trackBarPythagorasTree5.Name = "trackBar5";
            this.trackBarPythagorasTree5.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.trackBarPythagorasTree5.Size = new System.Drawing.Size(104, 45);
            this.trackBarPythagorasTree5.TabIndex = 8;
            this.trackBarPythagorasTree5.ValueChanged += new System.EventHandler(this.TrackBarPythagorasTreeValueChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(615, 187);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(13, 15);
            this.label16.TabIndex = 9;
            this.label16.Text = "0";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(744, 187);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(19, 15);
            this.label17.TabIndex = 9;
            this.label17.Text = "12";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(646, 168);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(64, 15);
            this.label18.TabIndex = 10;
            this.label18.Text = "Итерации:";
            // 
            // label19
            // 
            this.labelPythagorasTree.AccessibleName = "";
            this.labelPythagorasTree.AutoSize = true;
            this.labelPythagorasTree.Location = new System.Drawing.Point(712, 170);
            this.labelPythagorasTree.Name = "label19";
            this.labelPythagorasTree.Size = new System.Drawing.Size(13, 15);
            this.labelPythagorasTree.TabIndex = 11;
            this.labelPythagorasTree.Text = "0";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(634, 22);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(116, 15);
            this.label20.TabIndex = 12;
            this.label20.Text = "Панель управления";
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(784, 601);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.labelPythagorasTree);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.trackBarPythagorasTree5);
            this.Controls.Add(this.trackBarSerpinskiTriangle);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.labelSerpinskiTriangle);
            this.Controls.Add(this.labelSerpinskiCarpet);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.trackBarSerpinskiCarpet);
            this.Controls.Add(this.trackBarCantorSet);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.labelCantorSet);
            this.Controls.Add(this.labelKochCurve);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.trackBarKochCurve);
            this.Controls.Add(this.cantorSetButton);
            this.Controls.Add(this.serpinskiTriangle);
            this.Controls.Add(this.serpinskiTriangleButton);
            this.Controls.Add(this.applyPythagorasTreeButton);
            this.Controls.Add(this.applyKochCurveButton);
            this.Controls.Add(this.pictureBox);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximumSize = new System.Drawing.Size(800, 640);
            this.MinimumSize = new System.Drawing.Size(800, 640);
            this.Name = "MainForm";
            this.Text = "Итерации";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarKochCurve)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarCantorSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSerpinskiCarpet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSerpinskiTriangle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarPythagorasTree5)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Button applyKochCurveButton;
        private System.Windows.Forms.Button applyPythagorasTreeButton;
        private System.Windows.Forms.Button serpinskiTriangleButton;
        private System.Windows.Forms.Button serpinskiTriangle;
        private System.Windows.Forms.Button cantorSetButton;
        private System.Windows.Forms.TrackBar trackBarKochCurve;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelKochCurve;
        private System.Windows.Forms.Label labelCantorSet;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TrackBar trackBarCantorSet;
        private System.Windows.Forms.TrackBar trackBarSerpinskiCarpet;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label labelSerpinskiCarpet;
        private System.Windows.Forms.Label labelSerpinskiTriangle;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TrackBar trackBarSerpinskiTriangle;
        private System.Windows.Forms.TrackBar trackBarPythagorasTree5;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label labelPythagorasTree;
        private System.Windows.Forms.Label label20;
    }
}

