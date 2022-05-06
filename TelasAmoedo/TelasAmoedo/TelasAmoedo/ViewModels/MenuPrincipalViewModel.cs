using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TelasAmoedo.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TelasAmoedo.ViewModels
{
    public class MenuPrincipalViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand AvancarCampanhas { get; set; }
        public ICommand AvancarVoucher { get; set; }
        public ICommand AvancarResgate { get; set; }
        public ICommand AvancarExtrato { get; set; }
        public ICommand UploadImage { get; set; }
        public ICommand GetImage { get; set; }
        public ICommand SaveImage { get; set; }

        public string _imagePath = "profile.png"; //Perfil de imagem em branco

        public string ImageRecord
        {

            get => Preferences.Get(nameof(ImageRecord), _imagePath);
            set => Preferences.Set(nameof(ImageRecord), value);

        }

        public MenuPrincipalViewModel()
        {
            AvancarCampanhas = new Command(async () => await RedirectToMenu("menucampanhas"));
            AvancarVoucher = new Command(async () => await RedirectToMenu("menuvoucher"));
            AvancarResgate = new Command(async () => await RedirectToMenu("resgate"));
            AvancarExtrato = new Command(async () => await RedirectToMenu("extrato"));
            UploadImage = new Command(async () => await ChangeImage());
            GetImage = new Command(async () => await CaptureImage());
            SaveImage = new Command(async () => await SalvarImage());

            ImagePath = ImageRecord;

        }

        //Método para associar via command e salvar a imagem escolhida como definitiva
        public async Task SalvarImage()
        {
            ImageRecord = ImagePath;
        }


        private async Task RedirectToMenu(string menu)
        {
            await Shell.Current.GoToAsync(menu);
        }

        private async Task CaptureImage()
        {
            var photo = await MediaPicker.CapturePhotoAsync();
            var stream = await LoadPhotoAsync(photo);
        }

        private async Task ChangeImage()
        {
            var photo = await MediaPicker.PickPhotoAsync();
            var stream = await LoadPhotoAsync(photo);
          

        }

        async Task<Stream> LoadPhotoAsync(FileResult photo)
        {
            if (photo == null)
            {
                return null;
            }

            var newFile = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
            var stream = await photo.OpenReadAsync();
            ImagePath = photo.FullPath;
            return stream;
        }


        //Determina onde a imagem deve ser carregada na view
        public string ImagePath
        {
            get { return _imagePath; }

            set
            {
                if (_imagePath != value)
                {
                    _imagePath = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("ImagePath"));
                    }
                   ;

                }

            }
        }

    }
}
