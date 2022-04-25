using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using TelasAmoedo.Views;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TelasAmoedo.ViewModels
{
    public class OpcoesViewModel : ContentPage, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private bool _isToggled;
        public bool IsToggled
        {
            get { return _isToggled; }
            set
            {
                if (_isToggled != value)
                {
                    _isToggled = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("IsToggled"));
                    }
                }
            }
        }
        public bool UsarBiometria
        {
            get => Preferences.Get(nameof(UsarBiometria), false);
            set => Preferences.Set(nameof(UsarBiometria), value);
        }
        public ICommand LogoutCommand { get; set; }
        public ICommand SalvarCommand { get; set; }

        public OpcoesViewModel()
        {
            //MostrarDados();
            IsToggled = UsarBiometria;
            LogoutCommand = new Command(async () => await LogoutAsync());
            SalvarCommand = new Command(async () => await SalvarDados());
        }
        
        public async Task SalvarDados()
        {
            UsarBiometria = IsToggled;
            await App.Current.MainPage.DisplayToastAsync("Salvo com sucesso!", 2000);
        }
        //public void MostrarDados()
        //{
        //    IsToggled = UsarBiometria;
        //}
        public async Task LogoutAsync()
        {
            Preferences.Clear();
            SecureStorage.RemoveAll();
            await Shell.Current.GoToAsync($"//{nameof(Login)}");
        }
    }
}
