using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using System.Diagnostics;

namespace ImageSaveApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ImagesPage : ContentPage
    {
        public string pathName;
        public ImagesPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            imgList.ItemsSource = App.DB.GetItems();
            base.OnAppearing();
        }
        void UpdateList()
        {
            imgList.ItemsSource = App.DB.GetItems();
        }

        private async void BtnOpenCamera_Clicked(object sender, EventArgs e)
        {
            try
            {
                var photo = await MediaPicker.CapturePhotoAsync(new MediaPickerOptions
                {
                    Title = $"xamarin.{DateTime.Now.ToString("dd.MM.yyyy_hh.mm.ss")}.png"
                });

                var newFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), photo.FileName);
                using (var stream = await photo.OpenReadAsync())
                using (var newStream = File.OpenWrite(newFile))
                    await stream.CopyToAsync(newStream);

                Debug.WriteLine($"Путь фото {photo.FullPath}");

                pathName = photo.FullPath;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Сообщение об ошибке", ex.Message, "OK");
            }
        }

        private async void BtnOpenGallery_Clicked(object sender, EventArgs e)
        {
            try
            {
                var photo = await MediaPicker.PickPhotoAsync();
                pathName = photo.FullPath;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Сообщение об ошибке", ex.Message, "OK");
            }
        }

        private void BtnAddImage_Clicked(object sender, EventArgs e)
        {
            DataBase.Image img = new DataBase.Image();
            img.Name = ImageName.Text;
            img.ImagePath = pathName;
            App.DB.SaveItem(img);
            UpdateList();
        }

        private async void imgList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            DataBase.Image selectedImage = (DataBase.Image)e.SelectedItem;
            SelectedImagePage imagePage = new SelectedImagePage();
            imagePage.BindingContext = selectedImage;
            await Navigation.PushAsync(imagePage);
        }
    }
}