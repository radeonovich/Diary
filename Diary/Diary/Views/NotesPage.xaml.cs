using System;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Diary.DBItemModel;
using Xamarin.Essentials;

namespace Diary.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotesPage : ContentPage
    {
        
        public NotesPage()
        {
            InitializeComponent();
        }
        
        protected override async void OnAppearing()
        {
            if (Preferences.Get("is_authorized", "false") == "true")
            // флаг, который не дает зайти в записи без авторизации
            {
                collectionView.ItemsSource = await App.DiaryDB.GetNotesAsync();
                base.OnAppearing();
            }
            else
            {
                await DisplayAlert("Ошибка", "Записи скрыты. Сначала авторизуйтесь!", "Ок");
                await Shell.Current.GoToAsync("LoginPage");
            }
            
        }
        private async void AddButton_Clicked(object sender, EventArgs e)
        {
            // Добавить запись
            await Shell.Current.GoToAsync(nameof(NoteAddingPage));
        }

        private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                Note note = (Note)e.CurrentSelection.FirstOrDefault();
                await Shell.Current.GoToAsync(
                    $"{nameof(NoteAddingPage)}?{nameof(NoteAddingPage.ItemId)}={note.ID.ToString()}");
            }
        }
    }
}