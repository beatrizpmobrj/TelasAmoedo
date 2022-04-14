using Plugin.Fingerprint;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Plugin.Fingerprint.Abstractions;
using System.Windows.Input;
using Xamarin.Forms;
using System.ComponentModel;
using TelasAmoedo.Views;

namespace TelasAmoedo.ViewModels
{
    public class LoginViewModel : ContentPage, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _email;
        public string Email
        {
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
        public string Senha
        {
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
        public ICommand CadastroCommand { get; set; }
        public ICommand SalvarLoginCommand { get; set; }

        public LoginViewModel()
        {
            LeitorBiometricoCommand = new Command(async () => await IsDigitalValidadaAsync());
            MostrarLoginCommand = new Command(async () => await MostrarLoginAsync());
            LoginCommand = new Command(async () => await LoginAsync());
            CadastroCommand = new Command(async () => await CadastroAsync());
            SalvarLoginCommand = new Command(async () => await SalvarLogin()); // Icone do Face Id, apenas para teste
        }

        private async Task CadastroAsync()
        {
            await Shell.Current.GoToAsync("cadastropage");
        }

        private async Task LoginAsync()
        {
            Email = "email@mobrj.br";
            Senha = "123456";

            await ValidarLogin(Email, Senha);
        }

        private async Task IsDigitalValidadaAsync()
        {
            var availability = await CrossFingerprint.Current.IsAvailableAsync();

            var authenticationType = await CrossFingerprint.Current.GetAuthenticationTypeAsync();

            if (!availability)
            {
                await App.Current.MainPage.DisplayAlert("Erro!", "Leitor biométrico não disponível.", "OK");
                return;
            }

            var authResult = await CrossFingerprint.Current.AuthenticateAsync(
                new AuthenticationRequestConfiguration("Acesso Biométrico", "Confirme sua impressão digital para acessar sua conta."));

            if (authResult.Authenticated)
            {
                await App.Current.MainPage.DisplayAlert("Pronto!", "Acesso liberado!", "OK");

                await Shell.Current.GoToAsync("confirmacaotelefonepage");
            }
        }
        private async Task MostrarLoginAsync()
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
        private async Task SalvarLogin()
        {
            await Xamarin.Essentials.SecureStorage.SetAsync("email", Email);
            await Xamarin.Essentials.SecureStorage.SetAsync("senha", Senha);
            await App.Current.MainPage.DisplayAlert("Pronto!", "Login salvo com sucesso!", "OK");
        }
        public async Task ValidarLogin(string email, string senha)
        {
            string emailValido = "email@mobrj.br";
            string senhaValida = "123456";

            if (emailValido == email & senhaValida == senha)
            {
                var answer = await App.Current.MainPage.DisplayAlert("Alerta!", "Deseja usar biometria?", "Sim", "Não");
                if (answer)
                {
                    var authResult = await CrossFingerprint.Current.AuthenticateAsync(
                        new AuthenticationRequestConfiguration("Acesso Biométrico", "Confirme sua impressão digital para acessar sua conta."));

                    if (authResult.Authenticated)
                    {
                        await SalvarLogin();
                        await Shell.Current.GoToAsync($"//{nameof(MenuPrincipal)}");
                    }
                }
                else
                    await Shell.Current.GoToAsync($"//{nameof(MenuPrincipal)}");
            }
        }
    }
}
