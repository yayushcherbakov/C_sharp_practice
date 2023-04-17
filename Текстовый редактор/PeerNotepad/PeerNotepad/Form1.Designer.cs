
namespace PeerNotepad
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openTxtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createDocumentInNewWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createDocumentInNewTabToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.savingCurrentDocumentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAllOpenDocumentsInWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openRtfToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectedFontToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectedCursiveFontToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectedBoldFontToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectedUnderlinedFontToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectedStrikethroughFontToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.customizeThemeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chooseDarkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chooseLightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chooseAutosaveFrequencyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveEveryTwoMinutesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveEveryTwenteenMinutesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveEveryHourToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(809, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openTxtToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.exitToolStripMenuItem,
            this.createDocumentInNewWindowToolStripMenuItem,
            this.createDocumentInNewTabToolStripMenuItem,
            this.savingCurrentDocumentToolStripMenuItem,
            this.saveAllOpenDocumentsInWindowToolStripMenuItem,
            this.openRtfToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(59, 24);
            this.fileToolStripMenuItem.Text = "Файл";
            // 
            // openTxtToolStripMenuItem
            // 
            this.openTxtToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openTxtToolStripMenuItem.Image")));
            this.openTxtToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openTxtToolStripMenuItem.Name = "openTxtToolStripMenuItem";
            this.openTxtToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openTxtToolStripMenuItem.Size = new System.Drawing.Size(467, 26);
            this.openTxtToolStripMenuItem.Text = "Открыть *.txt";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripMenuItem.Image")));
            this.saveToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(467, 26);
            this.saveToolStripMenuItem.Text = "Сохранить";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(467, 26);
            this.exitToolStripMenuItem.Text = "Закрыть";
            // 
            // createDocumentInNewWindowToolStripMenuItem
            // 
            this.createDocumentInNewWindowToolStripMenuItem.Name = "createDocumentInNewWindowToolStripMenuItem";
            this.createDocumentInNewWindowToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.createDocumentInNewWindowToolStripMenuItem.Size = new System.Drawing.Size(467, 26);
            this.createDocumentInNewWindowToolStripMenuItem.Text = "Создание документа в новом окне";
            // 
            // createDocumentInNewTabToolStripMenuItem
            // 
            this.createDocumentInNewTabToolStripMenuItem.Name = "createDocumentInNewTabToolStripMenuItem";
            this.createDocumentInNewTabToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.createDocumentInNewTabToolStripMenuItem.Size = new System.Drawing.Size(467, 26);
            this.createDocumentInNewTabToolStripMenuItem.Text = "Создание документа в новой вкладке";
            // 
            // savingCurrentDocumentToolStripMenuItem
            // 
            this.savingCurrentDocumentToolStripMenuItem.Name = "savingCurrentDocumentToolStripMenuItem";
            this.savingCurrentDocumentToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.savingCurrentDocumentToolStripMenuItem.Size = new System.Drawing.Size(467, 26);
            this.savingCurrentDocumentToolStripMenuItem.Text = "Сохранение текущего документа";
            // 
            // saveAllOpenDocumentsInWindowToolStripMenuItem
            // 
            this.saveAllOpenDocumentsInWindowToolStripMenuItem.Name = "saveAllOpenDocumentsInWindowToolStripMenuItem";
            this.saveAllOpenDocumentsInWindowToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.K)));
            this.saveAllOpenDocumentsInWindowToolStripMenuItem.Size = new System.Drawing.Size(467, 26);
            this.saveAllOpenDocumentsInWindowToolStripMenuItem.Text = "Сохранение всех открытых в окне документов";
            // 
            // openRtfToolStripMenuItem
            // 
            this.openRtfToolStripMenuItem.Name = "openRtfToolStripMenuItem";
            this.openRtfToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.openRtfToolStripMenuItem.Size = new System.Drawing.Size(467, 26);
            this.openRtfToolStripMenuItem.Text = "Открыть *.rtf";
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem,
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.selectAllToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(74, 24);
            this.editToolStripMenuItem.Text = "Правка";
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(255, 26);
            this.undoToolStripMenuItem.Text = "Отменить";
            // 
            // redoToolStripMenuItem
            // 
            this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            this.redoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.Z)));
            this.redoToolStripMenuItem.Size = new System.Drawing.Size(255, 26);
            this.redoToolStripMenuItem.Text = "Вернуть";
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("cutToolStripMenuItem.Image")));
            this.cutToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            this.cutToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.cutToolStripMenuItem.Size = new System.Drawing.Size(255, 26);
            this.cutToolStripMenuItem.Text = "Вырезать";
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("copyToolStripMenuItem.Image")));
            this.copyToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(255, 26);
            this.copyToolStripMenuItem.Text = "Копировать";
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("pasteToolStripMenuItem.Image")));
            this.pasteToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(255, 26);
            this.pasteToolStripMenuItem.Text = "Вставить";
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            this.selectAllToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(255, 26);
            this.selectAllToolStripMenuItem.Text = "Копировать всё";
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectedFontToolStripMenuItem,
            this.selectedCursiveFontToolStripMenuItem,
            this.selectedBoldFontToolStripMenuItem,
            this.selectedUnderlinedFontToolStripMenuItem,
            this.selectedStrikethroughFontToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(77, 24);
            this.toolsToolStripMenuItem.Text = "Формат";
            // 
            // selectedFontToolStripMenuItem
            // 
            this.selectedFontToolStripMenuItem.Name = "selectedFontToolStripMenuItem";
            this.selectedFontToolStripMenuItem.Size = new System.Drawing.Size(202, 26);
            this.selectedFontToolStripMenuItem.Text = "Выбрать шрифт";
            // 
            // selectedCursiveFontToolStripMenuItem
            // 
            this.selectedCursiveFontToolStripMenuItem.Name = "selectedCursiveFontToolStripMenuItem";
            this.selectedCursiveFontToolStripMenuItem.Size = new System.Drawing.Size(202, 26);
            this.selectedCursiveFontToolStripMenuItem.Text = "Курсив";
            // 
            // selectedBoldFontToolStripMenuItem
            // 
            this.selectedBoldFontToolStripMenuItem.Name = "selectedBoldFontToolStripMenuItem";
            this.selectedBoldFontToolStripMenuItem.Size = new System.Drawing.Size(202, 26);
            this.selectedBoldFontToolStripMenuItem.Text = "Жирный";
            // 
            // selectedUnderlinedFontToolStripMenuItem
            // 
            this.selectedUnderlinedFontToolStripMenuItem.Name = "selectedUnderlinedFontToolStripMenuItem";
            this.selectedUnderlinedFontToolStripMenuItem.Size = new System.Drawing.Size(202, 26);
            this.selectedUnderlinedFontToolStripMenuItem.Text = "Подчёркнутый";
            // 
            // selectedStrikethroughFontToolStripMenuItem
            // 
            this.selectedStrikethroughFontToolStripMenuItem.Name = "selectedStrikethroughFontToolStripMenuItem";
            this.selectedStrikethroughFontToolStripMenuItem.Size = new System.Drawing.Size(202, 26);
            this.selectedStrikethroughFontToolStripMenuItem.Text = "Зачёркнутый ";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.customizeThemeToolStripMenuItem,
            this.chooseAutosaveFrequencyToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(98, 24);
            this.helpToolStripMenuItem.Text = "Настройки";
            // 
            // customizeThemeToolStripMenuItem
            // 
            this.customizeThemeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.chooseDarkToolStripMenuItem,
            this.chooseLightToolStripMenuItem});
            this.customizeThemeToolStripMenuItem.Name = "customizeThemeToolStripMenuItem";
            this.customizeThemeToolStripMenuItem.Size = new System.Drawing.Size(325, 26);
            this.customizeThemeToolStripMenuItem.Text = "Настроить тему";
            // 
            // chooseDarkToolStripMenuItem
            // 
            this.chooseDarkToolStripMenuItem.Name = "chooseDarkToolStripMenuItem";
            this.chooseDarkToolStripMenuItem.Size = new System.Drawing.Size(248, 26);
            this.chooseDarkToolStripMenuItem.Text = "Выбрать тёмную тему";
            // 
            // chooseLightToolStripMenuItem
            // 
            this.chooseLightToolStripMenuItem.Name = "chooseLightToolStripMenuItem";
            this.chooseLightToolStripMenuItem.Size = new System.Drawing.Size(248, 26);
            this.chooseLightToolStripMenuItem.Text = "Выбрать светлую тему";
            // 
            // chooseAutosaveFrequencyToolStripMenuItem
            // 
            this.chooseAutosaveFrequencyToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveEveryTwoMinutesToolStripMenuItem,
            this.saveEveryTwenteenMinutesToolStripMenuItem,
            this.saveEveryHourToolStripMenuItem});
            this.chooseAutosaveFrequencyToolStripMenuItem.Name = "chooseAutosaveFrequencyToolStripMenuItem";
            this.chooseAutosaveFrequencyToolStripMenuItem.Size = new System.Drawing.Size(325, 26);
            this.chooseAutosaveFrequencyToolStripMenuItem.Text = "Выбрать частоту автосохранения";
            // 
            // saveEveryTwoMinutesToolStripMenuItem
            // 
            this.saveEveryTwoMinutesToolStripMenuItem.Name = "saveEveryTwoMinutesToolStripMenuItem";
            this.saveEveryTwoMinutesToolStripMenuItem.Size = new System.Drawing.Size(216, 26);
            this.saveEveryTwoMinutesToolStripMenuItem.Text = "Каждые 2 минуты";
            // 
            // saveEveryTwenteenMinutesToolStripMenuItem
            // 
            this.saveEveryTwenteenMinutesToolStripMenuItem.Name = "saveEveryTwenteenMinutesToolStripMenuItem";
            this.saveEveryTwenteenMinutesToolStripMenuItem.Size = new System.Drawing.Size(216, 26);
            this.saveEveryTwenteenMinutesToolStripMenuItem.Text = "Каждые 20 минут";
            // 
            // saveEveryHourToolStripMenuItem
            // 
            this.saveEveryHourToolStripMenuItem.Name = "saveEveryHourToolStripMenuItem";
            this.saveEveryHourToolStripMenuItem.Size = new System.Drawing.Size(216, 26);
            this.saveEveryHourToolStripMenuItem.Text = "Каждый час ";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog1_FileOk);
            // 
            // tabControl1
            // 
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 28);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(809, 432);
            this.tabControl1.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(809, 460);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.Name = "Form1";
            this.Text = "Блокнот ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openTxtToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem selectedFontToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem customizeThemeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem chooseDarkToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem chooseLightToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem selectedCursiveFontToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectedBoldFontToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectedUnderlinedFontToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectedStrikethroughFontToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createDocumentInNewWindowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createDocumentInNewTabToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem savingCurrentDocumentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAllOpenDocumentsInWindowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openRtfToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripMenuItem chooseAutosaveFrequencyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem everyTwoMinutes;
        private System.Windows.Forms.ToolStripMenuItem saveEveryTwenteenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveEveryHourToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveEveryTwoMinutesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveEveryTwenteenMinutesToolStripMenuItem;
    }
}

