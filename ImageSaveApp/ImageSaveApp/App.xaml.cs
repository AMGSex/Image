using System;
using Xamarin.Forms;
using ImageSaveApp.Pages;
using System.IO;
using ImageSaveApp.DataBase;

namespace ImageSaveApp
{
    public partial class App : Application
    {
        public const string DATABASE_NAME = "Images.db";
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new ImagesPage());
        }
        internal static ImageStorage db;
        internal static ImageStorage DB
        {
            get
            {
                if (db == null)
                {
                    db = new ImageStorage(
                        Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DATABASE_NAME));
                }
                return db;
            }
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
