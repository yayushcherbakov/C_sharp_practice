using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace PeerNotepad
{
    // Класс, который нужен для сохранения настроек при перезагрузке.
    public class Settings
    {
        public List<string> OpenedFiles { get; set; }
        public int TimerInterval { get; set; }
        public Color BackgroundColor { get; set; }

        public Settings()
        {
            OpenedFiles = new List<string>();
            TimerInterval = 2000;
            BackgroundColor = Color.White;
        }
    }
}
