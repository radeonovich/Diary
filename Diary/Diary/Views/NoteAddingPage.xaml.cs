using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Diary.DBItemModel;
using Xamarin.Essentials;

namespace Diary.Views
{
    [QueryProperty(nameof(ItemId),nameof(ItemId))]
    public partial class NoteAddingPage : ContentPage
    {
        public string ItemId
        {
            set
            {
                LoadNote(value);
            }
        }

        public NoteAddingPage()
        {
            InitializeComponent();

            BindingContext = new Note();
        }

        private async void LoadNote(string value)
        {
            try
            {
                int id = Convert.ToInt32(value);
                Note note = await App.DiaryDB.GetNoteAsync(id); // получаем запись из БД
                BindingContext = note; // загрузка записи в интерфейс
            }
            catch { }
        }

        private async void OnSave_Clicked(object sender, EventArgs e)
        {
            Note note = (Note)BindingContext; // получение записи из интерфейса
            note.Date = DateTime.UtcNow;
            note.DateString = note.Date.ToString("dddd, dd MMMM yyyy"); // конвертация в отображаемый формат
            if (!string.IsNullOrWhiteSpace(note.Text)) // если текст не пустой, добавляем запись в БД
            {
                if (note.Text.Length > 100) // ограничивает предпросмотр содержимого записи до 100 символов
                    // и убирает переносы из превью
                {
                    note.TextPreview = (note.Text.Substring(0, 100) + "...").Replace("\n", " ").Replace("\r"," ");
                }
                else
                {
                    note.TextPreview = note.Text.Replace("\n", " ").Replace("\r", " ");
                }
                await App.DiaryDB.SaveNoteAsync(note);
            }
            await Shell.Current.GoToAsync(".."); // возвращение к списку записей
        }

        private async void OnDelete_Clicked(object sender, EventArgs e)
        {
            Note note = (Note)BindingContext; // получение записи из интерфейса
            await App.DiaryDB.DeleteNoteAsync(note);
            await Shell.Current.GoToAsync(".."); // возвращение к списку записей
        }
    }
}