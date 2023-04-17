using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PeerNotepad
{
    // Класс, который позволяет создавать новые вкладки в приложении. 
    public class CustomTab : TabPage
    {
        public RichTextBox TextBox;
        public string FilePath = string.Empty;

        private bool _isDocumentSaved = true;
        public bool IsDocumentSaved
        {
            get
            {
                return _isDocumentSaved;
            }
            set
            {
                _isDocumentSaved = value;
                if(string.IsNullOrWhiteSpace(FilePath))
                {
                    this.Text = value ? "Новый документ" : "Новый документ(*)";
                    return;
                }
                this.Text = value ? Path.GetFileName(FilePath) : $"{Path.GetFileName(FilePath)}(*)";
            }
        }
            
        public CustomTab()
        {
            TextBox = new RichTextBox();
            this.Controls.Add(TextBox);
            TextBox.Dock = DockStyle.Fill;
            this.Text = "Новый документ";

            this.TextBox.TextChanged += new EventHandler(this.RichTextBoxTextChanged);
        }

        private void RichTextBoxTextChanged(object sender, EventArgs e)
        {
            this.IsDocumentSaved = false;

        }
    }
}
