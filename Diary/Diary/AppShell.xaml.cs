using Xamarin.Forms;
using Diary.Views;

namespace Diary
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            // Добавление маршрутов для навигации через TabBar
            Routing.RegisterRoute(nameof(NoteAddingPage), typeof(NoteAddingPage));
            Routing.RegisterRoute(nameof(NotesPage), typeof(NotesPage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
        }
    }
}