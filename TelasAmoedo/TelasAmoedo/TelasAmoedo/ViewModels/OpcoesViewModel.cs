using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using TelasAmoedo.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TelasAmoedo.ViewModels
{
    public class OpcoesViewModel : ContentPage, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private bool isChecked;
        public bool IsChecked
        {
            get { return isChecked; }
            set
            {
                if (isChecked != value)
                {
                    isChecked = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("IsSelected"));
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
            MostrarDados();
            LogoutCommand = new Command(async () => await LogoutAsync());
            SalvarCommand = new Command(SalvarDados);
        }
        
        public void SalvarDados()
        {
            UsarBiometria = IsChecked;
        }
        public void MostrarDados()
        {
            IsChecked = UsarBiometria;
        }
        public async Task LogoutAsync()
        {
            Preferences.Clear();
            SecureStorage.RemoveAll();
            await Shell.Current.GoToAsync($"//{nameof(Login)}");
        }
    }
}
