using Plugin.Fingerprint;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Plugin.Fingerprint.Abstractions;
using System.Windows.Input;
using Xamarin.Forms;
using System.ComponentModel;
using TelasAmoedo.Views;
using Xamarin.Essentials;
using Xamarin.CommunityToolkit.Extensions;

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
            await ValidarLogin(Email, Senha);
        }

        private async Task IsDigitalValidadaAsync()
        {
            var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();

            if(status != PermissionStatus.Granted)
                status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();

            if (status != PermissionStatus.Granted) // Se por qualquer motivo o status for diferente de Granted; caso seja negado o uso
                return;

            var availability = await CrossFingerprint.Current.IsAvailableAsync();

            var authenticationType = await CrossFingerprint.Current.GetAuthenticationTypeAsync(); // Pegando o tipo para posterior mensagem personalizada

            if (!availability)
            {
                await App.Current.MainPage.DisplayToastAsync("Leitor biométrico não disponível.", 5000);
                return;
            }

            var authResult = await CrossFingerprint.Current.AuthenticateAsync(
                new AuthenticationRequestConfiguration("Acesso Biométrico", "Confirme sua impressão digital para acessar sua conta."));

            if (authResult.Authenticated)
            {
                await App.Current.MainPage.DisplayToastAsync("Acesso liberado!", 5000);
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
                await App.Current.MainPage.DisplayToastAsync("Não encontrado!", 5000);
            }
            await App.Current.MainPage.DisplayAlert("Seu login é:", $"Email: {Email}\r\n Senha: {Senha}", "OK");
        }
        private async Task SalvarLogin()
        {
            await Xamarin.Essentials.SecureStorage.SetAsync("email", Email);
            await Xamarin.Essentials.SecureStorage.SetAsync("senha", Senha);
            await App.Current.MainPage.DisplayToastAsync("Login salvo com sucesso!", 5000);

        }
        public async Task ValidarLogin(string email, string senha)
        {
            string emailValido = "email@mobrj.br";
            string senhaValida = "123456";

            if (emailValido == email & senhaValida == senha)
            {
                var reposta = await App.Current.MainPage.DisplayAlert("Alerta!", "Deseja usar biometria?", "Sim", "Não");
                if (reposta)
                {
                    var availability = await CrossFingerprint.Current.IsAvailableAsync();

                    if (!availability)
                    {
                        await App.Current.MainPage.DisplayToastAsync("Leitor biométrico não disponível.", 5000);

                        return;
                    }
                    var authResult = await CrossFingerprint.Current.AuthenticateAsync(
                        new AuthenticationRequestConfiguration("Acesso Biométrico", "Confirme sua impressão digital para acessar sua conta."));

                    if (authResult.Authenticated)
                    {
                        await this.DisplayToastAsync("Acesso liberado!", 5000);
                        await SalvarLogin();
                        await Shell.Current.GoToAsync($"//{nameof(MenuPrincipal)}");
                    }
                }
                else
                    await Shell.Current.GoToAsync($"//{nameof(MenuPrincipal)}");
            }
            else
            {
                await App.Current.MainPage.DisplayToastAsync("Email e/ou senha inválidos", 5000);
            }
        }
    }
}
