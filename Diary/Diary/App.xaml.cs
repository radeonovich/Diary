using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Diary.Data;
using System.IO;
using Diary.Views;
using Xamarin.Essentials;

namespace Diary
{
    public partial class App : Application
    {
        static DiaryDB diaryDB;

        public static DiaryDB DiaryDB
        {
            get
            {
                // паттерн Singleton
                // Новая таблица создается, только если ее нет
                if (diaryDB == null)
                {
                    diaryDB = new DiaryDB(
                        // получение пути к БД (путь зависит от платформы Android/iOS)
                        Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "NotesDatabase.db3"
                            )
                        );

                }
                return diaryDB;
            }
        }

        public App()
        {
            InitializeComponent();
            Preferences.Set("is_authorized", "false");
            MainPage = new AppShell();
            //MainPage = new AppShell();
            
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
