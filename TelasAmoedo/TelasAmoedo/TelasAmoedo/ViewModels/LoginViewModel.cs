using Plugin.Fingerprint;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Plugin.Fingerprint.Abstractions;
using System.Windows.Input;
using Xamarin.Forms;

namespace TelasAmoedo.ViewModels
{
    public class LoginViewModel : Shell
    {
        public ICommand LeitorBiometricoCommand { get; set; }

        public LoginViewModel()
        {
            LeitorBiometricoCommand = new Command(IsDigitalValidada);
        }

        private async void IsDigitalValidada()
        {
            var availability = await CrossFingerprint.Current.IsAvailableAsync();

            if(!availability)
            {
                await DisplayAlert("Erro!", "Leitor biométrico não disponível.", "OK");
                return;
            }

            var authResult = await CrossFingerprint.Current.AuthenticateAsync(
                new AuthenticationRequestConfiguration("Acesso Biométrico", "Confirme sua impressão digital para acessar sua conta."));

            if (authResult.Authenticated)
            {
                await DisplayAlert("Pronto!", "Acesso liberado!", "OK");
            }
        }
    }
}
