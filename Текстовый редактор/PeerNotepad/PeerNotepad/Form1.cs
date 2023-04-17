using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PeerNotepad
{

    public partial class Form1 : Form
    {
        private string settingsFileName = "setting.np";
        private Settings settings = new Settings();
        private RichTextBox selectedRichTextBox;

        public Form1()
        {
            InitializeComponent();
            ReadingSettingsOnReboot();

            // Настройка таймера автосохранения.
            this.timer1.Interval = settings.TimerInterval;
            timer1.Enabled = true;
            timer1.Tick += this.SavingCurrentDocumentToolStripMenuItemClicked;

            tabControl1.SelectedIndexChanged += new EventHandler(this.SelectedIndexChanged);

            var existingsFiles = new List<string>();
            if (settings.OpenedFiles.Count > 0)
            {

                foreach (var file in settings.OpenedFiles)
                {
                    if (File.Exists(file))
                    {
                        existingsFiles.Add(file);
                    }
                }
            }
            if (existingsFiles.Count > 0)
            {
                foreach (var file in existingsFiles)
                {
                    var newTab = new CustomTab();
                    tabControl1.TabPages.Add(newTab);
                    tabControl1.SelectedTab = newTab;
                    selectedRichTextBox = ((CustomTab)tabControl1.SelectedTab).TextBox;
                    selectedRichTextBox.ContextMenuStrip = contextMenuStrip1;
                    selectedRichTextBox.BackColor = settings.BackgroundColor;
                    OpenFileFromPath(file);
                }
            }
            else
            {
                CreateNewTextTab();
            }

            CreateMenuEvents();
            CreateContextMenuItemAndEvents();
        }

        // Считывание настроек при перезагрузке.
        private void ReadingSettingsOnReboot()
        {
            if (File.Exists(settingsFileName))
            {
                try
                {
                    settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(settingsFileName));
                }
                catch (Exception ex)
                {
                    settings = new Settings()
                    {
                        OpenedFiles = new List<string>(),
                        TimerInterval = 3000
                    };
                }
                finally
                {
                    if (settings is null)
                    {
                        settings = new Settings()
                        {
                            OpenedFiles = new List<string>(),
                            TimerInterval = 3000
                        };
                    }
                    if (settings.OpenedFiles is null)
                    {
                        settings.OpenedFiles = new List<string>();
                    }
                    if (settings.TimerInterval < 1000)
                    {
                        settings.TimerInterval = 1000;
                    }
                }
            }
        }

        //Создание контекстного меню и событий к его элементам.
        private void CreateContextMenuItemAndEvents()
        {
            // Создание элементов меню.
            ToolStripMenuItem chooseAllTextMenuItem = new ToolStripMenuItem("Выбрать весь текст");
            ToolStripMenuItem cutMenuItem = new ToolStripMenuItem("Вырезать выделенный фрагмент");
            ToolStripMenuItem copyMenuItem = new ToolStripMenuItem("Копировать выделенный фрагмент");
            ToolStripMenuItem pastMenuItem = new ToolStripMenuItem("Вставить сохранённый в буфере обмена");
            ToolStripMenuItem setFormatFragmentMenuItem = new ToolStripMenuItem("Задать формат выделенного фрагмента текста");

            // Задать формат выделенного фрагмента текста.
            setFormatFragmentMenuItem.Click += new EventHandler(this.SetFormatFragmentMenuItemClicked);

            // Вырезать выделенный фрагмент.
            cutMenuItem.Click += new EventHandler(this.CutMenuItemClicked);

            // Копировать выделенный фрагмент.
            copyMenuItem.Click += new EventHandler(this.CopyMenuItemClicked);

            // Вставить сохранённый в буфере обмена.
            pastMenuItem.Click += new EventHandler(this.PastMenuItemClicked);

            // Выделить весь текст.
            chooseAllTextMenuItem.Click += new EventHandler(this.СhooseAllTextMenuItemClicked);

            // Добавление элементов в меню.
            contextMenuStrip1.Items.AddRange(new[] { chooseAllTextMenuItem, cutMenuItem, copyMenuItem, pastMenuItem, setFormatFragmentMenuItem });

            // Привязка richTextBox1 к контекстному меню.
            selectedRichTextBox.ContextMenuStrip = contextMenuStrip1;
        }

        //Создание меню событий.
        private void CreateMenuEvents()
        {
            this.saveEveryTwoMinutesToolStripMenuItem.Click += new EventHandler(this.SaveEveryTwoMinutesToolStripMenuItemClicked);
            this.saveEveryHourToolStripMenuItem.Click += new EventHandler(this.SaveEveryHourToolStripMenuItemClicked);
            this.saveEveryTwenteenMinutesToolStripMenuItem.Click += new EventHandler(this.SaveEveryTwenteenMinutesToolStripMenuItemClicked);
            this.openTxtToolStripMenuItem.Click += new EventHandler(this.OpenToolStripMenuItemClicked);
            this.saveToolStripMenuItem.Click += new EventHandler(this.SaveToolStripMenuItemClicked);
            this.exitToolStripMenuItem.Click += new EventHandler(this.ExitToolStripMenuItemClicked);
            this.openRtfToolStripMenuItem.Click += new EventHandler(this.OpenRtfToolStripMenuItemClicked);
            this.createDocumentInNewWindowToolStripMenuItem.Click += new EventHandler(this.CreateDocumentInNewWindowToolStripMenuItemClicked);
            this.createDocumentInNewTabToolStripMenuItem.Click += new EventHandler(this.CreateDocumentInNewTabToolStripMenuItemClicked);
            this.savingCurrentDocumentToolStripMenuItem.Click += new EventHandler(this.SavingCurrentDocumentToolStripMenuItemClicked);
            this.saveAllOpenDocumentsInWindowToolStripMenuItem.Click += new EventHandler(this.SaveAllOpenDocumentsInWindowToolStripMenuItemClicked);
            this.chooseDarkToolStripMenuItem.Click += new EventHandler(this.ChooseDarkToolStripMenuItemClicked);
            this.chooseLightToolStripMenuItem.Click += new EventHandler(this.ChooseLightToolStripMenuItemClicked);
            this.selectedFontToolStripMenuItem.Click += new EventHandler(this.SetFormatFragmentMenuItemClicked);
            this.selectedCursiveFontToolStripMenuItem.Click += new EventHandler(this.SelectedCursiveFontToolStripMenuItemClicked);
            this.selectedBoldFontToolStripMenuItem.Click += new EventHandler(this.SelectedBoldFontToolStripMenuItemClicked);
            this.selectedUnderlinedFontToolStripMenuItem.Click += new EventHandler(this.SelectedUnderlinedFontToolStripMenuItemClicked);
            this.selectedStrikethroughFontToolStripMenuItem.Click += new EventHandler(this.SelectedStrikethroughFontToolStripMenuItemClicked);
            this.undoToolStripMenuItem.Click += new EventHandler(this.UndoToolStripMenuItemClicked);
            this.redoToolStripMenuItem.Click += new EventHandler(this.RedoToolStripMenuItemClicked);
            this.cutToolStripMenuItem.Click += new EventHandler(this.CutMenuItemClicked);
            this.copyToolStripMenuItem.Click += new EventHandler(this.CopyMenuItemClicked);
            this.pasteToolStripMenuItem.Click += new EventHandler(this.PastMenuItemClicked);
            this.selectAllToolStripMenuItem.Click += new EventHandler(this.СhooseAllTextMenuItemClicked);
        }

        // Автоматическое сохранение каждые 20 минут.
        private void SaveEveryTwenteenMinutesToolStripMenuItemClicked(object sender, EventArgs e)
        {
            try
            {
                this.timer1.Interval = 1_200_000;
            }
            catch(Exception ex) { }
        }

        // Автоматическое сохранение каждые 2 минуты.
        private void SaveEveryTwoMinutesToolStripMenuItemClicked(object sender, EventArgs e)
        {
            try
            {
                this.timer1.Interval = 120_000;
            }
            catch(Exception ex) { }
        }

        // Автоматическое сохранение каждый час.
        private void SaveEveryHourToolStripMenuItemClicked(object sender, EventArgs e)
        {
            try
            {
                this.timer1.Interval = 3_600_000;
            }
            catch(Exception ex) { }
        }

        // Создание новой вкладки.
        private void CreateNewTextTab()
        {
            try
            {
                var newTab = new CustomTab();
                tabControl1.TabPages.Add(newTab);
                tabControl1.SelectedTab = newTab;
                selectedRichTextBox = ((CustomTab)tabControl1.SelectedTab).TextBox;
                selectedRichTextBox.ContextMenuStrip = contextMenuStrip1;
            }
            catch (Exception ex) { }
        }

        // Сохранение всех открытых документов в окне.
        private void SaveAllOpenDocumentsInWindowToolStripMenuItemClicked(object sender, EventArgs e)
        {
            try
            {
                foreach (var tab in this.tabControl1.TabPages)
                {
                    SaveDocumentInCurrentTab((CustomTab)tab);
                }
            }
            catch (Exception ex) { }
        }

        // Сохранение текущего документа.
        private void SavingCurrentDocumentToolStripMenuItemClicked(object sender, EventArgs e)
        {
            try
            {
                foreach (var tab in this.tabControl1.TabPages)
                {
                    SaveDocumentInCurrentTab((CustomTab)tab);
                }
            }
            catch(Exception ex) { }
        }

        // Сохранение документа в текущей вкладке.
        private static void SaveDocumentInCurrentTab(CustomTab customTab)
        {
            try
            {
                // Сохранения безымянных документов. 
                if (string.IsNullOrWhiteSpace(customTab.FilePath))
                {
                    var isDefaultFileNameExist = File.Exists($"Новый документ.txt");
                    if (!isDefaultFileNameExist)
                    {
                        File.WriteAllText("Новый документ.txt", customTab.TextBox.Text);
                        customTab.FilePath = Path.Combine(Directory.GetCurrentDirectory(), "Новый документ.txt");
                        customTab.IsDocumentSaved = true;
                        return;
                    }
                    int i = 0;
                    while (isDefaultFileNameExist)
                    {
                        i++;
                        isDefaultFileNameExist = File.Exists($"Новый документ{i}.txt");
                    }
                    File.WriteAllText($"Новый документ{i}.txt", customTab.TextBox.Text);
                    customTab.FilePath = Path.Combine(Directory.GetCurrentDirectory(), $"Новый документ{i}.txt");
                    customTab.IsDocumentSaved = true;
                    return;
                }
                if (customTab.FilePath.ToLower().EndsWith(".rtf"))
                {
                    customTab.TextBox.SaveFile(customTab.FilePath);
                    customTab.IsDocumentSaved = true;
                    return;
                }
                File.WriteAllText(customTab.FilePath, customTab.TextBox.Text);
                customTab.IsDocumentSaved = true;
            }
            catch(Exception ex) { }
        }

        // Создание документа в новой вкладке.
        private void CreateDocumentInNewTabToolStripMenuItemClicked(object sender, EventArgs e)
        {
            try
            {
                CreateNewTextTab();
            }
            catch(Exception ex) { }
        }

        // Создание документа в новом окне.
        private void CreateDocumentInNewWindowToolStripMenuItemClicked(object sender, EventArgs e)
        {
            try
            {
                Form1 form = new Form1();
                form.Show();
            }
            catch(Exception ex) { }
        }

        // Отмена последнего действия. 
        private void UndoToolStripMenuItemClicked(object sender, EventArgs e)
        {
            try
            {
                selectedRichTextBox.Undo();
            }
            catch(Exception ex) { }
        }

        // Отмена отмены последнего действия.
        private void RedoToolStripMenuItemClicked(object sender, EventArgs e)
        {
            try
            {
                selectedRichTextBox.Redo();
            }
            catch(Exception ex) { }
        }

        // Копирование выделенного текста.
        private void CopyMenuItemClicked(object sender, EventArgs e)
        {
            try
            {
                selectedRichTextBox.Copy();
            }
            catch(Exception ex) { }
        }

        // Вставка сохранённого в буфере обмена текста.
        private void PastMenuItemClicked(object sender, EventArgs e)
        {
            try
            {
                selectedRichTextBox.Paste();
            }
            catch (Exception ex) { }
        }

        // Выделение всего текста в окне.
        private void СhooseAllTextMenuItemClicked(object sender, EventArgs e)
        {
            try
            {
                selectedRichTextBox.SelectAll();
            }
            catch (Exception ex) { }
        }

        // Обрезка выделенного текста.
        private void CutMenuItemClicked(object sender, EventArgs e)
        {
            try
            {
                selectedRichTextBox.Cut();
            }
            catch (Exception ex) { }
        }

        // Настройка формата выделенного фрагмента текста.
        private void SetFormatFragmentMenuItemClicked(object sender, EventArgs e)
        {
            try
            {
                FontDialog fontDialog = new FontDialog();

                fontDialog.ShowColor = true;
                fontDialog.MinSize = 8;
                fontDialog.MaxSize = 48;

                if (fontDialog.ShowDialog() == DialogResult.OK)
                {
                    selectedRichTextBox.SelectionFont = fontDialog.Font;
                    selectedRichTextBox.SelectionColor = fontDialog.Color;
                }
            }
           catch(Exception ex) { }
        }

        // Зачёркивание выделенного фрагмента текста.
        private void SelectedStrikethroughFontToolStripMenuItemClicked(object sender, EventArgs e)
        {
            try
            {
                selectedRichTextBox.SelectionFont = new Font(selectedRichTextBox.SelectionFont, FontStyle.Strikeout ^ selectedRichTextBox.SelectionFont.Style);
                selectedRichTextBox.Select();
            }
            catch(Exception ex) { }
        }

        // Выделение жирным шрифтом выделенного фрагмента текста.
        private void SelectedBoldFontToolStripMenuItemClicked(object sender, EventArgs e)
        {
            try
            {
                selectedRichTextBox.SelectionFont = new Font(selectedRichTextBox.SelectionFont, FontStyle.Bold ^ selectedRichTextBox.SelectionFont.Style);
                selectedRichTextBox.Select();
            }
            catch (Exception ex) { }
        }

        // Выделение курсивом выделенного фрагмента текста.
        private void SelectedCursiveFontToolStripMenuItemClicked(object sender, EventArgs e)
        {
            try
            {
                selectedRichTextBox.SelectionFont = new Font(selectedRichTextBox.SelectionFont, FontStyle.Italic ^ selectedRichTextBox.SelectionFont.Style);
                selectedRichTextBox.Select();
            }
            catch (Exception ex) { }
        }

        // Подчёркивание выделенного фрагмента текста.
        private void SelectedUnderlinedFontToolStripMenuItemClicked(object sender, EventArgs e)
        {
            try
            {
                selectedRichTextBox.SelectionFont = new Font(selectedRichTextBox.SelectionFont, FontStyle.Underline ^ selectedRichTextBox.SelectionFont.Style);
                selectedRichTextBox.Select();
            }
            catch (Exception ex) { }
        }

        // Открытие документа.
        private void OpenToolStripMenuItemClicked(object sender, EventArgs e)
        {
            try
            {
                this.openFileDialog1.ShowDialog();
            }
            catch(Exception ex) { }
        }

        // Изменение выбранного индекса.
        private void SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.selectedRichTextBox = ((CustomTab)tabControl1.SelectedTab).TextBox;
            }
            catch (Exception ex) { }
        }

        // Открытие rtf файла. 
        private void OpenRtfToolStripMenuItemClicked(object sender, EventArgs e)
        {
            try
            {
                this.openFileDialog1.Filter = "|*.rtf";
                this.openFileDialog1.ShowDialog();
                this.openFileDialog1.Filter = "";
            }
            catch (Exception ex) { }
        }

        // Сохранение файла. 
        private void SaveToolStripMenuItemClicked(object sender, EventArgs e)
        {
            try
            {
                this.saveFileDialog1.ShowDialog();
            }
            catch (Exception ex) { }
        }

        // Закрытие вкладок. 
        private void ExitToolStripMenuItemClicked(object sender, EventArgs e)
        {
            try
            {
                // Вызов предложения сохраниенить несохранённый документ.
                if (((CustomTab)this.tabControl1.SelectedTab).IsDocumentSaved == false)
                {
                    switch (MessageBox.Show($"Вы хотите сохранить изменения в текущем документе?", "Блокнот", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation))
                    {
                        case DialogResult.Yes:
                            var customTab = (CustomTab)this.tabControl1.SelectedTab;
                            SaveDocumentInCurrentTab(customTab);
                            break;
                        case DialogResult.No:
                            break;
                        case DialogResult.Cancel:
                            return;
                    }
                }

                if (tabControl1.TabCount == 1)
                {
                    Application.Exit();
                    return;
                }

                tabControl1.TabPages.Remove(this.tabControl1.SelectedTab);
            }
            catch (Exception ex) { }
        }

        // Изменение темы на тёмную.
        private void ChooseDarkToolStripMenuItemClicked(object sender, EventArgs e)
        {
            try
            {
                foreach (var tab in this.tabControl1.TabPages)
                {
                    ((CustomTab)tab).TextBox.BackColor = Color.DarkGray;
                }
                if (!(settings is null)) settings.BackgroundColor = Color.DarkGray;
            }
            catch (Exception ex) { }
        }

        // Изменение темы на светлую.
        private void ChooseLightToolStripMenuItemClicked(object sender, EventArgs e)
        {
            try
            {
                foreach (var tab in this.tabControl1.TabPages)
                {
                    ((CustomTab)tab).TextBox.BackColor = Color.White;
                }
                if (!(settings is null)) settings.BackgroundColor = Color.White;
            }
            catch(Exception ex) { }
        }

        // Открытие окна файлового диалога.
        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            try
            {
                if (!(sender is OpenFileDialog))
                {
                    return;
                }
                var dialog = sender as OpenFileDialog;

                var fileName = dialog.FileName;

                OpenFileFromPath(fileName);
            }
            catch (Exception ex) { }
        }

        // Открытие файла через его путь.
        private void OpenFileFromPath(string fileName)
        {
            try
            {
                if (fileName.ToLower().EndsWith(".rtf"))
                {
                    selectedRichTextBox.LoadFile(fileName);
                    SetFilePathToTab(fileName);
                    return;
                }

                this.selectedRichTextBox.Text = File.ReadAllText(fileName);
                SetFilePathToTab(fileName);
            }
            catch (Exception ex) { }
        }

        // Сохранение файла в окне файлового диалога.
        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            try
            {
                if (!(sender is SaveFileDialog))
                {
                    return;
                }
                var dialog = sender as SaveFileDialog;
                var fileName = dialog.FileName;
                selectedRichTextBox.SaveFile(fileName);
                SetFilePathToTab(fileName);
            }
            catch (Exception ex) { }
        }

        // Записать путь к файлу ко вкладке. 
        private void SetFilePathToTab(string fileName)
        {
            try
            {
                ((CustomTab)this.tabControl1.SelectedTab).FilePath = fileName;
                ((CustomTab)this.tabControl1.SelectedTab).IsDocumentSaved = true;
            }
            catch (Exception ex) { }
        }

        // Закрытие приложения. 
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                // Вызов предложения сохраниенить несохранённый документ.
                if (((CustomTab)this.tabControl1.SelectedTab).IsDocumentSaved == false)
                {
                    switch (MessageBox.Show($"Вы хотите сохранить изменения в текущем документе?", "Блокнот", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation))
                    {
                        case DialogResult.Yes:
                            foreach (var tab in this.tabControl1.TabPages)
                            {
                                SaveDocumentInCurrentTab((CustomTab)tab);
                            }
                            break;
                        case DialogResult.No:
                            break;
                        case DialogResult.Cancel:
                            e.Cancel = true;
                            return;
                    }
                }

                // Записть всех незакрытых в приложении файлов. 
                if (settings is null) settings = new Settings();
                settings.OpenedFiles = new List<string>();
                foreach (var tab in this.tabControl1.TabPages)
                {
                    settings.OpenedFiles.Add(((CustomTab)tab).FilePath);
                }
                settings.TimerInterval = this.timer1.Interval;

                File.WriteAllText(settingsFileName, JsonConvert.SerializeObject(settings));
            }
            catch (Exception ex) { }
        }
    }
}
