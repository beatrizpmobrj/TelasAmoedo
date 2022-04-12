using Plugin.Fingerprint;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Plugin.Fingerprint.Abstractions;
using System.Windows.Input;
using Xamarin.Forms;
using System.ComponentModel;

namespace TelasAmoedo.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _email;
        public string Email { 
            get 
            {
                return _email;
            }
            set
            {
                _email = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Email"));
            }
        }
        private string _senha;
        public string Senha { 
            get
            {
                return _senha;
            }
            set
            {
                _senha = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Senha"));
            }
        }
        public ICommand LeitorBiometricoCommand { get; set; }
        public ICommand MostrarLoginCommand { get; set; }
        public ICommand LoginCommand { get; set; }

        public LoginViewModel()
        {
            LeitorBiometricoCommand = new Command(IsDigitalValidada);
            MostrarLoginCommand = new Command(MostrarLogin);
            LoginCommand = new Command(Login);
        }
        private async void Login()
        {
            await Xamarin.Essentials.SecureStorage.SetAsync("email", Email);
            await Xamarin.Essentials.SecureStorage.SetAsync("senha", Senha);
            await App.Current.MainPage.DisplayAlert("Pronto!", "Login salvo com sucesso!", "OK");
        }

        private async void IsDigitalValidada()
        {
            var availability = await CrossFingerprint.Current.IsAvailableAsync();

            if(!availability)
            {
                await App.Current.MainPage.DisplayAlert("Erro!", "Leitor biométrico não disponível.", "OK");
                return;
            }

            var authResult = await CrossFingerprint.Current.AuthenticateAsync(
                new AuthenticationRequestConfiguration("Acesso Biométrico", "Confirme sua impressão digital para acessar sua conta."));

            if (authResult.Authenticated)
            {
                await App.Current.MainPage.DisplayAlert("Pronto!", "Acesso liberado!", "OK");
            }
        }
        private async void MostrarLogin()
        {
            try
            {
                _email = await Xamarin.Essentials.SecureStorage.GetAsync("email");
                _senha = await Xamarin.Essentials.SecureStorage.GetAsync("senha");
            }
            catch (Exception)
            {
                await App.Current.MainPage.DisplayAlert("Erro!", "Não encontrado!", "OK");
            }
            await App.Current.MainPage.DisplayAlert("Seu login é:", $"Email: {Email}\r\n Senha: {Senha}", "OK");
        }
    }
}
