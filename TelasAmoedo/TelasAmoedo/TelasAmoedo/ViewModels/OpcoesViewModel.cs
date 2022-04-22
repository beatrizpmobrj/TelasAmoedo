using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TelasAmoedo.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TelasAmoedo.ViewModels
{
    public class OpcoesViewModel
    {
        public bool UsarBiometria
        {
            get => Preferences.Get(nameof(UsarBiometria), false);
            set => Preferences.Set(nameof(UsarBiometria), value);
        }
        public ICommand LogoutCommand { get; set; }        
        public ICommand UsarBiometriaCommand { get; set; }        

        public OpcoesViewModel()
        {
            LogoutCommand = new Command(async () => await LogoutAsync());
            UsarBiometriaCommand = new Command(UsaBiometria);
        }

        public void UsaBiometria()
        {
            if (UsarBiometria)
            {
                UsarBiometria = false;
            }
            else
            {
                UsarBiometria = true;
            }
        }

        public async Task LogoutAsync()
        {
            Preferences.Clear();
            SecureStorage.RemoveAll();
            await Shell.Current.GoToAsync($"//{nameof(Login)}");
        }
    }
}
