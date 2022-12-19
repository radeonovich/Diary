using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Diary.DBItemModel
{
    public class Note
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Text {get; set; }
        public string TextPreview { get; set; }
        public DateTime Date { get; set; }
        public string DateString { get; set; }
    }
}
