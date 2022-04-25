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
using TelasAmoedo.Services;
using Xamarin.CommunityToolkit;
using Xamarin.CommunityToolkit.Behaviors;

namespace TelasAmoedo.ViewModels
{
    public class LoginViewModel : ContentPage, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public bool LembrarUsuario
        {
            get => Preferences.Get(nameof(LembrarUsuario), false);
            set => Preferences.Set(nameof(LembrarUsuario), value);
        }
        public string Usuario
        {
            get => Preferences.Get(nameof(Usuario), string.Empty);
            set => Preferences.Set(nameof(Usuario), value);
        }
        public bool UsarBiometria;
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
        public ICommand MostrarLoginCommand { get; set; }
        public ICommand LoginCommand { get; set; }
        public ICommand CadastroCommand { get; set; }
        public ICommand SalvarLoginCommand { get; set; }

        public LoginViewModel()
        {
            if (LembrarUsuario == true)
            {
                Task.Run(async () => await MostrarLoginAsync());
            }
            Task.Run(async () => await LoginAsync());

            LoginCommand = new Command(async () => await LoginAsync());
            CadastroCommand = new Command(async () => await CadastroAsync());
        }

        private async Task CadastroAsync()
        {
            await Shell.Current.GoToAsync("confirmacaotelefonepage");
        }

        private async Task LoginAsync()
        {
            string emailValido = "email@mobrj.br";
            string _emailSecureStorage = await Xamarin.Essentials.SecureStorage.GetAsync("email");
            string _senhaSecureStorage = await Xamarin.Essentials.SecureStorage.GetAsync("senha");
            string _isPrimeiroAcesso = await Xamarin.Essentials.SecureStorage.GetAsync("isPrimeiroAcesso");

            if (!String.IsNullOrWhiteSpace(Email) && !String.IsNullOrWhiteSpace(Senha)) // Usuário passa email e senha
            {
                if (Email != _emailSecureStorage) // Se email não for o do secure storage, perguntar se quer salvar
                {
                    var availability = await CrossFingerprint.Current.IsAvailableAsync();

                    if (availability) //Se possuir viabilidade de biometria
                    {
                        var reposta = await App.Current.MainPage.DisplayAlert("Alerta!", "Deseja usar biometria para entrar na próxima vez?", "Sim", "Não");

                        if (reposta) // Se o usuário quiser salvar o login
                        {
                            Preferences.Set(nameof(UsarBiometria), true);
                            await SalvarLogin();
                            await ValidarLogin(Email, Senha);
                        }
                        else // Se o usuário não quiser salvar o login
                        {
                            LembrarUsuario = false;
                            await ValidarLogin(Email, Senha);
                        }
                    }
                    else //Se não possuir viabilidade de biometria
                    {
                        var reposta = await App.Current.MainPage.DisplayAlert("Alerta!", "Deseja salvar seu email para entrar na próxima vez?", "Sim", "Não");
                        if (reposta)
                        {
                            await SalvarLogin();
                            await ValidarLogin(Email, Senha);
                        }
                        else
                        {
                            LembrarUsuario = false;
                            await ValidarLogin(Email, Senha);
                        }
                    }
                }
                else // Se passar email e senha e o email for o do secure storage
                {
                    await ValidarLogin(Email, Senha);
                }
            }
            else if (Email == _emailSecureStorage || Email == emailValido) // Se o email for igual ao secure storage e validado pela api, verificar se possui biometria
            {
                var availability = await CrossFingerprint.Current.IsAvailableAsync();

                if (availability) // Se possuir biometria, logar com ela
                {
                    // ToDo - Verificar se o usuário optou por logar com biometria
                    var _usarBiometria = Preferences.Get(nameof(UsarBiometria), false);

                    if (_usarBiometria)
                    {
                        var authResult = await CrossFingerprint.Current.AuthenticateAsync(
                            new AuthenticationRequestConfiguration("Acesso Biométrico", "Confirme sua impressão digital para acessar sua conta."));

                        if (authResult.Authenticated) // Caso não seja autenticado o próprio pacote possui uma mensagem
                        {
                            await Shell.Current.GoToAsync($"//{nameof(MenuPrincipal)}");
                        }
                    }
                    else // Se o usuário optou por não usar biometria para logar
                    {
                        await ValidarLogin(Email, Senha);
                    }


                }
                else // Se não possuir biometria, entrar com login e senha
                {
                    await ValidarLogin(Email, Senha);
                }
            }
            else
            {
                await ValidarLogin(Email, Senha);
            }
        }

        //private async Task IsDigitalValidadaAsync()
        //{
        //    var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();

        //    if (status != PermissionStatus.Granted)
        //        status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();

        //    if (status != PermissionStatus.Granted) // Se por qualquer motivo o status for diferente de Granted; caso seja negado o uso
        //        return;

        //    var availability = await CrossFingerprint.Current.IsAvailableAsync();

        //    var authenticationType = await CrossFingerprint.Current.GetAuthenticationTypeAsync(); // Pegando o tipo para posterior mensagem personalizada

        //    if (!availability)
        //    {
        //        await App.Current.MainPage.DisplayToastAsync("Leitor biométrico não disponível.", 5000);
        //        return;
        //    }

        //    var authResult = await CrossFingerprint.Current.AuthenticateAsync(
        //        new AuthenticationRequestConfiguration("Acesso Biométrico", "Confirme sua impressão digital para acessar sua conta."));

        //    if (authResult.Authenticated)
        //    {
        //        await App.Current.MainPage.DisplayToastAsync("Acesso liberado!", 5000);
        //        await Shell.Current.GoToAsync("confirmacaotelefonepage");
        //    }
        //}
        private async Task<string> MostrarLoginAsync()
        {
            try
            {
                _email = await Xamarin.Essentials.SecureStorage.GetAsync("email");
            }
            catch (Exception)
            {
                _email = "";
            }
            Email = _email;
            return _email;
        }
        private async Task SalvarLogin()
        {
            await SecureStorage.SetAsync("email", Email);
            await SecureStorage.SetAsync("senha", Senha);
            LembrarUsuario = true;
        }
        public async Task ValidarLogin(string email, string senha)
        {
            string emailValido = "email@mobrj.br";
            string senhaValida = "123456";

            if (!String.IsNullOrWhiteSpace(email) && !String.IsNullOrWhiteSpace(senha))
            {
                if (emailValido == email && senhaValida == senha)
                {
                    await App.Current.MainPage.DisplayToastAsync("Acesso liberado!", 2000);
                    await Shell.Current.GoToAsync($"//{nameof(MenuPrincipal)}");
                }
                else
                {
                    await App.Current.MainPage.DisplayToastAsync("Email e/ou senha inválidos", 3000);
                }
            }
            else
            {
                await App.Current.MainPage.DisplayToastAsync("Digite seu email e senha.", 3000);
            }
        }
    }
}
